using Microsoft.VisualStudio.TestTools.UnitTesting;
using ActorReposLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ActorReposLib.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ActorReposLib.Tests
{
    [TestClass()]
    public class ActorDbTests
    {
        private const bool useDatabase = true;
        private static IActorRepos _repo;

        [ClassInitialize]
        public static void InitOnce(TestContext context)
        {
            if (useDatabase)
            {
                var optionsBuilder = new DbContextOptionsBuilder<ActorDbContext>();
                optionsBuilder.UseSqlServer(DbSecret.ConnectionStringLocal);
                ActorDbContext _dbContext = new(optionsBuilder.Options);
                _dbContext.Database.ExecuteSqlRaw("TRUNCATE TABLE dbo.Actors");
                _repo = new ActorReposDb(_dbContext);
            }
            else
            {
                _repo = new ActorReposList();
            }
        }

        [TestMethod()]
        public void TestGenericGetAll_ReturnsCorrect()
        {
            // Actors
            Actor actor_John = new Actor { BirthYear = 1999, Name = "John" };
            Actor actor_Karl = new Actor { BirthYear = 2000, Name = "Karl" };
            Actor actor_Børge = new Actor { BirthYear = 2001, Name = "Børge" };

            List<Actor> expectedActors = new List<Actor> { actor_Karl, actor_Børge, actor_John }; //Actor John, Karl og Børge

            GeneriskRepos<Actor> actorDb = new GeneriskRepos<Actor>();

            List<Actor> actualActors = _repo.GetActors();

            CollectionAssert.AreEquivalent(expectedActors, actualActors);
        }

        [TestMethod()]
        public void TestGetById()
        {
            Actor actor = new Actor();
            GeneriskRepos<Actor> actorDb = new GeneriskRepos<Actor>();
            actorDb.Add(actor);
            
            Assert.AreEqual(actor,actorDb.GetItemById(actor.Id));
            Assert.ThrowsException<ArgumentNullException>(() => actorDb.GetItemById(100));
        }

        [TestMethod()]
        public void TestGenericAdd()
        {
            Actor actor = new Actor();
            GeneriskRepos<Actor> actorDb = new GeneriskRepos<Actor>();
            actorDb.Add(actor);

            Assert.AreEqual(1, _repo.GetActors().Count);
            Assert.AreEqual(actor, _repo.GetActorById(actor.Id));
            Assert.ThrowsException<ArgumentNullException>(() => _repo.Add(null));
        }

        [TestMethod()]
        public void RemoveGenericTest()
        {
            Actor actor = new Actor();
            GeneriskRepos<Actor> actorDb = new GeneriskRepos<Actor>();
            actorDb.Add(actor);
            Assert.AreEqual(1, actorDb.GetList().Count);
            actorDb.Remove(1);
            Assert.AreEqual(0, actorDb.GetList().Count);
            Assert.ThrowsException<ArgumentNullException>(() => actorDb.Remove(100));
        }
    }
}