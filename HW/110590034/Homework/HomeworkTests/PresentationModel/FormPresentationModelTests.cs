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
            Assert.AreEqual(Cursors.Arrow, _presentationModel.UsingCursor);
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

        // test is resize state
        [TestMethod()]
        public void IsResizeStateTest()
        {
            _mockModel.Setup(model => model.CheckIsResizeState(It.IsAny<double>(), It.IsAny<double>())).Returns(true);
            Assert.IsTrue(_presentationModel.IsResizeState(1, 1));
            _mockModel.Verify(model => model.CheckIsResizeState(1, 1), Times.Once);

            _mockModel.Setup(model => model.CheckIsResizeState(It.IsAny<double>(), It.IsAny<double>())).Returns(false);
            Assert.IsFalse(_presentationModel.IsResizeState(2, 2));
            _mockModel.Verify(model => model.CheckIsResizeState(2, 2), Times.Once);
        }

        // test is location right bottom
        [TestMethod()]
        public void IsLocationRightBottomTest()
        {
            _mockModel.Setup(model => model.CheckLocationIsRightBottom(It.IsAny<double>(), It.IsAny<double>())).Returns(true);
            Assert.IsTrue(_presentationModel.IsLocationRightBottom(1, 1));
            _mockModel.Verify(model => model.CheckLocationIsRightBottom(1, 1), Times.Once);

            _mockModel.Setup(model => model.CheckLocationIsRightBottom(It.IsAny<double>(), It.IsAny<double>())).Returns(false);
            Assert.IsFalse(_presentationModel.IsLocationRightBottom(2, 2));
            _mockModel.Verify(model => model.CheckLocationIsRightBottom(2, 2), Times.Once);
        }
        // test press pointer
        [TestMethod()]
        public void PressPointerWithResizeStateTest()
        {
            // resize state
            _presentationModel.IsPressed = false;
            _mockModel.Setup(model => model.CheckIsResizeState(It.IsAny<double>(), It.IsAny<double>())).Returns(true);
            _presentationModel.PressPointer(50, 50);
            _mockModel.Verify(model => model.ChangeState(Constant.Constant.RESIZE_STATE), Times.Once);
            _mockModel.Verify(model => model.PressPointer(50, 50), Times.Once);
            Assert.IsTrue(_presentationModel.IsPressed);
        }

        // test press pointer
        [TestMethod()]
        public void PressPointerWithPointStateTest()
        {
            // point state
            _presentationModel.IsPressed = false;
            _mockModel.Setup(model => model.CheckIsResizeState(It.IsAny<double>(), It.IsAny<double>())).Returns(false);
            _presentationModel.PressPointer(51, 51);
            _mockModel.Verify(model => model.ChangeState(Constant.Constant.POINT_STATE), Times.Once);
            _mockModel.Verify(model => model.PressPointer(51, 51), Times.Once);
            Assert.IsTrue(_presentationModel.IsPressed);
        }

        // test press pointer
        [TestMethod()]
        public void PressPointerWithDrawingStateWithReturnIsTrueTest()
        {
            // drawing state
            _presentationModel.IsPressed = false;
            _presentationModel.IsDefaultCursorEnabled = false;
            _mockModel.Setup(model => model.CheckIsResizeState(It.IsAny<double>(), It.IsAny<double>())).Returns(true);
            _presentationModel.PressPointer(51, 51);
            _mockModel.Verify(model => model.ChangeState(Constant.Constant.POINT_STATE), Times.Never);
            _mockModel.Verify(model => model.PressPointer(51, 51), Times.Once);
            Assert.IsTrue(_presentationModel.IsPressed);
            // clean
            _presentationModel.IsDefaultCursorEnabled = true;
        }

        // test move pointer
        [TestMethod()]
        public void MovePointerWithPointStateTest()
        {
            // right bottom
            bool isCalledCursorChanged = false;
            _presentationModel._cursorChanged += (cursor) => { isCalledCursorChanged = true; };
            _presentationModel.IsPressed = false;
            _mockModel.Setup(model => model.CheckLocationIsRightBottom(It.IsAny<double>(), It.IsAny<double>())).Returns(true);
            _presentationModel.MovePointer(23, 50);
            Assert.IsTrue(isCalledCursorChanged);
            Assert.AreEqual(Cursors.SizeNWSE, _presentationModel.UsingCursor);
            _mockModel.Verify(model => model.MovePointer(23, 50), Times.Once);
            // not right bottom
            isCalledCursorChanged = false;
            _presentationModel.IsPressed = false;
            _mockModel.Setup(model => model.CheckLocationIsRightBottom(It.IsAny<double>(), It.IsAny<double>())).Returns(false);
            _presentationModel.MovePointer(25, 51);
            Assert.IsTrue(isCalledCursorChanged);
            Assert.AreEqual(Cursors.Arrow, _presentationModel.UsingCursor);
            _mockModel.Verify(model => model.MovePointer(25, 51), Times.Once);
        }

        // test move pointer
        [TestMethod()]
        public void MovePointerWithoutPointStateTest()
        {
            bool isCalledCursorChanged = false;
            _presentationModel._cursorChanged += (cursor) => { isCalledCursorChanged = true; };
            _presentationModel.IsPressed = true;
            _mockModel.Setup(model => model.CheckLocationIsRightBottom(It.IsAny<double>(), It.IsAny<double>())).Returns(true);
            _presentationModel.MovePointer(23, 50);
            Assert.IsFalse(isCalledCursorChanged);
            _mockModel.Verify(model => model.MovePointer(23, 50), Times.Once);
        }

        // test release pointer
        [TestMethod()]
        public void ReleasePointerWithShapeNameIsNotPointTest()
        {
            bool isCalledCursorChanged = false;
            _presentationModel._cursorChanged += (cursor) => { isCalledCursorChanged = true; };
            _presentationModel.EnableLine();
            Assert.IsTrue(isCalledCursorChanged);
            _presentationModel.SetState(Constant.Constant.LINE);
            Assert.AreEqual(Cursors.Cross, _presentationModel.UsingCursor);
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
            Assert.IsFalse(_presentationModel.IsPressed);
        }

        // test release pointer
        [TestMethod()]
        public void ReleasePointerWithShapeNameIsPointTest()
        {
            _presentationModel.SetMode(Constant.Constant.POINT);
            _presentationModel.IsPressed = true;
            _presentationModel.ReleasePointer(56, 50);
            Assert.IsFalse(_presentationModel.IsLineEnabled);
            Assert.IsFalse(_presentationModel.IsRectangleEnabled);
            Assert.IsFalse(_presentationModel.IsEllipseEnabled);
            Assert.IsTrue(_presentationModel.IsDefaultCursorEnabled);
            Assert.IsFalse(_presentationModel.IsPressed);
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
            bool isCalledCursorChanged = false;
            _presentationModel._cursorChanged += (cursor) => { isCalledCursorChanged = true; };
            _presentationModel.EnableDefaultCursor();
            Assert.IsTrue(isCalledCursorChanged);
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
            bool isCalledCursorChanged = false;
            _presentationModel._cursorChanged += (cursor) => { isCalledCursorChanged = true; };
            _presentationModel.EnableLine();
            Assert.IsTrue(isCalledCursorChanged);
            Assert.AreEqual(Cursors.Cross, _presentationModel.UsingCursor);
            Assert.IsTrue(_presentationModel.IsLineEnabled);
            Assert.IsFalse(_presentationModel.IsRectangleEnabled);
            Assert.IsFalse(_presentationModel.IsEllipseEnabled);
            Assert.IsFalse(_presentationModel.IsDefaultCursorEnabled);
        }

        // test enable rectangle
        [TestMethod()]
        public void EnableRectangleTest()
        {
            bool isCalledCursorChanged = false;
            _presentationModel._cursorChanged += (cursor) => { isCalledCursorChanged = true; };
            _presentationModel.EnableRectangle();
            Assert.IsTrue(isCalledCursorChanged);
            Assert.AreEqual(Cursors.Cross, _presentationModel.UsingCursor);
            Assert.IsFalse(_presentationModel.IsLineEnabled);
            Assert.IsTrue(_presentationModel.IsRectangleEnabled);
            Assert.IsFalse(_presentationModel.IsEllipseEnabled);
            Assert.IsFalse(_presentationModel.IsDefaultCursorEnabled);
        }

        // test enable ellipse
        [TestMethod()]
        public void EnableEllipseTest()
        {
            bool isCalledCursorChanged = false;
            _presentationModel._cursorChanged += (cursor) => { isCalledCursorChanged = true; };
            _presentationModel.EnableEllipse();
            Assert.IsTrue(isCalledCursorChanged);
            Assert.AreEqual(Cursors.Cross, _presentationModel.UsingCursor);
            Assert.IsFalse(_presentationModel.IsLineEnabled);
            Assert.IsFalse(_presentationModel.IsRectangleEnabled);
            Assert.IsTrue(_presentationModel.IsEllipseEnabled);
            Assert.IsFalse(_presentationModel.IsDefaultCursorEnabled);
        }

        // test enable default cursor
        [TestMethod()]
        public void EnableDefaultCursorTest()
        {
            bool isCalledCursorChanged = false;
            _presentationModel._cursorChanged += (cursor) => { isCalledCursorChanged = true; };
            _presentationModel.EnableDefaultCursor();
            Assert.IsTrue(isCalledCursorChanged);
            Assert.AreEqual(Cursors.Arrow, _presentationModel.UsingCursor);
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