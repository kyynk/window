using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.ComponentModel;
using System.Windows.Forms;
using Moq;

namespace Homework.Model.Tests
{
    [TestClass()]
    public class ModelTests
    {
        private Model _model;
        private PrivateObject _privateModel;

        // setup
        [TestInitialize()]
        public void Initialize()
        {
            _model = new Model();
            _privateModel = new PrivateObject(_model);
        }

        // test constructor
        [TestMethod()]
        public void ModelTest()
        {
            _model = new Model();
            _privateModel = new PrivateObject(_model);

            Assert.IsNotNull(_model);
            Assert.AreEqual(Constant.Constant.POINT, _model.ShapeName);
            Assert.IsNotNull(_model.GetShapes());
            Assert.AreEqual(490, (int)_privateModel.GetField("_panelMaxX"));
            Assert.AreEqual(415, (int)_privateModel.GetField("_panelMaxY"));
            Assert.IsNull((Shape)_privateModel.GetField("_tempShape"));
            Assert.IsNotNull((Shapes)_privateModel.GetField("_shapesData"));
            Assert.IsNotNull((ShapeFactory)_privateModel.GetField("_shapeFactory"));
            Assert.IsInstanceOfType(_privateModel.GetField("_state"), typeof(PointState));
        }

        // test change state
        [TestMethod()]
        public void ChangeStateTest()
        {
            _model.ChangeState(Constant.Constant.DRAWING_STATE);
            Assert.IsInstanceOfType(_privateModel.GetField("_state"), typeof(DrawingState));
            _model.ChangeState(Constant.Constant.POINT_STATE);
            Assert.IsInstanceOfType(_privateModel.GetField("_state"), typeof(PointState));
        }

        // test ShapeName property
        [TestMethod()]
        public void ShapeNamePropertyTest()
        {
            _model.ShapeName = Constant.Constant.LINE;
            Assert.AreEqual(Constant.Constant.LINE, _model.ShapeName);
        }

        // test press pointer
        [TestMethod()]
        public void PressPointerTest()
        {
            Mock<IState> mockState = new Mock<IState>();
            _privateModel.SetField("_state", mockState.Object);
            _privateModel.SetField("_panelMaxX", 100);
            _privateModel.SetField("_panelMaxY", 100);
            // test branch coverage
            _model.PressPointer(10, 20);
            _model.PressPointer(1000, 20);
            _model.PressPointer(-10, 20);
            _model.PressPointer(10, 200);
            _model.PressPointer(10, -20);
            _model.PressPointer(1000, 2000);
            _model.PressPointer(-10, -20);
            mockState.Verify(state => state.MouseDown(It.IsAny<Point>(), It.IsAny<string>(), It.IsAny<bool>()), Times.Exactly(7));
        }

        // test move pointer
        [TestMethod()]
        public void MovePointerTest()
        {
            Mock<IState> mockState = new Mock<IState>();
            _privateModel.SetField("_state", mockState.Object);
            double mouseX = 10;
            double mouseY = 20;
            _model.MovePointer(mouseX, mouseY);
            mockState.Verify(state => state.MouseMove(It.IsAny<Point>()), Times.Once);
        }

        // test release pointer
        [TestMethod()]
        public void ReleasePointerTest()
        {
            Mock<IState> mockState = new Mock<IState>();
            _privateModel.SetField("_state", mockState.Object);
            double mouseX = 10;
            double mouseY = 20;
            _model.ReleasePointer(mouseX, mouseY);
            mockState.Verify(state => state.MouseUp(It.IsAny<Point>(), It.IsAny<string>()), Times.Once);
        }

        // test draw
        [TestMethod()]
        public void DrawTest()
        {
            var mockGraphics = new MockGraphics();
            var mockState = new Mock<IState>();
            _privateModel.SetField("_state", mockState.Object);
            _model.Create(Constant.Constant.LINE);
            _model.Draw(mockGraphics);
            Assert.AreEqual(1, mockGraphics.DrawLineCount);
            mockState.Verify(state => state.Drawing(mockGraphics), Times.Once);
            // clean
            _model.Delete(0);
        }

        // test create drawing shape
        [TestMethod()]
        public void CreateDrawingShapeTest()
        {
            string shapeType = Constant.Constant.LINE;
            Point firstPoint = new Point(10, 20);
            _model.CreateDrawingShape(shapeType, firstPoint);
            Assert.IsNotNull(_privateModel.GetField("_tempShape"));
            Assert.AreEqual(shapeType, ((Line)_privateModel.GetField("_tempShape")).GetShapeName());
            // clean
            _model.ClearDrawingShape();
        }

