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

        // draw
        public void Draw(System.Drawing.Graphics graphics)
        {
            // graphics物件是Paint事件帶進來的，只能在當次Paint使用
            // 而Adaptor又直接使用graphics，這樣DoubleBuffer才能正確運作
            // 因此，Adaptor不能重複使用，每次都要重新new
            _model.Draw(new FormGraphicsAdaptor(graphics));
        }

        // line enable
        public void EnableLine()
        {
            _isLineEnabled = true;
            _isRectangleEnabled = false;
            _isEllipseEnabled = false;
            _model.SetMode(Model.Mode.DrawLine);
        }

        // line enable
        public void EnableRectangle()
        {
            _isLineEnabled = false;
            _isRectangleEnabled = true;
            _isEllipseEnabled = false;
            _model.SetMode(Model.Mode.DrawRectangle);
        }

        // line enable
        public void EnableEllipse()
        {
            _isLineEnabled = false;
            _isRectangleEnabled = false;
            _isEllipseEnabled = true;
            _model.SetMode(Model.Mode.DrawEllipse);
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
    }
}
