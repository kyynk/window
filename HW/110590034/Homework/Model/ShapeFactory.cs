﻿using System;

namespace Homework.Model
{
    public class ShapeFactory
    {
        private const string ARGUMENT_WRONG = "ARGUMENT_WRONG";
        private const int MIN_X_RANGE = 0;
        private const int MIN_Y_RANGE = 0;
        private int _maxRangeX;
        private int _maxRangeY;
        private readonly Random _randomNumber;

        public ShapeFactory()
        {
            _randomNumber = new Random();
            _maxRangeX = Constant.Constant.DEFAULT_MAX_PANEL_X;
            _maxRangeY = Constant.Constant.DEFAULT_MAX_PANEL_Y;
        }

        // add shape
        public virtual Shape AddDrawingShape(string shapeType, Point point1, Point point2)
        {
            if (shapeType == Constant.Constant.LINE)
            {
                return new Line(point1, point2);
            }
            else if (shapeType == Constant.Constant.RECTANGLE)
            {
                return new Rectangle(point1, point2);
            }
            else if (shapeType == Constant.Constant.ELLIPSE)
            {
                return new Ellipse(point1, point2);
            }
            else
            {
                throw new ArgumentException(ARGUMENT_WRONG);
            }
        }

        // create shape
        public Shape CreateShape(string shapeType)
        {
            return AddDrawingShape(shapeType, new Point(_randomNumber.Next(MIN_X_RANGE, _maxRangeX), _randomNumber.Next(MIN_Y_RANGE, _maxRangeY)), new Point(_randomNumber.Next(MIN_X_RANGE, _maxRangeX), _randomNumber.Next(MIN_Y_RANGE, _maxRangeY)));
        }

        // set range
        public void SetRange(double ratio)
        {
            _maxRangeX = (int)((double)_maxRangeX * ratio);
            _maxRangeY = (int)((double)_maxRangeY * ratio);
        }
    }
}