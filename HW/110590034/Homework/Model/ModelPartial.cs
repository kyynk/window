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
        // add page
        public void AddPage(int index)
        {
            // command
            _commandManager.Execute(new AddPageCommand(this, new Shapes(), index), _panelMaxX);
        }

        // remove page
        public void RemovePage(int index)
        {
            // command
            _commandManager.Execute(new DeletePageCommand(this, _shapesData, _pageIndex), _panelMaxX);
        }

        // insert page
        public void InsertPageByIndex(Shapes shapes, int index)
        {
            _pages.InsertPageByIndex(index, shapes);
        }


        // remove page
        public void RemovePageByIndex(int index)
        {
            _pages.RemovePageByIndex(index);
        }

        // select page
        public void SelectPage(int index)
        {
            _pageIndex = index;
            _shapesData = _pages.GetSelectPage(index);
        }
    }
}
