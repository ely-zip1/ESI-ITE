using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using ESI_ITE.Model;
using System.ComponentModel;
using System.Text.RegularExpressions;
using ESI_ITE.ViewModel.Command;

namespace ESI_ITE.ViewModel
{
    public class TransactionEntryViewModel : ViewModelBase, IDataErrorInfo
    {
        #region Properties

        public LineItemCommand lineItemCommand { get; set; }

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

        public List<TransactionTypesModel> TransactionTypesList { get { return transactionTypesList; } }
        public List<WareHouseModel> WareHouseList { get { return wareHouseList; } }
        public List<LocationModel> LocationList { get { return locationList; } }
        public List<string> PriceCategoryList { get { return priceCategoryList; } }
        public List<string> PriceTypeList { get { return priceTypeList; } }
        public List<ReasonsModel> ReasonList { get { return reasonList; } }

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
            set { cmbReason = value; }
        }

        #region Properties for Binding
        private string selectedTransactionNumber;
        public string SelectedTransactionNumber
        {
            get { return selectedTransactionNumber; }
            set
            {
                if (selectedTransactionNumber != value)
                {
                    selectedTransactionNumber = value;
                    OnPropertyChanged("SelectedTransactionNumber");
                    if (!string.IsNullOrEmpty(selectedTransactionNumber))
                    {
                        SelectionChanged("Transaction Number");
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
                if (selectedTransactionType != value)
                {
                    selectedTransactionType = value;
                    OnPropertyChanged("SelectedTransactionType");
                    if (!string.IsNullOrEmpty(Convert.ToString(selectedTransactionType)))
                    {
                        SelectionChanged("Transaction Type");
                        IsDestinationEnabled(selectedTransactionType.Code);

                    }
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
                    OnPropertyChanged("DocumentNumber");

                }
            }
        }

        private string transactionDate;
        public string TransactionDate
        {
            get { return transactionDate; }
            set
            {
                if (transactionDate != value)
                {
                    transactionDate = value;
                    OnPropertyChanged("TransactionDate");

                }
            }
        }

        private WareHouseModel selectedSourceWarehouse;
        public WareHouseModel SelectedSourceWarehouse
        {
            get { return selectedSourceWarehouse; }
            set
            {
                if (selectedSourceWarehouse != value)
                {
                    selectedSourceWarehouse = value;
                    OnPropertyChanged("SelectedSourceWarehouse");
                    SelectionChanged("Warehouse");

                }
            }
        }

        private LocationModel selectedSourceLocation;
        public LocationModel SelectedSourceLocation
        {
            get { return selectedSourceLocation; }
            set
            {
                if (selectedSourceLocation != value)
                {
                    selectedSourceLocation = value;
                    OnPropertyChanged("SelectedSourceLocation");
                    SelectionChanged("Location");

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
                    OnPropertyChanged("SelectedPriceCategory");

                }
            }
        }

        private string selectedPriceType;
        public string SelectedPriceType
        {
            get { return selectedPriceType; }
            set
            {
                if (!string.IsNullOrEmpty(value) || selectedPriceType != value)
                {
                    selectedPriceType = value;
                    OnPropertyChanged("SelectedPriceType");

                }
            }
        }

        private WareHouseModel selectedDestinationWarehouse;
        public WareHouseModel SelectedDestinationWarehouse
        {
            get { return selectedDestinationWarehouse; }
            set
            {
                if (selectedDestinationWarehouse != value)
                {
                    selectedDestinationWarehouse = value;
                    OnPropertyChanged("SelectedDestinationWarehouse");

                }
            }
        }

        private LocationModel selectedDestinationLocation;
        public LocationModel SelectedDestinationLocation
        {
            get { return selectedDestinationLocation; }
            set
            {
                if (selectedDestinationLocation != value)
                {
                    selectedDestinationLocation = value;
                    OnPropertyChanged("SelectedDestinationLocation");

                }
            }
        }

        private ReasonsModel selectedReason;
        public ReasonsModel SelectedReason
        {
            get { return selectedReason; }
            set
            {
                if (selectedReason != value)
                {
                    selectedReason = value;
                    OnPropertyChanged("SelectedReason");

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
            }
        }
        #endregion

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
        private bool lineItems;
        public bool LineItems
        {
            get { return lineItems; }
            set
            {
                lineItems = value;
                OnPropertyChanged("LineItems");

            }
        }


        private bool isFirstLoad;

        private bool updateTransNo = false;

        private string latestTransNo;

        #endregion

        #region Constructor

        public TransactionEntryViewModel()
        {
            this.lineItemCommand = new LineItemCommand(this);
            isFirstLoad = true;
            Load();
        }

        #endregion

        private void Load()
        {
            latestTransNo = transactionNumber.Fetch();

            foreach (var obj in (transaction.FetchAll()))
            {
                CmbTransactionNumbers.Add(obj.TransactionNumber);
            }
            CmbTransactionNumbers.Add(latestTransNo);

            CmbTransactionType.Add(new TransactionTypesModel());
            foreach (var type in transactionType.FetchAll())
            {
                CmbTransactionType.Add(type);
            }

            transactionDate = currentDate.ToString("MM/dd/yyyy");

            CmbWarehouse.Add(new WareHouseModel());
            foreach (var wh in warehouse.FetchAll())
            {
                CmbWarehouse.Add(wh);
            }

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

            reasonList = reason.FetchAll();

            EnableDestination = false;
        }

        private void fillForm()
        {
            foreach (var trans in transaction.FetchAll())
                if (trans.TransactionNumber == SelectedTransactionNumber)
                {
                    //transaction type
                    foreach (var transType in transactionType.FetchAll())
                        if (transType.Code == trans.TransactionCode)
                        {
                            IndexTransactionType = transType.Id;
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
                            IndexSourceWarehouse = wh.Id;

                        if (wh.Code == trans.DestinationWarehouseCode)
                            IndexDestinationWarehouse = wh.Id;
                    }

                    //location
                    foreach (var loc in location.FetchAll())
                    {
                        if (loc.Code == trans.SourceLocationCode)
                            IndexSourceLocation = loc.Id;

                        if (loc.Code == trans.DestinationLocationCode)
                            IndexDestinationLocation = loc.Id;
                    }

                    //price category
                    switch (trans.PriceCategory)
                    {
                        case "Selling Price":
                            IndexPriceCategory = 1;
                            break;
                        case "Purchase Price":
                            IndexPriceCategory = 2;
                            break;
                    }

                    //price type
                    switch (trans.PriceType)
                    {
                        case "Current":
                            IndexPriceType = 1;
                            break;
                        case "3 Months Ago":
                            IndexPriceType = 2;
                            break;
                        case "6 Months Ago":
                            IndexPriceType = 3;
                            break;
                    }

                    //reason code
                    foreach (var reason in reason.FetchAll())
                    {
                        if (reason.ReasonCode == trans.ReasonCode)
                        {
                            IndexReason = int.Parse(reason.ReasonCode.Substring(2));
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
                case "Transaction Number":
                    if (selectedTransactionNumber == latestTransNo)
                    {
                        isFirstLoad = true;
                        updateTransNo = true;

                        //clear the form
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
                    else
                    {
                        isFirstLoad = false;
                        updateTransNo = false;
                        fillForm();
                    }
                    break;

                case "Transaction Type":
                    CmbReason.Clear();
                    CmbReason.Add(new ReasonsModel());
                    foreach (var item in reasonList)
                    {
                        if (item.TransactionType == selectedTransactionType.Code)
                        {
                            CmbReason.Add(item);
                        }
                    }
                    break;
            }
        }

        public void IsDestinationEnabled(string transactionType)
        {
            switch (transactionType)
            {
                case "AD":
                case "WW":
                case "LL":
                    EnableDestination = true;
                    break;

                case "PR":
                case "DS":
                case "SD":
                case "SL":
                    EnableDestination = false;
                    break;

                default:
                    EnableDestination = false;
                    break;
            }
        }

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
                if (!isFirstLoad)
                    return GetValidationError(propertyName);
                return null;
            }
        }
        #endregion

        #region Validation


        static readonly string[] ValidatedProperties =
        {
            "SelectedTransactionNumber",
            "SelectedTransactionType",
            "DocumentNumber",
            "TransactionDate",
            "SelectedSourceWarehouse",
            "SelectedSourceLocation",
            "SelectedPriceCategory",
            "SelectedPriceType",
            "SelectedDestinationWarehouse",
            "SelectedDestinationLocation",
            "SelectedReason"
        };

        public bool IsValid()
        {
            int i = 0;
            foreach (var property in ValidatedProperties)
            {
                if (GetValidationError(property) != null)
                    i++;
            }

            if (i > 0)
                return false;
            else
                return true;
        }

        private string GetValidationError(string propertyName)
        {
            string error = null;
            switch (propertyName)
            {
                case "SelectedTransactionNumber":
                    error = ValidateNullOrEmpty(propertyName, selectedTransactionNumber);
                    break;
                case "SelectedTransactionType":
                    if (SelectedTransactionType != null)
                        error = ValidateNullOrEmpty(propertyName, Convert.ToString(SelectedTransactionType.Code));
                    break;
                case "DocumentNumber":
                    error = ValidateNullOrEmpty(propertyName, DocumentNumber);
                    break;
                case "TransactionDate":
                    error = ValidateNullOrEmpty(propertyName, Convert.ToString(TransactionDate));
                    if (error == null)
                    {
                        error = CheckDate(Convert.ToString(TransactionDate));
                    }
                    break;
                case "SelectedSourceWarehouse":
                    if (SelectedSourceWarehouse != null)
                        error = ValidateNullOrEmpty(propertyName, Convert.ToString(SelectedSourceWarehouse.Code));
                    break;
                case "SelectedSourceLocation":
                    if (SelectedSourceLocation != null)
                        error = ValidateNullOrEmpty(propertyName, Convert.ToString(SelectedSourceLocation.Code));
                    break;
                case "SelectedPriceCategory":
                    error = ValidateNullOrEmpty(propertyName, SelectedPriceCategory);
                    break;
                case "SelectedPriceType":
                    error = ValidateNullOrEmpty(propertyName, SelectedPriceType);
                    break;
                case "SelectedDestinationWarehouse":
                    if (EnableDestination)
                        if (SelectedDestinationWarehouse != null)
                            error = ValidateNullOrEmpty(propertyName, Convert.ToString(SelectedDestinationWarehouse.Code));
                    break;
                case "SelectedDestinationLocation":
                    if (EnableDestination)
                        if (SelectedDestinationLocation != null)
                            error = ValidateNullOrEmpty(propertyName, Convert.ToString(SelectedDestinationLocation.Code));
                    break;
                case "SelectedReason":
                    if (SelectedReason != null)
                        error = ValidateNullOrEmpty(propertyName, Convert.ToString(SelectedReason.Description));
                    break;
                case "Comment":
                    error = ValidateNullOrEmpty(propertyName, Comment);
                    break;
            }

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
            else if (regex.IsMatch(documentNumber))
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
