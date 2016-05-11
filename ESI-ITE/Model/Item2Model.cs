using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESI_ITE.Model
{
    class Item2Model
    {

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

    }
}
