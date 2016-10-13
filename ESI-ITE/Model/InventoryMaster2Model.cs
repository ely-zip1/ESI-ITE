using ESI_ITE.Data_Access;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESI_ITE.Model
{
    public class InventoryMaster2Model: IModelTemplate
    {

        #region Properties

        DataAccess db = new DataAccess();

        private int id;
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        private int itemId;
        public int ItemId
        {
            get { return itemId; }
            set { itemId = value; }
        }

        private int warehouseId;
        public int WarehouseId
        {
            get { return warehouseId; }
            set { warehouseId = value; }
        }

        private int locationId;
        public int LocationId
        {
            get { return locationId; }
            set { locationId = value; }
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

        private DateTime expirationDate;
        public DateTime ExpirationDate
        {
            get { return expirationDate; }
            set { expirationDate = value; }
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
            var inventory = new InventoryMaster2Model();
            var result = new List<CloneableDictionary<string, string>>();

            switch ( type )
            {
                case "id":
                    result = db.SelectMultiple("select * from inventory_master2 where inventory2_id = '" + id + "'");
                    break;
            }

            foreach ( var row in result )
            {
                var clone = row.Clone();

                inventory.Id = int.Parse(row["inventory2_id"]);
                inventory.ItemId = int.Parse(row["item_id_link"]);
                inventory.WarehouseId = int.Parse(row["warehouse_id"]);
                inventory.LocationId = int.Parse(row["location_link"]);
                inventory.Cases = int.Parse(row["i_cases"]);
                inventory.Pieces = int.Parse(row["i_pieces"]);
                inventory.ExpirationDate = DateTime.ParseExact(row["expiration_date"], "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
                inventory.LotNumber = row["lot_number"];
            }

            return inventory;
        }

        public List<InventoryMaster2Model> FetchPerItem( int itemId )
        {
            var inventoryList = new List<InventoryMaster2Model>();
            var results = db.SelectMultiple("select * from inventory_master2 where item_id_link = '" + itemId + "'");

            foreach ( var row in results )
            {
                var clone = row.Clone();
                var inventory = new InventoryMaster2Model();

                inventory.Id = int.Parse(row["inventory2_id"]);
                inventory.ItemId = int.Parse(row["item_id_link"]);
                inventory.WarehouseId = int.Parse(row["warehouse_id"]);
                inventory.LocationId = int.Parse(row["location_link"]);
                inventory.Cases = int.Parse(row["i_cases"]);
                inventory.Pieces = int.Parse(row["i_pieces"]);
                inventory.ExpirationDate = DateTime.ParseExact(row["expiration_date"], "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
                inventory.LotNumber = row["lot_number"];

                inventoryList.Add(inventory);
            }

            return inventoryList;
        }

        public InventoryMaster2Model FetchItem( int itemId, DateTime expiration )
        {
            var inventoryItem = new InventoryMaster2Model();

            var result = db.SelectMultiple("select * from inventory_master2 where item_id_link = '" + ItemId + "' and expiration_date = str_to_date('" + expiration.ToString("MM/dd/yyyy") + "','%m/%d/%Y')");

            foreach ( var row in result )
            {
                var clone = row.Clone();

                inventoryItem.Id = int.Parse(row["inventory2_id"]);
                inventoryItem.ItemId = int.Parse(row["item_id_link"]);
                inventoryItem.WarehouseId = int.Parse(row["warehouse_id"]);
                inventoryItem.LocationId = int.Parse(row["location_link"]);
                inventoryItem.Cases = int.Parse(row["i_cases"]);
                inventoryItem.Pieces = int.Parse(row["i_pieces"]);
                inventoryItem.ExpirationDate = DateTime.ParseExact(row["expiration_date"], "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
                inventoryItem.LotNumber = row["lot_number"];
            }

            return inventoryItem;
        }

        public List<InventoryMaster2Model> FetchPerItem( string itemCode )
        {
            var inventoryList = new List<InventoryMaster2Model>();
            var results = db.SelectMultiple("select * from inventory_master2 where item_id_link = '" + itemId + "'");

            foreach ( var row in results )
            {
                var clone = row.Clone();
                var inventory = new InventoryMaster2Model();

                inventory.Id = int.Parse(row["inventory2_id"]);
                inventory.ItemId = int.Parse(row["item_id_link"]);
                inventory.WarehouseId = int.Parse(row["warehouse_id"]);
                inventory.LocationId = int.Parse(row["location_link"]);
                inventory.Cases = int.Parse(row["i_cases"]);
                inventory.Pieces = int.Parse(row["i_pieces"]);
                inventory.ExpirationDate = DateTime.ParseExact(row["expiration_date"], "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
                inventory.LotNumber = row["lot_number"];

                inventoryList.Add(inventory);
            }

            return inventoryList;
        }

        public List<object> FetchAll( )
        {
            throw new NotImplementedException();
        }

        public void UpdateItem( string qry )
        {
            db.Update(qry);
        }

        public void UpdateItem( InventoryMaster2Model item )
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("update inventory_master2 set ");
            sb.Append("item_id_link = '" + item.ItemId + "', ");
            sb.Append("warehouse_id = '" + item.WarehouseId + "', ");
            sb.Append("location_link = '" + item.LocationId + "', ");
            sb.Append("i_cases = '" + item.Cases + "', ");
            sb.Append("i_pieces = '" + item.Pieces + "', ");
            sb.Append("expiration_date = str_to_date('" + item.ExpirationDate.ToString("MM/dd/yyyy") + "','%m/%d%/Y'), ");
            sb.Append("lot_number = '" + item.LotNumber + "' ");
            sb.Append("where inventory2_id = '" + item.Id + "'");

            db.Update(sb.ToString());
        }

        public string GetUpdateQuery( InventoryMaster2Model item )
        {

            StringBuilder sb = new StringBuilder();

            sb.Append("update inventory_master2 set ");
            sb.Append("item_id_link = '" + item.ItemId + "', ");
            sb.Append("warehouse_id = '" + item.WarehouseId + "', ");
            sb.Append("location_link = '" + item.LocationId + "', ");
            sb.Append("i_cases = '" + item.Cases + "', ");
            sb.Append("i_pieces = '" + item.Pieces + "', ");
            sb.Append("expiration_date = str_to_date('" + item.ExpirationDate.ToString("MM/dd/yyyy") + "','%m/%d%/Y'), ");
            sb.Append("lot_number = '" + item.LotNumber + "' ");
            sb.Append("where inventory2_id = '" + item.Id + "'");

            return sb.ToString();
        }
    }
}
