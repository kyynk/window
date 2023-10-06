using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework
{
    public class ShapeFactory
    {
        private readonly ShapeType _shapeType;
        public ShapeFactory()
        {
            _shapeType = new ShapeType();
        }

        // create shape
        public Shape CreateShape(string shapeType)
        {
            if (shapeType == _shapeType.GetLineChinese())
            {
                return new Line();
            }
            else
            {
                return new Rectangle();
            }
        }
    }
}
