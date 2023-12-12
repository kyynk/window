using Microsoft.VisualStudio.TestTools.UnitTesting;
using Homework.Model;
using Moq;

namespace Homework.Command.Tests
{
    [TestClass()]
    public class DeleteCommandTests
    {
        private DeleteCommand _deleteCommand;
        private Mock<Model.Model> _mockModel;
        private Mock<Shape> _mockShape;
        private PrivateObject _privateDeleteCommand;
        private int _index;
        private double _panelWidth;

        // setup
        [TestInitialize()]
        public void Initialize()
        {
            _index = 0;
            _panelWidth = 500;
            _mockModel = new Mock<Model.Model>();
            _mockShape = new Mock<Shape>();
            _deleteCommand = new DeleteCommand(_mockModel.Object, _mockShape.Object, _index);
            _privateDeleteCommand = new PrivateObject(_deleteCommand);
        }

        // test constructor
        [TestMethod()]
        public void DrawCommandTest()
        {
            _deleteCommand = new DeleteCommand(_mockModel.Object, _mockShape.Object, _index);
            _privateDeleteCommand = new PrivateObject(_deleteCommand);

            Assert.AreEqual(0, (int)_privateDeleteCommand.GetField("_shapeIndex"));
            Assert.AreEqual(-1, (double)_privateDeleteCommand.GetField("_panelWidth"));
            Assert.IsNotNull((Shape)_privateDeleteCommand.GetField("_shape"));
            Assert.IsNotNull((Model.Model)_privateDeleteCommand.GetField("_model"));
        }

        // test execute
        [TestMethod()]
        public void ExecuteTest()
        {
            _deleteCommand.Execute(_panelWidth);
            _mockModel.Verify(model => model.DeleteShape((int)_privateDeleteCommand.GetField("_shapeIndex")), Times.Once);
            Assert.AreEqual(_panelWidth, (double)_privateDeleteCommand.GetField("_panelWidth"));
        }

        // test undo
        [TestMethod()]
        public void UndoTest()
        {
            _deleteCommand.SetPanelWidth(_panelWidth);
            _deleteCommand.Undo(50);
            _mockModel.Verify(model => model.InsertShape((Shape)_privateDeleteCommand.GetField("_shape"), (int)_privateDeleteCommand.GetField("_shapeIndex")), Times.Once);
            _mockShape.Verify(shape => shape.ResizeForPanel(50 / _panelWidth), Times.Once);
            Assert.AreEqual(50, (double)_privateDeleteCommand.GetField("_panelWidth"));
        }

        // test store panel width
        [TestMethod()]
        public void StorePanelWidthTest()
        {
            _deleteCommand.SetPanelWidth(200);
            Assert.AreEqual(200, (double)_privateDeleteCommand.GetField("_panelWidth"));
        }

        // test adjust panel width
        [TestMethod()]
        public void AdjustPanelWidthTest()
        {
            _deleteCommand.SetPanelWidth(200);

            _deleteCommand.AdjustPanelWidth(100);
            _mockShape.Verify(shape => shape.ResizeForPanel(0.5), Times.Once);
            Assert.AreEqual(100, (double)_privateDeleteCommand.GetField("_panelWidth"));
        }
    }
}