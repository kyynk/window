﻿using Homework.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.ComponentModel;
using System.Windows.Forms;
using Homework.State;
using Homework.Command;
using Moq;
using System;

namespace Homework.Model.Tests
{
    [TestClass()]
    public class ModelTests
    {
        private Model _model;
        private PrivateObject _privateModel;

        // setup
        [TestInitialize()]
        public void Initialize()
        {
            _model = new Model();
            _privateModel = new PrivateObject(_model);
            _model._pagesChanged += HandlePagesChanged;
        }

        // test constructor
        [TestMethod()]
        public void ModelTest()
        {
            _model = new Model();
            _privateModel = new PrivateObject(_model);
            _model._pagesChanged += HandlePagesChanged;
            Assert.IsNotNull(_model);
            Assert.AreEqual(Constant.Constant.POINT, _model.ShapeName);
            Assert.IsNotNull(_model.GetShapes());
            Assert.AreEqual(448, (int)_privateModel.GetField("_panelMaxX"));
            Assert.AreEqual(252, (int)_privateModel.GetField("_panelMaxY"));
            Assert.IsNull((Shape)_privateModel.GetField("_tempShape"));
            Assert.IsNotNull((Shapes)_privateModel.GetField("_shapesData"));
            Assert.IsNotNull((ShapeFactory)_privateModel.GetField("_shapeFactory"));
            Assert.IsInstanceOfType(_privateModel.GetField("_state"), typeof(PointState));
            Assert.AreEqual(-1, ((Point)_privateModel.GetField("_firstPoint")).X);
            Assert.AreEqual(-1, ((Point)_privateModel.GetField("_firstPoint")).Y);
            Assert.IsInstanceOfType(_privateModel.GetField("_commandManager"), typeof(CommandManager));
        }

        // handle pages changed
        public void HandlePagesChanged(Pages.PageAction pageAction, int index)
        {
            if (pageAction == Pages.PageAction.Switch)
                _model.SwitchPage(index);
        }

        // test ShapeName property
        [TestMethod()]
        public void ShapeNamePropertyTest()
        {
            _model.ShapeName = Constant.Constant.LINE;
            Assert.AreEqual(Constant.Constant.LINE, _model.ShapeName);
        }

        // test change state
        [TestMethod()]
        public void ChangeStateTest()
        {
            _model.ChangeState(Constant.Constant.DRAWING_STATE);
            Assert.IsInstanceOfType(_privateModel.GetField("_state"), typeof(DrawingState));
            _model.ChangeState(Constant.Constant.POINT_STATE);
            Assert.IsInstanceOfType(_privateModel.GetField("_state"), typeof(PointState));
            _model.ChangeState(Constant.Constant.RESIZE_STATE);
            Assert.IsInstanceOfType(_privateModel.GetField("_state"), typeof(ResizeState));
        }

        // test press pointer
        [TestMethod()]
        public void PressPointerTest()
        {
            Mock<IState> mockState = new Mock<IState>();
            _privateModel.SetField("_state", mockState.Object);
            _privateModel.SetField("_panelMaxX", 100);
            _privateModel.SetField("_panelMaxY", 100);
            // test branch coverage
            _model.PressPointer(10, 20);
            _model.PressPointer(1000, 20);
            _model.PressPointer(-10, 20);
            _model.PressPointer(10, 200);
            _model.PressPointer(10, -20);
            _model.PressPointer(1000, 2000);
            _model.PressPointer(-10, -20);
            mockState.Verify(state => state.MouseDown(It.IsAny<Point>(), It.IsAny<string>(), It.IsAny<bool>()), Times.Exactly(7));
        }

        // test move pointer
        [TestMethod()]
        public void MovePointerTest()
        {
            Mock<IState> mockState = new Mock<IState>();
            _privateModel.SetField("_state", mockState.Object);
            double mouseX = 10;
            double mouseY = 20;
            _model.MovePointer(mouseX, mouseY);
            mockState.Verify(state => state.MouseMove(It.IsAny<Point>()), Times.Once);
        }

