using ESI_ITE.Model;
using ESI_ITE.View;
using ESI_ITE.ViewModel.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Windows.Input;

namespace ESI_ITE.ViewModel
{
    class TransactionEntryPageViewModel : ViewModelBase, IDataErrorInfo
    {
        #region Constructor

        public TransactionEntryPageViewModel()
        {
            lineItemCommand = new DelegateCommand(LineItem);
            deleteTransactionCommand = new DelegateCommand(DeleteTransaction);
            printCommand = new DelegateCommand(Print);
            postingCommand = new DelegateCommand(Post);

            IsFirstLoad = true;
            Load();
        }

        public TransactionEntryPageViewModel(ITEViewModel container)
        {
            this.Container = container;

            lineItemCommand = new DelegateCommand(LineItem);
            deleteTransactionCommand = new DelegateCommand(DeleteTransaction);
            printCommand = new DelegateCommand(Print);

            IsFirstLoad = true;
            Load();
        }

        #endregion

        #region Properties

        ITEViewModel Container;

        #region Collection
        DateTime currentDate = DateTime.Now.Date;

        TransactionModel transaction = new TransactionModel();
        TransactionNumberModel transactionNumber = new TransactionNumberModel();
        TransactionTypesModel transactionType = new TransactionTypesModel();
        WareHouseModel warehouse = new WareHouseModel();
        LocationModel location = new LocationModel();
        ReasonsModel reason = new ReasonsModel();

        private List<TransactionTypesModel> transactionTypesList = new List<TransactionTypesModel>();
        private List<WareHouseModel> wareHouseList = new List<WareHouseModel>();
        private List<LocationModel> locationList = new List<LocationModel>();
        private List<string> priceCategoryList = new List<string>();
        private List<string> priceTypeList = new List<string>();
        private List<ReasonsModel> reasonList = new List<ReasonsModel>();

        public List<TransactionTypesModel> TransactionTypesList
        {
            get { return transactionTypesList; }
            set { transactionTypesList = value; }
        }
        public List<WareHouseModel> WareHouseList
        {
            get { return wareHouseList; }
            set { wareHouseList = value; }
        }
        public List<LocationModel> LocationList
        {
            get { return locationList; }
            set { locationList = value; }
        }
        public List<string> PriceCategoryList
        {
            get { return priceCategoryList; }
            set { priceCategoryList = value; }
        }
        public List<string> PriceTypeList
        {
            get { return priceTypeList; }
            set { priceTypeList = value; }
        }
        public List<ReasonsModel> ReasonList
        {
            get { return reasonList; }
            set { reasonList = value; }
        }

        private ObservableCollection<string> cmbTransactionNumbers = new ObservableCollection<string>();
        public ObservableCollection<string> CmbTransactionNumbers
        {
            get { return cmbTransactionNumbers; }
            set { cmbTransactionNumbers = value; }
        }

        private ObservableCollection<TransactionTypesModel> cmbTransactionType = new ObservableCollection<TransactionTypesModel>();
        public ObservableCollection<TransactionTypesModel> CmbTransactionType
        {
            get { return cmbTransactionType; }
            set { cmbTransactionType = value; }
        }

        private ObservableCollection<WareHouseModel> cmbWarehouse = new ObservableCollection<WareHouseModel>();
        public ObservableCollection<WareHouseModel> CmbWarehouse
        {
            get { return cmbWarehouse; }
            set { cmbWarehouse = value; }
        }

        private ObservableCollection<LocationModel> cmbLocation = new ObservableCollection<LocationModel>();
        public ObservableCollection<LocationModel> CmbLocation
        {
            get { return cmbLocation; }
            set { cmbLocation = value; }
        }

        private ObservableCollection<ReasonsModel> cmbReason = new ObservableCollection<ReasonsModel>();
        public ObservableCollection<ReasonsModel> CmbReason
        {
            get { return cmbReason; }
            set
            {
                cmbReason = value;
                OnPropertyChanged("CmbReason");
            }
        }
        #endregion

        #region Properties for Binding

