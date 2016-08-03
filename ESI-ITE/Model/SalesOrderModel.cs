using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ESI_ITE.Data_Access;

namespace ESI_ITE.Model
{
    public class SalesOrderModel: IModelTemplate
    {
        #region Properties
        DataAccess db = new DataAccess();

        private int orderId;
        public int OrderId
        {
            get { return orderId; }
            set { orderId = value; }
        }

        private string orderNumber;
        public string OrderNumber
        {
            get { return orderNumber; }
            set { orderNumber = value; }
        }

        private DateTime orderDate;
        public DateTime OrderDate
        {
            get { return orderDate; }
            set { orderDate = value; }
        }

        private DateTime requiredShipDate;
        public DateTime RequiredShipDate
        {
            get { return requiredShipDate; }
            set { requiredShipDate = value; }
        }

        private string poNumber;
        public string PONumber
        {
            get { return poNumber; }
            set { poNumber = value; }
        }

        private string orderNote;
        public string OrderNote
        {
            get { return orderNote; }
            set { orderNote = value; }
        }

        private decimal orderAmount;
        public decimal OrderAmount
        {
            get { return orderAmount; }
            set { orderAmount = value; }
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

        private bool isServed;
        public bool IsServed
        {
            get { return isServed; }
            set { isServed = value; }
        }

        private bool isPicked;
        public bool IsPicked
        {
            get { return isPicked; }
            set { isPicked = value; }
        }

        private bool isPrinted;
        public bool IsPrinted
        {
            get { return isPrinted; }
            set { isPrinted = value; }
        }

        private int districtId;
        public int DistrictId
        {
            get { return districtId; }
            set { districtId = value; }
        }

        private int customerId;
        public int CustomerID
        {
            get { return customerId; }
            set { customerId = value; }
        }

        private int routeId;
        public int RouteId
        {
            get { return routeId; }
            set { routeId = value; }
        }

        private int termId;
        public int TermId
        {
            get { return termId; }
            set { termId = value; }
        }

        private int priceId;
        public int PriceId
        {
            get { return priceId; }
            set { priceId = value; }
        }

        private int warehouseId;
        public int WarehouseId
        {
            get { return warehouseId; }
            set { warehouseId = value; }
        }


        #endregion

        public void AddNew( object item )
        {
            var order = item as SalesOrderModel;
            StringBuilder sb = new StringBuilder();

            sb.Append("insert into orders values (null,");
            sb.Append("'" + order.OrderNumber + "',");
            sb.Append("str_to_date('" + order.OrderDate.Date.ToString("MM/dd/yyyy") + "', '%m/%d/%Y'),");
            sb.Append("str_to_date('" + order.RequiredShipDate.Date.ToString("MM/dd/yyyy") + "', '%m/%d/%Y'),");
            sb.Append("'" + order.PONumber + "',");
            sb.Append("'" + order.OrderNote + "',");
            sb.Append("'" + order.OrderAmount + "',");
            sb.Append("'" + order.Cases + "',");
            sb.Append("'" + order.Pieces + "',");
            sb.Append("'" + 0 + "',");
            sb.Append("'" + 0 + "',");
            sb.Append("'" + 0 + "',");
            sb.Append("'" + order.CustomerID + "',");
            sb.Append("'" + order.RouteId + "',");
            sb.Append("'" + order.TermId + "',");
            sb.Append("'" + order.PriceId + "',");
            sb.Append("'" + order.DistrictId + "',");
            sb.Append("'" + order.WarehouseId + "'");
            sb.Append(")");

            db.Insert(sb.ToString());
        }

        public void DeleteItem( string qry )
        {
            db.Delete(qry);
        }

        public void UpdateInventoryDummy( string type, string value )
        {
            if ( type == "cutOffDate" )
            {
                var orderIdList = db.SelectMultiple("select order_id from orders where order_date <= str_to_date('" + value + "','%m/%d/%Y')");

                foreach ( var row in orderIdList )
                {
                    var clone = row.Clone();
                    var dummy = new InventoryDummy2Model();

                    dummy.DeleteItem("delete from inventory_dummy_2 where order_id = '" + row + "'");
                }
            }
            else if ( type == "orderNumber" )
            {
                var orderId = db.Select("select order_id from orders where order_number = '" + value + "'");

                var dummy = new InventoryDummy2Model();
                dummy.DeleteItem("delete from inventory_dummy_2 where order_id = '" + orderId + "'");
            }
        }

        public void DeleteOrders( DateTime date )
        {
            var cutOffDate = date.ToString("MM/dd/yyyy");

            db.Delete("delete from orders where order_date <= str_to_date('" + cutOffDate + "','%m/%d/%Y')");
        }

        public object Fetch( string id, string type )
        {
            SalesOrderModel orderModel = new SalesOrderModel();

            var record = new List<CloneableDictionary<string, string>>();

            if ( type == "code" )
            {
                record = db.SelectMultiple("select * from orders where order_number = '" + id + "'");
            }
            else if ( type == "id" )
            {
                record = db.SelectMultiple("select * from orders where order_id = '" + id + "'");
            }
            IFormatProvider culture = new System.Globalization.CultureInfo("en-US", true);

            foreach ( var row in record )
            {
                var clone = row.Clone();

                orderModel.OrderId = Int32.Parse(row["order_id"]);
                orderModel.OrderNumber = row["order_number"].ToString();
                orderModel.OrderDate = DateTime.Parse(row["order_date"]);
                orderModel.RequiredShipDate = DateTime.Parse(row["required_ship_date"]);
                orderModel.PONumber = row["po_number"].ToString();
                orderModel.OrderNote = row["order_note"].ToString();
                orderModel.OrderAmount = Decimal.Parse(row["order_amount"]);
                orderModel.Cases = Int32.Parse(row["cases"]);
                orderModel.Pieces = Int32.Parse(row["pieces"]);
                orderModel.IsServed = bool.Parse(row["served"]);
                orderModel.IsPicked = bool.Parse(row["picked"]);
                orderModel.IsPrinted = bool.Parse(row["printed"]);
                orderModel.DistrictId = Int32.Parse(row["district_id"]);
                orderModel.CustomerID = Int32.Parse(row["customer_id"]);
                orderModel.RouteId = Int32.Parse(row["route_id"]);
                orderModel.TermId = Int32.Parse(row["term_id"]);
                orderModel.PriceId = Int32.Parse(row["price_id"]);
                orderModel.WarehouseId = Int32.Parse(row["warehouse_id"]);

            }

            return orderModel;
        }

        public List<object> FetchAll( )
        {
            List<object> list = new List<object>();

            var record = db.SelectMultiple("select * from orders order by order_number desc");

            IFormatProvider culture = new System.Globalization.CultureInfo("en-US", true);

            list.Clear();
            foreach ( var row in record )
            {
                var order = new SalesOrderModel();
                var clone = row.Clone();

                order.OrderId = Int32.Parse(row["order_id"]);
                order.OrderNumber = row["order_number"].ToString();
                order.OrderDate = DateTime.Parse(row["order_date"]);
                order.RequiredShipDate = DateTime.Parse(row["required_ship_date"]);
                order.PONumber = row["po_number"].ToString();
                order.OrderNote = row["order_note"].ToString();
                order.OrderAmount = Decimal.Parse(row["order_amount"]);
                order.Cases = Int32.Parse(row["cases"]);
                order.Pieces = Int32.Parse(row["pieces"]);
                order.IsServed = bool.Parse(row["served"]);
                order.IsPicked = bool.Parse(row["picked"]);
                order.IsPrinted = bool.Parse(row["printed"]);
                order.DistrictId = Int32.Parse(row["district_id"]);
                order.CustomerID = Int32.Parse(row["customer_id"]);
                order.RouteId = Int32.Parse(row["route_id"]);
                order.TermId = Int32.Parse(row["term_id"]);
                order.PriceId = Int32.Parse(row["price_id"]);
                order.WarehouseId = Int32.Parse(row["warehouse_id"]);

                list.Add(order);
            }

            return list;
        }

        public void UpdateItem( string qry )
        {
            db.Update(qry);
        }
    }
}
