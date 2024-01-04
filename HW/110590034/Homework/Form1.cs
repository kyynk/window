using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Homework.PresentationModel;

namespace Homework.View
{
    public partial class Form1 : Form
    {
        private readonly FormPresentationModel _presentationModel;
        private CreateShapeDialog _createShapeDialog;
        private List<Button> _pageButtons;
        private int _currentPageIndex;

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
            _currentPageIndex = -1;
            AddPageButton();
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
            Button button = sender as Button;
            // Draw the contents of _canvas onto button with scaling
            _presentationModel.DrawOnButton(e.Graphics, button.Size, _canvas.Size);
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
        private void ClickeAddPageButton(object sender, EventArgs e)
        {
            AddPageButton();
        }

        // add page button
        public void AddPageButton()
        {
            int panelWidth = _splitContainer1.Panel1.Width;

            Button newPageButton = new Button();
            newPageButton.Click += SelectPage;
            newPageButton.BackColor = System.Drawing.Color.White;
            newPageButton.Paint += HandleButtonPaint;

            newPageButton.Width = panelWidth - Constant.Constant.SLIDE_MARGIN * Constant.Constant.TWO;
            newPageButton.Height = (int)((double)(newPageButton.Width) * Constant.Constant.PANEL_RATIO);

            // 新增到列表和選擇區
            // 插入到選擇的頁面後面一個位置
            int insertIndex = _currentPageIndex + 1;
            _pageButtons.Insert(insertIndex, newPageButton);
            _flowLayoutPanel.Controls.Add(newPageButton);

            SetCheckedPage(newPageButton, true);

            // 重新調整頁面順序
            UpdatePageOrder();

            // 切換當前頁面
            SwitchCurrentPage(insertIndex);
        }

        // update page order
        private void UpdatePageOrder()
        {
            // 更新頁面按鈕的順序
            for (int i = 0; i < _pageButtons.Count; i++)
            {
                _flowLayoutPanel.Controls.SetChildIndex(_pageButtons[i], i);
            }
        }

        // select page
        private void SelectPage(object sender, EventArgs e)
        {
            // 點擊頁面按鈕時切換當前頁面
            int pageIndex = _pageButtons.IndexOf(sender as Button);
            SwitchCurrentPage(pageIndex);
        }

        // switch current page
        private void SwitchCurrentPage(int pageIndex)
        {
            // 切換當前頁面的Checked
            if (_currentPageIndex >= 0 && _currentPageIndex < _pageButtons.Count)
            {
                SetCheckedPage(_pageButtons[_currentPageIndex], false);
            }

            _currentPageIndex = pageIndex;

            if (_currentPageIndex >= 0 && _currentPageIndex < _pageButtons.Count)
            {
                SetCheckedPage(_pageButtons[_currentPageIndex], true);
            }

            // 在此處切換畫面相應的繪圖操作
            // ...

        }

        // set checked page
        private void SetCheckedPage(Button button, bool isChecked)
        {
            // 設定Checked樣式
            if (isChecked)
            {
                button.FlatStyle = FlatStyle.Flat;
                button.FlatAppearance.BorderColor = System.Drawing.Color.Blue;
            }
            else
            {
                button.FlatStyle = FlatStyle.Standard;
                button.FlatAppearance.BorderColor = System.Drawing.Color.Empty;
            }
        }
    }
}