        private string selectedTransactionNumber;
        public string SelectedTransactionNumber
        {
            get { return selectedTransactionNumber; }
            set
            {
                if (selectedTransactionNumber != value && value != null)
                {
                    selectedTransactionNumber = value;
                    strTransactionNumber = value;
                    OnPropertyChanged("SelectedTransactionNumber");
                    if (!string.IsNullOrEmpty(selectedTransactionNumber))
                    {
                        SelectionChanged("SelectedTransactionNumber");
                    }
                }
            }
        }

        private TransactionTypesModel selectedTransactionType;
        public TransactionTypesModel SelectedTransactionType
        {
            get { return selectedTransactionType; }
            set
            {
                if (selectedTransactionType != value && value != null)
                {
                    selectedTransactionType = value;
                    validateReason = false;
                    OnPropertyChanged("SelectedTransactionType");
                    SelectionChanged("SelectedTransactionType");
                }
            }
        }

        private string documentNumber;
        public string DocumentNumber
        {
            get { return documentNumber; }
            set
            {
                if (documentNumber != value)
                {
                    documentNumber = value;
                    strDocumentNumber = value;
                    OnPropertyChanged("DocumentNumber");
                    SelectionChanged("DocumentNumber");
                }
            }
        }

        private string transactionDate;
        public string TransactionDate
        {
            get { return transactionDate; }
            set
            {
                transactionDate = value;
                OnPropertyChanged("TransactionDate");
                SelectionChanged("TransactionDate");
            }
        }

        private WareHouseModel selectedSourceWarehouse;
        public WareHouseModel SelectedSourceWarehouse
        {
            get { return selectedSourceWarehouse; }
            set
            {
                if (selectedSourceWarehouse != value && value != null)
                {
                    selectedSourceWarehouse = value;
                    strSourceWarehouse[0] = value.Code;
                    strSourceWarehouse[1] = value.Name;
                    OnPropertyChanged("SelectedSourceWarehouse");
                    SelectionChanged("SelectedSourceWarehouse");
                }
            }
        }

        private LocationModel selectedSourceLocation;
        public LocationModel SelectedSourceLocation
        {
            get { return selectedSourceLocation; }
            set
            {
                if (selectedSourceLocation != value && value != null)
                {
                    selectedSourceLocation = value;
                    strSourceLocation[0] = value.Code;
                    strSourceLocation[1] = value.Location;
                    OnPropertyChanged("SelectedSourceLocation");
                    SelectionChanged("SelectedSourceLocation");
                }
            }
        }

        private string selectedPriceCategory;
        public string SelectedPriceCategory
        {
            get { return selectedPriceCategory; }
            set
            {
                if (!string.IsNullOrEmpty(value) || selectedPriceCategory != value)
                {
                    selectedPriceCategory = value;
                    strPriceCategory = value;
                    OnPropertyChanged("SelectedPriceCategory");
                    SelectionChanged("SelectedPriceCategory");
                }
            }
        }

        private string selectedPriceType;
        public string SelectedPriceType
        {
            get { return selectedPriceType; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    selectedPriceType = value;
                    strPriceType = value;
                    OnPropertyChanged("SelectedPriceType");
                    SelectionChanged("SelectedPriceType");
                }
            }
        }

        private WareHouseModel selectedDestinationWarehouse;
        public WareHouseModel SelectedDestinationWarehouse
        {
            get { return selectedDestinationWarehouse; }
            set
            {
                if (selectedDestinationWarehouse != value && value != null)
                {
                    selectedDestinationWarehouse = value;
                    strDestinationWarehouse[0] = value.Code;
                    strDestinationWarehouse[1] = value.Name;
                    OnPropertyChanged("SelectedDestinationWarehouse");
                    SelectionChanged("SelectedDestinationWarehouse");
                }
            }
        }

        private LocationModel selectedDestinationLocation;
        public LocationModel SelectedDestinationLocation
        {
            get { return selectedDestinationLocation; }
            set
            {
                if (selectedDestinationLocation != value && value != null)
                {
                    selectedDestinationLocation = value;
                    strDestinationLocation[0] = value.Code;
                    strDestinationLocation[1] = value.Location;
                    OnPropertyChanged("SelectedDestinationLocation");
                    SelectionChanged("SelectedDestinationLocation");
                }
            }
        }

