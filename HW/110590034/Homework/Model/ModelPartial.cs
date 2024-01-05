﻿using System;
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
        public void RemovePage()
        {
            // command
            _commandManager.Execute(new DeletePageCommand(this, _shapesData, _pageIndex), _panelMaxX);
        }

        // insert page
        public void InsertPageByIndex(Shapes shapes, int index)
        {
            _pages.InsertPageByIndex(index, shapes);
            SelectPage(index);
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
        }

        // select page
        public void SelectPage(int index)
        {
            if (_shapesData != null)
            {
                _shapesData.ClearSelectedShape();
            }
            _pageIndex = index;
            _shapesData = _pages.GetSelectPage(index);
        }

        // Is selected shape
        public bool IsSelectedShape()
        {
            return _shapesData.GetSelectedShape() != null;
        }
    }
}
