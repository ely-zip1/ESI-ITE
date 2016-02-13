using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using ESI_ITE.Model;
using System.ComponentModel;
using System.Text.RegularExpressions;
using ESI_ITE.ViewModel.Command;
using System.Diagnostics;
using System.Windows.Input;

namespace ESI_ITE.ViewModel
{
    public class TransactionEntryViewModel : ViewModelBase, IDataErrorInfo
    {
        #region Properties

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
                if (selectedTransactionType != value)
                {
                    selectedTransactionType = value;
                    OnPropertyChanged("SelectedTransactionType");
                    if (!string.IsNullOrEmpty(Convert.ToString(selectedTransactionType)))
                    {
                        SelectionChanged("SelectedTransactionType");
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
                if (transactionDate != value)
                {
                    transactionDate = value;
                    OnPropertyChanged("TransactionDate");
                    SelectionChanged("TransactionDate");
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
                    if (!string.IsNullOrEmpty(Convert.ToString(selectedSourceWarehouse)))
                    {
                        selectedSourceWarehouse = value;
                        OnPropertyChanged("SelectedSourceWarehouse");
                        SelectionChanged("SelectedSourceWarehouse");
                    }
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
                    if (!string.IsNullOrEmpty(Convert.ToString(selectedSourceLocation)))
                    {
                        selectedSourceLocation = value;
                        OnPropertyChanged("SelectedSourceLocation");
                        SelectionChanged("SelectedSourceLocation");
                    }
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
                if (!string.IsNullOrEmpty(value) || selectedPriceType != value)
                {
                    selectedPriceType = value;
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
                if (selectedDestinationWarehouse != value)
                {
                    if (!string.IsNullOrEmpty(Convert.ToString(selectedDestinationWarehouse)))
                    {
                        selectedDestinationWarehouse = value;
                        OnPropertyChanged("SelectedDestinationWarehouse");
                        SelectionChanged("SelectedDestinationWarehouse");
                    }
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
                    if (!string.IsNullOrEmpty(Convert.ToString(selectedDestinationLocation)))
                    {
                        selectedDestinationLocation = value;
                        OnPropertyChanged("SelectedDestinationLocation");
                        SelectionChanged("SelectedDestinationLocation");
                    }
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
                    if (!string.IsNullOrEmpty(Convert.ToString(selectedReason)))
                    {
                        selectedReason = value;
                        OnPropertyChanged("SelectedReason");
                        SelectionChanged("SelectedReason");
                    }
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
                OnPropertyChanged("LineItems");

            }
        }

        public bool IsFirstLoad;

        private bool updateTransNo = false;

        private string latestTransNo;

        private DelegateCommand lineItemCommand;
        public ICommand LineItemCommand
        {
            get { return lineItemCommand; }
        }
        #endregion

        #region Constructor

        public TransactionEntryViewModel()
        {
            lineItemCommand = new DelegateCommand(LineItem);
            IsFirstLoad = true;
            Load();
        }

        #endregion

        private void LineItem()
        {

        }

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

            ReasonList = reason.FetchAll();

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
                case "SelectedTransactionNumber":
                    resetValidProperties();
                    if (selectedTransactionNumber == latestTransNo)
                    {
                        IsFirstLoad = true;
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
                        IsFirstLoad = false;
                        updateTransNo = false;
                        fillForm();
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

                    ReasonList.Clear();
                    ReasonList = reason.FetchAll();

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
                case "selectedPriceType":
                    if (string.IsNullOrEmpty(selectedPriceType))
                    {
                        validProperties[7] = "";
                        IsLineable = false;
                    }
                    else
                        validProperties[7] = null;
                    break;
                case "SelectedDestinationWarehouse":
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


        //static readonly string[] ValidatedProperties =
        //{
        //    "SelectedTransactionNumber",
        //    "SelectedTransactionType",
        //    "DocumentNumber",
        //    "TransactionDate",
        //    "SelectedSourceWarehouse",
        //    "SelectedSourceLocation",
        //    "SelectedPriceCategory",
        //    "SelectedPriceType",
        //    "SelectedDestinationWarehouse",
        //    "SelectedDestinationLocation",
        //    "SelectedReason"
        //};

        string[] validProperties = { "Error", "Error", "Error", "Error", "Error", "Error", "Error", "Error", "Error", "Error", "Error" };
        private void resetValidProperties()
        {
            var list = new List<string>();
            foreach (var i in validProperties)
                list.Add("Error");
            validProperties = list.ToArray();
        }

        //public bool IsValid()
        //{
        //    int i = 0;
        //    foreach (var property in ValidatedProperties)
        //    {
        //        if (GetValidationError(property) != null)
        //            i++;
        //    }

        //    if (i > 0)
        //        return false;
        //    else
        //        return true;
        //}

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
                }
                else
                {
                    if (i != null)
                    {
                        counter++;
                        Debug.WriteLine("validProperties[" + x + "] = " + i);
                    }
                }

                x++;
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
                    error = ValidateNullOrEmpty(propertyName, selectedTransactionNumber);
                    if (string.IsNullOrEmpty(error))
                        validProperties[0] = null;
                    else
                        validProperties[0] = error;
                    break;
                case "SelectedTransactionType":
                    if (SelectedTransactionType != null)
                        error = ValidateNullOrEmpty(propertyName, Convert.ToString(SelectedTransactionType.Code));
                    if (string.IsNullOrEmpty(error))
                        validProperties[1] = null;
                    else
                        validProperties[1] = error;
                    break;
                case "DocumentNumber":
                    error = ValidateDocumentNumber();
                    if (string.IsNullOrEmpty(error))
                        validProperties[2] = null;
                    else
                        validProperties[2] = error;
                    break;
                case "TransactionDate":
                    error = ValidateNullOrEmpty(propertyName, Convert.ToString(TransactionDate));
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
                        error = ValidateNullOrEmpty(propertyName, Convert.ToString(SelectedSourceWarehouse.Code));
                    if (string.IsNullOrEmpty(error))
                        validProperties[4] = null;
                    else
                        validProperties[4] = error;
                    break;
                case "SelectedSourceLocation":
                    if (SelectedSourceLocation != null)
                        error = ValidateNullOrEmpty(propertyName, Convert.ToString(SelectedSourceLocation.Code));
                    if (string.IsNullOrEmpty(error))
                        validProperties[5] = null;
                    else
                        validProperties[5] = error;
                    break;
                case "SelectedPriceCategory":
                    error = ValidateNullOrEmpty(propertyName, SelectedPriceCategory);
                    if (string.IsNullOrEmpty(error))
                        validProperties[6] = null;
                    else
                        validProperties[6] = error;
                    break;
                case "SelectedPriceType":
                    error = ValidateNullOrEmpty(propertyName, SelectedPriceType);
                    if (string.IsNullOrEmpty(error))
                        validProperties[7] = null;
                    else
                        validProperties[7] = error;
                    break;
                case "SelectedDestinationWarehouse":
                    if (EnableDestination)
                        if (SelectedDestinationWarehouse != null)
                            error = ValidateNullOrEmpty(propertyName, Convert.ToString(SelectedDestinationWarehouse.Code));
                    if (string.IsNullOrEmpty(error))
                        validProperties[8] = null;
                    else
                        validProperties[8] = error;
                    break;
                case "SelectedDestinationLocation":
                    if (EnableDestination)
                        if (SelectedDestinationLocation != null)
                            error = ValidateNullOrEmpty(propertyName, Convert.ToString(SelectedDestinationLocation.Code));
                    if (string.IsNullOrEmpty(error))
                        validProperties[9] = null;
                    else
                        validProperties[9] = error;
                    break;
                case "SelectedReason":
                    if (SelectedReason != null)
                        error = ValidateNullOrEmpty(propertyName, Convert.ToString(SelectedReason.Description));
                    if (string.IsNullOrEmpty(error))
                        validProperties[10] = null;
                    else
                        validProperties[10] = error;
                    break;
                case "Comment":
                    error = ValidateNullOrEmpty(propertyName, Comment);
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
