using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ESI_ITE.Model;
using ESI_ITE.Data_Access;
using ESI_ITE.ViewModel.Command;
using System.Windows.Input;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Threading;

namespace ESI_ITE.ViewModel
{
    public class SOLineItemViewModel : ViewModelBase, IDataErrorInfo
    {
        public SOLineItemViewModel()
        {
            closeCommand = new DelegateCommand(closePage);
            showItemSearchCommand = new DelegateCommand(toggleItemSearchVisibility);
            selectItemSearchCommand = new DelegateCommand(selectItem);
            addItemCommand = new DelegateCommand(addItem);
            deleteItemCommand = new DelegateCommand(deleteItem);

            Load();
        }

        #region Properties

        DataAccess db = new DataAccess();

        private SalesOrderModel salesOrder = new SalesOrderModel();
        private CustomerModel customer = new CustomerModel();
        private SalesmanModel salesman = new SalesmanModel();
        private DistrictModel district = new DistrictModel();
        private TermModel term = new TermModel();
        private PriceTypeModel priceType = new PriceTypeModel();
        private WareHouseModel warehouse = new WareHouseModel();

        //Collection
        private ObservableCollection<InventoryDummy2Model> datagridItems = new ObservableCollection<InventoryDummy2Model>();
        public ObservableCollection<InventoryDummy2Model> DatagridItems
        {
            get { return datagridItems; }
            set
            {
                datagridItems = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<ItemModel> itemCollection = new ObservableCollection<ItemModel>();
        public ObservableCollection<ItemModel> ItemCollection
        {
            get { return itemCollection; }
            set { itemCollection = value; }
        }

        private ObservableCollection<ItemModel> searchedItemCollection = new ObservableCollection<ItemModel>();
        public ObservableCollection<ItemModel> SearchedItemCollection
        {
            get { return searchedItemCollection; }
            set { searchedItemCollection = value; }
        }

        private ObservableCollection<PriceTypeModel> pricetypeCollection = new ObservableCollection<PriceTypeModel>();
        public ObservableCollection<PriceTypeModel> PricetypeCollection
        {
            get { return pricetypeCollection; }
            set { pricetypeCollection = value; }
        }


        private List<PriceTypeModel> pricetypeList = new List<PriceTypeModel>();
        public List<PriceTypeModel> PricetypeList
        {
            get { return pricetypeList; }
            set { pricetypeList = value; }
        }

        private List<LocationModel> locationList = new List<LocationModel>();
        public List<LocationModel> LocationList
        {
            get { return locationList; }
            set { locationList = value; }
        }


        //Selected Item
        private InventoryDummy2Model selectedDatagridItem = new InventoryDummy2Model();
        public InventoryDummy2Model SelectedDatagridItem
        {
            get { return selectedDatagridItem; }
            set
            {
                if (value != null)
                {
                    selectedDatagridItem = value;
                    OnPropertyChanged("SelectedDatagridItem");
                    SelectedItemChanged();
                }
            }
        }

        private ItemModel selectedSearchedItem = new ItemModel();
        public ItemModel SelectedSearchedItem
        {
            get { return selectedSearchedItem; }
            set
            {
                selectedSearchedItem = value;
                OnPropertyChanged("SelectedSearchedItem");
                SelectedSearchedItemChanged();
            }
        }

        private PriceTypeModel selectedPricetype;
        public PriceTypeModel SelectedPricetype
        {
            get { return selectedPricetype; }
            set
            {
                selectedPricetype = value;
                OnPropertyChanged();
            }
        }

        private LocationModel selectedLocation = new LocationModel();
        public LocationModel SelectedLocation
        {
            get { return selectedLocation; }
            set
            {
                selectedLocation = value;
                OnPropertyChanged("SelectedLocation");
                locationChanged();
            }
        }


        //Selected Index
        private int selectedIndexLineItem;
        public int SelectedIndexLineItem
        {
            get { return selectedIndexLineItem; }
            set
            {
                selectedIndexLineItem = value;
                OnPropertyChanged("SelectedIndexLineItem");
            }
        }

        private int selectedIndexSearchedItem;
        public int SelectedIndexSearchedItem
        {
            get { return selectedIndexSearchedItem; }
            set
            {
                selectedIndexSearchedItem = value;
                OnPropertyChanged("SelectedIndexSearchedItem");
            }
        }

        private int selectedIndexPricetype = -1;
        public int SelectedIndexPricetype
        {
            get { return selectedIndexPricetype; }
            set
            {
                selectedIndexPricetype = value;
                SelectedPriceTypeChanged();
                OnPropertyChanged("SelectedIndexPricetype");
            }
        }

        private int selectedIndexLocation;
        public int SelectedIndexLocation
        {
            get { return selectedIndexLocation; }
            set
            {
                selectedIndexLocation = value;
                OnPropertyChanged("SelectedIndexLocation");
            }
        }


        //Header
        private string txtCustomerNumber;
        public string TxtCustomerNumber
        {
            get { return txtCustomerNumber; }
            set
            {
                txtCustomerNumber = value;
                OnPropertyChanged("TxtCustomerNumber");
            }
        }

        private string txtCustomerName;
        public string TxtCustomerName
        {
            get { return txtCustomerName; }
            set
            {
                txtCustomerName = value;
                OnPropertyChanged("TxtCustomerName");
            }
        }

        private string txtSalesmanNumber;
        public string TxtSalesmanNumber
        {
            get { return txtSalesmanNumber; }
            set
            {
                txtSalesmanNumber = value;
                OnPropertyChanged("TxtSalesmanNumber");
            }
        }

        private string txtSalesmanName;
        public string TxtsalesmanName
        {
            get { return txtSalesmanName; }
            set
            {
                txtSalesmanName = value;
                OnPropertyChanged("TxtsalesmanName");
            }
        }

        private string txtTermCode;
        public string TxtTermCode
        {
            get { return txtTermCode; }
            set
            {
                txtTermCode = value;
                OnPropertyChanged("TxtTermCode");
            }
        }

        private string txtTermDescription;
        public string TxtTermDescription
        {
            get { return txtTermDescription; }
            set
            {
                txtTermDescription = value;
                OnPropertyChanged("TxtTermDescription");
            }
        }

        private string txtOrderNumber;
        public string TxtOrderNumber
        {
            get { return txtOrderNumber; }
            set
            {
                txtOrderNumber = value;
                OnPropertyChanged("TxtOrderNumber");
            }
        }

        private string txtTotalCases;
        public string TxtTotalCases
        {
            get { return txtTotalCases; }
            set
            {
                txtTotalCases = value;
                OnPropertyChanged("TxtTotalCases");
            }
        }

        private string txtTotalPieces;
        public string TxtTotalPieces
        {
            get { return txtTotalPieces; }
            set
            {
                txtTotalPieces = value;
                OnPropertyChanged("TxtTotalPieces");
            }
        }


        //Item Entry
        private string txtItemCode;
        public string TxtItemCode
        {
            get { return txtItemCode; }
            set
            {
                txtItemCode = value;
                ItemCodeChanged();
                OnPropertyChanged();
            }
        }

        private string txtItemDescription;
        public string TxtItemDescription
        {
            get { return txtItemDescription; }
            set
            {
                txtItemDescription = value;
                OnPropertyChanged();
            }
        }

        private string txtPT;
        public string TxtPT
        {
            get { return txtPT; }
            set
            {
                txtPT = value;
                OnPropertyChanged();
            }
        }

        //private string txtLC;
        //public string TxtLC
        //{
        //    get { return txtLC; }
        //    set
        //    {
        //        txtLC = value;
        //        OnPropertyChanged();
        //    }
        //}

        private string txtCases;
        public string TxtCases
        {
            get { return txtCases; }
            set
            {
                txtCases = value;
                //quantityChanged("Cases");
            }
        }

        private string txtPieces;
        public string TxtPieces
        {
            get { return txtPieces; }
            set
            {
                txtPieces = value;
                //quantityChanged("Pieces");
            }
        }

        private string txtUnitPrice;
        public string TxtUnitPrice
        {
            get { return txtUnitPrice; }
            set
            {
                txtUnitPrice = value;
                OnPropertyChanged();
            }
        }

        private string txtTaxRate;
        public string TxtTaxRate
        {
            get { return txtTaxRate; }
            set
            {
                txtTaxRate = value;
                OnPropertyChanged();
            }
        }

        private string txtWarehouse;
        public string TxtWarehouse
        {
            get { return txtWarehouse; }
            set
            {
                txtWarehouse = value;
                OnPropertyChanged("TxtWarehouse");
            }
        }

        private string txtLotNumber;
        public string TxtLotNumber
        {
            get { return txtLotNumber; }
            set
            {
                txtLotNumber = value;
                OnPropertyChanged("TxtLotNumber");
            }
        }

        private string txtOrderAmount;
        public string TxtOrderAmount
        {
            get { return txtOrderAmount; }
            set
            {
                txtOrderAmount = value;
                OnPropertyChanged("TxtOrderAmount");
            }
        }

        private string txtPesoDiscount;
        public string TxtPesoDiscount
        {
            get { return txtPesoDiscount; }
            set
            {
                txtPesoDiscount = value;
                OnPropertyChanged("TxtPesoDiscount");
            }
        }

        private string txtTruckingCharge;
        public string TxtTruckingCharge
        {
            get { return txtTruckingCharge; }
            set
            {
                txtTruckingCharge = value;
                OnPropertyChanged("TxtTruckingCharge");
            }
        }


        //Item Search
        private string txtSearchItemCode;
        public string TxtSearchItemCode
        {
            get { return txtSearchItemCode; }
            set
            {
                txtSearchItemCode = value;
                OnPropertyChanged("TxtSearchItemCode");
            }
        }

        private string txtSearchItemDescription;
        public string TxtSearchItemDescription
        {
            get { return txtSearchItemDescription; }
            set
            {
                txtSearchItemDescription = value;
                OnPropertyChanged("TxtSearchItemDescription");
                SearchItem(TxtSearchItemDescription);
            }
        }

        private string loadingMessage;
        public string LoadingMessage
        {
            get { return loadingMessage; }
            set
            {
                loadingMessage = value;
                OnPropertyChanged();
            }
        }



        #region Commands

        private DelegateCommand closeCommand;
        public ICommand CloseCommand
        {
            get { return closeCommand; }
        }

        private DelegateCommand showItemSearchCommand;
        public ICommand ShowItemSearchCommand
        {
            get { return showItemSearchCommand; }
        }

        private DelegateCommand selectItemSearchCommand;
        public ICommand SelectItemSearchCommand
        {
            get { return selectItemSearchCommand; }
        }

        private DelegateCommand addItemCommand;
        public ICommand AddItemCommand
        {
            get { return addItemCommand; }
        }

        private DelegateCommand deleteItemCommand;
        public ICommand DeleteItemCommand
        {
            get { return deleteItemCommand; }
        }

        private int blurIntensity = 0;
        public int BlurIntensity
        {
            get { return blurIntensity; }
            set
            {
                blurIntensity = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region Flags

        private bool isItemSearchVisible = false;
        public bool IsItemSearchVisible
        {
            get { return isItemSearchVisible; }
            set
            {
                isItemSearchVisible = value;
                OnPropertyChanged("IsItemSearchVisible");
            }
        }

        private bool isAddable;
        public bool IsAddable
        {
            get { return isAddable; }
            set
            {
                isAddable = value;
                OnPropertyChanged("IsAddable");
            }
        }

        private bool isFirstLoad = true;
        public bool IsFirstLoad
        {
            get { return isFirstLoad; }
            set
            {
                isFirstLoad = value;
                OnPropertyChanged("IsFirstLoad");
            }
        }

        private bool isItemsGridVisible = true;
        public bool IsItemsGridVisible
        {
            get { return isItemsGridVisible; }
            set
            {
                isItemsGridVisible = value;
                OnPropertyChanged();
            }
        }


        #endregion

        private ItemModel currentItem = new ItemModel();

        private List<PriceSellingModel> PriceList = new List<PriceSellingModel>();

        #endregion

        private void Load()
        {
            salesOrder = MyGlobals.SalesOrder;

            IsItemSearchVisible = false;

            //ITEMS
            var item = new ItemModel();
            var itemList = item.FetchAll();

            foreach (var row in itemList)
            {
                ItemCollection.Add(row as ItemModel);
                SearchedItemCollection.Add(row as ItemModel);
            }

            //LOCATION
            var location = new LocationModel();
            var locations = location.FetchAll();
            foreach (var row in locations)
            {
                LocationList.Add(row);
            }

            SelectedIndexLocation = -1;

            customer = (CustomerModel)customer.Fetch(salesOrder.CustomerID.ToString(), "id");
            district = (DistrictModel)district.Fetch(salesOrder.DistrictId.ToString(), "id");
            term = (TermModel)term.Fetch(salesOrder.TermId.ToString(), "id");
            priceType = (PriceTypeModel)priceType.Fetch(salesOrder.PriceId.ToString(), "id");
            salesman = (SalesmanModel)salesman.Fetch(district.Salesman.ToString(), "id");
            warehouse = (WareHouseModel)warehouse.Fetch(salesOrder.WarehouseId.ToString(), "id");

            TxtCustomerNumber = customer.CustomerNumber;
            TxtCustomerName = customer.CustomerName;

            TxtSalesmanNumber = salesman.SalesmanNumber;
            TxtsalesmanName = salesman.SalesmanName;

            TxtTermCode = term.TermCode;
            TxtTermDescription = term.TermDescription;

            TxtOrderNumber = salesOrder.OrderNumber;
            TxtTotalCases = salesOrder.Cases.ToString();
            TxtTotalPieces = salesOrder.Pieces.ToString();

            //PRICE TYPE
            //TxtPT = priceType.Code;
            var _pricetypeList = priceType.FetchAll();
            foreach (var row in _pricetypeList)
            {
                var pt = (PriceTypeModel)row;
                PricetypeList.Add(pt);
            }

            TxtTaxRate = customer.TaxRate.ToString();
            TxtWarehouse = warehouse.Code;

            CallItemLoad();
            IsItemsGridVisible = true;
            LoadingMessage = "Loading Items...";
        }

        private async void CallItemLoad()
        {
            List<InventoryDummy2Model> result = await LoadUpdateItemsAsync();

            DatagridItems.Clear();
            foreach (var row in result)
            {
                DatagridItems.Add(row);
            }

            computeTotalOrderAmount();

            IsItemsGridVisible = false;
        }

        private Task<List<InventoryDummy2Model>> LoadUpdateItemsAsync()
        {
            return Task.Factory.StartNew(() => LoadUpdateItems());
        }

        private List<InventoryDummy2Model> LoadUpdateItems()
        {
            //Thread.Sleep(5000);
            //DATAGRID ITEMS
            var dummy = new InventoryDummy2Model();
            var dummyList = dummy.FetchPerOrder(salesOrder.OrderNumber);

            priceType = (PriceTypeModel)priceType.Fetch(salesOrder.PriceId.ToString(), "id");

            foreach (var dummyItem in dummyList)
            {
                if (dummyItem.PriceType == priceType.Code)
                    break;
                else
                {
                    var _item = new ItemModel();
                    _item = (ItemModel)_item.Fetch(dummyItem.ItemCode, "code");

                    var price = decimal.Parse(db.Select("select selling_price from price_selling where item_id = '" + _item.ItemId + "' and pricetype_id = '" + priceType.PriceTypeId + "'"));

                    var piecePerCase = _item.PackSize * _item.PackSizeBO;
                    var pricePerPiece = price / piecePerCase;
                    var totalPieces = (dummyItem.Cases * piecePerCase) + dummyItem.Pieces;
                    var lineAmount = totalPieces * pricePerPiece;

                    dummy.Id = dummyItem.Id;
                    dummy.OrderNumber = salesOrder.OrderNumber;
                    dummy.PriceType = priceType.Code;
                    dummy.Location = dummyItem.Location;
                    dummy.ItemCode = dummyItem.ItemCode;
                    dummy.ItemDescription = dummyItem.ItemDescription;
                    dummy.Cases = dummyItem.Cases;
                    dummy.Pieces = dummyItem.Pieces;
                    dummy.PricePerPiece = pricePerPiece;
                    dummy.LineAmount = lineAmount;
                    dummy.LotNumber = dummyItem.LotNumber;

                    dummy.UpdateItem(dummy);

                }
            }
            dummyList = dummy.FetchPerOrder(salesOrder.OrderNumber);

            return dummyList;
        }

        private void SelectedItemChanged()
        {
            var item = new ItemModel();
            setCurrentItem((ItemModel)item.Fetch(SelectedDatagridItem.ItemCode, "code"));

            TxtCases = SelectedDatagridItem.Cases.ToString();
            TxtPieces = SelectedDatagridItem.Pieces.ToString();
            TxtLotNumber = SelectedDatagridItem.LotNumber;

            var counter = 0;
            foreach (var loc in LocationList)
            {
                if (loc.Code == SelectedDatagridItem.Location)
                {
                    SelectedIndexLocation = counter;
                    break;
                }
            }
        }

        private void SelectedSearchedItemChanged()
        {
            if (SelectedSearchedItem != null)
                TxtSearchItemCode = SelectedSearchedItem.Code;
        }

        private void SearchItem(string searchTerm)
        {
            SearchedItemCollection.Clear();
            foreach (var item in ItemCollection)
            {
                if (item.Description.ToLower().Contains(searchTerm.ToLower()))
                {
                    SearchedItemCollection.Add(item);
                }
            }
            if (SearchedItemCollection.Count > 0)
            {
                SelectedIndexSearchedItem = 0;
                SelectedSearchedItem = SearchedItemCollection.ElementAt(0);
            }
        }

        private void ItemCodeChanged()
        {
            resetValidProperties();
            if (TxtItemCode.Trim().Length == 6)
            {
                if (string.IsNullOrWhiteSpace(ValidateItemCode(TxtItemCode)))
                {
                    var itemObj = new ItemModel();
                    itemObj = (ItemModel)itemObj.Fetch(TxtItemCode, "code");

                    setCurrentItem(itemObj);
                }
            }
            IsFirstLoad = false;

        }

        private void ClearForm()
        {
            IsFirstLoad = true;
            TxtItemCode = "";
            TxtItemDescription = "";
            TxtCases = "0";
            TxtPieces = "0";
            TxtUnitPrice = "";
            TxtLotNumber = "";

            SelectedIndexLocation = -1;
        }

        private void closePage()
        {
            MyGlobals.SoViewModel.SelectedPage = MyGlobals.SalesOrderEntryPage;
            //(this as IDisposable).Dispose();
        }

        private void toggleItemSearchVisibility()
        {
            if (IsItemSearchVisible)
            {
                IsItemSearchVisible = false;
                BlurIntensity = 0;
            }
            else
            {
                IsItemSearchVisible = true;
                BlurIntensity = 10;
            }
        }

        private void setCurrentItem(ItemModel item)
        {
            currentItem = item;

            var _pricetypeList = priceType.FetchPerItem(currentItem.ItemId.ToString());

            PricetypeCollection.Clear();
            PriceList.Clear();

            var x = 0;
            var ptIndex = 0;
            foreach (var row in _pricetypeList)
            {
                PricetypeCollection.Add(row);
                if (row.Code == priceType.Code)
                    ptIndex = x;
                x++;
            }

            var priceSellingObj = new PriceSellingModel();
            PriceList = priceSellingObj.FetchCurrentPrice(currentItem.ItemId.ToString(), "id");

            TxtItemDescription = currentItem.Description;
            TxtTaxRate = currentItem.Taxrate.ToString();

            SelectedIndexPricetype = ptIndex;
        }

        private void SelectedPriceTypeChanged()
        {
            if(PricetypeCollection.Count > 0)
            {
                if(SelectedIndexPricetype > -1)
                {
                    TxtUnitPrice = PriceList[SelectedIndexPricetype].SellingPrice.ToString();
                }
            }
        }

        private void selectItem()
        {
            ClearForm();
            TxtItemCode = SelectedSearchedItem.Code;
            //setCurrentItem(SelectedSearchedItem);
            toggleItemSearchVisibility();
        }

        private void locationChanged()
        {
            if (SelectedLocation != null)
                if (!string.IsNullOrWhiteSpace(SelectedLocation.Code))
                    if (SelectedLocation.Code == "FR")
                    {
                        TxtUnitPrice = "0";
                    }
                    else
                    {
                        SelectedIndexPricetype = SelectedIndexPricetype;
                    }
        }

        private void quantityChanged(string unit)
        {
            switch (unit)
            {
                case "Cases":
                    //if ( IsClearForm == false )
                    //    IsFirstLoad = false;

                    //if ( CanBeDeleted )
                    //    CanBeAdded = true;
                    if (TxtPieces != "0" && TxtCases != "0")
                        if (!string.IsNullOrWhiteSpace(TxtCases) && string.IsNullOrWhiteSpace(TxtPieces))
                        {
                            OnPropertyChanged("TxtPieces");
                            validProperties[1] = null;
                        }
                        else if (!string.IsNullOrWhiteSpace(TxtPieces) && string.IsNullOrWhiteSpace(TxtCases))
                        {
                            OnPropertyChanged("TxtPieces");
                            validProperties[1] = null;
                        }
                        else if (!string.IsNullOrWhiteSpace(TxtCases) && !string.IsNullOrWhiteSpace(TxtPieces))
                        {
                            OnPropertyChanged("TxtPieces");
                            validProperties[1] = null;
                        }
                        else if ((string.IsNullOrWhiteSpace(TxtCases) && string.IsNullOrWhiteSpace(TxtPieces)) ||
                             int.Parse(TxtCases) == 0 && int.Parse(TxtPieces) == 0)
                        {
                            OnPropertyChanged("TxtPieces");
                            validProperties[1] = "Error";
                        }
                    break;

                case "Pieces":
                    //if ( IsClearForm == false )
                    //    IsFirstLoad = false;

                    //if ( CanBeDeleted )
                    //    CanBeAdded = true;
                    if (TxtPieces != "0" && TxtCases != "0")
                        if (!string.IsNullOrWhiteSpace(TxtPieces) && string.IsNullOrWhiteSpace(TxtCases))
                        {
                            OnPropertyChanged("TxtCases");
                            validProperties[1] = null;
                        }
                        else if (string.IsNullOrWhiteSpace(TxtPieces) && !string.IsNullOrWhiteSpace(TxtCases))
                        {
                            OnPropertyChanged("TxtCases");
                            validProperties[1] = null;
                        }
                        else if (!string.IsNullOrWhiteSpace(TxtCases) && !string.IsNullOrWhiteSpace(TxtPieces))
                        {
                            OnPropertyChanged("TxtCases");
                            validProperties[1] = null;
                        }
                        else if ((string.IsNullOrWhiteSpace(TxtCases) && string.IsNullOrWhiteSpace(TxtPieces)) ||
                            int.Parse(TxtCases) == 0 && int.Parse(TxtPieces) == 0)
                        {
                            OnPropertyChanged("TxtCases");
                            validProperties[1] = "Error";
                        }
                    break;
            }
        }

        private void addItem()
        {
            var isUpdateable = checkForDuplicates();
            var updateItem = false;

            var inventoryDummyItem = new InventoryDummy2Model();

            inventoryDummyItem.OrderNumber = salesOrder.OrderNumber;
            inventoryDummyItem.ItemCode = TxtItemCode;
            inventoryDummyItem.ItemDescription = TxtItemDescription;
            inventoryDummyItem.PriceType = SelectedPricetype.Code;
            inventoryDummyItem.Location = SelectedLocation.Code;

            var totalQuantity = refactorQuantity(currentItem.PackSize * currentItem.PackSizeBO);

            inventoryDummyItem.Cases = int.Parse(TxtCases);
            inventoryDummyItem.Pieces = int.Parse(TxtPieces);

            if (!string.IsNullOrWhiteSpace(TxtUnitPrice))
                inventoryDummyItem.PricePerPiece = decimal.Parse(TxtUnitPrice);
            else
                inventoryDummyItem.PricePerPiece = 0;

            inventoryDummyItem.LineAmount = inventoryDummyItem.PricePerPiece * totalQuantity;

            if (isUpdateable)
            {
                var dialogMessage = "Item already exists. Do you want to update it?";
                var dialogTitle = "Duplicate Item";
                MessageBoxResult result = MessageBox.Show(dialogMessage, dialogTitle, MessageBoxButton.YesNo);

                switch (result)
                {
                    case MessageBoxResult.Yes:

                        try
                        {
                            var sb = new StringBuilder();
                            sb.Append("update inventory_dummy_2 set ");
                            sb.Append("location = '" + SelectedLocation.Id + "', ");
                            sb.Append("cases = '" + inventoryDummyItem.Cases + "', ");
                            sb.Append("pieces = '" + inventoryDummyItem.Pieces + "', ");
                            sb.Append("line_amount = '" + inventoryDummyItem.LineAmount + "', ");
                            sb.Append("lot_number = '" + inventoryDummyItem.LotNumber + "' ");
                            sb.Append("where order_id = '" + salesOrder.OrderId + "' and item = '" + currentItem.ItemId + "'");

                            inventoryDummyItem.UpdateItem(sb.ToString());

                            DatagridItems.RemoveAt(SelectedIndexLineItem);
                            DatagridItems.Add(inventoryDummyItem);

                            ClearForm();
                        }
                        catch (Exception e)
                        {
                            MessageBox.Show(e.Message);
                        }
                        break;
                    case MessageBoxResult.No:
                        break;
                }
            }
            else
            {

                try
                {
                    inventoryDummyItem.AddNew(inventoryDummyItem);

                    DatagridItems.Add(inventoryDummyItem);

                    ClearForm();
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }
            }

            computeTotalOrderAmount();

        }

        private void computeTotalOrderAmount()
        {
            decimal totalOrderAmount = 0;
            var totalPieces = 0;
            var totalCases = 0;

            foreach (var datagridItem in DatagridItems)
            {
                totalOrderAmount += datagridItem.LineAmount;
                totalPieces += datagridItem.Pieces;
                totalCases += datagridItem.Cases;
            }

            TxtOrderAmount = totalOrderAmount.ToString("#,##0.00");
            TxtTotalCases = totalCases.ToString();
            TxtTotalPieces = totalPieces.ToString();

            salesOrder.OrderAmount = decimal.Parse(TxtOrderAmount);
            salesOrder.Cases = totalCases;
            salesOrder.Pieces = totalPieces;

            salesOrder.UpdateItem(salesOrder);

            MyGlobals.SalesOrder.OrderAmount = decimal.Parse(TxtOrderAmount);
            MyGlobals.SalesOrder.Pieces = totalPieces;
            MyGlobals.SalesOrder.Cases = totalCases;

            MyGlobals.SoEntryViewModel.OrderAmount = TxtOrderAmount;
            MyGlobals.SoEntryViewModel.TotalPieces = totalPieces.ToString();
            MyGlobals.SoEntryViewModel.TotalCases = totalCases.ToString();
        }

        private bool checkForDuplicates()
        {
            var isUpdateable = false;
            foreach (var dummyItem in DatagridItems)
            {
                if (dummyItem.ItemCode == TxtItemCode)
                {
                    isUpdateable = true;
                    break;
                }
                else
                {
                    isUpdateable = false;
                }
            }

            return isUpdateable;
        }

        private int refactorQuantity(int piecePerCase)
        {

            var cases = 0;
            var pieces = 0;

            if (string.IsNullOrWhiteSpace(TxtCases))
                cases = 0;
            else
                cases = int.Parse(TxtCases);

            if (string.IsNullOrWhiteSpace(TxtPieces))
                pieces = 0;
            else
                pieces = int.Parse(TxtPieces);

            if (pieces >= piecePerCase)
            {
                cases += pieces / piecePerCase;
                pieces = pieces % piecePerCase;
            }
            var totalQuantityInPieces = (cases * piecePerCase) + pieces;

            TxtCases = cases.ToString();
            TxtPieces = pieces.ToString();

            return totalQuantityInPieces;
        }

        private void deleteItem()
        {
            var dialogueMessage = "This will delete the current item. Do you want to proceed?";
            var dialogueTitle = "Confirm Deletion";

            var result = MessageBox.Show(dialogueMessage, dialogueTitle, MessageBoxButton.OKCancel);

            switch (result)
            {
                case MessageBoxResult.OK:
                    var dummy = new InventoryDummy2Model();
                    dummy.DeleteItem("delete from inventory_dummy_2 where order_id = '" + salesOrder.OrderId + "' and item = '" + currentItem.ItemId + "'");

                    foreach (var dummyItem in DatagridItems)
                    {
                        if (dummyItem.ItemCode == currentItem.Code)
                        {
                            DatagridItems.Remove(dummyItem);
                            break;
                        }
                    }

                    computeTotalOrderAmount();

                    ClearForm();
                    break;
                case MessageBoxResult.Cancel:
                    break;
            }
        }

        #region IDataErrorInfo

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
                if (IsFirstLoad)
                    return null;
                else
                    return GetValidationError(propertyName);
            }
        }

        #endregion

        #region Validation

        string[] validProperties = { "Error", "Error", "Error" };

        private void resetValidProperties()
        {
            int x = validProperties.Length;
            for (int i = 0; i < x; i++)
            {
                validProperties[i] = "Error";
            }
            IsAddable = false;
        }

        private void IsValid()
        {
            int fieldNumber = 0;
            int errorCounter = 0;

            Debug.WriteLine("VALIDATION");

            foreach (var field in validProperties)
            {
                if (string.IsNullOrWhiteSpace(field) == false)
                {
                    errorCounter++;
                    Debug.WriteLine("Error on field: " + fieldNumber);
                }
                fieldNumber++;
            }

            if (errorCounter > 0)
            {
                IsAddable = false;
            }
            else
            {
                if (SelectedLocation != null)
                    if (!string.IsNullOrWhiteSpace(SelectedLocation.Code))
                        IsAddable = true;
            }
        }

        private string GetValidationError(string propertyName)
        {
            string error = null;

            switch (propertyName)
            {
                case "TxtItemCode":
                    if (string.IsNullOrEmpty(TxtItemCode))
                    {
                        validProperties[0] = null;
                        break;
                    }

                    if (string.IsNullOrWhiteSpace(error))
                    {
                        error = ValidateItemCode(TxtItemCode);
                    }

                    if (string.IsNullOrWhiteSpace(error))
                        validProperties[0] = null;
                    else
                        validProperties[0] = "Error";
                    break;
                case "TxtCases":
                    if (string.IsNullOrWhiteSpace(TxtCases))
                    {
                        TxtCases = "0";
                    }

                    var pc = 0;
                    var cs = 0;

                    try
                    {
                        cs = int.Parse(TxtCases);
                    }
                    catch (Exception e)
                    {
                        error = "Invalid Input";
                    }

                    try
                    {
                        pc = int.Parse(TxtPieces);
                    }
                    catch (Exception e)
                    {
                        if (string.IsNullOrWhiteSpace(error))
                            OnPropertyChanged("TxtPieces");
                    }

                    if (string.IsNullOrWhiteSpace(error))
                    {
                        if (cs + pc > 0)
                        {
                            validProperties[1] = null;
                            if (!string.IsNullOrWhiteSpace(validProperties[2]))
                            {
                                OnPropertyChanged("TxtPieces");
                            }
                        }
                        else
                        {
                            error = "Quantity be empty!";
                            validProperties[1] = "error";
                        }
                    }
                    else
                    {
                        validProperties[1] = "error";
                    }

                    //if (string.IsNullOrWhiteSpace(error))
                    //{
                    //    error = ValidateQuantity();
                    //}

                    //if (string.IsNullOrWhiteSpace(error))
                    //    validProperties[1] = null;
                    //else
                    //    validProperties[1] = "Error";
                    break;
                case "TxtPieces":
                    if (string.IsNullOrWhiteSpace(TxtPieces))
                    {
                        TxtPieces = "0";
                    }

                    pc = 0;
                    cs = 0;

                    try
                    {
                        pc = int.Parse(TxtPieces);
                    }
                    catch (Exception e)
                    {
                        error = "Invalid Input";
                    }

                    try
                    {
                        cs = int.Parse(TxtCases);
                    }
                    catch (Exception e)
                    {
                        if (string.IsNullOrWhiteSpace(error))
                            OnPropertyChanged("TxtCases");
                    }

                    if (string.IsNullOrWhiteSpace(error))
                    {
                        if (cs + pc > 0)
                        {
                            validProperties[2] = null;
                            if (!string.IsNullOrWhiteSpace(validProperties[1]))
                            {
                                OnPropertyChanged("TxtCases");
                            }
                        }
                        else
                        {
                            error = "Quantity be empty!";
                            validProperties[2] = "error";
                        }
                    }
                    else
                    {
                        validProperties[2] = "error";
                    }

                    //if (string.IsNullOrWhiteSpace(error))
                    //{
                    //    error = ValidateQuantity();
                    //}

                    //if (string.IsNullOrWhiteSpace(error))
                    //    validProperties[1] = null;
                    //else
                    //    validProperties[1] = "Error";
                    break;
                case "SelectedLocation":
                    break;
            }

            IsValid();

            return error;
        }


        private string ValidateQuantity()
        {
            string error = null;
            int qtty;

            if (!string.IsNullOrWhiteSpace(TxtCases) && string.IsNullOrWhiteSpace(TxtPieces))
            {
                try
                {
                    qtty = int.Parse(TxtCases);

                    if (TxtCases == "0")
                    {
                        error = "Fields cannot be empty!";
                    }
                }
                catch (Exception e)
                {
                    error = "Invalid Input!";
                }
            }
            else if (string.IsNullOrWhiteSpace(TxtCases) && !string.IsNullOrWhiteSpace(TxtPieces))
            {
                try
                {
                    qtty = int.Parse(TxtPieces);

                    if (TxtPieces == "0")
                    {
                        error = "Fields cannot be empty!";
                    }
                }
                catch (Exception e)
                {
                    error = "Invalid Input!";
                }
            }
            else if (!string.IsNullOrWhiteSpace(TxtCases) && !string.IsNullOrWhiteSpace(TxtPieces))
            {
                try
                {
                    qtty = int.Parse(TxtCases);
                    qtty = int.Parse(TxtPieces);

                    if (TxtCases == "0" && TxtPieces == "0")
                    {
                        error = "Fields cannot be empty!";
                    }
                }
                catch (Exception e)
                {
                    error = "Invalid Input!";
                }
            }
            else if (string.IsNullOrWhiteSpace(TxtCases) && string.IsNullOrWhiteSpace(TxtPieces))
            {
                error = "Fields cannot be empty!";
            }

            return error;
        }

        private string ValidateNullOrEmpty(string propertyName, string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return propertyName + " cannot be empty!";
            else
                return null;
        }

        private string ValidateItemCode(string itemCode)
        {
            string error = null;

            var hasValue = false;
            foreach (var row in ItemCollection)
            {
                if (row.Code == itemCode)
                {
                    hasValue = true;
                    TxtItemDescription = row.Description;
                    break;
                }
            }

            if (!hasValue)
                error = "Invalid Item Code";

            return error;
        }

        #endregion
    }
}
