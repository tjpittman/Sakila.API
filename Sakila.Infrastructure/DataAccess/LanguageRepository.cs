using Microsoft.EntityFrameworkCore;
using Sakila.Core.Movies.Interfaces;
using Sakila.Core.Movies.Models;

namespace Sakila.Infrastructure.DataAccess
{
    public class LanguageRepository : ILanguageRepository
    {
        private readonly MySqlContext _mySqlContext;

        public LanguageRepository(MySqlContext mySqlContext)
        {
            _mySqlContext = mySqlContext;
        }

        public async Task<int> AddLanguageAsync(Language language)
        {
            await _mySqlContext.Language.AddAsync(language);
            
            return await _mySqlContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Language>> GetAllLanguagesAsync()
        {
            return await _mySqlContext.Language.ToListAsync();
        }

        public async Task<Language?> GetLanguageByLanguageIdAsync(int? languageId)
        {
            return await _mySqlContext.Language.FindAsync(languageId);
        }

        public async Task<int> UpdateLanguageAsync(Language language)
        {
            _mySqlContext.Language.Update(language);

            return await _mySqlContext.SaveChangesAsync();
        }

        public async Task<int> DeleteLanguageAsync(Language language)
        {
            _mySqlContext.Language.Remove(language);

            return await _mySqlContext.SaveChangesAsync();
        }
         
    }
}
