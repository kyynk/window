using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing;
using Homework.Model;
using Homework.View;

namespace Homework.PresentationModel
{
    public class FormPresentationModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public event Model.Model.ModelChangedEventHandler _modelChanged;
        public delegate void CursorChangedEventHandler(Cursor cursor);
        public event CursorChangedEventHandler _cursorChanged;
        private Model.Model _model;
        private bool _isLineEnabled;
        private bool _isRectangleEnabled;
        private bool _isEllipseEnabled;
        private bool _isDefaultCursorEnabled;
        private FormGraphicsAdaptor _graphicsAdaptor;

        public FormPresentationModel(Model.Model model)
        {
            _model = model;
            IsLineEnabled = false;
            IsRectangleEnabled = false;
            IsEllipseEnabled = false;
            IsDefaultCursorEnabled = true;
            _model._modelChanged += HandleModelChanged;
        }

        // get shapes
        public BindingList<Shape> GetShapes()
        {
            return _model.GetShapes();
        }

        // create shape
        public void CreateShape(string shapeType)
        {
            if (shapeType != "")
                _model.Create(shapeType);
        }

        // remove shape by index
        public void DeleteShape(int index)
        {
            _model.Delete(index);
        }

        // pointer press
        public void PressPointer(double mouseX, double mouseY)
        {
            _model.PressPointer(mouseX, mouseY);
        }

        // pointer move
        public void MovePointer(double mouseX, double mouseY)
        {
            _model.MovePointer(mouseX, mouseY);
        }

        // pointer press
        public void ReleasePointer(double mouseX, double mouseY)
        {
            _model.ReleasePointer(mouseX, mouseY);
            if (_model.ShapeName == Constant.Constant.POINT)
                ResetBoolean();
            else
                EnableDefaultCursor();
        }

        // handle model change
        public void HandleModelChanged()
        {
            if (_modelChanged != null)
                _modelChanged();
        }

        // draw
        public void Draw(Graphics graphics)
        {
            // graphics物件是Paint事件帶進來的，只能在當次Paint使用
            // 而Adaptor又直接使用graphics，這樣DoubleBuffer才能正確運作
            // 因此，Adaptor不能重複使用，每次都要重新new
            _graphicsAdaptor = new FormGraphicsAdaptor(graphics);
            _model.Draw(_graphicsAdaptor);
        }

        // draw
        public void DrawOnButton(Graphics graphics, Size buttonSize, Size canvasSize)
        {
            // Calculate scaling factors
            float scaleX = (float)buttonSize.Width / canvasSize.Width;
            float scaleY = (float)buttonSize.Height / canvasSize.Height;
            // Apply scaling to the graphics object
            graphics.ScaleTransform(scaleX, scaleY);

            _graphicsAdaptor = new FormGraphicsAdaptor(graphics);
            _model.Draw(_graphicsAdaptor);
        }

        // reset boolean
        public void ResetBoolean()
        {
            IsLineEnabled = false;
            IsRectangleEnabled = false;
            IsEllipseEnabled = false;
            IsDefaultCursorEnabled = true;
        }

        // set state
        public void SetState(string name)
        {
            if (name == Constant.Constant.POINT)
                _model.ChangeState(Constant.Constant.POINT_STATE);
            else
            {
                _model.ChangeState(Constant.Constant.DRAWING_STATE);
                IsDefaultCursorEnabled = false;
            }
        }

        // set mode
        public void SetMode(string name)
        {
            ResetBoolean();
            SetState(name);
            if (name == Constant.Constant.LINE)
            {
                IsLineEnabled = true;
            }
            else if (name == Constant.Constant.RECTANGLE)
            {
                IsRectangleEnabled = true;
            }
            else if (name == Constant.Constant.ELLIPSE)
            {
                IsEllipseEnabled = true;
            }
            ShapeName = name;
        }

        // line enable
        public void EnableLine()
        {
            SetMode(Constant.Constant.LINE);
        }

        // line enable
        public void EnableRectangle()
        {
            SetMode(Constant.Constant.RECTANGLE);
        }

        // line enable
        public void EnableEllipse()
        {
            SetMode(Constant.Constant.ELLIPSE);
        }

        // default cursor enable
        public void EnableDefaultCursor()
        {
            SetMode(Constant.Constant.POINT);
        }

        // shape name
        public string ShapeName
        {
            get
            {
                return _model.ShapeName;
            }
            set
            {
                _model.ShapeName = value;
            }
        }

        // is line enabled
        public bool IsLineEnabled
        {
            set
            {
                _isLineEnabled = value;
                NotifyPropertyChanged(Constant.Constant.IS_LINE_ENABLED);
            }
            get
            {
                return _isLineEnabled;
            }
        }

        // is rectangle enabled
        public bool IsRectangleEnabled
        {
            set
            {
                _isRectangleEnabled = value;
                NotifyPropertyChanged(Constant.Constant.IS_RECTANGLE_ENABLED);
            }
            get
            {
                return _isRectangleEnabled;
            }
        }

        // is ellipse enabled
        public bool IsEllipseEnabled
        {
            set
            {
                _isEllipseEnabled = value;
                NotifyPropertyChanged(Constant.Constant.IS_ELLIPSE_ENABLED);
            }
            get
            {
                return _isEllipseEnabled;
            }
        }

        // is default cursor enabled
        public bool IsDefaultCursorEnabled
        {
            set
            {
                _isDefaultCursorEnabled = value;
                NotifyPropertyChanged(Constant.Constant.IS_CURSOR_ENABLED);
            }
            get
            {
                return _isDefaultCursorEnabled;
            }
        }

        // handle key down
        public void HandleKeyDown(Keys keyCode)
        {
            _model.HandleKeyDown(keyCode);
        }

        // handle cursor changed
        public void HandleCursorChanged(Cursor newCursor)
        {
            
        }

        // property changed
        public void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
