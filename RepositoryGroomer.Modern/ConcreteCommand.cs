using System;
using System.Windows.Input;

namespace RepositoryGroomer.Modern
{
    public class ConcreteCommand: ICommand {
        private readonly Action _executeAction;

        public ConcreteCommand(Action executeAction)
        {
            if(executeAction == null)
                throw new ArgumentException("Argument cannot be null");

            _executeAction = executeAction;
        }

        public bool CanExecute(object parameter) => true;

        public void Execute(object parameter) => _executeAction();

        public event EventHandler CanExecuteChanged;
    }
}