        // test move drawing shape
        [TestMethod()]
        public void MoveDrawingShapeTest()
        {
            string shapeType = Constant.Constant.LINE;
            Point firstPoint = new Point(10, 20);
            _model.CreateDrawingShape(shapeType, firstPoint);
            Point secondPoint = new Point(20, 20);
            _model.MoveDrawingShape(secondPoint);
            Assert.AreEqual(secondPoint.X, ((Shape)_privateModel.GetField("_tempShape")).Point2.X);
            Assert.AreEqual(secondPoint.Y, ((Shape)_privateModel.GetField("_tempShape")).Point2.Y);
            // clean
            _model.ClearDrawingShape();
        }

        // test add drawing shape
        [TestMethod()]
        public void AddDrawingShapeTest()
        {
            _model.AddDrawingShape(Constant.Constant.LINE, new Point(10, 20), new Point(20, 20));
            _model.AddDrawingShape(Constant.Constant.RECTANGLE, new Point(0, 10), new Point(10, 0));
            _model.AddDrawingShape(Constant.Constant.ELLIPSE, new Point(50, 60), new Point(70, 80));
            Assert.AreEqual(3, _model.GetShapes().Count);
            Assert.AreEqual(Constant.Constant.LINE, _model.GetShapes()[0].GetShapeName());
            Assert.AreEqual(Constant.Constant.RECTANGLE, _model.GetShapes()[1].GetShapeName());
            Assert.AreEqual(Constant.Constant.ELLIPSE, _model.GetShapes()[2].GetShapeName());
            // clean
            _model.Delete(0);
            _model.Delete(0);
            _model.Delete(0);
        }

        // test clear drawing shape
        [TestMethod()]
        public void ClearDrawingShapeTest()
        {
            string shapeType = Constant.Constant.LINE;
            Point firstPoint = new Point(10, 20);
            _model.CreateDrawingShape(shapeType, firstPoint);
            Assert.IsNotNull(_privateModel.GetField("_tempShape"));
            _model.ClearDrawingShape();
            Assert.IsNull(_privateModel.GetField("_tempShape"));
        }

        // test draw drawing shape
        [TestMethod()]
        public void DrawDrawingShapeTest()
        {
            string shapeType = Constant.Constant.LINE;
            Point firstPoint = new Point(10, 20);
            MockGraphics mockGraphics = new MockGraphics();
            _model.CreateDrawingShape(shapeType, firstPoint);
            _model.DrawDrawingShape(mockGraphics);
            Assert.AreEqual(1, mockGraphics.DrawLineCount);
            // clean
            _model.ClearDrawingShape();
        }

        // test get selected shape
        [TestMethod()]
        public void GetSelectedShapeTest()
        {
            _model.AddDrawingShape(Constant.Constant.LINE, new Point(0, 0), new Point(10, 10));
            _model.AddDrawingShape(Constant.Constant.RECTANGLE, new Point(11, 11), new Point(20, 20));
            _model.AddDrawingShape(Constant.Constant.ELLIPSE, new Point(50, 50), new Point(70, 70));
            bool isSelected = _model.CheckSelectedShape(5, 5);
            Assert.IsInstanceOfType(_model.GetSelectedShape(), typeof(Shape));
            Assert.IsTrue(isSelected);
            Assert.AreEqual(Constant.Constant.LINE, _model.GetSelectedShape().GetShapeName());

            // clean
            _model.Delete(0);
            _model.Delete(0);
            _model.Delete(0);
        }

        // test check selected shape
        [TestMethod()]
        public void CheckSelectedShapeTest()
        {
            _model.AddDrawingShape(Constant.Constant.LINE, new Point(0, 0), new Point(10, 10));
            _model.AddDrawingShape(Constant.Constant.RECTANGLE, new Point(11, 11), new Point(20, 20));
            _model.AddDrawingShape(Constant.Constant.ELLIPSE, new Point(50, 50), new Point(70, 70));
            Assert.IsTrue(_model.CheckSelectedShape(5, 5));
            Assert.IsTrue(_model.CheckSelectedShape(15, 15));
            Assert.IsTrue(_model.CheckSelectedShape(65, 65));
            Assert.IsFalse(_model.CheckSelectedShape(105, 105));
            // clean
            _model.Delete(0);
            _model.Delete(0);
            _model.Delete(0);
        }

        // test move selected shape
        [TestMethod()]
        public void MoveSelectedShapeTest()
        {
            _model.AddDrawingShape(Constant.Constant.LINE, new Point(6, 5), new Point(11, 10));
            Assert.IsTrue(_model.CheckSelectedShape(8, 8));
            double diffX = 5;
            double diffY = 5;
            _model.MoveSelectedShape(diffX, diffY);
            Assert.AreEqual(6 - diffX, _model.GetSelectedShape().Point1.X);
            Assert.AreEqual(5 - diffY, _model.GetSelectedShape().Point1.Y);
            Assert.AreEqual(11 - diffX, _model.GetSelectedShape().Point2.X);
            Assert.AreEqual(10 - diffY, _model.GetSelectedShape().Point2.Y);
            // clean
            _model.Delete(0);
        }

