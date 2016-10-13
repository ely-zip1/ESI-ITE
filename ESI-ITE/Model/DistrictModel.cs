using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ESI_ITE.Data_Access;

namespace ESI_ITE.Model
{
    public class DistrictModel: IModelTemplate
    {
        #region Properties

        DataAccess db = new DataAccess();

        private int districtId;
        public int DistrictId
        {
            get { return districtId; }
            set { districtId = value; }
        }

        private string districtNumber;
        public string DistrictNumber
        {
            get { return districtNumber; }
            set { districtNumber = value; }
        }

        private string area;
        public string Area
        {
            get { return area; }
            set { area = value; }
        }

        private int salesman;
        public int Salesman
        {
            get { return salesman; }
            set { salesman = value; }
        }

        private decimal target;
        public decimal Target
        {
            get { return target; }
            set { target = value; }
        }

        private int warehouse;
        public int Warehouse
        {
            get { return warehouse; }
            set { warehouse = value; }
        }

        #endregion


        public List<object> FetchAll( )
        {
            List<object> list = new List<object>();

            var record = db.SelectMultiple("select * from districts order by district_number asc");

            IFormatProvider culture = new System.Globalization.CultureInfo("en-US", true);

            list.Clear();

            foreach ( var row in record )
            {
                var district = new DistrictModel();
                var clone = row.Clone();

                district.DistrictId = int.Parse(row["district_id"]);
                district.DistrictNumber = row["district_number"].ToString();
                district.Area = row["area"].ToString();
                district.Salesman = int.Parse(row["salesman"]);
                district.Target = decimal.Parse(row["target"]);
                district.Warehouse = int.Parse(row["warehouse"]);

                list.Add(district);
            }

            return list;
        }

        public object Fetch( string id, string type )
        {
            DistrictModel district = new DistrictModel();

            var record = new List<CloneableDictionary<string, string>>();

            if ( type == "code" )
                record = db.SelectMultiple("select * from districts where district_number = '" + id + "'");
            else if ( type == "id" )
                record = db.SelectMultiple("select * from districts where district_id = '" + id + "'");

            IFormatProvider culture = new System.Globalization.CultureInfo("en-US", true);

            foreach ( var row in record )
            {
                var clone = row.Clone();

                district.DistrictId = int.Parse(row["district_id"]);
                district.DistrictNumber = row["district_number"].ToString();
                district.Area = row["area"].ToString();
                district.Salesman = int.Parse(row["salesman"]);
                district.Target = decimal.Parse(row["target"]);
                district.Warehouse = int.Parse(row["warehouse"]);

            }

            return district;
        }

        public void AddNew( object item )
        {
            throw new NotImplementedException();
        }

        public void UpdateItem( string qry )
        {
            throw new NotImplementedException();
        }

        public void DeleteItem( string qry )
        {
            throw new NotImplementedException();
        }
    }
}
