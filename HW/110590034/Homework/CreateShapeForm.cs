using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Homework
{
    public partial class CreateShapeForm : Form
    {
        private double _left;
        private double _top;
        private double _right;
        private double _bottom;
        private bool _isOk;
        private Size _canvasSize;

        public CreateShapeForm()
        {
            InitializeComponent();
            _left = -1;
            _top = -1;
            _right = -1;
            _bottom = -1;
            _isOk = false;
            _canvasSize = new Size(Constant.Constant.DEFAULT_MAX_PANEL_X, Constant.Constant.DEFAULT_MAX_PANEL_Y);

            _leftTextBox.TextChanged += HandleTextBoxTextChanged;
            _topTextBox.TextChanged += HandleTextBoxTextChanged;
            _rightTextBox.TextChanged += HandleTextBoxTextChanged;
            _bottomTextBox.TextChanged += HandleTextBoxTextChanged;
            _okButton.Enabled = false;
        }

        public bool IsOk
        {
            get
            {
                return _isOk;
            }
        }

        // click ok button
        private void _okButton_Click(object sender, EventArgs e)
        {
            _isOk = true;
        }

        // click cancel button
        private void _cancelButton_Click(object sender, EventArgs e)
        {
            _isOk = false;
        }

        // handle text box text changed
        private void HandleTextBoxTextChanged(object sender, EventArgs e)
        {
            _okButton.Enabled = false;
            bool isTransferToDouble = double.TryParse(_leftTextBox.Text, out _left) && double.TryParse(_topTextBox.Text, out _top) && double.TryParse(_rightTextBox.Text, out _right) && double.TryParse(_bottomTextBox.Text, out _bottom);
            if (isTransferToDouble)
            {
                bool isInRange = IsXInRange(_left) && IsYInRange(_top) && IsXInRange(_right) && IsYInRange(_bottom);
                if (isInRange)
                {
                    _okButton.Enabled = _left < _right && _top < _bottom;
                }
            }
        }

        // get left
        public double GetLeft()
        {
            return _left;
        }

        // get top
        public double GetTop()
        {
            return _top;
        }

        // get right
        public double GetRight()
        {
            return _right;
        }

        // get bottom
        public double GetBottom()
        {
            return _bottom;
        }

        // set size
        public void SetCavasSize(Size currentSize)
        {
            _canvasSize = currentSize;
        }

        // is x in range
        public bool IsXInRange(double pointX)
        {
            return (pointX >= 0 && pointX <= _canvasSize.Width);
        }

        // is y in range
        public bool IsYInRange(double pointY)
        {
            return (pointY >= 0 && pointY <= _canvasSize.Height);
        }
    }
}
