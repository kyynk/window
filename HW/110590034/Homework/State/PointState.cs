using Homework.Model;

namespace Homework.State
{
    public class PointState : IState
    {
        private Model.Model _model;
        private Point _point;
        private bool _isSelected;
        private bool _isClicked;

        public PointState(Model.Model model)
        {
            _model = model;
            _point = new Point(-1, -1);
            _isSelected = false;
            _isClicked = false;
        }

        public bool IsSelected
        {
            get
            {
                return _isSelected;
            }
            set
            {
                _isSelected = value;
            }
        }

        public bool IsClicked
        {
            get
            {
                return _isClicked;
            }
            set
            {
                _isClicked = value;
            }
        }

        // mouse down
        public void MouseDown(Point mouse, string shapeName, bool isInPanel)
        {
            _isClicked = false;
            _isClicked = _model.CheckSelectedShape(mouse.X, mouse.Y);
            _isSelected = _model.CheckSelectedShape(mouse.X, mouse.Y);
            _point = mouse;
            if (!_isClicked)
                _isSelected = false;
        }

        // get diff
        public double GetDifference(double number1, double number2)
        {
            return number1 - number2;
        }

        // mouse move
        public void MouseMove(Point mouse)
        {
            if (_isSelected)
            {
                double diffX = GetDifference(_point.X, mouse.X);
                double diffY = GetDifference(_point.Y, mouse.Y);
                _model.MoveSelectedShape(diffX, diffY);
                _point = mouse;
            }
        }

        // mouse up
        public void MouseUp(Point mouse, string shapeName)
        {
            if (_isSelected)
            {
                _model.MoveDone(mouse.X, mouse.Y);
            }
            _isSelected = false;
        }

        // draw hint
        public void Drawing(IGraphics graphics)
        {
            Shape hint = _model.GetSelectedShape();
            if (_isClicked && hint != null)
                hint.DrawHint(graphics);
        }
    }
}
