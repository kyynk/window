using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework
{
    public class FormPresentationModel
    {
        Model _model;
        private bool _isLineEnabled = false;
        private bool _isRectangleEnabled = false;
        private bool _isEllipseEnabled = false;

        public FormPresentationModel(Model model)
        {
            _model = model;
        }

        // line enable
        public void EnableLine()
        {
            _isLineEnabled = true;
            _isRectangleEnabled = false;
            _isEllipseEnabled = false;
        }

        // line enable
        public void EnableRectangle()
        {
            _isLineEnabled = false;
            _isRectangleEnabled = true;
            _isEllipseEnabled = false;
        }

        // line enable
        public void EnableEllipse()
        {
            _isLineEnabled = false;
            _isRectangleEnabled = false;
            _isEllipseEnabled = true;
        }

        // is line enabled
        public bool IsLineEnabled()
        {
            return _isLineEnabled;
        }

        // is rectangle enabled
        public bool IsRectangleEnabled()
        {
            return _isRectangleEnabled;
        }

        // is ellipse enabled
        public bool IsEllipseEnabled()
        {
            return _isEllipseEnabled;
        }

        // create
        public void Create(string shapeType)
        {
            _model.Create(shapeType);
        }

        // delete
        public void Delete(int index)
        {
            _model.Delete(index);
        }

        // get new shape type
        public string GetNewShapeType()
        {
            return _model.GetNewShapeType();
        }

        // get new shape position
        public string GetNewShapePosition()
        {
            return _model.GetNewShapePosition();
        }
    }
}