        // test release pointer
        [TestMethod()]
        public void ReleasePointerTest()
        {
            Mock<IState> mockState = new Mock<IState>();
            _privateModel.SetField("_state", mockState.Object);
            double mouseX = 10;
            double mouseY = 20;
            _model.ReleasePointer(mouseX, mouseY);
            mockState.Verify(state => state.MouseUp(It.IsAny<Point>(), It.IsAny<string>()), Times.Once);
        }

        // test draw
        [TestMethod()]
        public void DrawTest()
        {
            var mockGraphics = new MockGraphics();
            var mockState = new Mock<IState>();
            _privateModel.SetField("_state", mockState.Object);
            _model.Create(Constant.Constant.LINE, new Point(1, 1), new Point(10, 10));
            _model.Draw(mockGraphics);
            Assert.AreEqual(1, mockGraphics.DrawLineCount);
            mockState.Verify(state => state.Drawing(mockGraphics), Times.Once);
            // clean
            _model.Delete(0);
        }

        // test create drawing shape
        [TestMethod()]
        public void CreateDrawingShapeTest()
        {
            string shapeType = Constant.Constant.LINE;
            Point firstPoint = new Point(10, 20);
            _model.CreateDrawingShape(shapeType, firstPoint);
            Assert.IsNotNull(_privateModel.GetField("_tempShape"));
            Assert.AreEqual(shapeType, ((Line)_privateModel.GetField("_tempShape")).GetShapeName());
            // clean
            _model.ClearDrawingShape();
        }

        // test move drawing shape
        [TestMethod()]
        public void MoveDrawingShapeTest()
        {
            string shapeType = Constant.Constant.LINE;
            Point firstPoint = new Point(10, 20);
            _model.CreateDrawingShape(shapeType, firstPoint);
            Point secondPoint = new Point(20, 20);
            _model.MoveDrawingShape(secondPoint);
            Assert.AreEqual(secondPoint.X, ((Shape)_privateModel.GetField("_tempShape")).Point2.X);
            Assert.AreEqual(secondPoint.Y, ((Shape)_privateModel.GetField("_tempShape")).Point2.Y);
            // clean
            _model.ClearDrawingShape();
        }

        // test add drawing shape
        [TestMethod()]
        public void AddDrawingShapeTest()
        {
            // command manager -> model, then add shape to shapesList
            _model.AddDrawingShape(Constant.Constant.LINE, new Point(10, 20), new Point(20, 20));
            _model.AddDrawingShape(Constant.Constant.RECTANGLE, new Point(0, 10), new Point(10, 0));
            _model.AddDrawingShape(Constant.Constant.ELLIPSE, new Point(50, 60), new Point(70, 80));
            Assert.AreEqual(3, _model.GetShapes().Count);
            Assert.AreEqual(Constant.Constant.LINE, _model.GetShapes()[0].GetShapeName());
            Assert.AreEqual(Constant.Constant.RECTANGLE, _model.GetShapes()[1].GetShapeName());
            Assert.AreEqual(Constant.Constant.ELLIPSE, _model.GetShapes()[2].GetShapeName());
            // clean
            _model.Delete(0);
            _model.Delete(0);
            _model.Delete(0);
        }

        // test clear drawing shape
        [TestMethod()]
        public void ClearDrawingShapeTest()
        {
            string shapeType = Constant.Constant.LINE;
            Point firstPoint = new Point(10, 20);
            _model.CreateDrawingShape(shapeType, firstPoint);
            Assert.IsNotNull(_privateModel.GetField("_tempShape"));
            _model.ClearDrawingShape();
            Assert.IsNull(_privateModel.GetField("_tempShape"));
        }

