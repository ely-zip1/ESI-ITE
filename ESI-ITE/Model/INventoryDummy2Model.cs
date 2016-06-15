using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ESI_ITE.Data_Access;

namespace ESI_ITE.Model
{
    public class InventoryDummy2Model: IModelTemplate
    {
        #region Properties

        DataAccess db = new DataAccess();

        private int id;
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        private string order;
        public string Order
        {
            get { return order; }
            set { order = value; }
        }

        private string priceType;
        public string PriceType
        {
            get { return priceType; }
            set { priceType = value; }
        }

        private string location;
        public string Location
        {
            get { return location; }
            set { location = value; }
        }

        private string itemCode;
        public string ItemCode
        {
            get { return itemCode; }
            set { itemCode = value; }
        }

        private string itemDescription;
        public string ItemDescription
        {
            get { return itemDescription; }
            set { itemDescription = value; }
        }

        private int cases;
        public int Cases
        {
            get { return cases; }
            set { cases = value; }
        }

        private int pieces;
        public int Pieces
        {
            get { return pieces; }
            set { pieces = value; }
        }

        private decimal pricePerPiece;
        public decimal PricePerPiece
        {
            get { return pricePerPiece; }
            set { pricePerPiece = value; }
        }

        private decimal lineAmount;
        public decimal LineAmount
        {
            get { return lineAmount; }
            set { lineAmount = value; }
        }

        private string lotNumber;
        public string LotNumber
        {
            get { return lotNumber; }
            set { lotNumber = value; }
        }


        #endregion

        public void AddNew( object item )
        {
            throw new NotImplementedException();
        }

        public void DeleteItem( string qry )
        {
            throw new NotImplementedException();
        }

        public object Fetch( string id, string type )
        {
            var dummy = new InventoryDummy2Model();
            var record = new List<CloneableDictionary<string, string>>();

            record = db.SelectMultiple("select * from view_inventory_dummy_2 where id = '" + id + "' limit 1");

            foreach ( var row in record )
            {
                var clone = row.Clone();

                dummy.Id = int.Parse(row["id"]);
                dummy.Order = row["order"];
                dummy.PriceType = row["price_type"];
                dummy.Location = row["location"];
                dummy.ItemCode = row["item_code"];
                dummy.ItemDescription = row["description"];
                dummy.Cases = int.Parse(row["cases"]);
                dummy.Pieces = int.Parse(row["pieces"]);
                dummy.PricePerPiece = decimal.Parse(row["price_per_piece"]);
                dummy.LineAmount = decimal.Parse(row["line_amount"]);
                dummy.LotNumber = row["lot_number"];
            }

            return dummy;
        }

        public List<InventoryDummy2Model> FetchPerOrder( string orderId )
        {
            var items = new List<InventoryDummy2Model>();

            var record = db.SelectMultiple("select * from view_inventory_dummy_2 where order = '" + orderId + "'");

            foreach ( var row in record )
            {
                var dummy = new InventoryDummy2Model();
                var clone = row.Clone();

                dummy.Id = int.Parse(row["id"]);
                dummy.Order = row["order"];
                dummy.PriceType = row["price_type"];
                dummy.Location = row["location"];
                dummy.ItemCode = row["item_code"];
                dummy.ItemDescription = row["description"];
                dummy.Cases = int.Parse(row["cases"]);
                dummy.Pieces = int.Parse(row["pieces"]);
                dummy.PricePerPiece = decimal.Parse(row["price_per_piece"]);
                dummy.LineAmount = decimal.Parse(row["line_amount"]);
                dummy.LotNumber = row["lot_number"];

                items.Add(dummy);
            }

            return items;
        }

        public List<object> FetchAll( )
        {
            var items = new List<object>();

            var record = db.SelectMultiple("select * view_from inventory_dummy_2");

            foreach ( var row in record )
            {
                var dummy = new InventoryDummy2Model();
                var clone = row.Clone();

                dummy.Id = int.Parse(row["id"]);
                dummy.Order = row["order"];
                dummy.PriceType = row["price_type"];
                dummy.Location = row["location"];
                dummy.ItemCode = row["item_code"];
                dummy.ItemDescription = row["description"];
                dummy.Cases = int.Parse(row["cases"]);
                dummy.Pieces = int.Parse(row["pieces"]);
                dummy.PricePerPiece = decimal.Parse(row["price_per_piece"]);
                dummy.LineAmount = decimal.Parse(row["line_amount"]);
                dummy.LotNumber = row["lot_number"];

                items.Add(dummy);
            }

            return items;
        }

        public void UpdateItem( string qry )
        {
            throw new NotImplementedException();
        }
    }
}
