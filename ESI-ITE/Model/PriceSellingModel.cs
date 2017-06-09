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
            get { return id; }
            set { id = value; }
        }

        private int itemId;
        public int ItemId
        {
            get { return itemId; }
            set { itemId = value; }
        }

        private int priceTypeId;
        public int PriceTypeId
        {
            get { return priceTypeId; }
            set { priceTypeId = value; }
        }

        private decimal sellingPrice;
        public decimal SellingPrice
        {
            get { return sellingPrice; }
            set { sellingPrice = value; }
        }

        private DateTime effectiveFrom;
        public DateTime EffectiveFrom
        {
            get { return effectiveFrom; }
            set { effectiveFrom = value; }
        }

        private DateTime effectiveTo;
        public DateTime EffectiveTo
        {
            get { return effectiveTo; }
            set { effectiveTo = value; }
        }

        private bool isCurrent;
        public bool IsCurrent
        {
            get { return isCurrent; }
            set { isCurrent = value; }
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

            var results = db.SelectMultiple("select * from price_selling where code = '" + id + "'");

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
    }
}
