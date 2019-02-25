using ESI_ITE.Data_Access;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESI_ITE.Model
{
    public class DebitNoteHeaderModel : IModelTemplate
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

        private string dnNumber;
        public string DnNumber
        {
            get
            {
                return dnNumber;
            }
            set
            {
                dnNumber = value;
            }
        }

        private DateTime dnDate;
        public DateTime DnDate
        {
            get
            {
                return dnDate;
            }
            set
            {
                dnDate = value;
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

        private decimal dnAmount;
        public decimal DnAmount
        {
            get
            {
                return dnAmount;
            }
            set
            {
                dnAmount = value;
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
            var dnObect = item as DebitNoteHeaderModel;

            var sb = new StringBuilder();

            sb.Append("insert into dn_header values(");
            sb.Append("null,");
            sb.Append("'" + dnObect.DnNumber + "',");
            sb.Append("str_to_date('" + dnObect.DnDate.ToString("MM/dd/yyyy") + "','%m/%d/%Y'),");
            sb.Append("'" + dnObect.ReferenceNumber + "',");
            sb.Append("'" + dnObect.CustomerId + "',");
            sb.Append("'" + dnObect.WarehouseId + "',");
            sb.Append("'" + dnObect.Comment + "',");
            sb.Append("'" + dnObect.DnAmount + "',");
            sb.Append("'" + dnObect.TotalCases + "',");
            sb.Append("'" + dnObect.TotalPieces + "',");
            sb.Append("'");
            sb.Append(dnObect.IsPrinted ? "1" : "0");
            sb.Append("',");
            sb.Append("'" + dnObect.ReturnCodeId + "',");
            sb.Append("'" + dnObect.PriceUsed + "',");
            sb.Append("'" + dnObect.UserId + "',");
            sb.Append("'" + dnObect.ReasonId + "',");
            sb.Append("'" + dnObect.PriceTypeId + "'");
            sb.Append(")");

            db.Insert(sb.ToString());
        }

        public void DeleteItem(string qry)
        {
            throw new NotImplementedException();
        }

        public void DeleteItem(DebitNoteHeaderModel dnHeader)
        {
            if (dnHeader.DnNumber != "")
            {
                db.Delete("delete from dn_header where id = '" + dnHeader.Id + "'");
            }
        }

        public object Fetch(string id, string type)
        {
            var dnObject = new DebitNoteHeaderModel();
            var result = new List<CloneableDictionary<string, string>>();

            if (type == "code")
            {
                var tempId = db.Select("select id from dn_header where dn_number = '" + id + "'");
                if (!string.IsNullOrWhiteSpace(tempId))
                    result = db.SelectMultiple("select * from dn_header where id = '" + tempId + "'");
            }
            else if (type == "id")
            {
                result = db.SelectMultiple("select * from dn_header where id = '" + id + "'");
            }

            foreach (var row in result)
            {
                var clone = row.Clone();

                dnObject.Id = int.Parse(row["id"]);
                dnObject.DnNumber = row["dn_number"];
                dnObject.DnDate = DateTime.Parse(row["dn_date"]);
                dnObject.ReferenceNumber = row["reference"];
                dnObject.CustomerId = int.Parse(row["customer_id"]);
                dnObject.WarehouseId = int.Parse(row["warehouse_id"]);
                dnObject.Comment = row["comment"];
                dnObject.DnAmount = decimal.Parse(row["dn_amount"]);
                dnObject.TotalCases = int.Parse(row["total_cases"]);
                dnObject.TotalPieces = int.Parse(row["total_pieces"]);
                dnObject.IsPrinted = row["is_printed"] == "1" ? true : false;
                dnObject.ReturnCodeId = int.Parse(row["returncode_id"]);
                dnObject.PriceUsed = row["price_used"];
                dnObject.UserId = int.Parse(row["user_id"]);
                dnObject.ReasonId = int.Parse(row["reason_id"]);
                dnObject.PriceTypeId = int.Parse(row["pricetype_id"]);

                break;
            }

            return dnObject;
        }

        public List<object> FetchAll()
        {
            var results = db.SelectMultiple("select * from dn_header");

            var dnHeaderList = new List<object>();
            foreach (var row in results)
            {
                var clone = row.Clone();

                var dnObject = new DebitNoteHeaderModel();

                dnObject.Id = int.Parse(row["id"]);
                dnObject.DnNumber = row["dn_number"];
                dnObject.DnDate = DateTime.Parse(row["dn_date"]);
                dnObject.ReferenceNumber = row["reference"];
                dnObject.CustomerId = int.Parse(row["customer_id"]);
                dnObject.WarehouseId = int.Parse(row["warehouse_id"]);
                dnObject.Comment = row["comment"];
                dnObject.DnAmount = decimal.Parse(row["dn_amount"]);
                dnObject.TotalCases = int.Parse(row["total_cases"]);
                dnObject.TotalPieces = int.Parse(row["total_pieces"]);
                dnObject.IsPrinted = row["is_printed"] == "1" ? true : false;
                dnObject.ReturnCodeId = int.Parse(row["returncode_id"]);
                dnObject.PriceUsed = row["price_used"];
                dnObject.UserId = int.Parse(row["user_id"]);
                dnObject.ReasonId = int.Parse(row["reason_id"]);
                dnObject.PriceTypeId = int.Parse(row["pricetype_id"]);

                dnHeaderList.Add(dnObject);
            }

            return dnHeaderList;
        }

        public void UpdateItem(string qry)
        {
            throw new NotImplementedException();
        }

        public int GetCases(string dnHeadId)
        {
            return int.Parse(db.Select("select total_cases from dn_header where id = '" + dnHeadId + "'"));
        }

        public int GetPieces(string dnHeadId)
        {
            return int.Parse(db.Select("select total_pieces from dn_header where id = '" + dnHeadId + "'"));
        }

        public void AddCasesPieces(string dnHeadId, int casesToBeAdded, int piecesToBeAdded)
        {
            var currentCases = int.Parse(db.Select("select total_cases from dn_header where id = '" + dnHeadId + "'"));
            var currentPieces = int.Parse(db.Select("select total_pieces from dn_header where id = '" + dnHeadId + "'"));

            var totalCases = currentCases + casesToBeAdded;
            var totalPieces = currentPieces + piecesToBeAdded;

            db.Update("update dn_header set total_cases = '" + totalCases + "', total_pieces = '" + TotalPieces + "' where id = '" + dnHeadId + "'");
        }

        public void SubtractCasesPieces(string dnHeadId, int casesToBeSubtracted, int piecesToBeSubtracted)
        {
            var currentCases = int.Parse(db.Select("select total_cases from dn_header where id = '" + dnHeadId + "'"));
            var currentPieces = int.Parse(db.Select("select total_pieces from dn_header where id = '" + dnHeadId + "'"));

            var totalCases = currentCases - casesToBeSubtracted;
            var totalPieces = currentPieces - piecesToBeSubtracted;

            db.Update("update dn_header set total_cases = '" + totalCases + "', total_pieces = '" + TotalPieces + "' where id = '" + dnHeadId + "'");
        }
    }
}
