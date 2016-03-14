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
using ESI_ITE.ViewModel;

namespace ESI_ITE.View {
    /// <summary>
    /// Interaction logic for PrintPreviewPageView.xaml
    /// </summary>
    public partial class PrintPreviewPageView: Page {
        public PrintPreviewPageView( )
        {
            InitializeComponent();
            docViewer.FitToWidth();
        }

        private void CheckBox_Unchecked( object sender, RoutedEventArgs e )
        {
            viewModel.ItemUnchecked();
        }

        private void CheckBox_Checked( object sender, RoutedEventArgs e )
        {
            viewModel.ItemChecked();
        }
    }
}
