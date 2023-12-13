using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Homework.Model;

namespace Homework.State
{
    public class ResizeState : IState
    {
        private Model.Model _model;
        private bool _isPressed;

        public ResizeState(Model.Model model)
        {
            _model = model;
            _isPressed = false;
        }

        // mouse down
        public void MouseDown(Point mouse, string shapeName, bool isInPanel)
        {
            _isPressed = true;
            _model.CheckBottomPoint();
            //Console.WriteLine("resize down");
        }

        // mouse move
        public void MouseMove(Point mouse)
        {
            // resize shape
            //Console.WriteLine("resize move");
            if (_isPressed)
                _model.ResizeSelectedShape(mouse.X, mouse.Y);
        }

        // mouse up
        public void MouseUp(Point mouse, string shapeName)
        {
            _isPressed = false;
            _model.StopResizeSelectedShape();
        }

        // drawing
        public void Drawing(IGraphics graphics)
        {
            Shape hint = _model.GetSelectedShape();
            if (hint != null)
                hint.DrawHint(graphics);
        }
    }
}
