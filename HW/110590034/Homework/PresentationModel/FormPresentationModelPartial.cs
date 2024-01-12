using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing;
using Homework.Model;
using Homework.View;
using System;
using System.Threading.Tasks;

namespace Homework.PresentationModel
{
    public partial class FormPresentationModel : INotifyPropertyChanged
    {
        public delegate void SaveButtonEventHandler(bool isEnabled);
        public event SaveButtonEventHandler _saveButtonChanged;

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

        // for unit test
        public Cursor UsingCursor
        {
            set;
            get;
        }

        public bool IsPressed
        {
            set
            {
                _isPressed = value;
            }
            get
            {
                return _isPressed;
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

        // is undo enabled
        public bool IsUndoEnabled
        {
            get
            {
                return _model.IsUndoEnabled;
            }
        }

        // is redo enabled
        public bool IsRedoEnabled
        {
            get
            {
                return _model.IsRedoEnabled;
            }
        }

        // property changed
        public void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        // slide index
        public int SlideIndex
        {
            get
            {
                return _model.PageIndex;
            }
            set
            {
                _model.PageIndex = value;
            }
        }

        // save
        public async void Save()
        {
            _saveButtonChanged(false);
            var task = Task.Run(() => _model.Save());
            await task;
            _saveButtonChanged(true);
        }

        // load
        public void Load()
        {

        }
    }
}
