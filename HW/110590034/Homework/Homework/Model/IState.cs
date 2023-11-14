using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace Homework
{
    public interface IState
    {
        // mouse down
        void MouseDown(Point mouse, string mode, ref Shapes shapes, ref ShapeFactory shapeFactory);
        // mouse move
        void MouseMove(Point mouse, ref Shapes shapes);
        // mouse up
        void MouseUp(Point mouse, string mode, ref Shapes shapes);
        // drawing
        void Drawing(IGraphics graphics, ref Shapes shapes);
    }
}
