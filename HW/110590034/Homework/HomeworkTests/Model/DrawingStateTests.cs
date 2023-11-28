using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Moq;

namespace Homework.Model.Tests
{
    [TestClass()]
    public class DrawingStateTests
    {
        private DrawingState _drawingState;
        private PrivateObject _privateState;
        private Mock<Model> _mockModel;
        private MockGraphics _mockGraphics;

        // setup
        [TestInitialize()]
        public void Initialize()
        {
            _mockModel = new Mock<Model>();
            _drawingState = new DrawingState(_mockModel.Object);
            _privateState = new PrivateObject(_drawingState);
            _mockGraphics = new MockGraphics();
        }

        // test constructor
        [TestMethod()]
        public void DrawingStateTest()
        {
            _mockModel = new Mock<Model>();
            _drawingState = new DrawingState(_mockModel.Object);
            _privateState = new PrivateObject(_drawingState);
            Assert.IsNotNull((Model)_privateState.GetField("_model"));
            Assert.AreEqual(-1, ((Point)_privateState.GetField("_point1")).X);
            Assert.AreEqual(-1, ((Point)_privateState.GetField("_point1")).Y);
            Assert.IsFalse((bool)_privateState.GetField("_isPressed"));
        }

        // test mouse down
        [TestMethod()]
        public void MouseDownNotInPanelTest()
        {
            Point mouse = new Point(10, 10);
            string shapeName = Constant.Constant.ELLIPSE;
            bool isInPanel = false;
            _drawingState.MouseDown(mouse, shapeName, isInPanel);
            _mockModel.Verify(m => m.CreateDrawingShape(shapeName, mouse), Times.Never);
            Assert.IsFalse((bool)_privateState.GetField("_isPressed"));
        }

        // test mouse down
        [TestMethod()]
        public void MouseDownInPanelTest()
        {
            Point mouse = new Point(10, 10);
            string shapeName = Constant.Constant.ELLIPSE;
            bool isInPanel = true;
            _drawingState.MouseDown(mouse, shapeName, isInPanel);
            _mockModel.Verify(m => m.CreateDrawingShape(shapeName, mouse), Times.Once);
            Assert.IsTrue((bool)_privateState.GetField("_isPressed"));
        }

        // test mouse move
        [TestMethod()]
        public void MouseMoveWhenIsNotPressedTest()
        {
            var mouse = new Point(20, 20);
            _privateState.SetField("_isPressed", false);
            _drawingState.MouseMove(mouse);
            _mockModel.Verify(m => m.MoveDrawingShape(mouse), Times.Never);
        }

        // test mouse move
        [TestMethod()]
        public void MouseMoveWhenIsPressedTest()
        {
            var mouse = new Point(20, 20);
            _privateState.SetField("_isPressed", true);
            _drawingState.MouseMove(mouse);
            _mockModel.Verify(m => m.MoveDrawingShape(mouse), Times.Once);
        }

        // test mouse up
        [TestMethod()]
        public void MouseUpWhenIsNotPressedTest()
        {
            _privateState.SetField("_isPressed", false);
            Point mouse = new Point(30, 30);
            string shapeName = Constant.Constant.ELLIPSE;
            _drawingState.MouseUp(mouse, shapeName);
            _mockModel.Verify(m => m.AddDrawingShape(shapeName, (Point)_privateState.GetField("_point1"), It.IsAny<Point>()), Times.Never);
            _mockModel.Verify(m => m.ClearDrawingShape(), Times.Never);
            Assert.IsFalse((bool)_privateState.GetField("_isPressed"));
        }

        // test mouse up
        [TestMethod()]
        public void MouseUpWhenIsPressedTest()
        {
            _privateState.SetField("_isPressed", true);
            Point mouse = new Point(30, 30);
            string shapeName = Constant.Constant.ELLIPSE;
            _drawingState.MouseUp(mouse, shapeName);
            _mockModel.Verify(m => m.AddDrawingShape(shapeName, It.IsAny<Point>(), It.IsAny<Point>()), Times.Once);
            _mockModel.Verify(m => m.ClearDrawingShape(), Times.Once);
            Assert.IsFalse((bool)_privateState.GetField("_isPressed"));
        }

        // test drawing
        [TestMethod()]
        public void DrawingWhenIsNotPressedTest()
        {
            _privateState.SetField("_isPressed", false);
            _drawingState.Drawing(_mockGraphics);
            _mockModel.Verify(m => m.DrawDrawingShape(_mockGraphics), Times.Never);
        }

        // test drawing
        [TestMethod()]
        public void DrawingWhenIsPressedTest()
        {
            _privateState.SetField("_isPressed", true);
            _drawingState.Drawing(_mockGraphics);
            _mockModel.Verify(m => m.DrawDrawingShape(_mockGraphics), Times.Once);
        }
    }
}