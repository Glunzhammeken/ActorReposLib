using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;


namespace ActorReposLib
{
    public class ActorDBContext : DbContext
    {
        public ActorDBContext(
            DbContextOptions<ActorDBContext> options) :
            base(options)
        { }

        public DbSet<Actor> Actors { get; set; }
    }
}
