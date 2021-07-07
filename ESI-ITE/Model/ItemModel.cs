using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ESI_ITE.Data_Access;

namespace ESI_ITE.Model
{
    public class ItemModel: IModelTemplate
    {
        DataAccess db = new DataAccess();

        private int itemId;
        public int ItemId
        {
            get { return itemId; }
            set { itemId = value; }
        }

        private string code;
        public string Code
        {
            get { return code; }
            set { code = value; }
        }

        private string description;
        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        private int location;
        public int Location
        {
            get { return location; }
            set { location = value; }
        }

        private int warehouse;
        public int Warehouse
        {
            get { return warehouse; }
            set { warehouse = value; }
        }

        private int supplier;
        public int Supplier
        {
            get { return supplier; }
            set { supplier = value; }
        }

        private int packSize;
        public int PackSize
        {
            get { return packSize; }
            set { packSize = value; }
        }

        private string unitMeasure;
        public string UnitMeasure
        {
            get { return unitMeasure; }
            set { unitMeasure = value; }
        }

        private string smallestUnit;
        public string SmallestUnit
        {
            get { return smallestUnit; }
            set { smallestUnit = value; }
        }

        private string smallestUnitSymbol;
        public string SmallestUnitSymbol
        {
            get { return smallestUnitSymbol; }
            set { smallestUnitSymbol = value; }
        }

        private decimal unitWeight;
        public decimal UnitWeight
        {
            get { return unitWeight; }
            set { unitWeight = value; }
        }

        private decimal taxrate;
        public decimal Taxrate
        {
            get { return taxrate; }
            set { taxrate = value; }
        }

        private bool opgActive;
        public bool OPGActive
        {
            get { return opgActive; }
            set { opgActive = value; }
        }

        private bool activeItem;
        public bool ActiveItem
        {
            get { return activeItem; }
            set { activeItem = value; }
        }

        private bool showItem;
        public bool ShowItem
        {
            get { return showItem; }
            set { showItem = value; }
        }

        private string category;
        public string Category
        {
            get { return category; }
            set { category = value; }
        }

        private string brand;
        public string Brand
        {
            get { return brand; }
            set { brand = value; }
        }

        private string categoryCode;
        public string CategoryCode
        {
            get { return categoryCode; }
            set { categoryCode = value; }
        }

        private string brandCode;
        public string BrandCode
        {
            get { return brandCode; }
            set { brandCode = value; }
        }

        private bool isSelected;
        public bool IsSelected
        {
            get { return isSelected; }
            set { isSelected = value; }
        }

        private int weeksCoverd;
        public int WeeksCovered
        {
            get { return weeksCoverd; }
            set { weeksCoverd = value; }
        }

        private bool lotControlled;
        public bool LotControlled
        {
            get { return lotControlled; }
            set { lotControlled = value; }
        }

        private string locDescription;
        public string LocDescription
        {
            get { return locDescription; }
            set { locDescription = value; }
        }

        private decimal currentPrice;
        public decimal CurrentPrice
        {
            get { return currentPrice; }
            set { currentPrice = value; }
        }

        private bool isUpdated;
        public bool IsUpdated
        {
            get { return isUpdated; }
            set { isUpdated = value; }
        }

        private int packSizeBO;
        public int PackSizeBO
        {
            get { return packSizeBO; }
            set { packSizeBO = value; }
        }

        private string source;
        public string Source
        {
            get { return source; }
            set { source = value; }
        }


        public List<object> FetchAll( )
        {
            var list = new List<object>();
            
            var record = db.SelectMultiple("select * from item_master");

            foreach ( var row in record )
            {
                var clone = row.Clone();
                var item = new ItemModel();

                item.ItemId = int.Parse(row["item_master2_id"]);
                item.Code = row["item_code"];
                item.Description = row["description"];
                item.Location = int.Parse(row["location_id"]);
                item.Warehouse = int.Parse(row["warehouse_id"]);
                item.Supplier = int.Parse(row["supplier_id"]);
                item.PackSize = int.Parse(row["packsize"]);
                item.UnitMeasure = row["unit_measure"];
                item.SmallestUnit = row["smallest_unit"];
                item.SmallestUnitSymbol = row["smallest_unit_symbol"];
                item.UnitWeight = decimal.Parse(row["unit_weight"]);
                item.Taxrate = decimal.Parse(row["tax_rate"]);
                item.OPGActive = row["opg_active"] == "1" ? true : false;
                item.ActiveItem = row["active_item"] == "1" ? true : false;
                item.ShowItem = row["show_item"] == "1" ? true : false;
                item.Category = row["category"];
                item.Brand = row["brand"];
                item.CategoryCode = row["category_code"];
                item.BrandCode = row["brand_code"];
                item.IsSelected = row["selected"] == "1" ? true : false;
                item.WeeksCovered = int.Parse(row["weeks_cover"]);
                item.LotControlled = row["lot_controlled"] == "1" ? true : false;
                item.LocDescription = row["loc_description"];
                item.CurrentPrice = decimal.Parse(row["current_price"]);
                item.IsUpdated = row["updated"] == "1" ? true : false;
                item.PackSizeBO = int.Parse(row["pack_size_bo"]);
                item.Source = row["source"];

                list.Add(item);
            }

            return list;
        }

        public object Fetch( string id, string type )
        {
            var item = new ItemModel();
            var record = new List<CloneableDictionary<string, string>>();

            if ( type == "id" )
                record = db.SelectMultiple("select * from item_master where item_master2_id = '" + id + "'");
            else if ( type == "code" )
                record = db.SelectMultiple("select * from item_master where item_code = '" + id + "'");

            foreach ( var row in record )
            {
                var clone = row.Clone();

                item.ItemId = int.Parse(row["item_master2_id"]);
                item.Code = row["item_code"];
                item.Description = row["description"];
                item.Location = int.Parse(row["location_id"]);
                item.Warehouse = int.Parse(row["warehouse_id"]);
                item.Supplier = int.Parse(row["supplier_id"]);
                item.PackSize = int.Parse(row["packsize"]);
                item.UnitMeasure = row["unit_measure"];
                item.SmallestUnit = row["smallest_unit"];
                item.SmallestUnitSymbol = row["smallest_unit_symbol"];
                item.UnitWeight = decimal.Parse(row["unit_weight"]);
                item.Taxrate = decimal.Parse(row["tax_rate"]);
                item.OPGActive = row["opg_active"] == "1" ? true : false;
                item.ActiveItem = row["active_item"] == "1" ? true : false;
                item.ShowItem = row["show_item"] == "1" ? true : false;
                item.Category = row["category"];
                item.Brand = row["brand"];
                item.CategoryCode = row["category_code"];
                item.BrandCode = row["brand_code"];
                item.IsSelected = row["selected"] == "1" ? true : false;
                item.WeeksCovered = int.Parse(row["weeks_cover"]);
                item.LotControlled = row["lot_controlled"] == "1" ? true : false;
                item.LocDescription = row["loc_description"];
                item.CurrentPrice = decimal.Parse(row["current_price"]);
                item.IsUpdated = row["updated"] == "1" ? true : false;
                item.PackSizeBO = int.Parse(row["pack_size_bo"]);
                item.Source = row["source"];
            }

            return item;
        }

        public void AddNew( object item )
        {
            throw new NotImplementedException();
        }

        public void UpdateItem( string qry )
        {
            throw new NotImplementedException();
        }

        public void DeleteItem( string qry )
        {
            throw new NotImplementedException();
        }
    }
}
