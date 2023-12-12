namespace Homework.Command
{
    public interface ICommand
    {
        // execute
        void Execute();

        // unexecute
        void Undo();

        // store panel width
        void StorePanelWidth(double width);

        // adjust panel width
        void AdjustPanelWidth(double width);
    }
}
