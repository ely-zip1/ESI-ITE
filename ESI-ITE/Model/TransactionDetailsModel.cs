using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ESI_ITE.Data_Access;

namespace ESI_ITE.Model
{
    public class TransactionDetailsModel
    {
        public int Id { get; set; }
        public string ItemCode { get; set; }
        public string PriceType { get; set; }
        public string Location { get; set; }
        public int Cases { get; set; }
        public int Pieces { get; set; }
        public DateTime Expiration { get; set; }
        public decimal PricePerPiece { get; set; }
        public decimal LineAmount { get; set; }
        public string TransactionCode { get; set; }

        DataAccess db = new DataAccess();

        public List<TransactionDetailsModel> FetchAll(string transactionEntryCode)
        {
            List<List<string>> record = new List<List<string>>();
            List<TransactionDetailsModel> transactionDetails = new List<TransactionDetailsModel>();
            TransactionDetailsModel td = new TransactionDetailsModel();

            string transactionEntryId = db.Select("select entry_id from transaction_entry where trans_no = '" + transactionEntryCode + "'");

            record = db.SelectMultiple("select * from view_transaction_details where transaction_entry_id = '" + transactionEntryId + "'");

            foreach (List<string> row in record)
            {
                td.ItemCode = row[0];
                td.PriceType = row[0];
                td.Location = row[0];
                td.Cases = Int32.Parse(row[3]);
                td.Pieces = Int32.Parse(row[4]);
                td.Expiration = DateTime.Parse(row[5]);
                td.PricePerPiece = Decimal.Parse(row[6]);
                td.LineAmount = Decimal.Parse(row[7]);
                td.TransactionCode = row[8];

                transactionDetails.Add(td);
            }

            return transactionDetails;
        }

        public void AddNew(TransactionDetailsModel detail)
        {
            string insert = string.Empty;

            //Foreign keys
            int itemId;
            int priceTypeId;
            int locationId;
            int transactionId;

            itemId = Int32.Parse(db.Select("select item_id from item_master where item_code = '" + detail.ItemCode + "'"));
            priceTypeId = Int32.Parse(db.Select("select id from price_list where "))

        }
    }
}
