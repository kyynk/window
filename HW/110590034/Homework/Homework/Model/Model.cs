using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework
{
    public class Model
    {
        public enum Mode
        {
            Pointer,
            DrawLine,
            DrawRectangle,
            DrawEllipse
        }

        public event ModelChangedEventHandler _modelChanged;
        public delegate void ModelChangedEventHandler();
        private double _firstPointX;
        private double _firstPointY;
        private bool _isPressed = false;
        private Shapes _shapesData;
        private Shape _tempShape;
        private Mode _mode = Mode.Pointer;

        public Model()
        {
            _shapesData = new Shapes();
        }

        // set mode
        public void SetMode(Mode mode)
        {
            _mode = mode;
            if (mode == Mode.DrawLine)
            {
                _tempShape = new Line();
            }
            else if (mode == Mode.DrawRectangle)
            {
                _tempShape = new Rectangle();
            }
            else if (mode == Mode.DrawEllipse)
            {
                _tempShape = new Ellipse();
            }
        }

        // pointer pressed
        public void PointerPressed(double x, double y)
        {
            Console.WriteLine("Press");
            Console.WriteLine($"x:{x}, y:{y}");
            if (_mode != Mode.Pointer && x > 0 && y > 0)
            {
                _firstPointX = x;
                _firstPointY = y;
                _tempShape.SetPoint1(new Point(_firstPointX,  _firstPointY));
                _isPressed = true;
            }
        }

        // pointer moved
        public void PointerMoved(double x, double y)
        {
            Console.WriteLine("Move");
            Console.WriteLine($"x:{x}, y:{y}");
            if (_isPressed)
            {
                _tempShape.SetPoint2(new Point(x, y));
                NotifyModelChanged();
            }
        }

        // pointer released
        public void PointerReleased(double x, double y)
        {
            if (_isPressed)
            {
                _isPressed = false;
                Point point1 = new Point(_firstPointX, _firstPointY);
                Point point2 = new Point(x, y);
                _shapesData.AddNewShapeByDrawing(_mode, point1, point2);
                _mode = Mode.Pointer;
                Console.WriteLine($"x1:{point1.X}, y1:{point1.Y}, x2:{point2.X}, y2:{point2.Y}");
                NotifyModelChanged();
            }
        }

        // draw
        public void Draw(IGraphics graphics)
        {
            Console.WriteLine("drawing");
            graphics.ClearAll();
            foreach (Shape shape in _shapesData.GetShapes())
                shape.Draw(graphics);
            if (_isPressed)
                _tempShape.Draw(graphics);
        }

        // add new shape to shapes
        public void Create(string shapeType)
        {
            _shapesData.AddNewShapeByRandom(shapeType);
        }

        // get new shape name
        public string GetNewShapeType()
        {
            return _shapesData.GetNewShapeName();
        }

        // get new shape location
        public string GetNewShapePosition()
        {
            return _shapesData.GetNewShapeInfo();
        }

        // delete selected shape from _shapes
        public void Delete(int index)
        {
            _isPressed = false;
            _shapesData.DeleteSelectedShape(index);
            NotifyModelChanged();
        }

        // notify observer
        private void NotifyModelChanged()
        {
            if (_modelChanged != null)
                _modelChanged();
        }
    }
}
