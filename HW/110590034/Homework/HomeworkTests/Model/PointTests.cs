using Microsoft.VisualStudio.TestTools.UnitTesting;
using Homework.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework.Model.Tests
{
    [TestClass()]
    public class PointTests
    {
        private Point _point;

        // setup
        [TestInitialize()]
        public void Initialize()
        {
            _point = new Point(5, 10);
        }

        // test constructor
        [TestMethod()]
        public void PointTest()
        {
            _point = new Point(3, 5);
            Assert.AreEqual(3, _point.X);
            Assert.AreEqual(5, _point.Y);
        }

        // test property
        [TestMethod()]
        public void PointPropertyTest()
        {
            _point = new Point(100, 500);
            _point.X = 3;
            _point.Y = 5;
            Assert.AreEqual(3, _point.X);
            Assert.AreEqual(5, _point.Y);
        }
    }
}