        // test draw drawing shape
        [TestMethod()]
        public void DrawDrawingShapeTest()
        {
            string shapeType = Constant.Constant.LINE;
            Point firstPoint = new Point(10, 20);
            MockGraphics mockGraphics = new MockGraphics();
            _model.CreateDrawingShape(shapeType, firstPoint);
            _model.DrawDrawingShape(mockGraphics);
            Assert.AreEqual(1, mockGraphics.DrawLineCount);
            // clean
            _model.ClearDrawingShape();
        }

        // test get selected shape
        [TestMethod()]
        public void GetSelectedShapeTest()
        {
            _model.AddDrawingShape(Constant.Constant.LINE, new Point(0, 0), new Point(10, 10));
            _model.AddDrawingShape(Constant.Constant.RECTANGLE, new Point(11, 11), new Point(20, 20));
            _model.AddDrawingShape(Constant.Constant.ELLIPSE, new Point(50, 50), new Point(70, 70));
            bool isSelected = _model.CheckSelectedShape(5, 5);
            Assert.IsInstanceOfType(_model.GetSelectedShape(), typeof(Shape));
            Assert.IsTrue(isSelected);
            Assert.AreEqual(Constant.Constant.LINE, _model.GetSelectedShape().GetShapeName());

            // clean
            _model.Delete(0);
            _model.Delete(0);
            _model.Delete(0);
        }

        // test check selected shape
        [TestMethod()]
        public void CheckSelectedShapeTest()
        {
            _model.AddDrawingShape(Constant.Constant.LINE, new Point(0, 0), new Point(10, 10));
            _model.AddDrawingShape(Constant.Constant.RECTANGLE, new Point(11, 11), new Point(20, 20));
            _model.AddDrawingShape(Constant.Constant.ELLIPSE, new Point(50, 50), new Point(70, 70));
            Assert.IsTrue(_model.CheckSelectedShape(5, 5));
            Assert.IsTrue(_model.CheckSelectedShape(15, 15));
            Assert.IsTrue(_model.CheckSelectedShape(65, 65));
            Assert.IsFalse(_model.CheckSelectedShape(105, 105));
            // clean
            _model.Delete(0);
            _model.Delete(0);
            _model.Delete(0);
        }

        // test move selected shape
        [TestMethod()]
        public void MoveSelectedShapeTest()
        {
            _model.AddDrawingShape(Constant.Constant.LINE, new Point(6, 5), new Point(11, 10));
            Assert.IsTrue(_model.CheckSelectedShape(8, 8));
            double diffX = 5;
            double diffY = 5;
            _model.MoveSelectedShape(diffX, diffY);
            Assert.AreEqual(6 - diffX, _model.GetSelectedShape().Point1.X);
            Assert.AreEqual(5 - diffY, _model.GetSelectedShape().Point1.Y);
            Assert.AreEqual(11 - diffX, _model.GetSelectedShape().Point2.X);
            Assert.AreEqual(10 - diffY, _model.GetSelectedShape().Point2.Y);
            // clean
            _model.Delete(0);
        }

        // test move done
        [TestMethod()]
        public void MoveDoneTest()
        {
            _model.AddDrawingShape(Constant.Constant.LINE, new Point(6, 5), new Point(11, 10));
            Assert.IsTrue(_model.CheckSelectedShape(8, 8));

            bool eventRaised = false;
            _model._modelChanged += () => { eventRaised = true; };
            _model.MoveDone(5, 5);
            Assert.IsTrue(eventRaised);
            // clean
            _model.Delete(0);
        }

        // test get location
        [TestMethod()]
        public void GetLocationTest()
        {
            _model.AddDrawingShape(Constant.Constant.LINE, new Point(0, 0), new Point(10, 10));
            bool isSelected = _model.CheckSelectedShape(5, 5);
            Assert.IsInstanceOfType(_model.GetSelectedShape(), typeof(Shape));
            Assert.IsTrue(isSelected);
            Assert.IsInstanceOfType(_model.GetLocation(1, 1), typeof(Shape.Location));
            _model.Delete(0);
        }

