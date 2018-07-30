﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ESI_ITE.Model;

namespace ESI_ITE.ViewModel.CNDN
{
    public class CNLineItemViewModel : ViewModelBase
    {
        public CNLineItemViewModel()
        {

        }

        #region Properties

        private ObservableCollection<List<string>> linedItemCollection = new ObservableCollection<List<string>>();
        public ObservableCollection<List<string>> LinedItemCollection
        {
            get
            {
                return linedItemCollection;
            }
            set
            {
                linedItemCollection = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<ItemModel> searchItemCollection = new ObservableCollection<ItemModel>();
        public ObservableCollection<ItemModel> SearchItemCollection
        {
            get
            {
                return searchItemCollection;
            }
            set
            {
                searchItemCollection = value;
                OnPropertyChanged();
            }
        }

        private ItemModel selectedSearchItem;
        public ItemModel SelectedSearchItem
        {
            get
            {
                return selectedSearchItem;
            }
            set
            {
                selectedSearchItem = value;
                OnPropertyChanged();
            }
        }

        private int selectedIndexSearchItem;
        public int SelectedIndexSearchItem
        {
            get
            {
                return selectedIndexSearchItem;
            }
            set
            {
                selectedIndexSearchItem = value;
                OnPropertyChanged();
            }
        }

        private List<string> selectedLinedItem;
        public List<string> SelectedLinedItem
        {
            get
            {
                return selectedLinedItem;
            }
            set
            {
                selectedLinedItem = value;
                OnPropertyChanged();
            }
        }

        private int selectedIndexLinedItem;
        public int SelectedIndexLinedItem
        {
            get
            {
                return selectedIndexLinedItem;
            }
            set
            {
                selectedIndexLinedItem = value;
                OnPropertyChanged();
            }
        }

        private bool isTenPercent;
        public bool IsTenPercent
        {
            get
            {
                return isTenPercent;
            }
            set
            {
                isTenPercent = value;
                OnPropertyChanged();
            }
        }

        private bool isTwelvePercent;
        public bool IsTwelvePercent
        {
            get
            {
                return isTwelvePercent;
            }
            set
            {
                isTwelvePercent = value;
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
            get
            {
                return customerName;
            }
            set
            {
                customerName = value;
                OnPropertyChanged();
            }
        }

        private string salesmanNumber;
        public string SalesmanNumber
        {
            get
            {
                return salesmanNumber;
            }
            set
            {
                salesmanNumber = value;
                OnPropertyChanged();
            }
        }

        private string salesmanName;
        public string SalesmanName
        {
            get
            {
                return salesmanName;
            }
            set
            {
                salesmanName = value;
                OnPropertyChanged();
            }
        }

        private string termCode;
        public string TermCode
        {
            get
            {
                return termCode;
            }
            set
            {
                termCode = value;
                OnPropertyChanged();
            }
        }

        private string termDescription;
        public string TermDescription
        {
            get
            {
                return termDescription;
            }
            set
            {
                termDescription = value;
                OnPropertyChanged();
            }
        }

        private string priceUsed;
        public string PriceUsed
        {
            get
            {
                return priceUsed;
            }
            set
            {
                priceUsed = value;
                OnPropertyChanged();
            }
        }

        private string cnNumber;
        public string CnNumber
        {
            get
            {
                return cnNumber;
            }
            set
            {
                cnNumber = value;
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

        private string selectedItemCode;
        public string SelectedItemCode
        {
            get
            {
                return selectedItemCode;
            }
            set
            {
                selectedItemCode = value;
                OnPropertyChanged();
            }
        }

        private string selectedPriceType;
        public string SelectedPriceType
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

        private string selectedLocation;
        public string SelectedLocation
        {
            get
            {
                return selectedLocation;
            }
            set
            {
                selectedLocation = value;
                OnPropertyChanged();
            }
        }

        private int cases;
        public int Cases
        {
            get
            {
                return cases;
            }
            set
            {
                cases = value;
                OnPropertyChanged();
            }
        }

        private int pieces;
        public int Pieces
        {
            get
            {
                return pieces;
            }
            set
            {
                pieces = value;
                OnPropertyChanged();
            }
        }

        private string pricePerPiece;
        public string PricePerPiece
        {
            get
            {
                return pricePerPiece;
            }
            set
            {
                pricePerPiece = value;
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

        private string warehouseCode;
        public string WarehouseCode
        {
            get
            {
                return warehouseCode;
            }
            set
            {
                warehouseCode = value;
                OnPropertyChanged();
            }
        }

        private string truckingCharge;
        public string TruckingCharge
        {
            get
            {
                return truckingCharge;
            }
            set
            {
                truckingCharge = value;
                OnPropertyChanged();
            }
        }

        private string orderAmount;
        public string OrderAmount
        {
            get
            {
                return orderAmount;
            }
            set
            {
                orderAmount = value;
                OnPropertyChanged();
            }
        }

        #endregion


        private void Load()
        {
            var cnHeaderObject = new CreditNoteHeaderModel();
            cnHeaderObject = (CreditNoteHeaderModel)cnHeaderObject.Fetch(MyGlobals.SelectedCNDNTransaction, "code");

            var cnLineObject = new CreditNoteLineModel();
            var cnLineList = cnLineObject.FetchPerCreditNoteHead(cnHeaderObject.Id.ToString());

            if (cnLineList.Count > 0)
            {
                foreach (var row in cnLineList)
                {
                    var itemObject = new ItemModel();
                    itemObject = (ItemModel)itemObject.Fetch(row.ItemId.ToString(), "id");

                    var lineItem = new List<string>();
                    lineItem.Add(itemObject.Code);
                    lineItem.Add(row.PriceType);
                    lineItem.Add(itemObject.Description);
                    lineItem.Add(row.Location);
                    lineItem.Add(row.LotNumber);
                    lineItem.Add(row.Cases.ToString());
                    lineItem.Add(row.Pieces.ToString());
                    lineItem.Add(row.PricePerPiece.ToString());
                    lineItem.Add(row.LineAmount.ToString());

                    LinedItemCollection.Add(lineItem);
                }
            }

            //Load search items

            SearchItemCollection.Clear();
            var itemModelObject = new ItemModel();
            var itemModelList = itemModelObject.FetchAll();

            foreach (ItemModel item in itemModelList)
            {
                SearchItemCollection.Add(item);
            }


        }
    }
}