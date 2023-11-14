﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace Homework
{
    public class PointState : IState
    {
        private Point _point;
        private bool _isSelected;
        private bool _isClicked;

        public PointState()
        {
            _point = new Point(-1, -1);
            _isSelected = false;
            _isClicked = false;
        }

        // mouse down
        public void MouseDown(Point mouse, string mode, ref Shapes shapes, ref ShapeFactory shapeFactory)
        {
            _isClicked = false;
            _isClicked = shapes.CheckSelect(mouse.X, mouse.Y);
            _isSelected = shapes.CheckSelect(mouse.X, mouse.Y);
            _point = mouse;
            if (!_isClicked)
                _isSelected = false;
            //Console.WriteLine("Click");
            //Console.WriteLine(_isSelected.ToString());
        }

        // get diff
        public double GetDifference(double number1, double number2)
        {
            return number1 - number2;
        }

        // mouse move
        public void MouseMove(Point mouse, ref Shapes shapes)
        {
            if (_isSelected)
            {
                double diffX = GetDifference(_point.X, mouse.X);
                double diffY = GetDifference(_point.Y, mouse.Y);
                shapes.MoveSelectedShape(diffX, diffY);
                _point = mouse;
            }
        }

        // mouse up
        public void MouseUp(Point mouse, string mode, ref Shapes shapes)
        {
            _isSelected = false;
        }

        // draw hint
        public void Drawing(IGraphics graphics, ref Shapes shapes)
        {
            //Console.WriteLine("draw");
            //Console.WriteLine(_isSelected.ToString());
            if (_isClicked)
                shapes.SelectShape(graphics);
        }
    }
}