﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework
{
    public class Shapes
    {
        private readonly List<Shape> _shapes;
        private readonly ShapeFactory _shapeFactory;

        public Shapes()
        {
            _shapes = new List<Shape>();
            _shapeFactory = new ShapeFactory();
        }

        // add new shape to _shapes
        public void AddNewShape(string shapeType)
        {
            _shapes.Add(_shapeFactory.CreateShape(shapeType));
        }

        // delete selected shape from _shapes
        public void DeleteSelectedShape(int index)
        {
            _shapes.RemoveAt(index);
        }

        // get shape name by index
        public string GetShapeNameByIndex(int index)
        {
            return _shapes[index].GetShapeName();
        }
        
        // get shape info by index
        public string GetShapeInfoByIndex(int index)
        {
            return _shapes[index].GetInfo();
        }

        // get new shape name
        public string GetNewShapeName()
        {
            return GetShapeNameByIndex(_shapes.Count - 1);
        }

        // get new shape info
        public string GetNewShapeInfo()
        {
            return GetShapeInfoByIndex(_shapes.Count - 1);
        }
    }
}