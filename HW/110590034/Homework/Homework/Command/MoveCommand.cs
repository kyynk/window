using Homework.Model;

namespace Homework.Command
{
    public class MoveCommand : ICommand
    {
        Shape _shape;
        double _offsetX;
        double _offsetY;
        bool _isNotFirstTime;

        public MoveCommand(Shape shape, double offsetX, double offsetY)
        {
            _shape = shape;
            _offsetX = offsetX;
            _offsetY = offsetY;
            _isNotFirstTime = false;
        }

        // execute
        public void Execute()
        {
            if (_isNotFirstTime)
                _shape.Move(_offsetX, _offsetY);
            _isNotFirstTime = true;
        }

        // unexcute
        public void Undo()
        {
            _shape.Move(-_offsetX, -_offsetY);
        }
    }
}
