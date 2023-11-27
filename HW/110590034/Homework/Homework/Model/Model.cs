using System.ComponentModel;
using System.Windows.Forms;

namespace Homework.Model
{
    public class Model
    {
        public event ModelChangedEventHandler _modelChanged;
        public delegate void ModelChangedEventHandler();
        private int _panelMaxX;
        private int _panelMaxY;
        private Shapes _shapesData;
        private ShapeFactory _shapeFactory;
        private IState _state;
        private string _shapeName;

        public Model()
        {
            const int DEFAULT_MAX_PANEL_X = 490;
            const int DEFAULT_MAX_PANEL_Y = 415;
            _shapesData = new Shapes();
            _shapeFactory = new ShapeFactory();
            _shapeName = Constant.Constant.POINT;
            _state = new PointState(this);
            _panelMaxX = DEFAULT_MAX_PANEL_X;
            _panelMaxY = DEFAULT_MAX_PANEL_Y;
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
        public void ChangeState(string state)
        {
            if (state == Constant.Constant.POINT_STATE)
                _state = new PointState(this);
            else
                _state = new DrawingState(this, _shapeFactory);
        }

        // pointer pressed
        public void PressPointer(double mouseX, double mouseY)
        {
            _state.MouseDown(new Point(mouseX, mouseY), _shapeName);
        }

        // pointer moved
        public void MovePointer(double mouseX, double mouseY)
        {
            mouseX = CheckRangeOfX(mouseX);
            mouseY = CheckRangeOfY(mouseY);
            _state.MouseMove(new Point(mouseX, mouseY));
            NotifyModelChanged();
        }

        // pointer released
        public void ReleasePointer(double mouseX, double mouseY)
        {
            
            mouseX = CheckRangeOfX(mouseX);
            mouseY = CheckRangeOfY(mouseY);
            _state.MouseUp(new Point(mouseX, mouseY), _shapeName);
            NotifyModelChanged();
        }

        // draw
        public void Draw(IGraphics graphics)
        {
            graphics.ClearAll();
            foreach (Shape aShape in _shapesData.ShapeList)
                aShape.Draw(graphics);
            _state.Drawing(graphics);
        }

        // add drawing shape
        public virtual void AddDrawingShape(string shapeName, Point point1, Point point2)
        {
            _shapesData.AddNewShapeByDrawing(shapeName, point1, point2);
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

        // check range for painting and return the value of range
        public double CheckRangeOfX(double mouseX)
        {
            if (mouseX < 0)
            {
                return 0;
            }
            else if (mouseX > _panelMaxX)
            {
                return _panelMaxX;
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
            else if (mouseY > _panelMaxY)
            {
                return _panelMaxY;
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
            _shapesData.DeleteShapeByIndex(index);
            NotifyModelChanged();
        }

        // handle key down
        // if keycode = delete, will delete selected shape
        public void HandleKeyDown(Keys keyCode)
        {
            if (keyCode == Keys.Delete)
                _shapesData.DeleteSelectedShape();
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
