using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ESI_ITE.Data_Access;

namespace ESI_ITE.Model
{
    public class CustomerModel: IModelTemplate
    {
        #region Properties

        DataAccess db = new DataAccess();

        private int customedId;
        public int CustomerId
        {
            get { return customedId; }
            set { customedId = value; }
        }

        private string customerNumber;
        public string CustomerNumber
        {
            get { return customerNumber; }
            set { customerNumber = value; }
        }

        private string customerName;
        public string CustomerName
        {
            get { return customerName; }
            set { customerName = value; }
        }

        private string addressMain;
        public string AddressMain
        {
            get { return addressMain; }
            set { addressMain = value; }
        }

        private string addressCity;
        public string AddressCity
        {
            get { return addressCity; }
            set { addressCity = value; }
        }

        private string addressProvince;
        public string AddressProvince
        {
            get { return addressProvince; }
            set { addressProvince = value; }
        }

        private string addressZipcode;
        public string AddressZipcode
        {
            get { return addressZipcode; }
            set { addressZipcode = value; }
        }

        private string telephone;
        public string Telephone
        {
            get { return telephone; }
            set { telephone = value; }
        }

        private decimal taxRate;
        public decimal TaxRate
        {
            get { return taxRate; }
            set { taxRate = value; }
        }

        private decimal creditLimit;
        public decimal CreditLimit
        {
            get { return creditLimit; }
            set { creditLimit = value; }
        }

        private decimal netSales;
        public decimal NetSales
        {
            get { return netSales; }
            set { netSales = value; }
        }

        private string tinNumber;
        public string TinNumber
        {
            get { return tinNumber; }
            set { tinNumber = value; }
        }

        private DateTime entryDate;
        public DateTime EntryDate
        {
            get { return entryDate; }
            set { entryDate = value; }
        }

        private bool isBad;
        public bool Isbad
        {
            get { return isBad; }
            set { isBad = value; }
        }

        private DateTime badSince;
        public DateTime Badsince
        {
            get { return badSince; }
            set { badSince = value; }
        }


        private int tradeClass;
        public int TradeClass
        {
            get { return tradeClass; }
            set { tradeClass = value; }
        }

        private int districtId;
        public int DistrictId
        {
            get { return districtId; }
            set { districtId = value; }
        }

        private int termId;
        public int TermId
        {
            get { return termId; }
            set { termId = value; }
        }

        private int routeId;
        public int RouteId
        {
            get { return routeId; }
            set { routeId = value; }
        }

        private int pricetypeId;
        public int PricetypeId
        {
            get { return pricetypeId; }
            set { pricetypeId = value; }
        }

        private string contactPerson;
        public string ContactPerson
        {
            get { return contactPerson; }
            set { contactPerson = value; }
        }

        private bool isExtracted;
        public bool IsExtracted
        {
            get { return isExtracted; }
            set { isExtracted = value; }
        }

        #endregion


        public List<object> FetchAll( )
        {
            List<object> list = new List<object>();

            var record = db.SelectMultiple("select * from customers");

            IFormatProvider culture = new System.Globalization.CultureInfo("en-US", true);
            list.Clear();

            foreach ( var row in record )
            {
                var customer = new CustomerModel();
                var clone = row.Clone();

                customer.CustomerId = Int32.Parse(row["customer_id"]);
                customer.CustomerNumber = row["customer_number"].ToString();
                customer.CustomerName = row["customer_name"].ToString();
                customer.AddressMain = row["address_main"].ToString();
                customer.AddressCity = row["address_city"].ToString();
                customer.AddressProvince = row["address_province"].ToString();
                customer.AddressZipcode = row["address_zipcode"].ToString();
                customer.Telephone = row["telephone"].ToString();
                customer.TaxRate = decimal.Parse(row["taxrate"]);
                customer.CreditLimit = decimal.Parse(row["credit_limit"]);
                customer.NetSales = decimal.Parse(row["net_sales"]);
                customer.TinNumber = row["tin_number"].ToString();
                customer.EntryDate = DateTime.Parse(row["entry_date"]);
                customer.Isbad = bool.Parse(row["is_bad"]);
                customer.Badsince = DateTime.Parse(row["bad_since"]);
                customer.TradeClass = Int32.Parse(row["trade_class_id"]);
                customer.DistrictId = Int32.Parse(row["district_id"]);
                customer.TermId = Int32.Parse(row["term_id"]);
                customer.RouteId = Int32.Parse(row["route_id"]);
                customer.PricetypeId = Int32.Parse(row["pricetype_id"]);
                customer.ContactPerson = row["contact_person"].ToString();
                customer.IsExtracted = bool.Parse(row["extracted"]);

                list.Add(customer);
            }

            return list;
        }

        public object Fetch( string id, string type )
        {
            CustomerModel customer = new CustomerModel();

            var record = new List<CloneableDictionary<string, string>>();

            if ( type == "code" )
            {
                record = db.SelectMultiple("select * from customers where customer_number = '" + id + "'");
            }
            else if ( type == "id" )
            {
                record = db.SelectMultiple("select * from customers where customer_id = '" + id + "'");
            }

            IFormatProvider culture = new System.Globalization.CultureInfo("en-US", true);

            foreach ( var row in record )
            {
                var clone = row.Clone();

                customer.CustomerId = Int32.Parse(row["customer_id"]);
                customer.CustomerNumber = row["customer_number"].ToString();
                customer.CustomerName = row["customer_name"].ToString();
                customer.AddressMain = row["address_main"].ToString();
                customer.AddressCity = row["address_city"].ToString();
                customer.AddressProvince = row["address_province"].ToString();
                customer.AddressZipcode = row["address_zipcode"].ToString();
                customer.Telephone = row["telephone"].ToString();
                customer.TaxRate = decimal.Parse(row["taxrate"]);
                customer.CreditLimit = decimal.Parse(row["credit_limit"]);
                customer.NetSales = decimal.Parse(row["net_sales"]);
                customer.TinNumber = row["tin_number"].ToString();
                customer.EntryDate = DateTime.Parse(row["entry_date"]);
                customer.Isbad = bool.Parse(row["is_bad"]);
                customer.Badsince = DateTime.Parse(row["bad_since"]);
                customer.TradeClass = Int32.Parse(row["trade_class_id"]);
                customer.DistrictId = Int32.Parse(row["district_id"]);
                customer.TermId = Int32.Parse(row["term_id"]);
                customer.RouteId = Int32.Parse(row["route_id"]);
                customer.PricetypeId = Int32.Parse(row["pricetype_id"]);
                customer.ContactPerson = row["contact_person"].ToString();
                customer.IsExtracted = bool.Parse(row["extracted"]);

            }

            return customer;
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
