using ESI_ITE.Data_Access;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESI_ITE.Model
{
    public class PricePurchaseModel : IModelTemplate
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

        private decimal purchasePrice;
        public decimal PurchasePrice
        {
            get { return purchasePrice; }
            set { purchasePrice = value; }
        }

        private bool isActive;
        public bool IsActive
        {
            get { return isActive; }
            set { isActive = value; }
        }

        private DateTime priceDate;
        public DateTime PriceDate
        {
            get { return priceDate; }
            set { priceDate = value; }
        }

        #endregion


        public void AddNew(object item)
        {
            throw new NotImplementedException();
        }

        public void DeleteItem(string qry)
        {
            throw new NotImplementedException();
        }

        public object Fetch(string id, string type)
        {
            var results = new List<CloneableDictionary<string, string>>();

            if (type == "id")
            {
                results = db.SelectMultiple("select * from price_purchase where price_id = '" + id + "'");
            }

            var price = new PricePurchaseModel();

            foreach (var row in results)
            {
                var clone = row.Clone();

                price.Id = int.Parse(row["price_id"]);
                price.ItemId = int.Parse(row["item_id"]);
                price.PurchasePrice = decimal.Parse(row["purchase_price"]);
                price.IsActive = (row["isactive"] == "1") ? true : false;
                price.PriceDate = DateTime.Parse(row["date"]);
            }

            return price;
        }

        public List<object> FetchAll()
        {
            var results = db.SelectMultiple("select * from price_purchase");

            var priceList = new List<object>();

            foreach (var row in results)
            {
                var clone = row.Clone();
                var price = new PricePurchaseModel();

                price.Id = int.Parse(row["price_id"]);
                price.ItemId = int.Parse(row["item_id"]);
                price.PurchasePrice = decimal.Parse(row["purchase_price"]);
                price.IsActive = (row["isactive"] == "1") ? true : false;
                price.PriceDate = DateTime.Parse(row["date"]);

                priceList.Add(price);
            }

            return priceList;
        }

        public void UpdateItem(string qry)
        {
            throw new NotImplementedException();
        }

        public PricePurchaseModel FetchCurrentPrice(string item, string type)
        {
            var price = new PricePurchaseModel();

            var itemObj = new ItemModel();
            var result = new List<CloneableDictionary<string, string>>();

            if (type == "code")
            {
                itemObj = (ItemModel)itemObj.Fetch(item, "code");

                result = db.SelectMultiple("select * from price_purchase where item_id = '" + itemObj.ItemId + "' order by date desc limit 1");
            }
            else if (type == "id")
            {
                result = db.SelectMultiple("select * from price_purchase where item_id = '" + item + "' order by date desc limit 1");
            }

            if (result.Count == 0)
            {
                return null;
            }
            else
            {
                return setPrice(result);
            }
        }

        public PricePurchaseModel FetchPrice6MonthsAgo(string item, string type)
        {
            var price = new PricePurchaseModel();
            var priceList = new List<PricePurchaseModel>();

            var itemObj = new ItemModel();
            var result = new List<CloneableDictionary<string, string>>();

            if (type == "code")
            {
                itemObj = (ItemModel)itemObj.Fetch(item, "code");

                result = db.SelectMultiple("select * from price_purchase where item_id = '" + itemObj.ItemId + "' order by date desc");
            }
            else if (type == "id")
            {
                result = db.SelectMultiple("select * from price_purchase where item_id = '" + item + "' order by date desc");
            }

            if (result.Count == 0)
            {
                return null;
            }
            else if (result.Count == 1)
            {
                return setPrice(result);
            }
            else if (result.Count > 1)
            {
                foreach (var row in result)
                {
                    var clone = row.Clone();
                    var tempPrice = new PricePurchaseModel();

                    tempPrice.Id = int.Parse(row["price_id"]);
                    tempPrice.ItemId = int.Parse(row["item_id"]);
                    tempPrice.PurchasePrice = decimal.Parse("purchase_price");
                    tempPrice.IsActive = (row["isActive"] == "1") ? true : false;
                    tempPrice.PriceDate = DateTime.Parse(row["date"]);

                    priceList.Add(tempPrice);
                }

                var priceCount = priceList.Count;
                var currentPrice = priceList[1];
                var date3MonthsAgo = currentPrice.PriceDate.AddMonths(-3);
                var date6MonthsAgo = date3MonthsAgo.AddMonths(-3);

                foreach (var priceObj in priceList)
                {
                    if (priceObj.PriceDate >= date6MonthsAgo && priceObj.PriceDate < date3MonthsAgo)
                    {
                        price = priceObj;
                    }
                    else
                    {
                        price = null;
                    }
                }

                if (price == null)
                {
                    price = currentPrice;
                }
            }

            return price;
        }


        public PricePurchaseModel FetchPrice3MonthsAgo(string item, string type)
        {
            var price = new PricePurchaseModel();
            var priceList = new List<PricePurchaseModel>();

            var itemObj = new ItemModel();
            var result = new List<CloneableDictionary<string, string>>();

            if (type == "code")
            {
                itemObj = (ItemModel)itemObj.Fetch(item, "code");

                result = db.SelectMultiple("select * from price_purchase where item_id = '" + itemObj.ItemId + "' order by date desc");
            }
            else if (type == "id")
            {
                result = db.SelectMultiple("select * from price_purchase where item_id = '" + item + "' order by date desc");
            }

            if (result.Count == 0)
            {
                return null;
            }
            else if (result.Count == 1)
            {
                return setPrice(result);
            }
            else if (result.Count > 1)
            {
                foreach (var row in result)
                {
                    var clone = row.Clone();
                    var tempPrice = new PricePurchaseModel();

                    tempPrice.Id = int.Parse(row["price_id"]);
                    tempPrice.ItemId = int.Parse(row["item_id"]);
                    tempPrice.PurchasePrice = decimal.Parse("purchase_price");
                    tempPrice.IsActive = (row["isActive"] == "1") ? true : false;
                    tempPrice.PriceDate = DateTime.Parse(row["date"]);

                    priceList.Add(tempPrice);
                }

                var priceCount = priceList.Count;
                var currentPrice = priceList[1];
                var currentPriceDate = currentPrice.PriceDate;
                var date3MonthsAgo = currentPriceDate.AddMonths(-3);

                foreach (var priceObj in priceList)
                {
                    if (priceObj.PriceDate >= date3MonthsAgo && priceObj.PriceDate < currentPriceDate)
                    {
                        price = priceObj;
                    }
                    else
                    {
                        price = null;
                    }
                }

                if (price == null)
                {
                    price = currentPrice;
                }

            }
            return price;
        }

        private PricePurchaseModel setPrice(List<CloneableDictionary<string, string>> result)
        {
            var price = new PricePurchaseModel();

            foreach (var row in result)
            {
                var clone = row.Clone();

                price.Id = int.Parse(row["price_id"]);
                price.ItemId = int.Parse(row["item_id"]);
                price.PurchasePrice = decimal.Parse("purchase_price");
                price.IsActive = (row["isActive"] == "1") ? true : false;
                price.PriceDate = DateTime.Parse(row["date"]);

                break;
            }

            return price;
        }
    }
}
