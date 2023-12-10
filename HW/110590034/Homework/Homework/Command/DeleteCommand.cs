using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Homework.Model;

namespace Homework.Command
{
    public class DeleteCommand
    {
        Shape _shape;
        Model.Model _model;
        public DeleteCommand(Model.Model model, Shape shape)
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
