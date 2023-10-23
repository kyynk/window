using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework
{
    public class ShapeFactory
    {
        private const string LINE_CHINESE = "線";
        private const string RECTANGLE_CHINESE = "矩形";
        private const string CIRCLE_CHINESE = "圓";
        private const string NOT_IMPLEMENTED = "NOT_IMPLEMENTED";
        private const int MIN_X_RANGE = 150;
        private const int MAX_X_RANGE = 800;
        private const int MIN_Y_RANGE = 100;
        private const int MAX_Y_RANGE = 550;
        private Random _randomNumber;


        public ShapeFactory()
        {
            _randomNumber = new Random();
        }

        // add shape
        public Shape AddDrawingShape(Model.Mode shapeType, Point point1, Point point2)
        {
            Shape newShape;
            if (shapeType == Model.Mode.DrawLine)
            {
                newShape = new Line();
                newShape.SetPosition(point1, point2);
                return newShape;
            }
            else if (shapeType == Model.Mode.DrawRectangle)
            {
                newShape = new Rectangle();
                newShape.SetPosition(point1, point2);
                return newShape;
            }
            else if (shapeType == Model.Mode.DrawEllipse)
            {
                newShape = new Ellipse();
                newShape.SetPosition(point1, point2);
                return newShape;
            }
            else
            {
                throw new NotImplementedException(NOT_IMPLEMENTED);
            }
        }

        // create shape
        public Shape CreateShape(string shapeType)
        {
            Shape newShape;
            Point point1 = new Point(_randomNumber.Next(MIN_X_RANGE, MAX_X_RANGE), _randomNumber.Next(MIN_Y_RANGE, MAX_Y_RANGE));
            Point point2 = new Point(_randomNumber.Next(MIN_X_RANGE, MAX_X_RANGE), _randomNumber.Next(MIN_Y_RANGE, MAX_Y_RANGE));
            if (shapeType == LINE_CHINESE)
            {
                newShape = new Line();
            }
            else if (shapeType == RECTANGLE_CHINESE)
            {
                newShape = new Rectangle();
            }
            else if (shapeType == CIRCLE_CHINESE)
            {
                newShape = new Ellipse();
            }
            else
            {
                throw new NotImplementedException(NOT_IMPLEMENTED);
            }
            newShape.SetPosition(point1, point2);
            return newShape;
        }
    }
}
