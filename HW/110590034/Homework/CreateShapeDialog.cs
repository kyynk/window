using System;
using System.Drawing;
using System.Windows.Forms;

namespace Homework.View
{
    public partial class CreateShapeDialog : Form
    {
        private double _left;
        private double _top;
        private double _right;
        private double _bottom;
        private bool _isOk;
        private Size _canvasSize;

        public CreateShapeDialog()
        {
            InitializeComponent();
            ResetValue();
            _canvasSize = new Size(Constant.Constant.DEFAULT_MAX_PANEL_X, Constant.Constant.DEFAULT_MAX_PANEL_Y);

            _leftTextBox.TextChanged += HandleTextBoxTextChanged;
            _topTextBox.TextChanged += HandleTextBoxTextChanged;
            _rightTextBox.TextChanged += HandleTextBoxTextChanged;
            _bottomTextBox.TextChanged += HandleTextBoxTextChanged;

        }

        public bool IsOk
        {
            get
            {
                return _isOk;
            }
        }

        // click ok button
        private void HandleOkButtonClick(object sender, EventArgs e)
        {
            _isOk = true;
            Close();
        }

        // click cancel button
        private void HandleCancelButtonClick(object sender, EventArgs e)
        {
            _isOk = false;
            Close();
        }

        // handle text box text changed
        private void HandleTextBoxTextChanged(object sender, EventArgs e)
        {
            _okButton.Enabled = false;
            bool isTransferToDouble = double.TryParse(_leftTextBox.Text, out _left) && double.TryParse(_topTextBox.Text, out _top) && double.TryParse(_rightTextBox.Text, out _right) && double.TryParse(_bottomTextBox.Text, out _bottom);
            if (isTransferToDouble)
            {
                bool isInRange = IsPointInWidthRange(_left) && IsPointInHeightRange(_top) && IsPointInWidthRange(_right) && IsPointInHeightRange(_bottom);
                if (isInRange)
                {
                    _okButton.Enabled = _left < _right && _top < _bottom;
                }
            }
        }

        // get left
        public Model.Point GetLeftTop()
        {
            return new Model.Point(_left, _top);
        }

        // get right
        public Model.Point GetRightBottom()
        {
            return new Model.Point(_right, _bottom);
        }

        // set size
        public void SetCanvasSize(Size currentSize)
        {
            _canvasSize = currentSize;
        }

        // is x in range
        public bool IsPointInWidthRange(double pointX)
        {
            return (pointX >= 0 && pointX <= _canvasSize.Width);
        }

        // is y in range
        public bool IsPointInHeightRange(double pointY)
        {
            return (pointY >= 0 && pointY <= _canvasSize.Height);
        }

        // reset value
        public void ResetValue()
        {
            _leftTextBox.Text = "";
            _topTextBox.Text = "";
            _rightTextBox.Text = "";
            _bottomTextBox.Text = "";
            _left = -1;
            _top = -1;
            _right = -1;
            _bottom = -1;
            _isOk = false;
            _okButton.Enabled = false;
        }
    }
}
