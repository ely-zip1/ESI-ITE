using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ESI_ITE.Data_Access;
using System.ComponentModel;
using System.Text.RegularExpressions;

namespace ESI_ITE.Model
{
    public class TransactionModel : ObjectBase, IDataErrorInfo
    {

        #region Constructors
        public TransactionModel()
        {

        }

        public TransactionModel(TransactionModel source)
        {
            Id = source.Id;
            TransactionNumber = source.TransactionNumber;
            TransactionCode = source.TransactionCode;
            TransactionType = source.TransactionType;
            DocumentNumber = source.DocumentNumber;
            TransactionDate = source.TransactionDate;
            SourceWarehouse = source.SourceWarehouse;
            SourceWarehouseCode = source.SourceWarehouseCode;
            SourceLocation = source.SourceLocation;
            SourceLocationCode = source.SourceLocationCode;
            DestinationWarehouse = source.DestinationWarehouse;
            DestinationWarehouseCode = source.DestinationWarehouseCode;
            DestinationLocation = source.DestinationLocation;
            DestinationLocationCode = source.DestinationLocationCode;
            PriceCategory = source.PriceCategory;
            PriceType = source.PriceType;
            Reason = source.Reason;
            ReasonCode = source.ReasonCode;
            Comment = source.Comment;
            IsPrinted = source.IsPrinted;

        }
        #endregion

        #region Properties

        private int id;
        public int Id
        {
            get { return id; }
            set
            {
                if (id != value)
                {
                    id = value;
                    OnPropertyChanged("Id");
                }
            }
        }

        private string transactionNumber;
        public string TransactionNumber
        {
            get { return transactionNumber; }
            set
            {
                if (transactionNumber != value)
                {
                    transactionNumber = value;
                    OnPropertyChanged("TransactioNumber");
                }
            }
        }

        private string transactionCode;
        public string TransactionCode
        {
            get { return transactionCode; }
            set
            {
                if (transactionCode != value)
                {
                    transactionCode = value;
                    OnPropertyChanged("TransactionCode");
                }
            }
        }

        private string transactionType;
        public string TransactionType
        {
            get { return transactionType; }
            set
            {
                if (transactionType != value)
                {
                    transactionType = value;
                    OnPropertyChanged("TransactionType");
                }
            }
        }

        private string documentNumber;
        public string DocumentNumber
        {
            get { return documentNumber; }
            set
            {
                if (documentNumber != value)
                {
                    documentNumber = value;
                    OnPropertyChanged("DocumentNumber");
                }
            }
        }

        private DateTime transactionDate;
        public DateTime TransactionDate
        {
            get { return transactionDate; }
            set
            {
                if (transactionDate != value)
                {
                    transactionDate = value;
                    OnPropertyChanged("TransactionDate");
                }
            }
        }

        private string sourceWarehouse;
        public string SourceWarehouse
        {
            get { return sourceWarehouse; }
            set
            {
                if (sourceWarehouse != value)
                {
                    sourceWarehouse = value;
                    OnPropertyChanged("SourceWarehouse");
                }
            }
        }

        private string sourceWarehouseCode;
        public string SourceWarehouseCode
        {
            get { return sourceWarehouseCode; }
            set
            {
                if (sourceWarehouseCode != value)
                {
                    sourceWarehouseCode = value;
                    OnPropertyChanged("SourceWarehouseCode");
                }
            }
        }

        private string destinationWarehouse;
        public string DestinationWarehouse
        {
            get { return destinationWarehouse; }
            set
            {
                if (destinationWarehouse != value)
                {
                    destinationWarehouse = value;
                    OnPropertyChanged("DestinationWarehouse");
                }
            }
        }

        private string destinationWarehouseCode;
        public string DestinationWarehouseCode
        {
            get { return destinationWarehouseCode; }
            set
            {
                if (destinationWarehouseCode != value)
                {
                    destinationWarehouseCode = value;
                    OnPropertyChanged("DestinationWarehouseCode");
                }
            }
        }

