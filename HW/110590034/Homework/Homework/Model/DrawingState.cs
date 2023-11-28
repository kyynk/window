namespace Homework.Model
{
    public class DrawingState : IState
    {
        private Model _model;
        private Point _point1;
        private bool _isPressed;

        public DrawingState(Model model)
        {
            _model = model;
            _point1 = new Point(-1, -1);
            _isPressed = false;
        }

        // mouse down
        public void MouseDown(Point mouse, string shapeName, bool isInPanel)
        {
            if (isInPanel)
            {
                _point1 = mouse;
                _model.CreateDrawingShape(shapeName, _point1);
                _isPressed = true;
            }
        }

        // mouse move
        public void MouseMove(Point mouse)
        {
            if (_isPressed)
            {
                _model.MoveDrawingShape(mouse);
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
                _model.ClearDrawingShape();
            }
        }

        // drawing
        public void Drawing(IGraphics graphics)
        {
            if (_isPressed)
                _model.DrawDrawingShape(graphics);
        }
    }
}
