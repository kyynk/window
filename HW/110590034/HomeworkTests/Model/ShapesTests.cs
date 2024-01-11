using Homework.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Homework.Model.Tests
{
    [TestClass()]
    public class ShapesTests
    {
        private Shapes _shapes;

        // setup
        [TestInitialize()]
        public void Initialize()
        {
            _shapes = new Shapes();
        }

        // test constructor
        [TestMethod()]
        public void ShapesTest()
        {
            _shapes = new Shapes();
            Assert.AreEqual(0, _shapes.ShapeList.Count);
        }

        // test insert shape by index
        [TestMethod()]
        public void InsertShapeByIndexTest()
        {
            int initialCount = _shapes.ShapeList.Count;
            _shapes.InsertShapeByIndex(new Line(new Point(0, 0), new Point(10, 10)), 0);
            Assert.AreEqual(initialCount + 1, _shapes.ShapeList.Count);
            _shapes.DeleteShapeByIndex(0);
        }

        // test delete shape by index
        [TestMethod()]
        public void DeleteShapeByIndexTest()
        {
            _shapes.InsertShapeByIndex(new Rectangle(new Point(0, 0), new Point(10, 10)), 0);
            int initialCount = _shapes.ShapeList.Count;
            _shapes.DeleteShapeByIndex(0);
            Assert.AreEqual(initialCount - 1, _shapes.ShapeList.Count);
        }

        // test check select
        [TestMethod()]
        public void CheckSelectTest()
        {
            // test selected shape is null
            _shapes.InsertShapeByIndex(new Rectangle(new Point(3, 3), new Point(10, 10)), 0);
            bool isSelected = _shapes.CheckSelect(15, 15);
            Assert.IsFalse(isSelected);
            Assert.IsNull(_shapes.GetSelectedShape());
            // test selected shape
            _shapes.InsertShapeByIndex(new Rectangle(new Point(0, 0), new Point(10, 10)), 1);
            isSelected = _shapes.CheckSelect(5, 5);
            Assert.IsTrue(isSelected);
            Assert.IsNotNull(_shapes.GetSelectedShape());
            Assert.IsTrue(_shapes.GetSelectedShape().IsSelected);
            // test branch coverage for repeat CheckSelect
            isSelected = _shapes.CheckSelect(5, 5);
            Assert.IsTrue(isSelected);
            // clean
            _shapes.DeleteShapeByIndex(0);
            _shapes.DeleteShapeByIndex(0);
        }

        // test get selected shape
        [TestMethod()]
        public void GetSelectedShapeTest()
        {
            _shapes.InsertShapeByIndex(new Rectangle(new Point(0, 0), new Point(10, 10)), 0);
            _shapes.CheckSelect(5, 5);
            Assert.IsNotNull(_shapes.GetSelectedShape());
            _shapes.DeleteShapeByIndex(0);
        }

        // test move selected shape
        [TestMethod()]
        public void MoveSelectedShapeTest()
        {
            // selected shape is null
            _shapes.MoveSelectedShape(10, 0);
            Assert.AreEqual(0, _shapes.ShapeList.Count);
            // selected shape
            _shapes.InsertShapeByIndex(new Line(new Point(0, 0), new Point(10, 10)), 0);
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
        public void ClearSelectedShapeTest()
        {
            _shapes.InsertShapeByIndex(new Line(new Point(0, 0), new Point(10, 10)), 0);
            _shapes.CheckSelect(5, 5);
            _shapes.ClearSelectedShape();
            Assert.IsNull(_shapes.GetSelectedShape());
            _shapes.DeleteShapeByIndex(0);
            Assert.AreEqual(0, _shapes.ShapeList.Count);
        }

        // test resize selected shape
        [TestMethod()]
        public void ResizeSelectedShapeTest()
        {
            // selected shape is null
            _shapes.ResizeSelectedShape(Shape.Location.RightBottom, new Point(15, 15));
            Assert.IsNull(_shapes.GetSelectedShape());
            // selected shape not null
            _shapes.InsertShapeByIndex(new Ellipse(new Point(0, 0), new Point(10, 10)), 0);
            _shapes.CheckSelect(5, 5);
            _shapes.ResizeSelectedShape(Shape.Location.RightBottom, new Point(15, 15));
            Assert.AreEqual(0, _shapes.GetSelectedShape().Point1.X);
            Assert.AreEqual(0, _shapes.GetSelectedShape().Point1.Y);
            Assert.AreEqual(15, _shapes.GetSelectedShape().Point2.X);
            Assert.AreEqual(15, _shapes.GetSelectedShape().Point2.Y);
            // clean
            _shapes.ClearSelectedShape();
        }

        // test cancel resize
        [TestMethod()]
        public void CancelResizeTest()
        {
            // selected shape is null
            _shapes.CancelResize();
            Assert.IsNull(_shapes.GetSelectedShape());
            // selected shape not null
            _shapes.InsertShapeByIndex(new Ellipse(new Point(0, 0), new Point(10, 10)), 0);
            _shapes.CheckSelect(5, 5);
            _shapes.ResizeSelectedShape(Shape.Location.RightBottom, new Point(15, 15));
            Assert.IsTrue(_shapes.GetSelectedShape().IsResizing);
            _shapes.CancelResize();
            Assert.IsFalse(_shapes.GetSelectedShape().IsResizing);
            // clean
            _shapes.ClearSelectedShape();
        }

        // test get selected shape location
        [TestMethod()]
        public void GetSelectedShapeLocationTest()
        {
            _shapes.InsertShapeByIndex(new Ellipse(new Point(0, 0), new Point(10, 10)), 0);
            _shapes.CheckSelect(5, 5);
            Assert.AreEqual(Shape.Location.Right, _shapes.GetSelectedShapeLocation(7, 7));
            Assert.AreEqual(Shape.Location.None, _shapes.GetSelectedShapeLocation(20, 20));
            // clean
            _shapes.ClearSelectedShape();
        }

        // test set selected shape is first point bottom
        [TestMethod()]
        public void SetSelectedShapeIsFirstPointBottomWithFalseTest()
        {
            _shapes.InsertShapeByIndex(new Line(new Point(0, 0), new Point(10, 10)), 0);
            _shapes.CheckSelect(5, 5);
            _shapes.SetSelectedShapeIsFirstPointBottom();
            Assert.IsFalse(_shapes.GetSelectedShape().IsFirstPointBottom);
            //clean
            _shapes.ClearSelectedShape();
        }

        // test set selected shape is first point bottom
        [TestMethod()]
        public void SetSelectedShapeIsFirstPointBottomWithTrueTest()
        {
            _shapes.InsertShapeByIndex(new Line(new Point(11, 20), new Point(20, 11)), 0);
            _shapes.CheckSelect(15, 15);
            _shapes.SetSelectedShapeIsFirstPointBottom();
            Assert.IsTrue(_shapes.GetSelectedShape().IsFirstPointBottom);
            //clean
            _shapes.ClearSelectedShape();
        }

        // test resize for panel
        [TestMethod()]
        public void ResizeForPanelTest()
        {
            _shapes.InsertShapeByIndex(new Line(new Point(10, 20), new Point(20, 10)), 0);
            _shapes.ResizeForPanel(0.5);
            Assert.AreEqual(5, _shapes.ShapeList[0].Point1.X);
            Assert.AreEqual(10, _shapes.ShapeList[0].Point1.Y);
            Assert.AreEqual(10, _shapes.ShapeList[0].Point2.X);
            Assert.AreEqual(5, _shapes.ShapeList[0].Point2.Y);
            // clean
            _shapes.DeleteShapeByIndex(0);
        }
    }
}