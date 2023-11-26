using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Homework.Model.Tests
{
    [TestClass()]
    public class LineTests
    {
        private Line _line;
        private MockGraphics _mockGraphics;

        // setup
        [TestInitialize()]
        public void Initialize()
        {
            _line = new Line(new Point(1, 1), new Point(2, 5));
            _mockGraphics = new MockGraphics();
        }

        // test draw
        [TestMethod()]
        public void DrawTest()
        {
            _line.Draw(_mockGraphics);
            Assert.AreEqual(1, _mockGraphics.CountLine);
        }

        // test get shape name
        [TestMethod()]
        public void GetShapeNameTest()
        {
            Assert.AreEqual(Constant.Constant.LINE, _line.GetShapeName());
        }

        // test get info
        [TestMethod()]
        public void GetInfoTest()
        {
            Assert.AreEqual("(1, 1), (2, 5)", _line.GetInfo());
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
            Assert.AreEqual("(2, 2), (3, 7)", _line.GetInfo());
        }
    }
}