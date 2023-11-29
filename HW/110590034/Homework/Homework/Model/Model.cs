using System;
using System.ComponentModel;
using System.Windows.Forms;
using Homework.State;

namespace Homework.Model
{
    public class Model
    {
        public event ModelChangedEventHandler _modelChanged;
        public delegate void ModelChangedEventHandler();
        private int _panelMaxX;
        private int _panelMaxY;
        private Shape _tempShape;
        private Shapes _shapesData;
        private ShapeFactory _shapeFactory;
        private IState _state;
        private string _shapeName;
        private Shape.Location _selectedLocation;

        public Model()
        {
            _tempShape = null;
            _shapesData = new Shapes();
            _shapeFactory = new ShapeFactory();
            _shapeName = Constant.Constant.POINT;
            _state = new PointState(this);
            _panelMaxX = Constant.Constant.DEFAULT_MAX_PANEL_X;
            _panelMaxY = Constant.Constant.DEFAULT_MAX_PANEL_Y;
            _selectedLocation = Shape.Location.None;
        }

        public string ShapeName
        {
            get
            {
                return _shapeName;
            }
            set
            {
                _shapeName = value;
            }
        }

        // change state
        // add one more state -> resize state
        public virtual void ChangeState(string state)
        {
            if (state == Constant.Constant.POINT_STATE)
                _state = new PointState(this);
            else if (state == Constant.Constant.RESIZE_STATE)
                _state = new ResizeState(this);
            else
                _state = new DrawingState(this);
        }

        // pointer pressed
        public virtual void PressPointer(double mouseX, double mouseY)
        {
            _state.MouseDown(new Point(mouseX, mouseY), _shapeName, mouseX == CheckRange(mouseX, _panelMaxX) && mouseY == CheckRange(mouseY, _panelMaxY));
        }

        // pointer moved
        public virtual void MovePointer(double mouseX, double mouseY)
        {
            mouseX = CheckRange(mouseX, _panelMaxX);
            mouseY = CheckRange(mouseY, _panelMaxY);
            _state.MouseMove(new Point(mouseX, mouseY));
            NotifyModelChanged();
        }

        // pointer released
        public virtual void ReleasePointer(double mouseX, double mouseY)
        {

            mouseX = CheckRange(mouseX, _panelMaxX);
            mouseY = CheckRange(mouseY, _panelMaxY);
            _state.MouseUp(new Point(mouseX, mouseY), _shapeName);
            NotifyModelChanged();
        }

        // draw
        public virtual void Draw(IGraphics graphics)
        {
            graphics.ClearAll();
            foreach (Shape aShape in _shapesData.ShapeList)
                aShape.Draw(graphics);
            _state.Drawing(graphics);
        }

        // create drawing shape
        public virtual void CreateDrawingShape(string shapeType, Point firstPoint)
        {
            _tempShape = _shapeFactory.AddDrawingShape(shapeType, firstPoint, firstPoint);
        }

        // move drawing shape
        public virtual void MoveDrawingShape(Point secondPoint)
        {
            _tempShape.Point2 = new Point(secondPoint.X, secondPoint.Y);
        }

        // add drawing shape
        public virtual void AddDrawingShape(string shapeName, Point point1, Point point2)
        {
            _shapesData.AddNewShapeByDrawing(shapeName, point1, point2);
        }

        // clear drawing shape
        public virtual void ClearDrawingShape()
        {
            _tempShape = null;
        }

        // draw drawing shape
        public virtual void DrawDrawingShape(IGraphics graphics)
        {
            _tempShape.Draw(graphics);
        }

        // get select shape
        public virtual Shape GetSelectedShape()
        {
            return _shapesData.GetSelectedShape();
        }

        // check select shape
        public virtual bool CheckSelectedShape(double pointX, double pointY)
        {
            return _shapesData.CheckSelect(pointX, pointY);
        }

        // move select shape
        public virtual void MoveSelectedShape(double diffX, double diffY)
        {
            _shapesData.MoveSelectedShape(diffX, diffY);
        }

        // get location
        public Shape.Location GetLocation(double mouseX, double mouseY)
        {
            _selectedLocation = _shapesData.GetSelectedShapeLocation(mouseX, mouseY);
            return _selectedLocation;
        }

        // check is resize state or not
        public virtual bool CheckIsResizeState(double mouseX, double mouseY)
        {
            if (_shapesData.GetSelectedShape() != null)
            {
                if (GetLocation(mouseX, mouseY) != Shape.Location.None)
                {
                    return true;
                }
            }
            return false;
        }

        // check location is right bottom
        public virtual bool CheckLocationIsRightBottom(double mouseX, double mouseY)
        {
            if (GetSelectedShape() != null)
                return GetLocation(mouseX, mouseY) == Shape.Location.RightBottom;
            return false;
        }

        // resize selected shape
        public virtual void ResizeSelectedShape(double mouseX, double mouseY)
        {
            // now is constant
            if (_selectedLocation == Shape.Location.RightBottom)
                _shapesData.ResizeSelectedShape(_selectedLocation, new Point(mouseX, mouseY));
        }

        // stop resize selected shape
        public virtual void StopResizeSelectedShape()
        {
            _shapesData.CancelResize();
        }

        // check range for painting and return the value of range
        public double CheckRange(double mouse, double max)
        {
            if (mouse < 0)
            {
                return 0;
            }
            else if (mouse > max)
            {
                return max;
            }
            else
            {
                return mouse;
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
            _shapesData.DeleteShapeByIndex(index);
            NotifyModelChanged();
        }

        // handle key down
        // if keycode = delete, will delete selected shape
        public virtual void HandleKeyDown(Keys keyCode)
        {
            if (keyCode == Keys.Delete)
                _shapesData.DeleteSelectedShape();
            NotifyModelChanged();
        }

        // notify observer
        public void NotifyModelChanged()
        {
            if (_modelChanged != null)
                _modelChanged();
        }
    }
}
