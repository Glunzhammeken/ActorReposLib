using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using ActorReposLib.Interfaces;

namespace ActorReposLib
{
    public class ActorReposDb : IActorRepos
    {
        private readonly ActorDbContext _context;
        public ActorReposDb(ActorDbContext DbContext) 
        {
            _context = DbContext;
        }
        public Actor Add(Actor actor)
        {
            actor.Id = 0;
            _context.Actors.Add(actor);
            _context.SaveChanges();
            return actor;
        }

        public Actor? GetActorById(int id)
        {
            return _context.Actors.FirstOrDefault(a => a.Id == id);
        }

        public IEnumerable<Actor> GetActors(int? Birthyearbefore = null, int? Birthyearafter = null, string? name = null, string? sortBy = null)
        {
            IQueryable<Actor> query = _context.Actors.ToList().AsQueryable();

            query.Where(a => (Birthyearbefore == null || a.BirthYear < Birthyearbefore) &&
                            (Birthyearafter == null || a.BirthYear > Birthyearafter) &&
                            (string.IsNullOrEmpty(name) || a.Name.Contains(name, StringComparison.OrdinalIgnoreCase)));

            if (!string.IsNullOrEmpty(sortBy))
            {
                sortBy = sortBy.ToLower();
                switch (sortBy)
                {
                    case "NameSort":
                        query = query.OrderBy(a => a.Name);
                        break;
                    case "Ascending":
                        query = query.OrderBy(a => a.BirthYear);
                        break;
                    case "Descending":
                        query = query.OrderByDescending(a => a.BirthYear);
                        break;
                    default: break;
                }
            }

            return query;
        }

        public Actor Remove(int id)
        {
            Actor? actor = GetActorById(id);
            if (actor == null) 
            {
                return null;
            }
            _context.Actors.Remove(actor);
            _context.SaveChanges();
            return actor;
        }

        public Actor? UpdateActor(int id, Actor nyData)
        {
            Actor? actor_ToUpdate = GetActorById(id);
            if (actor_ToUpdate == null)
            {
                return null;
            }
            actor_ToUpdate.Name = nyData.Name;
            actor_ToUpdate.BirthYear = nyData.BirthYear;
            _context.SaveChanges();
            return actor_ToUpdate;


        }
    }
}