        // test check bottom point
        [TestMethod()]
        public void CheckBottomPointIsSecondPointTest()
        {
            _model.AddDrawingShape(Constant.Constant.LINE, new Point(0, 0), new Point(10, 10));
            bool isSelected = _model.CheckSelectedShape(5, 5);
            Assert.IsInstanceOfType(_model.GetSelectedShape(), typeof(Shape));
            Assert.IsTrue(isSelected);
            _model.CheckBottomPoint();
            Assert.IsFalse(_model.GetSelectedShape().IsFirstPointBottom);
            _model.Delete(0);
        }

        // test check bottom point
        [TestMethod()]
        public void CheckBottomPointIsFirstPointTest()
        {
            _model.AddDrawingShape(Constant.Constant.LINE, new Point(0, 10), new Point(10, 0));
            bool isSelected = _model.CheckSelectedShape(5, 5);
            Assert.IsInstanceOfType(_model.GetSelectedShape(), typeof(Shape));
            Assert.IsTrue(isSelected);
            _model.CheckBottomPoint();
            Assert.IsTrue(_model.GetSelectedShape().IsFirstPointBottom);
            _model.Delete(0);
        }

        // test check is resize state
        [TestMethod()]
        public void CheckIsResizeStateTest()
        {
            _model.AddDrawingShape(Constant.Constant.LINE, new Point(0, 0), new Point(10, 10));
            bool isSelected = _model.CheckSelectedShape(5, 5);
            Assert.IsInstanceOfType(_model.GetSelectedShape(), typeof(Shape));
            Assert.IsTrue(isSelected);
            Assert.IsFalse(_model.CheckIsResizeState(100, 100));
            Assert.IsTrue(_model.CheckIsResizeState(0, 1));
            _model.Delete(0);
            Assert.IsFalse(_model.CheckIsResizeState(100, 100));
        }

        // test check location is right bottom
        [TestMethod()]
        public void CheckLocationIsRightBottomTest()
        {
            _model.AddDrawingShape(Constant.Constant.LINE, new Point(0, 0), new Point(10, 10));
            Assert.IsFalse(_model.CheckLocationIsRightBottom(10, 10));
            bool isSelected = _model.CheckSelectedShape(5, 5);
            Assert.IsInstanceOfType(_model.GetSelectedShape(), typeof(Shape));
            Assert.IsTrue(isSelected);
            Assert.IsFalse(_model.CheckLocationIsRightBottom(100, 100));
            Assert.IsTrue(_model.CheckLocationIsRightBottom(11, 11));
            _model.Delete(0);
        }

        // test resize selected shape
        [TestMethod()]
        public void ResizeSelectedShapeTest()
        {
            _model.AddDrawingShape(Constant.Constant.ELLIPSE, new Point(0, 0), new Point(10, 10));
            bool isSelected = _model.CheckSelectedShape(5, 5);
            Assert.IsInstanceOfType(_model.GetSelectedShape(), typeof(Shape));
            Assert.IsTrue(isSelected);
            Assert.IsTrue(_model.CheckLocationIsRightBottom(11, 11));
            _model.ResizeSelectedShape(15, 15);
            Assert.AreEqual(15, _model.GetSelectedShape().Point2.X);
            Assert.AreEqual(15, _model.GetSelectedShape().Point2.Y);
            Assert.IsFalse(_model.CheckLocationIsRightBottom(0, 0));
            _model.ResizeSelectedShape(20, 20);
            Assert.AreEqual(15, _model.GetSelectedShape().Point2.X);
            Assert.AreEqual(15, _model.GetSelectedShape().Point2.Y);
            _model.Delete(0);
        }

