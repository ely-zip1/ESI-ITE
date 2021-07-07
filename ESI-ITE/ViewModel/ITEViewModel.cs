using ESI_ITE.View;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace ESI_ITE.ViewModel
{
    [Export(typeof(IModule))]
    public class ITEViewModel: ViewModelBase, IModule
    {
        public ITEViewModel( )
        {
            MyGlobals.IteViewModel = this;
        }

        public string Name
        {
            get
            {
                return "Inventory Transaction Entry";
            }
        }

        public UserControl Userinterface
        {
            get
            {
                return new ITEView();
            }
        }

        private Page selectedPage = new TransactionEntryPageView();
        public Page SelectedPage
        {
            get { return selectedPage; }
            set
            {
                selectedPage = value;
                OnPropertyChanged("SelectedPage");
            }
        }

        private bool backNavigationEnabled;
        public bool BackNavigationEnabled
        {
            get { return backNavigationEnabled; }
            set
            {
                backNavigationEnabled = value;
                BackNavigationExecute();
            }
        }

        private void BackNavigationExecute( )
        {

        }
    }
}
