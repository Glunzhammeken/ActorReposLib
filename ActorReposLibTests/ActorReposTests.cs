using Microsoft.VisualStudio.TestTools.UnitTesting;
using ActorReposLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography.X509Certificates;

namespace ActorReposLib.Tests
{
    [TestClass()]
    public class ActorReposTests
    {
        [TestMethod()]
        public void AddTest()
        {
            Actor actor = new Actor();
            ActorReposList actorRepos = new ActorReposList();
            actorRepos.Add(actor);
            
            Assert.AreEqual(1,actorRepos.GetActors().Count);
            Assert.AreEqual(actor, actorRepos.GetActorById(actor.Id));
            Assert.ThrowsException<ArgumentNullException>(() => actorRepos.Add(null));
        }
        [TestMethod()]
        public void RemoveTest() 
        {
            Actor actor = new Actor();
            ActorReposList actorRepos = new ActorReposList();
            actorRepos.Add(actor);
            actorRepos.Remove(actor.Id);
            Assert.AreEqual(0, actorRepos.GetActors().Count);
            Assert.ThrowsException<ArgumentNullException>(() => actorRepos.Remove(100));
        }
        [TestMethod()]
        public void GetActorByIdTest()
        {
            Actor actor = new Actor();
            ActorReposList actorRepos = new ActorReposList();
            actorRepos.Add(actor);
            Assert.AreEqual(actor,actorRepos.GetActorById(actor.Id));
            Assert.ThrowsException<ArgumentNullException>(() => actorRepos.GetActorById(100));

        }
        [TestMethod()]
        public void UpdateActorTest()
        {
            Actor actorToUpdate = new Actor();
            actorToUpdate.Name = "test";
            actorToUpdate.BirthYear = 1999;

            Actor ExpectedUpdate = new Actor();
            ExpectedUpdate.Name = "testupdate";
            ExpectedUpdate.BirthYear = 2000;

            ActorReposList actorRepos = new ActorReposList();
            
            actorRepos.Add(actorToUpdate);
            actorRepos.UpdateActor(actorToUpdate.Id, ExpectedUpdate);
            Assert.AreEqual(ExpectedUpdate.Name, actorToUpdate.Name);
            Assert.AreEqual(ExpectedUpdate.BirthYear, actorToUpdate.BirthYear);
            Assert.ThrowsException<ArgumentNullException>(() => actorRepos.UpdateActor(100, ExpectedUpdate));

        }

