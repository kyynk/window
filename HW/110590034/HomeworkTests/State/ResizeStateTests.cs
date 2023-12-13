using Microsoft.VisualStudio.TestTools.UnitTesting;
using Homework.Model;
using Homework.Model.Tests;
using Moq;

namespace Homework.State.Tests
{
    [TestClass()]
    public class ResizeStateTests
    {
        private ResizeState _resizeState;
        private PrivateObject _privateState;
        private Mock<Model.Model> _mockModel;

        // setup
        [TestInitialize()]
        public void Initialize()
        {
            _mockModel = new Mock<Model.Model>();
            _resizeState = new ResizeState(_mockModel.Object);
            _privateState = new PrivateObject(_resizeState);
        }

        // test constructor
        [TestMethod()]
        public void ResizeStateTest()
        {
            _mockModel = new Mock<Model.Model>();
            _resizeState = new ResizeState(_mockModel.Object);
            _privateState = new PrivateObject(_resizeState);
            Assert.IsNotNull(_privateState.GetField("_model"));
            Assert.IsFalse((bool)_privateState.GetField("_isPressed"));
        }

        // test mouse down
        [TestMethod()]
        public void MouseDownTest()
        {
            _resizeState.MouseDown(new Point(1, 1), Constant.Constant.LINE, true);
            Assert.IsTrue((bool)_privateState.GetField("_isPressed"));
        }

        // test mouse move
        [TestMethod()]
        public void MouseMoveTest()
        {
            _privateState.SetField("_isPressed", false);
            _resizeState.MouseMove(new Point(1, 1));
            _mockModel.Verify(model => model.ResizeSelectedShape(It.IsAny<double>(), It.IsAny<double>()), Times.Never);
            _privateState.SetField("_isPressed", true);
            _resizeState.MouseMove(new Point(1, 1));
            _mockModel.Verify(model => model.ResizeSelectedShape(It.IsAny<double>(), It.IsAny<double>()), Times.Once);
        }

        // test mouse up
        [TestMethod()]
        public void MouseUpTest()
        {
            _resizeState.MouseUp(new Point(1, 1), Constant.Constant.LINE);
            Assert.IsFalse((bool)_privateState.GetField("_isPressed"));
            _mockModel.Verify(model => model.StopResizeSelectedShape(), Times.Once);
        }

        // test drawing
        [TestMethod()]
        public void DrawingWithShapeNullTest()
        {
            var mockShape = new Mock<Shape>();
            var mockGraphics = new MockGraphics();
            _mockModel.Setup(model => model.GetSelectedShape()).Returns((Shape)null);
            _resizeState.Drawing(mockGraphics);
            mockShape.Verify(shape => shape.DrawHint(mockGraphics), Times.Never);
        }

        // test drawing
        [TestMethod()]
        public void DrawingWithShapeNotNullTest()
        {
            var mockShape = new Mock<Shape>();
            var mockGraphics = new MockGraphics();
            _mockModel.Setup(model => model.GetSelectedShape()).Returns(mockShape.Object);
            _resizeState.Drawing(mockGraphics);
            mockShape.Verify(shape => shape.DrawHint(mockGraphics), Times.Once);
        }
    }
}