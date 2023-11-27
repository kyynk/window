using Homework.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Homework.Model.Tests
{
    [TestClass()]
    public class ShapeTests
    {
        private Shape _shape;
        private Shape _throwShape;
        private MockGraphics _mockGraphics;

        // setup
        [TestInitialize()]
        public void Initialize()
        {
            _throwShape = new Shape(new Point(0, 0), new Point(1, 1));
            _shape = new Ellipse(new Point(3, 2), new Point(5, 6));
            _mockGraphics = new MockGraphics();
        }

        // test shape
        [TestMethod()]
        public void ShapeTest()
        {
            _throwShape = new Shape(new Point(0, 0), new Point(1, 1));
            Assert.AreEqual(Constant.Constant.NONE, _throwShape.ShapeName);
            Shape shapeDefaultConstructor = new Shape();
            Assert.AreEqual(Constant.Constant.NONE, shapeDefaultConstructor.ShapeName);
        }

        // test get shape name
        [TestMethod()]
        public void GetShapeNameTest()
        {
            Assert.AreEqual(Constant.Constant.NONE, _throwShape.ShapeName);
            Assert.AreEqual(Constant.Constant.ELLIPSE, _shape.ShapeName);
            Assert.AreEqual(Constant.Constant.ELLIPSE, _shape.GetShapeName());
        }

        // test get info
        [TestMethod()]
        public void GetInfoTest()
        {
            Assert.ThrowsException<NotImplementedException>(() => _throwShape.GetInfo());
            Assert.AreEqual("(3, 2), (5, 6)", _shape.Info);
            Assert.AreEqual("(3, 2), (5, 6)", _shape.GetInfo());
        }

        // test draw
        [TestMethod()]
        public void DrawTest()
        {
            Assert.ThrowsException<NotImplementedException>(() => _throwShape.Draw(_mockGraphics));
            _shape.Draw(_mockGraphics);
            Assert.AreEqual(1, _mockGraphics.DrawEllipseCount);
        }

        // test reset point
        [TestMethod()]
        public void ResetPointTest()
        {
            Point point1 = new Point(3, 7);
            Point point2 = new Point(2, 2);
            _shape.Point1 = point1;
            _shape.Point2 = point2;
            _shape.ResetPoint();
            Assert.AreEqual("(2, 2), (3, 7)", _shape.GetInfo());
        }

        // test move
        [TestMethod()]
        public void MoveTest()
        {
            Point point1 = new Point(0, 0);
            Point point2 = new Point(2, 3);
            _shape.Point1 = new Point(0, 0); // avoid ref (don't _shape.Point = point)
            _shape.Point2 = new Point(2, 3);
            double offsetX = 1.0;
            double offsetY = 2.0;

            _shape.Move(offsetX, offsetY);

            Assert.AreEqual(point1.X - offsetX, _shape.Point1.X);
            Assert.AreEqual(point2.X - offsetX, _shape.Point2.X);
            Assert.AreEqual(point1.Y - offsetY, _shape.Point1.Y);
            Assert.AreEqual(point2.Y - offsetY, _shape.Point2.Y);
        }

        // test check select
        [TestMethod()]
        public void CheckSelectTest()
        {
            Point point1 = new Point(0, 0);
            Point point2 = new Point(3, 3);
            _shape.Point1 = point1;
            _shape.Point2 = point2;

            Assert.IsTrue(_shape.CheckSelect(1.0, 2.0));
            Assert.IsFalse(_shape.CheckSelect(5.0, 5.0));
            Assert.IsFalse(_shape.CheckSelect(-5.0, -5.0));

            _shape.isSelected = true;
            Assert.IsTrue(_shape.isSelected);
            _shape.isSelected = false;
            Assert.IsFalse(_shape.isSelected);
        }

        // test draw hint
        [TestMethod()]
        public void DrawHintTest()
        {
            _shape.DrawHint(_mockGraphics);
            Assert.AreEqual(1, _mockGraphics.DrawHintCount);
            Assert.AreEqual(8, _mockGraphics.DrawHintCircleCount);
        }
    }
}