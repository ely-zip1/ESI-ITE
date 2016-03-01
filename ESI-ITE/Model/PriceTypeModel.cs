using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ESI_ITE.Data_Access;

namespace ESI_ITE.Model
{
    public class PriceTypeModel
    {
        DataAccess db = new DataAccess();

        private string id;
        public string Id
        {
            get { return id; }
            set { id = value; }
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

        public List<PriceTypeModel> FetchAll(string priceCategory)
        {
            var priceTypes = new List<PriceTypeModel>();
            var table = db.SelectMultiple("select * from price_type");

            if (priceCategory == "Selling Price")
            {
                foreach (var row in table)
                {
                    var type = new PriceTypeModel();
                    var clone = row.Clone();

                    type.Id = row["price_type_id"];
                    type.Code = row["price_code"];
                    type.Description = row["description"];

                    priceTypes.Add(type);
                }
            }
            else
            {
                foreach (var row in table)
                {
                    var type = new PriceTypeModel();
                    var clone = row.Clone();

                    if (row["price_code"] == "PL1")
                    {
                        type.Id = row["price_type_id"];
                        type.Code = row["price_code"];
                        type.Description = row["description"];

                        priceTypes.Add(type);
                        break;
                    }
                }

            }

            return priceTypes;
        }

    }
}
