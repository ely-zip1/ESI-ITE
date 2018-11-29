using ESI_ITE.Data_Access;
using ESI_ITE.Model;
using ESI_ITE.View.PrintingTemplate;
using ESI_ITE.ViewModel.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;

namespace ESI_ITE.ViewModel
{
    public class InvoicingPickListViewModel : ViewModelBase, IDataErrorInfo
    {
        public InvoicingPickListViewModel()
        {
            applyFilterCommand = new DelegateCommand(filterOrders);
            showAllCommand = new DelegateCommand(ShowAllOrders);
            itemCheckedCommand = new DelegateCommand(toggleItemChecked);
            itemUncheckedCommand = new DelegateCommand(toggleItemChecked);
            selectAllCommand = new DelegateCommand(toggleSelectAll);
            allocateStocksCommand = new DelegateCommand(StartAllocation);
            deallocateStocksCommand = new DelegateCommand(StartDeallocation);
            printPicklistCommand = new DelegateCommand(StartPrinting);

            Load();
        }

        #region Properties

        DataAccess db = new DataAccess();

        private List<SalesOrderModel> salesOrderList = new List<SalesOrderModel>();
        public List<SalesOrderModel> SalesOrderList
        {
            get
            {
                return salesOrderList;
            }
            set
            {
                salesOrderList = value;
                OnPropertyChanged();
            }
        }

        private List<PickListHeaderModel> pickHeadList = new List<PickListHeaderModel>();
        public List<PickListHeaderModel> PickHeadList
        {
            get
            {
                return pickHeadList;
            }
            set
            {
                pickHeadList = value;
                OnPropertyChanged();
            }
        }


