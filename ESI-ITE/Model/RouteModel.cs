using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ESI_ITE.Data_Access;

namespace ESI_ITE.Model
{
    public class RouteModel: IModelTemplate
    {
        #region Properties

        DataAccess db = new DataAccess();

        private int routeId;
        public int RouteId
        {
            get { return routeId; }
            set { routeId = value; }
        }

        private string routeCode;
        public string RouteCode
        {
            get { return routeCode; }
            set { routeCode = value; }
        }

        private string routeDescription;
        public string RouteDescription
        {
            get { return routeDescription; }
            set { routeDescription = value; }
        }

        #endregion


        public List<object> FetchAll( )
        {
            List<object> list = new List<object>();

            var record = db.SelectMultiple("select * from routes");

            IFormatProvider culture = new System.Globalization.CultureInfo("en-US", true);

            list.Clear();

            foreach ( var row in record )
            {
                var route = new RouteModel();
                var clone = row.Clone();

                route.RouteId = int.Parse(row["route_id"]);
                route.RouteCode = row["code"].ToString();
                route.RouteDescription = row["description"].ToString();

                list.Add(route);
            }

            return list;
        }

        public object Fetch( string id, string type )
        {
            RouteModel route = new RouteModel();

            var record = new List<CloneableDictionary<string, string>>();

            if ( type == "code" )
                record = db.SelectMultiple("select * from routes where code = '" + id + "'");
            else if ( type == "id" )
                record = db.SelectMultiple("select * from routes where route_id = '" + id + "'");

            IFormatProvider culture = new System.Globalization.CultureInfo("en-US", true);

            foreach ( var row in record )
            {
                var clone = row.Clone();

                route.RouteId = int.Parse(row["route_id"]);
                route.RouteCode = row["code"].ToString();
                route.RouteDescription = row["description"].ToString();

            }

            return route;
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
