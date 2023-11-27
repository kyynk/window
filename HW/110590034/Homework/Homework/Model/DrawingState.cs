namespace Homework.Model
{
    public class DrawingState : IState
    {
        private Model _model;
        private ShapeFactory _shapeFactory;
        private Point _point1;
        private bool _isPressed;
        private Shape _tempShape;
        private const int MAX_PANEL_X = 490;
        private const int MAX_PANEL_Y = 415;

        public DrawingState(Model model, ShapeFactory shapeFactory)
        {
            _model = model;
            _shapeFactory = shapeFactory;
            _point1 = new Point(-1, -1);
            _isPressed = false;
        }

        // check x can rm
        public bool CheckX(double pointX)
        {
            return (pointX >= 0 && pointX <= MAX_PANEL_X);
        }

        // check y can rm
        public bool CheckY(double pointY)
        {
            return (pointY >= 0 && pointY <= MAX_PANEL_Y);
        }

        // check range
        public bool CheckRange(Point point)
        {
            return (CheckX(point.X) && CheckY(point.Y));
        }

        // mouse down
        public void MouseDown(Point mouse, string shapeName)
        {
            if (CheckRange(mouse))
            {
                _point1 = mouse;
                _tempShape = _shapeFactory.AddDrawingShape(shapeName, _point1, _point1);
                _isPressed = true;
            }
        }

        // mouse move
        public void MouseMove(Point mouse)
        {
            if (_isPressed)
            {
                _tempShape.Point2 = new Point(mouse.X, mouse.Y);
            }
        }

        // mouse up
        public void MouseUp(Point mouse, string shapeName)
        {
            if (_isPressed)
            {
                _isPressed = false;
                Point point2 = new Point(mouse.X, mouse.Y);
                _model.AddDrawingShape(shapeName, _point1, point2);
                _tempShape = null;
            }
        }

        // drawing
        public void Drawing(IGraphics graphics)
        {
            if (_isPressed)
                _tempShape.Draw(graphics);
        }
    }
}
