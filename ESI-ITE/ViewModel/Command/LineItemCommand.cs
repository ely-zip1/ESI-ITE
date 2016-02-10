using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ESI_ITE.ViewModel.Command
{
    public class LineItemCommand : ICommand
    {
        public TransactionEntryViewModel ViewModel { get; set; }

        public LineItemCommand(TransactionEntryViewModel vm)
        {
            this.ViewModel = vm;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return this.ViewModel.IsValid();
        }

        public void Execute(object parameter)
        {
            throw new NotImplementedException();
        }
    }
}
