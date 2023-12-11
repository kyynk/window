using Homework.Model;

namespace Homework.Command
{
    public class DeleteCommand : ICommand
    {
        Shape _shape;
        Model.Model _model;
        int _shapeIndex;

        public DeleteCommand(Model.Model model, Shape shape, int index)
        {
            _shape = shape;
            _model = model;
            _shapeIndex = index;
        }

        // execute
        public void Execute()
        {
            _model.DeleteShape(_shapeIndex);
        }

        // unexcute
        public void UnExecute()
        {
            _model.InserShape(_shape, _shapeIndex);
        }
    }
}
