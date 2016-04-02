using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using ESI_ITE.ViewModel;
using ESI_ITE.View;
using System.ComponentModel.Composition.Hosting;

namespace ESI_ITE
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App: Application
    {
        private void Application_Startup( object sender, StartupEventArgs e )
        {
            MyGlobals.ProgressCurrentStep = 0;
            MyGlobals.ProgressTotalStep = 0;
            MyGlobals.ProgressPercentComplete = 0;

            var home = new MainWindow();

            var catalog = new AssemblyCatalog(this.GetType().Assembly);
            var container = new CompositionContainer(catalog);
            var modules = container.GetExportedValues<IModule>();

            home.DataContext = new MainWindowViewModel() { Modules = modules.ToList() };
            home.Show();
        }
    }
}
