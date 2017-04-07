using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ESI_ITE.Model;
using System.Collections.ObjectModel;
using ESI_ITE.ViewModel.Command;
using System.Windows.Input;
using System.Windows.Documents;

namespace ESI_ITE.ViewModel
{
    public class InvoicingAssignmentViewModel : ViewModelBase, IDataErrorInfo
    {
        public InvoicingAssignmentViewModel()
        {
            itemCheckedCommand = new DelegateCommand(itemChecked);
            itemUncheckedCommand = new DelegateCommand(itemUnchecked);
            selectAllCommand = new DelegateCommand(selectAllChanged);
            assignInvoiceCommand = new DelegateCommand(assignInvoice);
            printInvoiceCommand = new DelegateCommand(StartPrinting);

            Load();
        }

        #region Properties

        private PickListHeaderModel PickHead = new PickListHeaderModel();
        private PickListLineModel PickLine = new PickListLineModel();
        private InventoryDummy2Model InventoryDummy = new InventoryDummy2Model();

        private ObservableCollection<List<string>> pickNumberCollection = new ObservableCollection<List<string>>();
        public ObservableCollection<List<string>> PickNumberCollection
        {
            get { return pickNumberCollection; }
            set
            {
                pickNumberCollection = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<OrdersToBeInvoiced> ordersCollection = new ObservableCollection<OrdersToBeInvoiced>();
        public ObservableCollection<OrdersToBeInvoiced> OrdersCollection
        {
            get { return ordersCollection; }
            set
            {
                ordersCollection = value;
                OnPropertyChanged();
            }
        }

        private List<string> selectedPickNumber;
        public List<string> SelectedPickNumber
        {
            get { return selectedPickNumber; }
            set
            {
                selectedPickNumber = value;
                OnPropertyChanged();

                if (SelectedIndexPickNumber > -1)
                    SelectedPickNumberChanged();
            }
        }

        private OrdersToBeInvoiced selectedOrder;
        public OrdersToBeInvoiced SelectedOrder
        {
            get { return selectedOrder; }
            set
            {
                selectedOrder = value;
                OnPropertyChanged();
            }
        }

        private int selectedIndexPickNumber = -1;
        public int SelectedIndexPickNumber
        {
            get { return selectedIndexPickNumber; }
            set
            {
                selectedIndexPickNumber = value;
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

        private bool isChecked;
        public bool IsChecked
        {
            get { return isChecked; }
            set
            {
                isChecked = value;
                OnPropertyChanged();
            }
        }

        private string currentInvoiceNumber;
        public string CurrentInvoiceNumber
        {
            get { return currentInvoiceNumber; }
            set
            {
                currentInvoiceNumber = value;
                OnPropertyChanged();
            }
        }

        private bool isItemChecked = false;
        private bool selectAllClicked = true;

        #region Commands

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

        private DelegateCommand assignInvoiceCommand;
        public ICommand AssignInvoiceCommand
        {
            get { return assignInvoiceCommand; }
        }

        private DelegateCommand printInvoiceCommand;
        public ICommand PrintInvoiceCommand
        {
            get { return printInvoiceCommand; }
        }

        #endregion

        #endregion

        private void Load()
        {
            //
            // Load Pick Numbers
            //
            var pickheads = PickHead.FetchAll();
            foreach (PickListHeaderModel _pickhead in pickheads)
            {
                var user = new UserModel();
                user = (UserModel)user.Fetch(_pickhead.UserId.ToString(), "id");

                var listOfPickHeads = new List<string>();
                listOfPickHeads.Add(_pickhead.HeaderNumber);
                listOfPickHeads.Add(user.Username);
                listOfPickHeads.Add(_pickhead.Pickdate.ToString());
                listOfPickHeads.Add(_pickhead.Id.ToString());

                PickNumberCollection.Add(listOfPickHeads);
            }
        }

        private void SelectedPickNumberChanged()
        {
            OrdersCollection.Clear();

            var _ordersCollection = new List<string>();

            var orderObj = new SalesOrderModel();
            var customerObj = new CustomerModel();

            var picklists = PickLine.FetchPerPickHead(SelectedPickNumber[3]);
            foreach (var _picklist in picklists)
            {
                var inventoryDummy = (InventoryDummy2Model)InventoryDummy.Fetch(_picklist.InventoryDummyId.ToString(), "id");
                var order = (SalesOrderModel)orderObj.Fetch(inventoryDummy.OrderNumber, "code");
                var customer = (CustomerModel)customerObj.Fetch(order.CustomerID.ToString(), "id");

                var orderExists = false;
                if (_ordersCollection.Count > 0)
                {
                    foreach (var _order in OrdersCollection)
                    {
                        if (_order.OrderNumber == order.OrderNumber)
                        {
                            orderExists = true;
                            _order.AllocCasesQuantity += _picklist.AllocatedCases;
                            _order.AllocPiecesQuantity += _picklist.AllocatedPieces;
                        }
                    }

                    if (orderExists == false)
                    {
                        var orderToBeInvoiced = new OrdersToBeInvoiced();
                        orderToBeInvoiced.OrderNumber = order.OrderNumber;
                        orderToBeInvoiced.CustomerName = customer.CustomerName;
                        orderToBeInvoiced.SoCasesQuantity = order.Cases;
                        orderToBeInvoiced.SoPiecesQuantity = order.Pieces;
                        orderToBeInvoiced.AllocCasesQuantity = _picklist.AllocatedCases;
                        orderToBeInvoiced.AllocPiecesQuantity = _picklist.AllocatedPieces;

                        OrdersCollection.Add(orderToBeInvoiced);
                    }

                }
                else
                {
                    var orderToBeInvoiced = new OrdersToBeInvoiced();
                    orderToBeInvoiced.OrderNumber = order.OrderNumber;
                    orderToBeInvoiced.CustomerName = customer.CustomerName;
                    orderToBeInvoiced.SoCasesQuantity = order.Cases;
                    orderToBeInvoiced.SoPiecesQuantity = order.Pieces;
                    orderToBeInvoiced.AllocCasesQuantity = _picklist.AllocatedCases;
                    orderToBeInvoiced.AllocPiecesQuantity = _picklist.AllocatedPieces;

                    OrdersCollection.Add(orderToBeInvoiced);
                }

            }
        }

        private void selectAllChanged()
        {
            selectAllClicked = true;

            if (IsChecked)
            {
                foreach (var orderToBeInvoiced in OrdersCollection)
                {
                    orderToBeInvoiced.IsSelected = true;
                }
            }
            else
            {
                foreach (var orderToBeInvoiced in OrdersCollection)
                {
                    orderToBeInvoiced.IsSelected = false;
                }
            }

            selectAllClicked = false;
        }

        private void itemChecked()
        {
            if (selectAllClicked == false)
            {
                OrdersCollection[SelectedIndexOrder].IsSelected = true;

                var checkCounter = 0;
                foreach (var orderToBeInvoiced in OrdersCollection)
                {
                    if (orderToBeInvoiced.IsSelected)
                        checkCounter++;
                }

                if (checkCounter == OrdersCollection.Count)
                    IsChecked = true;
            }
        }

        private void itemUnchecked()
        {
            if (selectAllClicked == false)
            {
                OrdersCollection[SelectedIndexOrder].IsSelected = false;
                IsChecked = false;
            }
        }

        private void assignInvoice()
        {
            foreach (var _orderToBeInvoiced in OrdersCollection)
            {
                var invoiceObj = new InvoicesModel();
                var invoiceNumberObj = new InvoiceNumberModel();

                if (_orderToBeInvoiced.AllocCasesQuantity != 0 && _orderToBeInvoiced.AllocPiecesQuantity != 0)
                {
                    var invoice = new InvoicesModel();
                    invoice.InvoiceNumber = invoiceNumberObj.FetchLatest().InvoiceNumber;
                    invoice.PickheadId = _orderToBeInvoiced.PickId;
                    invoice.OrderId = _orderToBeInvoiced.OrderId;
                    invoice.UserId = MyGlobals.LoggedUser.Id;

                    invoiceObj.AddNew(invoice);

                    invoiceNumberObj.AddNew(new InvoiceNumberModel());

                    _orderToBeInvoiced.InvoiceNumber = invoice.InvoiceNumber;
                }
            }
        }


        #region Printing

        private void StartPrinting()
        {
            CallPrintingAsync();
        }

        private async void CallPrintingAsync()
        {
            var result = await InvoicePrintingAsync();
        }

        private Task<FixedDocument> InvoicePrintingAsync()
        {
            return Task.Factory.StartNew(() => InvoicePrinting());
        }

        private FixedDocument InvoicePrinting()
        {
            FixedDocument fixedDoc = null;

            foreach (var _orderToBeInvoiced in OrdersCollection)
            {
                if (_orderToBeInvoiced.IsSelected)
                {
                    var invoiceTemplateViewModel = new InvoicePrintTemplateViewModel();
                    var inventoryDummy = new InventoryDummy2Model();
                    var itemMaster = new ItemModel();

                    var orderItems = inventoryDummy.FetchPerOrder(_orderToBeInvoiced.OrderNumber);


                }
            }

            return fixedDoc;
        }

        #endregion


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

    public class OrdersToBeInvoiced : ViewModelBase
    {
        private bool isSelected = false;
        public bool IsSelected
        {
            get { return isSelected; }
            set
            {
                isSelected = value;
                OnPropertyChanged();
            }
        }

        private int orderId;
        public int OrderId
        {
            get { return orderId; }
            set
            {
                orderId = value;
                OnPropertyChanged();
            }
        }

        private int pickId;
        public int PickId
        {
            get { return pickId; }
            set
            {
                pickId = value;
                OnPropertyChanged();
            }
        }

        private string invoiceNumber;
        public string InvoiceNumber
        {
            get { return invoiceNumber; }
            set
            {
                invoiceNumber = value;
                OnPropertyChanged();
            }
        }

        private string orderNumber;
        public string OrderNumber
        {
            get { return orderNumber; }
            set
            {
                orderNumber = value;
                OnPropertyChanged();
            }
        }

        private string customerName;
        public string CustomerName
        {
            get { return customerName; }
            set
            {
                customerName = value;
                OnPropertyChanged();
            }
        }

        private int soCasesQuantity;
        public int SoCasesQuantity
        {
            get { return soCasesQuantity; }
            set
            {
                soCasesQuantity = value;
                OnPropertyChanged();
            }
        }

        private int soPiecesQuantity;
        public int SoPiecesQuantity
        {
            get { return soPiecesQuantity; }
            set
            {
                soPiecesQuantity = value;
                OnPropertyChanged();
            }
        }

        private int allocCasesQuantity;
        public int AllocCasesQuantity
        {
            get { return allocCasesQuantity; }
            set
            {
                allocCasesQuantity = value;
                OnPropertyChanged();
            }
        }

        private int allocPiecesQuantity;
        public int AllocPiecesQuantity
        {
            get { return allocPiecesQuantity; }
            set
            {
                allocPiecesQuantity = value;
                OnPropertyChanged();
            }
        }

    }
}
