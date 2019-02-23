using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ESI_ITE.Data_Access;

namespace ESI_ITE.Model
{
    public class CreditNoteHeaderModel : IModelTemplate
    {
        #region Properties
        DataAccess db = new DataAccess();

        private int id;
        public int Id
        {
            get
            {
                return id;
            }
            set
            {
                id = value;
            }
        }

        private string cnNumber;
        public string CnNumber
        {
            get
            {
                return cnNumber;
            }
            set
            {
                cnNumber = value;
            }
        }

        private DateTime cnDate;
        public DateTime CnDate
        {
            get
            {
                return cnDate;
            }
            set
            {
                cnDate = value;
            }
        }

        private string referenceNumber;
        public string ReferenceNumber
        {
            get
            {
                return referenceNumber;
            }
            set
            {
                referenceNumber = value;
            }
        }

        private int customerId;
        public int CustomerId
        {
            get
            {
                return customerId;
            }
            set
            {
                customerId = value;
            }
        }

        private int warehouseId;
        public int WarehouseId
        {
            get
            {
                return warehouseId;
            }
            set
            {
                warehouseId = value;
            }
        }

        private string comment;
        public string Comment
        {
            get
            {
                return comment;
            }
            set
            {
                comment = value;
            }
        }

        private decimal cnAmount;
        public decimal CnAmount
        {
            get
            {
                return cnAmount;
            }
            set
            {
                cnAmount = value;
            }
        }

        private int totalCases;
        public int TotalCases
        {
            get
            {
                return totalCases;
            }
            set
            {
                totalCases = value;
            }
        }

        private int totalPieces;
        public int TotalPieces
        {
            get
            {
                return totalPieces;
            }
            set
            {
                totalPieces = value;
            }
        }

        private bool isPrinted;
        public bool IsPrinted
        {
            get
            {
                return isPrinted;
            }
            set
            {
                isPrinted = value;
            }
        }

        private int returnCodeId;
        public int ReturnCodeId
        {
            get
            {
                return returnCodeId;
            }
            set
            {
                returnCodeId = value;
            }
        }

        private string priceUsed;
        public string PriceUsed
        {
            get
            {
                return priceUsed;
            }
            set
            {
                priceUsed = value;
            }
        }

        private int userId;
        public int UserId
        {
            get
            {
                return userId;
            }
            set
            {
                userId = value;
            }
        }

        private int reasonId;
        public int ReasonId
        {
            get
            {
                return reasonId;
            }
            set
            {
                reasonId = value;
            }
        }

        private int priceTypeId;
        public int PriceTypeId
        {
            get
            {
                return priceTypeId;
            }
            set
            {
                priceTypeId = value;
            }
        }


        #endregion

        public void AddNew(object item)
        {
            var cnObect = item as CreditNoteHeaderModel;

            var sb = new StringBuilder();

            sb.Append("insert into cn_header values(");
            sb.Append("null,");
            sb.Append("'" + cnObect.CnNumber + "',");
            sb.Append("str_to_date('" + cnObect.CnDate.ToString("MM/dd/yyyy") + "','%m/%d/%Y'),");
            sb.Append("'" + cnObect.ReferenceNumber + "',");
            sb.Append("'" + cnObect.CustomerId + "',");
            sb.Append("'" + cnObect.WarehouseId + "',");
            sb.Append("'" + cnObect.Comment + "',");
            sb.Append("'" + cnObect.CnAmount + "',");
            sb.Append("'" + cnObect.TotalCases + "',");
            sb.Append("'" + cnObect.TotalPieces + "',");
            sb.Append("'");
            sb.Append(cnObect.IsPrinted ? "1" : "0");
            sb.Append("',");
            sb.Append("'" + cnObect.ReturnCodeId + "',");
            sb.Append("'" + cnObect.PriceUsed + "',");
            sb.Append("'" + cnObect.UserId + "',");
            sb.Append("'" + cnObect.ReasonId + "',");
            sb.Append("'" + cnObect.PriceTypeId + "'");
            sb.Append(")");

            db.Insert(sb.ToString());
        }

        public void DeleteItem(string qry)
        {
            throw new NotImplementedException();
        }

        public void DeleteItem(CreditNoteHeaderModel cnHeader)
        {
            if (cnHeader.CnNumber != "")
            {
                db.Delete("delete from cn_header where id = '" + cnHeader.Id + "'");
            }
        }

        public object Fetch(string id, string type)
        {
            var cnObject = new CreditNoteHeaderModel();
            var result = new List<CloneableDictionary<string, string>>();

            if (type == "code")
            {
                var tempId = db.Select("select id from cn_header where cn_number = '" + id + "'");
                if (!string.IsNullOrWhiteSpace(tempId))
                    result = db.SelectMultiple("select * from cn_header where id = '" + tempId + "'");
            }
            else if (type == "id")
            {
                result = db.SelectMultiple("select * from cn_header where id = '" + Id + "'");
            }

            foreach (var row in result)
            {
                var clone = row.Clone();

                cnObject.Id = int.Parse(row["id"]);
                cnObject.CnNumber = row["cn_number"];
                cnObject.CnDate = DateTime.Parse(row["cn_date"]);
                cnObject.ReferenceNumber = row["reference"];
                cnObject.CustomerId = int.Parse(row["customer_id"]);
                cnObject.WarehouseId = int.Parse(row["warehouse_id"]);
                cnObject.Comment = row["comment"];
                cnObject.CnAmount = decimal.Parse(row["cn_amount"]);
                cnObject.TotalCases = int.Parse(row["total_cases"]);
                cnObject.TotalPieces = int.Parse(row["total_pieces"]);
                cnObject.IsPrinted = row["is_printed"] == "1" ? true : false;
                cnObject.ReturnCodeId = int.Parse(row["returncode_id"]);
                cnObject.PriceUsed = row["price_used"];
                cnObject.UserId = int.Parse(row["user_id"]);
                cnObject.ReasonId = int.Parse(row["reason_id"]);
                cnObject.PriceTypeId = int.Parse(row["pricetype_id"]);

                break;
            }

            return cnObject;
        }

        public List<object> FetchAll()
        {
            var results = db.SelectMultiple("select * from cn_header");

            var cnHeaderList = new List<object>();
            foreach (var row in results)
            {
                var clone = row.Clone();

                var cnObject = new CreditNoteHeaderModel();

                cnObject.Id = int.Parse(row["id"]);
                cnObject.CnNumber = row["cn_number"];
                cnObject.CnDate = DateTime.Parse(row["cn_date"]);
                cnObject.ReferenceNumber = row["reference"];
                cnObject.CustomerId = int.Parse(row["customer_id"]);
                cnObject.WarehouseId = int.Parse(row["warehouse_id"]);
                cnObject.Comment = row["comment"];
                cnObject.CnAmount = decimal.Parse(row["cn_amount"]);
                cnObject.TotalCases = int.Parse(row["total_cases"]);
                cnObject.TotalPieces = int.Parse(row["total_pieces"]);
                cnObject.IsPrinted = row["is_printed"] == "1" ? true : false;
                cnObject.ReturnCodeId = int.Parse(row["returncode_id"]);
                cnObject.PriceUsed = row["price_used"];
                cnObject.UserId = int.Parse(row["user_id"]);
                cnObject.ReasonId = int.Parse(row["reason_id"]);
                cnObject.PriceTypeId = int.Parse(row["pricetype_id"]);

                cnHeaderList.Add(cnObject);
            }

            return cnHeaderList;
        }

        public void UpdateItem(string qry)
        {
            throw new NotImplementedException();
        }
    }
}
