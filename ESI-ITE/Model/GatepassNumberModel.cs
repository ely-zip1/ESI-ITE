using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ESI_ITE.Data_Access;

namespace ESI_ITE.Model
{
    public class GatepassNumberModel : IModelTemplate
    {
        #region properties

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

        private string gatepassNumber;
        public string GatepassNumber
        {
            get
            {
                return gatepassNumber;
            }
            set
            {
                gatepassNumber = value;
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
            throw new NotImplementedException();
        }

        public List<object> FetchAll()
        {
            throw new NotImplementedException();
        }

        public void UpdateItem(string qry)
        {
            throw new NotImplementedException();
        }

        public GatepassNumberModel FetchNew()
        {
            var result = db.SelectMultiple("select max(id) as id, gatepass_number from gatepass_number");

            var gatepassNumber = new GatepassNumberModel();
            foreach (var row in result)
            {
                var clone = row.Clone();

                gatepassNumber.Id = int.Parse(row["id"]);
                gatepassNumber.GatepassNumber = row["gatepass_number"];
            }

            var gatepassNumberString = (int.Parse(gatepassNumber.GatepassNumber) + 1).ToString();
            while (gatepassNumberString.Length < 6)
            {
                gatepassNumberString = "0" + gatepassNumberString;
            }
            db.Delete("truncate gatepass_number");
            db.Insert("insert into gatepass_number values(null,'" + gatepassNumberString + "')");

            return gatepassNumber;
        }
    }
}
