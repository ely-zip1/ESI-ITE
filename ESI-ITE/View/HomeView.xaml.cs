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
using ESI_ITE.Data_Access;
using ESI_ITE.Model;

namespace ESI_ITE.View
{
    /// <summary>
    /// Interaction logic for HomePageView.xaml
    /// </summary>
    public partial class HomeView : UserControl
    {
        public HomeView()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //DataAccess db = new DataAccess();
            //ItemModel itemObject = new ItemModel();
            //SupplierModel supplierObject = new SupplierModel();

            //var suppliers = supplierObject.FetchAll();

            //var items = itemObject.FetchAll();

            //foreach (var row in items)
            //{
            //    var item = row as ItemModel;

            //    foreach (var sup in suppliers)
            //    {
            //        var supplier = sup as SupplierModel;

            //        if (supplier.SupplierName == item.Supplier)
            //        {
            //            db.Update("update item_master set supplier_id = '" + supplier.SupplierId + "' where item_id = '" + item.Id + "'");
            //        }
            //    }
            //}
        }
    }
}
