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

        private ObservableCollection<InventoryDummyModel> items = new ObservableCollection<InventoryDummyModel>();
        public ObservableCollection<InventoryDummyModel> Items
        {
            get { return items; }
            set
            {
                items = value;
                OnPropertyChanged("Items");
            }
        }

        InventoryDummyModel dummy = new InventoryDummyModel();

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

        private string itemCode;
        public string ItemCode
        {
            get { return itemCode; }
            set
            {
                itemCode = value;
                OnPropertyChanged("ItemCode");
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


        #endregion

        private InventoryDummyModel selectedItem;
        public InventoryDummyModel SelectedItem
        {
            get { return selectedItem; }
            set
            {
                if (value != null && value != selectedItem)
                {
                    selectedItem = value;
                    SelectedItemChanged();
                    OnPropertyChanged("SelectedItem");
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

            decimal _orderAmount = 0;

            if (!MyGlobals.IsNewTransaction)
            {
                foreach (var item in dummy.FetchAll(TransactionNumber))
                {
                    _orderAmount += Convert.ToDecimal(item.LineAmount);
                    Items.Add(item);
                }
                OrderAmount = _orderAmount.ToString();
            }

        }

        private void SelectedItemChanged()
        {
            ItemCode = SelectedItem.ItemCode;
            PT = SelectedItem.PriceType;
            LC = SelectedItem.Location;
            Cases = SelectedItem.Cases.ToString();
            Pieces = SelectedItem.Pieces.ToString();
            UnitPrice = SelectedItem.PricePerPiece.ToString();
            Expiry = SelectedItem.Expiration.ToString("MM/dd/yyyy");
            ItemDescription = SelectedItem.ItemDescription;
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
