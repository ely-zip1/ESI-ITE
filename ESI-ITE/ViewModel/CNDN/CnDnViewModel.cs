using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using ESI_ITE.View.CNDN;

namespace ESI_ITE.ViewModel.CNDN
{
    [Export(typeof(IModule))]
    public class CnDnViewModel : ViewModelBase, IModule
    {
        public CnDnViewModel()
        {
            MyGlobals.CnDnVM = this;
        }

        public string Name
        {
            get
            {
                return "CN/DN";
            }
        }

        public UserControl Userinterface
        {
            get
            {
                return new CnDnView();
            }
        }


        private Page selectedPage = new CnDnMenuView();
        public Page SelectedPage
        {
            get
            {
                return selectedPage;
            }
            set
            {
                selectedPage = value;
                OnPropertyChanged();
            }
        }
        
    }
}
