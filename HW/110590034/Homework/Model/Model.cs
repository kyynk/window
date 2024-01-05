using System;
using System.ComponentModel;
using System.Windows.Forms;
using Homework.State;
using Homework.Command;

namespace Homework.Model
{
    public partial class Model
    {
        public event ModelChangedEventHandler _modelChanged;
        public delegate void ModelChangedEventHandler();
        public event Pages.PagesChanged _pagesChanged;
        private int _panelMaxX;
        private int _panelMaxY;
        private Shapes _shapesData;
        private Shape _tempShape;
        private ShapeFactory _shapeFactory;
        private IState _state;
        private string _shapeName;
        private Shape.Location _selectedLocation;
        private Point _firstPoint;
        private Point _firstPoint2;
        private CommandManager _commandManager;
        private Pages _pages;
        private int _pageIndex;

        public Model()
        {
            _tempShape = null;
            _shapeFactory = new ShapeFactory();
            _shapeName = Constant.Constant.POINT;
            _state = new PointState(this);
            _panelMaxX = Constant.Constant.DEFAULT_MAX_PANEL_X;
            _panelMaxY = Constant.Constant.DEFAULT_MAX_PANEL_Y;
            _selectedLocation = Shape.Location.None;
            _firstPoint = new Point(-1, -1);
            _firstPoint2 = new Point(-1, -1);
            _commandManager = new CommandManager();
            _pages = new Pages();
            _pages.InsertPageByIndex(0, new Shapes());
            _pageIndex = 0;
            _shapesData = _pages.GetSelectPage(_pageIndex);
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
            _firstPoint = new Point(mouseX, mouseY);
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
            _tempShape = _shapeFactory.CreateShape(shapeType, firstPoint, firstPoint);
        }

        // move drawing shape
        public virtual void MoveDrawingShape(Point secondPoint)
        {
            _tempShape.Point2 = new Point(secondPoint.X, secondPoint.Y);
        }

        // add drawing shape
        public virtual void AddDrawingShape(string shapeName, Point point1, Point point2)
        {
            Shape shape = _shapeFactory.CreateShape(shapeName, point1, point2);
            //_shapesData.AddNewShape(shape);
            _commandManager.Execute(new DrawCommand(this, shape, _shapesData.ShapeList.Count, _pageIndex), _panelMaxX);
            //NotifyModelChanged();
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

        // move done
        public virtual void MoveDone(double endX, double endY)
        {
            double diffX = _firstPoint.X - endX;
            double diffY = _firstPoint.Y - endY;
            _commandManager.Execute(new MoveCommand(GetSelectedShape(), diffX, diffY), _panelMaxX);
            NotifyModelChanged();
        }

        // get location
        public Shape.Location GetLocation(double mouseX, double mouseY)
        {
            _selectedLocation = _shapesData.GetSelectedShapeLocation(mouseX, mouseY);
            return _selectedLocation;
        }

        // set is first point on bottom (line) on resize state
        public virtual void CheckBottomPoint()
        {
            _firstPoint = new Point(GetSelectedShape().Point1);
            _firstPoint2 = new Point(GetSelectedShape().Point2);
            _shapesData.SetSelectedShapeIsFirstPointBottom();
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
            _commandManager.Execute(new ResizeCommand(GetSelectedShape(), _firstPoint, _firstPoint2), _panelMaxX);
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

        //undo
        public virtual void Undo()
        {
            if (IsUndoEnabled)
                _commandManager.Undo(_panelMaxX);
            NotifyModelChanged();
        }

        // redo
        public virtual void Redo()
        {
            if (IsRedoEnabled)
                _commandManager.Redo(_panelMaxX);
            NotifyModelChanged();
        }

        public virtual bool IsUndoEnabled
        {
            get
            {
                return _commandManager.IsUndoEnabled;
            }
        }

        public virtual bool IsRedoEnabled
        {
            get
            {
                return _commandManager.IsRedoEnabled;
            }
        }

        // insert shape
        public virtual void InsertShape(Shape shape, int shapeIndex, int pageIndex)
        {

            _pages.GetSelectPage(pageIndex).InsertShapeByIndex(shape, shapeIndex);
            NotifyModelChanged();
            Console.WriteLine("insert shap index : " + pageIndex + " now page " + _pageIndex);
        }

        // add new shape to shapes
        public virtual void Create(string shapeType, Point point1, Point point2)
        {
            Shape shape = _shapeFactory.CreateShape(shapeType, point1, point2);
            //_shapesData.AddNewShape(shape);
            _commandManager.Execute(new AddCommand(this, shape, _shapesData.ShapeList.Count, _pageIndex), _panelMaxX);
            //NotifyModelChanged();
        }

        // get shapes
        public BindingList<Shape> GetShapes()
        {
            return _shapesData.ShapeList;
        }

        // delete selected shape from _shapes
        public virtual void DeleteShape(int shapeIndex, int pageIndex)
        {
            _pages.GetSelectPage(pageIndex).DeleteShapeByIndex(shapeIndex);
            NotifyModelChanged();
            Console.WriteLine("delete shap index : " + pageIndex + " now page " + _pageIndex);
        }

        // delete selected shape from _shapes
        public virtual void Delete(int index)
        {
            //_shapesData.DeleteShapeByIndex(index);
            _commandManager.Execute(new DeleteCommand(this, _shapesData.ShapeList[index], index, _pageIndex), _panelMaxX);
            //NotifyModelChanged();
        }

        // get shape index
        public int GetShapeIndex(Shape shape)
        {
            int index = -1;
            foreach (Shape aShape in _shapesData.ShapeList)
            {
                index++;
                if (aShape == shape)
                {
                    return index;
                }
            }
            return -1;
        }

        // handle key down
        // if keycode = delete, will delete selected shape
        public virtual void HandleKeyDown(Keys keyCode)
        {
            if (keyCode == Keys.Delete && GetSelectedShape() != null)
            {
                _commandManager.Execute(new DeleteCommand(this, GetSelectedShape(), GetShapeIndex(GetSelectedShape()), _pageIndex), _panelMaxX);
                _shapesData.ClearSelectedShape();
            }
            NotifyModelChanged();
        }

        // set panel size
        public virtual void SetPanelSize(double width)
        {
            double ratio = width / (double)_panelMaxX;
            foreach (Shape aShape in _shapesData.ShapeList)
            {
                aShape.ResizeForPanel(ratio);
            }
            _shapeFactory.SetRange(ratio);
            _panelMaxX = (int)width;
            _panelMaxY = (int)(width * Constant.Constant.PANEL_RATIO);
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
