using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework
{
    public class Line : Shape
    {
        public Line()
        {
            _shapeName = _shapeType.GetLineChinese();
            SetInfo();
        }
    }
}
