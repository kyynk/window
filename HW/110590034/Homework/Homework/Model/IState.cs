namespace Homework.Model
{
    public interface IState
    {
        // mouse down
        void MouseDown(Point mouse, string shapeName);
        // mouse move
        void MouseMove(Point mouse);
        // mouse up
        void MouseUp(Point mouse, string shapeName);
        // drawing
        void Drawing(IGraphics graphics);
    }
}
