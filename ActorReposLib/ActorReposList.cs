using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using ActorReposLib.Interfaces;

namespace ActorReposLib
{
    public class ActorReposList : IActorRepos
    {
        private List<Actor> actors = new List<Actor>();
        private int nextId;


        public IEnumerable<Actor> GetActors(int? Birthyearbefore = null, int? Birthyearafter = null, string? name = null, string? sortBy = null)
        {
            List<Actor> actorsreturn = new List<Actor>();
            actorsreturn = actors.Where(a => (Birthyearbefore == null || a.BirthYear < Birthyearbefore) &&
                            (Birthyearafter == null || a.BirthYear > Birthyearafter) &&
                            (string.IsNullOrEmpty(name) || a.Name.Contains(name, StringComparison.OrdinalIgnoreCase))).ToList();

            if (!string.IsNullOrEmpty(sortBy))
            {
                switch (sortBy)
                {
                    case "NameSort":
                        actorsreturn = actorsreturn.OrderBy(a => a.Name).ToList();
                        break;
                    case "Ascending":
                        actorsreturn = actorsreturn.OrderBy(a => a.BirthYear).ToList();
                        break;
                    case "Descending":
                        actorsreturn = actorsreturn.OrderByDescending(a => a.BirthYear).ToList();
                        break;
                }
            }

            return actorsreturn;
        }

        public Actor? GetActorById(int id)
        {

            Actor actor = actors.Find(a => a.Id == id);
            if (actor == null)
                throw new ArgumentNullException($"Actor with ID {id} not found.");
            return actor;
        }

        public Actor Add(Actor actor)
        {
            if (actor == null) throw new ArgumentNullException("Actor is null");
            actor.Id = nextId++;
            actors.Add(actor);
            return actor;

        }
        public Actor Remove(int id)
        {
            Actor? actor = GetActorById(id);
            if (actor == null)
            {
                throw new ArgumentNullException("Book not found, id " + id);
            }
            actors.Remove(actor);
            return actor;
        }

        public Actor? UpdateActor(int id, Actor nyData)
        {
            Actor? actor = GetActorById(id);
            if (actor == null)
            {
                throw new ArgumentNullException($"Actor with ID {id} not found.");
            }
            actor.Name = nyData.Name;
            actor.BirthYear = nyData.BirthYear;
            return actor;
        }
    }
}
