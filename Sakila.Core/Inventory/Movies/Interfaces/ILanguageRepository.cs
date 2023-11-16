using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Sakila.Core.Inventory.Movies.Models;

namespace Sakila.Core.Inventory.Movies.Interfaces
{
    public interface ILanguageRepository
    {
        public Task<int> AddLanguage(Language language);
        public Task<IEnumerable<Language>> GetAllLanguagesAsync();
        public Task<Language> GetLanguageByLanguageIdAsync(int languageId);
        public Task<int> UpdateLanguage(Language language);
        public Task<int> DeleteLanguageAsync(Language language);
    }
}
