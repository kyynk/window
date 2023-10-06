using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework
{
    public class ShapeType
    {
        private const string LINE = "LINE";
        private const string LINE_CHINESE = "線";
        private const string RECTANGLE = "RECTANGLE";
        private const string RECTANGLE_CHINESE = "矩形";

        public ShapeType()
        {

        }

        // get line string
        public string GetLine()
        {
            return LINE;
        }

        // get line string (chinese)
        public string GetLineChinese()
        {
            return LINE_CHINESE;
        }

        // get rectangle string
        public string GetRectangle()
        {
            return RECTANGLE;
        }

        // get rectangle string (chinese)
        public string GetRectangleChinese()
        {
            return RECTANGLE_CHINESE;
        }
    }
}