        private ReasonsModel selectedReason;
        public ReasonsModel SelectedReason
        {
            get { return selectedReason; }
            set
            {
                if (selectedReason != value && value != null)
                {
                    selectedReason = value;
                    strReason[0] = value.ReasonCode;
                    strReason[1] = value.Description;
                    OnPropertyChanged("SelectedReason");
                    SelectionChanged("SelectedReason");
                }
            }
        }

        private string comment;
        public string Comment
        {
            get { return comment; }
            set
            {
                if (!string.IsNullOrEmpty(value) || comment != value)
                {
                    comment = value;
                    strComment = value;
                    OnPropertyChanged("Comment");
                }
            }
        }

        #endregion

        #region Selected Index

        private int indexTransactionNumber;
        public int IndexTransactionNumber
        {
            get { return indexTransactionNumber; }
            set
            {
                indexTransactionNumber = value;
                OnPropertyChanged("IndexTransactionNumber");
                SelectionChanged("SelectedTransactionNumber");
            }
        }

        private int indexTransactionType;
        public int IndexTransactionType
        {
            get { return indexTransactionType; }
            set
            {
                indexTransactionType = value;
                OnPropertyChanged("IndexTransactionType");
                SelectionChanged("SelectedTransactionType");
            }
        }

        private int indexSourceWarehouse;
        public int IndexSourceWarehouse
        {
            get { return indexSourceWarehouse; }
            set
            {
                indexSourceWarehouse = value;
                OnPropertyChanged("IndexSourceWarehouse");
                SelectionChanged("SelectedSourceWarehouse");
            }
        }

        private int indexSourceLocation;
        public int IndexSourceLocation
        {
            get { return indexSourceLocation; }
            set
            {
                indexSourceLocation = value;
                OnPropertyChanged("IndexSourceLocation");
                SelectionChanged("SelectedSourceLocation");
            }
        }

        private int indexPriceCategory;
        public int IndexPriceCategory
        {
            get { return indexPriceCategory; }
            set
            {
                indexPriceCategory = value;
                OnPropertyChanged("IndexPriceCategory");
                SelectionChanged("SelectedPriceCategory");
            }
        }

        private int indexPriceType;
        public int IndexPriceType
        {
            get { return indexPriceType; }
            set
            {
                indexPriceType = value;
                OnPropertyChanged("IndexPriceType");
                SelectionChanged("SelectedPriceType");
            }
        }

        private int indexDestinationWarehouse;
        public int IndexDestinationWarehouse
        {
            get { return indexDestinationWarehouse; }
            set
            {
                indexDestinationWarehouse = value;
                OnPropertyChanged("IndexDestinationWarehouse");
                SelectionChanged("SelectedDestinationWarehouse");
            }
        }

        private int indexDestinationLocation;
        public int IndexDestinationLocation
        {
            get { return indexDestinationLocation; }
            set
            {
                indexDestinationLocation = value;
                OnPropertyChanged("IndexDestinationLocation");
                SelectionChanged("SelectedDestinationLocation");
            }
        }

        private int indexReason;
        public int IndexReason
        {
            get { return indexReason; }
            set
            {
                indexReason = value;
                OnPropertyChanged("IndexReason");
                SelectionChanged("SelectedReason");
            }
        }
        #endregion

        private string strTransactionNumber = null;
        private string[] strTransactionType = new string[2];
        private string strDocumentNumber = null;
        private string strTransactionDate = null;
        private string[] strSourceWarehouse = new string[2];
        private string[] strSourceLocation = new string[2];
        private string[] strDestinationWarehouse = new string[2];
        private string[] strDestinationLocation = new string[2];
        private string strPriceCategory = null;
        private string strPriceType = null;
        private string[] strReason = new string[2];
        private string strComment = null;

        //indicates whether destination warehouse and location is enabled/disabled
        private bool enableDestination;
        public bool EnableDestination
        {
            get { return enableDestination; }
            set
            {
                enableDestination = value;
                OnPropertyChanged("EnableDestination");
            }
        }

        //
        private bool isLineable;
        public bool IsLineable
        {
            get { return isLineable; }
            set
            {
                isLineable = value;
                OnPropertyChanged("IsLineable");

            }
        }

