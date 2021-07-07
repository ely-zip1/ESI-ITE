using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ESI_ITE.View.CNDN;
using ESI_ITE.ViewModel.Command;

namespace ESI_ITE.ViewModel.CNDN
{
    public class CnDnMenuViewModel
    {
        public CnDnMenuViewModel()
        {
            entryCommand = new DelegateCommand(GoToEntry);
            printCommand = new DelegateCommand(GoToPrinting);
            postCommand = new DelegateCommand(GoToPosting);
        }

        #region Properties

        private DelegateCommand entryCommand;
        public ICommand EntryCommand
        {
            get
            {
                return entryCommand;
            }
        }

        private DelegateCommand printCommand;
        public ICommand PrintCommand
        {
            get
            {
                return printCommand;
            }
        }

        private DelegateCommand postCommand;
        public ICommand PostCommand
        {
            get
            {
                return postCommand;
            }
        }



        #endregion

        private void GoToEntry()
        {
            MyGlobals.CnDnVM.SelectedPage = new CnDnEntryOptionsView();
        }

        private void GoToPrinting()
        {
            MyGlobals.CnDnPrintingOptionsView = new CnDnPrintingOptionsView();
            MyGlobals.CnDnVM.SelectedPage = MyGlobals.CnDnPrintingOptionsView;
        }

        private void GoToPosting()
        {

        }

    }
}
