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
    public class ActorTests
    {
        [TestMethod()]
        public void ActorIdTest() 
        {
            Actor actor = new Actor();
            actor.Id = 1;
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => actor.Id = -1);
        }
        [TestMethod()]
        public void ActorNameTest() 
        {
            Actor actor = new Actor();
            actor.Name = "test";
            Assert.AreEqual("test", actor.Name);
            Assert.ThrowsException<ArgumentOutOfRangeException>( () => actor.Name = "te");
            Assert.ThrowsException<ArgumentNullException>(() => actor.Name = null);
        }
        [TestMethod()]
        public void ActorBirthyear()
        {
            Actor actor = new Actor();
            actor.BirthYear = 1900;
            Assert.AreEqual(actor.BirthYear, 1900);
            Assert.ThrowsException<ArgumentOutOfRangeException>( () => actor.BirthYear = 1800);
        }
        [TestMethod()]
        public void ToStringTest()
        {
            Actor actor = new Actor();
            actor.Id = 1;
            actor.Name = "test";
            actor.BirthYear = 1920;
            Assert.AreEqual("Name of actor: test Birthyear of actor: 1920", actor.ToString());
        }
    }
}