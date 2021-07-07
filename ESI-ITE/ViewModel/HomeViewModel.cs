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
    class HomeViewModel : IModule
    {
        public string Name
        {
            get
            {
                return "Home";
            }
        }

        public UserControl Userinterface
        {
            get
            {
                return new HomeView();
            }
        }
    }
}