        // test stop resize selected shape
        [TestMethod()]
        public void StopResizeSelectedShapeTest()
        {
            _model.AddDrawingShape(Constant.Constant.RECTANGLE, new Point(0, 0), new Point(10, 10));
            bool isSelected = _model.CheckSelectedShape(5, 5);
            Assert.IsInstanceOfType(_model.GetSelectedShape(), typeof(Shape));
            Assert.IsTrue(isSelected);
            Assert.IsTrue(_model.CheckLocationIsRightBottom(11, 11));
            _model.ResizeSelectedShape(15, 15);
            Assert.IsTrue(_model.GetSelectedShape().IsResizing);
            _model.StopResizeSelectedShape();
            Assert.IsFalse(_model.GetSelectedShape().IsResizing);
            _model.Delete(0);
        }

        // test check range
        [TestMethod()]
        public void CheckRangeTest()
        {
            double max = 100;
            Assert.AreEqual(0, _model.CheckRange(-10, max));
            Assert.AreEqual(50, _model.CheckRange(50, max));
            Assert.AreEqual(max, _model.CheckRange(max, max));
            Assert.AreEqual(max, _model.CheckRange(120, max));
        }

        // test undo
        [TestMethod()]
        public void UndoTest()
        {
            _model.AddDrawingShape(Constant.Constant.RECTANGLE, new Point(0, 0), new Point(10, 10));
            // can undo
            bool eventRaised = false;
            _model._modelChanged += () => { eventRaised = true; };
            _model.Undo();
            Assert.IsTrue(eventRaised);
            Assert.AreEqual(0, _model.GetShapes().Count);
            // can not undo
            eventRaised = false;
            _model._modelChanged += () => { eventRaised = true; };
            _model.Undo();
            Assert.IsTrue(eventRaised);
            Assert.AreEqual(0, _model.GetShapes().Count);
            // clean
            _model.Redo();
            _model.Delete(0);
        }

        // test redo
        [TestMethod()]
        public void RedoTest()
        {
            _model.AddDrawingShape(Constant.Constant.RECTANGLE, new Point(0, 0), new Point(10, 10));
            _model.Undo();
            Assert.AreEqual(0, _model.GetShapes().Count);
            // can redo
            bool eventRaised = false;
            _model._modelChanged += () => { eventRaised = true; };
            _model.Redo();
            Assert.IsTrue(eventRaised);
            Assert.AreEqual(1, _model.GetShapes().Count);
            // can not redo
            eventRaised = false;
            _model._modelChanged += () => { eventRaised = true; };
            _model.Redo();
            Assert.IsTrue(eventRaised);
            Assert.AreEqual(1, _model.GetShapes().Count);
            // clean
            _model.Delete(0);
        }

        // test is undo enabled property
        [TestMethod()]
        public void IsUndoEnabledTest()
        {
            Model temp = new Model();
            temp._pagesChanged += HandlePagesChanged;
            Assert.IsFalse(temp.IsUndoEnabled);
            temp.Create(Constant.Constant.RECTANGLE, new Point(1, 1), new Point(2, 2));
            Assert.IsTrue(temp.IsUndoEnabled);
        }

        // test is redo enabled property
        [TestMethod()]
        public void IsRedoEnabledTest()
        {
            Model temp = new Model();
            temp._pagesChanged += HandlePagesChanged;
            Assert.IsFalse(temp.IsRedoEnabled);
            temp.Create(Constant.Constant.RECTANGLE, new Point(1, 1), new Point(2, 2));
            temp.Undo();
            Assert.IsTrue(temp.IsRedoEnabled);
        }

        // test insert shape
        [TestMethod()]
        public void InsertShapeTest()
        {
            bool eventRaised = false;
            _model._modelChanged += () => { eventRaised = true; };
            _model.InsertShape(new Rectangle(new Point(0, 0), new Point(10, 10)), 0, 0);
            Assert.IsTrue(eventRaised);
            Assert.AreEqual(1, _model.GetShapes().Count);
            // clean
            _model.DeleteShape(0, 0);
        }

