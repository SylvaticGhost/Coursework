using GlobalModels.Vacancy;
using MongoDB.Driver;
using MongoDB.Entities;
using VacancyService.Models;

namespace VacancyService.SearchContext
{
    public class SearchVacancyContext : ISearchVacancyContext
    {
        private readonly IMongoCollection<Vacancy> _collection = DB.Collection<Vacancy>();

        public IEnumerable<Vacancy> SearchVacancy(SearchVacancyParams parameters)
        {
            var filterBuilder = Builders<Vacancy>.Filter;
            var filter = filterBuilder.Empty;
            
            
            if (!string.IsNullOrEmpty(parameters.Title))
                filter &= filterBuilder.Eq(v => v.Title, parameters.Title);

            if (!string.IsNullOrEmpty(parameters.VacancyId.ToString()))
                filter &= filterBuilder.Eq(v => v.VacancyId, parameters.VacancyId);
            
            
            if (!string.IsNullOrEmpty(parameters.CompanyId.ToString()))
                filter &= filterBuilder.Eq(v => v.CompanyInfo.CompanyId, parameters.CompanyId);

            if(!string.IsNullOrEmpty(parameters.Specialization))
                filter &= filterBuilder.Eq(v => v.Specialization, parameters.Specialization);

            return _collection.Find(filter).ToList();
        }
    }
}