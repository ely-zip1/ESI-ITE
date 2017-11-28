using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ESI_ITE.Data_Access;

namespace ESI_ITE.Model
{
    public class PriceTypeModel : IModelTemplate
    {
        #region Properties

        DataAccess db = new DataAccess();

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

        private string code;
        public string Code
        {
            get
            {
                return code;
            }
            set
            {
                code = value;
            }
        }

        private string description;
        public string Description
        {
            get
            {
                return description;
            }
            set
            {
                description = value;
            }
        }

        private bool modify;
        public bool Modify
        {
            get
            {
                return modify;
            }
            set
            {
                modify = value;
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
            var pricetype = new PriceTypeModel();

            var record = new List<CloneableDictionary<string, string>>();
            if (type == "code")
            {
                record = db.SelectMultiple("select * from pricetype where code = '" + id + "'");
            }
            else if (type == "id")
            {
                record = db.SelectMultiple("select * from pricetype where pricetype_id = '" + id + "'");
            }

            foreach (var row in record)
            {
                var clone = row.Clone();

                pricetype.PriceTypeId = int.Parse(row["pricetype_id"]);
                pricetype.Code = row["code"].ToString();
                pricetype.Description = row["description"].ToString();
                pricetype.Modify = bool.Parse(row["modify"]);
            }

            return pricetype;
        }

        public List<object> FetchAll()
        {
            List<object> list = new List<object>();

            var record = db.SelectMultiple("select * from pricetype");

            list.Clear();

            foreach (var row in record)
            {
                var pricetype = new PriceTypeModel();
                var clone = row.Clone();

                pricetype.PriceTypeId = int.Parse(row["pricetype_id"]);
                pricetype.Code = row["code"].ToString();
                pricetype.Description = row["description"].ToString();
                pricetype.Modify = bool.Parse(row["modify"]);

                list.Add(pricetype);
            }

            return list;
        }

        public List<PriceTypeModel> FetchPerItem(string itemId)
        {
            var pricetypeList = new List<PriceTypeModel>();
            var results = db.SelectMultiple("select * from price_selling where item_id = '" + itemId + "'");

            foreach (var row in results)
            {
                pricetypeList.Add((PriceTypeModel)this.Fetch(row["pricetype_id"], "id"));
            }

            return pricetypeList;
        }

        public void UpdateItem(string qry)
        {
            throw new NotImplementedException();
        }
    }
}
