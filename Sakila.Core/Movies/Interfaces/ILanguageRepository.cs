using Sakila.Core.Movies.Models;

namespace Sakila.Core.Movies.Interfaces
{
    public interface ILanguageRepository
    {
        public Task<int> AddLanguage(Language language);
        public Task<IEnumerable<Language>> GetAllLanguagesAsync();
        public Task<Language> GetLanguageByLanguageIdAsync(int languageId);
        public Task<int> UpdateLanguage(Language language);
        public Task<int> DeleteLanguageAsync(Language language);//TODO: change to go by id field
    }
}
