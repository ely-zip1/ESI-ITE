using ESI_ITE.ViewModel;
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

namespace ESI_ITE.View
{
    /// <summary>
    /// Interaction logic for InvoicingAllocationMaintenanceView.xaml
    /// </summary>
    public partial class InvoicingAllocationMaintenanceView: Page
    {
        public InvoicingAllocationMaintenanceView( )
        {
            InitializeComponent();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            (this.DataContext as InvoicingAllocationMaintenanceViewModel).CasesValueChangedCommand.Execute(e);
        }

        private void TextBox_TextChanged_1(object sender, TextChangedEventArgs e)
        {
            (this.DataContext as InvoicingAllocationMaintenanceViewModel).PiecesValueChangedCommand.Execute(e);
        }
    }
}
