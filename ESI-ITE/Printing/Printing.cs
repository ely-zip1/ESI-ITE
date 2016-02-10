using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ESI_ITE.Model;
using ESI_ITE.View;
using System.Windows.Documents;

namespace ESI_ITE.Printing
{
    public class Printing
    {
        public List<TransactionModel> TransactionEntry { get; set; }
        public List<InventoryDummyModel> TransactionDetail { get; set; }

        StringBuilder header = new StringBuilder();
        StringBuilder itemRow = new StringBuilder();
        StringBuilder footer = new StringBuilder();

        public void StartPrinting()
        {
            header.AppendLine("{0}");
           foreach(TransactionModel transaction in this.TransactionEntry)
            {
                foreach(InventoryDummyModel detailRow in this.TransactionDetail)
                {

                }
            }
        }
    }
}
