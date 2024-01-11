using Homework.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Homework.UI.Tests
{
    [TestClass()]
    public class UITests
    {
        //private const string PANEL = "panel";

        public string targetAppPath;
        public const string MENU_FORM = "Form1";
        public Robot _robot;
        private WindowsElement _canvas;
        private WindowsElement _flowLayoutPanel;
        private Random _random;

        // setup
        [TestInitialize()]
        public void Initialize()
        {
            var projectName = "Homework";
            string solutionPath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\..\\"));
            targetAppPath = Path.Combine(solutionPath, projectName, "bin", "Debug", "Homework.exe");
            Console.WriteLine(targetAppPath);
            Console.WriteLine(MENU_FORM);
            _robot = new Robot(targetAppPath, MENU_FORM);
            _random = new Random();

            _robot.Sleep(1);
            Console.WriteLine("hi 1111");
            _canvas = _robot.FindElementById("_canvas");
            Console.WriteLine("hi 2222");
            _flowLayoutPanel = _robot.FindElementById("_flowLayoutPanel");
            Console.WriteLine("hi 3333");
        }

        // teardown
        [TestCleanup()]
        public void Cleanup()
        {
            _robot.CleanUp();
        }

        // move pointer to point
        public Interaction MovePointerToPoint(PointerInputDevice device, Point point)
        {
            var size = _canvas.Size;
            Console.WriteLine("canvasSize" + size);
            Console.WriteLine("x " + ((int)point.X - size.Width / 2) + " y " + ((int)point.Y - size.Height / 2));
            return device.CreatePointerMove(_canvas, (int)point.X - size.Width / 2, (int)point.Y - size.Height / 2, TimeSpan.Zero);
        }

        // draw shape
        public void DrawShape(string shapeType, Point point1, Point point2)
        {
            _robot.ClickButton(shapeType);
            Console.WriteLine("point1.X = " + point1.X + " point1.Y = " + point1.Y);
            Console.WriteLine("point2.X = " + point2.X + " point2.Y = " + point2.Y);
            ActionBuilder actionBuilder = new ActionBuilder();
            PointerInputDevice pointer = new PointerInputDevice(PointerKind.Pen);
            actionBuilder
                .AddAction(MovePointerToPoint(pointer, point1))
                .AddAction(pointer.CreatePointerDown(MouseButton.Left))
                .AddAction(MovePointerToPoint(pointer, point2))
                .AddAction(pointer.CreatePointerUp(MouseButton.Left));
            _robot.PerformAction(actionBuilder.ToActionSequenceList());
        }

        // info transfer
        public string transferInformation(Point point1, Point point2)
        {
            return "(" + point1.X + ", " + point1.Y + "), (" + point2.X + ", " + point2.Y + ")";
        }

        // get slide by id
        public IReadOnlyCollection<AppiumWebElement> GetSlideByIdFromFlowLayoutPanel()
        {
            return _flowLayoutPanel.FindElementsByAccessibilityId("slide");
            // return _robot.FindElementById(FLOW_LAYOUT_PANEL1)
            // .FindElementsByClassName("WindowsForms10.Window.8.app.0.141b42a_r8_ad1");
        }

        // test tool strip button checked and draw for line
        [TestMethod()]
        public void LineButtonCheckedAndDrawTest()
        {
            _robot.ClickButton("_lineButton");
            _robot.AssertEnable("_lineButton", true);
            Assert.AreEqual(AccessibleStates.Checked, _robot.GetButtonState("_lineButton") & AccessibleStates.Checked);

            DrawShape("_lineButton", new Point(10, 10), new Point(150, 150));
            string[] expectedData = { "刪除", "線", "(10, 10), (150, 150)" };
            _robot.AssertDataGridViewRowDataBy("_shapeData", 0, expectedData);
        }

        // test tool strip button checked and draw for rectangle
        [TestMethod()]
        public void RectangleButtonCheckedAndDrawTest()
        {
            _robot.ClickButton("_rectangleButton");
            _robot.AssertEnable("_rectangleButton", true);
            Assert.AreEqual(AccessibleStates.Checked, _robot.GetButtonState("_rectangleButton") & AccessibleStates.Checked);

            DrawShape("_rectangleButton", new Point(10, 10), new Point(300, 200));
            string[] expectedData = { "刪除", "矩形", "(10, 10), (300, 200)" };
            _robot.AssertDataGridViewRowDataBy("_shapeData", 0, expectedData);
        }

        // test tool strip button checked and draw for ellipse
        [TestMethod()]
        public void EllipseButtonCheckedAndDrawTest()
        {
            _robot.ClickButton("_ellipseButton");
            _robot.AssertEnable("_ellipseButton", true);
            Assert.AreEqual(AccessibleStates.Checked, _robot.GetButtonState("_ellipseButton") & AccessibleStates.Checked);

            DrawShape("_ellipseButton", new Point(10, 10), new Point(300, 250));
            string[] expectedData = { "刪除", "圓", "(10, 10), (300, 250)" };
            _robot.AssertDataGridViewRowDataBy("_shapeData", 0, expectedData);
        }

        // test move shape
        [TestMethod()]
        public void MoveShapeTest()
        {
            DrawShape("_ellipseButton", new Point(10, 10), new Point(150, 150));
            string[] expectedData1 = { "刪除", "圓", "(10, 10), (150, 150)" };
            _robot.AssertDataGridViewRowDataBy("_shapeData", 0, expectedData1);
            ActionBuilder actionBuilder = new ActionBuilder();
            PointerInputDevice pointer = new PointerInputDevice(PointerKind.Pen);
            actionBuilder
                .AddAction(MovePointerToPoint(pointer, new Point(100, 100)))
                .AddAction(pointer.CreatePointerDown(MouseButton.Left))
                .AddAction(MovePointerToPoint(pointer, new Point(150, 150)))
                .AddAction(pointer.CreatePointerUp(MouseButton.Left));
            _robot.PerformAction(actionBuilder.ToActionSequenceList());

            string[] expectedData2 = { "刪除", "圓", "(60, 60), (200, 200)" };
            _robot.AssertDataGridViewRowDataBy("_shapeData", 0, expectedData2);
        }

        // test resize shape
        [TestMethod()]
        public void ResizeShapeTest()
        {
            DrawShape("_rectangleButton", new Point(10, 10), new Point(300, 250));
            string[] expectedData1 = { "刪除", "矩形", "(10, 10), (300, 250)" };
            _robot.AssertDataGridViewRowDataBy("_shapeData", 0, expectedData1);
            ActionBuilder actionBuilder = new ActionBuilder();
            PointerInputDevice pointer = new PointerInputDevice(PointerKind.Pen);
            actionBuilder
                .AddAction(MovePointerToPoint(pointer, new Point(100, 100)))
                .AddAction(pointer.CreatePointerDown(MouseButton.Left))
                .AddAction(pointer.CreatePointerUp(MouseButton.Left))
                .AddAction(MovePointerToPoint(pointer, new Point(300, 250)))
                .AddAction(pointer.CreatePointerDown(MouseButton.Left))
                .AddAction(MovePointerToPoint(pointer, new Point(200, 200)))
                .AddAction(pointer.CreatePointerUp(MouseButton.Left));
            _robot.PerformAction(actionBuilder.ToActionSequenceList());

            string[] expectedData2 = { "刪除", "矩形", "(10, 10), (200, 200)" };
            _robot.AssertDataGridViewRowDataBy("_shapeData", 0, expectedData2);
        }

        // test delete shape
        [TestMethod()]
        public void DeleteShapeWithKeyTest()
        {
            DrawShape("_lineButton", new Point(10, 10), new Point(300, 250));
            string[] expectedData1 = { "刪除", "線", "(10, 10), (300, 250)" };
            _robot.AssertDataGridViewRowDataBy("_shapeData", 0, expectedData1);
            _robot.AssertDataGridViewRowCountBy("_shapeData", 1);
            ActionBuilder actionBuilder = new ActionBuilder();
            PointerInputDevice pointer = new PointerInputDevice(PointerKind.Pen);
            actionBuilder
                .AddAction(MovePointerToPoint(pointer, new Point(100, 100)))
                .AddAction(pointer.CreatePointerDown(MouseButton.Left))
                .AddAction(pointer.CreatePointerUp(MouseButton.Left));
            _robot.PerformAction(actionBuilder.ToActionSequenceList());
            _robot.PerformDeleteKey();
            _robot.AssertDataGridViewRowCountBy("_shapeData", 0);
        }

        // test datagridview
        [TestMethod()]
        public void DataGridViewCreateShapeTest()
        {
            var canvasSize = _canvas.Size;
            List<string> shapeTypes = new List<string>();
            shapeTypes.Add("圓");
            shapeTypes.Add("矩形");
            shapeTypes.Add("線");

            int i = 0;
            foreach (string type in shapeTypes)
            {
                Point point1 = new Point(_random.Next(0, canvasSize.Width), _random.Next(0, canvasSize.Height));
                Point point2 = new Point(_random.Next((int)point1.X + 1, canvasSize.Width), _random.Next((int)point1.Y + 1, canvasSize.Height));
                _robot.SelectComboBoxValue("_shapeTypeComboBox", type);
                _robot.ClickButton("新增");
                _robot.FindElementById("_leftTextBox").SendKeys(point1.X.ToString());
                _robot.FindElementById("_topTextBox").SendKeys(point1.Y.ToString());
                _robot.FindElementById("_rightTextBox").SendKeys(point2.X.ToString());
                _robot.FindElementById("_bottomTextBox").SendKeys(point2.Y.ToString());
                _robot.ClickButton("OK");
                string[] expectedData = { "刪除", type, transferInformation(point1, point2) };
                _robot.AssertDataGridViewRowDataBy("_shapeData", i, expectedData);
                i++;
            }
        }

        // test datagridview
        [TestMethod()]
        public void DataGridViewDeleteShapeTest()
        {
            var canvasSize = _canvas.Size;
            List<string> shapeTypes = new List<string>();
            shapeTypes.Add("圓");
            shapeTypes.Add("矩形");
            shapeTypes.Add("線");

            foreach (string type in shapeTypes)
            {
                Point point1 = new Point(_random.Next(0, canvasSize.Width), _random.Next(0, canvasSize.Height));
                Point point2 = new Point(_random.Next((int)point1.X + 1, canvasSize.Width), _random.Next((int)point1.Y + 1, canvasSize.Height));
                _robot.SelectComboBoxValue("_shapeTypeComboBox", type);
                _robot.ClickButton("新增");
                _robot.FindElementById("_leftTextBox").SendKeys(point1.X.ToString());
                _robot.FindElementById("_topTextBox").SendKeys(point1.Y.ToString());
                _robot.FindElementById("_rightTextBox").SendKeys(point2.X.ToString());
                _robot.FindElementById("_bottomTextBox").SendKeys(point2.Y.ToString());
                _robot.ClickButton("OK");
                string[] expectedData = { "刪除", type, transferInformation(point1, point2) };
                _robot.AssertDataGridViewRowDataBy("_shapeData", 0, expectedData);

                _robot.ClickDataGridViewCellBy("_shapeData", 0, "刪除");
                _robot.AssertDataGridViewRowCountBy("_shapeData", 0);
            }
        }

        // test undo redo draw
        [TestMethod()]
        public void UndoRedoDrawTest()
        {
            _robot.AssertEnable("_undoButton", false);
            _robot.AssertEnable("_redoButton", false);
            DrawShape("_lineButton", new Point(10, 10), new Point(150, 150));
            string[] expectedData = { "刪除", "線", "(10, 10), (150, 150)" };
            _robot.AssertDataGridViewRowDataBy("_shapeData", 0, expectedData);
            _robot.AssertDataGridViewRowCountBy("_shapeData", 1);

            _robot.AssertEnable("_undoButton", true);
            _robot.AssertEnable("_redoButton", false);
            _robot.ClickButton("_undoButton");
            _robot.AssertDataGridViewRowCountBy("_shapeData", 0);

            _robot.AssertEnable("_undoButton", false);
            _robot.AssertEnable("_redoButton", true);
            _robot.ClickButton("_redoButton");
            _robot.AssertDataGridViewRowDataBy("_shapeData", 0, expectedData);
            _robot.AssertDataGridViewRowCountBy("_shapeData", 1);

            _robot.AssertEnable("_undoButton", true);
            _robot.AssertEnable("_redoButton", false);
        }

        // test undo redo resize
        [TestMethod()]
        public void UndoRedoResizeTest()
        {
            DrawShape("_rectangleButton", new Point(10, 10), new Point(300, 250));
            string[] expectedData1 = { "刪除", "矩形", "(10, 10), (300, 250)" };
            _robot.AssertDataGridViewRowDataBy("_shapeData", 0, expectedData1);
            ActionBuilder actionBuilder = new ActionBuilder();
            PointerInputDevice pointer = new PointerInputDevice(PointerKind.Pen);
            actionBuilder
                .AddAction(MovePointerToPoint(pointer, new Point(100, 100)))
                .AddAction(pointer.CreatePointerDown(MouseButton.Left))
                .AddAction(pointer.CreatePointerUp(MouseButton.Left))
                .AddAction(MovePointerToPoint(pointer, new Point(300, 250)))
                .AddAction(pointer.CreatePointerDown(MouseButton.Left))
                .AddAction(MovePointerToPoint(pointer, new Point(200, 200)))
                .AddAction(pointer.CreatePointerUp(MouseButton.Left));
            _robot.PerformAction(actionBuilder.ToActionSequenceList());

            string[] expectedData2 = { "刪除", "矩形", "(10, 10), (200, 200)" };
            _robot.AssertDataGridViewRowDataBy("_shapeData", 0, expectedData2);

            _robot.AssertEnable("_undoButton", true);
            _robot.AssertEnable("_redoButton", false);
            _robot.ClickButton("_undoButton");
            _robot.AssertDataGridViewRowDataBy("_shapeData", 0, expectedData1);

            _robot.AssertEnable("_undoButton", true);
            _robot.AssertEnable("_redoButton", true);
            _robot.ClickButton("_redoButton");
            _robot.AssertDataGridViewRowDataBy("_shapeData", 0, expectedData2);

            _robot.AssertEnable("_undoButton", true);
            _robot.AssertEnable("_redoButton", false);
        }

        // test undo redo move
        [TestMethod()]
        public void UndoRedoMoveTest()
        {
            DrawShape("_ellipseButton", new Point(10, 10), new Point(150, 150));
            string[] expectedData1 = { "刪除", "圓", "(10, 10), (150, 150)" };
            _robot.AssertDataGridViewRowDataBy("_shapeData", 0, expectedData1);
            ActionBuilder actionBuilder = new ActionBuilder();
            PointerInputDevice pointer = new PointerInputDevice(PointerKind.Pen);
            actionBuilder
                .AddAction(MovePointerToPoint(pointer, new Point(100, 100)))
                .AddAction(pointer.CreatePointerDown(MouseButton.Left))
                .AddAction(MovePointerToPoint(pointer, new Point(150, 150)))
                .AddAction(pointer.CreatePointerUp(MouseButton.Left));
            _robot.PerformAction(actionBuilder.ToActionSequenceList());

            string[] expectedData2 = { "刪除", "圓", "(60, 60), (200, 200)" };
            _robot.AssertDataGridViewRowDataBy("_shapeData", 0, expectedData2);

            _robot.AssertEnable("_undoButton", true);
            _robot.AssertEnable("_redoButton", false);
            _robot.ClickButton("_undoButton");
            _robot.AssertDataGridViewRowDataBy("_shapeData", 0, expectedData1);

            _robot.AssertEnable("_undoButton", true);
            _robot.AssertEnable("_redoButton", true);
            _robot.ClickButton("_redoButton");
            _robot.AssertDataGridViewRowDataBy("_shapeData", 0, expectedData2);

            _robot.AssertEnable("_undoButton", true);
            _robot.AssertEnable("_redoButton", false);
        }

        // test undo redo delete shape with key
        [TestMethod()]
        public void UndoRedoDeleteShapeWithKeyTest()
        {
            DrawShape("_lineButton", new Point(10, 10), new Point(300, 250));
            string[] expectedData1 = { "刪除", "線", "(10, 10), (300, 250)" };
            _robot.AssertDataGridViewRowDataBy("_shapeData", 0, expectedData1);
            _robot.AssertDataGridViewRowCountBy("_shapeData", 1);
            ActionBuilder actionBuilder = new ActionBuilder();
            PointerInputDevice pointer = new PointerInputDevice(PointerKind.Pen);
            actionBuilder
                .AddAction(MovePointerToPoint(pointer, new Point(100, 100)))
                .AddAction(pointer.CreatePointerDown(MouseButton.Left))
                .AddAction(pointer.CreatePointerUp(MouseButton.Left));
            _robot.PerformAction(actionBuilder.ToActionSequenceList());
            _robot.PerformDeleteKey();
            _robot.AssertDataGridViewRowCountBy("_shapeData", 0);

            _robot.AssertEnable("_undoButton", true);
            _robot.AssertEnable("_redoButton", false);
            _robot.ClickButton("_undoButton");
            _robot.AssertDataGridViewRowDataBy("_shapeData", 0, expectedData1);
            _robot.AssertDataGridViewRowCountBy("_shapeData", 1);

            _robot.AssertEnable("_undoButton", true);
            _robot.AssertEnable("_redoButton", true);
            _robot.ClickButton("_redoButton");
            _robot.AssertDataGridViewRowCountBy("_shapeData", 0);

            _robot.AssertEnable("_undoButton", true);
            _robot.AssertEnable("_redoButton", false);
        }

        // test undo redo datagridview
        [TestMethod()]
        public void UndoRedoDataGridView()
        {
            var canvasSize = _canvas.Size;
            List<string> shapeTypes = new List<string>();
            shapeTypes.Add("圓");
            shapeTypes.Add("矩形");
            shapeTypes.Add("線");

            int i = 0;
            foreach (string type in shapeTypes)
            {
                Point point1 = new Point(_random.Next(0, canvasSize.Width), _random.Next(0, canvasSize.Height));
                Point point2 = new Point(_random.Next((int)point1.X + 1, canvasSize.Width), _random.Next((int)point1.Y + 1, canvasSize.Height));
                _robot.SelectComboBoxValue("_shapeTypeComboBox", type);
                _robot.ClickButton("新增");
                _robot.FindElementById("_leftTextBox").SendKeys(point1.X.ToString());
                _robot.FindElementById("_topTextBox").SendKeys(point1.Y.ToString());
                _robot.FindElementById("_rightTextBox").SendKeys(point2.X.ToString());
                _robot.FindElementById("_bottomTextBox").SendKeys(point2.Y.ToString());
                _robot.ClickButton("OK");
                string[] expectedData = { "刪除", type, transferInformation(point1, point2) };
                _robot.AssertDataGridViewRowDataBy("_shapeData", i, expectedData);

                _robot.AssertEnable("_undoButton", true);
                _robot.AssertEnable("_redoButton", false);
                _robot.ClickButton("_undoButton");
                _robot.AssertDataGridViewRowCountBy("_shapeData", i);

                _robot.AssertEnable("_redoButton", true);
                _robot.ClickButton("_redoButton");
                _robot.AssertDataGridViewRowDataBy("_shapeData", i, expectedData);

                i++;
            }
        }

        // test undo redo datagridview delete
        [TestMethod()]
        public void UndoRedoDataGridViewDeleteShapeTest()
        {
            var canvasSize = _canvas.Size;
            List<string> shapeTypes = new List<string>();
            shapeTypes.Add("圓");
            shapeTypes.Add("矩形");
            shapeTypes.Add("線");

            foreach (string type in shapeTypes)
            {
                Point point1 = new Point(_random.Next(0, canvasSize.Width), _random.Next(0, canvasSize.Height));
                Point point2 = new Point(_random.Next((int)point1.X + 1, canvasSize.Width), _random.Next((int)point1.Y + 1, canvasSize.Height));
                _robot.SelectComboBoxValue("_shapeTypeComboBox", type);
                _robot.ClickButton("新增");
                _robot.FindElementById("_leftTextBox").SendKeys(point1.X.ToString());
                _robot.FindElementById("_topTextBox").SendKeys(point1.Y.ToString());
                _robot.FindElementById("_rightTextBox").SendKeys(point2.X.ToString());
                _robot.FindElementById("_bottomTextBox").SendKeys(point2.Y.ToString());
                _robot.ClickButton("OK");
                string[] expectedData = { "刪除", type, transferInformation(point1, point2) };
                _robot.AssertDataGridViewRowDataBy("_shapeData", 0, expectedData);

                _robot.ClickDataGridViewCellBy("_shapeData", 0, "刪除");
                _robot.AssertDataGridViewRowCountBy("_shapeData", 0);

                _robot.AssertEnable("_undoButton", true);
                _robot.AssertEnable("_redoButton", false);
                _robot.ClickButton("_undoButton");
                _robot.AssertDataGridViewRowDataBy("_shapeData", 0, expectedData);

                _robot.AssertEnable("_redoButton", true);
                _robot.ClickButton("_redoButton");
                _robot.AssertDataGridViewRowCountBy("_shapeData", 0);
            }
        }

        // test add page
        [TestMethod()]
        public void AddPageTest()
        {
            _robot.ClickButton("_addPageButton");
            Assert.AreEqual(2, GetSlideByIdFromFlowLayoutPanel().Count);
        }

        // test delete page
        [TestMethod()]
        public void DeletePageTest()
        {
            _robot.ClickButton("_addPageButton");
            Assert.AreEqual(2, GetSlideByIdFromFlowLayoutPanel().Count);

            _robot.PerformDeleteKey();
            Assert.AreEqual(1, GetSlideByIdFromFlowLayoutPanel().Count);
        }

        // test undo redo add page
        public void UndoRedoAddPageTest()
        {
            _robot.ClickButton("_addPageButton");
            Assert.AreEqual(2, GetSlideByIdFromFlowLayoutPanel().Count);

            _robot.AssertEnable("_undoButton", true);
            _robot.AssertEnable("_redoButton", false);
            _robot.ClickButton("_undoButton");

            Assert.AreEqual(1, GetSlideByIdFromFlowLayoutPanel().Count);

            _robot.AssertEnable("_undoButton", false);
            _robot.AssertEnable("_redoButton", true);
            _robot.ClickButton("_redoButton");
            Assert.AreEqual(2, GetSlideByIdFromFlowLayoutPanel().Count);
        }

        // test undo redo delete page
        public void UndoRedoDeletePageTest()
        {
            _robot.ClickButton("_addPageButton");
            Assert.AreEqual(2, GetSlideByIdFromFlowLayoutPanel().Count);

            _robot.PerformDeleteKey();
            Assert.AreEqual(1, GetSlideByIdFromFlowLayoutPanel().Count);

            _robot.AssertEnable("_undoButton", true);
            _robot.AssertEnable("_redoButton", false);
            _robot.ClickButton("_undoButton");

            Assert.AreEqual(2, GetSlideByIdFromFlowLayoutPanel().Count);

            _robot.AssertEnable("_undoButton", false);
            _robot.AssertEnable("_redoButton", true);
            _robot.ClickButton("_redoButton");
            Assert.AreEqual(1, GetSlideByIdFromFlowLayoutPanel().Count);
        }

        // test dynamic layout
        [TestMethod()]
        public void DynamicLayoutTest()
        {
            var slides = GetSlideByIdFromFlowLayoutPanel();
            _robot.ClickButton("_addPageButton");
            foreach (var aSlide in slides)
            {
                //Console.WriteLine("slide ratio " + (double)aSlide.Size.Height / (double)aSlide.Size.Width);
                //Console.WriteLine("expected ratio " + Constant.Constant.PANEL_RATIO);
                Assert.IsTrue(Math.Abs((double)aSlide.Size.Height / (double)aSlide.Size.Width - Constant.Constant.PANEL_RATIO) < 0.01);
                _robot.Sleep(1);
            }
            _robot.GetManage().Window.Maximize();
            _robot.ClickButton("_addPageButton");
            foreach (var aSlide in slides)
            {
                Assert.IsTrue(Math.Abs((double)aSlide.Size.Height / (double)aSlide.Size.Width - Constant.Constant.PANEL_RATIO) < 0.01);
                _robot.Sleep(1);
            }
            _robot.GetManage().Window.Size = new System.Drawing.Size(800, 700);
            _robot.ClickButton("_addPageButton");
            foreach (var aSlide in slides)
            {
                Assert.IsTrue(Math.Abs((double)aSlide.Size.Height / (double)aSlide.Size.Width - Constant.Constant.PANEL_RATIO) < 0.01);
                _robot.Sleep(1);
            }
        }
    }
}
