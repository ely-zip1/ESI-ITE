using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ESI_ITE.Model;
using ESI_ITE.View;
using ESI_ITE.View.CNDN;
using ESI_ITE.ViewModel.Command;

namespace ESI_ITE.ViewModel.CNDN
{
    public class CreditNoteEntryViewModel : ViewModelBase
    {
        public CreditNoteEntryViewModel()
        {
            MyGlobals.CreditNoteEntryVM = this;
            printEntriesCommand = new DelegateCommand(StartPrinting);
            lineItemsCommand = new DelegateCommand(LineItems);
            loadCustomerCommand = new DelegateCommand(loadCustomer);
            deleteEntryCommand = new DelegateCommand(DeleteEntries);
            showCustomerCommand = new DelegateCommand(ShowCustomerList);
            exitCommand = new DelegateCommand(Exit);

            Load();
        }

        #region Properties

        private ObservableCollection<List<string>> cnNumberCollection = new ObservableCollection<List<string>>();
        public ObservableCollection<List<string>> CnNumberCollection
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

        private ObservableCollection<ReasonsModel> reasonCodeCollection = new ObservableCollection<ReasonsModel>();
        public ObservableCollection<ReasonsModel> ReasonCodeCollection
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

        private ObservableCollection<PriceTypeModel> priceTypeCollection = new ObservableCollection<PriceTypeModel>();
        public ObservableCollection<PriceTypeModel> PriceTypeCollection
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

        private ObservableCollection<TermModel> termCodeCollection = new ObservableCollection<TermModel>();
        public ObservableCollection<TermModel> TermCodeCollection
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

        private ObservableCollection<SalesmanModel> salesmanCollection = new ObservableCollection<SalesmanModel>();
        public ObservableCollection<SalesmanModel> SalesmanCollection
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

        private ObservableCollection<WareHouseModel> warehouseCollection = new ObservableCollection<WareHouseModel>();
        public ObservableCollection<WareHouseModel> WarehouseCollection
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

        private ObservableCollection<ReturnTypeModel> returnTypeCollection = new ObservableCollection<ReturnTypeModel>();
        public ObservableCollection<ReturnTypeModel> ReturnTypeCollection
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

