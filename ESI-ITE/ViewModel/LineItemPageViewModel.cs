using System;
using System.ComponentModel;
using ESI_ITE.Model;
using System.Collections.ObjectModel;
using ESI_ITE.ViewModel.Command;
using System.Windows.Input;
using System.Windows.Documents;
using System.Windows.Markup;
using System.Windows.Controls;
using System.Printing;
using System.Windows.Xps;
using System.Collections.Generic;
using ESI_ITE.Printing;
using System.Data;

namespace ESI_ITE.ViewModel
{
    class LineItemPageViewModel : ViewModelBase, IDataErrorInfo
    {
        TransactionModel transactionModel;


        #region Constructor

        public LineItemPageViewModel()
        {
            cancelCommand = new DelegateCommand(CancelAndGoBack);
            printTransaction = new DelegateCommand(PrintDocument);
            itemCode_KeyDown = new DelegateCommand(KeyPressed);
            itemCode_KeyUp = new DelegateCommand(KeyUp);

            transactionModel = MyGlobals.Transaction;
            Load();
        }

        public LineItemPageViewModel(TransactionModel trans)
        {
            transactionModel = trans;
            Load();
        }

        #endregion

        #region Properties

        private ObservableCollection<InventoryDummyModel> dataGridItems = new ObservableCollection<InventoryDummyModel>();
        public ObservableCollection<InventoryDummyModel> DatagridItems
        {
            get { return dataGridItems; }
            set
            {
                dataGridItems = value;
                OnPropertyChanged("DatagridItems");
            }
        }

        InventoryDummyModel dummy = new InventoryDummyModel();

        ItemModel itemModel = new ItemModel();
        DataTable itemTable = new DataTable();

        PriceTypeModel priceTypeModel = new PriceTypeModel();
        PricingModel pricingModel = new PricingModel();
        PriceModel price;

        bool isKeyDown = false;

        #region Header

        private string weeksCover;
        public string WeeksCover
        {
            get { return weeksCover; }
            set
            {
                weeksCover = value;
                OnPropertyChanged("WeeksCover");
            }
        }

        private string transactionNumber;
        public string TransactionNumber
        {
            get { return transactionNumber; }
            set
            {
                transactionNumber = value;
                OnPropertyChanged("TransactionNumber");
            }
        }

        private string documentNumber;
        public string DocumentNumber
        {
            get { return documentNumber; }
            set
            {
                documentNumber = value;
                OnPropertyChanged("DocumentNumber");
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
            }
        }

        private string transactionTypeCode;
        public string TransactionTypeCode
        {
            get { return transactionTypeCode; }
            set
            {
                transactionTypeCode = value;
                OnPropertyChanged("TransactionTypeCode");
            }
        }

        private string transactionTypeDescription;
        public string TransactionTypeDescription
        {
            get { return transactionTypeDescription; }
            set
            {
                transactionTypeDescription = value;
                OnPropertyChanged("TransactionTypeDescription");
            }
        }

        private string reasonCode;
        public string ReasonCode
        {
            get { return reasonCode; }
            set
            {
                reasonCode = value;
                OnPropertyChanged("ReasonCode");
            }
        }

        private string reasonDescription;
        public string ReasonDescription
        {
            get { return reasonDescription; }
            set
            {
                reasonDescription = value;
                OnPropertyChanged("ReasonDescription");
            }
        }

        private string comment;
        public string Comment
        {
            get { return comment; }
            set
            {
                comment = value;
                OnPropertyChanged("Comment");
            }
        }

        private string stocksOnHand;
        public string StocksOnHand
        {
            get { return stocksOnHand; }
            set
            {
                stocksOnHand = value;
                OnPropertyChanged("StocksOnHand");
            }
        }

        private string sourceWarehouse;
        public string SourceWarehouse
        {
            get { return sourceWarehouse; }
            set
            {
                sourceWarehouse = value;
                OnPropertyChanged("SourceWarehouse");
            }
        }

        private string sourceLocation;
        public string SourceLocation
        {
            get { return sourceLocation; }
            set
            {
                sourceLocation = value;
                OnPropertyChanged("SourceLocation");
            }
        }

        private string destinationWarehouse;
        public string DestinationWarehouse
        {
            get { return destinationWarehouse; }
            set
            {
                destinationWarehouse = value;
                OnPropertyChanged("DestinationWarehouse");
            }
        }

        private string destinationLocation;
        public string DestinationLocation
        {
            get { return destinationLocation; }
            set
            {
                destinationLocation = value;
                OnPropertyChanged("DestinationLocation");
            }
        }

        #endregion

        #region Item entry

        private string itemCode = "";
        public string ItemCode
        {
            get { return itemCode; }
            set
            {
                itemCode = value;
                OnPropertyChanged("ItemCode");
                ClearForm();
                if (isKeyDown)
                {
                    SuggestItems(value);
                }
            }
        }

