using Microsoft.VisualStudio.TestTools.UnitTesting;
using Homework.Model;
using Moq;

namespace Homework.Command.Tests
{
    [TestClass()]
    public class AddCommandTests
    {
        private AddCommand _addCommand;
        private Mock<Model.Model> _mockModel;
        private Mock<Shape> _mockShape;
        private PrivateObject _privateAddCommand;
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
            _addCommand = new AddCommand(_mockModel.Object, _mockShape.Object, _shapeIndex, _pageIndex);
            _privateAddCommand = new PrivateObject(_addCommand);
        }

        // test constructor
        [TestMethod()]
        public void AddCommandTest()
        {
            _addCommand = new AddCommand(_mockModel.Object, _mockShape.Object, _shapeIndex, _pageIndex);
            _privateAddCommand = new PrivateObject(_addCommand);

            Assert.AreEqual(0, (int)_privateAddCommand.GetField("_shapeIndex"));
            Assert.AreEqual(0, (int)_privateAddCommand.GetField("_pageIndex"));
            Assert.AreEqual(-1, (double)_privateAddCommand.GetField("_panelWidth"));
            Assert.IsNotNull((Shape)_privateAddCommand.GetField("_shape"));
            Assert.IsNotNull((Model.Model)_privateAddCommand.GetField("_model"));
        }

        // test execute
        [TestMethod()]
        public void ExecuteTest()
        {
            _addCommand.SetPanelWidth(_panelWidth);
            _addCommand.Execute(_panelWidth);
            _mockModel.Verify(model => model.SelectPage((int)_privateAddCommand.GetField("_pageIndex")), Times.Once);
            _mockModel.Verify(model => model.InsertShape((Shape)_privateAddCommand.GetField("_shape"), (int)_privateAddCommand.GetField("_shapeIndex"), (int)_privateAddCommand.GetField("_pageIndex")), Times.Once);
            _mockShape.Verify(shape => shape.ResizeForPanel(1), Times.Once);
            Assert.AreEqual(_panelWidth, (double)_privateAddCommand.GetField("_panelWidth"));
        }

        // test undo
        [TestMethod()]
        public void UndoTest()
        {
            _addCommand.SetPanelWidth(_panelWidth);
            _addCommand.Undo(100);
            _mockModel.Verify(model => model.SelectPage((int)_privateAddCommand.GetField("_pageIndex")), Times.Once);
            _mockModel.Verify(model => model.DeleteShape((int)_privateAddCommand.GetField("_shapeIndex"), (int)_privateAddCommand.GetField("_pageIndex")), Times.Once);
            Assert.AreEqual(100, (double)_privateAddCommand.GetField("_panelWidth"));
        }

        // test set panel width
        [TestMethod()]
        public void SetPanelWidthTest()
        {
            _addCommand.SetPanelWidth(200);
            Assert.AreEqual(200, (double)_privateAddCommand.GetField("_panelWidth"));
        }

        // test adjust with panel width
        [TestMethod()]
        public void AdjustWithPanelWidthTest()
        {
            _addCommand.SetPanelWidth(200);

            _addCommand.AdjustWithPanelWidth(100);
            _mockShape.Verify(shape => shape.ResizeForPanel(0.5), Times.Once);
            Assert.AreEqual(100, (double)_privateAddCommand.GetField("_panelWidth"));
        }
    }
}