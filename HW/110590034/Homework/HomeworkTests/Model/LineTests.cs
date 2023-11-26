using Microsoft.VisualStudio.TestTools.UnitTesting;
using Homework.Tests;

namespace Homework.Model.Tests
{
    [TestClass()]
    public class LineTests
    {
        private Line _line;
        private MockGraphics _mockGraphics;

        // setup
        [TestInitialize()]
        public void Setup()
        {
            _line = new Line(new Point(1, 1), new Point(2, 5));
            _mockGraphics = new MockGraphics();
        }

        // test draw
        [TestMethod()]
        public void DrawTest()
        {
            _line.Draw(_mockGraphics);
            Assert.AreEqual(_mockGraphics.CountLine, 1);
        }

        // test get shape name
        [TestMethod()]
        public void GetShapeNameTest()
        {
            Assert.AreEqual(_line.GetShapeName(), Constant.Constant.LINE);
        }

        // test get info
        [TestMethod()]
        public void GetInfoTest()
        {
            Assert.AreEqual(_line.GetInfo(), "(1, 1), (2, 5)");
        }

        // test reset point
        [TestMethod()]
        public void ResetPointTest()
        {
            Point point1 = new Point(3, 7);
            Point point2 = new Point(2, 2);
            _line.Point1 = point1;
            _line.Point2 = point2;
            _line.ResetPoint();
            Assert.AreEqual(_line.GetInfo(), "(2, 2), (3, 7)");
        }
    }
}