using System;
using System.ComponentModel;
using ESI_ITE.Model;
using System.Collections.ObjectModel;

namespace ESI_ITE.ViewModel
{
    class LineItemPageViewModel : ViewModelBase, IDataErrorInfo
    {
        TransactionModel transactionModel;

        #region Constructor

        public LineItemPageViewModel()
        {

        }

        public LineItemPageViewModel(TransactionModel trans)
        {
            transactionModel = trans;
            Load();
        }

        #endregion

        #region Properties

        private ObservableCollection<InventoryDummyModel> items;
        public ObservableCollection<InventoryDummyModel> Items
        {
            get { return items; }
            set { items = value; }
        }

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

        private string transactionType;
        public string TransactionType
        {
            get { return transactionType; }
            set
            {
                transactionType = value;
                OnPropertyChanged("TransactionType");
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

        private void Load()
        {
            TransactionNumber = transactionModel.TransactionNumber;
            DocumentNumber = transactionModel.DocumentNumber;
            TransactionDate = transactionModel.TransactionDate.ToString();
            TransactionType = transactionModel.TransactionType;
            ReasonCode = transactionModel.ReasonCode + " " + transactionModel.Reason;
            Comment = transactionModel.Comment;
            SourceWarehouse = transactionModel.SourceWarehouseCode + " " + transactionModel.SourceWarehouse;
            SourceLocation = transactionModel.SourceLocationCode + " " + transactionModel.SourceLocation;
            DestinationWarehouse = transactionModel.DestinationWarehouseCode + " " + transactionModel.DestinationWarehouse;
            DestinationLocation = transactionModel.DestinationLocationCode + " " + transactionModel.DestinationLocation;
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
