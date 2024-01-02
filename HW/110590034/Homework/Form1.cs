using System;
using System.Windows.Forms;
using Homework.PresentationModel;

namespace Homework.View
{
    public partial class Form1 : Form
    {
        private readonly FormPresentationModel _presentationModel;
        private CreateShapeDialog _createShapeDialog;

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
            _presentationModel._cursorChanged += SetCursor;
            //_presentationModel._cursorChanged += SetCursor;
            // shape data (dataGridview)
            _shapeData.AutoGenerateColumns = false;
            _shapeData.DataSource = _presentationModel.GetShapes();
            // setting property for dataGridView (for header column)
            _shapeType.DataPropertyName = "ShapeName";
            _information.DataPropertyName = "Info";
            // tool strip binding button
            //_undoButton.DataBindings.Add(Constant.Constant.ENABLED, _presentationModel.IsUndoEnabled, Constant.Constant.IS_UNDO_ENABLED);
            //_redoButton.DataBindings.Add(Constant.Constant.ENABLED, _presentationModel.IsRedoEnabled, Constant.Constant.IS_REDO_ENABLED);
            _lineButton.DataBindings.Add(Constant.Constant.CHECKED, _presentationModel, Constant.Constant.IS_LINE_ENABLED);
            _rectangleButton.DataBindings.Add(Constant.Constant.CHECKED, _presentationModel, Constant.Constant.IS_RECTANGLE_ENABLED);
            _ellipseButton.DataBindings.Add(Constant.Constant.CHECKED, _presentationModel, Constant.Constant.IS_ELLIPSE_ENABLED);
            _defaultCursorButton.DataBindings.Add(Constant.Constant.CHECKED, _presentationModel, Constant.Constant.IS_CURSOR_ENABLED);
            // keyboard
            KeyDown += HandleKeyDown;
            KeyPreview = true;
            // create shape dialog
            _createShapeDialog = new CreateShapeDialog();
            // initialize
            UpdateUndoRedo();
            InitializeCanvasSize();
            _splitContainer1.Panel1.SizeChanged += ChangeLeftPanelSize;
            _splitContainer2.Panel1.SizeChanged += ChangeMiddlePanelSize;
        }

        // initialize canva size
        public void InitializeCanvasSize()
        {
            _canvas1.Width = Constant.Constant.DEFAULT_MAX_BUTTON_X;
            _canvas1.Height = Constant.Constant.DEFAULT_MAX_BUTTON_Y;
            _canvas.Width = Constant.Constant.DEFAULT_MAX_PANEL_X;
            _canvas.Height = Constant.Constant.DEFAULT_MAX_PANEL_Y;
            //Console.WriteLine("location");
            //Console.WriteLine("btn x " + _canvas1.Location.X + " btn y " + _canvas1.Location.Y);
            //Console.WriteLine("btn)) x " + (_canvas1.Location.X + _canvas1.Width) + " pnl111 x " + _splitContainer1.Panel1.Width);
            //Console.WriteLine("spliter x " + _splitContainer1.SplitterWidth);
            //Console.WriteLine("pnl x " + _canvas.Location.X + " pnl y " + _canvas.Location.Y);
            //Console.WriteLine("pnl)) x " + (_canvas.Location.X + _canvas.Width) + " pnl222 x " + _splitContainer2.Panel1.Width);
            //Console.WriteLine("spliter x " + _splitContainer1.SplitterWidth + " ???2 x " + (_splitContainer2.Panel1.Width - _splitContainer2.SplitterWidth));
        }

        // button size change
        private void ChangeLeftPanelSize(object sender, EventArgs e)
        {
            int panelWidth = _splitContainer1.Panel1.Width;
            _canvas1.Width = panelWidth - Constant.Constant.SLIDE_LOCATION_X * Constant.Constant.TWO;
            _canvas1.Height = (int)((double)(_canvas1.Width) * Constant.Constant.PANEL_RATIO);
        }

