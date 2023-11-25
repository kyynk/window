using Microsoft.VisualStudio.TestTools.UnitTesting;
using Homework.Model;

namespace Homework.Tests
{
    [TestClass()]
    public class EllipseTests
    {
        private Ellipse _ellipse;
        private MockGraphics _mockGraphics;

        // setup
        [TestInitialize()]
        public void Setup()
        {
            _ellipse = new Ellipse(new Point(1, 1), new Point(2, 5));
            _mockGraphics = new MockGraphics();
        }

        // test draw
        [TestMethod()]
        public void DrawTest()
        {
            _ellipse.Draw(_mockGraphics);
            Assert.AreEqual(_mockGraphics.CountEllipse, 1);
        }

        // test get shape name
        [TestMethod()]
        public void GetShapeNameTest()
        {
            Assert.AreEqual(_ellipse.GetShapeName(), Constant.ELLIPSE);
        }

        // test get info
        [TestMethod()]
        public void GetInfoTest()
        {
            Assert.AreEqual(_ellipse.GetInfo(), "(1, 1), (2, 5)");
        }
    }
}