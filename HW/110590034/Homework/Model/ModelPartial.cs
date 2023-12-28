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
        private List<Shapes> _pages;

        // add page
        public void AddPage()
        {
            _pages.Add(new Shapes());
        }

        // remove page
        public void RemovePage(int index)
        {
            _pages.RemoveAt(index);
        }

        // insert page
        public void InsertPage(Shapes shapes, int index)
        {
            _pages.Insert(index, shapes);
        }

        // select page
        public void SelectPage(int index)
        {
            _shapesData = _pages[index];
        }
    }
}
