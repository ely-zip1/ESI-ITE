using ESI_ITE.Printing;
using ESI_ITE.View.PrintingTemplate;
using ESI_ITE.ViewModel.Command;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;

namespace ESI_ITE.ViewModel
{
    public class PrintingMainPageViewModel: ViewModelBase
    {
        public PrintingMainPageViewModel( )
        {
            cancelPrintingCommand = new DelegateCommand(CancelPrinting);
            Load();
        }

        private FixedDocument fixedDoc;
        public FixedDocument FixedDoc
        {
            get { return fixedDoc; }
            set
            {
                fixedDoc = value;
                OnPropertyChanged();
            }
        }

        private DelegateCommand cancelPrintingCommand;
        public ICommand CancelPrintingCommand
        {
            get { return cancelPrintingCommand; }
        }

        private void Load( )
        {
            if ( MyGlobals.printingDoc != null )
                FixedDoc = MyGlobals.printingDoc;
        }

        private void CancelPrinting( )
        {
            MyGlobals.InvoicingVM.SelectedPage = MyGlobals.PrintingParent;
        }
    }
}
