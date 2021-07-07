using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ESI_ITE.Data_Access;
using System.ComponentModel;

namespace ESI_ITE.Model
{
    public class ReasonsModel : ObjectBase, IDataErrorInfo
    {
        public ReasonsModel()
        {

        }

        public ReasonsModel(ReasonsModel source)
        {
            Id = source.Id;
            TransactionType = source.TransactionType;
            ReasonCode = source.ReasonCode;
            Description = source.Description;
        }

        #region Properties
        DataAccess db = new DataAccess();
        private List<ReasonsModel> _reasons = new List<ReasonsModel>();

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

        private string description;
        public string Description
        {
            get { return description; }
            set
            {
                if (description != value)
                {
                    description = value;
                    OnPropertyChanged("Description");
                }
            }
        }

        #endregion

        public List<ReasonsModel> FetchAll()
        {
            _reasons.Clear();

            List<CloneableDictionary<string, string>> table = db.SelectMultiple("select * from reason_code");

            foreach (var row in table)
            {
                var temp = new ReasonsModel();
                var clone = row.Clone();

                temp.Id = Int32.Parse(row["reasoncode_id"]);
                temp.TransactionType = row["transaction_type"];
                temp.ReasonCode = row["reason_code"];
                temp.Description = row["reason_description"];

                _reasons.Add(temp);
            }
            return _reasons;
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

        private string GetValidationError(string propertyName)
        {
            string error = null;
            switch (propertyName)
            {
                case "TransactionType":
                    error = ValidateNullOrEmpty(propertyName, this.TransactionType.ToString());
                    break;
                case "ReasonCode":
                    error = ValidateNullOrEmpty(propertyName, this.ReasonCode.ToString());
                    break;
                case "Description":
                    error = ValidateNullOrEmpty(propertyName, this.Description.ToString());
                    break;
            }
            return error;
        }


        private string ValidateNullOrEmpty(string propertyName, string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return propertyName + " cannot be empty!";
            }
            return null;
        }

        #endregion

    }
}
