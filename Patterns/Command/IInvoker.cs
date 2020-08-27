namespace Command
{
    public interface IInvoker
    {
        bool AddCommand(string key, ICommand command);
        void InvokeCommand(string key);
    }
}
