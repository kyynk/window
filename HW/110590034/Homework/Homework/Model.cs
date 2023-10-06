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
        public string GetNewShapeTypeName()
        {
            return _shapesData.GetNewShapeName();
        }

        // get new shape location
        public string GetNewShapeInfo()
        {
            string location;
            // location = "(" + _shapesData.GetNewShape().GetInfo()[0][0] + ", " + _shapesData.GetNewShape().GetInfo()[0][1] + "), (" + _shapesData.GetNewShape().GetInfo()[1][0] + ", " + _shapesData.GetNewShape().GetInfo()[1][1] + ")";
            location = $"{GetNewShapeTypeName()} aaaa";
            return location;
        }


        // delete selected shape from _shapes
        public void Delete(int index)
        {
            _shapesData.DeleteSelectedShape(index);
        }
    }
}
