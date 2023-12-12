using Homework.Command;
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
        private PrivateObject _privateMoveCommand;
        private double _offsetX;
        private double _offsetY;
        private double _panelWidth;

        // setup
        [TestInitialize()]
        public void Initialize()
        {
            _offsetX = 5;
            _offsetY = 6;
            _panelWidth = 500;
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
            Assert.AreEqual(-1, (double)_privateMoveCommand.GetField("_panelWidth"));
            Assert.IsNotNull((Shape)_privateMoveCommand.GetField("_shape"));
            Assert.IsFalse((bool)_privateMoveCommand.GetField("_isNotFirstTime"));
        }

        // test execute
        [TestMethod()]
        public void ExecuteTest()
        {
            _moveCommand.SetPanelWidth(_panelWidth);
            // first time
            _moveCommand.Execute(1000);
            _mockShape.Verify(shape => shape.Move((double)_privateMoveCommand.GetField("_offsetX"), (double)_privateMoveCommand.GetField("_offsetY")), Times.Never);
            Assert.IsTrue((bool)_privateMoveCommand.GetField("_isNotFirstTime"));
            Assert.AreEqual(5 * 1000 / _panelWidth, (double)_privateMoveCommand.GetField("_offsetX"));
            Assert.AreEqual(6 * 1000 / _panelWidth, (double)_privateMoveCommand.GetField("_offsetY"));
            // not first time
            _moveCommand.Execute(1000);
            _mockShape.Verify(shape => shape.Move((double)_privateMoveCommand.GetField("_offsetX"), (double)_privateMoveCommand.GetField("_offsetY")), Times.Once);
            Assert.IsTrue((bool)_privateMoveCommand.GetField("_isNotFirstTime"));
            Assert.AreEqual(5 * 1000 / _panelWidth, (double)_privateMoveCommand.GetField("_offsetX"));
            Assert.AreEqual(6 * 1000 / _panelWidth, (double)_privateMoveCommand.GetField("_offsetY"));
        }

        // test undo
        [TestMethod()]
        public void UndoTest()
        {
            _moveCommand.SetPanelWidth(_panelWidth);
            _moveCommand.Undo(100);
            _mockShape.Verify(shape => shape.Move(-((double)_privateMoveCommand.GetField("_offsetX")), -((double)_privateMoveCommand.GetField("_offsetY"))), Times.Once);
            Assert.AreEqual(5 * 100 / _panelWidth, (double)_privateMoveCommand.GetField("_offsetX"));
            Assert.AreEqual(6 * 100 / _panelWidth, (double)_privateMoveCommand.GetField("_offsetY"));
        }

        // test store panel width
        [TestMethod()]
        public void StorePanelWidthTest()
        {
            _moveCommand.SetPanelWidth(200);
            Assert.AreEqual(200, (double)_privateMoveCommand.GetField("_panelWidth"));
        }

        // test adjust panel width
        [TestMethod()]
        public void AdjustPanelWidthTest()
        {
            _privateMoveCommand.SetField("_offsetX", 5);
            _privateMoveCommand.SetField("_offsetY", 6);
            _moveCommand.SetPanelWidth(200);

            _moveCommand.AdjustPanelWidth(100);
            Assert.AreEqual(2.5, (double)_privateMoveCommand.GetField("_offsetX"));
            Assert.AreEqual(3, (double)_privateMoveCommand.GetField("_offsetY"));
            Assert.AreEqual(100, (double)_privateMoveCommand.GetField("_panelWidth"));
        }
    }
}