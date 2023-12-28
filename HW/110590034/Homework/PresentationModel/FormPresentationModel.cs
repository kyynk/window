using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing;
using Homework.Model;
using Homework.View;
using System;

namespace Homework.PresentationModel
{
    public partial class FormPresentationModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public event Model.Model.ModelChangedEventHandler _modelChanged;
        public delegate void CursorChangedEventHandler(Cursor cursor);
        public event CursorChangedEventHandler _cursorChanged;
        private Model.Model _model;
        private bool _isPressed;
        private bool _isLineEnabled;
        private bool _isRectangleEnabled;
        private bool _isEllipseEnabled;
        private bool _isDefaultCursorEnabled;
        private FormGraphicsAdaptor _graphicsAdaptor;

        public FormPresentationModel(Model.Model model)
        {
            _model = model;
            IsPressed = false;
            IsLineEnabled = false;
            IsRectangleEnabled = false;
            IsEllipseEnabled = false;
            IsDefaultCursorEnabled = true;
            UsingCursor = Cursors.Arrow;
            _model._modelChanged += HandleModelChanged;
        }

        // get shapes
        public BindingList<Shape> GetShapes()
        {
            return _model.GetShapes();
        }

        // create shape
        public void CreateShape(string shapeType, Model.Point point1, Model.Point point2)
        {
            if (shapeType != "")
                _model.Create(shapeType, point1, point2);
        }

        // remove shape by index
        public void DeleteShape(int index)
        {
            _model.Delete(index);
        }

        // check resize state
        public bool IsResizeState(double mouseX, double mouseY)
        {
            return _model.CheckIsResizeState(mouseX, mouseY);
        }

        // check location is right bottom
        public bool IsLocationRightBottom(double mouseX, double mouseY)
        {
            return _model.CheckLocationIsRightBottom(mouseX, mouseY);
        }

        // pointer press
        public void PressPointer(double mouseX, double mouseY)
        {
            if (IsDefaultCursorEnabled)
            {
                if (IsResizeState(mouseX, mouseY))
                {
                    //Console.WriteLine("aaaaaaaa");
                    _model.ChangeState(Constant.Constant.RESIZE_STATE);
                }
                else
                {
                    //Console.WriteLine("cccccccc");
                    _model.ChangeState(Constant.Constant.POINT_STATE);
                }
            }
            //Console.WriteLine("bbbbbbb");
            IsPressed = true;
            _model.PressPointer(mouseX, mouseY);
        }

        // pointer move
        public void MovePointer(double mouseX, double mouseY)
        {
            _model.MovePointer(mouseX, mouseY);
            if (!IsPressed && _model.ShapeName == Constant.Constant.POINT)
            {
                if (IsLocationRightBottom(mouseX, mouseY))
                {
                    _cursorChanged(Cursors.SizeNWSE);
                    UsingCursor = Cursors.SizeNWSE;
                }
                else
                {
                    _cursorChanged(Cursors.Arrow);
                    UsingCursor = Cursors.Arrow;
                }
            }
        }

        // pointer press
        public void ReleasePointer(double mouseX, double mouseY)
        {
            _model.ReleasePointer(mouseX, mouseY);
            IsPressed = false;
            if (_model.ShapeName == Constant.Constant.POINT)
            {
                ResetBoolean();
            }
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
            _cursorChanged(Cursors.Cross);
            UsingCursor = Cursors.Cross;
        }

        // line enable
        public void EnableRectangle()
        {
            SetMode(Constant.Constant.RECTANGLE);
            _cursorChanged(Cursors.Cross);
            UsingCursor = Cursors.Cross;
        }

        // line enable
        public void EnableEllipse()
        {
            SetMode(Constant.Constant.ELLIPSE);
            _cursorChanged(Cursors.Cross);
            UsingCursor = Cursors.Cross;
        }

        // default cursor enable
        public void EnableDefaultCursor()
        {
            SetMode(Constant.Constant.POINT);
            _cursorChanged(Cursors.Arrow);
            UsingCursor = Cursors.Arrow;
        }

        // undo
        public void Undo()
        {
            _model.Undo();
        }

        // redo
        public void Redo()
        {
            _model.Redo();
        }

        // handle key down
        public void HandleKeyDown(Keys keyCode)
        {
            _model.HandleKeyDown(keyCode);
        }

        // set panel size
        public void SetPanelSize(double width)
        {
            _model.SetPanelSize(width);
        }
    }
}
