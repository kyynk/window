using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Homework.Model;

namespace Homework.Command
{
    public class DrawCommand
    {
        Shape _shape;
        Model.Model _model;
        public DrawCommand(Model.Model model, Shape shape)
        {
            _shape = shape;
            _model = model;
        }

        // execute
        public void Execute()
        {
            //_model.DrawShape(rect);
        }

        // unexcute
        public void UnExecute()
        {
            //_model.DeleteShape();
        }
    }
}
