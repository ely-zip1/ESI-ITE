using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ESI_ITE.Data_Access;

namespace ESI_ITE.Model
{
    class SalesOrderModel: IModelTemplate
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

        private int salesmanId;

        public int SalesmanId
        {
            get { return salesmanId; }
            set { salesmanId = value; }
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

        #endregion

        public void AddNew( object item )
        {
            throw new NotImplementedException();
        }

        public void DeleteItem( string qry )
        {
            throw new NotImplementedException();
        }

        public object Fetch( string qry )
        {
            SalesOrderModel orderModel = new SalesOrderModel();

            var record = db.SelectMultiple("select * from orders where order_number = '" + qry + "'");

            IFormatProvider culture = new System.Globalization.CultureInfo("en-US", true);

            foreach ( var row in record )
            {
                var clone = row.Clone();

                orderModel.OrderId = Int32.Parse(row["order_id"]);
                orderModel.OrderNumber = row["order_number"].ToString();
                orderModel.OrderDate = DateTime.Parse(row["order_date"], culture);
                orderModel.RequiredShipDate = DateTime.Parse(row["required_ship_date"], culture);
                orderModel.PONumber = row["po_number"].ToString();
                orderModel.OrderNote = row["order_note"].ToString();
                orderModel.OrderAmount = Decimal.Parse(row["order_amount"]);
                orderModel.Cases = Int32.Parse(row["cases"]);
                orderModel.Pieces = Int32.Parse(row["pieces"]);
                orderModel.IsServed = Int32.Parse(row["served"]) == 1 ? true : false;
                orderModel.IsPicked = Int32.Parse(row["picked"]) == 1 ? true : false;
                orderModel.IsPrinted = Int32.Parse(row["printed"]) == 1 ? true : false;
                orderModel.SalesmanId = Int32.Parse(row["salesman_id"]);
                orderModel.CustomerID = Int32.Parse(row["customer_id"]);
                orderModel.RouteId = Int32.Parse(row["route_id"]);
                orderModel.TermId = Int32.Parse(row["term_id"]);
                orderModel.PriceId = Int32.Parse(row["price_id"]);

            }

            return orderModel;
        }

        public List<object> FetchAll( )
        {
            List<object> list = new List<object>();

            var record = db.SelectMultiple("select * from view_inventory_dummy");

            IFormatProvider culture = new System.Globalization.CultureInfo("en-US", true);

            foreach ( var row in record )
            {
                list.Clear();
                var order = new SalesOrderModel();
                var clone = row.Clone();

                order.OrderId = Int32.Parse(row["order_id"]);
                order.OrderNumber = row["order_number"].ToString();
                order.OrderDate = DateTime.Parse(row["order_date"], culture);
                order.RequiredShipDate = DateTime.Parse(row["required_ship_date"], culture);
                order.PONumber = row["po_number"].ToString();
                order.OrderNote = row["order_note"].ToString();
                order.OrderAmount = Decimal.Parse(row["order_amount"]);
                order.Cases = Int32.Parse(row["cases"]);
                order.Pieces = Int32.Parse(row["pieces"]);
                order.IsServed = Int32.Parse(row["served"]) == 1 ? true : false;
                order.IsPicked = Int32.Parse(row["picked"]) == 1 ? true : false;
                order.IsPrinted = Int32.Parse(row["printed"]) == 1 ? true : false;
                order.SalesmanId = Int32.Parse(row["salesman_id"]);
                order.CustomerID = Int32.Parse(row["customer_id"]);
                order.RouteId = Int32.Parse(row["route_id"]);
                order.TermId = Int32.Parse(row["term_id"]);
                order.PriceId = Int32.Parse(row["price_id"]);

                list.Add(order);
            }

            return list;
        }

        public void UpdateItem( string qry )
        {
            throw new NotImplementedException();
        }
    }
}
