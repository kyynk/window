using System;
using System.Collections.Generic;

namespace Homework.Command
{
    public class CommandManager
    {
        Stack<ICommand> _undo;
        Stack<ICommand> _redo;

        public CommandManager()
        {
            _undo = new Stack<ICommand>();
            _redo = new Stack<ICommand>();
        }

        // execute
        public void Execute(ICommand command)
        {
            command.Execute();
            // push command 進 undo stack
            _undo.Push(command);
            // 清除redo stack
            _redo.Clear();
            //Console.WriteLine("execute");
            //Console.WriteLine("undo " + _undo.Count + " redo " + _redo.Count);
        }

        // undo
        public void Undo()
        {
            const string MESSAGE = "Cannot Undo exception\n";
            if (_undo.Count <= 0)
                throw new Exception(MESSAGE);
            ICommand command = _undo.Pop();
            _redo.Push(command);
            command.Undo();
            //Console.WriteLine("undo");
            //Console.WriteLine("undo1 " + _undo.Count + " redo " + _redo.Count);
        }

        // redo
        public void Redo()
        {
            const string MESSAGE = "Cannot Redo exception\n";
            if (_redo.Count <= 0)
                throw new Exception(MESSAGE);
            ICommand command = _redo.Pop();
            _undo.Push(command);
            command.Execute();
            //Console.WriteLine("redo");
            //Console.WriteLine("undo " + _undo.Count + " redo1 " + _redo.Count);
        }

        public bool IsRedoEnabled
        {
            get
            {
                //Console.WriteLine("redo " + _redo.Count);
                return _redo.Count != 0;
            }
        }

        public bool IsUndoEnabled
        {
            get
            {
                //Console.WriteLine("undo " + _undo.Count);
                return _undo.Count != 0;
            }
        }
    }
}
