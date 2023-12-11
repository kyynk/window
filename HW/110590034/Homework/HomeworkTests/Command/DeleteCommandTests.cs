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
        private int _index;
        private PrivateObject _privateDeleteCommand;

        // setup
        [TestInitialize()]
        public void Initialize()
        {
            _index = 0;
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
            Assert.IsNotNull((Shape)_privateDeleteCommand.GetField("_shape"));
            Assert.IsNotNull((Model.Model)_privateDeleteCommand.GetField("_model"));
        }

        // test execute
        [TestMethod()]
        public void ExecuteTest()
        {
            _deleteCommand.Execute();
            _mockModel.Verify(model => model.DeleteShape((int)_privateDeleteCommand.GetField("_shapeIndex")), Times.Once);
        }

        // test undo
        [TestMethod()]
        public void UndoTest()
        {
            _deleteCommand.Undo();
            _mockModel.Verify(model => model.InsertShape((Shape)_privateDeleteCommand.GetField("_shape"), (int)_privateDeleteCommand.GetField("_shapeIndex")), Times.Once);
        }
    }
}