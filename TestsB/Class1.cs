using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using BulletHell.GameEngine;
using BulletHell.Graphics;
using NUnit.Framework;

namespace TestsB
{
    [TestFixture]
    public class TestMovements
    {
        [Test]
        public void TestLinearXMove()
        {
            Assert.False(false);
            LinearLocationEquation path = new LinearLocationEquation(0, 1);

            Vector2 result = path.GetLocation(1);

            double allowance = .01;
            Assert.AreEqual(1, result.X, allowance);
            Assert.AreEqual(0, result.Y, allowance);

            result = path.GetLocation(9);

            Assert.AreEqual(9, result.X, allowance);
            Assert.AreEqual(0, result.Y, allowance);
        }

        [Test]
        public void TestLinearYMove()
        {
            double allowance = .01;

            Assert.True(true);
            LinearLocationEquation path = new LinearLocationEquation(Math.PI / 2, 1);

            Vector2 result = path.GetLocation(1);

            Assert.AreEqual(0, result.X, allowance);
            Assert.AreEqual(1, result.Y, allowance);

            result = path.GetLocation(9);

            Assert.AreEqual(0, result.X, allowance);
            Assert.AreEqual(9, result.Y, allowance);

            result = path.GetLocation(5000);

            Assert.AreEqual(0, result.X, allowance);
            Assert.AreEqual(5000, result.Y, allowance);

        }

        [Test]
        public void TestSpiral()
        {
            double allowance = .01;

            Assert.True(true);
            SpiralLocationEquation path = new SpiralLocationEquation(0, 0, 1);

            Vector2 res = path.GetLocation(1);

            Assert.AreEqual(Math.Cos(1), res.X, allowance);
            Assert.AreEqual(Math.Sin(1), res.Y, allowance);

            res = path.GetLocation(10);

            Assert.AreEqual(10 * Math.Cos(10), res.X, allowance);
            Assert.AreEqual(10 * Math.Sin(10), res.Y, allowance);

            res = path.GetLocation(100);

            Assert.AreEqual(100 * Math.Cos(100), res.X, allowance);
            Assert.AreEqual(100 * Math.Sin(100), res.Y, allowance);
        }
    }


    [TestFixture]
    public class TestCollision
    {
        [Test]
        public void TestAdd()
        {
            CollisionManager cM = new CollisionManager();

            GameObject g1 = new GameObject();
            GameObject g2 = new GameObject();


            cM.addToTeam(g1, TEAM.ENEMY);
            cM.addToTeam(g2, TEAM.ENEMY);

            int count = 0;

            foreach(var item in cM.teams[1])
            {
                count++;
            }

            Assert.AreEqual(2, count);
        }

        [Test]
        public void TestRemove()
        {
            CollisionManager cM = new CollisionManager();

            GameObject g1 = new GameObject();
            GameObject g2 = new GameObject();
            GameObject g3 = new GameObject();


            cM.addToTeam(g1, TEAM.ENEMY);
            cM.addToTeam(g2, TEAM.ENEMY);
            cM.addToTeam(g3, TEAM.ENEMY);

            cM.removeFromTeam(g3, TEAM.ENEMY);
            cM.removeFromTeam(g1, TEAM.ENEMY);

            int count = 0;

            foreach(var item in cM.teams[1])
            {
                count++;
            }

            Assert.AreEqual(1, count);

        }

  
    }




   
    //Tests gun equipped/fired
    //tests collisions
    //tests movements

}