        // test create
        [TestMethod()]
        public void CreateTest()
        {
            _model.Create(Constant.Constant.LINE, new Point(1, 1), new Point(2, 2));
            _model.Create(Constant.Constant.RECTANGLE, new Point(1, 1), new Point(2, 2));
            _model.Create(Constant.Constant.ELLIPSE, new Point(1, 1), new Point(2, 2));
            Assert.AreEqual(3, _model.GetShapes().Count);
            Assert.AreEqual(Constant.Constant.LINE, _model.GetShapes()[0].GetShapeName());
            Assert.AreEqual(Constant.Constant.RECTANGLE, _model.GetShapes()[1].GetShapeName());
            Assert.AreEqual(Constant.Constant.ELLIPSE, _model.GetShapes()[2].GetShapeName());
            // clean
            _model.Delete(0);
            _model.Delete(0);
            _model.Delete(0);
        }

        // test get shapes
        [TestMethod()]
        public void GetShapesTest()
        {
            Assert.IsInstanceOfType(_model.GetShapes(), typeof(BindingList<Shape>));
            _model.Create(Constant.Constant.LINE, new Point(1, 1), new Point(2, 2));
            _model.Create(Constant.Constant.RECTANGLE, new Point(1, 1), new Point(2, 2));
            _model.Create(Constant.Constant.ELLIPSE, new Point(1, 1), new Point(2, 2));
            Assert.AreEqual(3, _model.GetShapes().Count);
            Assert.AreEqual(Constant.Constant.LINE, _model.GetShapes()[0].GetShapeName());
            Assert.AreEqual(Constant.Constant.RECTANGLE, _model.GetShapes()[1].GetShapeName());
            Assert.AreEqual(Constant.Constant.ELLIPSE, _model.GetShapes()[2].GetShapeName());
            // clean
            _model.Delete(0);
            _model.Delete(0);
            _model.Delete(0);
        }

        // test delete shape
        [TestMethod()]
        public void DeleteShapeTest()
        {
            _model.InsertShape(new Rectangle(new Point(0, 0), new Point(10, 10)), 0, 0);

            bool eventRaised = false;
            _model._modelChanged += () => { eventRaised = true; };
            _model.DeleteShape(0, 0);
            Assert.IsTrue(eventRaised);
            Assert.AreEqual(0, _model.GetShapes().Count);
        }

        // test delete
        [TestMethod()]
        public void DeleteTest()
        {
            _model.Create(Constant.Constant.LINE, new Point(1, 1), new Point(2, 2));
            _model.Create(Constant.Constant.RECTANGLE, new Point(1, 1), new Point(2, 2));
            _model.Create(Constant.Constant.ELLIPSE, new Point(1, 1), new Point(2, 2));
            Assert.AreEqual(3, _model.GetShapes().Count);
            _model.Delete(0);
            Assert.AreEqual(2, _model.GetShapes().Count);
            _model.Delete(0);
            Assert.AreEqual(1, _model.GetShapes().Count);
            _model.Delete(0);
            Assert.AreEqual(0, _model.GetShapes().Count);
        }

        // test get shape index
        [TestMethod()]
        public void GetShapeIndexTest()
        {
            _model.InsertShape(new Rectangle(new Point(0, 0), new Point(10, 10)), 0, 0);
            int index1 = _model.GetShapeIndex(_model.GetShapes()[0]);
            int index2 = _model.GetShapeIndex(new Line(new Point(0, 0), new Point(10, 10)));
            Assert.AreEqual(0, index1);
            Assert.AreEqual(-1, index2);
            // clean
            _model.DeleteShape(0, 0);
        }