        private string sourceLocation;
        public string SourceLocation
        {
            get { return sourceLocation; }
            set
            {
                if (sourceLocation != value)
                {
                    sourceLocation = value;
                    OnPropertyChanged("SourceLocation");
                }
            }
        }

        private string sourceLocationCode;
        public string SourceLocationCode
        {
            get { return sourceLocationCode; }
            set
            {
                if (sourceLocationCode != value)
                {
                    sourceLocationCode = value;
                    OnPropertyChanged("SourceLocationCode");
                }
            }
        }

        private string destinationLocation;
        public string DestinationLocation
        {
            get { return destinationLocation; }
            set
            {
                if (destinationLocation != value)
                {
                    destinationLocation = value;
                    OnPropertyChanged("DestinationLocation");
                }
            }
        }

        private string destinationLocationCode;
        public string DestinationLocationCode
        {
            get { return destinationLocationCode; }
            set
            {
                if (destinationLocationCode != value)
                {
                    destinationLocationCode = value;
                    OnPropertyChanged("DestinationLocationCode");
                }
            }
        }

        private string sourceSalesman;
        public string SourceSalesman
        {
            get { return sourceSalesman; }
            set
            {
                if (sourceSalesman != value)
                {
                    sourceSalesman = value;
                    OnPropertyChanged("SourceSalesman");
                }
            }
        }

        private string destinationSalesman;
        public string DestinationSalesman
        {
            get { return destinationSalesman; }
            set
            {
                if (destinationSalesman != value)
                {
                    destinationSalesman = value;
                    OnPropertyChanged("DestinationSalesman");
                }
            }
        }

        private string priceCategory;
        public string PriceCategory
        {
            get { return priceCategory; }
            set
            {
                if (priceCategory != value)
                {
                    priceCategory = value;
                    OnPropertyChanged("PriceCategory");
                }
            }
        }

        private string priceType;
        public string PriceType
        {
            get { return priceType; }
            set
            {
                if (priceType != value)
                {
                    priceType = value;
                    OnPropertyChanged("PriceType");
                }
            }
        }

        private string reason;
        public string Reason
        {
            get { return reason; }
            set
            {
                if (reason != value)
                {
                    reason = value;
                    OnPropertyChanged("Reason");
                }
            }
        }

        private string reasonCode;
        public string ReasonCode
        {
            get { return reasonCode; }
            set
            {
                if (reasonCode != value)
                {
                    reasonCode = value;
                    OnPropertyChanged("ReasonCode");
                }
            }
        }

        private string comment;
        public string Comment
        {
            get { return comment; }
            set
            {
                if (comment != value)
                {
                    comment = value;
                    OnPropertyChanged("Comment");
                }
            }
        }

        private bool isPosted;
        public bool IsPrinted
        {
            get { return isPosted; }
            set
            {
                if (isPosted != value)
                {
                    isPosted = value;
                    OnPropertyChanged("IsPrinted");
                }
            }
        }

        private DataAccess db = new DataAccess();

        private StringBuilder insert = new StringBuilder();

        private List<TransactionModel> _allTransactions = new List<TransactionModel>();

        #endregion

