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

namespace Tests
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DataAccess db = new DataAccess();
        ItemModel itemModel = new ItemModel();

        public MainWindow()
        {
            InitializeComponent();
            UpdateItemMaster();
        }

        void UpdateItemMaster()
        {
            string pricePurchase;
            string priceSelling;
            string query;

            var itemList = itemModel.FetchAll();

            foreach (var item in itemList)
            {
                pricePurchase = db.Select("select pcode from price_purchase where pcode = '" + item.Code + "'");
                priceSelling = db.Select("select code from price_selling where code = '" + item.Code + "'");

                query = "update item_master set ";
                if (pricePurchase == item.Code)
                    query += "purchase_price_link = 1, ";
                else
                    query += "purchase_price_link = 0, ";

                if (priceSelling == item.Code)
                    query += "selling_price_link = 1 ";
                else
                    query += "selling_price_link = 0 ";

                query += "where item_code = '" + item.Code + "'";

                db.Update(query);
            }
        }
    }
}
