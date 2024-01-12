using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Homework.View
{
    public partial class SaveDialog : Form
    {
        private bool _isOk;

        public SaveDialog()
        {
            InitializeComponent();
            ResetValue();
        }

        public bool IsOk
        {
            get
            {
                return _isOk;
            }
        }

        // reset value
        public void ResetValue()
        {
            _isOk = false;
        }

        // handle click ok button
        private void HandleOkButtonClick(object sender, EventArgs e)
        {
            _isOk = true;
            Close();
        }

        // handle click cancel button
        private void HandleCancelButtonClick(object sender, EventArgs e)
        {
            _isOk = false;
            Close();
        }
    }
}
