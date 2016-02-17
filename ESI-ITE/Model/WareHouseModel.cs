using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ESI_ITE.Data_Access;

namespace ESI_ITE.Model
{
    public class WareHouseModel
    {
        public WareHouseModel()
        {

        }

        public WareHouseModel(WareHouseModel source)
        {
            Id = source.Id;
            Code = source.Code;
            Name = source.Name;
            Location = source.Location;
        }

        DataAccess db = new DataAccess();
        private List<WareHouseModel> _warehouses = new List<WareHouseModel>();

        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }

        public List<WareHouseModel> FetchAll()
        {
            List<CloneableDictionary<string, string>> table = db.SelectMultiple("select * from warehouse");
            foreach (var row in table)
            {
                var temp = new WareHouseModel();
                var clone = row.Clone();
                temp.Id = int.Parse(row["warehouse_id"]);
                temp.Code = row["code"].ToString();
                temp.Name = row["name"];
                temp.Location = row["location"];

                _warehouses.Add(temp);
            }
            return _warehouses;
        }
    }
}
