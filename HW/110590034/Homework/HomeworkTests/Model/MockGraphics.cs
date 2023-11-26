namespace Homework.Model.Tests
{
    public class MockGraphics : IGraphics
    {
        private int _drawLineCount;
        private int _drawRectangleCount;
        private int _drawEllipseCount;
        private int _drawHintCount;
        private int _drawHintCircleCount;

        public MockGraphics()
        {
            ResetAllCount();
        }

        // reset all
        public void ResetAllCount()
        {
            _drawLineCount = 0;
            _drawRectangleCount = 0;
            _drawEllipseCount = 0;
        }

        public int CountLine
        {
            get
            {
                return _drawLineCount;
            }
        }

        public int CountRectangle
        {
            get
            {
                return _drawRectangleCount;
            }
        }

        public int CountEllipse
        {
            get
            {
                return _drawEllipseCount;
            }
        }

        public int CountHint
        {
            get
            {
                return _drawHintCount;
            }
        }

        public int CountHintCircle
        {
            get
            {
                return _drawHintCircleCount;
            }
        }

        // clear all
        public void ClearAll()
        {

        }

        // draw line
        public void DrawLine(double x1, double y1, double x2, double y2)
        {
            _drawLineCount++;
        }

        // draw rectangle
        public void DrawRectangle(double x1, double y1, double x2, double y2)
        {
            _drawRectangleCount++;
        }

        // draw ellipse
        public void DrawEllipse(double x1, double y1, double x2, double y2)
        {
            _drawEllipseCount++;
        }

        // draw hint
        public void DrawHint(double x1, double y1, double x2, double y2)
        {
            _drawHintCount++;
        }

        // draw hint circle
        public void DrawHintCircle(double x1, double y1, double x2, double y2)
        {
            _drawHintCircleCount++;
        }
    }
}
