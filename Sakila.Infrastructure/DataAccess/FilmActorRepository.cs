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
        public async Task<int> AddFilmActor(FilmActor filmActor)
        {
            await _mySqlContext.AddAsync(filmActor);
            return await _mySqlContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<FilmActor>> GetAllFilmActors()
        {
            return await _mySqlContext.FilmActor.ToListAsync();
        }

        public async Task<FilmActor> GetFilmActorByActorIdFilmId(int actorId, int filmId)
        {
            return await _mySqlContext.FilmActor.FindAsync(actorId, filmId);
        }

        public async Task<int> UpdateFilmActor(FilmActor filmActor)
        {
            _mySqlContext.Update(filmActor);
            return await _mySqlContext.SaveChangesAsync();
        }

        public async Task<int> DeleteFilmActor(int actorId, int filmId)
        {
            var filmActor = await GetFilmActorByActorIdFilmId(actorId, filmId);
            
            _mySqlContext.Remove(actorId);

            return await _mySqlContext.SaveChangesAsync();
        }
    }
}
