using System.Collections.Generic;

namespace Command;

public class ProductCommandInvoker : IInvoker
{
    private readonly Dictionary<string, ICommand> _commands;

    public ProductCommandInvoker()
    {
        _commands = [];
    }

    public bool AddCommand(string key, ICommand command)
    {
        if (_commands.ContainsKey(key))
        {
            return false;
        }
        _commands[key] = command;
        return true;
    }

    public void InvokeCommand(string key)
    {
        _commands[key].Execute();
    }
}
