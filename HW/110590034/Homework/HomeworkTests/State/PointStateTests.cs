using Microsoft.VisualStudio.TestTools.UnitTesting;
using Homework.Model.Tests;
using Homework.Model;
using Moq;

namespace Homework.State.Tests
{
    [TestClass()]
    public class PointStateTests
    {
        private Mock<Model.Model> _mockModel;
        private MockGraphics _mockGraphics;
        private PointState _pointState;
        private PrivateObject _privatePointState;

        // setup
        [TestInitialize()]
        public void Initialize()
        {
            _mockModel = new Mock<Model.Model>();
            _mockGraphics = new MockGraphics();
            _pointState = new PointState(_mockModel.Object);
            _privatePointState = new PrivateObject(_pointState);
        }

        // test constructor
        [TestMethod()]
        public void PointStateTest()
        {
            _pointState = new PointState(_mockModel.Object);
            _privatePointState = new PrivateObject(_pointState);
            Assert.IsNotNull((Model.Model)_privatePointState.GetField("_model"));
            Assert.AreEqual(-1, ((Point)_privatePointState.GetField("_point")).X);
            Assert.AreEqual(-1, ((Point)_privatePointState.GetField("_point")).Y);
            Assert.IsFalse(_pointState.IsSelected);
            Assert.IsFalse(_pointState.IsClicked);
        }

        // test mouse down
        [TestMethod()]
        public void MouseDownWithoutClickAndSelectTest()
        {
            // not click, select
            Point mouse = new Point(10, 10);
            _mockModel.Setup(model => model.CheckSelectedShape(It.IsAny<double>(), It.IsAny<double>())).Returns(false);
            _pointState.MouseDown(mouse, Constant.Constant.LINE, true);
            _mockModel.Verify(model => model.CheckSelectedShape(mouse.X, mouse.Y), Times.Exactly(2));
            Assert.IsFalse(_pointState.IsClicked);
            Assert.IsFalse(_pointState.IsSelected);
        }

        // test mouse down
        [TestMethod()]
        public void MouseDownWithClickAndSelectTest()
        {
            // click, select
            Point mouse = new Point(10, 10);
            _mockModel.Setup(model => model.CheckSelectedShape(It.IsAny<double>(), It.IsAny<double>())).Returns(true);
            _pointState.MouseDown(mouse, Constant.Constant.LINE, true);
            _mockModel.Verify(model => model.CheckSelectedShape(mouse.X, mouse.Y), Times.Exactly(2));
            Assert.IsTrue(_pointState.IsClicked);
            Assert.IsTrue(_pointState.IsSelected);
        }

        // test get diff
        [TestMethod()]
        public void GetDifferenceTest()
        {
            Assert.AreEqual(5, _pointState.GetDifference(10, 5));
        }

        // test mouse move
        [TestMethod()]
        public void MouseMoveWithoutSelectTest()
        {
            Point mouse = new Point(20, 20);
            _pointState.IsSelected = false;
            _pointState.MouseMove(mouse);
            _mockModel.Verify(model => model.MoveSelectedShape(It.IsAny<double>(), It.IsAny<double>()), Times.Never);
        }

        // test mouse move
        [TestMethod()]
        public void MouseMoveWithSelectTest()
        {
            Point mouse = new Point(20, 20);
            _pointState.IsSelected = true;
            _pointState.MouseMove(mouse);
            // initial x, y = -1, -1
            _mockModel.Verify(model => model.MoveSelectedShape(-21, -21), Times.Once);
        }

        // test mouse up
        // not select => _isSelected = false
        [TestMethod()]
        public void MouseUpWithSelectTest()
        {
            // select
            _pointState.IsSelected = true;
            _pointState.MouseUp(new Point(5, 5), Constant.Constant.NONE);
            Assert.IsFalse(_pointState.IsSelected);
        }

        // test drawing
        [TestMethod()]
        public void DrawingWithShapeNullOrNotClickTest()
        {
            var mockShape = new Mock<Shape>();
            _pointState.IsClicked = false;
            _mockModel.Setup(model => model.GetSelectedShape()).Returns((Shape)null);
            _pointState.Drawing(_mockGraphics);
            mockShape.Verify(shape => shape.DrawHint(_mockGraphics), Times.Never);
        }

        // test drawing
        [TestMethod()]
        public void DrawingWithShapeNotNullAndClickTest()
        {
            var mockShape = new Mock<Shape>();
            _pointState.IsClicked = true;
            _mockModel.Setup(model => model.GetSelectedShape()).Returns(mockShape.Object);
            _pointState.Drawing(_mockGraphics);
            mockShape.Verify(shape => shape.DrawHint(_mockGraphics), Times.Once);
        }
    }
}