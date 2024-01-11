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
        public event Pages.PagesChanged _pagesChanged;
        public event CommandManager.UndoRedoChanged _undoRedoChanged;

        // add page
        public virtual void AddPage(int index)
        {
            // command
            _commandManager.Execute(new AddPageCommand(this, new Shapes(), index), _panelMaxX);
            Console.WriteLine("add ing page !!!");
        }

        // remove page
        public void RemovePage()
        {
            // command
            _commandManager.Execute(new DeletePageCommand(this, _pages.GetSelectPage(PageIndex), PageIndex), _panelMaxX);
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
            if (index == _pages.GetPages().Count - 1)
            {
                SelectPage(index - 1);
            }

            Console.WriteLine("rm index : " + index);
            _pages.RemovePageByIndex(index);

            Console.WriteLine("aaa : " + index);
            if (index < _pages.GetPages().Count)
            {
                SelectPage(index);
            }
            NotifyModelChanged();
        }

        // select page
        public virtual void SelectPage(int index)
        {
            _pagesChanged(Pages.PageAction.Switch, index);
        }

        // switch page
        public virtual void SwitchPage(int index)
        {
            Console.WriteLine("old page index : " + PageIndex);
            PageIndex = index;
            Console.WriteLine("now page index : " + PageIndex);
            _shapesData = _pages.GetSelectPage(PageIndex);
            NotifyModelChanged();
        }

        // Is selected shape
        public virtual bool IsSelectedShape()
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
        public virtual void HandlePagesChanged(Pages.PageAction pageAction, int index)
        {
            if (_pagesChanged != null)
            {
                _pagesChanged(pageAction, index);
            }
        }

        // handle undo redo changed
        public virtual void HandleUndoRedoChanged(bool isUndo, bool isRedo)
        {
            if (_undoRedoChanged != null)
            {
                _undoRedoChanged(isUndo, isRedo);
            }
        }
    }
}
