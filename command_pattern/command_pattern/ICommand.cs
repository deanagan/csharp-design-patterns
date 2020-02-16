using System;

namespace Command
{
    public interface ICommand
    {
        void Execute(IProduct product);
    }
}
