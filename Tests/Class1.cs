using System;
using BulletHellTests.GameEngine;
using NUnit.Framework;
using Microsoft.Xna.Framework;


namespace Tests
{
    [TestFixture]
    public class Tests
    {
        [Test]
        public void TestLinearPathMovementInXDirection()
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

    }
}