        public TransactionModel Fetch(string transactionNumber)
        {
            var records = new List<CloneableDictionary<string, string>>();
            var trans = new TransactionModel();

            records = db.SelectMultiple("select * from view_transaction_entry where transaction_number = '" + transactionNumber + "' ");

            foreach (var attribute in records)
            {
                trans.Id = int.Parse(attribute["id"]);
                trans.TransactionNumber = attribute["transaction_number"];
                trans.transactionCode = attribute["transaction_code"];
                trans.TransactionType = attribute["transaction_type"];
                trans.DocumentNumber = attribute["document_number"];
                trans.TransactionDate = DateTime.Parse(attribute["transaction_date"]);
                trans.SourceWarehouseCode = attribute["source_warehouse_code"];
                trans.SourceWarehouse = attribute["source_warehouse"];
                trans.SourceLocationCode = attribute["source_location_code"];
                trans.SourceLocation = attribute["source_location"];
                //trans.SourceSalesman = attribute["source_salesman"];
                trans.DestinationWarehouseCode = attribute["destination_warehouse_code"];
                trans.DestinationWarehouse = attribute["destination_warehouse"];
                trans.DestinationLocationCode = attribute["destination_location_code"];
                trans.DestinationLocation = attribute["destination_location"];
                //trans.DestinationSalesman = attribute["destination_salesman"];
                trans.PriceCategory = attribute["price_category"];
                trans.PriceType = attribute["price_type"];
                trans.Reason = attribute["reason_description"];
                trans.ReasonCode = attribute["reason_code"];
                trans.Comment = attribute["comment"];

                if (attribute["status"] == "0")
                {
                    trans.IsPrinted = false;
                }
                else
                {
                    trans.IsPrinted = true;
                }
            }
            return trans;
        }

        public List<TransactionModel> FetchAll()
        {
            _allTransactions.Clear();

            var records = new List<Dictionary<string, string>>();

            //records.AddRange(db.SelectMultiple("select * from view_transaction_entry where status = 0 "));

            foreach (var row in (db.SelectMultiple("select * from view_transaction_entry where status = 0 ")))
            {

                var trans = new TransactionModel();
                var clone = row.Clone();

                trans.Id = int.Parse(row["id"]);
                trans.TransactionNumber = row["transaction_number"];
                trans.TransactionCode = row["transaction_code"];
                trans.TransactionType = row["transaction_type"];
                trans.DocumentNumber = row["document_number"];
                trans.TransactionDate = DateTime.Parse(row["transaction_date"]);
                trans.SourceWarehouse = row["source_warehouse"];
                trans.SourceWarehouseCode = row["source_warehouse_code"];
                trans.SourceLocation = row["source_location"];
                trans.SourceLocationCode = row["source_location_code"];
                //trans.SourceSalesman = attribute["source_salesman"];
                trans.DestinationWarehouse = row["destination_warehouse"];
                trans.DestinationWarehouseCode = row["destination_warehouse_code"];
                trans.DestinationLocation = row["destination_location"];
                trans.DestinationLocationCode = row["destination_location_code"];
                //trans.DestinationSalesman = attribute["destination_salesman"];
                trans.PriceCategory = row["price_category"];
                trans.PriceType = row["price_type"];
                trans.Reason = row["reason_description"];
                trans.ReasonCode = row["reason_code"];
                trans.Comment = row["comment"];

                trans.IsPrinted = (row["status"] == "0") ? false : true;

                _allTransactions.Add(trans);
            }
            return _allTransactions;
        }

