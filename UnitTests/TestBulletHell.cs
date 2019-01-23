using System;
using System.Runtime.ConstrainedExecution;
using BulletHell;
using NUnit.Framework;

namespace UnitTests
{
    [TestFixture]
    public class Tests
    {
        [Test]
        public void TestLinearPathMovementInXDirection()
        {
            Assert.False(false);
            LinearPath path = new LinearPath(0, 1);

            Tuple<double, double> result = path.updateLocation(1);

            double allowance = .01;
            Assert.AreEqual(1, result.Item1, allowance);
            Assert.AreEqual(0, result.Item2, allowance);

            result = path.updateLocation(9);
            
            Assert.AreEqual(9, result.Item1, allowance);
            Assert.AreEqual(0, result.Item2, allowance);
        }
        
        [Test]
        public void TestLinearPathMovementInYDirection()
        {
            double allowance = .01;
            
            Assert.True(true);
            LinearPath path = new LinearPath(Math.PI / 2, 1);

            Tuple<double, double> result = path.updateLocation(1);
            
            Assert.AreEqual(0, result.Item1, allowance);
            Assert.AreEqual(1, result.Item2, allowance);

            result = path.updateLocation(9);

            Assert.AreEqual(0, result.Item1, allowance);
            Assert.AreEqual(9, result.Item2, allowance);

            result = path.updateLocation(5000);
            
            Assert.AreEqual(0, result.Item1, allowance);
            Assert.AreEqual(5000, result.Item2, allowance);
        }
    }
}