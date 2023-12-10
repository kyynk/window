using Homework.Model;

namespace Homework.Command
{
    public class MoveCommand : ICommand
    {
        Shape _shape;
        Model.Model _model;
        Point _point1;
        Point _point2;

        public MoveCommand(Model.Model model, Shape shape, Point point1, Point point2)
        {
            _shape = shape;
            _model = model;
            _point1 = point1;
            _point2 = point2;
        }

        // execute
        public void Execute()
        {
            
        }

        // unexcute
        public void UnExecute()
        {
            
        }
    }
}
