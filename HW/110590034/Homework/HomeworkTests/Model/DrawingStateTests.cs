using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Homework.Model.Tests
{
    [TestClass()]
    public class DrawingStateTests
    {
        private Shapes _shapes;
        private DrawingState _drawingState;
        private ShapeFactory _shapeFactory;
        private MockGraphics _mockGraphics;

        // setup
        [TestInitialize()]
        public void Initialize()
        {
            _shapes = new Shapes();
            _drawingState = new DrawingState();
            _mockGraphics = new MockGraphics();
            _shapeFactory = new ShapeFactory();
        }

        // test check x
        [TestMethod()]
        public void CheckXTest()
        {
            Assert.IsTrue(_drawingState.CheckX(100));
            Assert.IsFalse(_drawingState.CheckX(600));
            Assert.IsFalse(_drawingState.CheckX(-600));
        }

        // test check y
        [TestMethod()]
        public void CheckYTest()
        {
            Assert.IsTrue(_drawingState.CheckY(100));
            Assert.IsFalse(_drawingState.CheckY(600));
            Assert.IsFalse(_drawingState.CheckY(-600));
        }

        // test check range
        [TestMethod()]
        public void CheckRangeTest()
        {
            Assert.IsTrue(_drawingState.CheckRange(new Point(100, 100)));
            Assert.IsFalse(_drawingState.CheckRange(new Point(600, 600)));
        }

        // test mouse down
        [TestMethod()]
        public void MouseDownTest()
        {
            // not valid range
            _drawingState.MouseDown(new Point(-1, -1), Constant.Constant.LINE, ref _shapes, ref _shapeFactory);
            Assert.IsNull(_drawingState.TempShape);
            Assert.IsFalse(_drawingState.IsPressed);
            // valid range
            Point mousePoint = new Point(50, 50);
            _drawingState.MouseDown(mousePoint, Constant.Constant.LINE, ref _shapes, ref _shapeFactory);
            Assert.IsNotNull(_drawingState.TempShape);
            Assert.IsTrue(_drawingState.IsPressed);
            // clean
            _drawingState.MouseUp(mousePoint, Constant.Constant.LINE, ref _shapes);
            _shapes.DeleteShapeByIndex(0);
        }

        // test mouse move
        [TestMethod()]
        public void MouseMoveTest()
        {
            Point mousePoint = new Point(50, 50);
            Point movePoint = new Point(100, 100);
            // not press then no move, and no temp shape
            _drawingState.MouseDown(new Point(-1, -1), Constant.Constant.LINE, ref _shapes, ref _shapeFactory);
            _drawingState.MouseMove(movePoint, ref _shapes);
            Assert.IsNull(_drawingState.TempShape);
            Assert.IsFalse(_drawingState.IsPressed);
            // press then move
            _drawingState.MouseDown(mousePoint, Constant.Constant.LINE, ref _shapes, ref _shapeFactory);
            _drawingState.MouseMove(movePoint, ref _shapes);
            Assert.IsNotNull(_drawingState.TempShape);
            Assert.IsTrue(_drawingState.IsPressed);
            Assert.AreEqual(movePoint.X, _drawingState.TempShape.Point2.X);
            Assert.AreEqual(movePoint.Y, _drawingState.TempShape.Point2.Y);
            // clean
            _drawingState.MouseUp(mousePoint, Constant.Constant.LINE, ref _shapes);
            _shapes.DeleteShapeByIndex(0);
        }

        // test mouse up
        [TestMethod()]
        public void MouseUpTest()
        {
            Point mousePoint = new Point(50, 50);
            // not press valid range then mouse up
            _drawingState.MouseDown(new Point(-1, -1), Constant.Constant.LINE, ref _shapes, ref _shapeFactory);
            _drawingState.MouseUp(mousePoint, Constant.Constant.LINE, ref _shapes);
            Assert.AreEqual(0, _shapes.ShapeList.Count);
            // press valid range then mouse up
            _drawingState.MouseDown(mousePoint, Constant.Constant.LINE, ref _shapes, ref _shapeFactory);
            _drawingState.MouseUp(mousePoint, Constant.Constant.LINE, ref _shapes);
            Assert.AreEqual(1, _shapes.ShapeList.Count);
            Assert.IsInstanceOfType(_shapes.ShapeList[0], typeof(Line));
            // clean
            _shapes.DeleteShapeByIndex(0);
        }

        // test drawing
        [TestMethod()]
        public void DrawingTest()
        {
            Point mousePoint = new Point(50, 50);
            // not press valid range then mouse up
            _drawingState.MouseDown(new Point(-1, -1), Constant.Constant.LINE, ref _shapes, ref _shapeFactory);
            _drawingState.Drawing(_mockGraphics, ref _shapes);
            Assert.IsNull(_drawingState.TempShape);
            Assert.IsFalse(_drawingState.IsPressed);
            Assert.AreEqual(0, _mockGraphics.CountLine);
            // press valid range then mouse up
            _drawingState.MouseDown(mousePoint, Constant.Constant.LINE, ref _shapes, ref _shapeFactory);
            _drawingState.Drawing(_mockGraphics, ref _shapes);
            Assert.IsNotNull(_drawingState.TempShape);
            Assert.IsTrue(_drawingState.IsPressed);
            Assert.AreEqual(1, _mockGraphics.CountLine);
            // clean
            _drawingState.MouseUp(mousePoint, Constant.Constant.LINE, ref _shapes);
            _shapes.DeleteShapeByIndex(0);
        }
    }
}