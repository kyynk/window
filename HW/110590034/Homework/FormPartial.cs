using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Homework.PresentationModel;

namespace Homework.View
{
    public partial class Form1
    {
        // update undo redo
        private void UpdateUndoRedo()
        {
            _undoButton.Enabled = _presentationModel.IsUndoEnabled;
            _redoButton.Enabled = _presentationModel.IsRedoEnabled;
        }

        // handle undo redo changed
        public void HandleUndoRedoChanged(bool isUndo, bool isRedo)
        {
            _undoButton.Enabled = isUndo;
            _redoButton.Enabled = isRedo;
        }

        // click add page button
        private void ClickAddPageButton(object sender, EventArgs e)
        {
            int insertIndex = _presentationModel.SlideIndex + 1;
            _presentationModel.AddPage(insertIndex);
        }

        // handle pages changed
        private void HandlePagesChanged(Model.Pages.PageAction pageAction, int index)
        {
            if (pageAction == Model.Pages.PageAction.Add)
            {
                AddPageAction(index);
            }
            else if (pageAction == Model.Pages.PageAction.Remove)
            {
                RemovePageAction(index);
            }
            else
            {
                SwitchCurrentPage(index);
            }
        }

        // split add page action
        public void AddPageAction(int index)
        {
            AddPageButton(index);
            UpdatePageOrder();
            SwitchCurrentPage(index);
        }

        // split remove page action
        public void RemovePageAction(int index)
        {
            _flowLayoutPanel.Controls.RemoveAt(index);
            _pageButtons.RemoveAt(index);
            UpdatePageOrder();
            _presentationModel.SelectPage(_presentationModel.SlideIndex);
            _shapeData.DataSource = _presentationModel.GetShapes();
            //SetCheckedPage(_pageButtons[_presentationModel.SlideIndex], true);
            _pageButtons[_presentationModel.SlideIndex].Focus();
        }

        // add page button
        public void AddPageButton(int insertIndex)
        {
            int panelWidth = _splitContainer1.Panel1.Width;

            Button newPageButton = new Button();
            newPageButton.Name = Constant.Constant.SLIDE;
            newPageButton.Click += SelectPage;
            newPageButton.BackColor = System.Drawing.Color.White;
            newPageButton.BackgroundImageLayout = ImageLayout.Stretch;

            newPageButton.Width = panelWidth - Constant.Constant.SLIDE_MARGIN * Constant.Constant.TWO;
            newPageButton.Height = (int)((double)(newPageButton.Width) * Constant.Constant.PANEL_RATIO);

            // 新增到列表和選擇區
            // 插入到選擇的頁面後面一個位置
            _pageButtons.Insert(insertIndex, newPageButton);
            _flowLayoutPanel.Controls.Add(newPageButton);
        }

        // update page order
        private void UpdatePageOrder()
        {
            for (int i = 0; i < _pageButtons.Count; i++)
            {
                _flowLayoutPanel.Controls.SetChildIndex(_pageButtons[i], i);
            }
        }

        // select page
        private void SelectPage(object sender, EventArgs e)
        {
            int pageIndex = _pageButtons.IndexOf(sender as Button);
            SwitchCurrentPage(pageIndex);
        }

        // switch current page
        private void SwitchCurrentPage(int pageIndex)
        {
            //if (_presentationModel.SlideIndex >= 0 && _presentationModel.SlideIndex < _pageButtons.Count)
            //{
            //    SetCheckedPage(_pageButtons[_presentationModel.SlideIndex], false);
            //}

            _presentationModel.SelectPage(pageIndex);
            _shapeData.DataSource = _presentationModel.GetShapes();
            _pageButtons[_presentationModel.SlideIndex].Focus();
            //if (_presentationModel.SlideIndex >= 0 && _presentationModel.SlideIndex < _pageButtons.Count)
            //{
            //    SetCheckedPage(_pageButtons[_presentationModel.SlideIndex], true);
            //}
        }

        //// set checked page
        //private void SetCheckedPage(Button button, bool isChecked)
        //{
        //    // 設定Checked樣式
        //    if (isChecked)
        //    {
        //        button.FlatStyle = FlatStyle.Flat;
        //        button.Focus();
        //    }
        //    else
        //    {
        //        Console.WriteLine("time");
        //        button.FlatStyle = FlatStyle.Standard;
        //    }
        //}
    }
}
