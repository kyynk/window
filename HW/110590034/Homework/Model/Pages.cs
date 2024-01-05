﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework.Model
{
    public class Pages
    {
        public delegate void PagesChanged(bool isAddingPage, int index);
        public event PagesChanged _pagesChanged;

        private List<Shapes> _pages;

        public Pages()
        {
            _pages = new List<Shapes>();
        }

        // insert page by index
        public void InsertPageByIndex(int index, Shapes shapes)
        {
            _pages.Insert(index, shapes);
            _pagesChanged?.Invoke(true, index);
        }

        // remove page by index
        public void RemovePageByIndex(int index)
        {
            _pages.RemoveAt(index);
            _pagesChanged?.Invoke(false, index);
        }

        // select page
        public Shapes GetSelectPage(int index)
        {
            Console.WriteLine("Get page index : " + index);
            return _pages[index];
        }

        // get pages
        public List<Shapes> GetPages()
        {
            return _pages;
        }
    }
}
