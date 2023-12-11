using Microsoft.VisualStudio.TestTools.UnitTesting;
using Homework.Model;
using Moq;


namespace Homework.Command.Tests
{
    [TestClass()]
    public class MoveCommandTests
    {
        private MoveCommand _moveCommand;
        private Mock<Shape> _mockShape;
        private double _offsetX;
        private double _offsetY;
        private PrivateObject _privateMoveCommand;

        // setup
        [TestInitialize()]
        public void Initialize()
        {
            _offsetX = 5;
            _offsetY = 6;
            _mockShape = new Mock<Shape>();
            _moveCommand = new MoveCommand(_mockShape.Object, _offsetX, _offsetY);
            _privateMoveCommand = new PrivateObject(_moveCommand);
        }

        // test constructor
        [TestMethod()]
        public void DrawCommandTest()
        {
            _moveCommand = new MoveCommand(_mockShape.Object, _offsetX, _offsetY);
            _privateMoveCommand = new PrivateObject(_moveCommand);

            Assert.AreEqual(5, (double)_privateMoveCommand.GetField("_offsetX"));
            Assert.AreEqual(6, (double)_privateMoveCommand.GetField("_offsetY"));
            Assert.IsNotNull((Shape)_privateMoveCommand.GetField("_shape"));
            Assert.IsFalse((bool)_privateMoveCommand.GetField("_isNotFirstTime"));
        }

        // test execute
        [TestMethod()]
        public void ExecuteTest()
        {
            // first time
            _moveCommand.Execute();
            _mockShape.Verify(shape => shape.Move((double)_privateMoveCommand.GetField("_offsetX"), (double)_privateMoveCommand.GetField("_offsetY")), Times.Never);
            Assert.IsTrue((bool)_privateMoveCommand.GetField("_isNotFirstTime"));
            // not first time
            _moveCommand.Execute();
            _mockShape.Verify(shape => shape.Move((double)_privateMoveCommand.GetField("_offsetX"), (double)_privateMoveCommand.GetField("_offsetY")), Times.Once);
            Assert.IsTrue((bool)_privateMoveCommand.GetField("_isNotFirstTime"));
        }

        // test undo
        [TestMethod()]
        public void UndoTest()
        {
            _moveCommand.Undo();
            _mockShape.Verify(shape => shape.Move(-((double)_privateMoveCommand.GetField("_offsetX")), -((double)_privateMoveCommand.GetField("_offsetY"))), Times.Once);
        }
    }
}