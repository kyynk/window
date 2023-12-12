namespace Homework.Command
{
    public interface ICommand
    {
        // execute
        void Execute(double width);

        // unexecute
        void Undo(double width);

        // store panel width
        void SetPanelWidth(double width);

        // adjust panel width
        void AdjustPanelWidth(double width);
    }
}
