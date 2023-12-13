using Homework.Model;

namespace Homework.State
{
    public interface IState
    {
        // mouse down
        void MouseDown(Point mouse, string shapeName, bool isInPanel);
        // mouse move
        void MouseMove(Point mouse);
        // mouse up
        void MouseUp(Point mouse, string shapeName);
        // drawing
        void Drawing(IGraphics graphics);
    }
}