        public bool IsFirstLoad;

        public bool validateReason = true;

        private bool updateTransNo = false;

        private string latestTransNo;

        private DelegateCommand lineItemCommand;
        public ICommand LineItemCommand
        {
            get { return lineItemCommand; }
        }

        private DelegateCommand deleteTransactionCommand;
        public ICommand DeleteTransactionCommand
        {
            get { return deleteTransactionCommand; }
        }

        private DelegateCommand printCommand;
        public ICommand PrintCommand
        {
            get { return printCommand; }
        }

        private DelegateCommand postingCommand;
        public ICommand PostingCommand
        {
            get { return postingCommand; }
        }

        private bool canEdit;
        public bool CanEdit
        {
            get { return canEdit; }
            set
            {
                canEdit = value;
                OnPropertyChanged("CanEdit");
            }
        }

        #endregion

        #region Methods

        private void Load()
        {
            //Sorts and Loads Transaction Numbers
            UpdateTransactionNumbers();

            //Loads Transaction Types
            CmbTransactionType.Add(new TransactionTypesModel());
            foreach (var type in transactionType.FetchAll())
            {
                CmbTransactionType.Add(type);
            }

            //Sets the current date as default date for new transaction
            transactionDate = currentDate.ToString("MM/dd/yyyy");

            //Loads SourceWarehouse
            CmbWarehouse.Add(new WareHouseModel());
            foreach (var wh in warehouse.FetchAll())
            {
                CmbWarehouse.Add(wh);
            }

            //Loads source Location
            CmbLocation.Add(new LocationModel());
            foreach (var loc in location.FetchAll())
            {
                CmbLocation.Add(loc);
            }

            priceCategoryList.Add("");
            priceCategoryList.Add("Selling Price");
            priceCategoryList.Add("Purchase Price");

            priceTypeList.Add("");
            priceTypeList.Add("Current");
            priceTypeList.Add("3 Months Ago");
            priceTypeList.Add("6 Months Ago");

            //Loads all transaction reasons
            ReasonList = reason.FetchAll();

            EnableDestination = false;
        }

        private void UpdateTransactionNumbers()
        {
            List<string> sortable = new List<string>();

            latestTransNo = transactionNumber.Fetch();
            MyGlobals.TransactionList.Clear();

            foreach (var obj in (transaction.FetchAll()))
            {
                MyGlobals.TransactionList.Add(obj);
                sortable.Add(obj.TransactionNumber);
            }
            sortable.Add(latestTransNo);

            sortable.Sort();

            CmbTransactionNumbers.Clear();
            foreach (var i in sortable)
            {
                CmbTransactionNumbers.Add(i);
            }

            var newTransaction = CmbTransactionNumbers[CmbTransactionNumbers.Count - 1] + " (New Transaction)";
            CmbTransactionNumbers.RemoveAt(CmbTransactionNumbers.Count - 1);
            CmbTransactionNumbers.Add(newTransaction);
        }

