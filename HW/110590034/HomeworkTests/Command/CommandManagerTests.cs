using Homework.Command;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Moq;
using System;

namespace Homework.Command.Tests
{
    [TestClass()]
    public class CommandManagerTests
    {
        private CommandManager _commandManager;
        private PrivateObject _privateCommandManager;
        private Mock<ICommand> _mockCommand;
        private double _width;

        // setup
        [TestInitialize()]
        public void Initialize()
        {
            _width = 500;
            _mockCommand = new Mock<ICommand>();
            _commandManager = new CommandManager();
            _privateCommandManager = new PrivateObject(_commandManager);
        }

        // test constructor
        [TestMethod()]
        public void CommandManagerTest()
        {
            _commandManager = new CommandManager();
            _privateCommandManager = new PrivateObject(_commandManager);

            Assert.IsNotNull((Stack<ICommand>)_privateCommandManager.GetField("_undo"));
            Assert.IsNotNull((Stack<ICommand>)_privateCommandManager.GetField("_redo"));
            Assert.IsInstanceOfType((Stack<ICommand>)_privateCommandManager.GetField("_undo"), typeof(Stack<ICommand>));
            Assert.IsInstanceOfType((Stack<ICommand>)_privateCommandManager.GetField("_redo"), typeof(Stack<ICommand>));
        }

        // test execute
        [TestMethod()]
        public void ExecuteTest()
        {
            _commandManager.Execute(_mockCommand.Object, _width);

            Assert.AreEqual(1, ((Stack<ICommand>)_privateCommandManager.GetField("_undo")).Count);
            Assert.AreEqual(0, ((Stack<ICommand>)_privateCommandManager.GetField("_redo")).Count);
        }

        // test undo
        [TestMethod()]
        public void UndoTest()
        {
            _mockCommand = new Mock<ICommand>();
            _commandManager.Execute(_mockCommand.Object, _width);
            // _undo.Count > 0
            Assert.IsTrue(_commandManager.IsUndoEnabled);
            _commandManager.Undo(_width);
            Assert.AreEqual(0, ((Stack<ICommand>)_privateCommandManager.GetField("_undo")).Count);
            Assert.AreEqual(1, ((Stack<ICommand>)_privateCommandManager.GetField("_redo")).Count);
            _mockCommand.Verify(command => command.Undo(_width), Times.Once);
            // _undo.Count <= 0
            Assert.IsFalse(_commandManager.IsUndoEnabled);
            Assert.ThrowsException<Exception>(() => _commandManager.Undo(_width));
        }

        // test redo
        [TestMethod()]
        public void RedoTest()
        {
            _mockCommand = new Mock<ICommand>();
            _commandManager.Execute(_mockCommand.Object, _width);
            _commandManager.Undo(_width);
            // _redo.Count > 0
            // 2 times, since execute and redo
            Assert.IsTrue(_commandManager.IsRedoEnabled);
            _commandManager.Redo(_width);
            Assert.AreEqual(1, ((Stack<ICommand>)_privateCommandManager.GetField("_undo")).Count);
            Assert.AreEqual(0, ((Stack<ICommand>)_privateCommandManager.GetField("_redo")).Count);
            _mockCommand.Verify(command => command.Execute(_width), Times.Exactly(2));
            // _redo.Count <= 0
            Assert.IsFalse(_commandManager.IsRedoEnabled);
            Assert.ThrowsException<Exception>(() => _commandManager.Redo(_width));
        }

        // test all clear
        [TestMethod()]
        public void AllClearTest()
        {
            
            bool eventRaised = false;
            _commandManager._undoRedoChanged += (isUndo, isRedu) =>
            {
                eventRaised = true;
            };
            _commandManager.AllClear();
            Assert.IsTrue(eventRaised);
            Assert.IsFalse(_commandManager.IsUndoEnabled);
            Assert.IsFalse(_commandManager.IsRedoEnabled);
        }
    }
}