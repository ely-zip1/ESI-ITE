using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ESI_ITE.ViewModel.Command
{
    public class DelegateCommand : ICommand
    {
        private Action _executeMethod;

        public DelegateCommand(Action method)
        {
            _executeMethod = method;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _executeMethod.Invoke();
        }
    }
}
