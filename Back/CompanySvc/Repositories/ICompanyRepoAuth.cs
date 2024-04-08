namespace CompanySvc.Repositories;

public interface ICompanyRepoAuth
{
    public Task<Guid> AddCompanyAuth(Guid companyId);

    public bool CheckIfCompanyHasAuth(Guid companyId);
}