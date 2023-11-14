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
            double width = Math.Abs(x2 - x1);
            double height = Math.Abs(y2 - y1);
            if (x1 > x2)
                x1 -= width;
            if (y1 > y2)
                y1 -= height;
            _graphics.DrawRectangle(Pens.Black, (float)x1, (float)y1, (float)width, (float)height);
        }

        // draw ellipse
        public void DrawEllipse(double x1, double y1, double x2, double y2)
        {
            double width = Math.Abs(x2 - x1);
            double height = Math.Abs(y2 - y1);
            if (x1 > x2)
                x1 -= width;
            if (y1 > y2)
                y1 -= height;
            _graphics.DrawEllipse(Pens.Black, (float)x1, (float)y1, (float)width, (float)height);
        }

        // draw hint
        public void DrawHint(double x1, double y1, double x2, double y2)
        {
            double width = Math.Abs(x2 - x1);
            double height = Math.Abs(y2 - y1);
            if (x1 > x2)
                x1 -= width;
            if (y1 > y2)
                y1 -= height;
            _graphics.DrawRectangle(Pens.Red, (float)x1, (float)y1, (float)width, (float)height);
        }
    }
}
