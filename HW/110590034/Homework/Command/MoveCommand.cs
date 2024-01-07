using Homework.Model;
using System;

namespace Homework.Command
{
    public class MoveCommand : ICommand
    {
        Model.Model _model;
        Shape _shape;
        double _offsetX;
        double _offsetY;
        bool _isNotFirstTime;
        double _panelWidth;
        int _pageIndex;

        public MoveCommand(Model.Model model, Shape shape, Point offset, int pageIndex)
        {
            _model = model;
            _shape = shape;
            _offsetX = offset.X;
            _offsetY = offset.Y;
            _isNotFirstTime = false;
            _panelWidth = -1;
            _pageIndex = pageIndex;
        }

        // execute
        public void Execute(double width)
        {
            AdjustWithPanelWidth(width);
            _model.SelectPage(_pageIndex);
            if (_isNotFirstTime)
                _shape.Move(_offsetX, _offsetY);
            _isNotFirstTime = true;
        }

        // unexcute
        public void Undo(double width)
        {
            AdjustWithPanelWidth(width);
            _model.SelectPage(_pageIndex);
            _shape.Move(-_offsetX, -_offsetY);
        }

        // store panel width
        public void SetPanelWidth(double width)
        {
            _panelWidth = width;
        }

        // adjust panel width
        public void AdjustWithPanelWidth(double width)
        {
            double ratio = width / _panelWidth;
            //_shape.ResizeForPanel(ratio);
            _offsetX = Math.Round(_offsetX * ratio, 1);
            _offsetY = Math.Round(_offsetY * ratio, 1);
            SetPanelWidth(width);
        }
    }
}
