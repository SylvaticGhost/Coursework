using System.Security.Claims;
using CompanySvc.Helpers;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

namespace CompanySvc.Controllers;

public abstract class CompanyControllerBase(IPublishEndpoint publisher, IConfiguration configuration) : ControllerBase
{
    protected readonly IPublishEndpoint Publisher = publisher;
    protected readonly LocalAuthHelpers LocalAuthHelpers = new(configuration);
    
    protected Guid GetCompanyId() =>
        Guid.Parse(User.FindFirst(ClaimTypes.PrimarySid)?.Value!);
}