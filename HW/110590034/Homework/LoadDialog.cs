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
    public partial class LoadDialog : Form
    {
        private bool _isOk;

        public LoadDialog()
        {
            InitializeComponent();
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
    }
}
