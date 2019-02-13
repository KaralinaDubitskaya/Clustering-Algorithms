using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Clustering.Models
{
    public class Command :  ICommand
    {
        // The action that is invoked when the command is activated.
        protected Action _action = null;
        protected Action<object> _parameterizedAction = null;

        // Enables the command execution.
        private bool _canExecute = false;

        public bool CanExecute
        {
            get { return _canExecute; }
            set
            {
                if (_canExecute != value)
                {
                    _canExecute = value;
                    CanExecuteChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        public event EventHandler CanExecuteChanged;
        public event EventHandler Executing;
        public event EventHandler Executed;

        public Command(Action action, bool canExecute = true)
        {
            _action = action;
            _canExecute = canExecute;
        }

        // Defines if the command can be executed in the current state.
        bool ICommand.CanExecute(object parameter)
        {
            return _canExecute;
        }

        // The command execution.
        void ICommand.Execute(object parameter)
        {
            Executing?.Invoke(this, EventArgs.Empty);

            Action action = _action;
            Action<object> parameterizedAction = _parameterizedAction;
            if (action != null)
            {
                action();
            }
            else
            {
                parameterizedAction?.Invoke(parameter);
            }

            Executed?.Invoke(this, EventArgs.Empty);
        }
    }
}
