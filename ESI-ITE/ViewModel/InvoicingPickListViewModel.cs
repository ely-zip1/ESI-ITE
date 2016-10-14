using ESI_ITE.Data_Access;
using ESI_ITE.Model;
using ESI_ITE.ViewModel.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ESI_ITE.ViewModel
{
    public class InvoicingPickListViewModel: ViewModelBase, IDataErrorInfo
    {
        public InvoicingPickListViewModel( )
        {
            applyFilterCommand = new DelegateCommand(filterOrders);
            showAllCommand = new DelegateCommand(ShowAllOrders);
            itemCheckedCommand = new DelegateCommand(toggleItemChecked);
            itemUncheckedCommand = new DelegateCommand(toggleItemChecked);
            selectAllCommand = new DelegateCommand(toggleSelectAll);
            allocateStocksCommand = new DelegateCommand(allocateStocks);
            deallocateStocksCommand = new DelegateCommand(deallocateStocks);

            Load();
        }

        #region Properties

        DataAccess db = new DataAccess();

        private List<SalesOrderModel> salesOrderCollection = new List<SalesOrderModel>();
        public List<SalesOrderModel> SalesOrderCollection
        {
            get { return salesOrderCollection; }
            set
            {
                salesOrderCollection = value;
                OnPropertyChanged();
            }
        }

        private List<PickListHeaderModel> pickHeadCollection = new List<PickListHeaderModel>();
        public List<PickListHeaderModel> PickHeadCollection
        {
            get { return pickHeadCollection; }
            set
            {
                pickHeadCollection = value;
                OnPropertyChanged();
            }
        }


        private ObservableCollection<PickListSO> picklistSalesOrdersCollection = new ObservableCollection<PickListSO>();
        public ObservableCollection<PickListSO> PicklistSalesOrdersCollection
        {
            get { return picklistSalesOrdersCollection; }
            set
            {
                picklistSalesOrdersCollection = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<List<string>> districtCollection = new ObservableCollection<List<string>>();
        public ObservableCollection<List<string>> DistrictCollection
        {
            get { return districtCollection; }
            set
            {
                districtCollection = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<List<string>> picklistCollection = new ObservableCollection<List<string>>();
        public ObservableCollection<List<string>> PicklistCollection
        {
            get { return picklistCollection; }
            set
            {
                picklistCollection = value;
                OnPropertyChanged();
            }
        }


        private PickListSO selectedOrder;
        public PickListSO SelectedOrder
        {
            get { return selectedOrder; }
            set
            {
                selectedOrder = value;
                OnPropertyChanged();
            }
        }

        private List<string> selectedDistrict;
        public List<string> SelectedDistrict
        {
            get { return selectedDistrict; }
            set
            {
                selectedDistrict = value;
                OnPropertyChanged();
            }
        }

        private List<string> selectedPickList;
        public List<string> SelectedPickList
        {
            get { return selectedPickList; }
            set
            {
                selectedPickList = value;
                OnPropertyChanged();
            }
        }



        private int selectedIndexOrder = -1;
        public int SelectedIndexOrder
        {
            get { return selectedIndexOrder; }
            set
            {
                selectedIndexOrder = value;
                OnPropertyChanged();
            }
        }

        private int selectedIndexDistrict = 0;
        public int SelectedIndexDistrict
        {
            get { return selectedIndexDistrict; }
            set
            {
                selectedIndexDistrict = value;
                OnPropertyChanged();
            }
        }

        private int selectedIndexPickList = -1;
        public int SelectedIndexPickList
        {
            get { return selectedIndexPickList; }
            set
            {
                selectedIndexPickList = value;
                IsDeallocateable = (SelectedIndexPickList > 0) ? true : false;
                OnPropertyChanged();
            }
        }


        private string orderBeginDate;
        public string OrderBeginDate
        {
            get { return orderBeginDate; }
            set
            {
                orderBeginDate = value;
                OnPropertyChanged();
            }
        }

        private string orderEndDate;
        public string OrderEndDate
        {
            get { return orderEndDate; }
            set
            {
                orderEndDate = value;
                OnPropertyChanged();
            }
        }

        private bool hasAllSelected = false;
        public bool HasAllSelected
        {
            get { return hasAllSelected; }
            set
            {
                if ( value != hasAllSelected )
                {
                    hasAllSelected = value;
                    OnPropertyChanged();
                    SelectAllToggled();
                }
            }
        }

        private List<string> AllocationQueries = new List<string>();

        #region Commands

        private DelegateCommand applyFilterCommand;
        public ICommand ApplyFilterCommand
        {
            get { return applyFilterCommand; }
        }

        private DelegateCommand showAllCommand;
        public ICommand ShowAllCommand
        {
            get { return showAllCommand; }
        }

        private DelegateCommand allocateStocksCommand;
        public ICommand AllocateStocksCommand
        {
            get { return allocateStocksCommand; }
        }

        private DelegateCommand deallocateStocksCommand;
        public ICommand DeallocateStocksCommand
        {
            get { return deallocateStocksCommand; }
        }

        private DelegateCommand printPicklistCommand;
        public ICommand PrintPicklistCommand
        {
            get { return printPicklistCommand; }
        }

        private DelegateCommand itemCheckedCommand;
        public ICommand ItemCheckedCommand
        {
            get { return itemCheckedCommand; }
        }

        private DelegateCommand itemUncheckedCommand;
        public ICommand ItemUncheckedCommand
        {
            get { return itemUncheckedCommand; }
        }

        private DelegateCommand selectAllCommand;
        public ICommand SelectAllCommand
        {
            get { return selectAllCommand; }
        }

        #endregion

        #region Flags

        public bool isItemSelected;

        private bool isAllocateable = false;
        public bool IsAllocateable
        {
            get { return isAllocateable; }
            set
            {
                isAllocateable = value;
                OnPropertyChanged();
            }
        }

        private bool isDeallocateable;
        public bool IsDeallocateable
        {
            get { return isDeallocateable; }
            set
            {
                isDeallocateable = value;
                OnPropertyChanged();
            }
        }


        #endregion

        #endregion

        private void Load( )
        {
            //Sales Orders

            var salesOrder = new SalesOrderModel();
            var orderList = salesOrder.FetchAll();

            foreach ( var row in orderList )
            {
                var order = (SalesOrderModel)row;
                SalesOrderCollection.Add(order);
            }

            ShowAllOrders();

            SelectedIndexOrder = -1;

            //Districts
            var district = new DistrictModel();
            var districts = district.FetchAll();

            DistrictCollection.Clear();
            DistrictCollection.Add(new List<string> { "All", "" });
            foreach ( var row in districts )
            {
                var tempDistrict = (DistrictModel)row;
                var _salesman = new SalesmanModel();

                _salesman = (SalesmanModel)_salesman.Fetch(tempDistrict.Salesman.ToString(), "id");

                var tempList = new List<string>();
                tempList.Add(_salesman.SalesmanName);
                tempList.Add(tempDistrict.DistrictNumber);

                DistrictCollection.Add(tempList);
            }

            SelectedIndexDistrict = 0;

            OrderBeginDate = DateTime.Now.AddDays(-1).ToString("MMddyyyy");
            OrderEndDate = DateTime.Now.ToString("MMddyyyy");

            //PickList
            var pick = new PickListHeaderModel();
            var pickList = pick.FetchAll();

            PickHeadCollection.Clear();
            foreach ( var row in pickList )
            {
                var tempPick = (PickListHeaderModel)row;
                var user = db.Select("select username from users where id = '" + tempPick.UserId + "'");
                var list = new List<string>();
                list.Add(tempPick.HeaderNumber);
                list.Add(user);
                list.Add(tempPick.Pickdate.ToString());

                PicklistCollection.Add(list);
            }
        }

        private void ShowAllOrders( )
        {
            PicklistSalesOrdersCollection.Clear();
            foreach ( var row in SalesOrderCollection )
            {
                if ( (row.IsServed == false && row.IsPicked == false) || (row.IsServed == true && row.IsPicked == false) )
                    PicklistSalesOrdersCollection.Add(FillPicklistSO(row));
            }
        }

        private void filterOrders( )
        {
            var district = new DistrictModel();
            var startDate = new DateTime();
            var endDate = new DateTime();

            if ( OrderBeginDate.Contains("/") )
                startDate = DateTime.ParseExact(OrderBeginDate, "MM/dd/yyyy", CultureInfo.InvariantCulture);
            else
                startDate = DateTime.ParseExact(OrderBeginDate, "MMddyyyy", CultureInfo.InvariantCulture);

            if ( OrderEndDate.Contains("/") )
                endDate = DateTime.ParseExact(OrderEndDate, "MM/dd/yyyy", CultureInfo.InvariantCulture);
            else
                endDate = DateTime.ParseExact(OrderEndDate, "MMddyyyy", CultureInfo.InvariantCulture);


            if ( SelectedIndexDistrict <= 0 )
                ApplyFilter(null, startDate, endDate);
            else
            {
                district = (DistrictModel)district.Fetch(SelectedDistrict[1], "code");
                ApplyFilter(district, startDate, endDate);
            }

        }

        private void ApplyFilter( DistrictModel district, DateTime startDate, DateTime endDate )
        {
            PicklistSalesOrdersCollection.Clear();

            foreach ( var row in SalesOrderCollection )
            {
                if ( district == null )
                {
                    if ( startDate <= row.OrderDate && row.OrderDate <= endDate )
                    {
                        PicklistSalesOrdersCollection.Add(FillPicklistSO(row));
                    }
                }
                else if ( row.DistrictId == district.DistrictId )
                {
                    if ( startDate <= row.OrderDate && row.OrderDate <= endDate )
                    {
                        PicklistSalesOrdersCollection.Add(FillPicklistSO(row));
                    }
                }
            }
        }

        private PickListSO FillPicklistSO( SalesOrderModel order )
        {
            var picklistSO = new PickListSO();

            var _customer = new CustomerModel();
            var _term = new TermModel();
            var _district = new DistrictModel();

            _customer = (CustomerModel)_customer.Fetch(order.CustomerID.ToString(), "id");
            _term = (TermModel)_term.Fetch(order.TermId.ToString(), "id");
            _district = (DistrictModel)_district.Fetch(order.DistrictId.ToString(), "id");

            picklistSO.IsSelected = false;
            picklistSO.SoNumber = order.OrderNumber;
            picklistSO.CustomerName = _customer.CustomerName;
            picklistSO.OrderAmount = order.OrderAmount;
            picklistSO.SoDate = order.OrderDate;

            if ( order.IsServed == false && order.IsPicked == false )
                picklistSO.Status = "New Order";
            else if ( order.IsServed && order.IsPicked == false )
                picklistSO.Status = "Partially Served";

            picklistSO.Term = _term.TermDescription;
            picklistSO.Address = _customer.AddressMain + ", " + _customer.AddressCity + ", " + _customer.AddressProvince;
            picklistSO.District = _district.DistrictNumber;

            return picklistSO;
        }

        private void SelectAllToggled( )
        {
            if ( HasAllSelected )
            {
                foreach ( var row in PicklistSalesOrdersCollection )
                {
                    row.IsSelected = true;
                    IsAllocateable = true;
                }
            }
            else
            {
                if ( isItemSelected == false )
                    foreach ( var row in PicklistSalesOrdersCollection )
                    {
                        row.IsSelected = false;
                        IsAllocateable = false;
                    }
            }
        }

        private void toggleSelectAll( )
        {
            isItemSelected = false;
        }

        private void toggleItemChecked( )
        {
            isItemSelected = true;

            var checkCounter = 0;
            foreach ( var row in PicklistSalesOrdersCollection )
            {
                if ( row.IsSelected )
                    checkCounter++;
            }

            if ( checkCounter == PicklistSalesOrdersCollection.Count )
                HasAllSelected = true;
            else
            {
                HasAllSelected = false;
            }

            if ( checkCounter > 0 )
            {
                IsAllocateable = true;
            }
            else
                IsAllocateable = false;
        }

        private void allocateStocks( )
        {
            //Thread.Sleep(5000);
            var order = new SalesOrderModel();
            var dummy = new InventoryDummy2Model();
            var pickHead = new PickListHeaderModel();
            var picknumber = new PickListNumberModel();
            Dictionary<int, string> ordersToBeRemoved = new Dictionary<int, string>();

            AllocationQueries.Clear();

            pickHead.HeaderNumber = picknumber.GenerateNumber();
            pickHead.UserId = MyGlobals.LoggedUser.Id;
            pickHead.Pickdate = DateTime.UtcNow;
            pickHead.IsSuccessful = true;
            pickHead.IsAssigned = false;
            pickHead.IsGatepassPrinted = false;
            pickHead.GatepassId = null;

            AllocationQueries.Add(pickHead.GetAddQuery(pickHead));
            AllocationQueries.Add(picknumber.GetAddQuery());

            var index = 0;
            foreach ( var row in PicklistSalesOrdersCollection )
            {
                if ( row.IsSelected )
                {
                    ordersToBeRemoved.Add(index, row.SoNumber);
                    allocatePerOrder(row.SoNumber, pickHead);
                }
                index++;
            }

            db.RunMySqlTransaction(AllocationQueries);

            foreach ( var i in ordersToBeRemoved )
            {
                order = (SalesOrderModel)order.Fetch(i.Value, "code");
                order.IsPicked = true;
                order.UpdateItem(order);

                PicklistSalesOrdersCollection.RemoveAt(i.Key);
            }
        }

        private void allocatePerOrder( string soNumber, PickListHeaderModel pickhead )
        {
            var dummy = new InventoryDummy2Model();
            var dummyList = dummy.FetchPerOrder(soNumber);

            foreach ( var row in dummyList )
            {
                allocatePerItem(row, pickhead);
            }
        }

        private void allocatePerItem( InventoryDummy2Model orderedItem, PickListHeaderModel pickhead )
        {
            pickhead = (PickListHeaderModel)pickhead.Fetch(pickhead.HeaderNumber, "code");

            var inventory = new InventoryMaster2Model();
            var inventoryItems = inventory.FetchPerItem(orderedItem.ItemCode);

            var itemModel = new Item2Model();
            var item = (Item2Model)itemModel.Fetch(orderedItem.ItemCode, "code");
            var piecePerCase = item.PackSize * item.PackSizeBO;

            var orderedCases = orderedItem.Cases;
            var orderedPieces = orderedItem.Pieces;
            var orderInPieces = ConvertToPieces(orderedCases, orderedPieces, piecePerCase);

            var allocatedCases = 0;
            var allocatedPieces = 0;
            var allocatedInPieces = 0;
            var remainingOrderInPieces = orderInPieces;

            var picklistLine = new PickListLineModel();
            var allocateStocks = new AllocatedStocksModel();
            var _allocatedItemInPieces = 0;

            inventoryItems.Sort(( x, y ) => x.ExpirationDate.CompareTo(y.ExpirationDate));

            foreach ( var inventoryItem in inventoryItems )
            {
                _allocatedItemInPieces = 0;
                var currentItemInPieces = ConvertToPieces(inventoryItem.Cases, inventoryItem.Pieces, piecePerCase);

                if ( remainingOrderInPieces > 0 )
                {
                    if ( currentItemInPieces > 0 )
                    {
                        if ( currentItemInPieces >= remainingOrderInPieces )
                        {
                            allocatedInPieces += remainingOrderInPieces;
                            _allocatedItemInPieces += remainingOrderInPieces;
                            currentItemInPieces -= remainingOrderInPieces;

                            remainingOrderInPieces = orderInPieces - allocatedInPieces;

                            inventoryItem.Cases = currentItemInPieces / piecePerCase;
                            inventoryItem.Pieces = currentItemInPieces % piecePerCase;
                            break;
                        }
                        else
                        {
                            _allocatedItemInPieces += currentItemInPieces;
                            allocatedInPieces += currentItemInPieces;
                            currentItemInPieces = 0;

                            remainingOrderInPieces = orderInPieces - allocatedInPieces;

                            inventoryItem.Cases = 0;
                            inventoryItem.Pieces = 0;
                        }

                        allocateStocks.InventoryDummyId = orderedItem.Id;
                        if ( _allocatedItemInPieces >= piecePerCase )
                            allocateStocks.Cases = _allocatedItemInPieces / piecePerCase;
                        else
                            allocateStocks.Cases = 0;

                        allocateStocks.Pieces = _allocatedItemInPieces % piecePerCase;
                        allocateStocks.Expiry = inventoryItem.ExpirationDate;

                        AllocationQueries.Add(allocateStocks.GetAddQuery(allocateStocks));
                        AllocationQueries.Add(inventoryItem.GetUpdateQuery(inventoryItem));
                    }
                }
                else
                    break;
            }

            picklistLine.PickListHeaderId = pickhead.GenerateId();
            picklistLine.InventoryDummyId = orderedItem.Id;

            if ( allocatedInPieces >= piecePerCase )
                picklistLine.AllocatedCases = allocatedInPieces / piecePerCase;
            else
                picklistLine.AllocatedCases = 0;

            picklistLine.AllocatedPieces = allocatedInPieces % piecePerCase;

            if ( allocatedInPieces < orderInPieces )
                picklistLine.IsCritical = true;
            else
                picklistLine.IsCritical = false;

            AllocationQueries.Add(picklistLine.GetAddQuery(picklistLine));
        }

        private void deallocateStocks( )
        {
            var pickHead = new PickListHeaderModel();
            var pickLine = new PickListLineModel();
            var allocatedStocks = new AllocatedStocksModel();
            var inventory = new InventoryMaster2Model();
            var transactionString = new List<string>();
            var item = new Item2Model();
            var dummy = new InventoryDummy2Model();
            var inventoryItem = new InventoryMaster2Model();
            var piecePerCase = 0;
            int? previousItem = null;
            int stockInPieces = 0;
            int inventoryItemInPieces = 0;
            int totalPieces = 0;

            pickHead = (PickListHeaderModel)pickHead.Fetch(SelectedPickList[0], "code");

            var picklineList = pickLine.FetchPerPickHead(pickHead.Id.ToString());
            var allocatedStocksList = allocatedStocks.FetchPerPickList(pickHead.Id.ToString());

            foreach ( var stock in allocatedStocksList )
            {
                if ( previousItem == null || previousItem != stock.InventoryDummyId )
                {
                    dummy = (InventoryDummy2Model)dummy.Fetch(stock.InventoryDummyId.ToString(), "id");
                    item = (Item2Model)item.Fetch(dummy.ItemCode, "code");
                    piecePerCase = item.PackSize * item.PackSizeBO;
                }
                inventoryItem = (InventoryMaster2Model)inventoryItem.FetchItem(item.ItemId, stock.Expiry);

                stockInPieces = (stock.Cases * piecePerCase) + stock.Pieces;
                inventoryItemInPieces = (inventoryItem.Cases * piecePerCase) + inventoryItem.Pieces;

                totalPieces = stockInPieces + inventoryItemInPieces;

                if ( totalPieces >= piecePerCase )
                    inventoryItem.Cases = totalPieces / piecePerCase;
                else
                    inventoryItem.Cases = 0;

                inventoryItem.Pieces = totalPieces % piecePerCase;

                transactionString.Add(inventoryItem.GetUpdateQuery(inventoryItem));//update inventory

                previousItem = stock.InventoryDummyId;
            }
            transactionString.Add(pickHead.GetDeleteQuery(pickHead));

            db.RunMySqlTransaction(transactionString);
        }

        private int ConvertToPieces( int cases, int pieces, int piecePerCase )
        {
            var itemInPieces = 0;
            itemInPieces = (cases * piecePerCase) + pieces;
            return itemInPieces;
        }


        #region IDataErrorInfo Members
        public string this[string columnName]
        {
            get
            {
                return null;
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

    public class PickListSO: ViewModelBase
    {
        private bool isSelected;
        public bool IsSelected
        {
            get { return isSelected; }
            set
            {
                isSelected = value;
                OnPropertyChanged();
            }
        }

        private string soNumber;
        public string SoNumber
        {
            get { return soNumber; }
            set { soNumber = value; }
        }

        private string customerName;
        public string CustomerName
        {
            get { return customerName; }
            set { customerName = value; }
        }

        private decimal orderAmount;
        public decimal OrderAmount
        {
            get { return orderAmount; }
            set { orderAmount = value; }
        }

        private DateTime soDate;
        public DateTime SoDate
        {
            get { return soDate; }
            set { soDate = value; }
        }

        private string status;
        public string Status
        {
            get { return status; }
            set { status = value; }
        }

        private string term;
        public string Term
        {
            get { return term; }
            set { term = value; }
        }

        private string address;
        public string Address
        {
            get { return address; }
            set { address = value; }
        }

        private string district;
        public string District
        {
            get { return district; }
            set { district = value; }
        }
    }
}
