﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using ESI_ITE.Model;
using ESI_ITE.ViewModel.Command;

namespace ESI_ITE.ViewModel.CNDN
{
    public class DNLineItemViewModel : ViewModelBase
    {
        public DNLineItemViewModel()
        {
            exitCommand = new DelegateCommand(Exit);
            listValuesCommand = new DelegateCommand(ListItems);
            deleteLineCommand = new DelegateCommand(DeleteLineItem);
            selectItemSearchCommand = new DelegateCommand(SelectItem);
            addItemCommand = new DelegateCommand(DuplicateCheck);

            Load();
        }

        #region Properties

        private DebitNoteHeaderModel dnHeaderObject = new DebitNoteHeaderModel();

        private ObservableCollection<DNLineItem> linedItemCollection = new ObservableCollection<DNLineItem>();
        public ObservableCollection<DNLineItem> LinedItemCollection
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

        private ObservableCollection<PriceTypeModel> priceTypeList = new ObservableCollection<PriceTypeModel>();
        public ObservableCollection<PriceTypeModel> PriceTypeList
        {
            get
            {
                return priceTypeList;
            }
            set
            {
                priceTypeList = value;
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

        private LineItem selectedLinedItem;
        public LineItem SelectedLinedItem
        {
            get
            {
                return selectedLinedItem;
            }
            set
            {
                selectedLinedItem = value;
                OnPropertyChanged();
                if (SelectedIndexLinedItem > -1)
                    LinedItemSelected();
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

        private string dnNumber;
        public string DnNumber
        {
            get
            {
                return dnNumber;
            }
            set
            {
                dnNumber = value;
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


        private ItemModel selectedItem;
        public ItemModel SelectedItem
        {
            get
            {
                return selectedItem;
            }
            set
            {
                selectedItem = value;
            }
        }

        private string itemCode;
        public string ItemCode
        {
            get
            {
                return itemCode;
            }
            set
            {
                itemCode = value;
                OnPropertyChanged();

                if (value.Length == 6)
                    ItemCodeChanged(value);
            }
        }

        private string itemDescription;
        public string ItemDescription
        {
            get
            {
                return itemDescription;
            }
            set
            {
                itemDescription = value;
                OnPropertyChanged();
            }
        }

        private string priceType;
        public string PriceType
        {
            get
            {
                return priceType;
            }
            set
            {
                priceType = value;
                OnPropertyChanged();
            }
        }

        private PriceTypeModel selectedPriceType;
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
                OnPropertyChanged();
            }
        }

        private ReturnTypeModel selectedReturnType;
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

        private List<PriceSellingModel> sellingPriceList = new List<PriceSellingModel>();
        public List<PriceSellingModel> SellingPriceList
        {
            get
            {
                return sellingPriceList;
            }
            set
            {
                sellingPriceList = value;
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

        private decimal orderAmount;
        public decimal OrderAmount
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

        private bool isItemSearchVisible = false;
        public bool IsItemSearchVisible
        {
            get
            {
                return isItemSearchVisible;
            }
            set
            {
                isItemSearchVisible = value;
                OnPropertyChanged();
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

        private DelegateCommand listValuesCommand;
        public ICommand ListValuesCommand
        {
            get
            {
                return listValuesCommand;
            }
        }

        private DelegateCommand deleteLineCommand;
        public ICommand DeleteLineCommand
        {
            get
            {
                return deleteLineCommand;
            }
        }

        private DelegateCommand selectItemSearchCommand;
        public ICommand SelectItemSearchCommand
        {
            get
            {
                return selectItemSearchCommand;
            }
        }

        private DelegateCommand addItemCommand;
        public ICommand AddItemCommand
        {
            get
            {
                return addItemCommand;
            }
        }

        private bool IsFirstLoad = true;

        #endregion


        private void Load()
        {
            dnHeaderObject = (DebitNoteHeaderModel)dnHeaderObject.Fetch(MyGlobals.SelectedCNDNTransaction, "code");

            var customerObject = new CustomerModel();
            var districtObject = new DistrictModel();
            var salesmanObject = new SalesmanModel();
            var termObject = new TermModel();

            customerObject = (CustomerModel)customerObject.Fetch(dnHeaderObject.CustomerId.ToString(), "id");
            districtObject = (DistrictModel)districtObject.Fetch(customerObject.DistrictId.ToString(), "id");
            salesmanObject = (SalesmanModel)salesmanObject.Fetch(districtObject.Salesman.ToString(), "id");
            termObject = (TermModel)termObject.Fetch(customerObject.TermId.ToString(), "id");


            CustomerNumber = customerObject.CustomerNumber;
            CustomerName = customerObject.CustomerName;
            SalesmanNumber = salesmanObject.SalesmanNumber;
            SalesmanName = salesmanObject.SalesmanName;
            TermCode = termObject.TermCode;
            TermDescription = termObject.TermDescription;
            PriceUsed = MyGlobals.CnDnEntryOptionsVM.SelectedPrice;
            DnNumber = dnHeaderObject.DnNumber;
            TotalCases = dnHeaderObject.TotalCases.ToString();
            TotalPieces = dnHeaderObject.TotalPieces.ToString();
            OrderAmount = dnHeaderObject.DnAmount;
            TaxRate = MyGlobals.DebitNoteEntryVM.TaxRate;
            WarehouseCode = MyGlobals.DebitNoteEntryVM.SelectedWarehouse.Code;

            var dnLineObject = new DebitNoteLineModel();
            var dnLineList = dnLineObject.FetchPerDebitNoteHead(dnHeaderObject.Id.ToString());

            if (dnLineList.Count > 0)
            {
                foreach (var row in dnLineList)
                {
                    var itemObject = new ItemModel();
                    itemObject = (ItemModel)itemObject.Fetch(row.ItemId.ToString(), "id");

                    var lineItem = new DNLineItem();
                    lineItem.ItemCode = itemObject.Code;
                    lineItem.PriceType = row.PriceType;
                    lineItem.Description = itemObject.Description;
                    lineItem.Location = row.Location;
                    lineItem.Cases = row.Cases.ToString();
                    lineItem.Pieces = row.Pieces.ToString();
                    lineItem.PricePerPiece = row.PricePerPiece.ToString();
                    lineItem.LineAmount = row.LineAmount.ToString();

                    LinedItemCollection.Add(lineItem);
                    OrderAmount += row.LineAmount;
                }
                OrderAmount = Math.Round(OrderAmount, 2);
            }

            var sellingPriceObject = new PriceSellingModel();
            foreach (PriceSellingModel row in sellingPriceObject.FetchAll())
            {
                SellingPriceList.Add(row);
            }

            //Load search items

            SearchItemCollection.Clear();
            var itemModelObject = new ItemModel();
            var itemModelList = itemModelObject.FetchAll();

            foreach (ItemModel item in itemModelList)
            {
                SearchItemCollection.Add(item);
            }

            var priceTypeObject = new PriceTypeModel();
            SelectedPriceType = priceTypeObject.Fetch(dnHeaderObject.PriceTypeId.ToString(), "id") as PriceTypeModel;
            PriceType = SelectedPriceType.Code;

            var returnObject = new ReturnTypeModel();
            SelectedReturnType = returnObject.Fetch(dnHeaderObject.ReturnCodeId.ToString(), "id") as ReturnTypeModel;
            Location = SelectedReturnType.Code;

            IsFirstLoad = false;
        }


        private void ItemCodeChanged(string value)
        {
            var matchedItem = new ItemModel();
            foreach (var row in SearchItemCollection)
            {
                if (row.Code == value)
                {
                    matchedItem = row;
                    SelectedItem = row;
                    break;
                }
            }
            if (matchedItem.Code.Length < 6)
            {
                return;
            }

            ItemDescription = SelectedItem.Description;
            //var pricetypeObject = new PriceTypeModel();
            //foreach (var row in pricetypeObject.FetchPerItem(matchedItem.ItemId.ToString()))
            //{
            //    PriceTypeList.Add(row);
            //}

            var sellingPriceObject = new PriceSellingModel();
            foreach (var row in sellingPriceObject.FetchCurrentPrice(matchedItem.ItemId.ToString(), "id"))
            {
                SellingPriceList.Add(row);
            }

            Cases = 0;
            Pieces = 0;

            decimal sellingPrice = 0;
            foreach (var row in SellingPriceList)
            {
                if (row.PriceTypeId == SelectedPriceType.PriceTypeId)
                    sellingPrice = row.SellingPrice;
            }

            PricePerPiece = Math.Round((sellingPrice / matchedItem.PackSize), 2).ToString();

            TaxRate = matchedItem.Taxrate.ToString();

            var warehouseObject = new WareHouseModel();
            warehouseObject = (WareHouseModel)warehouseObject.Fetch(matchedItem.Warehouse.ToString(), "id");
        }


        private void SelectItem()
        {
            if (IsItemSearchVisible)
                IsItemSearchVisible = false;
            else
                IsItemSearchVisible = true;

            ItemCode = SelectedSearchItem.Code;
        }


        private void DeleteLineItem()
        {
            var itemIndex = 0;
            var isDeleted = false;
            foreach (var row in LinedItemCollection)
            {
                if (ItemCode == row.ItemCode)
                {
                    var result = MessageBox.Show("Do you want to delete this entry?", "Delete Entry", MessageBoxButton.YesNo);
                    if (result == MessageBoxResult.Yes)
                    {
                        var itemObject = new ItemModel();
                        itemObject = (ItemModel)itemObject.Fetch(ItemCode, "code");

                        var dnLineObject = new DebitNoteLineModel();
                        dnLineObject.DeleteItem(dnHeaderObject.Id.ToString(), itemObject.ItemId.ToString());

                        isDeleted = true;
                    }
                    break;
                }
                itemIndex++;
            }

            if (isDeleted)
            {
                OrderAmount -= decimal.Parse(LinedItemCollection[itemIndex].LineAmount);
                LinedItemCollection.RemoveAt(itemIndex);
                Clear();
            }
        }


        private void ListItems()
        {
            if (IsItemSearchVisible)
                IsItemSearchVisible = false;
            else
                IsItemSearchVisible = true;
        }


        private void DuplicateCheck()
        {
            var hasDuplicate = false;
            foreach (var row in LinedItemCollection)
            {
                if (ItemCode == row.ItemCode)
                {
                    hasDuplicate = true;

                    var result = MessageBox.Show("Do you want to update this entry?", "Duplicate Entry", MessageBoxButton.YesNo);
                    if (result == MessageBoxResult.Yes)
                    {
                        UpdateItem();
                    }
                    break;
                }
            }

            if (hasDuplicate == false)
            {
                AddItem();
            }
        }

        private void UpdateItem()
        {
            var dnLineObject = new DebitNoteLineModel();

            dnLineObject.DebitNoteHeadId = dnHeaderObject.Id;
            dnLineObject.ItemId = SelectedItem.ItemId;
            dnLineObject.PriceType = PriceType;
            dnLineObject.Cases = Cases;
            dnLineObject.Pieces = Pieces;
            dnLineObject.PricePerPiece = decimal.Parse(PricePerPiece);
            dnLineObject.Location = Location;
            dnLineObject.LineAmount = (((SelectedItem.PackSize * Cases) + Pieces) * decimal.Parse(PricePerPiece));

            dnLineObject.UpdateItem(dnLineObject);

            var itemIndex = 0;
            if (dnLineObject.Verify(dnHeaderObject.Id.ToString(), SelectedItem.ItemId.ToString()))
            {
                foreach (var row in LinedItemCollection)
                {
                    if (ItemCode == row.ItemCode)
                    {
                        break;
                    }
                    itemIndex++;
                }

                LinedItemCollection[itemIndex].Cases = Cases.ToString();
                LinedItemCollection[itemIndex].Pieces = Pieces.ToString();
                LinedItemCollection[itemIndex].LineAmount = dnLineObject.LineAmount.ToString();
                Clear();
            }
        }

        private void AddItem()
        {
            if (string.IsNullOrWhiteSpace(ItemCode))
            {
                //do nothing
            }
            else if (Cases == 0 && Pieces == 0)
            {
                MessageBox.Show("Please input the quantity.");
            }
            else
            {
                var dnLineObject = new DebitNoteLineModel();

                dnLineObject.DebitNoteHeadId = dnHeaderObject.Id;
                dnLineObject.ItemId = SelectedItem.ItemId;
                dnLineObject.PriceType = PriceType;
                dnLineObject.Cases = Cases;
                dnLineObject.Pieces = Pieces;
                dnLineObject.PricePerPiece = decimal.Parse(PricePerPiece);
                dnLineObject.Location = Location;
                dnLineObject.LineAmount = (((SelectedItem.PackSize * Cases) + Pieces) * decimal.Parse(PricePerPiece));

                dnLineObject.AddNew(dnLineObject);

                if (dnLineObject.Verify(dnHeaderObject.Id.ToString(), SelectedItem.ItemId.ToString()))
                {
                    var listItem = new DNLineItem();
                    listItem.ItemCode = SelectedItem.Code;
                    listItem.PriceType = PriceType;
                    listItem.Description = SelectedItem.Description;
                    listItem.Location = Location;
                    listItem.Cases = Cases.ToString();
                    listItem.Pieces = Pieces.ToString();
                    listItem.PricePerPiece = PricePerPiece;
                    listItem.LineAmount = (((SelectedItem.PackSize * Cases) + Pieces) * decimal.Parse(PricePerPiece)).ToString();

                    LinedItemCollection.Add(listItem);
                    SelectedIndexLinedItem = -1;
                    Clear();
                }
                else
                {
                    MessageBox.Show("Error adding item!");
                }
            }
        }

        private void Clear()
        {
            ItemCode = "";
            ItemDescription = "";
            Cases = 0;
            Pieces = 0;
            PricePerPiece = "";
        }

        private void LinedItemSelected()
        {
            var itemObject = new ItemModel();
            SelectedItem = (ItemModel)itemObject.Fetch(SelectedLinedItem.ItemCode, "code");

            ItemCode = SelectedLinedItem.ItemCode;
            ItemDescription = SelectedLinedItem.Description;
            Cases = int.Parse(SelectedLinedItem.Cases);
            Pieces = int.Parse(SelectedLinedItem.Pieces);
            PricePerPiece = SelectedLinedItem.PricePerPiece;
        }

        private void Exit()
        {
            MyGlobals.CnDnVM.SelectedPage = MyGlobals.DebitNoteEntryView;
        }
    }

    public class DNLineItem : ViewModelBase
    {
        private string itemCode;
        public string ItemCode
        {
            get
            {
                return itemCode;
            }
            set
            {
                itemCode = value;
                OnPropertyChanged();
            }
        }

        private string priceType;
        public string PriceType
        {
            get
            {
                return priceType;
            }
            set
            {
                priceType = value;
                OnPropertyChanged();
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
                OnPropertyChanged();
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
                OnPropertyChanged();
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
                OnPropertyChanged();
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

        private string lineAmount;
        public string LineAmount
        {
            get
            {
                return lineAmount;
            }
            set
            {
                lineAmount = value;
            }
        }

    }
}
