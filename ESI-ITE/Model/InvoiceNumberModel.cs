using ESI_ITE.Data_Access;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESI_ITE.Model
{
    class InvoiceNumberModel : IModelTemplate
    {
        #region Properties

        DataAccess db = new DataAccess();

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

        #endregion



        public void AddNew(object item)
        {
            var invoiceNumberObj = FetchLatest();

            var latestInvoiceNumber = int.Parse(invoiceNumberObj.InvoiceNumber);

            StringBuilder sb = new StringBuilder();

            sb.Append("insert into invoice_number values(");
            sb.Append("null, ");
            sb.Append("'" + latestInvoiceNumber++ + "' ");
            sb.Append(")");

            db.Insert(sb.ToString());

            db.Delete("delete from invoice_number where id = (select min(id) from (select * from invoice_number) as temp limit 1)");
        }

        public void DeleteItem(string qry)
        {
            throw new NotImplementedException();
        }

        public object Fetch(string id, string type)
        {
            throw new NotImplementedException();
        }

        public InvoiceNumberModel FetchLatest()
        {
            var latestInvoiceNumber = new InvoiceNumberModel();

            var results = db.SelectMultiple("select * from invoice_number order by id desc limit 1");

            foreach (var row in results)
            {
                var clone = row.Clone();

                latestInvoiceNumber.Id = int.Parse(row["id"]);
                latestInvoiceNumber.InvoiceNumber = row["invoice_number"];
                break;
            }

            return latestInvoiceNumber;
        }

        public List<object> FetchAll()
        {
            throw new NotImplementedException();
        }

        public void UpdateItem(string qry)
        {
            throw new NotImplementedException();
        }
    }
}
