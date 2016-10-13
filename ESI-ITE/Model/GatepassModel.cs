using ESI_ITE.Data_Access;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESI_ITE.Model
{
    public class GatepassModel: IModelTemplate
    {
        #region Properties

        DataAccess db = new DataAccess();

        private int id;
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        private string gatepassNumber;
        public string GatepassNumber
        {
            get { return gatepassNumber; }
            set { gatepassNumber = value; }
        }

        private int locationId;
        public int LocationId
        {
            get { return locationId; }
            set { locationId = value; }
        }

        private int itemId;
        public int ItemId
        {
            get { return itemId; }
            set { itemId = value; }
        }

        private string unit1;
        public string Unit1
        {
            get { return unit1; }
            set { unit1 = value; }
        }

        private string unit2;

        public string Unit2
        {
            get { return unit2; }
            set { unit2 = value; }
        }

        private int cases;
        public int Cases
        {
            get { return cases; }
            set { cases = value; }
        }

        private int pieces;
        public int Pieces
        {
            get { return pieces; }
            set { pieces = value; }
        }

        private DateTime expiry;
        public DateTime Expiry
        {
            get { return expiry; }
            set { expiry = value; }
        }

        private decimal weight;
        public decimal Weight
        {
            get { return weight; }
            set { weight = value; }
        }

        private DateTime date;
        public DateTime Date
        {
            get { return date; }
            set { date = value; }
        }


        #endregion


        public void AddNew( object item )
        {
            var gatepass = (GatepassModel)item;
            StringBuilder sb = new StringBuilder();

            sb.Append("insert into gatepass values(null, ");
            sb.Append("'" + gatepass.GatepassNumber + "', ");
            sb.Append("'" + gatepass.LocationId + "', ");
            sb.Append("'" + gatepass.ItemId + "', ");
            sb.Append("'" + gatepass.Unit1 + "', ");
            sb.Append("'" + gatepass.Unit2 + "', ");
            sb.Append("'" + gatepass.Cases + "', ");
            sb.Append("'" + gatepass.Pieces + "', ");
            sb.Append("'" + gatepass.Expiry.ToString("%M/%d/yyyy") + "', ");
            sb.Append("'" + gatepass.Weight + "', ");
            sb.Append("'" + gatepass.Date.ToString("%M/%d/yyyy") + "' ");
            sb.Append(")");

            db.Insert(sb.ToString());
        }

        public void DeleteItem( string qry )
        {
            db.Delete(qry);
        }

        public void DeleteItem( GatepassModel gatepass )
        {
            db.Delete("delete from gatepass where gatepass_id = '" + Id + "'");
        }

        public object Fetch( string id, string type )
        {
            var results = new List<CloneableDictionary<string, string>>();
            var gatepass = new GatepassModel();

            switch ( type )
            {
                case "id":
                    results = db.SelectMultiple("select * from gatepass  where gatepass_id = '" + id + "'");
                    break;
                case "code":
                    results = db.SelectMultiple("select * from gatepass  where gatepass_number = '" + id + "'");
                    break;
            }

            foreach ( var row in results )
            {
                var clone = row.Clone();
                gatepass = fillObject(row);
            }

            return gatepass;
        }

        public List<object> FetchAll( )
        {
            var gatepassList = new List<object>();
            var results = new List<CloneableDictionary<string, string>>();

            foreach ( var row in results )
            {
                var clone = row.Clone();

                gatepassList.Add(fillObject(row));
            }

            return gatepassList;
        }

        public void UpdateItem( string qry )
        {
            throw new NotImplementedException();
        }

        private GatepassModel fillObject( CloneableDictionary<string, string> row )
        {
            var gatepass = new GatepassModel();

            gatepass.Id = int.Parse(row["gatepass_id"]);
            gatepass.GatepassNumber = row["gatepass_number"];
            gatepass.LocationId = int.Parse(row["location_id"]);
            gatepass.ItemId = int.Parse(row["item_id"]);
            gatepass.Unit1 = row["unit1"];
            gatepass.Unit2 = row["unit2"];
            gatepass.Cases = int.Parse(row["cases"]);
            gatepass.Pieces = int.Parse(row["pieces"]);
            gatepass.Expiry = DateTime.Parse(row["expiry"]);
            gatepass.Weight = decimal.Parse(row["weight"]);
            gatepass.Date = DateTime.Parse(row["date"]);

            return gatepass;
        }
    }
}
