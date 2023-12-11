using Homework.Model;

namespace Homework.Command
{
    public class AddCommand : ICommand
    {
        Shape _shape;
        Model.Model _model;
        int _shapeIndex;

        public AddCommand(Model.Model model, Shape shape, int index)
        {
            _shape = shape;
            _model = model;
            _shapeIndex = index;
        }

        // execute
        public void Execute()
        {
            _model.InsertShape(_shape, _shapeIndex);
        }

        // unexcute
        public void Undo()
        {
            _model.DeleteShape(_shapeIndex);
        }
    }
}