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

        public ResizeState(Model.Model model)
        {
            _model = model;
        }

        // mouse down
        public void MouseDown(Point mouse, string shapeName, bool isInPanel)
        {
            // if not in shape resize point
            // -> point state
        }

        // mouse move
        public void MouseMove(Point mouse)
        {
            // resize shape
        }

        // mouse up
        public void MouseUp(Point mouse, string shapeName)
        {
            
        }

        // drawing
        public void Drawing(IGraphics graphics)
        {
            Shape hint = _model.GetSelectedShape();
            //if (_isClicked && hint != null)
            //    hint.DrawHint(graphics);
        }
    }
}
