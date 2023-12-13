namespace Homework.Model
{
    public interface IGraphics
    {
        // clear all
        void ClearAll();
        // draw line
        void DrawLine(double x1, double y1, double x2, double y2);
        // draw rectangle
        void DrawRectangle(double x1, double y1, double x2, double y2);
        // draw ellipse
        void DrawEllipse(double x1, double y1, double x2, double y2);
        // draw hint
        void DrawHint(double x1, double y1, double x2, double y2);
        // draw hint circle
        void DrawHintCircle(double x1, double y1, double x2, double y2);
    }
}
