using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESI_ITE.ViewModel.CNDN
{
    public class DebitNotePrintTemplateViewModel:ViewModelBase
    {
        private string user;
        public string User
        {
            get { return user; }
            set
            {
                user = value;
                OnPropertyChanged();
            }
        }

        private string printDate;
        public string PrintDate
        {
            get { return printDate; }
            set
            {
                printDate = value;
                OnPropertyChanged();
            }
        }

        private string printTime;
        public string PrintTime
        {
            get { return printTime; }
            set
            {
                printTime = value;
                OnPropertyChanged();
            }
        }

        private string pageNumber;
        public string PageNumber
        {
            get { return pageNumber; }
            set
            {
                pageNumber = value;
                OnPropertyChanged();
            }
        }

        private string customerNumber;
        public string CustomerNumber
        {
            get { return customerNumber; }
            set
            {
                customerNumber = value;
                OnPropertyChanged();

            }
        }

        private string customerName;
        public string CustomerName
        {
            get { return customerName; }
            set
            {
                customerName = value;
                OnPropertyChanged();
            }
        }

        private string barangay;
        public string Barangay
        {
            get { return barangay; }
            set
            {
                barangay = value;
                OnPropertyChanged();
            }
        }

        private string city;
        public string City
        {
            get { return city; }
            set
            {
                city = value;
                OnPropertyChanged();
            }
        }

        private string province;
        public string Province
        {
            get { return province; }
            set
            {
                province = value;
                OnPropertyChanged();
            }
        }

        private string warehouseCode;
        public string WarehouseCode
        {
            get { return warehouseCode; }
            set
            {
                warehouseCode = value;
                OnPropertyChanged();
            }
        }

        private string warehouseDescription;
        public string WarehouseDescription
        {
            get { return warehouseDescription; }
            set
            {
                warehouseDescription = value;
                OnPropertyChanged();
            }
        }

        private string location;
        public string Location
        {
            get { return location; }
            set
            {
                location = value;
                OnPropertyChanged();
            }
        }

        private string salesmanNumber;
        public string SalesmanNumber
        {
            get { return salesmanNumber; }
            set
            {
                salesmanNumber = value;
                OnPropertyChanged();
            }
        }

        private string salesmanName;
        public string SalesmanName
        {
            get { return salesmanName; }
            set
            {
                salesmanName = value;
                OnPropertyChanged();
            }
        }

        private string dnNumber;
        public string DnNumber
        {
            get { return dnNumber; }
            set
            {
                dnNumber = value;
                OnPropertyChanged();
            }
        }

        private string dnDate;
        public string DnDate
        {
            get { return dnDate; }
            set
            {
                dnDate = value;
                OnPropertyChanged();
            }
        }

        private string referenceNumber;
        public string ReferenceNumber
        {
            get { return referenceNumber; }
            set
            {
                referenceNumber = value;
                OnPropertyChanged();
            }
        }

        private string comment;
        public string Comment
        {
            get { return comment; }
            set
            {
                comment = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<DnItem> dnItemCollection = new ObservableCollection<DnItem>();
        public ObservableCollection<DnItem> DnItemCollection
        {
            get { return dnItemCollection; }
            set
            {
                dnItemCollection = value;
                OnPropertyChanged();
            }
        }

        private string totalCases;
        public string TotalCases
        {
            get { return totalCases; }
            set
            {
                totalCases = value;
                OnPropertyChanged();
            }
        }

        private string totalPieces;
        public string TotalPieces
        {
            get { return totalPieces; }
            set
            {
                totalPieces = value;
                OnPropertyChanged();
            }
        }

        private string dnTotal;
        public string DnTotal
        {
            get { return dnTotal; }
            set
            {
                dnTotal = value;
                OnPropertyChanged();
            }
        }

    }

    public class DnItem : ViewModelBase
    {
        public DnItem()
        {

        }

        private string itemDescription;
        public string ItemDescription
        {
            get { return itemDescription; }
            set
            {
                itemDescription = value;
                OnPropertyChanged();
            }
        }

        private string itemNumber;
        public string ItemNumber
        {
            get { return itemNumber; }
            set
            {
                itemNumber = value;
                OnPropertyChanged();
            }
        }

        private string priceType;
        public string PriceType
        {
            get { return priceType; }
            set
            {
                priceType = value;
                OnPropertyChanged();
            }
        }

        private string cases;
        public string Cases
        {
            get { return cases; }
            set
            {
                cases = value;
                OnPropertyChanged();
            }
        }

        private string pieces;
        public string Pieces
        {
            get { return pieces; }
            set
            {
                pieces = value;
                OnPropertyChanged();
            }
        }

        private string unitPrice;
        public string UnitPrice
        {
            get { return unitPrice; }
            set
            {
                unitPrice = value;
                OnPropertyChanged();
            }
        }

        private string pricePerPiece;
        public string PricePerPiece
        {
            get { return pricePerPiece; }
            set
            {
                pricePerPiece = value;
                OnPropertyChanged();
            }
        }

        private string amount;
        public string Amount
        {
            get { return amount; }
            set
            {
                amount = value;
                OnPropertyChanged();
            }
        }

    }
}
