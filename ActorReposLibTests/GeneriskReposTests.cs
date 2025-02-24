using Microsoft.VisualStudio.TestTools.UnitTesting;
using ActorReposLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActorReposLib.Tests
{
    [TestClass()]
    public class GeneriskReposTests
    {
       

        [TestMethod()]
        public void TestGeneriskGetAll_ReturnsCorrect()
        {
            // Actors
            Actor actor_John = new Actor { BirthYear = 1999, Name = "John" };
            Actor actor_Karl = new Actor { BirthYear = 2000, Name = "Karl" };
            Actor actor_Børge = new Actor { BirthYear = 2001, Name = "Børge" };

            List<Actor> expectedActors = new List<Actor> { actor_Karl, actor_Børge, actor_John }; //Actor John, Karl og Børge

            GeneriskRepos<Actor> generiskRepos = new GeneriskRepos<Actor>();

            generiskRepos.Add(actor_John);
            generiskRepos.Add(actor_Karl);
            generiskRepos.Add(actor_Børge);

            List<Actor> actualActors = generiskRepos.GetList();

            CollectionAssert.AreEquivalent(expectedActors, actualActors);

        }
        [TestMethod()]
        public void TestGetById() 
        {
            Actor actor = new Actor();
            GeneriskRepos<Actor> generiskRepos = new GeneriskRepos<Actor>();
            generiskRepos.Add(actor);
            
            Assert.AreEqual(actor,generiskRepos.GetItemById(actor.Id));
            Assert.ThrowsException<ArgumentNullException>(() => generiskRepos.GetItemById(100));
        }
        [TestMethod()]
        public void TestGeneriskAdd()
        {
            Actor actor = new Actor();
            GeneriskRepos<Actor> generiskRepos = new GeneriskRepos<Actor>();
            generiskRepos.Add(actor);

            Assert.AreEqual(1, generiskRepos.GetList().Count);
            Assert.AreEqual(actor, generiskRepos.GetItemById(actor.Id));
            Assert.ThrowsException<ArgumentNullException>(() => generiskRepos.Add(null));
        }
        [TestMethod()]
        public void RemoveGeneriskTest()
        {
            Actor actor = new Actor();
            GeneriskRepos<Actor> generiskRepos = new GeneriskRepos<Actor>();
            generiskRepos.Add(actor);
            Assert.AreEqual(1, generiskRepos.GetList().Count);
            generiskRepos.Remove(1);
            Assert.AreEqual(0, generiskRepos.GetList().Count);
            Assert.ThrowsException<ArgumentNullException>(() => generiskRepos.Remove(100));
        }
        [TestMethod()]
       
        
    }
}