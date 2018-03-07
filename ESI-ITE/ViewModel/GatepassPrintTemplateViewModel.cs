using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESI_ITE.ViewModel
{
    public class GatepassPrintTemplateViewModel : ViewModelBase
    {
        #region Properties

        private string username;
        public string Username
        {
            get
            {
                return username;
            }
            set
            {
                username = value;
                OnPropertyChanged();
            }
        }

        private string gatepassDate;
        public string GatePassDate
        {
            get
            {
                return gatepassDate;
            }
            set
            {
                gatepassDate = value;
                OnPropertyChanged();
            }
        }

        private string printTime;
        public string PrintTime
        {
            get
            {
                return printTime;
            }
            set
            {
                printTime = value;
                OnPropertyChanged();
            }
        }

        private string gatePassNumber;
        public string GatepassNumber
        {
            get
            {
                return gatePassNumber;
            }
            set
            {
                gatePassNumber = value;
                OnPropertyChanged();
            }
        }

        private string picklistNumber;
        public string PicklistNumber
        {
            get
            {
                return picklistNumber;
            }
            set
            {
                picklistNumber = value;
                OnPropertyChanged();
            }
        }

        private string pageNumber;
        public string PageNumber
        {
            get
            {
                return pageNumber;
            }
            set
            {
                pageNumber = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<GatepassItem> itemCollection = new ObservableCollection<GatepassItem>();
        public ObservableCollection<GatepassItem> ItemCollection
        {
            get
            {
                return itemCollection;
            }
            set
            {
                itemCollection = value;
                OnPropertyChanged();
            }
        }

        private int totalCases;
        public int TotalCases
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

        private int totalPieces;
        public int TotalPieces
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

        private decimal totalWeight;
        public decimal TotalWeight
        {
            get
            {
                return totalWeight;
            }
            set
            {
                totalWeight = value;
                OnPropertyChanged();
            }
        }

        private decimal totalValue;
        public decimal TotalValue
        {
            get
            {
                return totalValue;
            }
            set
            {
                totalValue = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<string> attachedInvoicesList = new ObservableCollection<string>();
        public ObservableCollection<string> AttachedInvoicesList
        {
            get
            {
                return attachedInvoicesList;
            }
            set
            {
                attachedInvoicesList = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<string> attachedCustomerList = new ObservableCollection<string>();
        public ObservableCollection<string> AttachedCustomerList
        {
            get
            {
                return attachedCustomerList;
            }
            set
            {
                attachedCustomerList = value;
                OnPropertyChanged();
            }
        }

        private bool isFooterVisible = true;
        public bool IsFooterVisible
        {
            get
            {
                return isFooterVisible;
            }
            set
            {
                isFooterVisible = value;
                OnPropertyChanged();
            }
        }


        #endregion
    }

    public class GatepassItem
    {
        private string itemcode;
        public string Itemcode
        {
            get
            {
                return itemcode;
            }
            set
            {
                itemcode = value;
            }
        }

        private string description;
        public string Description
        {
            get
            {
                return description;
            }
            set
            {
                description = value;
            }
        }

        private string location;
        public string Location
        {
            get
            {
                return location;
            }
            set
            {
                location = value;
            }
        }

        private string cases;
        public string Cases
        {
            get
            {
                return cases;
            }
            set
            {
                cases = value;
            }
        }

        private string casesCode;
        public string CasesCode
        {
            get
            {
                return casesCode;
            }
            set
            {
                casesCode = value;
            }
        }
        
        private string pieces;
        public string Pieces
        {
            get
            {
                return pieces;
            }
            set
            {
                pieces = value;
            }
        }

        private string piecesCode;
        public string PiecesCode
        {
            get
            {
                return piecesCode;
            }
            set
            {
                piecesCode = value;
            }
        }

        private string expirationDate;
        public string ExpirationDate
        {
            get
            {
                return expirationDate;
            }
            set
            {
                expirationDate = value;
            }
        }

    }
}
