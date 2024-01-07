using System;
using System.Collections.Generic;

namespace Homework.Command
{
    public class CommandManager
    {
        public delegate void UndoRedoChanged(bool isUndo, bool isRedo);
        public event UndoRedoChanged _undoRedoChanged;
        Stack<ICommand> _undo;
        Stack<ICommand> _redo;

        public CommandManager()
        {
            _undo = new Stack<ICommand>();
            _redo = new Stack<ICommand>();
        }

        // execute
        public void Execute(ICommand command, double width)
        {
            command.SetPanelWidth(width);
            command.Execute(width);
            // push command 進 undo stack
            _undo.Push(command);
            // 清除redo stack
            _redo.Clear();
            //Console.WriteLine("execute");
            //Console.WriteLine("undo " + _undo.Count + " redo " + _redo.Count);
            _undoRedoChanged?.Invoke(IsUndoEnabled, IsRedoEnabled);
        }

        // undo
        public void Undo(double width)
        {
            const string MESSAGE = "Cannot Undo exception\n";
            if (_undo.Count <= 0)
                throw new Exception(MESSAGE);
            ICommand command = _undo.Pop();
            //command.AdjustPanelWidth(width);
            _redo.Push(command);
            command.Undo(width);
            //Console.WriteLine("undo");
            //Console.WriteLine("undo1 " + _undo.Count + " redo " + _redo.Count);
            _undoRedoChanged?.Invoke(IsUndoEnabled, IsRedoEnabled);
        }

        // redo
        public void Redo(double width)
        {
            const string MESSAGE = "Cannot Redo exception\n";
            if (_redo.Count <= 0)
                throw new Exception(MESSAGE);
            ICommand command = _redo.Pop();
            //command.AdjustPanelWidth(width);
            _undo.Push(command);
            command.Execute(width);
            //Console.WriteLine("redo");
            //Console.WriteLine("undo " + _undo.Count + " redo1 " + _redo.Count);
            _undoRedoChanged?.Invoke(IsUndoEnabled, IsRedoEnabled);
        }

        // all clear
        public void AllClear()
        {
            _undo.Clear();
            _redo.Clear();
            _undoRedoChanged?.Invoke(IsUndoEnabled, IsRedoEnabled);
        }

        public bool IsRedoEnabled
        {
            get
            {
                Console.WriteLine("redo " + _redo.Count);
                return _redo.Count != 0;
            }
        }

        public bool IsUndoEnabled
        {
            get
            {
                Console.WriteLine("undo " + _undo.Count);
                return _undo.Count != 0;
            }
        }
    }
}
