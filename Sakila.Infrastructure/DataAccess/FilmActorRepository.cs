using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using Sakila.Core.Movies.Interfaces;
using Sakila.Core.Movies.Models;

namespace Sakila.Infrastructure.DataAccess
{
    public class FilmActorRepository : IFilmActorRepository
    {
        private readonly MySqlContext _mySqlContext;
        public FilmActorRepository(MySqlContext mySqlContext)
        {
            _mySqlContext = mySqlContext;
        }
        public async Task<int> AddFilmActorAsync(FilmActor filmActor)
        {
            await _mySqlContext.AddAsync(filmActor);
            return await _mySqlContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<FilmActor>> GetAllFilmActorsAsync()
        {
            return await _mySqlContext.FilmActor.ToListAsync();
        }
        public async Task<List<FilmActor>> GetFilmActorByFilmIdAsync(int filmId)
        {
            return await _mySqlContext.FilmActor.Where(fa => fa.FilmId == filmId).ToListAsync();
        }

        public async Task<List<FilmActor>> GetFilmActorByActorIdAsync(int actorId)
        {
            return await _mySqlContext.FilmActor.Where(fa => fa.ActorId == actorId).ToListAsync();
        }

        public async Task<FilmActor> GetFilmActorByActorIdFilmIdAsync(int actorId, int filmId)
        {
            return await _mySqlContext.FilmActor.FindAsync(actorId, filmId);
        }

        public async Task<int> UpdateFilmActorAsync(FilmActor filmActor)
        {
            _mySqlContext.Update(filmActor);
            return await _mySqlContext.SaveChangesAsync();
        }

        public async Task<int> DeleteFilmActorAsync(int actorId, int filmId)
        {
            var filmActor = await GetFilmActorByActorIdFilmIdAsync(actorId, filmId);
            
            _mySqlContext.Remove(actorId);

            return await _mySqlContext.SaveChangesAsync();
        }

    }
}