        private ObservableCollection<PickListSO> picklistSalesOrdersCollection = new ObservableCollection<PickListSO>();
        public ObservableCollection<PickListSO> PicklistSalesOrdersCollection
        {
            get
            {
                return picklistSalesOrdersCollection;
            }
            set
            {
                picklistSalesOrdersCollection = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<List<string>> districtCollection = new ObservableCollection<List<string>>();
        public ObservableCollection<List<string>> DistrictCollection
        {
            get
            {
                return districtCollection;
            }
            set
            {
                districtCollection = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<List<string>> picklistCollection = new ObservableCollection<List<string>>();
        public ObservableCollection<List<string>> PicklistCollection
        {
            get
            {
                return picklistCollection;
            }
            set
            {
                picklistCollection = value;
                OnPropertyChanged();
            }
        }


        private PickListSO selectedOrder;
        public PickListSO SelectedOrder
        {
            get
            {
                return selectedOrder;
            }
            set
            {
                selectedOrder = value;
                OnPropertyChanged();
            }
        }

        private List<string> selectedDistrict;
        public List<string> SelectedDistrict
        {
            get
            {
                return selectedDistrict;
            }
            set
            {
                selectedDistrict = value;
                OnPropertyChanged();
            }
        }

        private List<string> selectedPickList;
        public List<string> SelectedPickList
        {
            get
            {
                return selectedPickList;
            }
            set
            {
                selectedPickList = value;
                OnPropertyChanged();
            }
        }



        private int selectedIndexOrder = -1;
        public int SelectedIndexOrder
        {
            get
            {
                return selectedIndexOrder;
            }
            set
            {
                selectedIndexOrder = value;
                OnPropertyChanged();
            }
        }

        private int selectedIndexDistrict = 0;
        public int SelectedIndexDistrict
        {
            get
            {
                return selectedIndexDistrict;
            }
            set
            {
                selectedIndexDistrict = value;
                OnPropertyChanged();
            }
        }

        private int selectedIndexPickList = 0;
        public int SelectedIndexPickList
        {
            get
            {
                return selectedIndexPickList;
            }
            set
            {
                selectedIndexPickList = value;
                IsDeallocateable = (value > 0) ? true : false;
                OnPropertyChanged();
            }
        }

        private string orderBeginDate;
        public string OrderBeginDate
        {
            get
            {
                return orderBeginDate;
            }
            set
            {
                orderBeginDate = value;
                OnPropertyChanged();
            }
        }

        private string orderEndDate;
        public string OrderEndDate
        {
            get
            {
                return orderEndDate;
            }
            set
            {
                orderEndDate = value;
                OnPropertyChanged();
            }
        }

        private bool hasAllSelected = false;
        public bool HasAllSelected
        {
            get
            {
                return hasAllSelected;
            }
            set
            {
                if (value != hasAllSelected)
                {
                    hasAllSelected = value;
                    OnPropertyChanged();
                    SelectAllToggled();
                }
            }
        }

        private List<string> AllocationQueries = new List<string>();

        private string informationUpdates;
        public string InformationUpdates
        {
            get
            {
                return informationUpdates;
            }
            set
            {
                informationUpdates = value;
                OnPropertyChanged();
            }
        }

        private List<List<InventoryMaster2Model>> InventoryMasterList = new List<List<InventoryMaster2Model>>();// temporary/internal database

        private bool hasAllocationErrors = false;

        #region Commands

        private DelegateCommand applyFilterCommand;
        public ICommand ApplyFilterCommand
        {
            get
            {
                return applyFilterCommand;
            }
        }

        private DelegateCommand showAllCommand;
        public ICommand ShowAllCommand
        {
            get
            {
                return showAllCommand;
            }
        }

        private DelegateCommand allocateStocksCommand;
        public ICommand AllocateStocksCommand
        {
            get
            {
                return allocateStocksCommand;
            }
        }

        private DelegateCommand deallocateStocksCommand;
        public ICommand DeallocateStocksCommand
        {
            get
            {
                return deallocateStocksCommand;
            }
        }

        private DelegateCommand printPicklistCommand;
        public ICommand PrintPicklistCommand
        {
            get
            {
                return printPicklistCommand;
            }
        }

        private DelegateCommand itemCheckedCommand;
        public ICommand ItemCheckedCommand
        {
            get
            {
                return itemCheckedCommand;
            }
        }

        private DelegateCommand itemUncheckedCommand;
        public ICommand ItemUncheckedCommand
        {
            get
            {
                return itemUncheckedCommand;
            }
        }

        private DelegateCommand selectAllCommand;
        public ICommand SelectAllCommand
        {
            get
            {
                return selectAllCommand;
            }
        }

        #endregion

        #region Flags

        private bool isFirstLoad = true;

        public bool isItemSelected;

        private bool isAllocateable = false;
        public bool IsAllocateable
        {
            get
            {
                return isAllocateable;
            }
            set
            {
                isAllocateable = value;
                OnPropertyChanged();
            }
        }

        private bool isDeallocateable;
        public bool IsDeallocateable
        {
            get
            {
                return isDeallocateable;
            }
            set
            {
                isDeallocateable = value;
                OnPropertyChanged();
            }
        }

        private bool isWaitingVisible = false;
        public bool IsWaitingVisible
        {
            get
            {
                return isWaitingVisible;
            }
            set
            {
                isWaitingVisible = value;
                OnPropertyChanged();
            }
        }

        private bool isFilterEnabled = true;
        public bool IsFilterEnabled
        {
            get
            {
                return isFilterEnabled;
            }
            set
            {
                isFilterEnabled = value;
                OnPropertyChanged();
            }
        }

        private bool isShowAllEnabled = true;
        public bool IsShowAllEnabled
        {
            get
            {
                return isShowAllEnabled;
            }
            set
            {
                isShowAllEnabled = value;
                OnPropertyChanged();
            }
        }

        private bool IsAllocating = false;

        #endregion

        #endregion

        private void Load()
        {
            //Sales Orders
            LoadOrders();
            ShowAllOrders();

            SelectedIndexOrder = -1;

            //Districts
            var district = new DistrictModel();
            var districts = district.FetchAll();

            DistrictCollection.Clear();
            foreach (var row in districts)
            {
                var tempDistrict = (DistrictModel)row;
                var _salesman = new SalesmanModel();

                _salesman = (SalesmanModel)_salesman.Fetch(tempDistrict.Salesman.ToString(), "id");

                if (_salesman.SalesmanName == "VACANT")
                    continue;

                var tempList = new List<string>();
                tempList.Add(_salesman.SalesmanName);
                tempList.Add(tempDistrict.DistrictNumber);

                DistrictCollection.Add(tempList);
            }

            DistrictCollection.OrderBy(x => x[0]);
            DistrictCollection.Insert(0, new List<string> { "All", "" });

            SelectedIndexDistrict = 0;

            OrderBeginDate = DateTime.Now.AddDays(-1).ToString("MMddyyyy");
            OrderEndDate = DateTime.Now.ToString("MMddyyyy");

            //PickList
            var pick = new PickListHeaderModel();
            var pickList = pick.FetchAll();

            PickHeadList.Clear();
            PicklistCollection.Clear();
            PicklistCollection.Add(new List<string> { "", "", "" });
            foreach (var row in pickList)
            {
                var tempPick = (PickListHeaderModel)row;
                var user = db.Select("select username from users where id = '" + tempPick.UserId + "'");
                var list = new List<string>();
                list.Add(tempPick.HeaderNumber);
                list.Add(user);
                list.Add(tempPick.Pickdate.ToString());

                PicklistCollection.Add(list);
            }

            isFirstLoad = false;
        }

        private void LoadOrders()
        {
            var salesOrder = new SalesOrderModel();
            var orderList = salesOrder.FetchAll();

            SalesOrderList.Clear();

            foreach (var row in orderList)
            {
                var order = (SalesOrderModel)row;
                SalesOrderList.Add(order);
            }
        }

        private void ShowAllOrders()
        {
            PicklistSalesOrdersCollection.Clear();
            foreach (var row in SalesOrderList)
            {
                if ((row.IsServed == false && row.IsPicked == false) || (row.IsServed == true && row.IsPicked == false))
                    PicklistSalesOrdersCollection.Add(FillPicklistSO(row));
            }
        }

        private void filterOrders()
        {
            var district = new DistrictModel();

            var startDate = new DateTime();
            var endDate = new DateTime();


            if (OrderEndDate.Contains("/"))
                endDate = DateTime.ParseExact(OrderEndDate, "MM/dd/yyyy", CultureInfo.InvariantCulture);
            else
                endDate = DateTime.ParseExact(OrderEndDate, "MMddyyyy", CultureInfo.InvariantCulture);

            if (OrderBeginDate.Contains("/"))
                startDate = DateTime.ParseExact(OrderBeginDate, "MM/dd/yyyy", CultureInfo.InvariantCulture);
            else
                startDate = DateTime.ParseExact(OrderBeginDate, "MMddyyyy", CultureInfo.InvariantCulture);


            if (SelectedIndexDistrict <= 0)
                ApplyFilter(null, startDate, endDate);
            else
            {
                district = (DistrictModel)district.Fetch(SelectedDistrict[1], "code");
                ApplyFilter(district, startDate, endDate);
            }

        }

        private void ApplyFilter(DistrictModel district, DateTime startDate, DateTime endDate)
        {
            PicklistSalesOrdersCollection.Clear();

            foreach (var row in SalesOrderList)
            {
                if (district == null)
                {
                    if (startDate <= row.OrderDate && row.OrderDate <= endDate)
                    {
                        PicklistSalesOrdersCollection.Add(FillPicklistSO(row));
                    }
                }
                else if (row.DistrictId == district.DistrictId)
                {
                    if (startDate <= row.OrderDate && row.OrderDate <= endDate)
                    {
                        PicklistSalesOrdersCollection.Add(FillPicklistSO(row));
                    }
                }
            }
        }

        private PickListSO FillPicklistSO(SalesOrderModel order)
        {
            var picklistSO = new PickListSO();

            var _customer = new CustomerModel();
            var _term = new TermModel();
            var _district = new DistrictModel();

            _customer = (CustomerModel)_customer.Fetch(order.CustomerID.ToString(), "id");
            _term = (TermModel)_term.Fetch(order.TermId.ToString(), "id");
            _district = (DistrictModel)_district.Fetch(order.DistrictId.ToString(), "id");

            picklistSO.IsSelected = false;
            picklistSO.OrderId = order.OrderId;
            picklistSO.SoNumber = order.OrderNumber;
            picklistSO.CustomerName = _customer.CustomerName;
            picklistSO.OrderAmount = order.OrderAmount;
            picklistSO.SoDate = order.OrderDate;

            if (order.IsServed == false && order.IsPicked == false)
                picklistSO.Status = "New Order";
            else if (order.IsServed && order.IsPicked == false)
                picklistSO.Status = "Partially Served";

            picklistSO.Term = _term.TermDescription;
            picklistSO.Address = _customer.AddressCity;
            //picklistSO.Address = _customer.AddressMain + ", " + _customer.AddressCity + ", " + _customer.AddressProvince;
            picklistSO.District = _district.DistrictNumber;

            return picklistSO;
        }

        private void SelectAllToggled()
        {
            if (HasAllSelected)
            {
                foreach (var row in PicklistSalesOrdersCollection)
                {
                    row.IsSelected = true;
                    if (IsAllocating)
                        IsAllocateable = false;
                    else
                        IsAllocateable = true;
                }
            }
            else
            {
                if (isItemSelected == false)
                    foreach (var row in PicklistSalesOrdersCollection)
                    {
                        row.IsSelected = false;
                        IsAllocateable = false;
                    }
            }
        }

        private void toggleSelectAll()
        {
            isItemSelected = false;
        }

        private void toggleItemChecked()
        {
            isItemSelected = true;

            var checkCounter = 0;
            foreach (var row in PicklistSalesOrdersCollection)
            {
                if (row.IsSelected)
                    checkCounter++;
            }

            if (checkCounter == PicklistSalesOrdersCollection.Count)
                HasAllSelected = true;
            else
            {
                HasAllSelected = false;
            }

            if (checkCounter > 0)
            {
                if (IsAllocating)
                    IsAllocateable = false;
                else
                    IsAllocateable = true;
            }
            else
                IsAllocateable = false;
        }

        #region Stock Allocation
        //
        // Asynchronous
        //

        private void StartAllocation()
        {
            var haveItems = false;

            foreach (var order in PicklistSalesOrdersCollection)
            {
                if (order.IsSelected)
                {
                    var itemCount = db.Count("select count(*) from inventory_dummy_2 where order_id = '" + order.OrderId + "'");
                    if (itemCount > 0)
                    {
                        haveItems = true;
                        break;
                    }
                }
            }

            if (haveItems)
            {
                IsFilterEnabled = false;
                IsShowAllEnabled = false;
                IsAllocating = true;
                HasAllSelected = false;

                CallAllocateStocks();

                IsWaitingVisible = true;
                InformationUpdates = "Allocating Stocks. Please Wait . . .";
            }
            else
            {
                MessageBox.Show("There are no items to be allocated.");
            }
        }

        private async void CallAllocateStocks()
        {
            AllocationQueries.Clear();

            var pickNumber = new PickListNumberModel();
            var pickHead = new PickListHeaderModel();

            pickHead.HeaderNumber = pickNumber.GenerateNumber();
            pickHead.UserId = MyGlobals.LoggedUser.Id;
            pickHead.Pickdate = DateTime.Now;
            pickHead.IsSuccessful = true;
            pickHead.IsAssigned = false;
            pickHead.IsGatepassPrinted = false;
            pickHead.GatepassId = null;

            pickHead.AddNew(pickHead);

            var result = await AllocateStocksAsync(pickHead);

            var order = new SalesOrderModel();

            if (MyGlobals.hasTransactionError == false)
            {
                //
                // set orders as picked
                //
                foreach (var i in result)
                {
                    order = (SalesOrderModel)order.Fetch(i.Value, "code");
                    order.IsPicked = true;

                    order.UpdateItem(order);

                    PicklistSalesOrdersCollection.RemoveAt(0);
                }
                LoadOrders();
                ShowAllOrders();

                //
                //Add new Picklist
                //
                var newPickList = new List<string>();
                newPickList.Add(pickHead.HeaderNumber);
                newPickList.Add(MyGlobals.LoggedUser.Username);
                newPickList.Add(pickHead.Pickdate.ToString());

                PicklistCollection.Add(newPickList);
            }
            else
            {
                pickHead = pickHead.FetchLatest();
                pickHead.DeleteItem(pickHead);

                MyGlobals.hasTransactionError = false;
            }

            IsWaitingVisible = false;
            InformationUpdates = string.Empty;

            IsAllocating = false;
            IsFilterEnabled = true;
            IsShowAllEnabled = true;

            MyGlobals.InvoicingMainVM.AllocationMaintenanceView = new View.InvoicingAllocationMaintenanceView();
            MyGlobals.InvoicingMainVM.AssignmentView = new View.InvoicingAssignmentView();
            MyGlobals.InvoicingMainVM.GatepassView = new View.InvoicingGatepassView();
            MyGlobals.InvoicingMainVM.DispatchView = new View.InvoicingDispatchView();
        }

        private Task<Dictionary<int, string>> AllocateStocksAsync(PickListHeaderModel pickHead)
        {
            return Task.Factory.StartNew(() => allocateStocks(pickHead));
        }

        private Dictionary<int, string> allocateStocks(PickListHeaderModel pickHead)
        {
            InventoryMasterList.Clear();
            var picknumber = new PickListNumberModel();
            Dictionary<int, string> ordersToBeRemoved = new Dictionary<int, string>();

            pickHead.Fetch(pickHead.HeaderNumber, "code");

            AllocationQueries.Add(picknumber.GetAddQuery());

            var index = 0;
            foreach (var order in PicklistSalesOrdersCollection)
            {
                if (order.IsSelected)
                {
                    var inventoryDummy = new InventoryDummy2Model();
                    var orderItems = inventoryDummy.FetchPerOrder(order.SoNumber);

                    var groupedOrderItems = orderItems.GroupBy(d => d.ItemCode);

                    //
                    // populate temporary database with inventory items
                    //
                    foreach (var itemGroup in groupedOrderItems)
                    {
                        var currentItem = new ItemModel();
                        currentItem = (ItemModel)currentItem.Fetch(itemGroup.First().ItemCode, "code");

                        var inventoryMaster = new InventoryMaster2Model();
                        var inventoryItemList = inventoryMaster.FetchUnexpired(currentItem.ItemId);

                        if (inventoryItemList.Count > 0)
                        {
                            InventoryMasterList.Add(inventoryItemList);
                        }
                    }

                    ordersToBeRemoved.Add(index, order.SoNumber);
                    allocatePerOrder(order.SoNumber, pickHead);
                }
                index++;
            }

            db.RunMySqlTransaction(AllocationQueries, null, null);

            return ordersToBeRemoved;
        }

        private void allocatePerOrder(string soNumber, PickListHeaderModel pickhead)
        {
            var dummy = new InventoryDummy2Model();
            var dummyList = dummy.FetchPerOrder(soNumber);

            //dummyList.Sort((x, y) => x.ItemCode.CompareTo(y.ItemCode));

            foreach (var row in dummyList)
            {
                allocatePerItem(row, pickhead);
            }
        }

        private void allocatePerItem(InventoryDummy2Model orderedItem, PickListHeaderModel pickhead)
        {
            pickhead = (PickListHeaderModel)pickhead.Fetch(pickhead.HeaderNumber, "code");

            var item = new ItemModel();
            item = (ItemModel)item.Fetch(orderedItem.ItemCode, "code");

            var inventory = new InventoryMaster2Model();
            var inventoryItems = new List<InventoryMaster2Model>();
            var masterIndex = 0;
            //inventoryItems = inventory.FetchInStockItem(item.ItemId);

            //Find ItemSet in Temporary Inventory
            if (InventoryMasterList.Count > 0)
            {
                var i = 0;
                foreach (var inventoryItem in InventoryMasterList)
                {
                    if (inventoryItem.Count > 0)
                    {
                        if (inventoryItem[0].ItemId == item.ItemId)
                        {
                            inventoryItems = inventoryItem;
                            masterIndex = i;
                            break;
                        }
                    }
                    i++;
                }
            }

            //if (inventoryItems.Sum(inv => inv.Cases + inv.Pieces) <= 0)
            //    return;

            var piecePerCase = item.PackSize;

            var orderedCases = orderedItem.Cases;
            var orderedPieces = orderedItem.Pieces;
            var orderInPieces = ConvertToPieces(orderedCases, orderedPieces, piecePerCase);

            var allocatedCases = 0;
            var allocatedPieces = 0;
            var remainingOrderCases = orderedCases;
            var remainingOrderPieces = orderedPieces;

            var remainingOrderInPieces = orderInPieces; // required quantity

            var picklistLine = new PickListLineModel();
            var allocateStocks = new AllocatedStocksModel();
            var _allocatedItemInPieces = 0;

            inventoryItems.OrderBy(i => i.ExpirationDate).ThenBy(i => i.Cases).ThenBy(i => i.Pieces);

            #region perPiece
            //if (InventoryMasterList.Count > 0)
            //{
            //    if (inventoryItems.Count > 0)
            //    {
            //        _allocatedItemInPieces = 0;

            //        var i = 0;
            //        foreach (var inventoryItem in inventoryItems.ToList())
            //        {
            //            _allocatedItemInPieces = 0;

            //            if (inventoryItem.Cases == 0 && inventoryItem.Pieces == 0)
            //            {
            //                i++;
            //                continue;
            //            }

            //            var currentItemInPieces = ConvertToPieces(inventoryItem.Cases, inventoryItem.Pieces, piecePerCase);

            //            if (remainingOrderInPieces > 0)
            //            {
            //                if (currentItemInPieces > 0)
            //                {
            //                    if (currentItemInPieces >= remainingOrderInPieces)
            //                    {
            //                        _allocatedItemInPieces += remainingOrderInPieces;
            //                        currentItemInPieces -= remainingOrderInPieces;

            //                        //remainingOrderInPieces = orderInPieces - allocatedInPieces;
            //                        remainingOrderInPieces = 0;

            //                        inventoryItem.Cases = currentItemInPieces / piecePerCase;
            //                        inventoryItem.Pieces = currentItemInPieces % piecePerCase;

            //                        allocateStocks.PickHeadId = pickhead.Id;
            //                        allocateStocks.InventoryDummyId = orderedItem.Id;

            //                        if (_allocatedItemInPieces >= piecePerCase)
            //                            allocateStocks.Cases = _allocatedItemInPieces / piecePerCase;
            //                        else
            //                            allocateStocks.Cases = 0;

            //                        allocateStocks.Pieces = _allocatedItemInPieces - (allocateStocks.Cases * piecePerCase);
            //                        allocateStocks.InventoryId = inventoryItem.Id;

            //                        AllocationQueries.Add(allocateStocks.GetAddQuery(allocateStocks));
            //                        AllocationQueries.Add(inventoryItem.GetUpdateQuery(inventoryItem));

            //                        InventoryMasterList[masterIndex][i] = inventoryItem;
            //                        break;
            //                    }
            //                    else
            //                    {
            //                        _allocatedItemInPieces += currentItemInPieces;
            //                        currentItemInPieces = 0;

            //                        remainingOrderInPieces = orderInPieces - _allocatedItemInPieces;

            //                        inventoryItem.Cases = 0;
            //                        inventoryItem.Pieces = 0;

            //                        allocateStocks.PickHeadId = pickhead.Id;
            //                        allocateStocks.InventoryDummyId = orderedItem.Id;

            //                        if (_allocatedItemInPieces >= piecePerCase)
            //                            allocateStocks.Cases = _allocatedItemInPieces / piecePerCase;
            //                        else
            //                            allocateStocks.Cases = 0;

            //                        allocateStocks.Pieces = _allocatedItemInPieces - (allocateStocks.Cases * piecePerCase);
            //                        allocateStocks.InventoryId = inventoryItem.Id;

            //                        AllocationQueries.Add(allocateStocks.GetAddQuery(allocateStocks));
            //                        AllocationQueries.Add(inventoryItem.GetUpdateQuery(inventoryItem));

            //                        InventoryMasterList[masterIndex][i] = inventoryItem;
            //                    }
            //                }
            //            }
            //            else
            //                break;

            //            i++;
            //        }

            //        picklistLine.PickListHeaderId = pickhead.GenerateId();
            //        picklistLine.InventoryDummyId = orderedItem.Id;

            //        var orderObj = new SalesOrderModel();
            //        orderObj = (SalesOrderModel)orderObj.Fetch(orderedItem.OrderNumber, "code");

            //        picklistLine.OrderId = orderObj.OrderId;

            //        if (_allocatedItemInPieces >= piecePerCase)
            //            picklistLine.AllocatedCases = _allocatedItemInPieces / piecePerCase;
            //        else
            //            picklistLine.AllocatedCases = 0;

            //        picklistLine.AllocatedPieces = _allocatedItemInPieces % piecePerCase;

            //        if (_allocatedItemInPieces < orderInPieces)
            //            picklistLine.IsCritical = true;
            //        else
            //            picklistLine.IsCritical = false;

            //        AllocationQueries.Add(picklistLine.GetAddQuery(picklistLine));

            //    }
            //}
            #endregion

            if (InventoryMasterList.Count > 0)
            {
                if (inventoryItems.Count > 0)
                {
                    var i = 0;

                    //allocate cases
                    foreach (var inventoryItem in inventoryItems.ToList())
                    {
                        allocatedCases = 0;
                        allocatedPieces = 0;

                        if (inventoryItem.Cases <= 0)
                        {
                            i++;
                            continue;
                        }

                        if (remainingOrderCases > 0)
                        {
                            if (inventoryItem.Cases >= remainingOrderCases)
                            {
                                allocatedCases += remainingOrderCases;
                                remainingOrderCases = 0;
                                inventoryItem.Cases -= remainingOrderCases;

                                allocateStocks.PickHeadId = pickhead.Id;
                                allocateStocks.InventoryDummyId = orderedItem.Id;

                                allocateStocks.Cases = allocatedCases;
                                allocateStocks.Pieces = 0;
                                allocateStocks.InventoryId = inventoryItem.Id;

                                AllocationQueries.Add(allocateStocks.GetAddQuery(allocateStocks));
                                AllocationQueries.Add(inventoryItem.GetUpdateQuery(inventoryItem));

                                InventoryMasterList[masterIndex][i] = inventoryItem;
                                break;
                            }
                            else if (inventoryItem.Cases > 0)
                            {
                                allocatedCases += inventoryItem.Cases;
                                remainingOrderCases -= inventoryItem.Cases;
                                inventoryItem.Cases = 0;

                                allocateStocks.PickHeadId = pickhead.Id;
                                allocateStocks.InventoryDummyId = orderedItem.Id;

                                allocateStocks.Cases = allocatedCases;
                                allocateStocks.Pieces = 0;
                                allocateStocks.InventoryId = inventoryItem.Id;

                                AllocationQueries.Add(allocateStocks.GetAddQuery(allocateStocks));
                                AllocationQueries.Add(inventoryItem.GetUpdateQuery(inventoryItem));

                                InventoryMasterList[masterIndex][i] = inventoryItem;
                            }

                            i++;
                        }
                        else
                        {
                            break;
                        }
                    }

                    //allocate pieces
                    i = 0;
                    foreach (var inventoryItem in inventoryItems.ToList())
                    {
                        allocatedCases = 0;
                        allocatedPieces = 0;

                        if (inventoryItem.Pieces <= 0)
                        {
                            i++;
                            continue;
                        }

                        if (remainingOrderPieces > 0)
                        {
                            if (inventoryItem.Pieces >= remainingOrderPieces)
                            {
                                allocatedPieces += remainingOrderPieces;
                                remainingOrderPieces = 0;
                                inventoryItem.Pieces -= remainingOrderPieces;

                                allocateStocks.PickHeadId = pickhead.Id;
                                allocateStocks.InventoryDummyId = orderedItem.Id;

                                allocateStocks.Cases = 0;
                                allocateStocks.Pieces = allocatedPieces;
                                allocateStocks.InventoryId = inventoryItem.Id;

                                AllocationQueries.Add(allocateStocks.GetAddQuery(allocateStocks));
                                AllocationQueries.Add(inventoryItem.GetUpdateQuery(inventoryItem));

                                InventoryMasterList[masterIndex][i] = inventoryItem;
                                break;
                            }
                            else if (inventoryItem.Pieces > 0)
                            {
                                allocatedPieces += inventoryItem.Pieces;
                                remainingOrderPieces -= inventoryItem.Pieces;
                                inventoryItem.Pieces = 0;

                                allocateStocks.PickHeadId = pickhead.Id;
                                allocateStocks.InventoryDummyId = orderedItem.Id;

                                allocateStocks.Cases = 0;
                                allocateStocks.Pieces = allocatedPieces;
                                allocateStocks.InventoryId = inventoryItem.Id;

                                AllocationQueries.Add(allocateStocks.GetAddQuery(allocateStocks));
                                AllocationQueries.Add(inventoryItem.GetUpdateQuery(inventoryItem));

                                InventoryMasterList[masterIndex][i] = inventoryItem;
                            }

                            i++;
                        }
                        else
                        {
                            break;
                        }
                    }

                    var orderObj = new SalesOrderModel();
                    orderObj = (SalesOrderModel)orderObj.Fetch(orderedItem.OrderNumber, "code");

                    picklistLine.PickListHeaderId = pickhead.GenerateId();
                    picklistLine.InventoryDummyId = orderedItem.Id;
                    picklistLine.OrderId = orderObj.OrderId;
                    picklistLine.AllocatedCases = orderedCases - remainingOrderCases;
                    picklistLine.AllocatedPieces = orderedPieces - remainingOrderPieces;

                    if (picklistLine.AllocatedCases > 0 || picklistLine.AllocatedPieces > 0)
                    {
                        if (picklistLine.AllocatedCases < orderedCases || picklistLine.AllocatedPieces < orderedPieces)
                            picklistLine.IsCritical = true;
                        else
                            picklistLine.IsCritical = false;

                        AllocationQueries.Add(picklistLine.GetAddQuery(picklistLine));
                    }
                }
            }
        }

        #endregion

        #region Stock Deallocation

        private void StartDeallocation()
        {
            IsAllocating = true;
            IsAllocateable = false;
            HasAllSelected = false;

            CallDeallocateStocks();

            IsWaitingVisible = true;
            InformationUpdates = "Deallocating Stocks. Please Wait . . .";
        }

        private async void CallDeallocateStocks()
        {
            await DeallocateStocksAsync();

            IsWaitingVisible = false;
            InformationUpdates = string.Empty;

            ShowAllOrders();

            PicklistCollection.RemoveAt(SelectedIndexPickList);
            IsAllocating = false;

            MyGlobals.InvoicingMainVM.AllocationMaintenanceView = new View.InvoicingAllocationMaintenanceView();
            MyGlobals.InvoicingMainVM.AssignmentView = new View.InvoicingAssignmentView();
            MyGlobals.InvoicingMainVM.GatepassView = new View.InvoicingGatepassView();
            MyGlobals.InvoicingMainVM.DispatchView = new View.InvoicingDispatchView();

        }

        private Task DeallocateStocksAsync()
        {
            return Task.Factory.StartNew(() => deallocateStocks());
        }

        private void deallocateStocks()
        {
            //Thread.Sleep(5000);
            var pickHead = new PickListHeaderModel();
            var pickLine = new PickListLineModel();
            var allocatedStocks = new AllocatedStocksModel();
            var inventory = new InventoryMaster2Model();
            var transactionString = new List<string>();
            var item = new ItemModel();
            var dummy = new InventoryDummy2Model();
            var inventoryItem = new InventoryMaster2Model();
            var order = new SalesOrderModel();
            var piecePerCase = 0;
            int stockInPieces = 0;
            int inventoryItemInPieces = 0;
            int totalPieces = 0;
            var previousOrder = string.Empty;
            var deallocatedOrders = new List<string>();

            pickHead = (PickListHeaderModel)pickHead.Fetch(SelectedPickList[0], "code");

            var picklineList = pickLine.FetchPerPickHead(pickHead.Id.ToString());

            foreach (var line in picklineList)
            {
                dummy = (InventoryDummy2Model)dummy.Fetch(line.InventoryDummyId.ToString(), "id");

                if (line.AllocatedCases > 0 || line.AllocatedPieces > 0)
                {
                    item = (ItemModel)item.Fetch(dummy.ItemCode, "code");
                    piecePerCase = item.PackSize;

                    var stockList = allocatedStocks.FetchPerPickLine(pickHead.Id, dummy.Id);

                    foreach (var stock in stockList)
                    {
                        inventoryItem = (InventoryMaster2Model)inventoryItem.Fetch(stock.InventoryId.ToString(), "id");

                        //stockInPieces = (stock.Cases * piecePerCase) + stock.Pieces;
                        //inventoryItemInPieces = (inventoryItem.Cases * piecePerCase) + inventoryItem.Pieces;

                        //totalPieces = stockInPieces + inventoryItemInPieces;

                        //if (totalPieces >= piecePerCase)
                        //{
                        //    inventoryItem.Cases = totalPieces / piecePerCase;
                        //}
                        //else
                        //{
                        //    inventoryItem.Cases = 0;
                        //}
                        inventoryItem.Cases += stock.Cases;
                        inventoryItem.Pieces += stock.Pieces;

                        transactionString.Add(inventoryItem.GetUpdateQuery(inventoryItem));//update inventory
                    }
                }

                if (previousOrder != dummy.OrderNumber)
                {
                    order = (SalesOrderModel)order.Fetch(dummy.OrderNumber, "code");
                    order.IsPicked = false;
                    transactionString.Add(order.GetUpdateQuery(order));

                    previousOrder = order.OrderNumber;
                    deallocatedOrders.Add(previousOrder);
                }

            }
            transactionString.Add(pickHead.GetDeleteQuery(pickHead));

            db.RunMySqlTransaction(transactionString, null, null);

            LoadOrders();
        }

        #endregion

        private int ConvertToPieces(int cases, int pieces, int piecePerCase)
        {
            var itemInPieces = 0;
            itemInPieces = (cases * piecePerCase) + pieces;
            return itemInPieces;
        }

        #region Printing

        private void StartPrinting()
        {
            if (SelectedIndexPickList > 0)
                CallPrinting();
        }

        private async void CallPrinting()
        {
            var result = await PicklistPrintingAsync();

            MyGlobals.printingDoc = result;

            MyGlobals.PrintingParent = MyGlobals.InvoicingVM.SelectedPage;
            MyGlobals.InvoicingVM.SelectedPage = new PrintingMainPageView();
            //printingVM.FixedDoc = (FixedDocument)result;
        }

        private Task<FixedDocument> PicklistPrintingAsync()
        {
            return Task.Factory.StartNew(() => PicklistPrinting());
        }

        private FixedDocument PicklistPrinting()
        {
            FixedDocument fixedDoc = null;
            Application.Current.Dispatcher.Invoke(() =>
            {
                // Your Code Goes here
                fixedDoc = new FixedDocument();
                //var vmList = new List<PicklistPrintTemplateViewModel>();
                var pick = new PickListLineModel();
                var pickhead = new PickListHeaderModel();

                pickhead = (PickListHeaderModel)pickhead.Fetch(SelectedPickList[0], "code");

                var pickList = pick.FetchPerPickHead(pickhead.Id.ToString());

                InventoryDummy2Model inventoryDummy = new InventoryDummy2Model();

                SalesOrderModel order = new SalesOrderModel();
                CustomerModel customer = new CustomerModel();
                TermModel term = new TermModel();
                FixedPage fixedPage = new FixedPage();
                Grid grid = new Grid();
                PicklistPrintTemplateViewModel templateVM = new PicklistPrintTemplateViewModel();

                var listHeight = 0;
                var tempItemList = new List<List<string>>();
                var orderNumberList = new List<string>();
                var tempOrdersList = new List<List<string>>();
                var pageNumber = 1;
                var isFirstPage = true;
                var totalOrderCases = 0;
                var totalOrderPieces = 0;
                var totalAllocatedCases = 0;
                var totalAllocatedPieces = 0;

                foreach (var pickLine in pickList)
                {
                    if (pickLine.AllocatedCases + pickLine.AllocatedPieces > 0)
                    {
                        inventoryDummy = (InventoryDummy2Model)inventoryDummy.Fetch(pickLine.InventoryDummyId.ToString(), "id");
                        order = (SalesOrderModel)order.Fetch(inventoryDummy.OrderNumber, "code");
                        customer = (CustomerModel)customer.Fetch(order.CustomerID.ToString(), "id");
                        term = (TermModel)term.Fetch(order.TermId.ToString(), "id");

                        var isUpdated = false;
                        if (tempItemList.Count > 0)
                        {
                            foreach (var content in tempItemList)
                            {
                                if (content[0] == inventoryDummy.ItemCode)
                                {
                                    var _orderedCases = int.Parse(content[3]);
                                    var _orderedPieces = int.Parse(content[4]);
                                    var _allocCases = int.Parse(content[5]);
                                    var _allocPieces = int.Parse(content[6]);

                                    content[3] = (_orderedCases + inventoryDummy.Cases).ToString();
                                    content[4] = (_orderedPieces + inventoryDummy.Pieces).ToString();
                                    content[5] = (_allocCases + pickLine.AllocatedCases).ToString();
                                    content[6] = (_allocPieces + pickLine.AllocatedPieces).ToString();

                                    totalOrderCases += inventoryDummy.Cases;
                                    totalOrderPieces += inventoryDummy.Pieces;
                                    totalAllocatedCases += pickLine.AllocatedCases;
                                    totalAllocatedPieces += pickLine.AllocatedPieces;

                                    isUpdated = true;
                                    break;
                                }
                                else
                                {
                                    isUpdated = false;
                                }
                            }
                        }

                        if (isUpdated)
                            continue;

                        var item = new List<string>();
                        item.Add(inventoryDummy.ItemCode);
                        item.Add(inventoryDummy.ItemDescription);
                        item.Add(inventoryDummy.Location);
                        item.Add(inventoryDummy.Cases.ToString());
                        item.Add(inventoryDummy.Pieces.ToString());
                        item.Add(pickLine.AllocatedCases.ToString());
                        item.Add(pickLine.AllocatedPieces.ToString());
                        item.Add(pickLine.IsCritical ? "**" : "");

                        tempItemList.Add(item);

                        totalOrderCases += inventoryDummy.Cases;
                        totalOrderPieces += inventoryDummy.Pieces;
                        totalAllocatedCases += pickLine.AllocatedCases;
                        totalAllocatedPieces += pickLine.AllocatedPieces;


                        //Orders
                        var hasDuplicateOrder = false;

                        if (tempOrdersList.Count > 0)
                        {
                            foreach (var _order in orderNumberList)
                            {
                                if (_order == order.OrderNumber)
                                {
                                    hasDuplicateOrder = true;
                                    break;
                                }
                                else
                                {
                                    hasDuplicateOrder = false;
                                }
                            }

                            if (hasDuplicateOrder == false)
                            {
                                var orderItem = new List<string>();
                                orderItem.Add(order.OrderNumber);
                                orderItem.Add(term.TermCode);
                                orderItem.Add(customer.CustomerName);

                                tempOrdersList.Add(orderItem);
                                orderNumberList.Add(order.OrderNumber);
                            }
                        }
                        else
                        {
                            var orderItem = new List<string>();
                            orderItem.Add(order.OrderNumber);
                            orderItem.Add(term.TermCode);
                            orderItem.Add(customer.CustomerName);

                            tempOrdersList.Add(orderItem);
                            orderNumberList.Add(order.OrderNumber);
                        }
                    }
                }

                foreach (var item in tempItemList)
                {
                    if (int.Parse(item[5]) + int.Parse(item[6]) <= 0)
                        continue;

                    if (item[1].Length >= 45)
                        listHeight += 32;
                    else
                        listHeight += 17;

                    if (listHeight >= 850 || isFirstPage)
                    {
                        isFirstPage = false;

                        if (listHeight >= 850)
                            listHeight = 0;

                        List<object> newPage = CreateNewPage(pageNumber++);
                        fixedDoc.Pages.Add((PageContent)newPage[1]);
                        templateVM = (PicklistPrintTemplateViewModel)newPage[0];
                    }

                    templateVM.ItemList.Add(item);
                }

                templateVM.TotalOrderedCases = totalOrderCases.ToString();
                templateVM.TotalOrderedPieces = totalOrderPieces.ToString();
                templateVM.TotalAllocatedCases = totalAllocatedCases.ToString();
                templateVM.TotalAllocatedPieces = totalAllocatedPieces.ToString();

                if ((850 - listHeight) > 30)
                {
                    templateVM.IsTotalVisible = true;
                    listHeight += 30;
                }
                else
                {
                    listHeight = 0;
                    List<object> newPage = CreateNewPage(pageNumber++);
                    fixedDoc.Pages.Add((PageContent)newPage[1]);
                    templateVM = (PicklistPrintTemplateViewModel)newPage[0];

                    templateVM.IsTotalVisible = true;
                    listHeight += 30;
                }

                foreach (var orderItem in tempOrdersList)
                {
                    int q = 0;
                    if ((850 - listHeight) < 17)
                    {
                        listHeight = 30;
                        List<object> newPage = CreateNewPage(pageNumber++);
                        fixedDoc.Pages.Add((PageContent)newPage[1]);
                        templateVM = (PicklistPrintTemplateViewModel)newPage[0];
                    }
                    templateVM.OrdersList.Add(orderItem);
                    templateVM.IsOrderVisible = true;

                    listHeight += 17;
                    q++;
                }

            });
            return fixedDoc;
        }

        private List<object> CreateNewPage(int pageNumber)
        {
            var fixedPage = new FixedPage();
            var grid = new Grid();
            var templateView = new PicklistPrintTemplate();
            var templateVM = (PicklistPrintTemplateViewModel)templateView.DataContext;

            templateView.Width = 768;
            templateView.MinHeight = 100;

            fixedPage.Width = 768;
            fixedPage.Height = 1056;

            templateVM.User = MyGlobals.LoggedUser.Username;
            templateVM.Date = DateTime.Now.ToShortDateString();
            templateVM.Time = DateTime.Now.ToLongTimeString();
            templateVM.PageNumber = pageNumber.ToString();
            templateVM.PicklistNumber = SelectedPickList[0] + " " + SelectedPickList[1] + " " + DateTime.Parse(SelectedPickList[2]).ToShortDateString();
            grid.Children.Add(templateView);
            fixedPage.Children.Add(grid);

            var pageContent = new PageContent();
            pageContent.Child = fixedPage;

            var newPage = new List<object>();
            newPage.Add(templateVM);
            newPage.Add(pageContent);

            return newPage;
        }

        #endregion

        #region IDataErrorInfo Members
        public string this[string propertyName]
        {
            get
            {
                if (isFirstLoad)
                    return null;
                else
                    return GetValidationError(propertyName);
            }
        }

        public string Error
        {
            get
            {
                return null;
            }
        }

        public object Dispatcher
        {
            get; private set;
        }
        #endregion

        #region Validation

        string[] validProperties = { null, null };

        private void IsValid()
        {
            var x = 0;
            foreach (var row in validProperties)
            {
                if (!string.IsNullOrWhiteSpace(row))
                    x++;
            }

            if (x > 0)
                IsFilterEnabled = false;
            else
                ValidateFilterDate();
        }

        private void ResetValidProperties()
        {
            for (int x = 0; x < validProperties.Length; x++)
            {
                validProperties[x] = null;
            }
        }

        private void ValidateFilterDate()
        {
            var startDate = new DateTime();
            var endDate = new DateTime();

            if (OrderEndDate.Contains("/"))
                endDate = DateTime.ParseExact(OrderEndDate, "MM/dd/yyyy", CultureInfo.InvariantCulture);
            else if (OrderEndDate.Contains("_"))
                IsFilterEnabled = false;
            else
                endDate = DateTime.ParseExact(OrderEndDate, "MMddyyyy", CultureInfo.InvariantCulture);

            if (OrderBeginDate.Contains("/"))
                startDate = DateTime.ParseExact(OrderBeginDate, "MM/dd/yyyy", CultureInfo.InvariantCulture);
            else if (OrderBeginDate.Contains("_"))
                IsFilterEnabled = false;
            else
                startDate = DateTime.ParseExact(OrderBeginDate, "MMddyyyy", CultureInfo.InvariantCulture);

            if (endDate < startDate)
                IsFilterEnabled = false;
            else
                IsFilterEnabled = true;
        }

        private string GetValidationError(string propertyName)
        {
            string error = null;

            var startDate = new DateTime();
            var endDate = new DateTime();

            ResetValidProperties();

            switch (propertyName)
            {
                case "OrderBeginDate":
                    if (OrderBeginDate.Contains("/"))
                        try
                        {
                            startDate = DateTime.ParseExact(OrderBeginDate, "MM/dd/yyyy", CultureInfo.InvariantCulture);
                            error = null;
                        }
                        catch (Exception e)
                        {
                            error = "Invalid Date!";
                            validProperties[0] = "Error";
                        }
                    else
                        try
                        {
                            startDate = DateTime.ParseExact(OrderBeginDate, "MMddyyyy", CultureInfo.InvariantCulture);
                            error = null;
                        }
                        catch (Exception e)
                        {
                            error = "Invalid date!";
                            validProperties[0] = "Error";
                        }
                    break;

                case "OrderEndDate":
                    if (OrderEndDate.Contains("/"))
                        try
                        {
                            endDate = DateTime.ParseExact(OrderEndDate, "MM/dd/yyyy", CultureInfo.InvariantCulture);
                            error = null;
                        }
                        catch (Exception e)
                        {
                            error = "Invalid Date!";
                            validProperties[1] = "Error";
                        }
                    else
                        try
                        {
                            endDate = DateTime.ParseExact(OrderEndDate, "MMddyyyy", CultureInfo.InvariantCulture);
                            error = null;
                        }
                        catch (Exception e)
                        {
                            error = "Invalid date!";
                            validProperties[1] = "Error";
                        }
                    break;
            }
            IsValid();

            return error;
        }

        #endregion
    }

    public class PickListSO : ViewModelBase
    {
        private bool isSelected;
        public bool IsSelected
        {
            get
            {
                return isSelected;
            }
            set
            {
                isSelected = value;
                OnPropertyChanged();
            }
        }

        private int orderId;
        public int OrderId
        {
            get
            {
                return orderId;
            }
            set
            {
                orderId = value;
            }
        }

        private string soNumber;
        public string SoNumber
        {
            get
            {
                return soNumber;
            }
            set
            {
                soNumber = value;
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
            }
        }

        private DateTime soDate;
        public DateTime SoDate
        {
            get
            {
                return soDate;
            }
            set
            {
                soDate = value;
            }
        }

        private string status;
        public string Status
        {
            get
            {
                return status;
            }
            set
            {
                status = value;
            }
        }

        private string term;
        public string Term
        {
            get
            {
                return term;
            }
            set
            {
                term = value;
            }
        }

        private string address;
        public string Address
        {
            get
            {
                return address;
            }
            set
            {
                address = value;
            }
        }

        private string district;
        public string District
        {
            get
            {
                return district;
            }
            set
            {
                district = value;
            }
        }
    }
}
