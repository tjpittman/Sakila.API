using Microsoft.EntityFrameworkCore;
using Sakila.Core.Movies.Interfaces;
using Sakila.Core.Movies.Models;

namespace Sakila.Infrastructure.DataAccess
{
    public class ActorRepository : IActorRepository
    {
        private readonly MySqlContext _mySqlContext;

        public ActorRepository(MySqlContext mySqlContext)
        {
            _mySqlContext = mySqlContext;
        }

        public async Task<int> AddActorAsync(Actor actor)
        {
            await _mySqlContext.Actor.AddAsync(actor);

            return await _mySqlContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Actor>> GetAllActorsAsync()
        {
            return await _mySqlContext.Actor.ToListAsync();
        }

        public async Task<Actor> GetActorByIdAsync(int actorId)
        {
            return await _mySqlContext.Actor.FindAsync(actorId);
        }

        public async Task<int> UpdateActorAsync(Actor actor)
        {
            _mySqlContext.Actor.Update(actor);

            return await _mySqlContext.SaveChangesAsync();
        }

        public async Task<int> DeleteActor(Actor actor)
        { 
            _mySqlContext.Actor.Remove(actor);
                
            return await _mySqlContext.SaveChangesAsync();
        }
    }
}
