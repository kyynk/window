using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Homework.Model.Tests
{
    [TestClass()]
    public class RectangleTests
    {
        private Rectangle _rectangle;
        private MockGraphics _mockGraphics;

        // setup
        [TestInitialize()]
        public void Initialize()
        {
            _rectangle = new Rectangle(new Point(1, 1), new Point(2, 5));
            _mockGraphics = new MockGraphics();
        }

        // test draw
        [TestMethod()]
        public void DrawTest()
        {
            _rectangle.Draw(_mockGraphics);
            Assert.AreEqual(1, _mockGraphics.CountRectangle);
        }

        // test get shape name
        [TestMethod()]
        public void GetShapeNameTest()
        {
            Assert.AreEqual(Constant.Constant.RECTANGLE, _rectangle.GetShapeName());
        }

        // test get info
        [TestMethod()]
        public void GetInfoTest()
        {
            Assert.AreEqual("(1, 1), (2, 5)", _rectangle.GetInfo());
        }
    }
}