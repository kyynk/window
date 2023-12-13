using Homework.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

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

        // test constructor
        [TestMethod()]
        public void ShapeDefaultConstructorTest()
        {
            Shape shapeDefaultConstructor = new Shape();
            Assert.AreEqual(Constant.Constant.NONE, shapeDefaultConstructor.ShapeName);
        }

        // test constructor
        [TestMethod()]
        public void ShapeTest()
        {
            _throwShape = new Shape(new Point(0, 0), new Point(1, 1));
            Assert.AreEqual(Constant.Constant.NONE, _throwShape.ShapeName);
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

            _shape.IsSelected = true;
            Assert.IsTrue(_shape.IsSelected);
            _shape.IsSelected = false;
            Assert.IsFalse(_shape.IsSelected);
        }

        // test draw hint
        [TestMethod()]
        public void DrawHintTest()
        {
            _shape.DrawHint(_mockGraphics);
            Assert.AreEqual(1, _mockGraphics.DrawHintCount);
            Assert.AreEqual(8, _mockGraphics.DrawHintCircleCount);
        }

        // test get mean
        [TestMethod()]
        public void GetMeanTest()
        {
            Assert.AreEqual(5, _shape.GetMean(1, 9));
        }

        // test reset resize point
        [TestMethod()]
        public void ResetResizePointTest()
        {
            _shape = new Ellipse(new Point(1, 1), new Point(3, 3));
            PrivateObject privateShape = new PrivateObject(_shape);
            _shape.ResetResizePoint();
            Assert.AreEqual(1, ((Dictionary<Shape.Location, Point>)privateShape.GetField("_resizePoint"))[Shape.Location.LeftTop].X);
            Assert.AreEqual(1, ((Dictionary<Shape.Location, Point>)privateShape.GetField("_resizePoint"))[Shape.Location.LeftTop].Y);
            Assert.AreEqual(1, ((Dictionary<Shape.Location, Point>)privateShape.GetField("_resizePoint"))[Shape.Location.Left].X);
            Assert.AreEqual(2, ((Dictionary<Shape.Location, Point>)privateShape.GetField("_resizePoint"))[Shape.Location.Left].Y);
            Assert.AreEqual(1, ((Dictionary<Shape.Location, Point>)privateShape.GetField("_resizePoint"))[Shape.Location.LeftBottom].X);
            Assert.AreEqual(3, ((Dictionary<Shape.Location, Point>)privateShape.GetField("_resizePoint"))[Shape.Location.LeftBottom].Y);
            Assert.AreEqual(2, ((Dictionary<Shape.Location, Point>)privateShape.GetField("_resizePoint"))[Shape.Location.Top].X);
            Assert.AreEqual(1, ((Dictionary<Shape.Location, Point>)privateShape.GetField("_resizePoint"))[Shape.Location.Top].Y);
            Assert.AreEqual(2, ((Dictionary<Shape.Location, Point>)privateShape.GetField("_resizePoint"))[Shape.Location.Bottom].X);
            Assert.AreEqual(3, ((Dictionary<Shape.Location, Point>)privateShape.GetField("_resizePoint"))[Shape.Location.Bottom].Y);
            Assert.AreEqual(3, ((Dictionary<Shape.Location, Point>)privateShape.GetField("_resizePoint"))[Shape.Location.RightTop].X);
            Assert.AreEqual(1, ((Dictionary<Shape.Location, Point>)privateShape.GetField("_resizePoint"))[Shape.Location.RightTop].Y);
            Assert.AreEqual(3, ((Dictionary<Shape.Location, Point>)privateShape.GetField("_resizePoint"))[Shape.Location.Right].X);
            Assert.AreEqual(2, ((Dictionary<Shape.Location, Point>)privateShape.GetField("_resizePoint"))[Shape.Location.Right].Y);
            Assert.AreEqual(3, ((Dictionary<Shape.Location, Point>)privateShape.GetField("_resizePoint"))[Shape.Location.RightBottom].X);
            Assert.AreEqual(3, ((Dictionary<Shape.Location, Point>)privateShape.GetField("_resizePoint"))[Shape.Location.RightBottom].Y);
        }

        // test update point
        [TestMethod()]
        public void UpdatePointWithRightBottomTest()
        {
            _shape = new Ellipse(new Point(1, 1), new Point(3, 3));
            _shape.ResetResizePoint();
            _shape.SetResizePoint(Shape.Location.RightBottom, new Point(5, 5));
            _shape.UpdatePoint(Shape.Location.RightBottom);
            Assert.AreEqual(1, _shape.Point1.X);
            Assert.AreEqual(1, _shape.Point1.Y);
            Assert.AreEqual(5, _shape.Point2.X);
            Assert.AreEqual(5, _shape.Point2.Y);
        }

        // test update point
        [TestMethod()]
        public void UpdatePointWithOtherTest()
        {
            _shape = new Ellipse(new Point(1, 1), new Point(3, 3));
            _shape.ResetResizePoint();
            _shape.SetResizePoint(Shape.Location.Left, new Point(5, 5));
            _shape.UpdatePoint(Shape.Location.Left);
            Assert.AreEqual(1, _shape.Point1.X);
            Assert.AreEqual(1, _shape.Point1.Y);
            Assert.AreEqual(3, _shape.Point2.X);
            Assert.AreEqual(3, _shape.Point2.Y);
        }

        // test set resize point
        [TestMethod()]
        public void SetResizePointTest()
        {
            _shape = new Ellipse(new Point(1, 1), new Point(3, 3));
            PrivateObject privateShape = new PrivateObject(_shape);
            _shape.SetResizePoint(Shape.Location.Left, new Point(5, 5));
            Assert.AreEqual(5, ((Dictionary<Shape.Location, Point>)privateShape.GetField("_resizePoint"))[Shape.Location.Left].X);
            Assert.AreEqual(5, ((Dictionary<Shape.Location, Point>)privateShape.GetField("_resizePoint"))[Shape.Location.Left].Y);
        }

        // test is in resize point
        [TestMethod()]
        public void IsInResizePointTest()
        {
            _shape = new Rectangle(new Point(10, 10), new Point(30, 30));
            _shape.ResetResizePoint();
            Assert.IsTrue(_shape.IsInResizePoint(Shape.Location.LeftTop, 12.5, 11.6));
            Assert.IsFalse(_shape.IsInResizePoint(Shape.Location.LeftTop, 30.5, 11.6));
            Assert.IsFalse(_shape.IsInResizePoint(Shape.Location.LeftTop, 12.5, 21.6));
            Assert.IsFalse(_shape.IsInResizePoint(Shape.Location.LeftTop, -30.5, 11.6));
            Assert.IsFalse(_shape.IsInResizePoint(Shape.Location.LeftTop, 12.5, -21.6));
        }

        // test get location
        [TestMethod()]
        public void GetLocationTest()
        {
            _shape = new Line(new Point(10, 10), new Point(30, 30));
            _shape.ResetResizePoint();
            Assert.AreEqual(Shape.Location.RightBottom, _shape.GetLocation(27, 26));
            Assert.AreEqual(Shape.Location.None, _shape.GetLocation(20, 20));
        }

        // test reset point without resizing
        [TestMethod()]
        public void ResetPointWithoutResizingTest()
        {
            _shape = new Rectangle(new Point(50, 50), new Point(30, 30));
            _shape.IsResizing = true;
            _shape.ResetPointWithoutResizing();
            Assert.AreEqual(50, _shape.Point1.X);
            Assert.AreEqual(50, _shape.Point1.Y);
            Assert.AreEqual(30, _shape.Point2.X);
            Assert.AreEqual(30, _shape.Point2.Y);
            _shape.IsResizing = false;
            _shape.ResetPointWithoutResizing();
            Assert.AreEqual(30, _shape.Point1.X);
            Assert.AreEqual(30, _shape.Point1.Y);
            Assert.AreEqual(50, _shape.Point2.X);
            Assert.AreEqual(50, _shape.Point2.Y);
        }

        // test set first point bottom
        [TestMethod()]
        public void SetFirstPointBottomTest()
        {
            _shape = new Line(new Point(0, 0), new Point(10, 10));
            _shape.SetFirstPointBottom();
            Assert.IsFalse(_shape.IsFirstPointBottom);
            _shape.Point1.Y = 20;
            _shape.SetFirstPointBottom();
            Assert.IsTrue(_shape.IsFirstPointBottom);
        }

        // test resize for panel
        [TestMethod()]
        public void ResizeForPanelTest()
        {
            _shape = new Ellipse(new Point(10, 10), new Point(20, 20));
            
            _shape.ResizeForPanel(0.5);

            Assert.AreEqual(5, _shape.Point1.X);
            Assert.AreEqual(5, _shape.Point1.Y);
            Assert.AreEqual(10, _shape.Point2.X);
            Assert.AreEqual(10, _shape.Point2.Y);
        }
    }
}