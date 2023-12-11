namespace Homework.Command
{
    public interface ICommand
    {
        // execute
        void Execute();

        // unexecute
        void Undo();
    }
}
