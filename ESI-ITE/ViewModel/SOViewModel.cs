using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using ESI_ITE.View;

namespace ESI_ITE.ViewModel
{
    [Export(typeof(IModule))]
    public class SOViewModel: ViewModelBase, IModule
    {
        public string Name
        {
            get
            {
                return "Sales Order Entry";
            }
        }

        public UserControl Userinterface
        {
            get
            {
                return new SOView();
            }
        }

        private Page selectedPage = new SalesOrderEntryView();

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
