using System;
using System.Drawing;

namespace Homework
{
    public class FormGraphicsAdaptor : IGraphics
    {
        readonly Graphics _graphics;

        public FormGraphicsAdaptor(Graphics graphics)
        {
            _graphics = graphics;
        }

        // clear all
        public void ClearAll()
        {
            // OnPaint will clear
        }

        // draw line
        public void DrawLine(double x1, double y1, double x2, double y2)
        {
            _graphics.DrawLine(Pens.Black, (float)x1, (float)y1, (float)x2, (float)y2);
        }

        // draw rectangle
        public void DrawRectangle(double x1, double y1, double x2, double y2)
        {
            _graphics.DrawRectangle(Pens.Black, (float)x1, (float)y1, (float)x2, (float)y2);
        }

        // draw ellipse
        public void DrawEllipse(double x1, double y1, double x2, double y2)
        {
            _graphics.DrawEllipse(Pens.Black, (float)x1, (float)y1, (float)x2, (float)y2);
        }
    }
}
