using Homework.Model;

namespace Homework.Command
{
    public class DrawCommand : ICommand
    {
        Shape _shape;
        Model.Model _model;
        int _shapeIndex;

        public DrawCommand(Model.Model model, Shape shape, int index)
        {
            _shape = shape;
            _model = model;
            _shapeIndex = index;
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
