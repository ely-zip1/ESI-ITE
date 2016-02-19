using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ESI_ITE.ViewModel
{
    interface IModule
    {
        string Name { get; }
        UserControl Userinterface { get; }
    }
}
