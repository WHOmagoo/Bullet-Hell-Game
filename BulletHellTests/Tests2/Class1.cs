using System;
using BulletHellTests.GameEngine;
using Microsoft.Xna.Framework;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class Class1
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

        [Test]
        public void TestLinearPathMovementInYDirection()
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


    }
}
