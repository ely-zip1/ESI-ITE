using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace ESI_ITE.View
{
    /// <summary>
    /// Interaction logic for SalesOrderEntryView.xaml
    /// </summary>
    public partial class SalesOrderEntryView: Page
    {
        public SalesOrderEntryView( )
        {
            InitializeComponent();
            MyGlobals.SalesOrderEntryPage = this;
            //cmbOrderNumber.Focus();
        }

        private void gridSearch_IsVisibleChanged( object sender, DependencyPropertyChangedEventArgs e )
        {
            if ( gridSearch.Visibility == Visibility.Visible )
            {
                gridSearch.Dispatcher.BeginInvoke((Action)delegate
                {
                    Keyboard.Focus(txtCustomerSearch);
                }, DispatcherPriority.Render);
            }
        }

        private void customerSearchGrid_SelectionChanged( object sender, SelectionChangedEventArgs e )
        {
            if ( customerSearchGrid.SelectedItem != null )
                customerSearchGrid.ScrollIntoView(customerSearchGrid.SelectedItem);
        }
    }
}