        public void AddTransactionEntry(TransactionModel trans)
        {
            //foreign key id holders
            string transactionTypeId = db.Select("select id from transaction_type where transaction_code = '" + trans.TransactionCode + "'");
            string wareHouseIdSource = db.Select("select warehouse_id from warehouse where code = '" + trans.SourceWarehouseCode + "'");
            string locationIdSource = db.Select("select location_id from location where code = '" + trans.SourceLocationCode + "'");
            string reasonId = db.Select("select reasoncode_id from reason_code where reason_code = '" + trans.ReasonCode + "'");

            string wareHouseIdDestination = string.IsNullOrWhiteSpace(trans.DestinationWarehouseCode)
                ? "null"
                : db.Select("select warehouse_id from warehouse where code = '" + trans.DestinationWarehouseCode + "'");


            string locationIdDestination = string.IsNullOrWhiteSpace(trans.DestinationLocationCode)
                ? "null"
                : db.Select("select location_id from location where code = '" + trans.DestinationLocationCode + "'");


            int isPosted;

            if (trans.IsPrinted == true)
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
            insert.Append("'" + trans.TransactionDate.ToString("yyyy-MM-dd") + "',");
            insert.Append("'" + wareHouseIdSource + "',");
            insert.Append("'" + locationIdSource + "',");
            insert.Append("null,");
            insert.Append("" + wareHouseIdDestination + ",");
            insert.Append("" + locationIdDestination + ",");
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

        public void DeleteTransaction(string transactionNumber)
        {
            string transactionId =
                db.Select("select entry_id from transaction_entry where trans_no = '" + transactionNumber + "'");

            List<string> commands = new List<string>();

            commands.Add("Delete from inventory_dummy where transaction_link = " + transactionId + "");
            commands.Add("Delete from transaction_entry where trans_no = '" + transactionNumber + "'");

            db.RunMySqlTransaction(commands);
        }


        #region IDataErrorInfo Members
        string IDataErrorInfo.Error
        {
            get
            {
                return null;
            }
        }

        string IDataErrorInfo.this[string propertyName]
        {
            get
            {
                return GetValidationError(propertyName);

            }
        }
        #endregion

        #region Validation


        static readonly string[] ValidatedProperties =
        {
            "Id",
            "TransactionNumber",
            "TransactionCode",
            "TransactionType",
            "DocumentNumber",
            "TransactionDate",
            "SourceWarehouse",
            "DestinationWarehouse",
            "SourceLocation",
            "DestinationLocation",
            "PriceCategory",
            "PriceType",
            "Reason",
            "ReasonCode",
            "Comment"
        };

        public bool Isvalid
        {
            get
            {
                int i = 0;
                foreach (var property in ValidatedProperties)
                {
                    if (GetValidationError(property) != null)
                        i++;
                }

                if (i > 0)
                    return false;
                else
                    return true;
            }
        }

        private string GetValidationError(string propertyName)
        {
            string error = null;

            switch (propertyName)
            {
                case "TransactionNumber":
                    error = ValidateNullOrEmpty(propertyName, this.TransactionNumber.ToString());
                    break;
                case "TransactionCode":
                    error = ValidateNullOrEmpty(propertyName, this.TransactionCode.ToString());
                    break;
                case "TransactionType":
                    error = ValidateNullOrEmpty(propertyName, this.TransactionType.ToString());
                    break;
                case "TransactionDate":
                    error = ValidateNullOrEmpty(propertyName, this.TransactionDate.ToString());
                    break;
                case "SourceWarehouse":
                    error = ValidateNullOrEmpty(propertyName, this.SourceWarehouse.ToString());
                    break;
                case "DestinationWarehouse":
                    error = ValidateNullOrEmpty(propertyName, this.DestinationWarehouse.ToString());
                    break;
                case "PriceCategory":
                    error = ValidateNullOrEmpty(propertyName, this.PriceCategory.ToString());
                    break;
                case "PriceType":
                    error = ValidateNullOrEmpty(propertyName, this.PriceType.ToString());
                    break;
                case "Reason":
                    error = ValidateNullOrEmpty(propertyName, this.Reason.ToString());
                    break;
                case "ReasonCode":
                    error = ValidateNullOrEmpty(propertyName, this.ReasonCode.ToString());
                    break;
                case "Comment":
                    error = ValidateNullOrEmpty(propertyName, this.Comment.ToString());
                    break;
                case "DocumentNumber":
                    error = ValidateDocumentNumber();
                    break;
            }

            return error;
        }

        private string ValidateDocumentNumber()
        {
            Regex regex = new Regex("^[A-Za-z]*$");
            if (string.IsNullOrWhiteSpace(DocumentNumber))
            {
                return "Document Number cannot be empty!";
            }
            else if (regex.IsMatch(DocumentNumber))
            {
                return "Document Number cannot accept non-numeric values!";
            }

            return null;
        }

        private string ValidateNullOrEmpty(string propertyName, string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return propertyName + " cannot be empty!";
            else
                return null;
        }

        #endregion


    }
}
