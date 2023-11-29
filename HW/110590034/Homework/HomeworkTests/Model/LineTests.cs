using Homework.Model;
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

        // test constructor
        [TestMethod()]
        public void LineTest()
        {
            _line = new Line(new Point(1, 1), new Point(2, 5));
            Assert.AreEqual(Constant.Constant.LINE, _line.GetShapeName());
        }

        // test draw
        [TestMethod()]
        public void DrawTest()
        {
            _line.Draw(_mockGraphics);
            Assert.AreEqual(1, _mockGraphics.DrawLineCount);
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
            // not resize
            Assert.AreEqual("(1, 1), (2, 5)", _line.GetInfo());
            // is resize
            _line.Point1 = new Point(2, 5);
            _line.Point2 = new Point(1, 0);
            _line.IsResizing = true;
            Assert.AreEqual("(2, 5), (1, 0)", _line.GetInfo());
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

        // test update point
        [TestMethod()]
        public void UpdatePointWithRightBottomAndFirstPointIsLeftBottomTest()
        {
            Point point1 = new Point(1, 7);
            Point point2 = new Point(3, 5);
            _line.Point1 = point1;
            _line.Point2 = point2;
            _line.ResetResizePoint();
            _line.SetResizePoint(Shape.Location.RightBottom, new Point(5, 7));
            _line.UpdatePoint(Shape.Location.RightBottom);
            Assert.AreEqual(1, _line.Point1.X);
            Assert.AreEqual(7, _line.Point1.Y);
            Assert.AreEqual(5, _line.Point2.X);
            Assert.AreEqual(5, _line.Point2.Y);
        }

        // test update point
        [TestMethod()]
        public void UpdatePointWithRightBottomAndFirstPointIsLeftTopTest()
        {
            Point point1 = new Point(1, 1);
            Point point2 = new Point(6, 7);
            _line.Point1 = point1;
            _line.Point2 = point2;
            _line.ResetResizePoint();
            _line.SetResizePoint(Shape.Location.RightBottom, new Point(5, 5));
            _line.UpdatePoint(Shape.Location.RightBottom);
            Assert.AreEqual(1, _line.Point1.X);
            Assert.AreEqual(1, _line.Point1.Y);
            Assert.AreEqual(5, _line.Point2.X);
            Assert.AreEqual(5, _line.Point2.Y);
        }


        // test update point
        [TestMethod()]
        public void UpdatePointWithOtherTest()
        {
            Point point1 = new Point(1, 1);
            Point point2 = new Point(6, 7);
            _line.Point1 = point1;
            _line.Point2 = point2;
            _line.ResetResizePoint();
            _line.SetResizePoint(Shape.Location.Left, new Point(5, 5));
            _line.UpdatePoint(Shape.Location.Left);
            Assert.AreEqual(1, _line.Point1.X);
            Assert.AreEqual(1, _line.Point1.Y);
            Assert.AreEqual(6, _line.Point2.X);
            Assert.AreEqual(7, _line.Point2.Y);
        }

        // test is frist point bottom
        [TestMethod()]
        public void IsFirstPointBottomTest()
        {
            Point point1 = new Point(1, 1);
            Point point2 = new Point(6, 0);
            _line.Point1 = point1;
            _line.Point2 = point2;
            Assert.IsTrue(_line.IsFirstPointBottom(1));
            _line.Point2.Y = 7;
            Assert.IsFalse(_line.IsFirstPointBottom(7));
        }
    }
}