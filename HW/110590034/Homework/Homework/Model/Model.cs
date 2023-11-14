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
        private Shapes _shapesData;
        private ShapeFactory _shapeFactory;
        private Mode _mode;
        private IState _state;

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
            _mode = Mode.Pointer;
            _state = new DrawingState();
        }

        // set mode
        public void SetMode(Mode mode)
        {
            _mode = mode;
        }

        // pointer pressed
        public void PressPointer(double mouseX, double mouseY)
        {
            _state.MouseDown(mouseX, mouseY, _mode, ref _shapeFactory);
        }

        // pointer moved
        public void MovePointer(double mouseX, double mouseY)
        {
            mouseX = CheckRangeOfX(mouseX);
            mouseY = CheckRangeOfY(mouseY);
            _state.MouseMove(mouseX, mouseY);
            NotifyModelChanged();
        }

        // pointer released
        public void ReleasePointer(double mouseX, double mouseY)
        {
            
            mouseX = CheckRangeOfX(mouseX);
            mouseY = CheckRangeOfY(mouseY);
            _state.MouseUp(mouseX, mouseY, ref _mode, ref _shapesData);
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
    }
}
