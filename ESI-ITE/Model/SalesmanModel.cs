using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ESI_ITE.Data_Access;

namespace ESI_ITE.Model
{
    public class SalesmanModel: IModelTemplate
    {
        #region Properties

        DataAccess db = new DataAccess();

        private int salesmanId;
        public int SalesmanId
        {
            get { return salesmanId; }
            set { salesmanId = value; }
        }

        private string salesmanNumber;
        public string SalesmanNumber
        {
            get { return salesmanNumber; }
            set { salesmanNumber = value; }
        }

        private string salesmanName;
        public string SalesmanName
        {
            get { return salesmanName; }
            set { salesmanName = value; }
        }

        #endregion

        public List<object> FetchAll( )
        {
            List<object> list = new List<object>();

            var record = db.SelectMultiple("select * from salesman");

            IFormatProvider culture = new System.Globalization.CultureInfo("en-US", true);

            list.Clear();

            foreach ( var row in record )
            {
                var salesman = new SalesmanModel();
                var clone = row.Clone();

                salesman.SalesmanId = int.Parse(row["salesman_id"]);
                salesman.SalesmanNumber = row["salesman_number"].ToString();
                salesman.SalesmanName = row["salesman_name"].ToString();

                list.Add(salesman);
            }

            return list;
        }

        public object Fetch( string id, string type )
        {
            SalesmanModel salesman = new SalesmanModel();

            var record = new List<CloneableDictionary<string, string>>();

            if ( type == "code" )
                record = db.SelectMultiple("select * from salesman where salesman_number = '" + id + "'");
            else if ( type == "id" )
                record = db.SelectMultiple("select * from salesman where salesman_id = '" + id + "'");

            IFormatProvider culture = new System.Globalization.CultureInfo("en-US", true);

            foreach ( var row in record )
            {
                var clone = row.Clone();

                salesman.SalesmanId = int.Parse(row["salesman_id"]);
                salesman.SalesmanNumber = row["salesman_number"].ToString();
                salesman.SalesmanName = row["salesman_name"].ToString();
            }

            return salesman;
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