        private void fillForm()
        {
            foreach (var trans in transaction.FetchAll())
                if (trans.TransactionNumber == SelectedTransactionNumber)
                {
                    //transaction type
                    foreach (var transType in transactionType.FetchAll())
                        if (transType.Code == trans.TransactionTypeCode)
                        {
                            IndexTransactionType = transType.Id;
                            SelectedTransactionType = transType;
                            break;
                        }
                    //document number
                    DocumentNumber = trans.DocumentNumber;

                    //transaction date
                    TransactionDate = trans.TransactionDate.ToString("MM/dd/yyyy");

                    //warehouse
                    foreach (var wh in warehouse.FetchAll())
                    {
                        if (wh.Code == trans.SourceWarehouseCode)
                        {
                            IndexSourceWarehouse = wh.Id;
                            selectedSourceWarehouse = wh;
                            break;
                        }
                    }

                    foreach (var wh2 in warehouse.FetchAll())
                    {
                        if (wh2.Code == trans.DestinationWarehouseCode)
                        {
                            IndexDestinationWarehouse = wh2.Id;
                            SelectedDestinationWarehouse = wh2; //new WareHouseModel(wh2);
                            break;
                        }
                    }

                    //location
                    foreach (var loc in location.FetchAll())
                    {
                        if (loc.Code == trans.SourceLocationCode)
                        {
                            IndexSourceLocation = loc.Id;
                            SelectedSourceLocation = loc; //new LocationModel(loc);

                            break;
                        }
                    }

                    foreach (var loc2 in location.FetchAll())
                    {
                        if (loc2.Code == trans.DestinationLocationCode)
                        {
                            IndexDestinationLocation = loc2.Id;
                            SelectedDestinationLocation = loc2; //new LocationModel(loc2);

                            break;
                        }
                    }

                    //price category
                    switch (trans.PriceCategory)
                    {
                        case "Selling Price":
                            IndexPriceCategory = 1;
                            SelectedPriceCategory = "Selling Price";
                            break;
                        case "Purchase Price":
                            IndexPriceCategory = 2;
                            SelectedPriceCategory = "Purchase Price";
                            break;
                        default:
                            IndexPriceCategory = 0;
                            SelectedPriceCategory = "";
                            break;
                    }

                    //price type
                    switch (trans.PriceType)
                    {
                        case "Current":
                            IndexPriceType = 1;
                            SelectedPriceType = "Current";
                            break;
                        case "3 Months Ago":
                            IndexPriceType = 2;
                            SelectedPriceType = "3 Months Ago";
                            break;
                        case "6 Months Ago":
                            IndexPriceType = 3;
                            SelectedPriceType = "6 Months Ago";
                            break;
                        default:
                            IndexPriceType = 0;
                            SelectedPriceType = "";
                            break;
                    }

                    //reason code
                    foreach (var reason in reason.FetchAll())
                    {
                        if (reason.ReasonCode == trans.ReasonCode)
                        {
                            IndexReason = int.Parse(reason.ReasonCode.Substring(2));
                            SelectedReason = reason; //new ReasonsModel(reason);
                            break;
                        }
                    }

                    //comment
                    Comment = trans.Comment;

                }
        }

        private void SelectionChanged(string property)
        {
            switch (property)
            {
                case "SelectedTransactionNumber":
                    resetValidProperties();
                    if (selectedTransactionNumber.Substring(0, 6) == latestTransNo)
                    {
                        IsFirstLoad = true;
                        updateTransNo = true;

                        CanEdit = true;

                        //clear the form
                        ClearForm();
                    }
                    else
                    {
                        IsFirstLoad = false;
                        updateTransNo = false;
                        fillForm();

                        CanEdit = false;
                    }

                    if (string.IsNullOrEmpty(selectedTransactionNumber))
                    {
                        validProperties[0] = "";
                        IsLineable = false;
                    }
                    else
                        validProperties[0] = null;
                    break;

                case "SelectedTransactionType":
                    IsDestinationEnabled(SelectedTransactionType.Code);

                    CmbReason.Clear();
                    CmbReason.Add(new ReasonsModel());

                    if (SelectedTransactionType != null)
                        if (!string.IsNullOrEmpty(SelectedTransactionType.Code))
                        {
                            foreach (var item in ReasonList)
                            {
                                if (item.TransactionType == SelectedTransactionType.Code)
                                {
                                    CmbReason.Add(item);
                                }
                            }
                        }

                    if (string.IsNullOrEmpty(SelectedTransactionType.Code))
                    {
                        validProperties[1] = "";
                        IsLineable = false;
                    }
                    else
                        validProperties[1] = null;
                    break;

                case "DocumentNumber":
                    if (string.IsNullOrEmpty(DocumentNumber))
                    {
                        validProperties[2] = "";
                        IsLineable = false;
                    }
                    else
                        validProperties[2] = null;
                    break;

                case "TransactionDate":

                    if (string.IsNullOrEmpty(transactionDate))
                    {
                        validProperties[3] = "";
                        IsLineable = false;
                    }
                    else
                        validProperties[3] = null;
                    break;

                case "SelectedSourceWarehouse":

                    if (SelectedSourceWarehouse != null)
                        if (string.IsNullOrEmpty(SelectedSourceWarehouse.Code))
                        {
                            validProperties[4] = "";
                            IsLineable = false;
                        }
                        else
                            validProperties[4] = null;
                    break;

                case "SelectedSourceLocation":

                    if (SelectedSourceLocation != null)
                        if (string.IsNullOrEmpty(SelectedSourceLocation.Code))
                        {
                            validProperties[5] = "";
                            IsLineable = false;
                        }
                        else
                            validProperties[5] = null;
                    break;

                case "SelectedPriceCategory":

                    if (string.IsNullOrEmpty(SelectedPriceCategory))
                    {
                        validProperties[6] = "";
                        IsLineable = false;
                    }
                    else
                        validProperties[6] = null;
                    break;

                case "SelectedPriceType":

                    if (string.IsNullOrEmpty(SelectedPriceType))
                    {
                        validProperties[7] = "";
                        IsLineable = false;
                    }
                    else
                        validProperties[7] = null;
                    break;

                case "SelectedDestinationWarehouse":
                    if (EnableDestination)
                        if (SelectedDestinationWarehouse != null)
                            if (string.IsNullOrEmpty(SelectedDestinationWarehouse.Code))
                            {
                                validProperties[8] = "";
                                IsLineable = false;
                            }
                            else
                                validProperties[8] = null;
                    break;

                case "SelectedDestinationLocation":
                    if (EnableDestination)
                        if (SelectedDestinationLocation != null)
                            if (string.IsNullOrEmpty(SelectedDestinationLocation.Code))
                            {
                                validProperties[9] = "";
                                IsLineable = false;
                            }
                            else
                                validProperties[9] = null;
                    break;

                case "SelectedReason":

                    if (SelectedReason != null)
                        if (string.IsNullOrEmpty(SelectedReason.ReasonCode))
                        {
                            validProperties[10] = "";
                            IsLineable = false;
                        }
                        else
                            validProperties[10] = null;
                    break;

            }
            isValid();
        }

