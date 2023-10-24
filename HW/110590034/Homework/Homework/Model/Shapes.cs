using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework
{
    public class Shapes
    {
        private readonly BindingList<Shape> _shapeList;
        private readonly ShapeFactory _shapeFactory;

        public Shapes()
        {
            _shapeList = new BindingList<Shape>();
            _shapeFactory = new ShapeFactory();
        }

        // get shapes
        public BindingList<Shape> ShapeList
        {
            get
            {
                return _shapeList;
            }
        }

        // add new shape to _shapes
        public void AddNewShapeByDrawing(Model.Mode shapeType, Point point1, Point point2)
        {
            _shapeList.Add(_shapeFactory.AddDrawingShape(shapeType, point1, point2));
        }

        // add new shape to _shapes
        public void AddNewShapeByRandom(string shapeType)
        {
            _shapeList.Add(_shapeFactory.CreateShape(shapeType));
        }

        // delete selected shape from _shapes
        public void DeleteSelectedShape(int index)
        {
            _shapeList.RemoveAt(index);
        }
    }
}