﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework
{
    public class Rectangle : Shape
    {
        public Rectangle()
        {
            _shapeName = _shapeType.GetRectangleChinese();
            SetInfo();
        }
    }
}
