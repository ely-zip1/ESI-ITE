using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ESI_ITE.Data_Access;

namespace ESI_ITE.Model
{
    public class InvoiceHeadModel : IModelTemplate
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

        private string invoiceNumber;
        public string InvoiceNumber
        {
            get
            {
                return invoiceNumber;
            }
            set
            {
                invoiceNumber = value;
            }
        }

        private DateTime invoiceDate;
        public DateTime InvoiceDate
        {
            get
            {
                return invoiceDate;
            }
            set
            {
                invoiceDate = value;
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

        private int pickId;
        public int PickId
        {
            get
            {
                return pickId;
            }
            set
            {
                pickId = value;
            }
        }

        private int orderId;
        public int OrderId
        {
            get
            {
                return orderId;
            }
            set
            {
                orderId = value;
            }
        }

        private int cases;
        public int Cases
        {
            get
            {
                return cases;
            }
            set
            {
                cases = value;
            }
        }

        private int pieces;
        public int Pieces
        {
            get
            {
                return pieces;
            }
            set
            {
                pieces = value;
            }
        }

        private decimal invoiceAmount;
        public decimal InvoiceAmount
        {
            get
            {
                return invoiceAmount;
            }
            set
            {
                invoiceAmount = value;
            }
        }

        #endregion

        public void AddNew(object item)
        {
            var invoiceHeadObj = item as InvoiceHeadModel;

            StringBuilder sb = new StringBuilder();
            sb.Append("insert into invoice_head values(null, ");
            sb.Append("'" + invoiceHeadObj.InvoiceNumber + "', ");
            sb.Append("str_to_date('" + invoiceHeadObj.InvoiceDate.ToString("MM/dd/yyyy") + "','%m/%d/%Y'), ");
            sb.Append("'" + invoiceHeadObj.UserId + "', ");
            sb.Append("'" + invoiceHeadObj.PickId + "', ");
            sb.Append("'" + invoiceHeadObj.OrderId + "', ");
            sb.Append("'" + invoiceHeadObj.Cases + "', ");
            sb.Append("'" + invoiceHeadObj.Pieces + "', ");
            sb.Append("'" + invoiceHeadObj.InvoiceAmount + "' ");
            sb.Append(")");

            db.Insert(sb.ToString());
        }

        public string GetAddQuery(InvoiceHeadModel invoiceHeadObj)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("insert into invoice_head values(null, ");
            sb.Append("'" + invoiceHeadObj.InvoiceNumber + "', ");
            sb.Append("str_to_date('" + invoiceHeadObj.InvoiceDate.ToString("MM/dd/yyyy") + "','%m/%d/%Y'), ");
            sb.Append("'" + invoiceHeadObj.UserId + "', ");
            sb.Append("'" + invoiceHeadObj.PickId + "', ");
            sb.Append("'" + invoiceHeadObj.OrderId + "', ");
            sb.Append("'" + invoiceHeadObj.Cases + "', ");
            sb.Append("'" + invoiceHeadObj.Pieces + "', ");
            sb.Append("'" + invoiceHeadObj.InvoiceAmount + "'");
            sb.Append(")");

            return sb.ToString();
        }

        public void DeleteItem(string qry)
        {
            if (string.IsNullOrEmpty(qry) == false)
                db.Delete(qry);
        }

        public void DeleteItem(string id, string type)
        {
            if (type == "id")
            {
                db.Delete("delete from invoice_head where id = '" + id + "'");
            }
            else if (type == "code")
            {
                db.Delete("delete from invoice_head where invoice_number = '" + id + "'");
            }
        }

        public string GetDeleteQuery(string id, string type)
        {
            if (type == "id")
            {
                return "delete from invoice_head where id = '" + id + "'";
            }
            else
            {
                return "delete from invoice_head where invoice_number = '" + id + "'";
            }
        }

        public object Fetch(string id, string type)
        {
            var invoiceHead = new InvoiceHeadModel();
            var result = new List<CloneableDictionary<string, string>>();

            if (type == "id")
            {
                result = db.SelectMultiple("select * from invoice_head where id = '" + id + "'");
            }
            else if (type == "code")
            {
                result = db.SelectMultiple("select * from invoice_head where invoice_number = '" + id + "'");
            }

            foreach (var row in result)
            {
                var clone = row.Clone();

                invoiceHead.Id = int.Parse(row["id"]);
                invoiceHead.InvoiceNumber = row["invoice_number"];
                invoiceHead.InvoiceDate = DateTime.Parse(row["invoice_date"]);
                invoiceHead.UserId = int.Parse(row["user_id"]);
                invoiceHead.PickId = int.Parse(row["pick_id"]);
                invoiceHead.OrderId = int.Parse(row["order_id"]);
                invoiceHead.Cases = int.Parse(row["cases"]);
                invoiceHead.Pieces = int.Parse(row["pieces"]);
                invoiceHead.InvoiceAmount = decimal.Parse(row["amount"]);

                break;
            }

            return invoiceHead;
        }

        public List<object> FetchAll()
        {
            var invoiceHeadList = new List<object>();
            var result = db.SelectMultiple("select * from invoice_head");

            foreach (var row in result)
            {
                var clone = row.Clone();
                var invoiceHead = new InvoiceHeadModel();

                invoiceHead.Id = int.Parse(row["id"]);
                invoiceHead.InvoiceNumber = row["invoice_number"];
                invoiceHead.InvoiceDate = DateTime.Parse(row["invoice_date"]);
                invoiceHead.UserId = int.Parse(row["user_id"]);
                invoiceHead.PickId = int.Parse(row["pick_id"]);
                invoiceHead.OrderId = int.Parse(row["order_id"]);
                invoiceHead.Cases = int.Parse(row["cases"]);
                invoiceHead.Pieces = int.Parse(row["pieces"]);
                invoiceHead.InvoiceAmount = decimal.Parse(row["amount"]);

                invoiceHeadList.Add(invoiceHead);
            }

            return invoiceHeadList;
        }

        public List<InvoiceHeadModel> FetchPerPickHead(string pickHeadId, string type)
        {
            var invoiceHeadList = new List<InvoiceHeadModel>();

            var results = new List<CloneableDictionary<string, string>>();

            if (type == "id")
            {
                results = db.SelectMultiple("select * from invoice_head where id = '" + id + "'");
            }
            else if (type == "code")
            {
                var pickHead = new PickListHeaderModel();
                pickHead = (PickListHeaderModel)pickHead.Fetch(pickHeadId, "code");

                results = db.SelectMultiple("select * from invoice_head where pick_id = '" + pickHead.Id + "'");
            }

            if (results.Count > 0)
                foreach (var row in results)
                {
                    var clone = row.Clone();
                    var invoiceHead = new InvoiceHeadModel();

                    invoiceHead.Id = int.Parse(row["id"]);
                    invoiceHead.InvoiceNumber = row["invoice_number"];
                    invoiceHead.InvoiceDate = DateTime.Parse(row["invoice_date"]);
                    invoiceHead.UserId = int.Parse(row["user_id"]);
                    invoiceHead.PickId = int.Parse(row["pick_id"]);
                    invoiceHead.OrderId = int.Parse(row["order_id"]);
                    invoiceHead.Cases = int.Parse(row["cases"]);
                    invoiceHead.Pieces = int.Parse(row["pieces"]);
                    invoiceHead.InvoiceAmount = decimal.Parse(row["amount"]);

                    invoiceHeadList.Add(invoiceHead);
                }

            return invoiceHeadList;
        }

        public InvoiceHeadModel FetchPerOrder(string orderId, string pickId)
        {
            var invoiceHead = new InvoiceHeadModel();

            var result = db.SelectMultiple("select * from invoice_head where pick_id = '" + pickId + "' and order_id = '" + orderId + "'");

            foreach (var row in result)
            {
                var clone = row.Clone();
                
                invoiceHead.Id = int.Parse(row["id"]);
                invoiceHead.InvoiceNumber = row["invoice_number"];
                invoiceHead.InvoiceDate = DateTime.Parse(row["invoice_date"]);
                invoiceHead.UserId = int.Parse(row["user_id"]);
                invoiceHead.PickId = int.Parse(row["pick_id"]);
                invoiceHead.OrderId = int.Parse(row["order_id"]);
                invoiceHead.Cases = int.Parse(row["cases"]);
                invoiceHead.Pieces = int.Parse(row["pieces"]);
                invoiceHead.InvoiceAmount = decimal.Parse(row["amount"]);
            }

            return invoiceHead;
        }
        public void UpdateItem(string qry)
        {
            throw new NotImplementedException();
        }
    }
}
