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

        private bool labelVisibility = false;
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

        private string cases;
        public string Cases
        {
            get { return cases; }
            set { cases = value; }
        }

        private string pieces;
        public string Pieces
        {
            get { return pieces; }
            set { pieces = value; }
        }

        private string casesPrice;
        public string CasesPrice
        {
            get { return casesPrice; }
            set { casesPrice = value; }
        }

        private string piecesPrice;
        public string PiecesPrice
        {
            get { return piecesPrice; }
            set { piecesPrice = value; }
        }

        private string amount;
        public string Amount
        {
            get { return amount; }
            set { amount = value; }
        }

    }
}
