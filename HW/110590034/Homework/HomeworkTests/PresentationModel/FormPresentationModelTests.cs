using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing;
using Homework.View;
using Moq;

namespace Homework.PresentationModel.Tests
{
    [TestClass()]
    public class FormPresentationModelTests
    {
        private FormPresentationModel _presentationModel;
        private PrivateObject _privatePresentationModel;
        private Mock<Model.Model> _mockModel;

        // setup
        [TestInitialize()]
        public void Initialize()
        {
            _mockModel = new Mock<Model.Model>();
            _presentationModel = new FormPresentationModel(_mockModel.Object);
            _privatePresentationModel = new PrivateObject(_presentationModel);
        }

        // test constructor
        [TestMethod()]
        public void FormPresentationModelTest()
        {
            _mockModel = new Mock<Model.Model>();
            _presentationModel = new FormPresentationModel(_mockModel.Object);
            _privatePresentationModel = new PrivateObject(_presentationModel);

            Assert.IsNotNull(_presentationModel);
            Assert.IsFalse(_presentationModel.IsLineEnabled);
            Assert.IsFalse(_presentationModel.IsRectangleEnabled);
            Assert.IsFalse(_presentationModel.IsEllipseEnabled);
            Assert.IsTrue(_presentationModel.IsDefaultCursorEnabled);
            Assert.IsNotNull((Model.Model)_privatePresentationModel.GetField("_model"));
        }

        // test get shapes
        [TestMethod()]
        public void GetShapesTest()
        {
            Assert.IsInstanceOfType(_presentationModel.GetShapes(), typeof(BindingList<Model.Shape>));
        }

        // test create shape
        [TestMethod()]
        public void CreateShapeTest()
        {
            _presentationModel.CreateShape("");
            Assert.AreEqual(0, _presentationModel.GetShapes().Count);
            _presentationModel.CreateShape(Constant.Constant.LINE);
            Assert.AreEqual(1, _presentationModel.GetShapes().Count);
            Assert.AreEqual(Constant.Constant.LINE, _presentationModel.GetShapes()[0].ShapeName);
            // clean
            _presentationModel.DeleteShape(0);
        }

        // test delete shape
        [TestMethod()]
        public void DeleteShapeTest()
        {
            _presentationModel.CreateShape(Constant.Constant.LINE);
            _presentationModel.CreateShape(Constant.Constant.RECTANGLE);
            _presentationModel.CreateShape(Constant.Constant.ELLIPSE);
            Assert.AreEqual(3, _presentationModel.GetShapes().Count);
            _presentationModel.DeleteShape(0);
            Assert.AreEqual(2, _presentationModel.GetShapes().Count);
            _presentationModel.DeleteShape(0);
            Assert.AreEqual(1, _presentationModel.GetShapes().Count);
            _presentationModel.DeleteShape(0);
            Assert.AreEqual(0, _presentationModel.GetShapes().Count);
        }

        // test press pointer
        [TestMethod()]
        public void PressPointerTest()
        {
            _presentationModel.PressPointer(50, 50);
            _mockModel.Verify(model => model.PressPointer(50, 50), Times.Once);
        }

        // test move pointer
        [TestMethod()]
        public void MovePointerTest()
        {
            _presentationModel.MovePointer(23, 50);
            _mockModel.Verify(model => model.MovePointer(23, 50), Times.Once);
        }

        // test release pointer
        [TestMethod()]
        public void ReleasePointerWithShapeNameIsNotPointTest()
        {
            _presentationModel.EnableLine();
            _presentationModel.SetState(Constant.Constant.LINE);
            Assert.IsTrue(_presentationModel.IsLineEnabled);
            Assert.IsFalse(_presentationModel.IsRectangleEnabled);
            Assert.IsFalse(_presentationModel.IsEllipseEnabled);
            Assert.IsFalse(_presentationModel.IsDefaultCursorEnabled);

            _presentationModel.ReleasePointer(56, 50);
            _mockModel.Verify(model => model.ReleasePointer(56, 50), Times.Once);

            Assert.IsFalse(_presentationModel.IsLineEnabled);
            Assert.IsFalse(_presentationModel.IsRectangleEnabled);
            Assert.IsFalse(_presentationModel.IsEllipseEnabled);
            Assert.IsTrue(_presentationModel.IsDefaultCursorEnabled);
        }

