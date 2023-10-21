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

        public ShapeFactory()
        {

        }

        // create shape
        public Shape CreateShape(string shapeType)
        {
            if (shapeType == LINE_CHINESE)
            {
                return new Line();
            }
            else if (shapeType == RECTANGLE_CHINESE)
            {
                return new Rectangle();
            }
            else if (shapeType == CIRCLE_CHINESE)
            {
                return new Ellipse();
            }
            else
            {
                throw new NotImplementedException(NOT_IMPLEMENTED);
            }
        }
    }
}
