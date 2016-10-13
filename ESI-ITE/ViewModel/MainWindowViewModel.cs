using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using ESI_ITE.View;
using ESI_ITE.Data_Access;
using System.Threading;

namespace ESI_ITE.ViewModel
{
    public class MainWindowViewModel: ViewModelBase
    {
        public MainWindowViewModel( )
        {
            MyGlobals.MainWindow = this;
        }
        
        public List<IModule> Modules { get; set; }

        IModule selectedModule;
        public IModule SelectedModule
        {
            get { return selectedModule; }

            set
            {
                if ( selectedModule != value )
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
                if ( selectedModule == null )
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

        private int blurIntensity = 10;
        public int BlurIntensity
        {
            get { return blurIntensity; }
            set
            {
                blurIntensity = value;
                OnPropertyChanged();
            }
        }

        private string windowTitle = "ESI System";
        public string WindowTitle
        {
            get { return windowTitle; }
            set
            {
                windowTitle = value;
                OnPropertyChanged();
            }
        }

        private UserControl loginUi = new LoginView();
        public UserControl LoginUi
        {
            get { return loginUi; }
            set
            {
                loginUi = value;
                OnPropertyChanged();
            }
        }

        private bool isLoginVisible = true;
        public bool IsLoginVisible
        {
            get { return isLoginVisible; }
            set
            {
                isLoginVisible = value;
                OnPropertyChanged();
            }
        }
        
    }
}
