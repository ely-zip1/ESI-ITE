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
    public class InvoicingViewModel: ViewModelBase, IModule
    {

        public InvoicingViewModel( )
        {
            MyGlobals.InvoicingVM = this;
        }

        public string Name
        {
            get
            {
                return "Invoicing";
            }
        }

        public UserControl Userinterface
        {
            get
            {
                return new InvoicingView();
            }
        }

        private Page selectedPage = new InvoicingMainPageView();
        public Page SelectedPage
        {
            get { return selectedPage; }
            set
            {
                selectedPage = value;
                OnPropertyChanged();
            }
        }

    }
}
