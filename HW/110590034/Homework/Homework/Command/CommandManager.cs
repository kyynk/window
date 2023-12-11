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
        public void Execute(ICommand cmd)
        {
            cmd.Execute();
            _undo.Push(cmd);    // push command 進 undo stack
            _redo.Clear();      // 清除redo stack
            Console.WriteLine("execute");
            Console.WriteLine("undo " + _undo.Count + " redo " + _redo.Count);
        }

        // undo
        public void Undo()
        {
            if (_undo.Count <= 0)
                throw new Exception("Cannot Undo exception\n");
            ICommand cmd = _undo.Pop();
            _redo.Push(cmd);
            cmd.UnExecute();
            Console.WriteLine("undo");
            Console.WriteLine("undo1 " + _undo.Count + " redo " + _redo.Count);
        }

        // redo
        public void Redo()
        {
            if (_redo.Count <= 0)
                throw new Exception("Cannot Redo exception\n");
            ICommand cmd = _redo.Pop();
            _undo.Push(cmd);
            cmd.Execute();
            Console.WriteLine("redo");
            Console.WriteLine("undo " + _undo.Count + " redo1 " + _redo.Count);
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
