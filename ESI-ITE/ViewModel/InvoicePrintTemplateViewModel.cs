using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESI_ITE.ViewModel
{
    public class InvoicePrintTemplateViewModel : ViewModelBase
    {

        private string invoiceNumber;
        public string InvoiceNumber
        {
            get { return invoiceNumber; }
            set
            {
                invoiceNumber = value;
                OnPropertyChanged();
            }
        }

        private string invoiceDate;
        public string InvoiceDate
        {
            get { return invoiceDate; }
            set
            {
                invoiceDate = value;
                OnPropertyChanged();
            }
        }

        private string accountNumber;
        public string AccountNumber
        {
            get { return accountNumber; }
            set
            {
                accountNumber = value;
                OnPropertyChanged();
            }
        }

        private string salesman;
        public string Salesman
        {
            get { return salesman; }
            set
            {
                salesman = value;
                OnPropertyChanged();
            }
        }

        private string district;
        public string District
        {
            get { return district; }
            set
            {
                district = value;
                OnPropertyChanged();
            }
        }

        private string warehouse;
        public string Warehouse
        {
            get { return warehouse; }
            set
            {
                warehouse = value;
                OnPropertyChanged();
            }
        }

        private string customer;
        public string Customer
        {
            get { return customer; }
            set
            {
                customer = value;
                OnPropertyChanged();
            }
        }

        private string orderNumber;
        public string OrderNumber
        {
            get { return orderNumber; }
            set
            {
                orderNumber = value;
                OnPropertyChanged();
            }
        }

        private string orderDate;
        public string OrderDate
        {
            get { return orderDate; }
            set
            {
                orderDate = value;
                OnPropertyChanged();
            }
        }

        private string termcode;
        public string TermCode
        {
            get { return termcode; }
            set
            {
                termcode = value;
                OnPropertyChanged();
            }
        }

        private string address;
        public string Address
        {
            get { return address; }
            set
            {
                address = value;
                OnPropertyChanged();
            }
        }

        private string deliveryNotes;
        public string DeliveryNotes
        {
            get { return deliveryNotes; }
            set
            {
                deliveryNotes = value;
                OnPropertyChanged();
            }
        }

        private int totalCases;
        public int TotalCases
        {
            get { return totalCases; }
            set
            {
                totalCases = value;
                OnPropertyChanged();
            }
        }

        private int totalPieces;
        public int TotalPieces
        {
            get { return totalPieces; }
            set
            {
                totalPieces = value;
                OnPropertyChanged();
            }
        }

        private string totalAmount;
        public string TotalAmount
        {
            get { return totalAmount; }
            set
            {
                totalAmount = value;
                OnPropertyChanged();
            }
        }

        private string vatAmount;
        public string VatAmount
        {
            get { return vatAmount; }
            set
            {
                vatAmount = value;
                OnPropertyChanged();
            }
        }

        private string taxedTotal;
        public string TaxedTotal
        {
            get { return taxedTotal; }
            set
            {
                taxedTotal = value;
                OnPropertyChanged();
            }
        }

        private string note;
        public string Note
        {
            get { return note; }
            set
            {
                note = value;
                OnPropertyChanged();
            }
        }

        private int pageNumber;
        public int PageNumber
        {
            get { return pageNumber; }
            set
            {
                pageNumber = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<InvoiceItems> itemList = new ObservableCollection<InvoiceItems>();
        public ObservableCollection<InvoiceItems> ItemList
        {
            get { return itemList; }
            set
            {
                itemList = value;
                OnPropertyChanged();
            }
        }

        private bool labelVisibility = true;
        public bool LabelVisibility
        {
            get { return labelVisibility; }
            set
            {
                labelVisibility = value;
                OnPropertyChanged();
            }
        }

    }


    public class InvoiceItems
    {
        private string itemDescription;
        public string ItemDescription
        {
            get { return itemDescription; }
            set { itemDescription = value; }
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

        private decimal pricePerCase;
        public decimal PricePerCase
        {
            get { return pricePerCase; }
            set { pricePerCase = value; }
        }

        private decimal pricePerPiece;
        public decimal PricePerPiece
        {
            get { return pricePerPiece; }
            set { pricePerPiece = value; }
        }

        private decimal amount;
        public decimal Amount
        {
            get { return amount; }
            set { amount = value; }
        }

    }

    public class InvoiceHeader
    {
        private string invoiceNumber;
        public string InvoiceNumber
        {
            get { return invoiceNumber; }
            set { invoiceNumber = value; }
        }

        private string invoiceDate;
        public string InvoiceDate
        {
            get { return invoiceDate; }
            set { invoiceDate = value; }
        }

        private string accountNumber;
        public string AccountNumber
        {
            get { return accountNumber; }
            set { accountNumber = value; }
        }

        private string saleman;
        public string Salesman
        {
            get { return saleman; }
            set { saleman = value; }
        }

        private string district;
        public string District
        {
            get { return district; }
            set { district = value; }
        }

        private string warehouse;
        public string Warehouse
        {
            get { return warehouse; }
            set { warehouse = value; }
        }

        private string customerName;
        public string CustomerName
        {
            get { return customerName; }
            set { customerName = value; }
        }

        private string customerAddress;
        public string CustomerAddress
        {
            get { return customerAddress; }
            set { customerAddress = value; }
        }

        private string soNumber;
        public string SoNumber
        {
            get { return soNumber; }
            set { soNumber = value; }
        }

        private string soDate;
        public string SoDate
        {
            get { return soDate; }
            set { soDate = value; }
        }

        private string terms;
        public string Terms
        {
            get { return terms; }
            set { terms = value; }
        }

        private string deliveryNotes;
        public string DeliveryNotes
        {
            get { return deliveryNotes; }
            set { deliveryNotes = value; }
        }

    }
}