        private ItemModel selectedItemCode;
        public ItemModel SelectedItemCode
        {
            get { return selectedItemCode; }
            set
            {
                if (value != null)
                {
                    selectedItemCode = value;
                    MyGlobals.SelectedItem = value;
                    OnPropertyChanged("SelectedItemCode");
                    FillForm();
                }
            }
        }

        private ObservableCollection<PriceTypeModel> ptList = new ObservableCollection<PriceTypeModel>();
        public ObservableCollection<PriceTypeModel> PtList
        {
            get { return ptList; }
            set
            {
                ptList = value;
                OnPropertyChanged("PtList");
            }
        }

        private int ptSelectedIndex;
        public int PtSelectedIndex
        {
            get { return ptSelectedIndex; }
            set
            {
                ptSelectedIndex = value;
                OnPropertyChanged("PtSelectedIndex");
            }
        }

        private string pt;
        public string PT
        {
            get { return pt; }
            set
            {
                pt = value;
                OnPropertyChanged("PT");
            }
        }

        private string lc;
        public string LC
        {
            get { return lc; }
            set
            {
                lc = value;
                OnPropertyChanged("LC");
            }
        }

        private string cases;
        public string Cases
        {
            get { return cases; }
            set
            {
                cases = value;
                OnPropertyChanged("Cases");
            }
        }

        private string pieces;
        public string Pieces
        {
            get { return pieces; }
            set
            {
                pieces = value;
                OnPropertyChanged("Pieces");
            }
        }

        private string unitPrice;
        public string UnitPrice
        {
            get { return unitPrice; }
            set
            {
                unitPrice = value;
                OnPropertyChanged("UnitPrice");
            }
        }

        private string expiry;
        public string Expiry
        {
            get { return expiry; }
            set
            {
                expiry = value;
                OnPropertyChanged("Expiry");
            }
        }

        private string taxRate;
        public string TaxRate
        {
            get { return taxRate; }
            set
            {
                taxRate = value;
                OnPropertyChanged("Taxrate");
            }
        }

        private string warehouseCode;
        public string WarehouseCode
        {
            get { return warehouseCode; }
            set
            {
                warehouseCode = value;
                OnPropertyChanged("WarehouseCode");
            }
        }

        private string itemDescription;
        public string ItemDescription
        {
            get { return itemDescription; }
            set
            {
                itemDescription = value;
                OnPropertyChanged("ItemDescription");
            }
        }

        private string orderAmount = "0.00";
        public string OrderAmount
        {
            get { return orderAmount; }
            set
            {
                orderAmount = value;
                OnPropertyChanged("OrderAmount");
            }
        }

        private bool dropDownOpen;
        public bool DropDownOpen
        {
            get { return dropDownOpen; }
            set
            {
                dropDownOpen = value;
                OnPropertyChanged("DropDownOpen");
            }
        }

        #endregion

        private InventoryDummyModel datagridSelectedItem;
        public InventoryDummyModel DatagridSelectedItem
        {
            get { return datagridSelectedItem; }
            set
            {
                if (value != null && value != datagridSelectedItem)
                {
                    datagridSelectedItem = value;
                    SelectedDatagridItemChanged();
                    OnPropertyChanged("DatagridSelectedItem");
                }
            }
        }

        private DelegateCommand cancelCommand;
        public ICommand CancelCommand
        {
            get { return cancelCommand; }
        }

        private DelegateCommand printTransaction;
        public ICommand PrintTransaction
        {
            get { return printTransaction; }
        }

        private DelegateCommand itemCode_KeyDown;
        public ICommand ItemCode_KeyDown
        {
            get { return itemCode_KeyDown; }
        }

        private DelegateCommand itemCode_KeyUp;
        public ICommand ItemCode_KeyUp
        {
            get { return itemCode_KeyUp; }
        }

        private ObservableCollection<ItemModel> itemCodeList = new ObservableCollection<ItemModel>();
        public ObservableCollection<ItemModel> ItemCodeList
        {
            get { return itemCodeList; }
            set { itemCodeList = value; }
        }

        private ObservableCollection<ItemModel> suggestedItemCodeList = new ObservableCollection<ItemModel>();
        public ObservableCollection<ItemModel> SuggestedItemCodeList
        {
            get { return suggestedItemCodeList; }
            set { suggestedItemCodeList = value; }
        }

        #endregion