        // test handle keydown
        [TestMethod()]
        public void HandleKeyDownTest()
        {
            _model.AddDrawingShape(Constant.Constant.LINE, new Point(0, 0), new Point(10, 10));
            _model.AddDrawingShape(Constant.Constant.RECTANGLE, new Point(11, 11), new Point(20, 20));
            _model.AddDrawingShape(Constant.Constant.ELLIPSE, new Point(50, 50), new Point(70, 70));
            // select
            Assert.IsTrue(_model.CheckSelectedShape(6, 5));
            // test not Delete key
            Assert.AreEqual(Constant.Constant.LINE, _model.GetShapes()[0].ShapeName);
            Assert.AreEqual(Constant.Constant.LINE, _model.GetSelectedShape().ShapeName);
            _model.HandleKeyDown(Keys.Escape);
            Assert.AreEqual(Constant.Constant.LINE, _model.GetShapes()[0].ShapeName);
            Assert.AreEqual(3, _model.GetShapes().Count);
            Assert.IsNotNull(_model.GetSelectedShape());
            // test Delete key
            _model.HandleKeyDown(Keys.Delete);
            Assert.AreEqual(Constant.Constant.RECTANGLE, _model.GetShapes()[0].ShapeName);
            Assert.AreEqual(2, _model.GetShapes().Count);
            Assert.IsNull(_model.GetSelectedShape());

            // clean
            _model.Delete(0);
            _model.Delete(0);
        }

        // test handle keydown
        [TestMethod()]
        public void HandleKeyDownForDeletePageTest()
        {
            Assert.AreEqual(1, ((Pages)_privateModel.GetField("_pages")).GetPages().Count);
            
            // test cannot delete page
            _model.HandleKeyDown(Keys.Delete);
            Assert.AreEqual(1, ((Pages)_privateModel.GetField("_pages")).GetPages().Count);

            // test delete page
            _model.AddPage(0);
            Assert.AreEqual(2, ((Pages)_privateModel.GetField("_pages")).GetPages().Count);
            _model.HandleKeyDown(Keys.Delete);
            Assert.AreEqual(1, ((Pages)_privateModel.GetField("_pages")).GetPages().Count);
        }

        // test set panel size test
        [TestMethod()]
        public void SetPanelSizeTest()
        {
            double width = 100;
            _privateModel.SetField("_panelMaxX", 1600);
            _privateModel.SetField("_panelMaxY", 900);
            _model.InsertShape(new Ellipse(new Point(0, 0), new Point(16, 16)), 0, 0);
            bool eventRaised = false;
            _model._modelChanged += () => { eventRaised = true; };

            _model.SetPanelSize(width);

            Assert.IsTrue(eventRaised);
            Assert.AreEqual(0 * width / 1600, _model.GetShapes()[0].Point1.X);
            Assert.AreEqual(0 * width / 1600, _model.GetShapes()[0].Point1.Y);
            Assert.AreEqual(16 * width / 1600, _model.GetShapes()[0].Point2.X);
            Assert.AreEqual(16 * width / 1600, _model.GetShapes()[0].Point2.Y);
            Assert.AreEqual((int)width, _privateModel.GetField("_panelMaxX"));
            Assert.AreEqual((int)(width * Constant.Constant.PANEL_RATIO), _privateModel.GetField("_panelMaxY"));
        }

        // test notify model changed
        [TestMethod()]
        public void NotifyModelChangedTest()
        {
            bool eventRaised = false;
            _model._modelChanged += () => { eventRaised = true; };
            _model.NotifyModelChanged();
            Assert.IsTrue(eventRaised);
        }

        // test add page
        [TestMethod()]
        public void AddPageTest()
        {
            Assert.AreEqual(1, ((Pages)_privateModel.GetField("_pages")).GetPages().Count);
            _model.AddPage(0);
            Assert.AreEqual(2, ((Pages)_privateModel.GetField("_pages")).GetPages().Count);
            Assert.AreEqual(0, _model.PageIndex);

            // clean
            _model.HandleKeyDown(Keys.Delete);
        }

        // test remove page
        [TestMethod()]
        public void RemovePageTest()
        {
            _model.AddPage(0);
            Assert.AreEqual(2, ((Pages)_privateModel.GetField("_pages")).GetPages().Count);

            _model.RemovePage();

            Assert.AreEqual(1, ((Pages)_privateModel.GetField("_pages")).GetPages().Count);
            Assert.AreEqual(0, _model.PageIndex);
        }

