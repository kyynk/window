namespace Homework.Model
{
    public interface IState
    {
        // mouse down
        void MouseDown(Point mouse, string shapeName, ref Shapes shapes, ref ShapeFactory shapeFactory);
        // mouse move
        void MouseMove(Point mouse, ref Shapes shapes);
        // mouse up
        void MouseUp(Point mouse, string shapeName, ref Shapes shapes);
        // drawing
        void Drawing(IGraphics graphics, ref Shapes shapes);
    }
}
