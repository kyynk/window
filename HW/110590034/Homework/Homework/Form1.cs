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
            // canvas
            _canvas.BackColor = System.Drawing.Color.White;
            _canvas.MouseDown += HandleCanvasPressed;
            _canvas.MouseUp += HandleCanvasReleased;
            _canvas.MouseMove += HandleCanvasMoved;
            _canvas.Paint += HandleCanvasPaint;
            // canvas1
            _canvas1.Paint += HandleButtonPaint;
            // presentation model
            _presentationModel = presentationModel;
            _presentationModel._modelChanged += HandleModelChanged;
            // shape data (dataGridview)
            _shapeData.AutoGenerateColumns = false;
            _shapeData.DataSource = _presentationModel.GetShapes();
            // setting property for dataGridView (for header column)
            _shapeType.DataPropertyName = "ShapeName";
            _information.DataPropertyName = "Info";
            // tool strip binding button
            _lineButton.DataBindings.Add(Constant.CHECKED, _presentationModel, Constant.IS_LINE_ENABLED);
            _rectangleButton.DataBindings.Add(Constant.CHECKED, _presentationModel, Constant.IS_RECTANGLE_ENABLED);
            _ellipseButton.DataBindings.Add(Constant.CHECKED, _presentationModel, Constant.IS_ELLIPSE_ENABLED);
            _defaultCursorButton.DataBindings.Add(Constant.CHECKED, _presentationModel, Constant.IS_CURSOR_ENABLED);
            // keyboard
            KeyDown += HandleKeyDown;
            KeyPreview = true;
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

        // handle canvas1 paint
        public void HandleButtonPaint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            // Draw the contents of _canvas onto _canvas1 with scaling
            _presentationModel.DrawOnButton(e.Graphics, _canvas1.Size, _canvas.Size);
        }

        // click line button
        private void ClickLineButton(object sender, EventArgs e)
        {
            _presentationModel.EnableLine();
            _canvas.Cursor = Cursors.Cross;
        }

        // click rectangle button
        private void ClickRectangleButton(object sender, EventArgs e)
        {
            _presentationModel.EnableRectangle();
            _canvas.Cursor = Cursors.Cross;
        }

        // click ellipse button
        private void ClickEllipseButton(object sender, EventArgs e)
        {
            _presentationModel.EnableEllipse();
            _canvas.Cursor = Cursors.Cross;
        }

        // click default cursor button
        private void ClickDefaultCursorButton(object sender, EventArgs e)
        {
            _presentationModel.EnableDefaultCursor();
            _canvas.Cursor = Cursors.Arrow;
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

        // handle key down
        private void HandleKeyDown(object sender, KeyEventArgs e)
        {
            _presentationModel.HandleKeyDown(e.KeyCode);
        }

        // update view
        private void HandleModelChanged()
        {
            Invalidate(true);
        }
    }
}
