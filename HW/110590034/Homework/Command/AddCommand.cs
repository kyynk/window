using Homework.Model;
using System;

namespace Homework.Command
{
    public class AddCommand : ICommand
    {
        Shape _shape;
        Model.Model _model;
        int _shapeIndex;
        double _panelWidth;
        int _pageIndex;

        public AddCommand(Model.Model model, Shape shape, int shapeIndex, int pageIndex)
        {
            _shape = shape;
            _model = model;
            _shapeIndex = shapeIndex;
            _pageIndex = pageIndex;
            _panelWidth = -1;
        }

        // execute
        public void Execute(double width)
        {
            AdjustWithPanelWidth(width);
            Console.WriteLine("here");
            _model.SelectPage(_pageIndex);
            Console.WriteLine("select");
            _model.InsertShape(_shape, _shapeIndex, _pageIndex);
        }

        // unexcute
        public void Undo(double width)
        {
            SetPanelWidth(width);
            _model.SelectPage(_pageIndex);
            _model.DeleteShape(_shapeIndex, _pageIndex);
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
            _shape.ResizeForPanel(ratio);
            SetPanelWidth(width);
        }
    }
}