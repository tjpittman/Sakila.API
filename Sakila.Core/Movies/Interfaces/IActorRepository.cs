using Sakila.Core.Movies.Models;

namespace Sakila.Core.Movies.Interfaces
{
    public interface IActorRepository
    {
        public Task<int> AddActor(Actor actor);
        public Task<IEnumerable<Actor>> GetAllActors();
        public Task<Actor> GetActorById(int actorId);
        public Task<int> UpdateActor(Actor actor);
        public Task<int> RemoveActorByActorId(int actorId);
    }
}
