using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Homework.PresentationModel;

namespace Homework.View
{
    public partial class LoadDialog : Form
    {
        private bool _isOk;

        public LoadDialog()
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

        // handle ok button click
        private void HandleOkButtonClick(object sender, EventArgs e)
        {
            _isOk = true;
            Close();
        }

        // handle cancel button click
        private void HandleCancelButtonClick(object sender, EventArgs e)
        {
            _isOk = false;
            Close();
        }
    }
}
