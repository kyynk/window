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
        private readonly Model _model;
        private readonly FormPresentationModel _presentationModel;
        private const string DELETE = "刪除";

        public Form1(Model model, FormPresentationModel presentationModel)
        {
            InitializeComponent();
            _canvas.BackColor = System.Drawing.Color.LightYellow;
            _canvas.MouseDown += HandleCanvasPressed;
            _canvas.MouseUp += HandleCanvasReleased;
            _canvas.MouseMove += HandleCanvasMoved;
            _canvas.Paint += HandleCanvasPaint;
            _model = model;
            _presentationModel = presentationModel;
            _model._modelChanged += HandleModelChanged;
            RefreshButtonChecked();
        }

        // h
        public void HandleCanvasPressed(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            _model.PointerPressed(e.X, e.Y);
        }

        // h
        public void HandleCanvasReleased(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            _model.PointerReleased(e.X, e.Y);
        }

        // h
        public void HandleCanvasMoved(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            _model.PointerMoved(e.X, e.Y);
        }

        // h
        public void HandleCanvasPaint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            _presentationModel.Draw(e.Graphics);
        }

        // click line button
        private void ClickLineButton(object sender, EventArgs e)
        {
            _presentationModel.EnableLine();
            RefreshButtonChecked();
            Cursor = Cursors.Cross;
        }

        // click rectangle button
        private void ClickRectangleButton(object sender, EventArgs e)
        {
            _presentationModel.EnableRectangle();
            RefreshButtonChecked();
            Cursor = Cursors.Cross;
        }

        // click ellipse button
        private void ClickEllipseButton(object sender, EventArgs e)
        {
            _presentationModel.EnableEllipse();
            RefreshButtonChecked();
            Cursor = Cursors.Cross;
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
            _model.Create(_shapeTypeComboBox.Text);
            _shapeData.Rows.Add(DELETE, _model.GetNewShapeType(), _model.GetNewShapePosition());
        }

        // click delete button
        private void ClickDeleteButton(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0 && e.RowIndex >= 0)
            {
                DataGridViewRow rowToDelete = _shapeData.Rows[e.RowIndex];
                _shapeData.Rows.Remove(rowToDelete);
                _model.Delete(e.RowIndex);
            }
        }

        // update view
        private void HandleModelChanged()
        {
            Invalidate(true);
        }
    }
}