        // test release pointer
        [TestMethod()]
        public void ReleasePointerWithShapeNameIsPointTest()
        {
            _presentationModel.EnableDefaultCursor();
            _presentationModel.SetState(Constant.Constant.POINT);
            Assert.IsFalse(_presentationModel.IsLineEnabled);
            Assert.IsFalse(_presentationModel.IsRectangleEnabled);
            Assert.IsFalse(_presentationModel.IsEllipseEnabled);
            Assert.IsTrue(_presentationModel.IsDefaultCursorEnabled);

            _presentationModel.ReleasePointer(30, 50);
            _mockModel.Verify(model => model.ReleasePointer(30, 50), Times.Once);

            Assert.IsFalse(_presentationModel.IsLineEnabled);
            Assert.IsFalse(_presentationModel.IsRectangleEnabled);
            Assert.IsFalse(_presentationModel.IsEllipseEnabled);
            Assert.IsTrue(_presentationModel.IsDefaultCursorEnabled);
        }

        // test handle model changed
        [TestMethod()]
        public void HandleModelChangedTest()
        {
            bool eventCalled = false;
            _presentationModel._modelChanged += () => eventCalled = true;
            _presentationModel.HandleModelChanged();
            Assert.IsTrue(eventCalled);

            eventCalled = false;
            _presentationModel.PropertyChanged += (sender, args) => eventCalled = true;
            _presentationModel.EnableDefaultCursor();
            _presentationModel.HandleModelChanged(); // actual will do
            Assert.IsTrue(eventCalled);
        }

        // test draw
        [TestMethod()]
        public void DrawTest()
        {
            _presentationModel.Draw(Graphics.FromImage(new Bitmap(10, 20)));
            _mockModel.Verify(model => model.Draw((FormGraphicsAdaptor)_privatePresentationModel.GetField("_graphicsAdaptor")), Times.Once);
        }

        // test draw on button
        [TestMethod()]
        public void DrawOnButtonTest()
        {
            Size buttonSize = new Size(100, 80);
            Size canvasSize = new Size(600, 300);
            _presentationModel.DrawOnButton(Graphics.FromImage(new Bitmap(10, 20)), buttonSize, canvasSize);
            _mockModel.Verify(model => model.Draw((FormGraphicsAdaptor)_privatePresentationModel.GetField("_graphicsAdaptor")), Times.Once);
        }

        // test reset boolean
        [TestMethod()]
        public void ResetBooleanTest()
        {
            _presentationModel.IsLineEnabled = true;
            _presentationModel.IsRectangleEnabled = true;
            _presentationModel.IsEllipseEnabled = true;
            _presentationModel.IsDefaultCursorEnabled = false;
            _presentationModel.ResetBoolean();

            Assert.IsFalse(_presentationModel.IsLineEnabled);
            Assert.IsFalse(_presentationModel.IsRectangleEnabled);
            Assert.IsFalse(_presentationModel.IsEllipseEnabled);
            Assert.IsTrue(_presentationModel.IsDefaultCursorEnabled);
        }

        // test set state
        [TestMethod()]
        public void SetStateForPointStateTest()
        {
            _presentationModel.SetState(Constant.Constant.POINT);
            _mockModel.Verify(model => model.ChangeState(Constant.Constant.POINT_STATE), Times.Once);
            Assert.IsTrue(_presentationModel.IsDefaultCursorEnabled);
        }

        // test set state
        [TestMethod()]
        public void SetStateForDrawingStateTest()
        {
            _presentationModel.SetState(Constant.Constant.LINE);
            _mockModel.Verify(model => model.ChangeState(Constant.Constant.DRAWING_STATE), Times.Once);
            Assert.IsFalse(_presentationModel.IsDefaultCursorEnabled);
        }

        // test set mode
        [TestMethod()]
        public void SetModeWithLineTest()
        {
            _presentationModel.SetMode(Constant.Constant.LINE);
            Assert.IsTrue(_presentationModel.IsLineEnabled);
            Assert.IsFalse(_presentationModel.IsRectangleEnabled);
            Assert.IsFalse(_presentationModel.IsEllipseEnabled);
            Assert.IsFalse(_presentationModel.IsDefaultCursorEnabled);
            Assert.AreEqual(Constant.Constant.LINE, _presentationModel.ShapeName);
        }

