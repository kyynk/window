using System;
using System.ComponentModel;
using System.Windows.Forms;
using Homework.State;
using Homework.Command;
using System.Collections.Generic;

namespace Homework.Model
{
    public partial class Model
    {
        public event Pages.PagesChanged _pagesChanged;

        // add page
        public void AddPage(int index)
        {
            // command
            _commandManager.Execute(new AddPageCommand(this, new Shapes(), index), _panelMaxX);
        }

        // remove page
        public void RemovePage()
        {
            // command
            _commandManager.Execute(new DeletePageCommand(this, _shapesData, PageIndex), _panelMaxX);
        }

        // insert page
        public void InsertPageByIndex(Shapes shapes, int index)
        {
            _pages.InsertPageByIndex(index, shapes);
            SelectPage(index);
            NotifyModelChanged();
        }


        // remove page
        public void RemovePageByIndex(int index)
        {
            _pages.RemovePageByIndex(index);
            if (index > 0)
            {
                SelectPage(index - 1);
            }
            else
            {
                SelectPage(index);
            }
            NotifyModelChanged();
        }

        // select page
        public void SelectPage(int index)
        {
            Console.WriteLine("old page index : " + PageIndex);
            PageIndex = index;
            Console.WriteLine("now page index : " + PageIndex);
            _shapesData = _pages.GetSelectPage(PageIndex);
            NotifyModelChanged();
        }

        // Is selected shape
        public bool IsSelectedShape()
        {
            return _shapesData.GetSelectedShape() != null;
        }

        // page index
        public int PageIndex
        {
            get;
            set;
        }

        // handle pages changed
        public virtual void HandlePagesChanged(bool isAdd, int index)
        {
            if (_pagesChanged != null)
            {
                _pagesChanged(isAdd, index);
            }
        }
    }
}
