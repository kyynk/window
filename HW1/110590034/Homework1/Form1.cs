using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Homework1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            const double ZERO = 0.0;
            const string PLUS = "+";
            InitializeComponent();
            _model = new Model(ZERO, ZERO.ToString(), PLUS, true);
        }

        // add number to buffer
        private void ClickButtonNumber(object sender, EventArgs e)
        {
            Button buttonNumber = (Button)sender;
            _model.AddNumber(buttonNumber.Text);
            _textBoxResult.Text = _model.GetBuffer();
        }

        // add point to buffer
        private void ClickButtonPoint(object sender, EventArgs e)
        {
            _model.AddPoint();
            _textBoxResult.Text = _model.GetBuffer();
        }

        // clear entry
        private void ClickButtonClearEntry(object sender, EventArgs e)
        {
            _model.ClearEntry();
            _textBoxResult.Text = _model.GetBuffer();
        }

        // clear all
        private void ClickButtonClear(object sender, EventArgs e)
        {
            _model.Clear();
            _textBoxResult.Text = _model.GetBuffer();
        }

        // perform operation (only +-*/)
        private void ClickButtonOperation(object sender, EventArgs e)
        {
            Button buttonOperation = (Button)sender;
            _model.SetOperation(buttonOperation.Text);
        }

        // equal (use former operator and buffer)
        private void ClickButtonEqual(object sender, EventArgs e)
        {
            _model.Equal();
            _textBoxResult.Text = _model.GetMemory().ToString();
        }
    }
}