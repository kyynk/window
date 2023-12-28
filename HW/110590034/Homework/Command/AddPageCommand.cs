using Homework.Model;
using System;

namespace Homework.Command
{
    public class AddPageCommand : ICommand
    {
        Shapes _shapes;
        Model.Model _model;
        int _pageIndex;
        double _panelWidth;

        public AddPageCommand(Model.Model model, Shapes shapes, int index)
        {
            _shapes = shapes;
            _model = model;
            _pageIndex = index;
            _panelWidth = -1;
        }

        // execute
        public void Execute(double width)
        {
            AdjustWithPanelWidth(width);
            _model.InsertPage(_shapes, _pageIndex);
        }

        // unexcute
        public void Undo(double width)
        {
            SetPanelWidth(width);
            _model.RemovePage(_pageIndex);
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
            _shapes.ResizeForPanel(ratio);
            SetPanelWidth(width);
        }
    }
}