        private void ClearForm()
        {
            IndexTransactionType = 0;
            DocumentNumber = "";
            TransactionDate = DateTime.Now.ToString("MM/dd/yyyy");
            IndexSourceWarehouse = 0;
            IndexSourceLocation = 0;
            IndexPriceCategory = 0;
            IndexPriceType = 0;
            IndexDestinationWarehouse = 0;
            IndexDestinationLocation = 0;
            IndexReason = 0;
            Comment = "";
        }

        public void IsDestinationEnabled(string transactionType)
        {
            switch (transactionType)
            {
                case "AD":
                case "WW":
                case "LL":
                    EnableDestination = true;
                    validProperties[8] = "Error";
                    validProperties[9] = "Error";
                    break;

                case "PR":
                case "DS":
                case "SD":
                case "SL":
                    EnableDestination = false;
                    validProperties[8] = null;
                    validProperties[9] = null;
                    IndexDestinationWarehouse = 0;
                    IndexDestinationLocation = 0;
                    break;

                default:
                    EnableDestination = false;
                    break;
            }
        }

        public void ToggleFirstLoad()
        {
            IsFirstLoad = false;
        }

        private void LineItem()
        {
            IFormatProvider culture = new System.Globalization.CultureInfo("en-US", true);


            TransactionModel lineableTransaction = new TransactionModel();

            lineableTransaction.TransactionNumber = SelectedTransactionNumber.Substring(0, 6);
            lineableTransaction.TransactionTypeCode = SelectedTransactionType.Code;
            lineableTransaction.TransactionType = SelectedTransactionType.Description;
            lineableTransaction.DocumentNumber = DocumentNumber;
            lineableTransaction.TransactionDate = DateTime.Parse(TransactionDate, culture);
            lineableTransaction.SourceWarehouseCode = SelectedSourceWarehouse.Code;
            lineableTransaction.SourceWarehouse = SelectedSourceWarehouse.Name;
            lineableTransaction.SourceLocationCode = SelectedSourceLocation.Code;
            lineableTransaction.SourceLocation = SelectedSourceLocation.Location;
            lineableTransaction.PriceCategory = SelectedPriceCategory;
            lineableTransaction.PriceType = SelectedPriceType;
            lineableTransaction.DestinationWarehouseCode = SelectedDestinationWarehouse.Code;
            lineableTransaction.DestinationWarehouse = SelectedDestinationWarehouse.Name;
            lineableTransaction.DestinationLocationCode = SelectedDestinationLocation.Code;
            lineableTransaction.DestinationLocation = SelectedDestinationLocation.Location;
            lineableTransaction.ReasonCode = SelectedReason.ReasonCode;
            lineableTransaction.Reason = SelectedReason.Description;
            lineableTransaction.Comment = Comment;

            if (SelectedTransactionNumber.Substring(0, 6) == latestTransNo)
            {
                lineableTransaction.AddTransactionEntry(lineableTransaction);

                if (updateTransNo)
                {
                    transactionNumber.Update();
                    UpdateTransactionNumbers();
                }

                MyGlobals.IsNewTransaction = true;
                MyGlobals.Transaction = lineableTransaction;
            }
            else
            {
                MyGlobals.IsNewTransaction = false;
                MyGlobals.Transaction = lineableTransaction.Fetch(SelectedTransactionNumber);
            }
            MyGlobals.IteViewModel.SelectedPage = new LineItemPageView();
        }

