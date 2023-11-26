using Microsoft.VisualStudio.TestTools.UnitTesting;
using Homework.Tests;

namespace Homework.Model.Tests
{
    [TestClass()]
    public class RectangleTests
    {
        private Rectangle _rectangle;
        private MockGraphics _mockGraphics;

        // setup
        [TestInitialize()]
        public void Setup()
        {
            _rectangle = new Rectangle(new Point(1, 1), new Point(2, 5));
            _mockGraphics = new MockGraphics();
        }

        // test draw
        [TestMethod()]
        public void DrawTest()
        {
            _rectangle.Draw(_mockGraphics);
            Assert.AreEqual(_mockGraphics.CountRectangle, 1);
        }

        // test get shape name
        [TestMethod()]
        public void GetShapeNameTest()
        {
            Assert.AreEqual(_rectangle.GetShapeName(), Constant.Constant.RECTANGLE);
        }

        // test get info
        [TestMethod()]
        public void GetInfoTest()
        {
            Assert.AreEqual(_rectangle.GetInfo(), "(1, 1), (2, 5)");
        }
    }
}