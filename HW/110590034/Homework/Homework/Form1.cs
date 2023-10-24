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

        public Form1(FormPresentationModel presentationModel)
        {
            InitializeComponent();
            //
            // canvas
            //
            _canvas.BackColor = System.Drawing.Color.LightYellow;
            _canvas.MouseDown += HandleCanvasPressed;
            _canvas.MouseUp += HandleCanvasReleased;
            _canvas.MouseMove += HandleCanvasMoved;
            _canvas.Paint += HandleCanvasPaint;
            //
            // presentation model
            //
            _presentationModel = presentationModel;
            _presentationModel._modelChanged += HandleModelChanged;
            //
            // shape data (dataGridview)
            //
            _shapeData.AutoGenerateColumns = false;
            _shapeData.DataSource = _presentationModel.GetShapes();
            //
            // setting property for dataGridView (for header column)
            //
            _shapeType.DataPropertyName = "ShapeName";
            _information.DataPropertyName = "Info";
            //
            // initialize checked button
            //
            RefreshButtonChecked();
        }

        // handle canvas pressed
        public void HandleCanvasPressed(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            _presentationModel.PressPointer(e.X, e.Y);
        }

        // handle canvas released
        public void HandleCanvasReleased(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            _presentationModel.ReleasePointer(e.X, e.Y);
            RefreshButtonChecked();
            _canvas.Cursor = Cursors.Arrow;
        }

        // handle canvas moved
        public void HandleCanvasMoved(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            _presentationModel.MovePointer(e.X, e.Y);
        }

        // handle canvas paint
        public void HandleCanvasPaint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            _presentationModel.Draw(e.Graphics);
        }

        // click line button
        private void ClickLineButton(object sender, EventArgs e)
        {
            _presentationModel.EnableLine();
            RefreshButtonChecked();
            _canvas.Cursor = Cursors.Cross;
        }

        // click rectangle button
        private void ClickRectangleButton(object sender, EventArgs e)
        {
            _presentationModel.EnableRectangle();
            RefreshButtonChecked();
            _canvas.Cursor = Cursors.Cross;
        }

        // click ellipse button
        private void ClickEllipseButton(object sender, EventArgs e)
        {
            _presentationModel.EnableEllipse();
            RefreshButtonChecked();
            _canvas.Cursor = Cursors.Cross;
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
            _presentationModel.CreateShape(_shapeTypeComboBox.Text);
        }

        // click delete button
        private void ClickDeleteButton(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0 && e.RowIndex >= 0)
            {
                _presentationModel.DeleteShape(e.RowIndex);
            }
        }

        // update view
        private void HandleModelChanged()
        {
            Invalidate(true);
        }
    }
}
