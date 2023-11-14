using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework
{
    public class DrawingState : IState
    {
        private Point _point1;
        private bool _isPressed = false;
        private Shape _tempShape;

        public DrawingState()
        {
            _point1 = new Point(-1, -1);
        }

        // mouse down
        public void MouseDown(double mouseX, double mouseY, Model.Mode mode, ref ShapeFactory shapeFactory)
        {
            if (mode != Model.Mode.Pointer && mouseX > 0 && mouseY > 0)
            {
                _point1.X = mouseX;
                _point1.Y = mouseY;
                _tempShape = shapeFactory.AddDrawingShape(mode, _point1, _point1);
                _isPressed = true;
            }
        }

        // mouse move
        public void MouseMove(double mouseX, double mouseY)
        {
            if (_isPressed)
            {
                _tempShape.SetPoint2(new Point(mouseX, mouseY));
            }
        }

        // mouse up
        public void MouseUp(double mouseX, double mouseY, ref Model.Mode mode, ref Shapes shapes)
        {
            if (_isPressed)
            {
                _isPressed = false;
                Point point2 = new Point(mouseX, mouseY);
                shapes.AddNewShapeByDrawing(mode, _point1, point2);
                mode = Model.Mode.Pointer;
            }
        }

        // drawing
        public void Drawing(IGraphics graphics)
        {
            if (_isPressed)
                _tempShape.Draw(graphics);
        }
    }
}
