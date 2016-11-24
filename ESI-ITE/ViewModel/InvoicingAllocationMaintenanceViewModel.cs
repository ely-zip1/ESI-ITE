using ESI_ITE.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESI_ITE.ViewModel
{
    public class InvoicingAllocationMaintenanceViewModel : ViewModelBase, IDataErrorInfo
    {
        public InvoicingAllocationMaintenanceViewModel()
        {
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

        private ObservableCollection<List<string>> criticalItemCollection = new ObservableCollection<List<string>>();
        public ObservableCollection<List<string>> CriticalItemCollection
        {
            get { return criticalItemCollection; }
            set
            {
                criticalItemCollection = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<List<string>> partiallyServedOrderCollection = new ObservableCollection<List<string>>();
        public ObservableCollection<List<string>> PartiallyServedOrderCollection
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

        private List<string> selectedCriticalItem = new List<string>();
        public List<string> SelectedCriticalItem
        {
            get { return selectedCriticalItem; }
            set
            {
                selectedCriticalItem = value;
                OnPropertyChanged();
            }
        }

        private List<string> selectedPSO = new List<string>();
        public List<string> SelectedPSO
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

        private PickListHeaderModel PickHead;
        private List<SalesOrderModel> PartiallyServedOrderList;
        private List<PickListLineModel> PickLineList = new List<PickListLineModel>();
        private List<List<object>> PickOrderList = new List<List<object>>();

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
            var pickLine = new PickListLineModel();
            PickLineList = pickLine.FetchPerPickHead(PickHead.Id.ToString());

            var itemList = new List<List<object>>(); //[0] InventoryDummy
                                                     //[1] PickListLineModel
                                                     //[2] bool

            foreach (var line in PickLineList)
            {
                var inventoryDummy = new InventoryDummy2Model();
                inventoryDummy = (InventoryDummy2Model)inventoryDummy.Fetch(line.InventoryDummyId.ToString(), "id");

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
                            continue;
                        }
                }
                if (isUpdated)
                    continue;

                var _itemList = new List<object>();
                _itemList.Add(inventoryDummy);
                _itemList.Add(line);
                _itemList.Add(line.IsCritical);

                itemList.Add(_itemList);
            }

            foreach (var item in itemList)
            {
                var inventoryDummy = item[0] as InventoryDummy2Model;
                var _pickline = item[1] as PickListLineModel;
                var isCritical = (bool)item[2];
                if (isCritical)
                {
                    var critItemList = new List<string>();
                    critItemList.Add(inventoryDummy.ItemCode);
                    critItemList.Add(inventoryDummy.ItemDescription);
                    critItemList.Add(inventoryDummy.Cases.ToString());
                    critItemList.Add(inventoryDummy.Pieces.ToString());
                    critItemList.Add(_pickline.AllocatedCases.ToString());
                    critItemList.Add(_pickline.AllocatedPieces.ToString());
                    critItemList.Add(inventoryDummy.Location);

                    CriticalItemCollection.Add(critItemList);
                }
            }

            foreach (var line in PickLineList)
            {
                var inventoryDummy = new InventoryDummy2Model();
                inventoryDummy = (InventoryDummy2Model)inventoryDummy.Fetch(line.InventoryDummyId.ToString(), "id");

                var order = new SalesOrderModel();
                order = (SalesOrderModel)order.Fetch(inventoryDummy.OrderNumber, "code");

                var customer = new CustomerModel();
                customer = (CustomerModel)customer.Fetch(order.CustomerID.ToString(), "id");

                var pickOrder = new List<object>();
                pickOrder.Add(line);
                pickOrder.Add(inventoryDummy);
                pickOrder.Add(order);
                pickOrder.Add(customer);

                PickOrderList.Add(pickOrder);
            }

            LoadOrders(CriticalItemCollection[0][0]);

            var inventory = new InventoryMaster2Model();
        }

        private void LoadOrders(string itemCode)
        {
            foreach (var pickOrder in PickOrderList)
            {
                if ((pickOrder[1] as InventoryDummy2Model).ItemCode == itemCode)
                {
                    var pickline = pickOrder[0] as PickListLineModel;
                    var inventoryDummy = pickOrder[1] as InventoryDummy2Model;
                    var order = pickOrder[2] as SalesOrderModel;
                    var customer = pickOrder[3] as CustomerModel;

                    var PSO = new List<string>();
                    PSO.Add(customer.CustomerNumber);
                    PSO.Add(customer.CustomerName);
                    PSO.Add(inventoryDummy.Cases.ToString());
                    PSO.Add(inventoryDummy.Pieces.ToString());
                    PSO.Add(pickline.AllocatedCases.ToString());
                    PSO.Add(pickline.AllocatedPieces.ToString());
                    PSO.Add(inventoryDummy.Location);

                    PartiallyServedOrderCollection.Add(PSO);
                }
            }
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
