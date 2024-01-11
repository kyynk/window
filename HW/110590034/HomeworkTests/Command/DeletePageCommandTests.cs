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
    public class DeletePageCommandTests
    {
        private DeletePageCommand _deletePageCommand;
        private Mock<Model.Model> _mockModel;
        private Mock<Shapes> _mockShapes;
        private PrivateObject _privateDeletePageCommand;
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
            _deletePageCommand = new DeletePageCommand(_mockModel.Object, _mockShapes.Object, _pageIndex);
            _privateDeletePageCommand = new PrivateObject(_deletePageCommand);
        }

        // test constructor
        [TestMethod()]
        public void DeletePageCommandTest()
        {
            _deletePageCommand = new DeletePageCommand(_mockModel.Object, _mockShapes.Object, _pageIndex);
            _privateDeletePageCommand = new PrivateObject(_deletePageCommand);

            Assert.AreEqual(0, (int)_privateDeletePageCommand.GetField("_pageIndex"));
            Assert.AreEqual(-1, (double)_privateDeletePageCommand.GetField("_panelWidth"));
            Assert.IsNotNull((Shapes)_privateDeletePageCommand.GetField("_shapes"));
            Assert.IsNotNull((Model.Model)_privateDeletePageCommand.GetField("_model"));
        }

        // test execute
        [TestMethod()]
        public void ExecuteTest()
        {
            _deletePageCommand.SetPanelWidth(_panelWidth);
            _deletePageCommand.Execute(_panelWidth);
            _mockModel.Verify(model => model.RemovePageByIndex((int)_privateDeletePageCommand.GetField("_pageIndex")), Times.Once);
            Assert.AreEqual(_panelWidth, (double)_privateDeletePageCommand.GetField("_panelWidth"));
        }

        // test undo
        [TestMethod()]
        public void UndoTest()
        {
            _deletePageCommand.SetPanelWidth(_panelWidth);
            _deletePageCommand.Undo(_panelWidth);
            _mockModel.Verify(model => model.InsertPageByIndex(It.IsAny<Shapes>(), (int)_privateDeletePageCommand.GetField("_pageIndex")), Times.Once);
            Assert.AreEqual(_panelWidth, (double)_privateDeletePageCommand.GetField("_panelWidth"));
        }

        // test set panel width
        [TestMethod()]
        public void SetPanelWidthTest()
        {
            _deletePageCommand.SetPanelWidth(200);
            Assert.AreEqual(200, (double)_privateDeletePageCommand.GetField("_panelWidth"));
        }

        // test adjust with panel width
        [TestMethod()]
        public void AdjustWithPanelWidthTest()
        {
            _deletePageCommand.SetPanelWidth(200);

            _deletePageCommand.AdjustWithPanelWidth(100);
            _mockShapes.Verify(shapes => shapes.ResizeForPanel(0.5), Times.Once);
            Assert.AreEqual(100, (double)_privateDeletePageCommand.GetField("_panelWidth"));
        }
    }
}