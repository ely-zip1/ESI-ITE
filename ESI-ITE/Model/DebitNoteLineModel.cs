using ESI_ITE.Data_Access;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESI_ITE.Model
{
    public class DebitNoteLineModel : IModelTemplate
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

        private int debitNoteHeadId;
        public int DebitNoteHeadId
        {
            get
            {
                return debitNoteHeadId;
            }
            set
            {
                debitNoteHeadId = value;
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

        private string priceType;
        public string PriceType
        {
            get
            {
                return priceType;
            }
            set
            {
                priceType = value;
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

        private string lotNumber;
        public string LotNumber
        {
            get
            {
                return lotNumber;
            }
            set
            {
                lotNumber = value;
            }
        }

        private string location;
        public string Location
        {
            get
            {
                return location;
            }
            set
            {
                location = value;
            }
        }

        private decimal pricePerPiece;
        public decimal PricePerPiece
        {
            get
            {
                return pricePerPiece;
            }
            set
            {
                pricePerPiece = value;
            }
        }


        #endregion

        public void AddNew(object item)
        {
            var lineItem = item as DebitNoteLineModel;

            var sb = new StringBuilder();
            sb.Append("insert into dn_line values(null,");
            sb.Append("'" + lineItem.DebitNoteHeadId + "',");
            sb.Append("'" + lineItem.ItemId + "',");
            sb.Append("'" + lineItem.PriceType + "',");
            sb.Append("'" + lineItem.Cases + "',");
            sb.Append("'" + lineItem.Pieces + "',");
            sb.Append("'" + lineItem.LineAmount + "',");
            sb.Append("'" + lineItem.LotNumber + "',");
            sb.Append("'" + lineItem.Location + "',");
            sb.Append("'" + lineItem.PricePerPiece + "'");
            sb.Append(")");

            db.Insert(sb.ToString());
        }

        public void DeleteItem(string qry)
        {
            throw new NotImplementedException();
        }

        public void DeleteItem(string DNHeadId, string itemId)
        {
            db.Delete("Delete from dn_line where dn_head_id = '" + DNHeadId + "' and item_id = '" + itemId + "'");
        }

        public object Fetch(string id, string type)
        {
            var lineItem = new DebitNoteLineModel();
            var result = db.SelectMultiple("select * from dn_line where id = '" + id + "'");

            foreach (var row in result)
            {
                var clone = row.Clone();

                lineItem.Id = int.Parse(row["id"]);
                lineItem.DebitNoteHeadId = int.Parse(row["dn_head_id"]);
                lineItem.ItemId = int.Parse(row["item_id"]);
                lineItem.PriceType = row["price_type"];
                lineItem.Cases = int.Parse(row["cases"]);
                lineItem.Pieces = int.Parse(row["pieces"]);
                lineItem.LineAmount = decimal.Parse(row["line_amount"]);
                lineItem.LotNumber = row["lot_number"];
                lineItem.Location = row["location"];
                lineItem.PricePerPiece = decimal.Parse(row["price_per_piece"]);

            }

            return lineItem;
        }

        public List<object> FetchAll()
        {
            var lineItemList = new List<object>();

            var results = db.SelectMultiple("select * from dn_line");

            foreach (var row in results)
            {
                var clone = row.Clone();
                var lineItem = new DebitNoteLineModel();

                lineItem.Id = int.Parse(row["id"]);
                lineItem.DebitNoteHeadId = int.Parse(row["dn_head_id"]);
                lineItem.ItemId = int.Parse(row["item_id"]);
                lineItem.PriceType = row["price_type"];
                lineItem.Cases = int.Parse(row["cases"]);
                lineItem.Pieces = int.Parse(row["pieces"]);
                lineItem.LineAmount = decimal.Parse(row["line_amount"]);
                lineItem.LotNumber = row["lot_number"];
                lineItem.Location = row["location"];
                lineItem.PricePerPiece = decimal.Parse(row["price_per_piece"]);

                lineItemList.Add(lineItem);
            }

            return lineItemList;
        }

        public bool Verify(string dnHeadId, string itemId)
        {
            var results = db.SelectMultiple("select * from dn_line where dn_head_id = '" + dnHeadId + "' and item_id = '" + itemId + "'");

            if (results.Count > 0)
                return true;
            else
                return false;
        }

        public List<DebitNoteLineModel> FetchPerDebitNoteHead(string debitNoteHeadId)
        {
            var lineItemList = new List<DebitNoteLineModel>();

            var results = db.SelectMultiple("select * from dn_line where cn_head_id = '" + debitNoteHeadId + "'");

            foreach (var row in results)
            {
                var clone = row.Clone();
                var lineItem = new DebitNoteLineModel();

                lineItem.Id = int.Parse(row["id"]);
                lineItem.DebitNoteHeadId = int.Parse(row["dn_head_id"]);
                lineItem.ItemId = int.Parse(row["item_id"]);
                lineItem.PriceType = row["price_type"];
                lineItem.Cases = int.Parse(row["cases"]);
                lineItem.Pieces = int.Parse(row["pieces"]);
                lineItem.LineAmount = decimal.Parse(row["line_amount"]);
                lineItem.LotNumber = row["lot_number"];
                lineItem.Location = row["location"];
                lineItem.PricePerPiece = decimal.Parse(row["price_per_piece"]);

                lineItemList.Add(lineItem);
            }

            return lineItemList;
        }

        public void UpdateItem(string qry)
        {
            throw new NotImplementedException();
        }

        public void UpdateItem(DebitNoteLineModel lineItem)
        {
            db.Update("update dn_line set cases = '" + lineItem.Cases + "', pieces = '" + lineItem.Pieces + "', line_amount = '" + lineItem.LineAmount + "' " +
                "where dn_head_id = '" + lineItem.DebitNoteHeadId + "' and item_id = '" + lineItem.ItemId + "'");
        }

    }
}
