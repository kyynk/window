using Homework.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Homework.Model.Tests
{
    [TestClass()]
    public class EllipseTests
    {
        private Ellipse _ellipse;
        private MockGraphics _mockGraphics;

        // setup
        [TestInitialize()]
        public void Initialize()
        {
            _ellipse = new Ellipse(new Point(1, 1), new Point(2, 5));
            _mockGraphics = new MockGraphics();
        }

        // test constructor
        [TestMethod()]
        public void EllipseTest()
        {
            _ellipse = new Ellipse(new Point(1, 1), new Point(2, 5));
            Assert.AreEqual(Constant.Constant.ELLIPSE, _ellipse.GetShapeName());
        }

        // test draw
        [TestMethod()]
        public void DrawTest()
        {
            _ellipse.Draw(_mockGraphics);
            Assert.AreEqual(1, _mockGraphics.DrawEllipseCount);
        }

        // test get shape name
        [TestMethod()]
        public void GetShapeNameTest()
        {
            Assert.AreEqual(Constant.Constant.ELLIPSE, _ellipse.GetShapeName());
        }

        // test get info
        [TestMethod()]
        public void GetInfoTest()
        {
            // not resize
            Assert.AreEqual("(1, 1), (2, 5)", _ellipse.GetInfo());
            // is resize
            _ellipse.Point1 = new Point(2, 5);
            _ellipse.Point2 = new Point(1, 0);
            _ellipse.IsResizing = true;
            Assert.AreEqual("(2, 5), (1, 0)", _ellipse.GetInfo());
        }
    }
}