using Sakila.Core.Movies.Models;

namespace Sakila.Core.Movies.Interfaces
{
    public interface IActorRepository
    {
        public Task<int> AddActorAsync(Actor actor);
        public Task<IEnumerable<Actor>> GetAllActorsAsync();
        public Task<Actor> GetActorByIdAsync(int actorId);
        public Task<int> UpdateActorAsync(Actor actor);
        public Task<int> RemoveActorByActorIdAsync(int actorId);
    }
}
