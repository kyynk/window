using System;

namespace Homework.Model
{
    public class ShapeFactory
    {
        private const string NOT_IMPLEMENTED = "NOT_IMPLEMENTED";
        private const int MIN_X_RANGE = 0; // 150
        private const int MAX_X_RANGE = 490; // 800
        private const int MIN_Y_RANGE = 0; // 100
        private const int MAX_Y_RANGE = 415; // 550
        private readonly Random _randomNumber;

        public ShapeFactory()
        {
            _randomNumber = new Random();
        }

        // add shape
        public Shape AddDrawingShape(string shapeType, Point point1, Point point2)
        {
            if (shapeType == Constant.LINE)
            {
                return new Line(point1, point2);
            }
            else if (shapeType == Constant.RECTANGLE)
            {
                return new Rectangle(point1, point2);
            }
            else if (shapeType == Constant.ELLIPSE)
            {
                return new Ellipse(point1, point2);
            }
            else
            {
                throw new NotImplementedException(NOT_IMPLEMENTED);
            }
        }

        // create shape
        public Shape CreateShape(string shapeType)
        {
            if (shapeType == Constant.LINE)
            {
                return new Line(new Point(_randomNumber.Next(MIN_X_RANGE, MAX_X_RANGE), _randomNumber.Next(MIN_Y_RANGE, MAX_Y_RANGE)), new Point(_randomNumber.Next(MIN_X_RANGE, MAX_X_RANGE), _randomNumber.Next(MIN_Y_RANGE, MAX_Y_RANGE)));
            }
            else if (shapeType == Constant.RECTANGLE)
            {
                return new Rectangle(new Point(_randomNumber.Next(MIN_X_RANGE, MAX_X_RANGE), _randomNumber.Next(MIN_Y_RANGE, MAX_Y_RANGE)), new Point(_randomNumber.Next(MIN_X_RANGE, MAX_X_RANGE), _randomNumber.Next(MIN_Y_RANGE, MAX_Y_RANGE)));
            }
            else if (shapeType == Constant.ELLIPSE)
            {
                return new Ellipse(new Point(_randomNumber.Next(MIN_X_RANGE, MAX_X_RANGE), _randomNumber.Next(MIN_Y_RANGE, MAX_Y_RANGE)), new Point(_randomNumber.Next(MIN_X_RANGE, MAX_X_RANGE), _randomNumber.Next(MIN_Y_RANGE, MAX_Y_RANGE)));
            }
            else
            {
                throw new NotImplementedException(NOT_IMPLEMENTED);
            }
        }
    }
}
