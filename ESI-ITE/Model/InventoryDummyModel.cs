using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ESI_ITE.Data_Access;
using System.ComponentModel;

namespace ESI_ITE.Model
{
    public class InventoryDummyModel : ObjectBase, IDataErrorInfo
    {
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

        private string location;
        public string Location
        {
            get { return location; }
            set
            {
                if (location != value)
                {
                    location = value;
                    OnPropertyChanged("Location");
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

        private string itemCode;
        public string ItemCode
        {
            get { return itemCode; }
            set
            {
                if (itemCode != value)
                {
                    itemCode = value;
                    OnPropertyChanged("ItemCode");
                }
            }
        }

        private string itemDescription;
        public string ItemDescription
        {
            get { return itemDescription; }
            set
            {
                if (itemDescription != value)
                {
                    itemDescription = value;
                    OnPropertyChanged("ItemDescription");
                }
            }
        }

        private int cases;
        public int Cases
        {
            get { return cases; }
            set
            {
                if (cases != value)
                {
                    cases = value;
                    OnPropertyChanged("Cases");
                }
            }
        }

        private int pieces;
        public int Pieces
        {
            get { return pieces; }
            set
            {
                if (pieces != value)
                {
                    pieces = value;
                    OnPropertyChanged("Pieces");
                }
            }
        }

        private DateTime expiration;
        public DateTime Expiration
        {
            get { return expiration; }
            set
            {
                if (expiration != value)
                {
                    expiration = value;
                    OnPropertyChanged("Expiration");
                }
            }
        }

        private decimal pricePerPiece;
        public decimal PricePerPiece
        {
            get { return pricePerPiece; }
            set
            {
                if (pricePerPiece != value)
                {
                    pricePerPiece = value;
                    OnPropertyChanged("PricePerPiece");
                }
            }
        }

        private decimal lineAmount;
        public decimal LineAmount
        {
            get { return lineAmount; }
            set
            {
                if (lineAmount != value)
                {
                    lineAmount = value;
                    OnPropertyChanged("LineAmount");
                }
            }
        }

        DataAccess db = new DataAccess();

        private StringBuilder insert = new StringBuilder();
        #endregion

        #region Methods
        public List<InventoryDummyModel> FetchAll(string transactionNumber)
        {
            List<CloneableDictionary<string, string>> record = new List<CloneableDictionary<string, string>>();
            List<InventoryDummyModel> transactionDetails = new List<InventoryDummyModel>();
            InventoryDummyModel td = new InventoryDummyModel();

            string transactionEntryId = db.Select("select entry_id from transaction_entry " +
                "where trans_no = '" + transactionNumber + "'");

            record = db.SelectMultiple("select * from view_inventory_dummy " +
                "where transaction_entry_id = '" + transactionEntryId + "'");

            foreach (var row in record)
            {
                td.Id = Int32.Parse(row["id"]);
                td.TransactionCode = row["transaction_code"];
                td.Location = row["location_code"];
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
        #endregion

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
        private readonly string[] ValidatedProperties = {
            "PriceType",
            "ItemCode",
            "Cases",
            "Pieces",
            "Expiration",
            "PricePerPiece",
            "LineAmount"
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
                case "TransactionCode":
                    error = ValidateNullOrEmpty(propertyName, this.TransactionCode.ToString());
                    break;
                case "Location":
                    error = ValidateNullOrEmpty(propertyName, this.Location.ToString());
                    break;
                case "PriceType":
                    error = ValidateNullOrEmpty(propertyName, this.PriceType.ToString());
                    break;
                case "ItemCode":
                    error = ValidateNullOrEmpty(propertyName, this.ItemCode.ToString());
                    break;
                case "Cases":
                    error = ValidateCasesPieces(propertyName, this.Cases.ToString());
                    break;
                case "Pieces":
                    error = ValidateCasesPieces(propertyName, this.Pieces.ToString());
                    break;
                case "Expiration":
                    error = ValidateDate(propertyName, this.Expiration.ToString());
                    break;
                case "PricePerPiece":
                    error = ValidateNullOrEmpty(propertyName, this.PricePerPiece.ToString());
                    break;
                case "LineAmount":
                    error = ValidateNullOrEmpty(propertyName, this.LineAmount.ToString());
                    break;
            }
            return error;
        }

        private string ValidateDate(string propertyName, string value)
        {
            DateTime i;
            if (ValidateNullOrEmpty(propertyName, value) != null)
            {
                return propertyName + " cannot be empty!";
            }

            if (DateTime.TryParse(value, out i))
            {
                if (DateTime.Now.Date >= i)
                {
                    return "Invalid date! Value must be later than today.";
                }
            }
            else
            {
                return "Invalid date!";
            }
            return null;
        }

        private string ValidateNullOrEmpty(string propertyName, string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return propertyName + " cannot be empty!";
            }
            return null;
        }

        private string ValidateCasesPieces(string propertyName, string value)
        {
            int i;
            if (ValidateNullOrEmpty(propertyName, value) != null)
            {
                return propertyName + " cannot be empty!";
            }
            else if (!int.TryParse(value, out i))
            {
                return "Field only accepts numbers!";
            }
            return null;
        }
        #endregion

    }
}
