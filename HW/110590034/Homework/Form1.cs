using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Homework.PresentationModel;

namespace Homework.View
{
    public partial class Form1 : Form
    {
        private readonly FormPresentationModel _presentationModel;
        private CreateShapeDialog _createShapeDialog;
        private SaveDialog _saveDialog;
        private LoadDialog _loadDialog;
        private List<Button> _pageButtons;
        private Bitmap _bitmap;

        public Form1(FormPresentationModel presentationModel)
        {
            InitializeComponent();
            // canvas
            _canvas.BackColor = System.Drawing.Color.White;
            _canvas.MouseDown += HandleCanvasPressed;
            _canvas.MouseUp += HandleCanvasReleased;
            _canvas.MouseMove += HandleCanvasMoved;
            _canvas.Paint += HandleCanvasPaint;
            // page button
            _flowLayoutPanel.WrapContents = false;
            _pageButtons = new List<Button>();
            // presentation model
            _presentationModel = presentationModel;
            _presentationModel._modelChanged += HandleModelChanged;
            _presentationModel._cursorChanged += SetCursor;
            _presentationModel._pagesChanged += HandlePagesChanged;
            _presentationModel._undoRedoChanged += HandleUndoRedoChanged;
            _presentationModel._saveButtonChanged += HandleSaveButtonChanged;
            // shape data (dataGridview)
            _shapeData.AutoGenerateColumns = false;
            _shapeData.DataSource = _presentationModel.GetShapes();
            // setting property for dataGridView (for header column)
            _shapeType.DataPropertyName = "ShapeName";
            _information.DataPropertyName = "Info";
            // tool strip binding button
            _lineButton.DataBindings.Add(Constant.Constant.CHECKED, _presentationModel, Constant.Constant.IS_LINE_ENABLED);
            _rectangleButton.DataBindings.Add(Constant.Constant.CHECKED, _presentationModel, Constant.Constant.IS_RECTANGLE_ENABLED);
            _ellipseButton.DataBindings.Add(Constant.Constant.CHECKED, _presentationModel, Constant.Constant.IS_ELLIPSE_ENABLED);
            _defaultCursorButton.DataBindings.Add(Constant.Constant.CHECKED, _presentationModel, Constant.Constant.IS_CURSOR_ENABLED);
            // keyboard
            KeyDown += HandleKeyDown;
            KeyPreview = true;
            // dialogs
            _createShapeDialog = new CreateShapeDialog();
            _saveDialog = new SaveDialog();
            _loadDialog = new LoadDialog();
            // initialize
            UpdateUndoRedo();
            InitializeCanvasSize();
            _splitContainer1.Panel1.SizeChanged += ChangeLeftPanelSize;
            _splitContainer2.Panel1.SizeChanged += ChangeMiddlePanelSize;
            _presentationModel.SlideIndex = 0;
            AddPageButton(0);
            _pageButtons[0].Focus();
        }

        // initialize canva size
        public void InitializeCanvasSize()
        {
            _canvas.Width = Constant.Constant.DEFAULT_MAX_PANEL_X;
            _canvas.Height = Constant.Constant.DEFAULT_MAX_PANEL_Y;
        }

        // button size change
        private void ChangeLeftPanelSize(object sender, EventArgs e)
        {
            int panelWidth = _splitContainer1.Panel1.Width;
            _flowLayoutPanel.Width = panelWidth;
            _flowLayoutPanel.Height = _splitContainer1.Panel1.Height;
            for (var i = 0; i < _flowLayoutPanel.Controls.Count; i++)
            {
                _flowLayoutPanel.Controls[i].Width = _splitContainer1.Panel1.Width - Constant.Constant.SLIDE_MARGIN * Constant.Constant.TWO;
                _flowLayoutPanel.Controls[i].Height = (int)((double)_flowLayoutPanel.Controls[i].Width * Constant.Constant.PANEL_RATIO);
            }
        }

        // panel size change
        private void ChangeMiddlePanelSize(object sender, EventArgs e)
        {            
            int panelWidth = _splitContainer2.Panel1.Width;
            int panelHeight = _splitContainer2.Panel1.Height;

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

            _canvas.Location = new System.Drawing.Point((panelWidth - _canvas.Width) / Constant.Constant.TWO, (panelHeight - _canvas.Height) / Constant.Constant.TWO);

            _presentationModel.SetPanelSize((double)_canvas.Width);
        }

        // handle canvas pressed
        public void HandleCanvasPressed(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            _presentationModel.PressPointer(e.X, e.Y);
            //UpdateUndoRedo();
        }

        // handle canvas released
        public void HandleCanvasReleased(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            _presentationModel.ReleasePointer(e.X, e.Y);
            //_canvas.Cursor = Cursors.Arrow;
            //UpdateUndoRedo();
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
        public void HandleButtonPaint()
        {
            //Console.WriteLine("now page index is " + _presentationModel.SlideIndex);
            //Console.WriteLine("now flow layout panel is " + _flowLayoutPanel.Controls.Count);
            //Button button = sender as Button;
            //// Draw the contents of _canvas onto button with scaling
            //_presentationModel.DrawOnButton(e.Graphics, button.Size, _canvas.Size);
            _bitmap = new Bitmap(_canvas.Width, _canvas.Height);
            _canvas.DrawToBitmap(_bitmap, new System.Drawing.Rectangle(0, 0, _canvas.Width, _canvas.Height));
            // slide1.Image = new Bitmap(_brief, slide1.Size);
            int nowIndex = _presentationModel.SlideIndex;
            _flowLayoutPanel.Controls[nowIndex].BackgroundImage = new Bitmap(_bitmap, _flowLayoutPanel.Controls[nowIndex].Size);
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
            //UpdateUndoRedo();
        }

        // click redo button
        private void ClickRedoButton(object sender, EventArgs e)
        {
            _presentationModel.Redo();
            //UpdateUndoRedo();
        }

        // click create button
        private void ClickCreateButton(object sender, EventArgs e)
        {
            if (_shapeTypeComboBox.Text != "")
            {
                _createShapeDialog.ResetValue();
                _createShapeDialog.SetCanvasSize(_canvas.Size);
                _createShapeDialog.ShowDialog();
                if (_createShapeDialog.IsOk)
                {
                    _presentationModel.CreateShape(_shapeTypeComboBox.Text, _createShapeDialog.GetLeftTop(), _createShapeDialog.GetRightBottom());
                }
            }
            //UpdateUndoRedo();
        }

        // click delete button
        private void ClickDeleteButton(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0 && e.RowIndex >= 0)
            {
                _presentationModel.DeleteShape(e.RowIndex);
                //UpdateUndoRedo();
            }
        }

        // handle key down
        private void HandleKeyDown(object sender, KeyEventArgs e)
        {
            _presentationModel.HandleKeyDown(e.KeyCode);
            //UpdateUndoRedo();
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
            HandleButtonPaint();
        }
    }
}