        [TestMethod]
        public void GetActors_BirthyearFilter_ReturnsCorrectActors()
        {
            // Actors
            Actor actor_1999 = new Actor { BirthYear = 1999, Name = "John" };
            Actor actor_2000 = new Actor { BirthYear = 2000, Name = "Karl" };
            Actor actor_2001 = new Actor { BirthYear = 2001, Name = "Børge" };

            //List of excepted actors
            List<Actor> expectedActorsBefore = new List<Actor> { actor_1999, actor_2000 }; // Actors born before 2001
            List<Actor> expectedActorsAfter = new List<Actor> { actor_2001, actor_2000 }; // Actors born after 1999
            List<Actor> expectedActorsBeforeAfter = new List<Actor> { actor_2000 }; // Actors born after 1999 and before 2001

            ActorReposList actorRepos = new ActorReposList();
            actorRepos.Add(actor_1999);
            actorRepos.Add(actor_2000);
            actorRepos.Add(actor_2001);

            //List of acual actors
            List<Actor> actualActorsBefore = actorRepos.GetActors(Birthyearbefore : 2001); // Filter by birth year before 2001
            List<Actor> actualActorsAfter = actorRepos.GetActors(Birthyearafter : 1999); // Filter by birth year after 1999
            List<Actor> actualActorsBeforeAfter = actorRepos.GetActors(2001, 1999); // Filter by birth year after 1999 and 2001

            CollectionAssert.AreEquivalent(expectedActorsBefore, actualActorsBefore, "The actors before 2001 were not returned as expected.");
            CollectionAssert.AreEquivalent(expectedActorsAfter, actualActorsAfter, "The actors after 1999 were not returned as expected.");
            CollectionAssert.AreEquivalent(expectedActorsBeforeAfter, actualActorsBeforeAfter, "The actors after 1999 and before 2001 were not returned as expected.");
        }
        [TestMethod]
        public void GetActors_NameFilter_ReturnsCorrectActors()
        {
            // Actors
            Actor actor_John = new Actor { BirthYear = 1999, Name = "John" };
            Actor actor_Karl = new Actor { BirthYear = 2000, Name = "Karl" };
            Actor actor_Børge = new Actor { BirthYear = 2001, Name = "Børge" };

            //List of excepted actors
            List<Actor> expectedActorsJohn = new List<Actor> { actor_John }; //Actor named John
            List<Actor> expectedActorsJohnCS = new List<Actor> { actor_John }; //Actor named John

            ActorReposList actorRepos = new ActorReposList();
            actorRepos.Add(actor_John);
            actorRepos.Add(actor_Karl);
            actorRepos.Add(actor_Børge);

            // List of acual actors
            List<Actor> actualActorsJohn = actorRepos.GetActors(name :"John"); //filter by name John
            List<Actor> actualActorsJohnCS = actorRepos.GetActors(name: "JOHN");

            CollectionAssert.AreEquivalent(expectedActorsJohn, actualActorsJohn, "The actors named john was not returned as excepted");
            CollectionAssert.AreEquivalent(expectedActorsJohnCS, actualActorsJohnCS, "The actors named john was not returned as excepted, so case sensetive is not working");

        }
        [TestMethod()]
        public void SearchEmpty()
        {
            // Actors
            Actor actor_John = new Actor { BirthYear = 1999, Name = "John" };
            Actor actor_Karl = new Actor { BirthYear = 2000, Name = "Karl" };
            Actor actor_Børge = new Actor { BirthYear = 2001, Name = "Børge" };

            //List of excepted actors
            List<Actor> expectedActorsEmpty = new List<Actor> { actor_John, actor_Karl, actor_Børge }; // All actors


            ActorReposList actorRepos = new ActorReposList();
            actorRepos.Add(actor_John);
            actorRepos.Add(actor_Karl);
            actorRepos.Add(actor_Børge);

            List<Actor> actualActorsEmptySearch = actorRepos.GetActors(null, null, null,null);

            // Empty search gives all actors
            CollectionAssert.AreEquivalent(expectedActorsEmpty, actualActorsEmptySearch);

        }
        [TestMethod()]
        public void MultipleFilter_ReturnsCorrectActors()
        {
            // Actors
            Actor actor_John = new Actor { BirthYear = 1999, Name = "John" };
            Actor actor_Karl = new Actor { BirthYear = 2000, Name = "Karl" };
            Actor actor_Børge = new Actor { BirthYear = 2001, Name = "Børge" };

            //List of excepted actors
            List<Actor> expectedActorsKarl = new List<Actor> { actor_Karl }; //Actor named Karl

            ActorReposList actorRepos = new ActorReposList();
            actorRepos.Add(actor_John);
            actorRepos.Add(actor_Karl);
            actorRepos.Add(actor_Børge);

            List<Actor> actualActorsKarl = actorRepos.GetActors(2001, 1999, "Karl"); //Filter by name "Karl" and birth year between 1999 and 2001

            CollectionAssert.AreEquivalent(expectedActorsKarl, actualActorsKarl, "The actor with name 'Karl' and birth year between 1999 and 2001 was not returned as expected.");

        }
        [TestMethod()]
        public void sortByFilter_ReturnsCorrectActors()
        {
            // Actors
            Actor actor_1 = new Actor { BirthYear = 1900, Name = "Anders" };
            Actor actor_2 = new Actor { BirthYear = 1920, Name = "Daniel" };
            Actor actor_3 = new Actor { BirthYear = 1950, Name = "Børge" };
            Actor actor_4 = new Actor { BirthYear = 1930, Name = "Carl" };
            Actor actor_5 = new Actor { BirthYear = 1910, Name = "Frederik" };
            Actor actor_6 = new Actor { BirthYear = 1940, Name = "Erik" };

            

            List<Actor> Actors = new List<Actor> { actor_1, actor_5, actor_2, actor_4, actor_6, actor_3 };

            //Excpeted i will add the actors so that they fit the sortBy
            List<Actor> ExpectedActorsDescending = Actors.OrderByDescending(a => a.BirthYear).ToList();
            List<Actor> ExpectedActorsAscending = Actors.OrderBy(a => a.BirthYear).ToList();
            List<Actor> ExpectedActorsName = Actors.OrderBy(a => a.Name).ToList();
            

            //I have added the actors in an order that insures that it is random in the list
            ActorReposList actorRepos = new ActorReposList();
            actorRepos.Add(actor_1);
            actorRepos.Add(actor_2);
            actorRepos.Add(actor_3);
            actorRepos.Add(actor_4);
            actorRepos.Add(actor_5);
            actorRepos.Add(actor_6);

            // List of acual actors

            List<Actor> actualActorsDescending = actorRepos.GetActors(sortBy :"Descending");
            List<Actor> actualActorsAscending = actorRepos.GetActors(sortBy: "Ascending");
            List<Actor> actualActorsName = actorRepos.GetActors(sortBy: "NameSort");

          

            CollectionAssert.AreEqual(ExpectedActorsDescending, actualActorsDescending);
            CollectionAssert.AreEqual(ExpectedActorsAscending, actualActorsAscending);
            CollectionAssert.AreEqual(ExpectedActorsName, actualActorsName);

        }
        
    }
}