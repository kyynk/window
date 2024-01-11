using Homework.Model;
using System;

namespace Homework.Command
{
    public class ResizeCommand : ICommand
    {
        Model.Model _model;
        Shape _shape;
        Point _previousPointLeft;
        Point _previousPointRight;
        bool _isNotFirstTime;
        double _panelWidth;
        int _pageIndex;

        public ResizeCommand(Model.Model model, Shape shape, Point previousPointLeft, Point previousPointRight, int pageIndex)
        {
            _model = model;
            _shape = shape;
            //_prePointLeft = new Point(prePointLeft.X, prePointLeft.Y);
            //_prePointRight = new Point(prePointRight.X, prePointRight.Y);
            _previousPointLeft = new Point(previousPointLeft);
            _previousPointRight = new Point(previousPointRight);
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
            {
                Resize();
            }
            _isNotFirstTime = true;
        }

        // unexcute
        public void Undo(double width)
        {
            AdjustWithPanelWidth(width);
            _model.SelectPage(_pageIndex);
            Resize();
        }

        // resize
        public void Resize()
        {
            //Point tempPoint1 = new Point(_shape.Point1.X, _shape.Point1.Y);
            //Point tempPoint2 = new Point(_shape.Point2.X, _shape.Point2.Y);
            Point tempPoint1 = new Point(_shape.Point1);
            Point tempPoint2 = new Point(_shape.Point2);
            _shape.SetTwoPoint(_previousPointLeft, _previousPointRight);
            _previousPointLeft = tempPoint1;
            _previousPointRight = tempPoint2;
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
            _previousPointLeft.X = Math.Round(_previousPointLeft.X * ratio, 1);
            _previousPointLeft.Y = Math.Round(_previousPointLeft.Y * ratio, 1);
            _previousPointRight.X = Math.Round(_previousPointRight.X * ratio, 1);
            _previousPointRight.Y = Math.Round(_previousPointRight.Y * ratio, 1);
            SetPanelWidth(width);
        }
    }
}
