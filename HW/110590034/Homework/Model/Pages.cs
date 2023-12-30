using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework.Model
{
    public class Pages
    {
        private List<Shapes> _pages;

        public Pages()
        {
            _pages = new List<Shapes>();
        }

        // insert page by index
        public void InsertPageByIndex(int index, Shapes shapes)
        {
            _pages.Insert(index, shapes);
        }

        // remove page by index
        public void RemovePageByIndex(int index)
        {
            _pages.RemoveAt(index);
        }

        // select page
        public Shapes GetSelectPage(int index)
        {
            return _pages[index];
        }

        // get pages
        public List<Shapes> GetPages()
        {
            return _pages;
        }

        // get list len
        public int GetPagesLen()
        {
            return _pages.Coount;
        }
    }
}
