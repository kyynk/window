using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework
{
    public class Model
    {
        private readonly Shapes _shapesData;

        public Model()
        {
            _shapesData = new Shapes();
        }

        // add new shape to shapes
        public void Create(string shapeType)
        {
            _shapesData.AddNewShape(shapeType);
        }

        // get new shape name
        public string GetNewShapeType()
        {
            return _shapesData.GetNewShapeName();
        }

        // get new shape location
        public string GetNewShapePosition()
        {
            return _shapesData.GetNewShapeInfo();
        }

        // delete selected shape from _shapes
        public void Delete(int index)
        {
            _shapesData.DeleteSelectedShape(index);
        }
    }
}
