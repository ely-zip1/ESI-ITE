using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESI_ITE.Model
{
    class CustomerModel
    {
        #region Properties

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


        #endregion
    }
}
