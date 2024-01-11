using Homework.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
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

            _robot.Sleep(2);
            _canvas = _robot.FindElementById("_canvas");
            _flowLayoutPanel = _robot.FindElementById("_flowLayoutPanel");
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
            Console.WriteLine("In Move Pointer To");
            Console.WriteLine("canvasSize" + size);
            Console.WriteLine("X: " + ((int)point.X - size.Width / 2) + " Y: " + ((int)point.Y - size.Height / 2));
            return device.CreatePointerMove(_canvas, (int)point.X - size.Width / 2, (int)point.Y - size.Height / 2, TimeSpan.Zero);
        }

        // draw shape
        public void DrawShape(string shapeType, Point topLeftPoint, Point bottomRightPoint)
        {
            _robot.ClickButton(shapeType);
            Console.WriteLine("topLeftPoint.X = " + topLeftPoint.X + " topLeftPoint.Y = " + topLeftPoint.Y);
            ActionBuilder actionBuilder = new ActionBuilder();
            PointerInputDevice pointer = new PointerInputDevice(PointerKind.Pen);
            actionBuilder
                .AddAction(MovePointerToPoint(pointer, topLeftPoint))
                .AddAction(pointer.CreatePointerDown(MouseButton.Left))
                .AddAction(MovePointerToPoint(pointer, bottomRightPoint))
                .AddAction(pointer.CreatePointerUp(MouseButton.Left));
            _robot.PerformAction(actionBuilder.ToActionSequenceList());
        }

        // test tool strip button checked and draw for line
        [TestMethod]
        public void TestToolStripButtonCheckedAndDrawForLine()
        {
            _robot.ClickButton("_lineButton");
            _robot.AssertEnable("_lineButton", true);
            Assert.AreEqual(AccessibleStates.Checked, _robot.GetButtonState("_lineButton") & AccessibleStates.Checked);

            DrawShape("_lineButton", new Point(10, 10), new Point(150, 150));
            string[] expectedData = { "刪除", "線", "(10, 10), (150, 150)" };
            _robot.AssertDataGridViewRowDataBy("_shapeData", 0, expectedData);
        }

        // test tool strip button checked and draw for rectangle
        [TestMethod]
        public void TestToolStripButtonCheckedAndDrawForRectangle()
        {
            _robot.ClickButton("_rectangleButton");
            _robot.AssertEnable("_rectangleButton", true);
            Assert.AreEqual(AccessibleStates.Checked, _robot.GetButtonState("_rectangleButton") & AccessibleStates.Checked);

            DrawShape("_rectangleButton", new Point(10, 10), new Point(200, 200));
            string[] expectedData = { "刪除", "矩形", "(10, 10), (200, 200)" };
            _robot.AssertDataGridViewRowDataBy("_shapeData", 0, expectedData);
        }

        // test tool strip button checked and draw for ellipse
        [TestMethod]
        public void TestToolStripButtonCheckedAndDrawForEllipse()
        {
            _robot.ClickButton("_ellipseButton");
            _robot.AssertEnable("_ellipseButton", true);
            Assert.AreEqual(AccessibleStates.Checked, _robot.GetButtonState("_ellipseButton") & AccessibleStates.Checked);

            DrawShape("_ellipseButton", new Point(10, 10), new Point(160, 250));
            string[] expectedData = { "刪除", "圓", "(10, 10), (160, 250)" };
            _robot.AssertDataGridViewRowDataBy("_shapeData", 0, expectedData);
        }
    }
}
