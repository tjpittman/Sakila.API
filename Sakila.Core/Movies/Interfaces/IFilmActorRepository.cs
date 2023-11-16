using Sakila.Core.Movies.Models;

namespace Sakila.Core.Movies.Interfaces
{
    public interface IFilmActorRepository
    {
        public Task<int> AddFilmActor(FilmActor filmActor);
        public Task<IEnumerable<FilmActor>> GetAllFilmActors();
        public Task<FilmActor> GetFilmActorByActorIdFilmId(int actorId, int filmId);
        public Task<int> UpdateFilmActor(FilmActor filmActor);
        public Task<int> DeleteFilmActor(int actorId, int filmId);
    }
}
