using System;
using System.Collections.Generic;

namespace Homework.Command
{
    public class CommandManager
    {
        Stack<ICommand> undo;
        Stack<ICommand> redo;

        public CommandManager()
        {
            undo = new Stack<ICommand>();
            redo = new Stack<ICommand>();
        }

        // execute
        public void Execute(ICommand cmd)
        {
            cmd.Execute();
            undo.Push(cmd);    // push command 進 undo stack
            redo.Clear();      // 清除redo stack
        }

        // undo
        public void Undo()
        {
            if (undo.Count <= 0)
                throw new Exception("Cannot Undo exception\n");
            ICommand cmd = undo.Pop();
            redo.Push(cmd);
            cmd.UnExecute();
        }

        // redo
        public void Redo()
        {
            if (redo.Count <= 0)
                throw new Exception("Cannot Redo exception\n");
            ICommand cmd = redo.Pop();
            undo.Push(cmd);
            cmd.Execute();
        }

        public bool IsRedoEnabled
        {
            get
            {
                return redo.Count != 0;
            }
        }

        public bool IsUndoEnabled
        {
            get
            {
                return undo.Count != 0;
            }
        }
    }
}
