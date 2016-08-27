using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ESI_ITE.Data_Access;
using System.Windows;

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

        private string orderNumber;
        public string OrderNumber
        {
            get { return orderNumber; }
            set { orderNumber = value; }
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
            var inventoryDummyItem = (InventoryDummy2Model)item;

            var orderId = db.Select("select order_id from orders where order_number = '" + inventoryDummyItem.OrderNumber + "'");
            var itemId = db.Select("select item_master2_id from item_master2 where item_code = '" + inventoryDummyItem.ItemCode + "'");
            var priceTypeId = db.Select("select pricetype_id from so_pricetype where code = '" + inventoryDummyItem.PriceType + "'");
            var locationId = db.Select("select location_id from location where code = '" + inventoryDummyItem.Location + "'");

            var sb = new StringBuilder();

            sb.Append("insert into inventory_dummy_2 values (");
            sb.Append("null,");
            sb.Append("'" + orderId + "',");
            sb.Append("'" + priceTypeId + "',");
            sb.Append("'" + locationId + "',");
            sb.Append("'" + itemId + "',");
            sb.Append("'" + inventoryDummyItem.Cases + "',");
            sb.Append("'" + inventoryDummyItem.Pieces + "',");
            sb.Append("'" + inventoryDummyItem.PricePerPiece + "',");
            sb.Append("'" + inventoryDummyItem.LineAmount + "',");
            sb.Append("'" + inventoryDummyItem.LotNumber + "'");
            sb.Append(")");

            db.Insert(sb.ToString());
        }

        public void DeleteItem( string qry )
        {
            db.Delete(qry);
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
                dummy.OrderNumber = row["order_number"];
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

        public List<InventoryDummy2Model> FetchPerOrder( string orderNumber )
        {
            var items = new List<InventoryDummy2Model>();

            var record = db.SelectMultiple("select * from view_inventory_dummy_2 where order_number = '" + orderNumber + "'");

            foreach ( var row in record )
            {
                var dummy = new InventoryDummy2Model();
                var clone = row.Clone();

                dummy.Id = int.Parse(row["id"]);
                dummy.OrderNumber = row["order_number"];
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
                dummy.OrderNumber = row["order_number"];
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
            db.Update(qry);
        }

        public void UpdateItem( InventoryDummy2Model dummy )
        {
            StringBuilder sb = new StringBuilder();

            var orderId = db.Select("select order_id from orders where order_number = '" + dummy.OrderNumber + "'");
            var itemId = db.Select("select item_master2_id from item_master2 where item_code = '" + dummy.ItemCode + "'");
            var priceTypeId = db.Select("select pricetype_id from so_pricetype where code = '" + dummy.PriceType + "'");
            var locationId = db.Select("select location_id from location where code = '" + dummy.Location + "'");

            sb.Append("update inventory_dummy_2 set ");
            sb.Append("price_type = '" + priceTypeId + "', ");
            sb.Append("location = '" + locationId + "', ");
            sb.Append("cases = '" + dummy.Cases + "', ");
            sb.Append("pieces = '" + dummy.Pieces + "', ");
            sb.Append("price_per_piece = '" + dummy.PricePerPiece + "', ");
            sb.Append("line_amount = '" + dummy.LineAmount + "' ");
            sb.Append("where order_id = '" + orderId + "' ");
            sb.Append("and item = '" + itemId + "'");

            db.Update(sb.ToString());
        }
    }
}
