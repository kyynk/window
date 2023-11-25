using System.ComponentModel;
using System.Windows.Forms;

namespace Homework.Model
{
    public class Model
    {
        public event ModelChangedEventHandler _modelChanged;
        public delegate void ModelChangedEventHandler();
        private const int MAX_PANEL_X = 490;
        private const int MAX_PANEL_Y = 415;
        private Shapes _shapesData;
        private ShapeFactory _shapeFactory;
        private IState _state;
        private string _shapeName;

        public Model()
        {
            _shapesData = new Shapes();
            _shapeFactory = new ShapeFactory();
            _shapeName = Constant.Constant.POINT;
            _state = new PointState();
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

        // change state to point
        public void ChangeStatePoint()
        {
            _state = new PointState();
        }

        // change state
        public void ChangeStateDrawing()
        {
            _state = new DrawingState();
        }

        // pointer pressed
        public void PressPointer(double mouseX, double mouseY)
        {
            _state.MouseDown(new Point(mouseX, mouseY), _shapeName, ref _shapesData, ref _shapeFactory);
        }

        // pointer moved
        public void MovePointer(double mouseX, double mouseY)
        {
            mouseX = CheckRangeOfX(mouseX);
            mouseY = CheckRangeOfY(mouseY);
            _state.MouseMove(new Point(mouseX, mouseY), ref _shapesData);
            NotifyModelChanged();
        }

        // pointer released
        public void ReleasePointer(double mouseX, double mouseY)
        {
            
            mouseX = CheckRangeOfX(mouseX);
            mouseY = CheckRangeOfY(mouseY);
            _state.MouseUp(new Point(mouseX, mouseY), _shapeName, ref _shapesData);
            NotifyModelChanged();
        }

        // draw
        public void Draw(IGraphics graphics)
        {
            graphics.ClearAll();
            foreach (Shape aShape in _shapesData.ShapeList)
                aShape.Draw(graphics);
            _state.Drawing(graphics, ref _shapesData);
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
