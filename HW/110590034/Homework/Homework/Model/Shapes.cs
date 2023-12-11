using System.ComponentModel;

namespace Homework.Model
{
    public class Shapes
    {
        private readonly BindingList<Shape> _shapeList;
        private Shape _selectedShape;

        public Shapes()
        {
            _shapeList = new BindingList<Shape>();
            _selectedShape = null;
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
        public void AddNewShape(Shape shape)
        {
            _shapeList.Add(shape);
        }

        // delete selected shape from _shapes
        public void DeleteShapeByIndex(int index)
        {
            if (_selectedShape == _shapeList[index])
                _selectedShape = null;
            _shapeList.RemoveAt(index);
        }

        // check point in shape
        public bool CheckSelect(double pointX, double pointY)
        {
            foreach (Shape aShape in _shapeList)
            {
                if (_selectedShape != null)
                {
                    _selectedShape.IsSelected = false;
                }
                _selectedShape = null;
                if (aShape.CheckSelect(pointX, pointY))
                {
                    aShape.IsSelected = true;
                    _selectedShape = aShape;
                    return true;
                }
            }
            return false;
        }

        // draw hint select shape
        public Shape GetSelectedShape()
        {
            return _selectedShape;
        }

        // move select shape
        public void MoveSelectedShape(double offsetX, double offsetY)
        {
            if (_selectedShape != null)
                _selectedShape.Move(offsetX, offsetY);
        }

        // delete select shape
        public void DeleteSelectedShape()
        {
            //_shapeList.Remove(_selectedShape);
            _selectedShape = null;
        }

        // resize selected shape
        public void ResizeSelectedShape(Shape.Location location, Point mouse)
        {
            if (_selectedShape != null)
            {
                _selectedShape.IsResizing = true;
                _selectedShape.SetResizePoint(location, mouse);
                _selectedShape.UpdatePoint(location);
            }
        }

        // cancel resize
        public void CancelResize()
        {
            if (_selectedShape != null)
                _selectedShape.IsResizing = false;
        }

        // get location
        public Shape.Location GetSelectedShapeLocation(double mouseX, double mouseY)
        {
            return _selectedShape.GetLocation(mouseX, mouseY);
        }

        // set is first point on bottom
        public void SetSelectedShapeIsFirstPointBottom()
        {
            _selectedShape.SetFirstPointBottom();
        }

        // insert shape by index
        public void InsertShapeByIndex(Shape shape, int index)
        {
            _shapeList.Insert(index, shape);
        }
    }
}