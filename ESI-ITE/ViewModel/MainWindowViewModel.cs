using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ESI_ITE.ViewModel
{
    class MainWindowViewModel : ViewModelBase
    {
        IModule selectedModule;

        public List<IModule> Modules { get; set; }

        public IModule SelectedModule
        {
            get { return selectedModule; }

            set
            {
                if (selectedModule != value)
                {
                    selectedModule = value;
                    OnPropertyChanged("UserInterface");
                }
            }
        }

        public UserControl UserInterface
        {
            get
            {
                if (selectedModule == null)
                {
                    return null;
                }
                return SelectedModule.Userinterface;
            }
        }

        private int selectedIndex = 0;

        public int SelectedIndex
        {
            get { return selectedIndex; }
            set
            {
                selectedIndex = value;
                OnPropertyChanged("SelectedIndex");
            }
        }

    }
}
