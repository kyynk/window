using Microsoft.VisualStudio.TestTools.UnitTesting;
using Homework.Model;
using Moq;

namespace Homework.Command.Tests
{
    [TestClass()]
    public class DrawCommandTests
    {
        private DrawCommand _drawCommand;
        private Mock<Model.Model> _mockModel;
        private Mock<Shape> _mockShape;
        private PrivateObject _privateDrawCommand;
        private int _shapeIndex;
        private double _panelWidth;
        private int _pageIndex;

        // setup
        [TestInitialize()]
        public void Initialize()
        {
            _shapeIndex = 0;
            _pageIndex = 0;
            _panelWidth = 500;
            _mockModel = new Mock<Model.Model>();
            _mockShape = new Mock<Shape>();
            _drawCommand = new DrawCommand(_mockModel.Object, _mockShape.Object, _shapeIndex, _pageIndex);
            _privateDrawCommand = new PrivateObject(_drawCommand);
        }

        // test constructor
        [TestMethod()]
        public void DrawCommandTest()
        {
            _drawCommand = new DrawCommand(_mockModel.Object, _mockShape.Object, _shapeIndex, _pageIndex);
            _privateDrawCommand = new PrivateObject(_drawCommand);

            Assert.AreEqual(0, (int)_privateDrawCommand.GetField("_shapeIndex"));
            Assert.AreEqual(0, (int)_privateDrawCommand.GetField("_pageIndex"));
            Assert.AreEqual(-1, (double)_privateDrawCommand.GetField("_panelWidth"));
            Assert.IsNotNull((Shape)_privateDrawCommand.GetField("_shape"));
            Assert.IsNotNull((Model.Model)_privateDrawCommand.GetField("_model"));
        }

        // test execute
        [TestMethod()]
        public void ExecuteTest()
        {
            _drawCommand.SetPanelWidth(_panelWidth);
            _drawCommand.Execute(100);
            _mockModel.Verify(model => model.SelectPage((int)_privateDrawCommand.GetField("_pageIndex")), Times.Once);
            _mockModel.Verify(model => model.InsertShape((Shape)_privateDrawCommand.GetField("_shape"), (int)_privateDrawCommand.GetField("_shapeIndex"), (int)_privateDrawCommand.GetField("_pageIndex")), Times.Once);
            _mockShape.Verify(shape => shape.ResizeForPanel(100 / _panelWidth), Times.Once);
            Assert.AreEqual(100, (double)_privateDrawCommand.GetField("_panelWidth"));
        }

        // test undo
        [TestMethod()]
        public void UndoTest()
        {
            _drawCommand.SetPanelWidth(_panelWidth);
            _drawCommand.Undo(50);
            _mockModel.Verify(model => model.SelectPage((int)_privateDrawCommand.GetField("_pageIndex")), Times.Once);
            _mockModel.Verify(model => model.DeleteShape((int)_privateDrawCommand.GetField("_shapeIndex"), (int)_privateDrawCommand.GetField("_pageIndex")), Times.Once);
            Assert.AreEqual(50, (double)_privateDrawCommand.GetField("_panelWidth"));
        }

        // test set panel width
        [TestMethod()]
        public void SetPanelWidthTest()
        {
            _drawCommand.SetPanelWidth(200);
            Assert.AreEqual(200, (double)_privateDrawCommand.GetField("_panelWidth"));
        }

        // test adjust with panel width
        [TestMethod()]
        public void AdjustWithPanelWidthTest()
        {
            _drawCommand.SetPanelWidth(200);

            _drawCommand.AdjustWithPanelWidth(100);
            _mockShape.Verify(shape => shape.ResizeForPanel(0.5), Times.Once);
            Assert.AreEqual(100, (double)_privateDrawCommand.GetField("_panelWidth"));
        }
    }
}