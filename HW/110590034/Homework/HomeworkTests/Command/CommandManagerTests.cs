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

        // setup
        [TestInitialize()]
        public void Initialize()
        {
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
            _commandManager.Execute(_mockCommand.Object);

            Assert.AreEqual(1, ((Stack<ICommand>)_privateCommandManager.GetField("_undo")).Count);
            Assert.AreEqual(0, ((Stack<ICommand>)_privateCommandManager.GetField("_redo")).Count);
        }

        // test undo
        [TestMethod()]
        public void UndoTest()
        {
            _mockCommand = new Mock<ICommand>();
            _commandManager.Execute(_mockCommand.Object);
            // _undo.Count > 0
            _commandManager.Undo();
            Assert.AreEqual(0, ((Stack<ICommand>)_privateCommandManager.GetField("_undo")).Count);
            Assert.AreEqual(1, ((Stack<ICommand>)_privateCommandManager.GetField("_redo")).Count);
            _mockCommand.Verify(command => command.Undo(), Times.Once);
            // _undo.Count <= 0
            Assert.ThrowsException<Exception>(() => _commandManager.Undo());
        }

        // test redo
        [TestMethod()]
        public void RedoTest()
        {
            _mockCommand = new Mock<ICommand>();
            _commandManager.Execute(_mockCommand.Object);
            _commandManager.Undo();
            // _redo.Count > 0
            // 2 times, since execute and redo
            _commandManager.Redo();
            Assert.AreEqual(1, ((Stack<ICommand>)_privateCommandManager.GetField("_undo")).Count);
            Assert.AreEqual(0, ((Stack<ICommand>)_privateCommandManager.GetField("_redo")).Count);
            _mockCommand.Verify(command => command.Execute(), Times.Exactly(2));
            // _redo.Count <= 0
            Assert.ThrowsException<Exception>(() => _commandManager.Redo());
        }
    }
}