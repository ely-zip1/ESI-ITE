using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ESI_ITE.Data_Access;

namespace ESI_ITE.Model
{
    public class TransactionModel
    {
        public int Id { get; set; }
        public string TransactionNumber { get; set; }
        public string TransactionCode { get; set; }
        public string TransactionType { get; set; }
        public string DocumentNumber { get; set; }
        public DateTime TransactionDate { get; set; }
        public string SourceWarehouse { get; set; }
        public string DestinationWarehouse { get; set; }
        public string SourceLocation { get; set; }
        public string DestinationLocation { get; set; }
        public string SourceSalesman { get; set; }
        public string DestinationSalesman { get; set; }
        public string PriceCategory { get; set; }
        public string PriceType { get; set; }
        public string Reason { get; set; }
        public string ReasonCode { get; set; }
        public string Comment { get; set; }
        public bool IsPosted { get; set; }

        private DataAccess db = new DataAccess();

        public List<TransactionModel> fetchAllTransactions()
        {
            List<List<string>> records = new List<List<string>>();
            List<TransactionModel> allTrans = new List<TransactionModel>();
            TransactionModel trans = new TransactionModel();

            records = db.SelectMultiple("select * from view_transaction_entry where status = 0 ");

            foreach (List<string> attribute in records)
            {
                trans.Id = Int32.Parse(attribute[0]);
                trans.TransactionNumber = attribute[1];
                trans.TransactionType = attribute[2];
                trans.DocumentNumber = attribute[3];
                trans.TransactionDate = DateTime.Parse(attribute[4]);
                trans.SourceWarehouse = attribute[5];
                trans.SourceLocation = attribute[6];
                trans.SourceSalesman = attribute[7];
                trans.DestinationWarehouse = attribute[8];
                trans.DestinationLocation = attribute[9];
                trans.DestinationSalesman = attribute[10];
                trans.PriceCategory = attribute[11];
                trans.PriceType = attribute[12];
                trans.Reason = attribute[13];
                trans.ReasonCode = attribute[14];
                trans.Comment = attribute[15];

                if (attribute[16] == "0")
                {
                    trans.IsPosted = false;
                }
                else
                {
                    trans.IsPosted = true;
                }

                allTrans.Add(trans);

            }

            return allTrans;
        }

       
        public void addTransactionEntry(TransactionModel trans)
        {
            string insert = string.Empty;

            //foreign key id holders
            string transactionTypeId = string.Empty;
            string wareHouseIdSource = string.Empty;
            string wareHouseIdDestination = string.Empty;
            string locationIdSource = string.Empty;
            string locationIdDestination = string.Empty;
            string reasonId = string.Empty;

            int isPosted;

            transactionTypeId = db.Select("select id from transaction_type where transaction_code = '"
                + trans.TransactionCode + "'");
            wareHouseIdSource = db.Select("select warehouse_id from warehouse where code = '"
                + trans.SourceWarehouse + "'");
            wareHouseIdDestination = db.Select("select warehouse_id from warehouse where code = '"
                + trans.DestinationWarehouse + "'");
            locationIdSource = db.Select("select location_id from location where code = '"
                + trans.SourceLocation + "'");
            locationIdDestination = db.Select("select location_id from location where code = '"
                + trans.DestinationLocation + "'");
            reasonId = db.Select("select reasoncode_id from reason_code where reason_code = '"
                + trans.ReasonCode + "'");
            if (trans.IsPosted == true)
            {
                isPosted = 1;
            }
            else
            { isPosted = 0; }

            insert = "insert into transaction_entry values(null,'" +
                "'" + trans.TransactionNumber + "'," +
                "'" + transactionTypeId + "'," +
                "'" + trans.DocumentNumber + "'," +
                "'" + trans.TransactionDate + "'," +
                "'" + wareHouseIdSource + "'," +
                "'" + locationIdSource + "'," +
                "null," +
                "'" + wareHouseIdDestination + "'," +
                "'" + locationIdDestination + "'," +
                "null," +
                "'" + trans.PriceCategory + "'," +
                "'" + trans.PriceType + "'," +
                "'" + reasonId + "'," +
                "'" + trans.Comment + "'," +
                "'" + isPosted + "'" +
                ")";

            db.Insert(insert);


        }
    }
}
