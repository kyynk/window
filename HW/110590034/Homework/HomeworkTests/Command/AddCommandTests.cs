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
        private int _index;
        private PrivateObject _privateAddCommand;

        // setup
        [TestInitialize()]
        public void Initialize()
        {
            _index = 0;
            _mockModel = new Mock<Model.Model>();
            _mockShape = new Mock<Shape>();
            _addCommand = new AddCommand(_mockModel.Object, _mockShape.Object, _index);
            _privateAddCommand = new PrivateObject(_addCommand);
        }

        // test constructor
        [TestMethod()]
        public void DrawCommandTest()
        {
            _addCommand = new AddCommand(_mockModel.Object, _mockShape.Object, _index);
            _privateAddCommand = new PrivateObject(_addCommand);

            Assert.AreEqual(0, (int)_privateAddCommand.GetField("_shapeIndex"));
            Assert.IsNotNull((Shape)_privateAddCommand.GetField("_shape"));
            Assert.IsNotNull((Model.Model)_privateAddCommand.GetField("_model"));
        }

        // test execute
        [TestMethod()]
        public void ExecuteTest()
        {
            _addCommand.Execute();
            _mockModel.Verify(model => model.InsertShape((Shape)_privateAddCommand.GetField("_shape"), (int)_privateAddCommand.GetField("_shapeIndex")), Times.Once);
        }

        // test undo
        [TestMethod()]
        public void UndoTest()
        {
            _addCommand.Undo();
            _mockModel.Verify(model => model.DeleteShape((int)_privateAddCommand.GetField("_shapeIndex")), Times.Once);
        }
    }
}