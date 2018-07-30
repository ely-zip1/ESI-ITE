using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ESI_ITE.Data_Access;

namespace ESI_ITE.Model
{
    public class CreditNoteLineModel : IModelTemplate
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

        private int creditNoteHeadId;
        public int CreditNoteHeadId
        {
            get
            {
                return creditNoteHeadId;
            }
            set
            {
                creditNoteHeadId = value;
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
            var lineItem = item as CreditNoteLineModel;

            db.Insert("insert into cn_line values(null,");
            db.Insert("'" + lineItem.CreditNoteHeadId + "',");
            db.Insert("'" + lineItem.ItemId + "',");
            db.Insert("'" + lineItem.PriceType + "',");
            db.Insert("'" + lineItem.Cases + "',");
            db.Insert("'" + lineItem.Pieces + "',");
            db.Insert("'" + lineItem.LineAmount + "',");
            db.Insert("'" + lineItem.LotNumber + "',");
            db.Insert("'" + lineItem.Location + "',");
            db.Insert("'" + lineItem.PricePerPiece + "'");
            db.Insert(")");
        }

        public void DeleteItem(string qry)
        {
            throw new NotImplementedException();
        }

        public object Fetch(string id, string type)
        {
            var lineItem = new CreditNoteLineModel();
            var result = db.SelectMultiple("select * from cn_line where id = '" + id + "'");

            foreach (var row in result)
            {
                var clone = row.Clone();

                lineItem.Id = int.Parse(row["id"]);
                lineItem.CreditNoteHeadId = int.Parse(row["cn_head_id"]);
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

            var results = db.SelectMultiple("select * from cn_line");

            foreach (var row in results)
            {
                var clone = row.Clone();
                var lineItem = new CreditNoteLineModel();

                lineItem.Id = int.Parse(row["id"]);
                lineItem.CreditNoteHeadId = int.Parse(row["cn_head_id"]);
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

        public List<CreditNoteLineModel> FetchPerCreditNoteHead(string creditNoteHeadId)
        {
            var lineItemList = new List<CreditNoteLineModel>();

            var results = db.SelectMultiple("select * from cn_line where cn_head_id = '" + creditNoteHeadId + "'");

            foreach (var row in results)
            {
                var clone = row.Clone();
                var lineItem = new CreditNoteLineModel();

                lineItem.Id = int.Parse(row["id"]);
                lineItem.CreditNoteHeadId = int.Parse(row["cn_head_id"]);
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
    }
}
