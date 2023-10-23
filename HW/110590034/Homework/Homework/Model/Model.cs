using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework
{
    public class Model
    {
        public event ModelChangedEventHandler _modelChanged;
        public delegate void ModelChangedEventHandler();
        private const int MAX_PANEL_X = 490;
        private const int MAX_PANEL_Y = 415;
        private Point _point1;
        private bool _isPressed = false;
        private Shapes _shapesData;
        private ShapeFactory _shapeFactory;
        private Shape _tempShape;
        private Mode _mode;

        public enum Mode
        {
            Pointer,
            DrawLine,
            DrawRectangle,
            DrawEllipse
        }

        public Model()
        {
            _shapesData = new Shapes();
            _shapeFactory = new ShapeFactory();
            _point1 = new Point(-1, -1);
            _mode = Mode.Pointer;
        }

        // set mode
        public void SetMode(Mode mode)
        {
            _mode = mode;
        }

        // pointer pressed
        public void PressPointer(double mouseX, double mouseY)
        {
            if (_mode != Mode.Pointer && mouseX > 0 && mouseY > 0)
            {
                _point1.X = mouseX;
                _point1.Y = mouseY;
                _tempShape = _shapeFactory.AddDrawingShape(_mode, _point1, _point1);
                _isPressed = true;
            }
        }

        // pointer moved
        public void MovePointer(double mouseX, double mouseY)
        {
            if (_isPressed)
            {
                mouseX = CheckRangeOfX(mouseX);
                mouseY = CheckRangeOfY(mouseY);
                _tempShape.SetPoint2(new Point(mouseX, mouseY));
                NotifyModelChanged();
            }
        }

        // pointer released
        public void ReleasePointer(double mouseX, double mouseY)
        {
            if (_isPressed)
            {
                _isPressed = false;
                mouseX = CheckRangeOfX(mouseX);
                mouseY = CheckRangeOfY(mouseY);
                Point point2 = new Point(mouseX, mouseY);
                _shapesData.AddNewShapeByDrawing(_mode, _point1, point2);
                _mode = Mode.Pointer;
                NotifyModelChanged();
            }
        }

        // draw
        public void Draw(IGraphics graphics)
        {
            graphics.ClearAll();
            foreach (Shape aShape in _shapesData.ShapeList)
                aShape.Draw(graphics);
            if (_isPressed)
                _tempShape.Draw(graphics);
        }

        // check range for painting and return the value of range
        public double CheckRangeOfX(double mouseX)
        {
            if (mouseX < 0)
            {
                return 0;
            }
            else if (mouseX > MAX_PANEL_X)
            {
                return MAX_PANEL_X;
            }
            else
            {
                return mouseX;
            }
        }

        // check range for painting and return the value of range
        public double CheckRangeOfY(double mouseY)
        {
            if (mouseY < 0)
            {
                return 0;
            }
            else if (mouseY > MAX_PANEL_Y)
            {
                return MAX_PANEL_Y;
            }
            else
            {
                return mouseY;
            }
        }

        // add new shape to shapes
        public void Create(string shapeType)
        {
            _shapesData.AddNewShapeByRandom(shapeType);
            NotifyModelChanged();
        }

        // get shapes
        public BindingList<Shape> GetShapes()
        {
            return _shapesData.ShapeList;
        }

        // delete selected shape from _shapes
        public void Delete(int index)
        {
            _shapesData.DeleteSelectedShape(index);
            NotifyModelChanged();
        }

        // notify observer
        private void NotifyModelChanged()
        {
            if (_modelChanged != null)
                _modelChanged();
        }

        /*databinding
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
        */
    }
}
