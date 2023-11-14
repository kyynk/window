using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework
{
    public interface IState
    {
        // mouse down
        void MouseDown(double mouseX, double mouseY, Model.Mode mode, ref ShapeFactory shapeFactory);
        // mouse move
        void MouseMove(double mouseX, double mouseY);
        // mouse up
        void MouseUp(double mouseX, double mouseY, ref Model.Mode mode, ref Shapes shapes);
        // drawing
        void Drawing(IGraphics graphics);
    }
}
