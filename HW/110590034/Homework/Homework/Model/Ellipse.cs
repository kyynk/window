namespace Homework.Model
{
    public class Ellipse : Shape
    {
        private const string COMMA = ", ";
        private const string LEFT_PARENTHESIS = "(";
        private const string RIGHT_PARENTHESIS = ")";

        public Ellipse(Point point1, Point point2) : base(point1, point2)
        {
            _shapeName = Constant.ELLIPSE;
        }

        // draw
        public override void Draw(IGraphics graphics)
        {
            graphics.DrawEllipse(_point1.X, _point1.Y, _point2.X, _point2.Y);
        }

        // get shape name
        public override string GetShapeName()
        {
            return _shapeName;
        }

        // get info (position)
        public override string GetInfo()
        {
            ResetPoint();
            string firstCoordinate = LEFT_PARENTHESIS + _point1.X + COMMA + _point1.Y + RIGHT_PARENTHESIS;
            string secondCoordinate = LEFT_PARENTHESIS + _point2.X + COMMA + _point2.Y + RIGHT_PARENTHESIS;
            return firstCoordinate + COMMA + secondCoordinate;
        }
    }
}