        private void DeleteTransaction()
        {
            if (SelectedTransactionNumber != latestTransNo)
            {
                if (IsLineable)
                {
                    transaction.DeleteTransaction(SelectedTransactionNumber);
                    foreach (var transactionNumber in CmbTransactionNumbers)
                    {
                        if (transactionNumber == SelectedTransactionNumber)
                        {
                            CmbTransactionNumbers.RemoveAt(CmbTransactionNumbers.IndexOf(transactionNumber));
                            break;
                        }
                    }
                    IndexTransactionNumber = 0;
                    IsFirstLoad = true;
                    ClearForm();
                }
            }
        }

        private void Print()
        {
            MyGlobals.PrintingParent = MyGlobals.TransactionEntryPage;
            MyGlobals.IteViewModel.SelectedPage = new PrintPreviewPageView();
        }

        private void Post()
        {
            MyGlobals.PostingParent = MyGlobals.TransactionEntryPage;
            MyGlobals.IteViewModel.SelectedPage = new PostingPageView();
        }
        #endregion

        #region IDataErrorInfo Members

        public string Error
        {
            get
            {
                return null;
            }
        }


        public string this[string propertyName]
        {
            get
            {
                if (!IsFirstLoad)
                    return GetValidationError(propertyName);
                return null;
            }
        }
        #endregion

        #region Validation

        string[] validProperties = { "Error", "Error", "Error", "Error", "Error", "Error", "Error", "Error", "Error", "Error", "Error" };

        private void resetValidProperties()
        {
            int x = validProperties.Length;
            for (int i = 0; i < x; i++)
            {
                validProperties[i] = "Error";
            }
        }

        private void isValid()
        {
            Debug.WriteLine("ISVALID");
            int counter = 0;
            int x = 0;
            foreach (var i in validProperties)
            {
                if (!EnableDestination)
                {
                    if (x != 8 || x != 9)
                    {
                        if (i != null)
                        {
                            counter++;
                            Debug.WriteLine("validProperties[" + x + "] = " + i);
                        }
                    }
                    x++;
                }
                else
                {
                    if (i != null)
                    {
                        counter++;
                        Debug.WriteLine("validProperties[" + x + "] = " + i);
                    }
                    x++;
                }
            }

            if (counter > 0)
                IsLineable = false;
            else
                IsLineable = true;
        }

