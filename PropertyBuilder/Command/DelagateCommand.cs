using System;
using System.Windows.Input;

namespace PropertyBuilder.Command
{
    public class DelagateCommand : ICommand
    {
        private readonly Action _executeMethod;
        private readonly Func<object, bool> _canExecute;

        public DelagateCommand(Action executeMethod, Func<object, bool> canExecute)
        {
            _executeMethod = executeMethod;
            _canExecute = canExecute;
        }

        public DelagateCommand(Action executeMethod) : this(executeMethod, o => true)
        {
            _executeMethod = executeMethod;
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute.Invoke(parameter);
        }

        public void Execute(object parameter)
        {
            _executeMethod.Invoke();
        }

        public event EventHandler CanExecuteChanged;
    }
}
