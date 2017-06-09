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
    }
}
