using ESI_ITE.Data_Access;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESI_ITE.Model
{
    public class PriceSellingModel : IModelTemplate
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

        private int priceTypeId;
        public int PriceTypeId
        {
            get
            {
                return priceTypeId;
            }
            set
            {
                priceTypeId = value;
            }
        }

        private decimal sellingPrice;
        public decimal SellingPrice
        {
            get
            {
                return sellingPrice;
            }
            set
            {
                sellingPrice = value;
            }
        }

        private DateTime effectiveFrom;
        public DateTime EffectiveFrom
        {
            get
            {
                return effectiveFrom;
            }
            set
            {
                effectiveFrom = value;
            }
        }

        private DateTime effectiveTo;
        public DateTime EffectiveTo
        {
            get
            {
                return effectiveTo;
            }
            set
            {
                effectiveTo = value;
            }
        }

        private bool isCurrent;
        public bool IsCurrent
        {
            get
            {
                return isCurrent;
            }
            set
            {
                isCurrent = value;
            }
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
            var price = new PriceSellingModel();
            var results = new List<CloneableDictionary<string, string>>();

            if (type == "id")
                results = db.SelectMultiple("select * from price_selling where pricetype_id = '" + id + "'");
            else
                results = db.SelectMultiple("select * from price_selling where code = '" + id + "'");

            foreach (var row in results)
            {
                var clone = row.Clone();

                price.Id = int.Parse(row["price_id"]);
                price.ItemId = int.Parse(row["item_id"]);
                price.PriceTypeId = int.Parse(row["pricetype_id"]);
                price.SellingPrice = decimal.Parse(row["selling_price"]);
                price.EffectiveFrom = DateTime.Parse(row["effective_from"]);
                price.EffectiveTo = DateTime.Parse(row["effective_to"]);
                price.IsCurrent = (row["current"] == "1") ? true : false;
                break;
            }

            return price;
        }

        public List<object> FetchAll()
        {
            var priceCodeList = new List<object>();

            var results = db.SelectMultiple("select * from price_selling");
            foreach (var row in results)
            {
                var clone = row.Clone();
                var price = new PriceSellingModel();

                price.Id = int.Parse(row["price_id"]);
                price.ItemId = int.Parse(row["item_id"]);
                price.PriceTypeId = int.Parse(row["pricetype_id"]);
                price.SellingPrice = decimal.Parse(row["selling_price"]);
                price.EffectiveFrom = DateTime.Parse(row["effective_from"]);
                price.EffectiveTo = DateTime.Parse(row["effective_to"]);
                price.IsCurrent = (row["current"] == "1") ? true : false;

                priceCodeList.Add(price);
            }

            return priceCodeList;
        }

        public void UpdateItem(string qry)
        {
            throw new NotImplementedException();
        }

        public PriceSellingModel FetchPrice(string itemId, string priceType)
        {
            var price = new PriceSellingModel();

            var results = db.SelectMultiple("select * from price_selling where item_id = '" + itemId + "' and pricetype_id = '" + priceType + "'");

            foreach (var row in results)
            {
                var clone = row.Clone();

                price.Id = int.Parse(row["price_id"]);
                price.ItemId = int.Parse(row["item_id"]);
                price.PriceTypeId = int.Parse(row["pricetype_id"]);
                price.SellingPrice = decimal.Parse(row["selling_price"]);
                price.EffectiveFrom = DateTime.Parse(row["effective_from"]);
                price.EffectiveTo = DateTime.Parse(row["effective_to"]);
                price.IsCurrent = (row["current"] == "1") ? true : false;
            }

            return price;
        }

        public List<PriceSellingModel> FetchCurrentPrice(string itemId, string type)
        {
            var priceList = new List<PriceSellingModel>();
            var itemObj = new ItemModel();

            var results = new List<CloneableDictionary<string, string>>();

            if (type == "id")
            {
                itemObj = (ItemModel)itemObj.Fetch(itemId, "id");

                results = db.SelectMultiple("select * from " +
                    "(select * from price_selling where item_id = '" + itemId + "' order by pricetype_id asc, effective_from desc) as temp group by pricetype_id");
            }
            else if (type == "code")
            {
                itemObj = (ItemModel)itemObj.Fetch(itemId, "code");

                results = db.SelectMultiple("select * from " +
                    "(select * from price_selling where item_id = '" + itemObj.ItemId + "' order by pricetype_id asc, effective_from desc) as temp group by pricetype_id");
            }

            var packSize = itemObj.PackSize;
            var packSizeBo = itemObj.PackSizeBO;

            foreach (var row in results)
            {
                var clone = row.Clone();
                var price = new PriceSellingModel();

                price.Id = int.Parse(row["price_id"]);
                price.ItemId = int.Parse(row["item_id"]);
                price.PriceTypeId = int.Parse(row["pricetype_id"]);
                price.SellingPrice = decimal.Parse(row["selling_price"]) / packSize / packSizeBo;
                price.EffectiveFrom = DateTime.Parse(row["effective_from"]);
                price.EffectiveTo = DateTime.Parse(row["effective_to"]);
                price.IsCurrent = (row["current"] == "1") ? true : false;

                priceList.Add(price);
            }

            return priceList;
        }

        public List<PriceSellingModel> FetchPrice6MonthsAgo(string id, string type)
        {
            var priceList = new List<PriceSellingModel>();
            var currentPrices = FetchCurrentPrice(id, type);

            foreach (var currentPrice in currentPrices)
            {
                var date3MonthsAgo = currentPrice.EffectiveFrom.AddMonths(-3);
                var date6MonthsAgo = date3MonthsAgo.AddMonths(-3);

                var result = db.SelectMultiple("select * from price_selling where item_id = '" + currentPrice.itemId + "' and pricetype_id = '" + currentPrice.PriceTypeId + "' ");

                var hasMatch = false;
                foreach (var row in result)
                {
                    var clone = row.Clone();

                    var itemObj = new ItemModel();
                    itemObj = (ItemModel)itemObj.Fetch(currentPrice.ItemId.ToString(), "id");

                    var packSize = itemObj.PackSize;
                    var packSizeBo = itemObj.PackSizeBO;

                    var date = DateTime.Parse(row["effective_from"]);

                    if (date > date6MonthsAgo && date < date3MonthsAgo)
                    {
                        var price = new PriceSellingModel();

                        price.Id = int.Parse(row["price_id"]);
                        price.ItemId = int.Parse(row["item_id"]);
                        price.PriceTypeId = int.Parse(row["pricetype_id"]);
                        price.SellingPrice = decimal.Parse(row["selling_price"]) / packSize / packSizeBo;
                        price.EffectiveFrom = DateTime.Parse(row["effective_from"]);
                        price.EffectiveTo = DateTime.Parse(row["effective_to"]);
                        price.IsCurrent = (row["Current"] == "1") ? true : false;

                        priceList.Add(price);

                        hasMatch = true;
                    }
                    else
                    {
                        hasMatch = false;
                        currentPrice.SellingPrice = currentPrice.SellingPrice / packSize / packSizeBo;
                    }
                }

                if (hasMatch == false)
                {
                    priceList.Add(currentPrice);
                }

            }

            return priceList;
        }

        public List<PriceSellingModel> FetchPrice3MonthsAgo(string id, string type)
        {
            var priceList = new List<PriceSellingModel>();
            var currentPrices = FetchCurrentPrice(id, type);

            foreach (var currentPrice in currentPrices)
            {
                var currentPriceDate = currentPrice.EffectiveFrom;
                var date3MonthsAgo = currentPriceDate.AddMonths(-3);

                var result = db.SelectMultiple("select * from price_selling where item_id = '" + currentPrice.itemId + "' and pricetype_id = '" + currentPrice.PriceTypeId + "' ");

                var hasMatch = false;
                foreach (var row in result)
                {
                    var clone = row.Clone();

                    var itemObj = new ItemModel();
                    itemObj = (ItemModel)itemObj.Fetch(currentPrice.ItemId.ToString(), "id");

                    var packSize = itemObj.PackSize;
                    var packSizeBo = itemObj.PackSizeBO;

                    var date = DateTime.Parse(row["effective_from"]);

                    if (date > date3MonthsAgo && date < currentPriceDate)
                    {
                        var price = new PriceSellingModel();

                        price.Id = int.Parse(row["price_id"]);
                        price.ItemId = int.Parse(row["item_id"]);
                        price.PriceTypeId = int.Parse(row["pricetype_id"]);
                        price.SellingPrice = decimal.Parse(row["selling_price"]) / packSize / packSizeBo;
                        price.EffectiveFrom = DateTime.Parse(row["effective_from"]);
                        price.EffectiveTo = DateTime.Parse(row["effective_to"]);
                        price.IsCurrent = (row["Current"] == "1") ? true : false;

                        priceList.Add(price);

                        hasMatch = true;
                    }
                    else
                    {
                        hasMatch = false;
                        currentPrice.SellingPrice = currentPrice.SellingPrice / packSize / packSizeBo;
                    }
                }

                if (hasMatch == false)
                {
                    priceList.Add(currentPrice);
                }
            }

            return priceList;
        }

        public bool HasPrice(string id, string type)
        {
            var itemObj = new ItemModel();

            if (type == "id")
            {
                itemObj = (ItemModel)itemObj.Fetch(id, "id");

                if (string.IsNullOrWhiteSpace(itemObj.Description))
                    return false;
                else
                    return true;
            }
            else
            {
                itemObj = (ItemModel)itemObj.Fetch(id, "code");

                if (string.IsNullOrWhiteSpace(itemObj.Description))
                    return false;
                else
                    return true;
            }
        }
    }
}
