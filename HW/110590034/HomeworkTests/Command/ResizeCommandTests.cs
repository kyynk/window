using Microsoft.VisualStudio.TestTools.UnitTesting;
using Homework.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using Homework.Model;

namespace Homework.Command.Tests
{
    [TestClass()]
    public class ResizeCommandTests
    {
        private ResizeCommand _resizeCommand;
        private Mock<Model.Model> _mockModel;
        private Mock<Shape> _mockShape;
        private PrivateObject _privateResizeCommand;
        private Point _previousPointLeft;
        private Point _previousPointRight;
        private double _panelWidth;
        private int _pageIndex;

        // setup
        [TestInitialize()]
        public void Initialize()
        {
            _previousPointLeft = new Point(5, 6);
            _previousPointRight = new Point(10, 12);
            _pageIndex = 0;
            _panelWidth = 500;
            _mockModel = new Mock<Model.Model>();
            _mockShape = new Mock<Shape>();
            _resizeCommand = new ResizeCommand(_mockModel.Object, _mockShape.Object, _previousPointLeft, _previousPointRight, _pageIndex);
            _privateResizeCommand = new PrivateObject(_resizeCommand);
        }

        // test constructor
        [TestMethod()]
        public void ResizeCommandTest()
        {
            _resizeCommand = new ResizeCommand(_mockModel.Object, _mockShape.Object, _previousPointLeft, _previousPointRight, _pageIndex);
            _privateResizeCommand = new PrivateObject(_resizeCommand);

            Assert.AreEqual(5, ((Point)_privateResizeCommand.GetField("_previousPointLeft")).X);
            Assert.AreEqual(6, ((Point)_privateResizeCommand.GetField("_previousPointLeft")).Y);
            Assert.AreEqual(10, ((Point)_privateResizeCommand.GetField("_previousPointRight")).X);
            Assert.AreEqual(12, ((Point)_privateResizeCommand.GetField("_previousPointRight")).Y);
            Assert.AreEqual(0, (int)_privateResizeCommand.GetField("_pageIndex"));
            Assert.AreEqual(-1, (double)_privateResizeCommand.GetField("_panelWidth"));
            Assert.IsNotNull((Model.Model)_privateResizeCommand.GetField("_model"));
            Assert.IsNotNull((Shape)_privateResizeCommand.GetField("_shape"));
            Assert.IsFalse((bool)_privateResizeCommand.GetField("_isNotFirstTime"));
        }

        // test execute
        [TestMethod()]
        public void ExecuteTest()
        {
            _resizeCommand.SetPanelWidth(_panelWidth);
            // first time
            _resizeCommand.Execute(1000);

            _mockModel.Verify(model => model.SelectPage((int)_privateResizeCommand.GetField("_pageIndex")), Times.Once);
            _mockShape.Verify(shape => shape.SetTwoPoint(It.IsAny<Point>(), It.IsAny<Point>()), Times.Never);
            Assert.IsTrue((bool)_privateResizeCommand.GetField("_isNotFirstTime"));
            Assert.AreEqual(5 * 1000 / _panelWidth, ((Point)_privateResizeCommand.GetField("_previousPointLeft")).X);
            Assert.AreEqual(6 * 1000 / _panelWidth, ((Point)_privateResizeCommand.GetField("_previousPointLeft")).Y);
            Assert.AreEqual(10 * 1000 / _panelWidth, ((Point)_privateResizeCommand.GetField("_previousPointRight")).X);
            Assert.AreEqual(12 * 1000 / _panelWidth, ((Point)_privateResizeCommand.GetField("_previousPointRight")).Y);
            // not first time
            _resizeCommand.Execute(1000);
            _mockModel.Verify(model => model.SelectPage((int)_privateResizeCommand.GetField("_pageIndex")), Times.Exactly(2));
            _mockShape.Verify(shape => shape.SetTwoPoint(It.IsAny<Point>(), It.IsAny<Point>()), Times.Once);
            Assert.IsTrue((bool)_privateResizeCommand.GetField("_isNotFirstTime"));
        }

        // test undo
        [TestMethod()]
        public void UndoTest()
        {
            _resizeCommand.SetPanelWidth(_panelWidth);
            _resizeCommand.Undo(100);
            _mockModel.Verify(model => model.SelectPage((int)_privateResizeCommand.GetField("_pageIndex")), Times.Once);
            _mockShape.Verify(shape => shape.SetTwoPoint(It.IsAny<Point>(), It.IsAny<Point>()), Times.Once);
        }

        // test resize
        [TestMethod()]
        public void ResizeTest()
        {
            _resizeCommand.Resize();
            _mockShape.Verify(shape => shape.SetTwoPoint(It.IsAny<Point>(), It.IsAny<Point>()), Times.Once);
        }

        // test set panel width
        [TestMethod()]
        public void SetPanelWidthTest()
        {
            _resizeCommand.SetPanelWidth(200);
            Assert.AreEqual(200, (double)_privateResizeCommand.GetField("_panelWidth"));
        }

        // test adjust with panel width
        [TestMethod()]
        public void AdjustWithPanelWidthTest()
        {
            _privateResizeCommand.SetField("_previousPointLeft", new Point(5, 6));
            _privateResizeCommand.SetField("_previousPointRight", new Point(10, 12));
            _resizeCommand.SetPanelWidth(200);

            _resizeCommand.AdjustWithPanelWidth(100);
            Assert.AreEqual(2.5, ((Point)_privateResizeCommand.GetField("_previousPointLeft")).X);
            Assert.AreEqual(3, ((Point)_privateResizeCommand.GetField("_previousPointLeft")).Y);
            Assert.AreEqual(5, ((Point)_privateResizeCommand.GetField("_previousPointRight")).X);
            Assert.AreEqual(6, ((Point)_privateResizeCommand.GetField("_previousPointRight")).Y);
            Assert.AreEqual(100, (double)_privateResizeCommand.GetField("_panelWidth"));
        }
    }
}