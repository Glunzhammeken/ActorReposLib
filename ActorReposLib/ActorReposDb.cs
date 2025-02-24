using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ActorReposLib.Interfaces;

namespace ActorReposLib
{
    public class ActorReposDb : IActorRepos
    {
        private readonly ActorDBContext _Context;
        public ActorReposDb(ActorDBContext dBContext) 
        {
            _Context = dBContext;
        }
        public Actor Add(Actor actor)
        {
            throw new NotImplementedException();
        }

        public Actor? GetActorById(int id)
        {
            throw new NotImplementedException();
        }

        public List<Actor> GetActors(int? Birthyearbefore = null, int? Birthyearafter = null, string? name = null, string? sortBy = null)
        {
            throw new NotImplementedException();
        }

        public Actor Remove(int id)
        {
            throw new NotImplementedException();
        }

        public Actor? UpdateActor(int id, Actor nyData)
        {
            throw new NotImplementedException();
        }
    }
}