        // panel size change
        private void ChangeMiddlePanelSize(object sender, EventArgs e)
        {            
            int panelWidth = _splitContainer2.Panel1.Width;
            int panelHeight = _splitContainer2.Panel1.Height;

            //Console.WriteLine("spliter width: "+ panelWidth);
            //Console.WriteLine("spliter height: " + panelHeight);
            //Console.WriteLine("spliter Ratio: " + (double)((double)panelHeight / (double)panelWidth));
            //Console.WriteLine("need Ratio: " + Constant.Constant.PANEL_RATIO);


            if ((double)(panelHeight - Constant.Constant.TWO * Constant.Constant.PANEL_MARGIN) / (double)(panelWidth - Constant.Constant.TWO * Constant.Constant.PANEL_MARGIN) < Constant.Constant.PANEL_RATIO)
            {
                _canvas.Height = panelHeight - (Constant.Constant.TWO * Constant.Constant.PANEL_MARGIN);
                _canvas.Width = (int)((double)(_canvas.Height) / Constant.Constant.PANEL_RATIO);
            }
            else
            {
                _canvas.Width = panelWidth - (Constant.Constant.TWO * Constant.Constant.PANEL_MARGIN);
                _canvas.Height = (int)((double)(_canvas.Width) * Constant.Constant.PANEL_RATIO);
            }

            //Console.WriteLine("canvas width: " + _canvas.Width);
            //Console.WriteLine("canvas height: " + _canvas.Height);
            //Console.WriteLine("canvas Ratio: " + (double)((double)_canvas.Height / (double)_canvas.Width));

            _canvas.Location = new System.Drawing.Point((panelWidth - _canvas.Width) / Constant.Constant.TWO, (panelHeight - _canvas.Height) / Constant.Constant.TWO);

            _presentationModel.SetPanelSize((double)_canvas.Width);
        }

        // handle canvas pressed
        public void HandleCanvasPressed(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            _presentationModel.PressPointer(e.X, e.Y);
            UpdateUndoRedo();
        }

        // handle canvas released
        public void HandleCanvasReleased(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            _presentationModel.ReleasePointer(e.X, e.Y);
            //_canvas.Cursor = Cursors.Arrow;
            UpdateUndoRedo();
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
            //_canvas.Cursor = Cursors.Cross;
        }

        // click rectangle button
        private void ClickRectangleButton(object sender, EventArgs e)
        {
            _presentationModel.EnableRectangle();
            //_canvas.Cursor = Cursors.Cross;
        }

        // click ellipse button
        private void ClickEllipseButton(object sender, EventArgs e)
        {
            _presentationModel.EnableEllipse();
            //_canvas.Cursor = Cursors.Cross;
        }

        // click default cursor button
        private void ClickDefaultCursorButton(object sender, EventArgs e)
        {
            _presentationModel.EnableDefaultCursor();
            //_canvas.Cursor = Cursors.Arrow;
        }

        // click undo button
        private void ClickUndoButton(object sender, EventArgs e)
        {
            _presentationModel.Undo();
            UpdateUndoRedo();
        }

        // click redo button
        private void ClickRedoButton(object sender, EventArgs e)
        {
            _presentationModel.Redo();
            UpdateUndoRedo();
        }

        // click create button
        private void ClickCreateButton(object sender, EventArgs e)
        {
            if (_shapeTypeComboBox.Text != "")
            {
                _createShapeDialog.ResetValue();
                _createShapeDialog.SetCavasSize(_canvas.Size);
                _createShapeDialog.ShowDialog();
                if (_createShapeDialog.IsOk)
                {
                    _presentationModel.CreateShape(_shapeTypeComboBox.Text, _createShapeDialog.GetLeftTop(), _createShapeDialog.GetRightBottom());
                }
            }
            // testing...
            UpdateUndoRedo();
        }

        // click delete button
        private void ClickDeleteButton(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0 && e.RowIndex >= 0)
            {
                _presentationModel.DeleteShape(e.RowIndex);
                UpdateUndoRedo();
            }
        }

        // handle key down
        private void HandleKeyDown(object sender, KeyEventArgs e)
        {
            _presentationModel.HandleKeyDown(e.KeyCode);
            UpdateUndoRedo();
        }

        // handle cursor changed
        private void SetCursor(Cursor cursorType)
        {
            _canvas.Cursor = cursorType;
        }

        // update view
        private void HandleModelChanged()
        {
            Invalidate(true);
        }

        // update undo redo
        private void UpdateUndoRedo()
        {
            _undoButton.Enabled = _presentationModel.IsUndoEnabled;
            _redoButton.Enabled = _presentationModel.IsRedoEnabled;
        }

        // click add page button
        private void _addPageButton_Click(object sender, EventArgs e)
        {

        }
    }
}
