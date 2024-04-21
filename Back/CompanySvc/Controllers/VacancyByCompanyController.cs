using System.Security.Claims;
using CompanySvc.Repositories;
using Contracts;
using Contracts.Events.Messages.CreatingBoxEvents;
using Contracts.Events.ResponseOnVacancyEvents;
using Contracts.VacancyEvent;
using CustomExceptions._400s;
using GlobalHelpers;
using GlobalHelpers.Models;
using GlobalModels.Messages.CompanyResponse;
using GlobalModels.Vacancy;
using MassTransit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VacancyService.Models;

namespace CompanySvc.Controllers;

[Authorize]
[ApiController]
[Route("VacancyByCompany")]
public class VacancyByCompanyController(
    IPublishEndpoint publisher,
    ICompanyRepo companyRepo,
    IRequestClient<AddVacancyEvent> requestAddVacancyClient,
    IRequestClient<CreateVacancyMessageBoxEvent> requestCreateMessageBoxClient,
    IRequestClient<GetCompanyVacanciesEvent> requestGetVacancyClient,
    IRequestClient<DeleteVacancyEvent> requestDeleteVacancyClient,
    IRequestClient<UpdateVacancyEvent> requestUpdateVacancyClient
    )
    : ControllerBase
{
 
    [HttpPost("PublishVacancy")]
    public async Task<IActionResult> PublishVacancy([FromBody] VacancyToAddDto vacancy)
    {
        Guid companyId = Guid.Parse(User.FindFirst(ClaimTypes.PrimarySid)?.Value!);
        
        if (companyId == Guid.Empty)
            return Unauthorized();

        ValidationResults results = VacancyValidation.ValidateVacancyInputFields(vacancy);
        
        if (!results.IsValid)
            return new BadRequestObjectResult(results.ToString());
        
        CompanyShortInfo? companyShortInfo = await companyRepo.GetCompanyShortInfoById(companyId);
        
        if (companyShortInfo is null)
            throw new Exception("Company not found");
        
        AddVacancyEvent @event = new AddVacancyEvent(DateTime.UtcNow, companyShortInfo, vacancy);
        CancellationTokenSource tokenSource = new();
        
        var response = await requestAddVacancyClient.GetResponse<IServiceBusResult<Guid>>(@event, tokenSource.Token);

        if (!response.Message.IsSuccess)
        {
            await tokenSource.CancelAsync();
            return new BadRequestObjectResult(response.Message.ErrorMessage);
        }
        
        var createMessageBoxEvent = new CreateVacancyMessageBoxEvent(response.Message.Result, companyId);
        
        var responseCreateMessageBox = await requestCreateMessageBoxClient.GetResponse<IServiceBusResult<bool>>(createMessageBoxEvent);
        
        if (!responseCreateMessageBox.Message.IsSuccess)
            return new BadRequestObjectResult(responseCreateMessageBox.Message.ErrorMessage);
        
        return Ok(response.Message.Result);
    }


    [HttpPost("DeleteVacancy")]
    public async Task<IActionResult> DeleteVacancy(Guid id)
    {
        Guid companyId = GetCompanyId();
        
        DeleteVacancyEvent @event = new DeleteVacancyEvent(VacancyId: id,CompanyId: companyId, DateTime.UtcNow);

        try
        {
            Console.WriteLine("Sending request to delete vacancy");
            
            var result = await requestDeleteVacancyClient.GetResponse<IServiceBusResult<bool>>(@event);
            if (result.Message.IsSuccess)
                return Ok(result.Message.Result);
            
            Console.WriteLine("Get an error in DeleteVacancy method from request result to delete vacancy consumer");
            return new BadRequestObjectResult(result.Message.ErrorMessage);
        }
        catch (ForbiddenException ex)
        {
            Console.WriteLine("Catched an error in DeleteVacancy method ForbiddenException");
            return new BadRequestObjectResult(ex.Message);
        }
        catch (RequestTimeoutException)
        {
            Console.WriteLine("Catched an error in DeleteVacancy method RequestTimeoutException");
            return new BadRequestObjectResult("Request timeout");
        }
    }


    [HttpGet("GetCompanyVacancies")]
    public async Task<IActionResult> GetCompanyVacancies()
    {
        Guid companyId = GetCompanyId();
        
        GetCompanyVacanciesEvent @event = new GetCompanyVacanciesEvent(companyId);
        
        try
        {
            Console.WriteLine("Sending request to get company vacancies");
            await publisher.Publish(@event);
            var result = await requestGetVacancyClient.GetResponse<IServiceBusResult<IEnumerable<Vacancy>>>(@event);
            if (result.Message.IsSuccess)
                return Ok(result.Message.Result);

            return new BadRequestObjectResult(result.Message.ErrorMessage);
        }
        catch (RequestTimeoutException)
        {
            return new BadRequestObjectResult("Request timeout");
        }
    }
    
    
    [HttpPost("UpdateVacancy")]
    public async Task<IActionResult> UpdateVacancy([FromBody] VacancyToUpdateDto vacancy)
    {
        Guid companyId = GetCompanyId();
        
        ValidationResults results = VacancyValidation.ValidateVacancyInputFields(vacancy);
        
        if (!results.IsValid)
            return new BadRequestObjectResult(results.ToString());
        
        UpdateVacancyEvent @event = new UpdateVacancyEvent(companyId, vacancy, DateTime.UtcNow);
        
        var response = await requestUpdateVacancyClient.GetResponse<IServiceBusResult<bool>>(@event);

        if(response.Message.IsSuccess)
            return Ok(response.Message.Result);
        
        return new BadRequestObjectResult(response.Message.ErrorMessage);
    }
    
    
    private Guid GetCompanyId() =>
        Guid.Parse(User.FindFirst(ClaimTypes.PrimarySid)?.Value!);
}