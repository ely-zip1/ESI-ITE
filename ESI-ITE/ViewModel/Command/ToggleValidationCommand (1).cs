using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ESI_ITE.ViewModel.Command
{
    public class ToggleValidationCommand : ICommand
    {
        public TransactionEntryViewModel ViewModel { get; set; }

        public ToggleValidationCommand(TransactionEntryViewModel vm)
        {
            this.ViewModel = vm;
        }
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            this.ViewModel.IsFirstLoad = false;
        }
    }
}
