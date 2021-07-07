using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ESI_ITE.Data_Access;

namespace ESI_ITE.Model
{
    public class InvoiceLineModel : IModelTemplate
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

        private int invoiceId;
        public int InvoiceId
        {
            get
            {
                return invoiceId;
            }
            set
            {
                invoiceId = value;
            }
        }

        private int itemId;
        public int ItemId
        {
            get
            {
                return itemId;
            }
            set
            {
                itemId = value;
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

        private decimal casePrice;
        public decimal CasePrice
        {
            get
            {
                return casePrice;
            }
            set
            {
                casePrice = value;
            }
        }

        private decimal piecePrice;
        public decimal PiecePrice
        {
            get
            {
                return piecePrice;
            }
            set
            {
                piecePrice = value;
            }
        }

        private decimal lineAmount;
        public decimal LineAmount
        {
            get
            {
                return lineAmount;
            }
            set
            {
                lineAmount = value;
            }
        }

        #endregion

        public void AddNew(object item)
        {
            var invoiceLine = (InvoiceLineModel)item;

            var sb = new StringBuilder();

            sb.Append("insert into invoice_line values(");
            sb.Append("null,");
            sb.Append("'" + invoiceLine.InvoiceId + "',");
            sb.Append("'" + invoiceLine.ItemId + "',");
            sb.Append("'" + invoiceLine.Cases + "',");
            sb.Append("'" + invoiceLine.Pieces + "',");
            sb.Append("'" + invoiceLine.CasePrice + "',");
            sb.Append("'" + invoiceLine.PiecePrice + "',");
            sb.Append("'" + invoiceLine.LineAmount + "'");
            sb.Append(")");

            db.Insert(sb.ToString());
        }

        public string GetAddQuery(object item)
        {
            var invoiceLine = (InvoiceLineModel)item;

            var sb = new StringBuilder();

            sb.Append("insert into invoice_line values(");
            sb.Append("null,");
            sb.Append("'" + invoiceLine.InvoiceId + "',");
            sb.Append("'" + invoiceLine.ItemId + "',");
            sb.Append("'" + invoiceLine.Cases + "',");
            sb.Append("'" + invoiceLine.Pieces + "',");
            sb.Append("'" + invoiceLine.CasePrice + "',");
            sb.Append("'" + invoiceLine.PiecePrice + "',");
            sb.Append("'" + invoiceLine.LineAmount + "'");
            sb.Append(")");

            return sb.ToString();
        }

        public string GetAddQuery(string invoiceNumber, object item)
        {
            var invoiceLine = (InvoiceLineModel)item;
            var sb = new StringBuilder();

            sb.Append("insert into invoice_line values(");
            sb.Append("null,");
            sb.Append("(select id from invoice_head where invoice_number = '" + invoiceNumber + "'),");
            sb.Append("'" + invoiceLine.ItemId + "',");
            sb.Append("'" + invoiceLine.Cases + "',");
            sb.Append("'" + invoiceLine.Pieces + "',");
            sb.Append("'" + invoiceLine.CasePrice + "',");
            sb.Append("'" + invoiceLine.PiecePrice + "',");
            sb.Append("'" + invoiceLine.LineAmount + "'");
            sb.Append(")");

            return sb.ToString();
        }

        public void DeleteItem(string qry)
        {
            throw new NotImplementedException();
        }

        public object Fetch(string id, string type)
        {
            var invoiceLine = new InvoiceLineModel();

            var result = new List<CloneableDictionary<string, string>>();

            if (type == "id")
            {
                result = db.SelectMultiple("select * from invoice_line where id = '" + id + "'");
            }

            foreach (var row in result)
            {
                var clone = row.Clone();

                invoiceLine.Id = int.Parse(row["id"]);
                invoiceLine.InvoiceId = int.Parse(row["invoice_id"]);
                invoiceLine.ItemId = int.Parse(row["item_id"]);
                invoiceLine.Cases = int.Parse(row["cases"]);
                invoiceLine.Pieces = int.Parse(row["pieces"]);
                invoiceLine.CasePrice = decimal.Parse(row["case_price"]);
                invoiceLine.PiecePrice = decimal.Parse(row["piece_price"]);
                invoiceLine.LineAmount = decimal.Parse(row["line_amount"]);

                break;
            }

            return invoiceLine;
        }

        public List<InvoiceLineModel> FetchPerInvoice(InvoiceHeadModel invoice)
        {
            var results = new List<CloneableDictionary<string, string>>();

            results = db.SelectMultiple("select * from invoice_line where invoice_id = '" + invoice.Id + "'");

            var invoiceLineList = new List<InvoiceLineModel>();

            foreach (var row in results)
            {
                var clone = row.Clone();
                var invoiceLine = new InvoiceLineModel();

                invoiceLine.Id = int.Parse(row["id"]);
                invoiceLine.InvoiceId = int.Parse(row["invoice_id"]);
                invoiceLine.ItemId = int.Parse(row["item_id"]);
                invoiceLine.Cases = int.Parse(row["cases"]);
                invoiceLine.Pieces = int.Parse(row["pieces"]);
                invoiceLine.CasePrice = decimal.Parse(row["case_price"]);
                invoiceLine.PiecePrice = decimal.Parse(row["piece_price"]);
                invoiceLine.LineAmount = decimal.Parse(row["line_amount"]);

                invoiceLineList.Add(invoiceLine);
            }
            return invoiceLineList;
        }

        public List<object> FetchAll()
        {
            var invoiceLineList = new List<object>();

            var results = db.SelectMultiple("select * from invoice_line");
            foreach (var row in results)
            {
                var clone = row.Clone();

                var invoiceLine = new InvoiceLineModel();

                invoiceLine.Id = int.Parse(row["id"]);
                invoiceLine.InvoiceId = int.Parse(row["invoice_id"]);
                invoiceLine.ItemId = int.Parse(row["item_id"]);
                invoiceLine.Cases = int.Parse(row["cases"]);
                invoiceLine.Pieces = int.Parse(row["pieces"]);
                invoiceLine.CasePrice = decimal.Parse(row["case_price"]);
                invoiceLine.PiecePrice = decimal.Parse(row["piece_price"]);
                invoiceLine.LineAmount = decimal.Parse(row["line_amount"]);

                invoiceLineList.Add(invoiceLine);
            }

            return invoiceLineList;
        }

        public void UpdateItem(string qry)
        {
            throw new NotImplementedException();
        }
    }
}