        // test set mode
        [TestMethod()]
        public void SetModeWithRectangleTest()
        {
            _presentationModel.SetMode(Constant.Constant.RECTANGLE);
            Assert.IsFalse(_presentationModel.IsLineEnabled);
            Assert.IsTrue(_presentationModel.IsRectangleEnabled);
            Assert.IsFalse(_presentationModel.IsEllipseEnabled);
            Assert.IsFalse(_presentationModel.IsDefaultCursorEnabled);
            Assert.AreEqual(Constant.Constant.RECTANGLE, _presentationModel.ShapeName);
        }

        // test set mode
        [TestMethod()]
        public void SetModeWithEllipseTest()
        {
            _presentationModel.SetMode(Constant.Constant.ELLIPSE);
            Assert.IsFalse(_presentationModel.IsLineEnabled);
            Assert.IsFalse(_presentationModel.IsRectangleEnabled);
            Assert.IsTrue(_presentationModel.IsEllipseEnabled);
            Assert.IsFalse(_presentationModel.IsDefaultCursorEnabled);
            Assert.AreEqual(Constant.Constant.ELLIPSE, _presentationModel.ShapeName);
        }

        // test set mode
        [TestMethod()]
        public void SetModeWithDefaultCursorTest()
        {
            _presentationModel.SetMode(Constant.Constant.POINT);
            Assert.IsFalse(_presentationModel.IsLineEnabled);
            Assert.IsFalse(_presentationModel.IsRectangleEnabled);
            Assert.IsFalse(_presentationModel.IsEllipseEnabled);
            Assert.IsTrue(_presentationModel.IsDefaultCursorEnabled);
            Assert.AreEqual(Constant.Constant.POINT, _presentationModel.ShapeName);
        }

        // test enable line
        [TestMethod()]
        public void EnableLineTest()
        {
            _presentationModel.EnableLine();
            Assert.IsTrue(_presentationModel.IsLineEnabled);
            Assert.IsFalse(_presentationModel.IsRectangleEnabled);
            Assert.IsFalse(_presentationModel.IsEllipseEnabled);
            Assert.IsFalse(_presentationModel.IsDefaultCursorEnabled);
        }

        // test enable rectangle
        [TestMethod()]
        public void EnableRectangleTest()
        {
            _presentationModel.EnableRectangle();
            Assert.IsFalse(_presentationModel.IsLineEnabled);
            Assert.IsTrue(_presentationModel.IsRectangleEnabled);
            Assert.IsFalse(_presentationModel.IsEllipseEnabled);
            Assert.IsFalse(_presentationModel.IsDefaultCursorEnabled);
        }

        // test enable ellipse
        [TestMethod()]
        public void EnableEllipseTest()
        {
            _presentationModel.EnableEllipse();
            Assert.IsFalse(_presentationModel.IsLineEnabled);
            Assert.IsFalse(_presentationModel.IsRectangleEnabled);
            Assert.IsTrue(_presentationModel.IsEllipseEnabled);
            Assert.IsFalse(_presentationModel.IsDefaultCursorEnabled);
        }

        // test enable default cursor
        [TestMethod()]
        public void EnableDefaultCursorTest()
        {
            _presentationModel.EnableDefaultCursor();
            Assert.IsFalse(_presentationModel.IsLineEnabled);
            Assert.IsFalse(_presentationModel.IsRectangleEnabled);
            Assert.IsFalse(_presentationModel.IsEllipseEnabled);
            Assert.IsTrue(_presentationModel.IsDefaultCursorEnabled);
        }

        // test handle key down
        [TestMethod()]
        public void HandleKeyDownTest()
        {
            _presentationModel.HandleKeyDown(Keys.Delete);
            _mockModel.Verify(model => model.HandleKeyDown(Keys.Delete), Times.Once);
        }

        // test notify property changed
        [TestMethod()]
        public void NotifyPropertyChangedTest()
        {
            bool eventRaised = false;
            _presentationModel.PropertyChanged += (sender, args) => eventRaised = true;
            _presentationModel.NotifyPropertyChanged(Constant.Constant.IS_CURSOR_ENABLED);
            Assert.IsTrue(eventRaised);
        }
    }
}