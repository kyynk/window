using Microsoft.VisualStudio.TestTools.UnitTesting;
using Homework.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using Homework.Model;

namespace Homework.Command.Tests
{
    [TestClass()]
    public class AddPageCommandTests
    {
        private AddPageCommand _addPageCommand;
        private Mock<Model.Model> _mockModel;
        private Mock<Shapes> _mockShapes;
        private PrivateObject _privateAddPageCommand;
        private double _panelWidth;
        private int _pageIndex;

        // setup
        [TestInitialize()]
        public void Initialize()
        {
            _pageIndex = 0;
            _panelWidth = 500;
            _mockModel = new Mock<Model.Model>();
            _mockShapes = new Mock<Shapes>();
            _addPageCommand = new AddPageCommand(_mockModel.Object, _mockShapes.Object, _pageIndex);
            _privateAddPageCommand = new PrivateObject(_addPageCommand);
        }

        // test constructor
        [TestMethod()]
        public void AddPageCommandTest()
        {
            _addPageCommand = new AddPageCommand(_mockModel.Object, _mockShapes.Object, _pageIndex);
            _privateAddPageCommand = new PrivateObject(_addPageCommand);

            Assert.AreEqual(0, (int)_privateAddPageCommand.GetField("_pageIndex"));
            Assert.AreEqual(-1, (double)_privateAddPageCommand.GetField("_panelWidth"));
            Assert.IsNotNull((Shapes)_privateAddPageCommand.GetField("_shapes"));
            Assert.IsNotNull((Model.Model)_privateAddPageCommand.GetField("_model"));
        }

        // test execute
        [TestMethod()]
        public void ExecuteTest()
        {
            _addPageCommand.SetPanelWidth(_panelWidth);
            _addPageCommand.Execute(_panelWidth);
            _mockModel.Verify(model => model.InsertPageByIndex(It.IsAny<Shapes>(), (int)_privateAddPageCommand.GetField("_pageIndex")), Times.Once);
            Assert.AreEqual(_panelWidth, (double)_privateAddPageCommand.GetField("_panelWidth"));
        }

        // test undo
        [TestMethod()]
        public void UndoTest()
        {
            _addPageCommand.SetPanelWidth(_panelWidth);
            _addPageCommand.Undo(_panelWidth);
            _mockModel.Verify(model => model.RemovePageByIndex((int)_privateAddPageCommand.GetField("_pageIndex")), Times.Once);
            Assert.AreEqual(_panelWidth, (double)_privateAddPageCommand.GetField("_panelWidth"));
        }

        // test set panel width
        [TestMethod()]
        public void SetPanelWidthTest()
        {
            _addPageCommand.SetPanelWidth(200);
            Assert.AreEqual(200, (double)_privateAddPageCommand.GetField("_panelWidth"));
        }

        // test adjust with panel width
        [TestMethod()]
        public void AdjustWithPanelWidthTest()
        {
            _addPageCommand.SetPanelWidth(200);

            _addPageCommand.AdjustWithPanelWidth(100);
            _mockShapes.Verify(shapes => shapes.ResizeForPanel(0.5), Times.Once);
            Assert.AreEqual(100, (double)_privateAddPageCommand.GetField("_panelWidth"));
        }
    }
}