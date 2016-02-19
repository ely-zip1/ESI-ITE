using ESI_ITE.View;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ESI_ITE.ViewModel
{
    [Export(typeof(IModule))]
    public class ITEViewModel : ViewModelBase, IModule
    {
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

    }
}
