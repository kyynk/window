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
        private int _index;
        private PrivateObject _privateDrawCommand;

        // setup
        [TestInitialize()]
        public void Initialize()
        {
            _index = 0;
            _mockModel = new Mock<Model.Model>();
            _mockShape = new Mock<Shape>();
            _drawCommand = new DrawCommand(_mockModel.Object, _mockShape.Object, _index);
            _privateDrawCommand = new PrivateObject(_drawCommand);
        }

        // test constructor
        [TestMethod()]
        public void DrawCommandTest()
        {
            _drawCommand = new DrawCommand(_mockModel.Object, _mockShape.Object, _index);
            _privateDrawCommand = new PrivateObject(_drawCommand);

            Assert.AreEqual(0, (int)_privateDrawCommand.GetField("_shapeIndex"));
            Assert.IsNotNull((Shape)_privateDrawCommand.GetField("_shape"));
            Assert.IsNotNull((Model.Model)_privateDrawCommand.GetField("_model"));
        }

        // test execute
        [TestMethod()]
        public void ExecuteTest()
        {
            _drawCommand.Execute();
            _mockModel.Verify(model => model.InsertShape((Shape)_privateDrawCommand.GetField("_shape"), (int)_privateDrawCommand.GetField("_shapeIndex")), Times.Once);
        }

        // test undo
        [TestMethod()]
        public void UndoTest()
        {
            _drawCommand.Undo();
            _mockModel.Verify(model => model.DeleteShape((int)_privateDrawCommand.GetField("_shapeIndex")), Times.Once);
        }
    }
}