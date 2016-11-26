using ESI_ITE.Model;
using ESI_ITE.ViewModel.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ESI_ITE.ViewModel
{
    public class InvoicingAllocationMaintenanceViewModel : ViewModelBase, IDataErrorInfo
    {
        public InvoicingAllocationMaintenanceViewModel()
        {
            casesValueChangedCommand = new DelegateCommand(UpdateCases);
            piecesValueChangedCommand = new DelegateCommand(UpdatePieces);
            cancelAllocationCommand = new DelegateCommand(CancelAllocation);
            updateStocksCommand = new DelegateCommand(UpdateStocks);

            Load();
        }

        #region Properties
        private ObservableCollection<List<string>> picklistCollection = new ObservableCollection<List<string>>();
        public ObservableCollection<List<string>> PicklistCollection
        {
            get { return picklistCollection; }
            set
            {
                picklistCollection = value;
            }
        }

        private ObservableCollection<CriticalItems> criticalItemCollection = new ObservableCollection<CriticalItems>();
        public ObservableCollection<CriticalItems> CriticalItemCollection
        {
            get { return criticalItemCollection; }
            set
            {
                criticalItemCollection = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<PartiallyServedOrders> partiallyServedOrderCollection = new ObservableCollection<PartiallyServedOrders>();
        public ObservableCollection<PartiallyServedOrders> PartiallyServedOrderCollection
        {
            get { return partiallyServedOrderCollection; }
            set
            {
                partiallyServedOrderCollection = value;
                OnPropertyChanged();
            }
        }


        private List<string> selectedPicklist = new List<string>();
        public List<string> SelectedPicklist
        {
            get { return selectedPicklist; }
            set
            {
                selectedPicklist = value;
                OnPropertyChanged();
            }
        }

        private CriticalItems selectedCriticalItem = new CriticalItems();
        public CriticalItems SelectedCriticalItem
        {
            get { return selectedCriticalItem; }
            set
            {
                selectedCriticalItem = value;
                OnPropertyChanged();
                if (value != null)
                    SelectedItemChanged();
            }
        }

        private PartiallyServedOrders selectedPSO = new PartiallyServedOrders();
        public PartiallyServedOrders SelectedPSO
        {
            get { return selectedPSO; }
            set
            {
                selectedPSO = value;
                OnPropertyChanged();
            }
        }

        private int selectedIndexPickList = -1;
        public int SelectedIndexPicklist
        {
            get { return selectedIndexPickList; }
            set
            {
                selectedIndexPickList = value;
                OnPropertyChanged();
                if (value > -1)
                    SelectedPicklistChanged();
            }
        }

        private int selectedIndexCriticalItem = -1;
        public int SelectedIndexCriticalItem
        {
            get { return selectedIndexCriticalItem; }
            set
            {
                selectedIndexCriticalItem = value;
                OnPropertyChanged();
            }
        }

        private int selectedIndexPSO = -1;
        public int SelectedIndexPSO
        {
            get { return selectedIndexPSO; }
            set
            {
                selectedIndexPSO = value;
                OnPropertyChanged();
            }
        }


        private string casesOnHand;
        public string CasesOnHand
        {
            get { return casesOnHand; }
            set
            {
                casesOnHand = value;
                OnPropertyChanged();
            }
        }

        private string piecesOnHand;
        public string PiecesOnHand
        {
            get { return piecesOnHand; }
            set
            {
                piecesOnHand = value;
                OnPropertyChanged();
            }
        }

        #region Commands

        private DelegateCommand casesValueChangedCommand;
        public ICommand CasesValueChangedCommand
        {
            get { return casesValueChangedCommand; }
        }

        private DelegateCommand piecesValueChangedCommand;
        public ICommand PiecesValueChangedCommand
        {
            get { return piecesValueChangedCommand; }
        }

        private DelegateCommand cancelAllocationCommand;
        public ICommand CancelAllocationCommand
        {
            get { return cancelAllocationCommand; }
        }

        private DelegateCommand updateStocksCommand;
        public ICommand UpdateStocksCommand
        {
            get { return updateStocksCommand; }
        }

        #endregion

        private PickListHeaderModel PickHead;
        private List<PickListLineModel> PickLineList = new List<PickListLineModel>();
        private List<List<object>> PickOrderList = new List<List<object>>();
        private List<PartiallyServedOrders> itemsPerOrderList = new List<PartiallyServedOrders>();

        #endregion

        private void Load()
        {
            PickHead = new PickListHeaderModel();
            var user = new UserModel();

            var pickheadList = PickHead.FetchAll();
            foreach (var row in pickheadList)
            {
                var header = (PickListHeaderModel)row;

                PickHead = header;

                if (header.IsSuccessful && header.IsAssigned == false && header.IsGatepassPrinted == false)
                {
                    user = (UserModel)user.Fetch(header.UserId.ToString(), "id");

                    var picklist = new List<string>();
                    picklist.Add(header.HeaderNumber);
                    picklist.Add(user.Username);
                    picklist.Add(header.Pickdate.ToString());

                    PicklistCollection.Add(picklist);
                }
            }
        }

        private void SelectedPicklistChanged()
        {
            CriticalItemCollection.Clear();

            itemsPerOrderList.Clear();

            var pickLine = new PickListLineModel();
            PickLineList = pickLine.FetchPerPickHead(PickHead.Id.ToString());

            var itemList = new List<List<object>>(); //[0] InventoryDummy
                                                     //[1] PickListLineModel
                                                     //[2] bool

            foreach (var line in PickLineList)
            {
                var inventoryDummy = new InventoryDummy2Model();
                inventoryDummy = (InventoryDummy2Model)inventoryDummy.Fetch(line.InventoryDummyId.ToString(), "id");

                var order = new SalesOrderModel();
                order = (SalesOrderModel)order.Fetch(inventoryDummy.OrderNumber, "code");

                var customer = new CustomerModel();
                customer = (CustomerModel)customer.Fetch(order.CustomerID.ToString(), "id");

                var isUpdated = false;
                if (itemList.Count > 0)
                {
                    foreach (var item in itemList)
                        if ((item[0] as InventoryDummy2Model).ItemCode == inventoryDummy.ItemCode)
                        {
                            (item[0] as InventoryDummy2Model).Cases += inventoryDummy.Cases;
                            (item[0] as InventoryDummy2Model).Pieces += inventoryDummy.Pieces;
                            (item[1] as PickListLineModel).AllocatedCases += line.AllocatedCases;
                            (item[1] as PickListLineModel).AllocatedPieces += line.AllocatedPieces;

                            if ((item[1] as PickListLineModel).IsCritical)
                                item[2] = true;

                            isUpdated = true;
                            break;
                        }
                }
                if (isUpdated)
                    continue;

                var _item = new List<object>();
                _item.Add(inventoryDummy);
                _item.Add(line);
                _item.Add(line.IsCritical);

                itemList.Add(_item);
            }

            foreach (var line in PickLineList)
            {
                var inventoryDummy = new InventoryDummy2Model();
                inventoryDummy = (InventoryDummy2Model)inventoryDummy.Fetch(line.InventoryDummyId.ToString(), "id");

                var order = new SalesOrderModel();
                order = (SalesOrderModel)order.Fetch(inventoryDummy.OrderNumber, "code");

                var customer = new CustomerModel();
                customer = (CustomerModel)customer.Fetch(order.CustomerID.ToString(), "id");

                var PSO = new PartiallyServedOrders();
                PSO.Customer = customer;
                PSO.OrderItem = inventoryDummy;
                PSO.PicklistItem = line;
                PSO.AllocatedCases = line.AllocatedCases.ToString();
                PSO.AllocatedPieces = line.AllocatedPieces.ToString();

                itemsPerOrderList.Add(PSO);
            }

            foreach (var item in itemList)
            {
                var inventoryDummy = item[0] as InventoryDummy2Model;
                var _pickline = item[1] as PickListLineModel;
                var isCritical = (bool)item[2];
                if (isCritical)
                {
                    var critItem = new CriticalItems();
                    critItem.ItemCode = inventoryDummy.ItemCode;
                    critItem.ItemDescription = inventoryDummy.ItemDescription;
                    critItem.OrderCases = inventoryDummy.Cases;
                    critItem.OrderPieces = inventoryDummy.Pieces;
                    critItem.AllocatedCases = _pickline.AllocatedCases;
                    critItem.AllocatedPieces = _pickline.AllocatedPieces;
                    critItem.LC = inventoryDummy.Location;

                    CriticalItemCollection.Add(critItem);
                }
            }

            SelectedIndexCriticalItem = 0;

            LoadOrders(CriticalItemCollection[0].ItemCode);
        }

        private void LoadOrders(string itemCode)
        {
            PartiallyServedOrderCollection.Clear();
            foreach (var pickOrder in itemsPerOrderList)
            {
                if (pickOrder.OrderItem.ItemCode == itemCode)
                {
                    PartiallyServedOrderCollection.Add(pickOrder);
                }
            }

            var itemMaster = new Item2Model();
            itemMaster = (Item2Model)itemMaster.Fetch(itemCode, "code");

            var inventoryMaster = new InventoryMaster2Model();
            var inventoryItemList = inventoryMaster.FetchPerItem(itemMaster.ItemId);

            var totalCases = 0;
            var totalPieces = 0;
            foreach (var inventoryItem in inventoryItemList)
            {
                totalCases += inventoryItem.Cases;
                totalPieces += inventoryItem.Pieces;
            }

            CasesOnHand = totalCases.ToString();
            PiecesOnHand = totalPieces.ToString();
        }

        private void SelectedItemChanged()
        {
            LoadOrders(SelectedCriticalItem.ItemCode);
        }

        private void UpdateCases()
        {
            if (SelectedPSO != null)
                if (SelectedPSO.Customer != null)
                    if (SelectedPSO.IsCasesValid)
                    {
                        foreach (var PSO in itemsPerOrderList)
                        {
                            if (PSO.Customer.CustomerNumber == SelectedPSO.Customer.CustomerNumber &&
                                PSO.OrderItem.ItemCode == SelectedPSO.OrderItem.ItemCode)
                            {
                                if (int.Parse(SelectedPSO.AllocatedCases) > int.Parse(CasesOnHand))
                                {
                                    MessageBox.Show("Allocated stock cannot exceed the current stock on hand.",
                                        "Allocation Overflow",
                                        MessageBoxButton.OK,
                                        MessageBoxImage.Warning);
                                }
                                else if (int.Parse(SelectedPSO.AllocatedCases) > SelectedPSO.OrderItem.Cases)
                                {
                                    MessageBox.Show("Allocated stock cannot exceed the ordered quantity.",
                                        "Allocation Overflow",
                                        MessageBoxButton.OK,
                                        MessageBoxImage.Warning);
                                }
                                else {
                                    PSO.AllocatedCases = SelectedPSO.AllocatedCases;
                                }
                                break;
                            }
                        }

                        var totalCases = 0;
                        foreach (var PSO in itemsPerOrderList)
                        {
                            if (PSO.OrderItem.ItemCode == SelectedPSO.OrderItem.ItemCode)
                                totalCases += int.Parse(PSO.AllocatedCases);
                        }

                        if (totalCases > SelectedCriticalItem.OrderCases)
                        {
                            MessageBox.Show("Allocated stock must be equal or less than the ordered quantity.",
                                "Allocation Overflow",
                                MessageBoxButton.OK,
                                MessageBoxImage.Warning);
                        }
                        else
                            CriticalItemCollection.ElementAt(SelectedIndexCriticalItem).AllocatedCases = totalCases;
                    }
        }

        private void UpdatePieces()
        {
            if (SelectedPSO != null)
                if (SelectedPSO.Customer != null)
                    if (SelectedPSO.IsPiecesValid)
                    {
                        foreach (var PSO in itemsPerOrderList)
                        {
                            if (PSO.Customer.CustomerNumber == SelectedPSO.Customer.CustomerNumber &&
                                PSO.OrderItem.ItemCode == SelectedPSO.OrderItem.ItemCode)
                            {
                                PSO.AllocatedPieces = SelectedPSO.AllocatedPieces;
                                break;
                            }
                        }

                        var totalPieces = 0;
                        foreach (var PSO in itemsPerOrderList)
                        {
                            if (PSO.OrderItem.ItemCode == SelectedPSO.OrderItem.ItemCode)
                                totalPieces += int.Parse(PSO.AllocatedPieces);
                        }

                        CriticalItemCollection.ElementAt(SelectedIndexCriticalItem).AllocatedPieces = totalPieces;
                    }
        }

        private void CancelAllocation()
        {
            var result = MessageBox.Show("Are you sure you want to cancel?", "Cancel Allocation", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                PartiallyServedOrderCollection.Clear();
                CriticalItemCollection.Clear();
                itemsPerOrderList.Clear();
                PickOrderList.Clear();
                PickLineList.Clear();

                SelectedIndexPicklist = -1;
            }
        }

        private void UpdateStocks()
        {
            throw new NotImplementedException();
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

    public class PartiallyServedOrders : ViewModelBase, IDataErrorInfo
    {
        private CustomerModel customer;
        public CustomerModel Customer
        {
            get { return customer; }
            set
            {
                customer = value;
                OnPropertyChanged();
            }
        }

        private InventoryDummy2Model orderItem;
        public InventoryDummy2Model OrderItem
        {
            get { return orderItem; }
            set
            {
                orderItem = value;
                OnPropertyChanged();
            }
        }

        private PickListLineModel picklistItem;
        public PickListLineModel PicklistItem
        {
            get { return picklistItem; }
            set
            {
                picklistItem = value;
                OnPropertyChanged();
            }
        }

        private string allocatedCases;
        public string AllocatedCases
        {
            get { return allocatedCases; }
            set
            {
                allocatedCases = value;
                OnPropertyChanged();
            }
        }

        private string allocatedPieces;
        public string AllocatedPieces
        {
            get { return allocatedPieces; }
            set
            {
                allocatedPieces = value;
                OnPropertyChanged();
            }
        }

        private bool isCasesValid = true;
        public bool IsCasesValid
        {
            get { return isCasesValid; }
            set
            {
                isCasesValid = value;
                OnPropertyChanged();
            }
        }

        private bool isPiecesValid = true;
        public bool IsPiecesValid
        {
            get { return isPiecesValid; }
            set
            {
                isPiecesValid = value;
                OnPropertyChanged();
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

        public string this[string columnName]
        {
            get
            {
                return GetValidationError(columnName);
            }
        }
        #endregion

        #region Validation Members

        private string GetValidationError(string propertyName)
        {
            string error = null;
            int x = 0;
            switch (propertyName)
            {
                case "AllocatedCases":
                    try
                    {
                        if (string.IsNullOrWhiteSpace(AllocatedCases))
                        {
                            error = "Field must not be empty!";
                            IsCasesValid = false;
                        }
                        else {
                            x = int.Parse(AllocatedCases);
                            IsCasesValid = true;
                        }
                    }
                    catch (Exception e)
                    {
                        error = "Invalid Input!";
                        IsCasesValid = false;
                    }
                    break;
                case "AllocatedPieces":
                    try
                    {
                        if (string.IsNullOrWhiteSpace(AllocatedPieces))
                        {
                            error = "Field must not be empty!";
                            IsPiecesValid = false;
                        }
                        else {
                            x = int.Parse(AllocatedPieces);
                            IsPiecesValid = true;
                        }
                    }
                    catch (Exception e)
                    {
                        error = "Invalid Input!";
                        IsPiecesValid = false;
                    }
                    break;
            }

            return error;
        }

        #endregion
    }

    public class CriticalItems : ViewModelBase
    {
        private string itemCode;
        public string ItemCode
        {
            get { return itemCode; }
            set
            {
                itemCode = value;
                OnPropertyChanged();
            }
        }

        private string itemDescription;
        public string ItemDescription
        {
            get { return itemDescription; }
            set
            {
                itemDescription = value;
                OnPropertyChanged();
            }
        }

        private int orderCases;
        public int OrderCases
        {
            get { return orderCases; }
            set
            {
                orderCases = value;
                OnPropertyChanged();
            }
        }

        private int orderPieces;
        public int OrderPieces
        {
            get { return orderPieces; }
            set
            {
                orderPieces = value;
                OnPropertyChanged();
            }
        }

        private int allocatedCases;
        public int AllocatedCases
        {
            get { return allocatedCases; }
            set
            {
                allocatedCases = value;
                OnPropertyChanged();
            }
        }

        private int allocatedPieces;
        public int AllocatedPieces
        {
            get { return allocatedPieces; }
            set
            {
                allocatedPieces = value;
                OnPropertyChanged();
            }
        }

        private string lc;
        public string LC
        {
            get { return lc; }
            set
            {
                lc = value;
                OnPropertyChanged();
            }
        }
    }
}
