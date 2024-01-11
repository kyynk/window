using Microsoft.VisualStudio.TestTools.UnitTesting;
using Homework.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework.Model.Tests
{
    [TestClass()]
    public class PagesTests
    {
        private Pages _pages;

        // setup
        [TestInitialize()]
        public void Initialize()
        {
            _pages = new Pages();
        }

        // test constructor
        [TestMethod()]
        public void PagesTest()
        {
            _pages = new Pages();
            Assert.IsInstanceOfType(_pages.GetPages(), typeof(List<Shapes>));
        }

        // test insert page by index
        [TestMethod()]
        public void InsertPageByIndexTest()
        {
            bool eventRaised = false;
            int eventIndex = -1;
            _pages._pagesChanged += (action, index) =>
            {
                eventRaised = true;
                eventIndex = index;
            };

            _pages.InsertPageByIndex(0, new Shapes());
            Assert.AreEqual(1, _pages.GetPages().Count);
            Assert.IsTrue(eventRaised);
            Assert.AreEqual(0, eventIndex);
            // clear
            _pages.RemovePageByIndex(0);
        }

        // test remove page by index
        [TestMethod()]
        public void RemovePageByIndexTest()
        {
            bool eventRaised = false;
            int eventIndex = -1;
            _pages._pagesChanged += (action, index) =>
            {
                eventRaised = true;
                eventIndex = index;
            };

            _pages.InsertPageByIndex(0, new Shapes());
            Assert.AreEqual(1, _pages.GetPages().Count);
            eventRaised = false;
            eventIndex = -1;
            _pages.RemovePageByIndex(0);

            Assert.AreEqual(0, _pages.GetPages().Count);
            Assert.IsTrue(eventRaised);
            Assert.AreEqual(0, eventIndex);
        }

        // test get select page
        [TestMethod()]
        public void GetSelectPageTest()
        {
            Shapes shapes = new Shapes();
            _pages.InsertPageByIndex(0, shapes);
            Assert.AreEqual(shapes, _pages.GetSelectPage(0));
            
            // clean
            _pages.RemovePageByIndex(0);
        }

        // test get pages
        [TestMethod()]
        public void GetPagesTest()
        {
            Assert.IsInstanceOfType(_pages.GetPages(), typeof(List<Shapes>));
        }
    }
}