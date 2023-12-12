using Homework.Model;
using System;

namespace Homework.Command
{
    public class MoveCommand : ICommand
    {
        Shape _shape;
        double _offsetX;
        double _offsetY;
        bool _isNotFirstTime;
        double _panelWidth;

        public MoveCommand(Shape shape, double offsetX, double offsetY)
        {
            _shape = shape;
            _offsetX = offsetX;
            _offsetY = offsetY;
            _isNotFirstTime = false;
            _panelWidth = -1;
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

        // store panel width
        public void StorePanelWidth(double width)
        {
            _panelWidth = width;
        }

        // adjust panel width
        public void AdjustPanelWidth(double width)
        {
            double ratio = width / _panelWidth;
            _shape.ResizeForPanel(ratio);
            _offsetX = Math.Round(_offsetX * ratio, 1);
            _offsetY = Math.Round(_offsetY * ratio, 1);
            StorePanelWidth(width);
        }
    }
}