        private string GetValidationError(string propertyName)
        {
            string error = null;
            switch (propertyName)
            {
                case "SelectedTransactionNumber":
                    error = ValidateNullOrEmpty("Transaction Number", selectedTransactionNumber);
                    if (string.IsNullOrEmpty(error))
                        validProperties[0] = null;
                    else
                        validProperties[0] = error;
                    break;
                case "SelectedTransactionType":
                    if (SelectedTransactionType != null)
                    {
                        error = ValidateNullOrEmpty("Transaction Type", Convert.ToString(SelectedTransactionType.Code));
                        if (string.IsNullOrEmpty(error))
                            validProperties[1] = null;
                        else
                            validProperties[1] = error;
                    }
                    else
                    {
                        error = "Transaction Type cannot be empty!";
                    }
                    break;
                case "DocumentNumber":
                    error = ValidateDocumentNumber();
                    if (string.IsNullOrEmpty(error))
                        validProperties[2] = null;
                    else
                        validProperties[2] = error;
                    break;
                case "TransactionDate":
                    error = ValidateNullOrEmpty("Transaction Date", Convert.ToString(TransactionDate));
                    //if (error == null)
                    //{
                    //    error = CheckDate(Convert.ToString(TransactionDate));
                    //}
                    if (string.IsNullOrEmpty(error))
                        validProperties[3] = null;
                    else
                        validProperties[3] = error;
                    break;
                case "SelectedSourceWarehouse":
                    if (SelectedSourceWarehouse != null)
                    {
                        error = ValidateNullOrEmpty("Source Warehouse", Convert.ToString(SelectedSourceWarehouse.Code));
                        if (string.IsNullOrEmpty(error))
                            validProperties[4] = null;
                        else
                            validProperties[4] = error;
                    }
                    else
                    {
                        error = "Source Warehouse cannot be empty!";
                    }
                    break;
                case "SelectedSourceLocation":
                    if (SelectedSourceLocation != null)
                    {
                        error = ValidateNullOrEmpty("Source Location", Convert.ToString(SelectedSourceLocation.Code));
                        if (string.IsNullOrEmpty(error))
                            validProperties[5] = null;
                        else
                            validProperties[5] = error;
                    }
                    else
                    {
                        error = "Source Location cannot be empty!";
                    }
                    break;
                case "SelectedPriceCategory":
                    error = ValidateNullOrEmpty("Price Category", SelectedPriceCategory);
                    if (string.IsNullOrEmpty(error))
                        validProperties[6] = null;
                    else
                        validProperties[6] = error;
                    break;
                case "SelectedPriceType":
                    error = ValidateNullOrEmpty("Price Type", SelectedPriceType);
                    if (string.IsNullOrEmpty(error))
                        validProperties[7] = null;
                    else
                        validProperties[7] = error;
                    break;
                case "SelectedDestinationWarehouse":
                    if (EnableDestination)
                        if (SelectedDestinationWarehouse != null)
                        {
                            error = ValidateNullOrEmpty("Destination Warehouse", Convert.ToString(SelectedDestinationWarehouse.Code));
                            if (string.IsNullOrEmpty(error))
                                validProperties[8] = null;
                            else
                                validProperties[8] = error;
                        }
                        else
                        {
                            error = "Destination Warehouse cannot be empty!";
                        }
                    break;
                case "SelectedDestinationLocation":
                    if (EnableDestination)
                        if (SelectedDestinationLocation != null)
                        {
                            error = ValidateNullOrEmpty("Destination Location", Convert.ToString(SelectedDestinationLocation.Code));
                            if (string.IsNullOrEmpty(error))
                                validProperties[9] = null;
                            else
                                validProperties[9] = error;
                        }
                        else
                        {
                            error = "Destination Location cannot be empty!";
                        }
                    break;
                case "SelectedReason":
                    if (SelectedReason != null)
                    {
                        error = ValidateNullOrEmpty("Reason", Convert.ToString(SelectedReason.Description));
                        if (string.IsNullOrEmpty(error))
                            validProperties[10] = null;
                        else
                            validProperties[10] = error;
                    }
                    else
                    {
                        error = "Reason cannot be empty!";
                    }
                    break;
            }
            isValid();
            return error;
        }

        private string CheckDate(string date)
        {
            var temp = DateTime.UtcNow;
            string d = DateTime.TryParse(date, out temp).ToString();

            return d;
        }

        private string ValidateDocumentNumber()
        {
            Regex regex = new Regex("^[0-9]+$");
            if (string.IsNullOrEmpty(documentNumber))
                return "Document Number cannot be empty!";
            else if (!regex.IsMatch(documentNumber))
                return "Document Number cannot accept non-numeric values!";

            return null;
        }

        private string ValidateNullOrEmpty(string propertyName, string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return propertyName + " cannot be empty!";
            else
                return null;
        }

        #endregion

    }
}