        private ObservableCollection<List<string>> customerCollection = new ObservableCollection<List<string>>();
        public ObservableCollection<List<string>> CustomerCollection
        {
            get
            {
                return customerCollection;
            }
            set
            {
                customerCollection = value;
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

        private List<string> selectedCnNumber = new List<string>();
        public List<string> SelectedCnNumber
        {
            get
            {
                return selectedCnNumber;
            }
            set
            {
                selectedCnNumber = value;
                OnPropertyChanged();
                SelectedCnNumberChanged();
            }
        }

        private int selectedIndexCnNumber = -1;
        public int SelectedIndexCnNumber
        {
            get
            {
                return selectedIndexCnNumber;
            }
            set
            {
                selectedIndexCnNumber = value;
                OnPropertyChanged();
            }
        }

        private ReasonsModel selectedReasonCode = new ReasonsModel();
        public ReasonsModel SelectedReasonCode
        {
            get
            {
                return selectedReasonCode;
            }
            set
            {
                selectedReasonCode = value;
                OnPropertyChanged();
            }
        }

        private int selectedIndexReasonCode = -1;
        public int SelectedIndexReasonCode
        {
            get
            {
                return selectedIndexReasonCode;
            }
            set
            {
                selectedIndexReasonCode = value;
                OnPropertyChanged();
            }
        }

        private PriceTypeModel selectedPriceType = new PriceTypeModel();
        public PriceTypeModel SelectedPriceType
        {
            get
            {
                return selectedPriceType;
            }
            set
            {
                selectedPriceType = value;
                OnPropertyChanged();
            }
        }

        private int selectedIndexPriceType = -1;
        public int SelectedIndexPriceType
        {
            get
            {
                return selectedIndexPriceType;
            }
            set
            {
                selectedIndexPriceType = value;
                OnPropertyChanged();
            }
        }

        private TermModel selectedTermCode = new TermModel();
        public TermModel SelectedTermCode
        {
            get
            {
                return selectedTermCode;
            }
            set
            {
                selectedTermCode = value;
                OnPropertyChanged();
            }
        }

        private int selectedIndexTermCode = -1;
        public int SelectedIndexTermCode
        {
            get
            {
                return selectedIndexTermCode;
            }
            set
            {
                selectedIndexTermCode = value;
                OnPropertyChanged();
            }
        }

        private SalesmanModel selectedSalesman = new SalesmanModel();
        public SalesmanModel SelectedSalesman
        {
            get
            {
                return selectedSalesman;
            }
            set
            {
                selectedSalesman = value;
                OnPropertyChanged();
            }
        }

        private int selectedIndexSalesman = -1;
        public int SelectedIndexSalesman
        {
            get
            {
                return selectedIndexSalesman;
            }
            set
            {
                selectedIndexSalesman = value;
                OnPropertyChanged();
            }
        }

        private WareHouseModel selectedWarehouse = new WareHouseModel();
        public WareHouseModel SelectedWarehouse
        {
            get
            {
                return selectedWarehouse;
            }
            set
            {
                selectedWarehouse = value;
                OnPropertyChanged();
            }
        }

        private int selectedIndexWarehouse = -1;
        public int SelectedIndexWarehouse
        {
            get
            {
                return selectedIndexWarehouse;
            }
            set
            {
                selectedIndexWarehouse = value;
                OnPropertyChanged();
            }
        }

        private ReturnTypeModel selectedReturnType = new ReturnTypeModel();
        public ReturnTypeModel SelectedReturnType
        {
            get
            {
                return selectedReturnType;
            }
            set
            {
                selectedReturnType = value;
                OnPropertyChanged();
            }
        }

        private int selectedIndexReturnType = -1;
        public int SelectedIndexReturnType
        {
            get
            {
                return selectedIndexReturnType;
            }
            set
            {
                selectedIndexReturnType = value;
                OnPropertyChanged();
            }
        }

        private List<string> selectedCustomer = new List<string>();
        public List<string> SelectedCustomer
        {
            get
            {
                return selectedCustomer;
            }
            set
            {
                selectedCustomer = value;
                OnPropertyChanged();
            }
        }

        private int selectedIndexCustomer = -1;
        public int SelectedIndexCustomer
        {
            get
            {
                return selectedIndexCustomer;
            }
            set
            {
                selectedIndexCustomer = value;
                OnPropertyChanged();
            }
        }

        private bool isCustomerListWindowOpen = false;
        public bool IsCustomerListWindowOpen
        {
            get
            {
                return isCustomerListWindowOpen;
            }
            set
            {
                isCustomerListWindowOpen = value;
                OnPropertyChanged();
            }
        }


        private bool IsFirstLoad = true;

        #region

        private DelegateCommand printEntriesCommand;
        public ICommand PrintEntriesCommand
        {
            get
            {
                return printEntriesCommand;
            }
        }

        private DelegateCommand lineItemsCommand;
        public ICommand LineItemsCommand
        {
            get
            {
                return lineItemsCommand;
            }
        }

        private DelegateCommand loadCustomerCommand;
        public ICommand LoadCustomerCommand
        {
            get
            {
                return loadCustomerCommand;
            }
        }

        private DelegateCommand deleteEntryCommand;
        public ICommand DeleteEntryCommand
        {
            get
            {
                return deleteEntryCommand;
            }
        }

        private DelegateCommand showCustomerCommand;
        public ICommand ShowCustomerCommand
        {
            get
            {
                return showCustomerCommand;
            }
        }

        private DelegateCommand exitCommand;
        public ICommand ExitCommand
        {
            get
            {
                return exitCommand;
            }
        }
        #endregion
        #endregion



        private void Load()
        {
            if (IsFirstLoad)
            {
                var cnNumberObject = new CnNumberModel();
                var cnObject = new CreditNoteHeaderModel();
                var reasonsObject = new ReasonsModel();
                var pricetypeObject = new PriceTypeModel();
                var termsObject = new TermModel();
                var salesmanObject = new SalesmanModel();
                var warehouseObject = new WareHouseModel();
                var returnTypeObject = new ReturnTypeModel();
                var customerObject = new CustomerModel();

                var results = new List<CloneableDictionary<string, string>>();

                cnNumberObject = cnNumberObject.FetchLatest();

                var cnNumberList = new List<string>();
                cnNumberList.Add(cnNumberObject.CreditNoteNumber);
                cnNumberList.Add("New");

                CnNumberCollection.Add(cnNumberList);

                foreach (CreditNoteHeaderModel row in cnObject.FetchAll())
                {
                    var credit = new List<string>();
                    credit.Add(row.CnNumber);
                    credit.Add(row.CustomerId.ToString());

                    CnNumberCollection.Add(credit);
                }

                foreach (ReasonsModel row in reasonsObject.FetchAll())
                {
                    ReasonCodeCollection.Add(row);
                }

                foreach (PriceTypeModel row in pricetypeObject.FetchAll())
                {
                    PriceTypeCollection.Add(row);
                }

                foreach (TermModel row in termsObject.FetchAll())
                {
                    TermCodeCollection.Add(row);
                }

                foreach (SalesmanModel row in salesmanObject.FetchAll())
                {
                    SalesmanCollection.Add(row);
                }

                foreach (WareHouseModel row in warehouseObject.FetchAll())
                {
                    WarehouseCollection.Add(row);
                }

                foreach (ReturnTypeModel row in returnTypeObject.FetchAll())
                {
                    ReturnTypeCollection.Add(row);
                }

                foreach (CustomerModel row in customerObject.FetchAll())
                {
                    var customer = new List<string>();
                    customer.Add(row.CustomerNumber);
                    customer.Add(row.CustomerName);

                    CustomerCollection.Add(customer);
                }

                CnDate = DateTime.UtcNow;

                SelectedPrice = MyGlobals.SelectedCNDNPrice;

                IsFirstLoad = false;
            }
        }

        private void Exit()
        {
            MyGlobals.CnDnVM.SelectedPage = new CnDnEntryOptionsView();
        }

        private void ShowCustomerList()
        {
            if (IsCustomerListWindowOpen)
            {
                IsCustomerListWindowOpen = false;
                //MyGlobals.MainWindow.IsChildWindowOpen = false;
            }
            else
            {
                IsCustomerListWindowOpen = true;
                //MyGlobals.MainWindow.ChildWindowContent = new CustomerSelectionView();
                //MyGlobals.MainWindow.IsChildWindowOpen = true;
            }
        }

        private void loadCustomer()
        {
            if (SelectedCustomer != null)
            {
                searchedCustomerChanged(SelectedCustomer[0], "code");
                CustomerNumber = SelectedCustomer[0];
                ShowCustomerList();
            }
        }

        private void searchedCustomerChanged(string customerId, string type)
        {
            var customerObject = new CustomerModel();
            if (type == "code")
            {
                customerObject = (CustomerModel)customerObject.Fetch(customerId, type);
            }
            else if (type == "id")
            {
                customerObject = (CustomerModel)customerObject.Fetch(customerId, type);
            }

            var districtObject = new DistrictModel();
            districtObject = (DistrictModel)districtObject.Fetch(customerObject.DistrictId.ToString(), "id");

            var indexCounter = 0;
            foreach (var row in PriceTypeCollection)
            {
                if (customerObject.PricetypeId == row.PriceTypeId)
                {
                    SelectedIndexPriceType = indexCounter;
                    break;
                }
                indexCounter++;
            }

            indexCounter = 0;
            foreach (var row in TermCodeCollection)
            {
                if (customerObject.TermId == row.TermId)
                {
                    SelectedIndexTermCode = indexCounter;
                    break;
                }
                indexCounter++;
            }

            indexCounter = 0;
            foreach (var row in SalesmanCollection)
            {
                if (districtObject.Salesman == row.SalesmanId)
                {
                    SelectedIndexSalesman = indexCounter;
                    break;
                }
                indexCounter++;
            }

            indexCounter = 0;
            foreach (var row in WarehouseCollection)
            {
                if (customerObject.TermId == row.Id)
                {
                    SelectedIndexWarehouse = indexCounter;
                    break;
                }
                indexCounter++;
            }

            indexCounter = 0;
            foreach (var row in ReturnTypeCollection)
            {
                if (customerObject.TermId == row.Id)
                {
                    SelectedIndexReturnType = indexCounter;
                    break;
                }
                indexCounter++;
            }

            indexCounter = 0;
            TaxRate = customerObject.TaxRate.ToString();

        }

        private void SelectedCnNumberChanged()
        {
            if (SelectedIndexCnNumber == 0)
            {
                ReferenceNumber = "";
                CnDate = DateTime.Now;
                CustomerNumber = "";
                SelectedIndexReasonCode = -1;
                SelectedIndexPriceType = -1;
                SelectedIndexTermCode = -1;
                SelectedIndexSalesman = -1;
                SelectedIndexWarehouse = -1;
                SelectedIndexReturnType = -1;
                TaxRate = "";
                Comment = "";

                LastCreditNote = SelectedCnNumber[0];
                CnAmount = "";
                Discount = "";
                TotalCases = "";
                TotalPieces = "";
            }
            else if (SelectedIndexCnNumber > 0)
            {
                var cnHeaderObject = new CreditNoteHeaderModel();
                cnHeaderObject = (CreditNoteHeaderModel)cnHeaderObject.Fetch(SelectedCnNumber[0], "code");

                ReferenceNumber = cnHeaderObject.ReferenceNumber;
                CnDate = cnHeaderObject.CnDate;

                var index = 0;
                foreach (var row in ReasonCodeCollection)
                {
                    if (row.Id == cnHeaderObject.ReasonId)
                    {
                        SelectedIndexReasonCode = index;
                        break;
                    }
                    index++;
                }

                searchedCustomerChanged(cnHeaderObject.CustomerId.ToString(), "id");

                Comment = cnHeaderObject.Comment;

                LastCreditNote = SelectedCnNumber[0];
                CnAmount = cnHeaderObject.CnAmount.ToString();
                Discount = (cnHeaderObject.CnAmount * (int.Parse(TaxRate) / 100)).ToString();
                TotalCases = cnHeaderObject.TotalCases.ToString();
                TotalPieces = cnHeaderObject.TotalPieces.ToString();
            }
        }

        private void DeleteEntries()
        {

        }

        private void LineItems()
        {
            if (SelectedCnNumber[1] == "New")
            {
                var cnHeaderObj = new CreditNoteHeaderModel();
                var customerObj = new CustomerModel();

                customerObj = (CustomerModel)customerObj.Fetch(SelectedCustomer[0], "code");

                cnHeaderObj.CnNumber = SelectedCnNumber[0];
                cnHeaderObj.CnDate = CnDate;
                cnHeaderObj.ReferenceNumber = ReferenceNumber;
                cnHeaderObj.CustomerId = customerObj.CustomerId;
                cnHeaderObj.WarehouseId = SelectedWarehouse.Id;
                cnHeaderObj.Comment = Comment;
                cnHeaderObj.CnAmount = decimal.Parse(CnAmount);
                cnHeaderObj.TotalCases = int.Parse(TotalCases);
                cnHeaderObj.TotalPieces = int.Parse(TotalPieces);
                cnHeaderObj.IsPrinted = false;
                cnHeaderObj.ReturnCodeId = SelectedReturnType.Id;
                cnHeaderObj.PriceUsed = SelectedPrice;
                cnHeaderObj.UserId = MyGlobals.LoggedUser.Id;

                cnHeaderObj.AddNew(cnHeaderObj);
            }
            MyGlobals.SelectedCNDNTransaction = SelectedCnNumber[0];
            MyGlobals.CnDnVM.SelectedPage = new CNLineItemView();
        }

        private void StartPrinting()
        {

        }
    }
}