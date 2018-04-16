using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESI_ITE.ViewModel.CNDN
{
    public class CreditNoteEntryViewModel : ViewModelBase
    {
        #region Properties

        private ObservableCollection<string> cnNumberCollection;
        public ObservableCollection<string> CnNumberCollection
        {
            get
            {
                return cnNumberCollection;
            }
            set
            {
                cnNumberCollection = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<List<string>> reasonCodeCollection;
        public ObservableCollection<List<string>> ReasonCodeCollection
        {
            get
            {
                return reasonCodeCollection;
            }
            set
            {
                reasonCodeCollection = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<List<string>> priceTypeCollection;
        public ObservableCollection<List<string>> PriceTypeCollection
        {
            get
            {
                return priceTypeCollection;
            }
            set
            {
                priceTypeCollection = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<List<string>> termCodeCollection;
        public ObservableCollection<List<string>> TermCodeCollection
        {
            get
            {
                return termCodeCollection;
            }
            set
            {
                termCodeCollection = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<List<string>> salesmanCollection;
        public ObservableCollection<List<string>> SalesmanCollection
        {
            get
            {
                return salesmanCollection;
            }
            set
            {
                salesmanCollection = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<List<string>> warehouseCollection;
        public ObservableCollection<List<string>> WarehouseCollection
        {
            get
            {
                return warehouseCollection;
            }
            set
            {
                warehouseCollection = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<List<string>> returnTypeCollection;
        public ObservableCollection<List<string>> ReturnTypeCollection
        {
            get
            {
                return returnTypeCollection;
            }
            set
            {
                returnTypeCollection = value;
                OnPropertyChanged();
            }
        }

        private string referenceNumber;
        public string ReferenceNumber
        {
            get
            {
                return referenceNumber;
            }
            set
            {
                referenceNumber = value;
                OnPropertyChanged();
            }
        }

        private DateTime cnDate;
        public DateTime CnDate
        {
            get
            {
                return cnDate;
            }
            set
            {
                cnDate = value;
                OnPropertyChanged();
            }
        }

        private string customerNumber;
        public string CustomerNumber
        {
            get
            {
                return customerNumber;
            }
            set
            {
                customerNumber = value;
                OnPropertyChanged();
            }
        }

        private string taxRate;
        public string TaxRate
        {
            get
            {
                return taxRate;
            }
            set
            {
                taxRate = value;
                OnPropertyChanged();
            }
        }

        private string comment;
        public string Comment
        {
            get
            {
                return comment;
            }
            set
            {
                comment = value;
                OnPropertyChanged();
            }
        }

        private string selectedPrice;
        public string SelectedPrice
        {
            get
            {
                return selectedPrice;
            }
            set
            {
                selectedPrice = value;
                OnPropertyChanged();
            }
        }

        private string lastCreditNote;
        public string LastCreditNote
        {
            get
            {
                return lastCreditNote;
            }
            set
            {
                lastCreditNote = value;
                OnPropertyChanged();
            }
        }

        private string cnAmount;
        public string CnAmount
        {
            get
            {
                return cnAmount;
            }
            set
            {
                cnAmount = value;
            }
        }

        private string discount;
        public string Discount
        {
            get
            {
                return discount;
            }
            set
            {
                discount = value;
                OnPropertyChanged();
            }
        }

        private string totalCases;
        public string TotalCases
        {
            get
            {
                return totalCases;
            }
            set
            {
                totalCases = value;
                OnPropertyChanged();
            }
        }

        private string totalPieces;
        public string TotalPieces
        {
            get
            {
                return totalPieces;
            }
            set
            {
                totalPieces = value;
                OnPropertyChanged();
            }
        }

        #endregion
    }
}
