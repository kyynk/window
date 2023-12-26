using Homework.Model;
using System;

namespace Homework.Command
{
    public class ResizeCommand : ICommand
    {
        Shape _shape;
        Point _prePointLeft;
        Point _prePointRight;
        bool _isNotFirstTime;
        double _panelWidth;

        public ResizeCommand(Shape shape, Point prePointLeft, Point prePointRight)
        {
            _shape = shape;
            //_prePointLeft = new Point(prePointLeft.X, prePointLeft.Y);
            //_prePointRight = new Point(prePointRight.X, prePointRight.Y);
            _prePointLeft = new Point(prePointLeft);
            _prePointRight = new Point(prePointRight);
            _isNotFirstTime = false;
            _panelWidth = -1;
        }

        // execute
        public void Execute(double width)
        {
            AdjustWithPanelWidth(width);
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
            Resize();
        }

        // resize
        public void Resize()
        {
            //Point tempPoint1 = new Point(_shape.Point1.X, _shape.Point1.Y);
            //Point tempPoint2 = new Point(_shape.Point2.X, _shape.Point2.Y);
            Point tempPoint1 = new Point(_shape.Point1);
            Point tempPoint2 = new Point(_shape.Point2);
            _shape.Point1 = _prePointLeft;
            _shape.Point2 = _prePointRight;
            _prePointLeft = tempPoint1;
            _prePointRight = tempPoint2;
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
            _prePointLeft.X = Math.Round(_prePointLeft.X * ratio, 1);
            _prePointLeft.Y = Math.Round(_prePointLeft.Y * ratio, 1);
            _prePointRight.X = Math.Round(_prePointRight.X * ratio, 1);
            _prePointRight.Y = Math.Round(_prePointRight.Y * ratio, 1);
            SetPanelWidth(width);
        }
    }
}
