using ESI_ITE.Data_Access;
using System;
using System.Collections.Generic;
using System.Text;

namespace ESI_ITE.Model
{
    public class InvoicesModel : IModelTemplate
    {
        DataAccess db = new DataAccess();

        #region Properties

        private int id;
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        private string invoiceNumber;
        public string InvoiceNumber
        {
            get { return invoiceNumber; }
            set { invoiceNumber = value; }
        }

        private int pickheadId;
        public int PickheadId
        {
            get { return pickheadId; }
            set { pickheadId = value; }
        }

        private int orderId;
        public int OrderId
        {
            get { return orderId; }
            set { orderId = value; }
        }

        private int userId;
        public int UserId
        {
            get { return userId; }
            set { userId = value; }
        }

        #endregion


        public List<object> FetchAll()
        {
            var invoiceList = new List<object>();

            var results = db.SelectMultiple("select * from invoices");

            foreach (var row in results)
            {
                var clone = row.Clone();
                var invoice = new InvoicesModel();

                invoice.Id = int.Parse(row["id"]);
                invoice.InvoiceNumber = row["invoice_number"];
                invoice.Id = int.Parse(row["pick_id"]);
                invoice.Id = int.Parse(row["order_id"]);
                invoice.Id = int.Parse(row["user_id"]);

                invoiceList.Add(invoice);
            }

            return invoiceList;
        }

        public object Fetch(string id, string type)
        {
            var invoice = new InvoicesModel();
            var results = new List<CloneableDictionary<string, string>>();

            if (type == "id")
                results = db.SelectMultiple("select * from invoices where id = '" + id + "'");

            foreach (var row in results)
            {
                var clone = row.Clone();

                invoice.Id = int.Parse(row["id"]);
                invoice.InvoiceNumber = row["invoice_number"];
                invoice.Id = int.Parse(row["pick_id"]);
                invoice.Id = int.Parse(row["order_id"]);
                invoice.Id = int.Parse(row["user_id"]);
            }

            return invoice;
        }

        public void AddNew(object item)
        {
            var invoice = item as InvoicesModel;

            StringBuilder sb = new StringBuilder();
            sb.Append("insert into invoices values(");
            sb.Append("null, ");
            sb.Append("'" + invoice.InvoiceNumber + "', ");
            sb.Append("'" + invoice.PickheadId + "', ");
            sb.Append("'" + invoice.OrderId + "', ");
            sb.Append("'" + invoice.UserId + "' ");
            sb.Append(")");

            db.Insert(sb.ToString());
        }

        public void UpdateItem(string qry)
        {
            throw new NotImplementedException();
        }

        public void DeleteItem(string qry)
        {
            throw new NotImplementedException();
        }


    }
}