        private void Load()
        {
            TransactionNumber = transactionModel.TransactionNumber;
            DocumentNumber = transactionModel.DocumentNumber;
            TransactionDate = transactionModel.TransactionDate.ToString("MM/dd/yyyy");
            TransactionTypeCode = transactionModel.TransactionCode;
            TransactionTypeDescription = transactionModel.TransactionType;
            ReasonCode = transactionModel.ReasonCode;
            ReasonDescription = transactionModel.Reason;
            Comment = transactionModel.Comment;
            SourceWarehouse = transactionModel.SourceWarehouseCode + " - " + transactionModel.SourceWarehouse;
            SourceLocation = transactionModel.SourceLocationCode + " - " + transactionModel.SourceLocation;
            DestinationWarehouse = transactionModel.DestinationWarehouseCode + " - " + transactionModel.DestinationWarehouse;
            DestinationLocation = transactionModel.DestinationLocationCode + " - " + transactionModel.DestinationLocation;

            PtList.Clear();
            PtList.Add(new PriceTypeModel());
            foreach (var pt in priceTypeModel.FetchAll(transactionModel.PriceCategory))
            {
                PtList.Add(pt);
            }
            if (transactionModel.PriceType == "Purchase Price")
                PtSelectedIndex = 1;

            LC = transactionModel.SourceLocationCode;
            WarehouseCode = transactionModel.SourceWarehouseCode;

            decimal _orderAmount = 0;

            if (!MyGlobals.IsNewTransaction)
            {
                foreach (var item in dummy.FetchAll(TransactionNumber))
                {
                    _orderAmount += Convert.ToDecimal(item.LineAmount);
                    DatagridItems.Add(item);
                }
                OrderAmount = _orderAmount.ToString("#,##0.00");
                // _orderAmount.ToString();

            }

            //foreach(DataRow row in itemModel.FetchTable().Rows)
            //{
            //    ItemCodeList.Add(row["Code"].ToString());
            //}

            ItemCodeList.Clear();

            foreach (var item in itemModel.FetchAll())
            {
                if (transactionModel.PriceCategory == "Selling Price")
                {
                    if (item.SellingPriceLink == 1)
                    {
                        ItemCodeList.Add(item);
                    }
                }
                else if (transactionModel.PriceCategory == "Purchase Price")
                {
                    if (item.PurchasePriceLink == 1)
                    {
                        ItemCodeList.Add(item);
                    }
                }
            }

            //ItemCodeList = itemModel.FetchAll();
        }

        private void SelectedDatagridItemChanged()
        {
            ItemCode = DatagridSelectedItem.ItemCode;
            PT = DatagridSelectedItem.PriceType;
            LC = DatagridSelectedItem.Location;
            Cases = DatagridSelectedItem.Cases.ToString();
            Pieces = DatagridSelectedItem.Pieces.ToString();
            UnitPrice = DatagridSelectedItem.PricePerPiece.ToString();
            Expiry = DatagridSelectedItem.Expiration.ToString("MM/dd/yyyy");
            ItemDescription = DatagridSelectedItem.ItemDescription;

            var item = itemModel.Fetch(DatagridSelectedItem.ItemCode);

            TaxRate = item.TaxRate;
        }

        private void FillForm()
        {
            PriceTypeModel priceType = new PriceTypeModel();

            LC = transactionModel.SourceLocationCode;

            Cases = "0";
            Pieces = "0";

            price = new PriceModel();
            price = pricingModel.GetPrice(SelectedItemCode.Code, transactionModel.PriceCategory, transactionModel.PriceType);

            UnitPrice = Math.Round(price.Price, 2).ToString();

            TaxRate = selectedItemCode.TaxRate;

            WarehouseCode = transactionModel.SourceWarehouseCode;

            ItemDescription = SelectedItemCode.Description;
        }

        private void ClearForm()
        {
            if (string.IsNullOrWhiteSpace(ItemCode))
            {
                Cases = "0";
                Pieces = "0";
                UnitPrice = "";
                ItemDescription = "";
            }
        }

        private void SuggestItems(string value)
        {
            int counter = 1;
            SuggestedItemCodeList.Clear();
            foreach (var item in ItemCodeList)
            {
                if (item.Code.StartsWith(value))
                {
                    SuggestedItemCodeList.Add(item);
                    counter++;
                }
                if (counter > 10)
                {
                    break;
                }
            }
        }

        private void CancelAndGoBack()
        {
            MyGlobals.IteViewModel.SelectedPage = MyGlobals.TransactionEntryPage;
        }

        private void PrintDocument()
        {
            MyGlobals.TransactionList.Add(MyGlobals.Transaction);
            PrintingJob printJob = new PrintingJob();
            printJob.StartPrinting();
        }

        private void KeyPressed()
        {
            isKeyDown = true;
        }

        private void KeyUp()
        {
            isKeyDown = false;
        }

        private void AddItem()
        {
            InventoryDummyModel newItem = new InventoryDummyModel();

        }

        #region IDataErrorInfo Members

        public string this[string columnName]
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public string Error
        {
            get
            {
                throw new NotImplementedException();
            }
        }
        #endregion
    }
}
