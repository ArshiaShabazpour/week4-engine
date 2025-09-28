using System.Collections.Generic;
using UnityEngine;

public class CommandManager : MonoBehaviour
{
    private Stack<ICommand> undoStack = new Stack<ICommand>();


    public void ExecuteCommand(ICommand cmd)
    {
        cmd.Execute();
        undoStack.Push(cmd);
    }


    public void Undo()
    {
        if (undoStack.Count == 0) return;
        var cmd = undoStack.Pop();
        cmd.Undo();
    }
}