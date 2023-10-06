using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework
{
    public class Shapes
    {
        private readonly List<Shape> _shapes;
        private readonly ShapeFactory _shapeFactory;

        public Shapes()
        {
            _shapes = new List<Shape>();
            _shapeFactory = new ShapeFactory();
        }

        // add new shape to _shapes
        public void AddNewShape(string shapeType)
        {
            _shapes.Add(_shapeFactory.CreateShape(shapeType));
        }

        // delete selected shape from _shapes
        public void DeleteSelectedShape(int index)
        {
            _shapes.RemoveAt(index);
        }

        // get new shape
        public Shape GetNewShape()
        {
            return _shapes[_shapes.Count - 1];
        }

        // get new shape name
        public string GetNewShapeName()
        {
            return _shapes[_shapes.Count - 1].GetShapeName();
        }
        
        // get new shape info
        public List<List<int>> GetNewShapeInfo()
        {
            return _shapes[_shapes.Count - 1].GetInfo();
        }
    }
}