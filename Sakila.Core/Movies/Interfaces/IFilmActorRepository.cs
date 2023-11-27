using Sakila.Core.Movies.Models;

namespace Sakila.Core.Movies.Interfaces
{
    public interface IFilmActorRepository
    {
        public Task<int> AddFilmActorAsync(FilmActor filmActor);
        public Task<IEnumerable<FilmActor>> GetAllFilmActorsAsync();
        public Task<List<FilmActor>> GetFilmActorByFilmIdAsync(int filmId);
        public Task<List<FilmActor>> GetFilmActorByActorIdAsync(int actorId);
        public Task<FilmActor> GetFilmActorByActorIdFilmIdAsync(int actorId, int filmId);
        public Task<int> UpdateFilmActorAsync(FilmActor filmActor);
        public Task<int> DeleteFilmActorAsync(int actorId, int filmId);
    }
}
