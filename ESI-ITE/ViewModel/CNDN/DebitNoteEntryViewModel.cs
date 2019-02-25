using ESI_ITE.Model;
using ESI_ITE.View.CNDN;
using ESI_ITE.ViewModel.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ESI_ITE.ViewModel.CNDN
{
    public class DebitNoteEntryViewModel:ViewModelBase
    {
        #region Properties

        private ObservableCollection<List<string>> dnNumberCollection = new ObservableCollection<List<string>>();
        public ObservableCollection<List<string>> DnNumberCollection
        {
            get
            {
                return dnNumberCollection;
            }
            set
            {
                dnNumberCollection = value;
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

        private DateTime dnDate;
        public DateTime DnDate
        {
            get
            {
                return dnDate;
            }
            set
            {
                dnDate = value;
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

        private string dnAmount;
        public string DnAmount
        {
            get
            {
                return dnAmount;
            }
            set
            {
                dnAmount = value;
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

        private List<string> selectedDnNumber = new List<string>();
        public List<string> SelectedDnNumber
        {
            get
            {
                return selectedDnNumber;
            }
            set
            {
                selectedDnNumber = value;
                OnPropertyChanged();
                SelectedDnNumberChanged();
            }
        }

        private int selectedIndexDnNumber = -1;
        public int SelectedIndexDnNumber
        {
            get
            {
                return selectedIndexDnNumber;
            }
            set
            {
                selectedIndexDnNumber = value;
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
                var reasonsObject = new ReasonsModel();
                var pricetypeObject = new PriceTypeModel();
                var termsObject = new TermModel();
                var salesmanObject = new SalesmanModel();
                var warehouseObject = new WareHouseModel();
                var returnTypeObject = new ReturnTypeModel();
                var customerObject = new CustomerModel();

                var results = new List<CloneableDictionary<string, string>>();

                LoadDnNumbers();

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

                DnDate = DateTime.UtcNow;

                SelectedPrice = MyGlobals.SelectedCNDNPrice;

                IsFirstLoad = false;
            }
        }

        private void LoadDnNumbers()
        {
            DnNumberCollection.Clear();

            var dnObject = new CreditNoteHeaderModel();
            var cnNumberObject = new CnNumberModel();
            cnNumberObject = cnNumberObject.FetchLatest();

            var cnNumberList = new List<string>();
            cnNumberList.Add(cnNumberObject.CreditNoteNumber);
            cnNumberList.Add("New");

            DnNumberCollection.Add(cnNumberList);

            foreach (CreditNoteHeaderModel row in dnObject.FetchAll())
            {
                var credit = new List<string>();
                credit.Add(row.CnNumber);
                credit.Add(row.CustomerId.ToString());

                DnNumberCollection.Add(credit);
            }

            SelectedIndexDnNumber = -1;
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
                CustomerName = SelectedCustomer[1];
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
            TaxRate = customerObject.TaxRate.ToString();

        }

        private void SelectedDnNumberChanged()
        {
            if (SelectedIndexDnNumber <= 0)
            {
                ClearFields();
            }
            else if (SelectedIndexDnNumber > 0)
            {
                var cnHeaderObject = new CreditNoteHeaderModel();
                cnHeaderObject = (CreditNoteHeaderModel)cnHeaderObject.Fetch(SelectedDnNumber[0], "code");

                ReferenceNumber = cnHeaderObject.ReferenceNumber;
                DnDate = cnHeaderObject.CnDate;

                var customerObject = new CustomerModel();
                customerObject = (CustomerModel)customerObject.Fetch(cnHeaderObject.CustomerId.ToString(), "id");
                CustomerNumber = customerObject.CustomerNumber;
                CustomerName = customerObject.CustomerName;//.ToUpper();

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

                index = 0;
                foreach (var row in WarehouseCollection)
                {
                    if (row.Id == cnHeaderObject.WarehouseId)
                    {
                        SelectedIndexWarehouse = index;
                        break;
                    }
                    index++;
                }

                index = 0;
                foreach (var row in ReturnTypeCollection)
                {
                    if (row.Id == cnHeaderObject.ReturnCodeId)
                    {
                        SelectedIndexReturnType = index;
                        break;
                    }
                    index++;
                }

                searchedCustomerChanged(cnHeaderObject.CustomerId.ToString(), "id");

                Comment = cnHeaderObject.Comment;

                LastCreditNote = SelectedDnNumber[0];
                DnAmount = cnHeaderObject.CnAmount.ToString();
                Discount = (cnHeaderObject.CnAmount * (int.Parse(TaxRate) / 100)).ToString();
                TotalCases = cnHeaderObject.TotalCases.ToString();
                TotalPieces = cnHeaderObject.TotalPieces.ToString();
            }
        }

        private void DeleteEntries()
        {
            var dialogResult = MessageBox.Show("Do you want to delete the current CN entry?", "Confirm Delete", MessageBoxButton.OKCancel);
            if (dialogResult == MessageBoxResult.OK)
            {
                if (SelectedIndexDnNumber > 0)
                {
                    var cnHeaderObj = new CreditNoteHeaderModel();
                    cnHeaderObj = (CreditNoteHeaderModel)cnHeaderObj.Fetch(SelectedDnNumber[0], "code");

                    cnHeaderObj.DeleteItem(cnHeaderObj);

                    LoadDnNumbers();
                    ClearFields();
                }
            }
        }

        private void LineItems()
        {
            if (SelectedIndexDnNumber == -1)
            {
                return;
            }
            else if (SelectedDnNumber[1] == "New")
            {
                var cnHeaderObj = new CreditNoteHeaderModel();
                var customerObj = new CustomerModel();

                customerObj = (CustomerModel)customerObj.Fetch(SelectedCustomer[0], "code");

                cnHeaderObj.CnNumber = SelectedDnNumber[0];
                cnHeaderObj.CnDate = DnDate;
                cnHeaderObj.ReferenceNumber = ReferenceNumber;
                cnHeaderObj.CustomerId = customerObj.CustomerId;
                cnHeaderObj.WarehouseId = SelectedWarehouse.Id;
                cnHeaderObj.Comment = Comment;
                cnHeaderObj.CnAmount = decimal.Parse(DnAmount);
                cnHeaderObj.TotalCases = int.Parse(TotalCases);
                cnHeaderObj.TotalPieces = int.Parse(TotalPieces);
                cnHeaderObj.IsPrinted = false;
                cnHeaderObj.ReturnCodeId = SelectedReturnType.Id;
                cnHeaderObj.PriceUsed = SelectedPrice;
                cnHeaderObj.UserId = MyGlobals.LoggedUser.Id;
                cnHeaderObj.ReasonId = SelectedReasonCode.Id;

                cnHeaderObj.AddNew(cnHeaderObj);
            }

            var cnNumberObject = new CnNumberModel();
            cnNumberObject = cnNumberObject.FetchLatest();
            cnNumberObject.UpdateCnNumber(cnNumberObject);

            if (string.IsNullOrWhiteSpace(SelectedDnNumber[1]) == false)
            {
                MyGlobals.SelectedCNDNTransaction = SelectedDnNumber[0];
                MyGlobals.CnDnVM.SelectedPage = new CNLineItemView();
            }
        }

        public void ClearFields()
        {
            ReferenceNumber = "";
            DnDate = DateTime.Now;
            CustomerNumber = "";
            CustomerName = "";
            SelectedIndexReasonCode = -1;
            SelectedIndexPriceType = -1;
            SelectedIndexTermCode = -1;
            SelectedIndexSalesman = -1;
            SelectedIndexWarehouse = -1;
            SelectedIndexReturnType = -1;
            TaxRate = "";
            Comment = "";

            DnAmount = "0";
            Discount = "0";
            TotalCases = "0";
            TotalPieces = "0";
        }
    }
}
