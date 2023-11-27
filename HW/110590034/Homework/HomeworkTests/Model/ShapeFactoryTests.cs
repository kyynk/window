using Homework.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Homework.Model.Tests
{
    [TestClass()]
    public class ShapeFactoryTests
    {
        private ShapeFactory _shapeFactory;
        private PrivateObject _privateFactory;

        // setup
        [TestInitialize()]
        public void Initialize()
        {
            _shapeFactory = new ShapeFactory();
            _privateFactory = new PrivateObject(_shapeFactory);
        }

        // test shape factory
        [TestMethod()]
        public void ShapeFactoryTest()
        {
            _shapeFactory = new ShapeFactory();
            _privateFactory = new PrivateObject(_shapeFactory);
            Assert.IsNotNull((Random)_privateFactory.GetField("_randomNumber"));
        }

        // test add drawing shape
        [TestMethod()]
        public void AddDrawingShapeTest()
        {
            Point point1 = new Point(1, 1);
            Point point2 = new Point(2, 2);
            Assert.IsInstanceOfType(_shapeFactory.AddDrawingShape(Constant.Constant.LINE, point1, point2), typeof(Line));
            Assert.IsInstanceOfType(_shapeFactory.AddDrawingShape(Constant.Constant.RECTANGLE, point1, point2), typeof(Rectangle));
            Assert.IsInstanceOfType(_shapeFactory.AddDrawingShape(Constant.Constant.ELLIPSE, point1, point2), typeof(Ellipse));
            Assert.ThrowsException<ArgumentException>(() => _shapeFactory.AddDrawingShape("Triangle", point1, point2));
        }

        // test create shape
        [TestMethod()]
        public void CreateShapeTest()
        {
            Assert.IsInstanceOfType(_shapeFactory.CreateShape(Constant.Constant.LINE), typeof(Line));
            Assert.IsInstanceOfType(_shapeFactory.CreateShape(Constant.Constant.RECTANGLE), typeof(Rectangle));
            Assert.IsInstanceOfType(_shapeFactory.CreateShape(Constant.Constant.ELLIPSE), typeof(Ellipse));
            Assert.ThrowsException<ArgumentException>(() => _shapeFactory.CreateShape("Triangle"));
        }
    }
}