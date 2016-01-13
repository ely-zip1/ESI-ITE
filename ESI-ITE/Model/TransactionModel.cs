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
        //Properties
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
        private StringBuilder insert = new StringBuilder();
        
        public List<TransactionModel> AllTransactions = new List<TransactionModel>();

        //Constructors
        public TransactionModel()
        {
        }

        public TransactionModel(string transactionNumber)
        {
            List<Dictionary<string, string>> records = new List<Dictionary<string, string>>();
            TransactionModel trans = new TransactionModel();

            records = db.SelectMultiple("select * from view_transaction_entry where transaction_number = '" + transactionNumber + "' ");

            foreach (Dictionary<string, string> attribute in records)
            {
                trans.Id = Int32.Parse(attribute["id"]);
                trans.TransactionNumber = attribute["transaction_number"];
                trans.TransactionType = attribute["transaction_type"];
                trans.DocumentNumber = attribute["document_number"];
                trans.TransactionDate = DateTime.Parse(attribute["transaction_date"]);
                trans.SourceWarehouse = attribute["source_warehouse"];
                trans.SourceLocation = attribute["source_location"];
                trans.SourceSalesman = attribute["source_salesman"];
                trans.DestinationWarehouse = attribute["destination_warehouse"];
                trans.DestinationLocation = attribute["destination_location"];
                trans.DestinationSalesman = attribute["destination_salesman"];
                trans.PriceCategory = attribute["price_category"];
                trans.PriceType = attribute["price_type"];
                trans.Reason = attribute["reason_description"];
                trans.ReasonCode = attribute["reason_code"];
                trans.Comment = attribute["comment"];

                if (attribute["status"] == "0")
                {
                    trans.IsPosted = false;
                }
                else
                {
                    trans.IsPosted = true;
                }

            }
        }

        public void FetchAll()
        {
            List<Dictionary<string, string>> records = new List<Dictionary<string, string>>();
            TransactionModel trans = new TransactionModel();

            records = db.SelectMultiple("select * from view_transaction_entry where status = 0 ");

            foreach (Dictionary<string, string> attribute in records)
            {
                trans.Id = Int32.Parse(attribute["id"]);
                trans.TransactionNumber = attribute["transaction_number"];
                trans.TransactionType = attribute["transaction_type"];
                trans.DocumentNumber = attribute["document_number"];
                trans.TransactionDate = DateTime.Parse(attribute["transaction_date"]);
                trans.SourceWarehouse = attribute["source_warehouse"];
                trans.SourceLocation = attribute["source_location"];
                trans.SourceSalesman = attribute["source_salesman"];
                trans.DestinationWarehouse = attribute["destination_warehouse"];
                trans.DestinationLocation = attribute["destination_location"];
                trans.DestinationSalesman = attribute["destination_salesman"];
                trans.PriceCategory = attribute["price_category"];
                trans.PriceType = attribute["price_type"];
                trans.Reason = attribute["reason_description"];
                trans.ReasonCode = attribute["reason_code"];
                trans.Comment = attribute["comment"];

                if (attribute["status"] == "0")
                {
                    trans.IsPosted = false;
                }
                else
                {
                    trans.IsPosted = true;
                }

                AllTransactions.Add(trans);

            }
        }

        public void AddTransactionEntry(TransactionModel trans)
        {
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

            insert.Clear();

            insert.Append("insert into transaction_entry values(");
            insert.Append("null,");
            insert.Append("'" + trans.TransactionNumber + "',");
            insert.Append("'" + transactionTypeId + "',");
            insert.Append("'" + trans.DocumentNumber + "',");
            insert.Append("'" + trans.TransactionDate.ToString("MM/dd/yyyy") + "',");
            insert.Append("'" + wareHouseIdSource + "',");
            insert.Append("'" + locationIdSource + "',");
            insert.Append("null,");
            insert.Append("'" + wareHouseIdDestination + "',");
            insert.Append("'" + locationIdDestination + "',");
            insert.Append("null,");
            insert.Append("'" + trans.PriceCategory + "',");
            insert.Append("'" + trans.PriceType + "',");
            insert.Append("'" + reasonId + "',");
            insert.Append("'" + trans.Comment + "',");
            insert.Append("'" + isPosted + "'");
            insert.Append(")");

            db.Insert(insert.ToString());

            insert.Clear();

        }

    }
}
