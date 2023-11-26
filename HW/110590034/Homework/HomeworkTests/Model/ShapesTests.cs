using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Homework.Model.Tests
{
    [TestClass()]
    public class ShapesTests
    {
        private Shapes _shapes;

        // setup
        [TestInitialize()]
        public void Setup()
        {
            _shapes = new Shapes();
        }

        // test add new shape by drawing
        [TestMethod()]
        public void AddNewShapeByDrawingTest()
        {
            int initialCount = _shapes.ShapeList.Count;
            _shapes.AddNewShapeByDrawing(Constant.Constant.LINE, new Point(0, 0), new Point(10, 10));
            Assert.AreEqual(initialCount + 1, _shapes.ShapeList.Count);
            _shapes.DeleteShapeByIndex(0);
        }

        // test add new shape by random
        [TestMethod()]
        public void AddNewShapeByRandomTest()
        {
            int initialCount = _shapes.ShapeList.Count;
            _shapes.AddNewShapeByRandom(Constant.Constant.ELLIPSE);
            Assert.AreEqual(initialCount + 1, _shapes.ShapeList.Count);
            _shapes.DeleteShapeByIndex(0);
        }

        // test delete shape by index
        [TestMethod()]
        public void DeleteShapeByIndexTest()
        {
            _shapes.AddNewShapeByDrawing(Constant.Constant.RECTANGLE, new Point(0, 0), new Point(10, 10));
            int initialCount = _shapes.ShapeList.Count;
            _shapes.DeleteShapeByIndex(0);
            Assert.AreEqual(initialCount - 1, _shapes.ShapeList.Count);
        }

        // test check select
        [TestMethod()]
        public void CheckSelectTest()
        {
            _shapes.AddNewShapeByDrawing(Constant.Constant.RECTANGLE, new Point(3, 3), new Point(10, 10));
            bool isSelected = _shapes.CheckSelect(15, 15);
            Assert.IsFalse(isSelected);
            Assert.IsNull(_shapes.GetSelectedShape());

            _shapes.AddNewShapeByDrawing(Constant.Constant.RECTANGLE, new Point(0, 0), new Point(10, 10));
            isSelected = _shapes.CheckSelect(5, 5);
            Assert.IsTrue(isSelected);
            Assert.IsNotNull(_shapes.GetSelectedShape());
            Assert.IsTrue(_shapes.GetSelectedShape().isSelected);
            
            isSelected = _shapes.CheckSelect(5, 5);
            Assert.IsTrue(isSelected);

            _shapes.DeleteShapeByIndex(0);
            _shapes.DeleteShapeByIndex(0);
        }

        // test get selected shape
        [TestMethod()]
        public void GetSelectedShapeTest()
        {
            _shapes.AddNewShapeByDrawing(Constant.Constant.RECTANGLE, new Point(0, 0), new Point(10, 10));
            _shapes.CheckSelect(5, 5);
            Shape selectedShape = _shapes.GetSelectedShape();
            Assert.IsNotNull(selectedShape);
            _shapes.DeleteShapeByIndex(0);
        }

        // test move selected shape
        [TestMethod()]
        public void MoveSelectedShapeTest()
        {
            // _selectedShape is null
            _shapes.MoveSelectedShape(10, 0);
            Assert.AreEqual(0, _shapes.ShapeList.Count);

            _shapes.AddNewShapeByDrawing(Constant.Constant.LINE, new Point(0, 0), new Point(10, 10));
            _shapes.CheckSelect(5, 5);
            double initialX1 = _shapes.GetSelectedShape().Point1.X;
            double initialY1 = _shapes.GetSelectedShape().Point1.Y;
            double initialX2 = _shapes.GetSelectedShape().Point2.X;
            double initialY2 = _shapes.GetSelectedShape().Point2.Y;
            _shapes.MoveSelectedShape(10, 0);
            Assert.AreEqual(initialX1 - 10, _shapes.GetSelectedShape().Point1.X);
            Assert.AreEqual(initialY1, _shapes.GetSelectedShape().Point1.Y);
            Assert.AreEqual(initialX2 - 10, _shapes.GetSelectedShape().Point2.X);
            Assert.AreEqual(initialY2, _shapes.GetSelectedShape().Point2.Y);
            _shapes.DeleteShapeByIndex(0);
        }

        // test delete selected shape
        [TestMethod()]
        public void DeleteSelectedShapeTest()
        {
            _shapes.AddNewShapeByDrawing(Constant.Constant.LINE, new Point(0, 0), new Point(10, 10));
            _shapes.CheckSelect(5, 5);
            _shapes.DeleteSelectedShape();
            Assert.AreEqual(0, _shapes.ShapeList.Count);
        }
    }
}