        // test check range
        [TestMethod()]
        public void CheckRangeTest()
        {
            double max = 100;
            Assert.AreEqual(0, _model.CheckRange(-10, max));
            Assert.AreEqual(50, _model.CheckRange(50, max));
            Assert.AreEqual(max, _model.CheckRange(max, max));
            Assert.AreEqual(max, _model.CheckRange(120, max));
        }

        // test create
        [TestMethod()]
        public void CreateTest()
        {
            _model.Create(Constant.Constant.LINE);
            _model.Create(Constant.Constant.RECTANGLE);
            _model.Create(Constant.Constant.ELLIPSE);
            Assert.AreEqual(3, _model.GetShapes().Count);
            Assert.AreEqual(Constant.Constant.LINE, _model.GetShapes()[0].GetShapeName());
            Assert.AreEqual(Constant.Constant.RECTANGLE, _model.GetShapes()[1].GetShapeName());
            Assert.AreEqual(Constant.Constant.ELLIPSE, _model.GetShapes()[2].GetShapeName());
            // clean
            _model.Delete(0);
            _model.Delete(0);
            _model.Delete(0);
        }

        // test get shapes
        [TestMethod()]
        public void GetShapesTest()
        {
            Assert.IsInstanceOfType(_model.GetShapes(), typeof(BindingList<Shape>));
            _model.Create(Constant.Constant.LINE);
            _model.Create(Constant.Constant.RECTANGLE);
            _model.Create(Constant.Constant.ELLIPSE);
            Assert.AreEqual(3, _model.GetShapes().Count);
            Assert.AreEqual(Constant.Constant.LINE, _model.GetShapes()[0].GetShapeName());
            Assert.AreEqual(Constant.Constant.RECTANGLE, _model.GetShapes()[1].GetShapeName());
            Assert.AreEqual(Constant.Constant.ELLIPSE, _model.GetShapes()[2].GetShapeName());
            // clean
            _model.Delete(0);
            _model.Delete(0);
            _model.Delete(0);
        }

        // test delete
        [TestMethod()]
        public void DeleteTest()
        {
            _model.Create(Constant.Constant.LINE);
            _model.Create(Constant.Constant.RECTANGLE);
            _model.Create(Constant.Constant.ELLIPSE);
            Assert.AreEqual(3, _model.GetShapes().Count);
            _model.Delete(0);
            Assert.AreEqual(2, _model.GetShapes().Count);
            _model.Delete(0);
            Assert.AreEqual(1, _model.GetShapes().Count);
            _model.Delete(0);
            Assert.AreEqual(0, _model.GetShapes().Count);
        }

        // test handle keydown
        [TestMethod()]
        public void HandleKeyDownTest()
        {
            _model.AddDrawingShape(Constant.Constant.LINE, new Point(0, 0), new Point(10, 10));
            _model.AddDrawingShape(Constant.Constant.RECTANGLE, new Point(11, 11), new Point(20, 20));
            _model.AddDrawingShape(Constant.Constant.ELLIPSE, new Point(50, 50), new Point(70, 70));
            // select
            Assert.IsTrue(_model.CheckSelectedShape(6, 5));
            // test not Delete key
            Assert.AreEqual(Constant.Constant.LINE, _model.GetShapes()[0].ShapeName);
            Assert.AreEqual(Constant.Constant.LINE, _model.GetSelectedShape().ShapeName);
            _model.HandleKeyDown(Keys.Escape);
            Assert.AreEqual(Constant.Constant.LINE, _model.GetShapes()[0].ShapeName);
            Assert.AreEqual(3, _model.GetShapes().Count);
            Assert.IsNotNull(_model.GetSelectedShape());
            // test Delete key
            _model.HandleKeyDown(Keys.Delete);
            Assert.AreEqual(Constant.Constant.RECTANGLE, _model.GetShapes()[0].ShapeName);
            Assert.AreEqual(2, _model.GetShapes().Count);
            Assert.IsNull(_model.GetSelectedShape());
            // clean
            _model.Delete(0);
            _model.Delete(0);
        }

        // test notify model changed
        [TestMethod()]
        public void NotifyModelChangedTest()
        {
            bool eventRaised = false;
            _model._modelChanged += () => { eventRaised = true; };
            _model.NotifyModelChanged();
            Assert.IsTrue(eventRaised);
        }
    }
}