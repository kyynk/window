using Microsoft.VisualStudio.TestTools.UnitTesting;
using Homework.Tests;
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
        public void Setup()
        {
            _throwShape = new Shape(new Point(0, 0), new Point(1, 1));
            _shape = new Ellipse(new Point(3, 2), new Point(5, 6));
            _mockGraphics = new MockGraphics();
        }

        // test get shape name
        [TestMethod()]
        public void GetShapeNameTest()
        {
            Assert.AreEqual(_throwShape.ShapeName, Constant.Constant.NONE);
            Assert.AreEqual(_shape.ShapeName, Constant.Constant.ELLIPSE);
            Assert.AreEqual(_shape.GetShapeName(), Constant.Constant.ELLIPSE);
        }

        // test get info
        [TestMethod()]
        public void GetInfoTest()
        {
            Assert.ThrowsException<NotImplementedException>(() => _throwShape.GetInfo());
            Assert.AreEqual(_shape.Info, "(3, 2), (5, 6)");
            Assert.AreEqual(_shape.GetInfo(), "(3, 2), (5, 6)");
        }

        // test draw
        [TestMethod()]
        public void DrawTest()
        {
            Assert.ThrowsException<NotImplementedException>(() => _throwShape.Draw(_mockGraphics));
            _mockGraphics.ClearAll();
            _shape.Draw(_mockGraphics);
            Assert.AreEqual(_mockGraphics.CountEllipse, 1);
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
            Assert.AreEqual(_shape.GetInfo(), "(2, 2), (3, 7)");
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
            Assert.AreEqual(_mockGraphics.CountHint, 1);
            Assert.AreEqual(_mockGraphics.CountHintCircle, 8);
        }
    }
}