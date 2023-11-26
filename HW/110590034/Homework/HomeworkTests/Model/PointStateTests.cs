using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Homework.Model.Tests
{
    [TestClass()]
    public class PointStateTests
    {
        private Shapes _shapes;
        private PointState _pointState;
        private ShapeFactory _shapeFactory;
        private MockGraphics _mockGraphics;

        // setup
        [TestInitialize()]
        public void Initialize()
        {
            _shapes = new Shapes();
            _pointState = new PointState();
            _mockGraphics = new MockGraphics();
            _shapeFactory = new ShapeFactory();
            _shapes.AddNewShapeByDrawing(Constant.Constant.ELLIPSE, new Point(1, 1), new Point(10, 10));
        }

        // test mouse down
        [TestMethod()]
        public void MouseDownTest()
        {
            // not select
            _pointState.MouseDown(new Point(70, 100), Constant.Constant.NONE, ref _shapes, ref _shapeFactory);
            Assert.IsFalse(_pointState.IsClicked);
            Assert.IsFalse(_pointState.IsSelected);
            Assert.AreEqual(70, _pointState.ThisPoint.X);
            Assert.AreEqual(100, _pointState.ThisPoint.Y);
            // select
            _pointState.MouseDown(new Point(5, 5), Constant.Constant.NONE, ref _shapes, ref _shapeFactory);
            Assert.IsTrue(_pointState.IsClicked);
            Assert.IsTrue(_pointState.IsSelected);
            Assert.AreEqual(5, _pointState.ThisPoint.X);
            Assert.AreEqual(5, _pointState.ThisPoint.Y);
            // clean
            _pointState.MouseUp(new Point(5, 5), Constant.Constant.NONE, ref _shapes);
        }

        // test get diff
        [TestMethod()]
        public void GetDifferenceTest()
        {
            Assert.AreEqual(5, _pointState.GetDifference(10, 5));
        }

        // test mouse move
        [TestMethod()]
        public void MouseMoveTest()
        {
            _shapes.AddNewShapeByDrawing(Constant.Constant.LINE, new Point(30, 30), new Point(50, 50));
            // not select
            _pointState.MouseDown(new Point(70, 100), Constant.Constant.NONE, ref _shapes, ref _shapeFactory);
            _pointState.MouseMove(new Point(80, 110), ref _shapes);
            Assert.IsFalse(_pointState.IsSelected);
            Assert.AreEqual(70, _pointState.ThisPoint.X);
            Assert.AreEqual(100, _pointState.ThisPoint.Y);
            // select
            _pointState.MouseDown(new Point(35, 33), Constant.Constant.NONE, ref _shapes, ref _shapeFactory);
            _pointState.MouseMove(new Point(36, 34), ref _shapes);
            Assert.IsTrue(_pointState.IsSelected);
            Assert.AreEqual(36, _pointState.ThisPoint.X);
            Assert.AreEqual(34, _pointState.ThisPoint.Y);
            // clean
            _pointState.MouseUp(new Point(36, 34), Constant.Constant.NONE, ref _shapes);
        }

        // test mouse up
        [TestMethod()]
        public void MouseUpTest()
        {
            // not select
            _pointState.MouseDown(new Point(70, 100), Constant.Constant.NONE, ref _shapes, ref _shapeFactory);
            Assert.IsFalse(_pointState.IsSelected);
            _pointState.MouseUp(new Point(5, 5), Constant.Constant.NONE, ref _shapes);
            Assert.IsFalse(_pointState.IsSelected);
            // select
            _pointState.MouseDown(new Point(5, 5), Constant.Constant.NONE, ref _shapes, ref _shapeFactory);
            Assert.IsTrue(_pointState.IsSelected);
            _pointState.MouseUp(new Point(5, 5), Constant.Constant.NONE, ref _shapes);
            Assert.IsFalse(_pointState.IsSelected);
        }

        // test drawing
        [TestMethod()]
        public void DrawingTest()
        {
            // not click shape
            _pointState.MouseDown(new Point(70, 100), Constant.Constant.NONE, ref _shapes, ref _shapeFactory);
            _pointState.Drawing(_mockGraphics, ref _shapes);
            Assert.IsFalse(_pointState.IsClicked);
            Assert.AreEqual(0, _mockGraphics.CountHint);
            Assert.AreEqual(0, _mockGraphics.CountHintCircle);
            // click shape
            _pointState.MouseDown(new Point(5, 5), Constant.Constant.NONE, ref _shapes, ref _shapeFactory);
            _pointState.Drawing(_mockGraphics, ref _shapes);
            Assert.IsTrue(_pointState.IsClicked);
            Assert.AreEqual(1, _mockGraphics.CountHint);
            Assert.AreEqual(8, _mockGraphics.CountHintCircle);
            // delete clicked shape without cancel
            _shapes.DeleteSelectedShape();
            _pointState.Drawing(_mockGraphics, ref _shapes);
            Assert.IsTrue(_pointState.IsClicked);
            Assert.AreEqual(1, _mockGraphics.CountHint);
            Assert.AreEqual(8, _mockGraphics.CountHintCircle);
        }
    }
}