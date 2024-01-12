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

            _robot.Sleep(3);
            Console.WriteLine("hi 1111");
            _canvas = _robot.FindElementByAccessibilityId("_canvas");
            Console.WriteLine("hi 2222");
            _flowLayoutPanel = _robot.FindElementByAccessibilityId("_flowLayoutPanel");
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
            _robot.PerformActions(actionBuilder.ToActionSequenceList());
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

        // test move shape (all shape in redo undo)
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
            _robot.PerformActions(actionBuilder.ToActionSequenceList());

            string[] expectedData2 = { "刪除", "圓", "(60, 60), (200, 200)" };
            _robot.AssertDataGridViewRowDataBy("_shapeData", 0, expectedData2);
        }

        // test resize shape (all shape in redo undo)
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
            _robot.PerformActions(actionBuilder.ToActionSequenceList());

            string[] expectedData2 = { "刪除", "矩形", "(10, 10), (200, 200)" };
            _robot.AssertDataGridViewRowDataBy("_shapeData", 0, expectedData2);
        }

        // test delete shape (all shape in redo undo)
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
            _robot.PerformActions(actionBuilder.ToActionSequenceList());
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
                _robot.FindElementByAccessibilityId("_leftTextBox").SendKeys(point1.X.ToString());
                _robot.FindElementByAccessibilityId("_topTextBox").SendKeys(point1.Y.ToString());
                _robot.FindElementByAccessibilityId("_rightTextBox").SendKeys(point2.X.ToString());
                _robot.FindElementByAccessibilityId("_bottomTextBox").SendKeys(point2.Y.ToString());
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
                _robot.FindElementByAccessibilityId("_leftTextBox").SendKeys(point1.X.ToString());
                _robot.FindElementByAccessibilityId("_topTextBox").SendKeys(point1.Y.ToString());
                _robot.FindElementByAccessibilityId("_rightTextBox").SendKeys(point2.X.ToString());
                _robot.FindElementByAccessibilityId("_bottomTextBox").SendKeys(point2.Y.ToString());
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
            List<string> shapeTypes = new List<string>();
            shapeTypes.Add("圓");
            shapeTypes.Add("矩形");
            shapeTypes.Add("線");
            List<string> buttonName = new List<string>();
            buttonName.Add("_ellipseButton");
            buttonName.Add("_rectangleButton");
            buttonName.Add("_lineButton");

            _robot.AssertEnable("_undoButton", false);
            _robot.AssertEnable("_redoButton", false);

            for (int i = 0; i < 3; i++)
            {
                DrawShape(buttonName[i], new Point(10, 10), new Point(150, 150));
                string[] expectedData = { "刪除", shapeTypes[i], "(10, 10), (150, 150)" };
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

                _robot.ClickButton("_undoButton");
            }
        }

        // test undo redo resize
        [TestMethod()]
        public void UndoRedoResizeTest()
        {
            List<string> shapeTypes = new List<string>();
            shapeTypes.Add("圓");
            shapeTypes.Add("矩形");
            shapeTypes.Add("線");
            List<string> buttonName = new List<string>();
            buttonName.Add("_ellipseButton");
            buttonName.Add("_rectangleButton");
            buttonName.Add("_lineButton");

            for (int i = 0; i < 3; i++)
            {
                DrawShape(buttonName[i], new Point(10, 10), new Point(300, 250));
                string[] expectedData1 = { "刪除", shapeTypes[i], "(10, 10), (300, 250)" };
                _robot.AssertDataGridViewRowDataBy("_shapeData", 0, expectedData1);
                _robot.AssertDataGridViewRowCountBy("_shapeData", 1);
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
                _robot.PerformActions(actionBuilder.ToActionSequenceList());

                string[] expectedData2 = { "刪除", shapeTypes[i], "(10, 10), (200, 200)" };
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

                _robot.ClickButton("_undoButton");
                _robot.ClickButton("_undoButton");
            }
        }

        // test undo redo move
        [TestMethod()]
        public void UndoRedoMoveTest()
        {
            List<string> shapeTypes = new List<string>();
            shapeTypes.Add("圓");
            shapeTypes.Add("矩形");
            shapeTypes.Add("線");
            List<string> buttonName = new List<string>();
            buttonName.Add("_ellipseButton");
            buttonName.Add("_rectangleButton");
            buttonName.Add("_lineButton");

            for (int i = 0; i < 3; i++)
            {
                DrawShape(buttonName[i], new Point(10, 10), new Point(300, 250));
                string[] expectedData1 = { "刪除", shapeTypes[i], "(10, 10), (300, 250)" };
                _robot.AssertDataGridViewRowDataBy("_shapeData", 0, expectedData1);
                _robot.AssertDataGridViewRowCountBy("_shapeData", 1);
                ActionBuilder actionBuilder = new ActionBuilder();
                PointerInputDevice pointer = new PointerInputDevice(PointerKind.Pen);
                actionBuilder
                    .AddAction(MovePointerToPoint(pointer, new Point(100, 100)))
                    .AddAction(pointer.CreatePointerDown(MouseButton.Left))
                    .AddAction(MovePointerToPoint(pointer, new Point(150, 150)))
                    .AddAction(pointer.CreatePointerUp(MouseButton.Left));
                _robot.PerformActions(actionBuilder.ToActionSequenceList());

                string[] expectedData2 = { "刪除", shapeTypes[i], "(60, 60), (350, 300)" };
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

                _robot.ClickButton("_undoButton");
                _robot.ClickButton("_undoButton");
            }
        }

        // test undo redo delete shape with key
        [TestMethod()]
        public void UndoRedoDeleteShapeWithKeyTest()
        {
            List<string> shapeTypes = new List<string>();
            shapeTypes.Add("圓");
            shapeTypes.Add("矩形");
            shapeTypes.Add("線");
            List<string> buttonName = new List<string>();
            buttonName.Add("_ellipseButton");
            buttonName.Add("_rectangleButton");
            buttonName.Add("_lineButton");

            for (int i = 0; i < 3; i++)
            {
                DrawShape(buttonName[i], new Point(10, 10), new Point(300, 250));
                string[] expectedData1 = { "刪除", shapeTypes[i], "(10, 10), (300, 250)" };
                _robot.AssertDataGridViewRowDataBy("_shapeData", 0, expectedData1);
                _robot.AssertDataGridViewRowCountBy("_shapeData", 1);
                ActionBuilder actionBuilder = new ActionBuilder();
                PointerInputDevice pointer = new PointerInputDevice(PointerKind.Pen);
                actionBuilder
                    .AddAction(MovePointerToPoint(pointer, new Point(100, 100)))
                    .AddAction(pointer.CreatePointerDown(MouseButton.Left))
                    .AddAction(pointer.CreatePointerUp(MouseButton.Left));
                _robot.PerformActions(actionBuilder.ToActionSequenceList());
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

                _robot.ClickButton("_undoButton");
                _robot.ClickButton("_undoButton");
            }
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
                _robot.FindElementByAccessibilityId("_leftTextBox").SendKeys(point1.X.ToString());
                _robot.FindElementByAccessibilityId("_topTextBox").SendKeys(point1.Y.ToString());
                _robot.FindElementByAccessibilityId("_rightTextBox").SendKeys(point2.X.ToString());
                _robot.FindElementByAccessibilityId("_bottomTextBox").SendKeys(point2.Y.ToString());
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
                _robot.FindElementByAccessibilityId("_leftTextBox").SendKeys(point1.X.ToString());
                _robot.FindElementByAccessibilityId("_topTextBox").SendKeys(point1.Y.ToString());
                _robot.FindElementByAccessibilityId("_rightTextBox").SendKeys(point2.X.ToString());
                _robot.FindElementByAccessibilityId("_bottomTextBox").SendKeys(point2.Y.ToString());
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

        // test save and load
        [TestMethod()]
        public void SaveAndLoadTest()
        {
            DrawShape("_ellipseButton", new Point(50, 50), new Point(150, 150));
            DrawShape("_rectangleButton", new Point(50, 50), new Point(150, 150));
            DrawShape("_lineButton", new Point(50, 50), new Point(150, 150));
            DrawShape("_lineButton", new Point(50, 150), new Point(150, 50));
            string[] expectedData1 = { "刪除", "圓", transferInformation(new Point(50, 50), new Point(150, 150)) };
            string[] expectedData2 = { "刪除", "矩形", transferInformation(new Point(50, 50), new Point(150, 150)) };
            string[] expectedData3 = { "刪除", "線", transferInformation(new Point(50, 50), new Point(150, 150)) };
            string[] expectedData4 = { "刪除", "線", transferInformation(new Point(50, 150), new Point(150, 50)) };
            _robot.AssertDataGridViewRowDataBy("_shapeData", 0, expectedData1);
            _robot.AssertDataGridViewRowDataBy("_shapeData", 1, expectedData2);
            _robot.AssertDataGridViewRowDataBy("_shapeData", 2, expectedData3);
            _robot.AssertDataGridViewRowDataBy("_shapeData", 3, expectedData4);
            _robot.ClickButton("_saveButton");
            _robot.ClickButton("Cancel");
            _robot.AssertEnable("_saveButton", true);
            _robot.ClickButton("_saveButton");
            _robot.ClickButton("OK");
            _robot.AssertEnable("_saveButton", false);

            DrawShape("_rectangleButton", new Point(200, 50), new Point(150, 150));
            DrawShape("_ellipseButton", new Point(200, 50), new Point(150, 150));

            ActionBuilder actionBuilder = new ActionBuilder();
            PointerInputDevice pointer = new PointerInputDevice(PointerKind.Pen);
            actionBuilder
                .AddAction(MovePointerToPoint(pointer, new Point(100, 100)))
                .AddAction(pointer.CreatePointerDown(MouseButton.Left))
                .AddAction(pointer.CreatePointerUp(MouseButton.Left));
            _robot.PerformActions(actionBuilder.ToActionSequenceList());
            _robot.PerformDeleteKey();

            _robot.Sleep(10 + 5);

            _robot.AssertEnable("_saveButton", true);
            _robot.ClickButton("_loadButton");
            _robot.ClickButton("OK");
            _robot.Sleep(10);
            _robot.AssertDataGridViewRowDataBy("_shapeData", 0, expectedData1);
            _robot.AssertDataGridViewRowDataBy("_shapeData", 1, expectedData2);
            _robot.AssertDataGridViewRowDataBy("_shapeData", 2, expectedData3);
            _robot.AssertDataGridViewRowDataBy("_shapeData", 3, expectedData4);
        }

        // test all
        [TestMethod()]
        public void AllTest()
        {
            DrawFirstGraph();
            TestFirstGraph();
            _robot.ClickButton("_addPageButton");
            DrawSecondGraph();
            TestSecondGraph();
            _robot.ClickButton("_saveButton");
            _robot.ClickButton("OK");
            _robot.Sleep(1);

            _robot.PerformDeleteKey();
            _robot.ClickButton("_addPageButton");
            DrawThirdGraph();
            TestThirdGraph();
            
            _robot.Sleep(10);
            _robot.ClickButton("_loadButton");
            _robot.ClickButton("OK");
            _robot.Sleep(10);
            TestFirstGraph();
            _robot.PerformDeleteKey();
            TestSecondGraph();
        }

        // first graph
        public void DrawFirstGraph()
        {
            DrawShape("_ellipseButton", new Point(33, 36), new Point(50, 50));
            ResizeShape(new Point(42, 42), new Point(50, 50), new Point(91, 86));
            ClearSelect();
            InputShape("線", new Point(61, 88), new Point(63, 146));
            DrawShape("_rectangleButton", new Point(287, 99), new Point(361, 157));
            DrawShape("_lineButton", new Point(287, 99), new Point(324, 66));
            InputShape("線", new Point(326, 66), new Point(360, 98));
            DrawShape("_lineButton", new Point(29, 172), new Point(65, 146));
            DrawShape("_lineButton", new Point(62, 148), new Point(93, 168));
            DrawShape("_lineButton", new Point(26, 108), new Point(93, 115));
        }

        // test first graph
        public void TestFirstGraph()
        {
            string[] expectedData1 = { "刪除", "圓", transferInformation(new Point(33, 36), new Point(91, 86)) };
            string[] expectedData2 = { "刪除", "線", transferInformation(new Point(61, 88), new Point(63, 146)) };
            string[] expectedData3 = { "刪除", "矩形", transferInformation(new Point(287, 99), new Point(361, 157)) };
            string[] expectedData4 = { "刪除", "線", transferInformation(new Point(287, 99), new Point(324, 66)) };
            string[] expectedData5 = { "刪除", "線", transferInformation(new Point(326, 66), new Point(360, 98)) };
            string[] expectedData6 = { "刪除", "線", transferInformation(new Point(29, 172), new Point(65, 146)) };
            string[] expectedData7 = { "刪除", "線", transferInformation(new Point(62, 148), new Point(93, 168)) };
            string[] expectedData8 = { "刪除", "線", transferInformation(new Point(26, 108), new Point(93, 115)) };
            _robot.AssertDataGridViewRowDataBy("_shapeData", 0, expectedData1);
            _robot.AssertDataGridViewRowDataBy("_shapeData", 1, expectedData2);
            _robot.AssertDataGridViewRowDataBy("_shapeData", 2, expectedData3);
            _robot.AssertDataGridViewRowDataBy("_shapeData", 3, expectedData4);
            _robot.AssertDataGridViewRowDataBy("_shapeData", 4, expectedData5);
            _robot.AssertDataGridViewRowDataBy("_shapeData", 5, expectedData6);
            _robot.AssertDataGridViewRowDataBy("_shapeData", 6, expectedData7);
            _robot.AssertDataGridViewRowDataBy("_shapeData", 7, expectedData8);
        }

        // clear
        public void ClearSelect()
        {
            ActionBuilder actionBuilder = new ActionBuilder();
            PointerInputDevice pointer = new PointerInputDevice(PointerKind.Pen);
            actionBuilder
                .AddAction(MovePointerToPoint(pointer, new Point(1, 1)))
                .AddAction(pointer.CreatePointerDown(MouseButton.Left))
                .AddAction(pointer.CreatePointerUp(MouseButton.Left));
            _robot.PerformActions(actionBuilder.ToActionSequenceList());
        }

        // resize shape
        public void ResizeShape(Point inside, Point bottomRight, Point target)
        {
            ActionBuilder actionBuilder = new ActionBuilder();
            PointerInputDevice pointer = new PointerInputDevice(PointerKind.Pen);
            actionBuilder
                .AddAction(MovePointerToPoint(pointer, new Point(inside)))
                .AddAction(pointer.CreatePointerDown(MouseButton.Left))
                .AddAction(pointer.CreatePointerUp(MouseButton.Left))
                .AddAction(MovePointerToPoint(pointer, new Point(bottomRight)))
                .AddAction(pointer.CreatePointerDown(MouseButton.Left))
                .AddAction(MovePointerToPoint(pointer, new Point(target)))
                .AddAction(pointer.CreatePointerUp(MouseButton.Left));
            _robot.PerformActions(actionBuilder.ToActionSequenceList());
        }

        // move shape
        public void MoveShape(Point point1, Point point2)
        {
            ActionBuilder actionBuilder = new ActionBuilder();
            PointerInputDevice pointer = new PointerInputDevice(PointerKind.Pen);
            actionBuilder
                .AddAction(MovePointerToPoint(pointer, new Point(point1)))
                .AddAction(pointer.CreatePointerDown(MouseButton.Left))
                .AddAction(MovePointerToPoint(pointer, new Point(point2)))
                .AddAction(pointer.CreatePointerUp(MouseButton.Left));
            _robot.PerformActions(actionBuilder.ToActionSequenceList());
        }

        // delete shape
        public void DeleteShapeWithKey(Point inside)
        {
            ActionBuilder actionBuilder = new ActionBuilder();
            PointerInputDevice pointer = new PointerInputDevice(PointerKind.Pen);
            actionBuilder
                .AddAction(MovePointerToPoint(pointer, new Point(inside)))
                .AddAction(pointer.CreatePointerDown(MouseButton.Left))
                .AddAction(pointer.CreatePointerUp(MouseButton.Left));
            _robot.PerformActions(actionBuilder.ToActionSequenceList());
            _robot.PerformDeleteKey();
        }

        // second graph
        public void DrawSecondGraph()
        {
            InputShape("圓", new Point(36, 56), new Point(200, 91));
            DeleteShapeWithKey(new Point(100, 80));
            ClearSelect();
            InputShape("圓", new Point(36, 56), new Point(87, 91));
            MoveShape(new Point(50, 70), new Point(150, 70));
            ClearSelect();
            InputShape("圓", new Point(224, 90), new Point(226, 153));
            _robot.Sleep(1);
            _robot.ClickButton("_undoButton");
            InputShape("圓", new Point(107, 90), new Point(226, 153));
            DrawShape("_ellipseButton", new Point(71, 153), new Point(281, 226));
            DrawShape("_rectangleButton", new Point(235, 7), new Point(329, 87));
            DrawShape("_ellipseButton", new Point(235, 7), new Point(329, 87));
            DrawShape("_ellipseButton", new Point(305, 7), new Point(329, 87));
            _robot.ClickButton("_undoButton");
            _robot.ClickButton("_undoButton");
            _robot.ClickButton("_redoButton");
            DrawShape("_lineButton", new Point(282, 20), new Point(282, 54));
            DrawShape("_lineButton", new Point(282, 54), new Point(304, 68));
        }

        // test second graph
        public void TestSecondGraph()
        {
            string[] expectedData1 = { "刪除", "圓", transferInformation(new Point(136, 56), new Point(187, 91)) };
            string[] expectedData2 = { "刪除", "圓", transferInformation(new Point(107, 90), new Point(226, 153)) };
            string[] expectedData3 = { "刪除", "圓", transferInformation(new Point(71, 153), new Point(281, 226)) };
            string[] expectedData4 = { "刪除", "矩形", transferInformation(new Point(235, 7), new Point(329, 87)) };
            string[] expectedData5 = { "刪除", "圓", transferInformation(new Point(235, 7), new Point(329, 87)) };
            string[] expectedData6 = { "刪除", "線", transferInformation(new Point(282, 20), new Point(282, 54)) };
            string[] expectedData7 = { "刪除", "線", transferInformation(new Point(282, 54), new Point(304, 68)) };
            _robot.AssertDataGridViewRowDataBy("_shapeData", 0, expectedData1);
            _robot.AssertDataGridViewRowDataBy("_shapeData", 1, expectedData2);
            _robot.AssertDataGridViewRowDataBy("_shapeData", 2, expectedData3);
            _robot.AssertDataGridViewRowDataBy("_shapeData", 3, expectedData4);
            _robot.AssertDataGridViewRowDataBy("_shapeData", 4, expectedData5);
            _robot.AssertDataGridViewRowDataBy("_shapeData", 5, expectedData6);
            _robot.AssertDataGridViewRowDataBy("_shapeData", 6, expectedData7);
        }

        // input shape
        public void InputShape(string name, Point point1 , Point point2)
        {
            _robot.SelectComboBoxValue("_shapeTypeComboBox", name);
            _robot.ClickButton("新增");
            _robot.FindElementByAccessibilityId("_leftTextBox").SendKeys(point1.X.ToString());
            _robot.FindElementByAccessibilityId("_topTextBox").SendKeys(point1.Y.ToString());
            _robot.FindElementByAccessibilityId("_rightTextBox").SendKeys(point2.X.ToString());
            _robot.FindElementByAccessibilityId("_bottomTextBox").SendKeys(point2.Y.ToString());
            _robot.ClickButton("OK");
        }

        // third graph
        public void DrawThirdGraph()
        {
            InputShape("矩形", new Point(15, 67), new Point(212, 87));
            DeleteShapeWithKey(new Point(100, 80));
            ClearSelect();
            InputShape("線", new Point(116, 68), new Point(139, 80));
            ResizeShape(new Point(120, 69), new Point(139, 80), new Point(139, 41));
            ClearSelect();
            InputShape("線", new Point(143, 31), new Point(162, 57));
            MoveShape(new Point(150, 50), new Point(200, 60));
            ClearSelect();
            InputShape("矩形", new Point(115, 67), new Point(212, 187));
            InputShape("線", new Point(139, 41), new Point(191, 42));
            DrawShape("_rectangleButton", new Point(88, 101), new Point(130, 161));
            DrawShape("_ellipseButton", new Point(165, 86), new Point(227, 120));
            DrawShape("_rectangleButton", new Point(116, 187), new Point(143, 215));
            InputShape("矩形", new Point(184, 187), new Point(212, 215));
            InputShape("矩形", new Point(184, 187), new Point(212, 215));
            _robot.ClickDataGridViewCellBy("_shapeData", 7, "刪除");
            InputShape("線", new Point(169, 9), new Point(170, 40));
            DrawShape("_lineButton", new Point(169, 10), new Point(192, 18));
            DrawShape("_lineButton", new Point(170, 30), new Point(191, 20));
            DrawShape("_ellipseButton", new Point(343, 70), new Point(439, 210));
            DrawShape("_rectangleButton", new Point(354, 209), new Point(437, 236));
        }


        // test third graph
        public void TestThirdGraph()
        {
            string[] expectedData1 = { "刪除", "線", transferInformation(new Point(116, 68), new Point(139, 41)) };
            string[] expectedData2 = { "刪除", "線", transferInformation(new Point(193, 41), new Point(212, 67)) };
            string[] expectedData3 = { "刪除", "矩形", transferInformation(new Point(115, 67), new Point(212, 187)) };
            string[] expectedData4 = { "刪除", "線", transferInformation(new Point(139, 41), new Point(191, 42)) };
            string[] expectedData5 = { "刪除", "矩形", transferInformation(new Point(88, 101), new Point(130, 161)) };
            string[] expectedData6 = { "刪除", "圓", transferInformation(new Point(165, 86), new Point(227, 120)) };
            string[] expectedData7 = { "刪除", "矩形", transferInformation(new Point(116, 187), new Point(143, 215)) };
            string[] expectedData8 = { "刪除", "矩形", transferInformation(new Point(184, 187), new Point(212, 215)) };
            string[] expectedData9 = { "刪除", "線", transferInformation(new Point(169, 9), new Point(170, 40)) };
            string[] expectedData10 = { "刪除", "線", transferInformation(new Point(169, 10), new Point(192, 18)) };
            string[] expectedData11 = { "刪除", "線", transferInformation(new Point(170, 30), new Point(191, 20)) };
            string[] expectedData12 = { "刪除", "圓", transferInformation(new Point(343, 70), new Point(439, 210)) };
            string[] expectedData13 = { "刪除", "矩形", transferInformation(new Point(354, 209), new Point(437, 236)) };
            _robot.AssertDataGridViewRowDataBy("_shapeData", 0, expectedData1);
            _robot.AssertDataGridViewRowDataBy("_shapeData", 1, expectedData2);
            _robot.AssertDataGridViewRowDataBy("_shapeData", 2, expectedData3);
            _robot.AssertDataGridViewRowDataBy("_shapeData", 3, expectedData4);
            _robot.AssertDataGridViewRowDataBy("_shapeData", 4, expectedData5);
            _robot.AssertDataGridViewRowDataBy("_shapeData", 5, expectedData6);
            _robot.AssertDataGridViewRowDataBy("_shapeData", 6, expectedData7);
            _robot.AssertDataGridViewRowDataBy("_shapeData", 7, expectedData8);
            _robot.AssertDataGridViewRowDataBy("_shapeData", 8, expectedData9);
            _robot.AssertDataGridViewRowDataBy("_shapeData", 9, expectedData10);
            _robot.AssertDataGridViewRowDataBy("_shapeData", 10, expectedData11);
            _robot.AssertDataGridViewRowDataBy("_shapeData", 11, expectedData12);
            _robot.AssertDataGridViewRowDataBy("_shapeData", 12, expectedData13);
        }
    }
}
