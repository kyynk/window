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
    public partial class Form1 : Form
    {
        private readonly FormPresentationModel _presentationModel;
        private const string DELETE = "刪除";

        public Form1(FormPresentationModel presentationModel)
        {
            InitializeComponent();
            _presentationModel = presentationModel;
            RefreshButtonChecked();
        }

        // click line button
        private void ClickLineButton(object sender, EventArgs e)
        {
            _presentationModel.EnableLine();
            RefreshButtonChecked();
            Cursor.Current = Cursors.Cross;
        }

        // click rectangle button
        private void ClickRectangleButton(object sender, EventArgs e)
        {
            _presentationModel.EnableRectangle();
            RefreshButtonChecked();
            Cursor.Current = Cursors.Cross;
        }

        // click ellipse button
        private void ClickEllipseButton(object sender, EventArgs e)
        {
            _presentationModel.EnableEllipse();
            RefreshButtonChecked();
            Cursor.Current = Cursors.Cross;
        }

        // refresh checked
        private void RefreshButtonChecked()
        {
            _lineButton.Checked = _presentationModel.IsLineEnabled();
            _rectangleButton.Checked = _presentationModel.IsRectangleEnabled();
            _ellipseButton.Checked = _presentationModel.IsEllipseEnabled();
        }

        // click create button
        private void ClickCreateButton(object sender, EventArgs e)
        {
            _presentationModel.Create(_shapeTypeComboBox.Text);
            _shapeData.Rows.Add(DELETE, _presentationModel.GetNewShapeType(), _presentationModel.GetNewShapePosition());
        }

        // click delete button
        private void ClickDeleteButton(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0 && e.RowIndex >= 0)
            {
                DataGridViewRow rowToDelete = _shapeData.Rows[e.RowIndex];
                _shapeData.Rows.Remove(rowToDelete);
                _presentationModel.Delete(e.RowIndex);
            }
        }

        // update view
        private void UpdateView()
        {
            Invalidate();
        }
    }
}
