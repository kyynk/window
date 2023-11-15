﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework
{
    public class Shapes
    {
        private readonly BindingList<Shape> _shapeList;
        private readonly ShapeFactory _shapeFactory;
        private Shape _selectedShape;

        public Shapes()
        {
            _shapeList = new BindingList<Shape>();
            _shapeFactory = new ShapeFactory();
            _selectedShape = null;
        }

        // get shapes
        public BindingList<Shape> ShapeList
        {
            get
            {
                return _shapeList;
            }
        }

        // add new shape to _shapes
        public void AddNewShapeByDrawing(string shapeType, Point point1, Point point2)
        {
            _shapeList.Add(_shapeFactory.AddDrawingShape(shapeType, point1, point2));
        }

        // add new shape to _shapes
        public void AddNewShapeByRandom(string shapeType)
        {
            _shapeList.Add(_shapeFactory.CreateShape(shapeType));
        }

        // delete selected shape from _shapes
        public void DeleteShapeByIndex(int index)
        {
            if (_selectedShape == _shapeList[index])
                _selectedShape = null;
            _shapeList.RemoveAt(index);
        }

        // check point in shape
        public bool CheckSelect(double pointX, double pointY)
        {
            foreach (Shape aShape in _shapeList)
            {
                if (_selectedShape != null)
                {
                    _selectedShape.isSelected = false;
                }
                _selectedShape = null;
                if (aShape.CheckSelect(pointX, pointY))
                {
                    aShape.isSelected = true;
                    _selectedShape = aShape;
                    return true;
                }
            }
            return false;
        }

        // draw hint select shape
        public Shape GetSelectedShape()
        {
            return _selectedShape;
        }

        // move select shape
        public void MoveSelectedShape(double offsetX, double offsetY)
        {
            if (_selectedShape != null)
                _selectedShape.Move(offsetX, offsetY);
        }

        // delete select shape
        public void DeleteSelectedShape()
        {
            _shapeList.Remove(_selectedShape);
            _selectedShape = null;
        }
    }
}