        // test insert page by index
        [TestMethod()]
        public void InsertPageByIndexTest()
        {
            Assert.AreEqual(1, ((Pages)_privateModel.GetField("_pages")).GetPages().Count);
            _model.InsertPageByIndex(new Shapes(), 0);
            Assert.AreEqual(2, ((Pages)_privateModel.GetField("_pages")).GetPages().Count);
            Assert.AreEqual(0, _model.PageIndex);

            // clean
            _model.RemovePageByIndex(0);
        }

        // test remove page by index
        [TestMethod()]
        public void RemovePageByIndexTest()
        {
            // remove page
            _model.InsertPageByIndex(new Shapes(), 1);
            Assert.AreEqual(2, ((Pages)_privateModel.GetField("_pages")).GetPages().Count);

            _model.RemovePageByIndex(0);

            Assert.AreEqual(1, ((Pages)_privateModel.GetField("_pages")).GetPages().Count);
            Assert.AreEqual(0, _model.PageIndex);

            // remove page (last oage)
            _model.InsertPageByIndex(new Shapes(), 1);
            Assert.AreEqual(2, ((Pages)_privateModel.GetField("_pages")).GetPages().Count);

            Console.WriteLine("remove page (last oage)");
            _model.RemovePageByIndex(1);

            Assert.AreEqual(1, ((Pages)_privateModel.GetField("_pages")).GetPages().Count);
            Assert.AreEqual(0, _model.PageIndex);
        }

        // test select page
        [TestMethod()]
        public void SelectPageTest()
        {
            _model.AddPage(0);
            Assert.AreEqual(0, _model.PageIndex);

            _model.SelectPage(1);
            Assert.AreEqual(1, _model.PageIndex);

            // clean
            _model.RemovePageByIndex(0);
        }

        // test switch page
        [TestMethod()]
        public void SwitchPageTest()
        {
            _model.AddPage(0);
            Assert.AreEqual(0, _model.PageIndex);

            _model.SwitchPage(1);
            Assert.AreEqual(1, _model.PageIndex);

            // clean
            _model.RemovePageByIndex(0);
        }

        // test is selected shape
        [TestMethod()]
        public void IsSelectedShapeTest()
        {
            _model.AddDrawingShape(Constant.Constant.LINE, new Point(0, 0), new Point(10, 10));
            Assert.IsFalse(_model.IsSelectedShape());
            Assert.IsTrue(_model.CheckSelectedShape(6, 5));
            Assert.IsTrue(_model.IsSelectedShape());

            // clean
            _model.Delete(0);
        }

        // test handle pages changed
        [TestMethod()]
        public void HandlePagesChangedTest()
        {
            bool eventRaised = false;
            Pages.PageAction invokedAction = Pages.PageAction.Remove;
            int invokedIndex = -1;

            _model._pagesChanged += (pageAction, index) =>
            {
                eventRaised = true;
                invokedAction = pageAction;
                invokedIndex = index;
            };

            _model.HandlePagesChanged(Pages.PageAction.Add, 0);

            Assert.AreEqual(Pages.PageAction.Add, invokedAction);
            Assert.AreEqual(0, invokedIndex);
            Assert.IsTrue(eventRaised);
        }

        // test handle undo redo changed
        [TestMethod()]
        public void HandleUndoRedoChangedTest()
        {
            bool eventRaised = false;
            bool invokedIsUndo = false;
            bool invokedIsRedo = true;

            _model._undoRedoChanged += (isUndo, isRedo) =>
            {
                eventRaised = true;
                invokedIsUndo = isUndo;
                invokedIsRedo = isRedo;
            };

            _model.HandleUndoRedoChanged(true, true);

            Assert.IsTrue(invokedIsUndo);
            Assert.IsTrue(invokedIsRedo);
            Assert.IsTrue(eventRaised);
        }
    }
}