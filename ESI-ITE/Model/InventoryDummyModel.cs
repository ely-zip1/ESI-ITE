using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text;

using ESI_ITE.Data_Access;

namespace ESI_ITE.Model
{
    public class InventoryDummyModel
    {
        public int Id { get; set; }
        public string TransactionCode { get; set; }
        public string Location { get; set; }
        public string PriceType { get; set; }
        public string ItemCode { get; set; }
        public string ItemDescription { get; set; }
        public int Cases { get; set; }
        public int Pieces { get; set; }
        public DateTime Expiration { get; set; }
        public decimal PricePerPiece { get; set; }
        public decimal LineAmount { get; set; }

        DataAccess db = new DataAccess();

        private StringBuilder insert = new StringBuilder();

        public List<InventoryDummyModel> FetchAll(string transactionNumber)
        {
            List<Dictionary<string, string>> record = new List<Dictionary<string, string>>();
            List<InventoryDummyModel> transactionDetails = new List<InventoryDummyModel>();
            InventoryDummyModel td = new InventoryDummyModel();

            string transactionEntryId = db.Select("select entry_id from transaction_entry where trans_no = '" + transactionNumber + "'");

            record = db.SelectMultiple("select * from view_inventory_dummy where transaction_entry_id = '" + transactionEntryId + "'");

            foreach (Dictionary<string, string> row in record)
            {
                td.Id = Int32.Parse(row["id"]);
                td.TransactionCode = row["transaction_code"];
                td.Location = row["location"];
                td.PriceType = row["price_type"];
                td.ItemCode = row["item_code"];
                td.ItemDescription = row["item_description"];
                td.Cases = Int32.Parse(row["cases"]);
                td.Pieces = Int32.Parse(row["pieces"]);
                td.Expiration = DateTime.Parse(row["expiration"]);
                td.PricePerPiece = Decimal.Parse(row["price_per_piece"]);
                td.LineAmount = Decimal.Parse(row["line_amount"]);

                transactionDetails.Add(td);
            }

            return transactionDetails;
        }

        public void AddNew(InventoryDummyModel detail)
        {
            //Foreign keys
            int itemId;
            int locationId;
            int transactionId;

            itemId = Int32.Parse(db.Select("select item_id from item_master where item_code = '" + detail.ItemCode + "'"));
            locationId = Int32.Parse(db.Select("select location_id from location where code = '" + detail.Location + "'"));
            transactionId = Int32.Parse(db.Select("select entry_id from transaction_entry where trans_no = '" + detail.TransactionCode + "'"));

            insert.Clear();

            insert.Append("INSERT INTO inventory_dummy VALUES(");
            insert.Append("null,");
            insert.Append("" + transactionId + ",");
            insert.Append("" + locationId + ",");
            insert.Append("'" + detail.PriceType + "',");
            insert.Append("" + itemId + ",");
            insert.Append("" + detail.Cases + ",");
            insert.Append("" + detail.Pieces + ",");
            insert.Append("'" + detail.Expiration.ToString("MM/dd/yyyy") + "',");
            insert.Append("'" + detail.PricePerPiece + "',");
            insert.Append("'" + detail.LineAmount + "'");
            insert.Append(")");

            db.Insert(insert.ToString());

            insert.Clear();
        }
        
    }
}
