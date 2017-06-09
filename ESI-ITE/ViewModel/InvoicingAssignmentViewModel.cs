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
using ESI_ITE.View.PrintingTemplate;
using System.Windows.Controls;
using System.Windows;

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

            MyGlobals.printingDoc = result;

            MyGlobals.PrintingParent = MyGlobals.InvoicingVM.SelectedPage;
            MyGlobals.InvoicingVM.SelectedPage = new PrintingMainPageView();
        }

        private Task<FixedDocument> InvoicePrintingAsync()
        {
            return Task.Factory.StartNew(() => InvoicePrinting());
        }

        private FixedDocument InvoicePrinting()
        {
            FixedDocument fixedDoc = null;

            Application.Current.Dispatcher.Invoke(() =>
            {
                fixedDoc = new FixedDocument();
                foreach (var _orderToBeInvoiced in OrdersCollection)
                {
                    if (_orderToBeInvoiced.IsSelected)
                    {
                        var invoiceTemplateViewModel = new InvoicePrintTemplateViewModel();
                        var inventoryDummy = new InventoryDummy2Model();
                        var itemMaster = new ItemModel();
                        var allocationObj = new AllocatedStocksModel();

                        var invoiceableItems = new List<InvoiceItems>();
                        var allocatedOrderItems = allocationObj.FetchPerOrder(_orderToBeInvoiced.OrderNumber, SelectedPickNumber[0]);

                        // header - ORDER DETAILS

                        var order = new SalesOrderModel();
                        order = (SalesOrderModel)order.Fetch(_orderToBeInvoiced.OrderNumber, "code");

                        var customer = new CustomerModel();
                        var salesman = new SalesmanModel();
                        var district = new DistrictModel();
                        var warehouse = new WareHouseModel();
                        var terms = new TermModel();
                        var invoice = new InvoicesModel();
                        var priceType = new PriceTypeModel();

                        invoice = (InvoicesModel)invoice.Fetch(_orderToBeInvoiced.InvoiceNumber, "code");
                        customer = (CustomerModel)customer.Fetch(order.CustomerID.ToString(), "id");
                        district = (DistrictModel)district.Fetch(customer.DistrictId.ToString(), "id");
                        salesman = (SalesmanModel)salesman.Fetch(district.Salesman.ToString(), "id");
                        warehouse = (WareHouseModel)warehouse.Fetch(order.WarehouseId.ToString(), "id");
                        terms = (TermModel)terms.Fetch(order.TermId.ToString(), "id");
                        priceType = (PriceTypeModel)priceType.Fetch(order.PriceId.ToString(), "id");

                        var invoiceHeader = new InvoiceHeader();
                        invoiceHeader.InvoiceNumber = _orderToBeInvoiced.InvoiceNumber;
                        invoiceHeader.InvoiceDate = DateTime.Today.ToShortDateString();
                        invoiceHeader.AccountNumber = customer.CustomerNumber;
                        invoiceHeader.Salesman = salesman.SalesmanName;
                        invoiceHeader.District = district.DistrictNumber;
                        invoiceHeader.Warehouse = warehouse.Code;
                        invoiceHeader.CustomerName = customer.CustomerName;
                        invoiceHeader.CustomerAddress = customer.AddressMain + ", " + customer.AddressCity + ", " + customer.AddressProvince;
                        invoiceHeader.SoNumber = order.OrderNumber;
                        invoiceHeader.SoDate = order.OrderDate.ToShortDateString();
                        invoiceHeader.Terms = terms.TermCode;

                        // calculation

                        var totalCases = 0;
                        var totalPieces = 0;
                        decimal totalAmount = 0;

                        foreach (var allocatedItem in allocatedOrderItems)
                        {
                            var itemExists = false;

                            inventoryDummy = (InventoryDummy2Model)inventoryDummy.Fetch(allocatedItem.InventoryDummyId.ToString(), "id");
                            itemMaster = (ItemModel)itemMaster.Fetch(inventoryDummy.ItemCode, "code");

                            if (invoiceableItems.Count > 0)
                            {
                                foreach (var invoiceItem in invoiceableItems)
                                {
                                    if (invoiceItem.ItemDescription == itemMaster.Description)
                                    {
                                        invoiceItem.Cases += allocatedItem.Cases;
                                        invoiceItem.Pieces += allocatedItem.Pieces;

                                        invoiceItem.Amount += (allocatedItem.Cases * invoiceItem.PricePerCase) + (allocatedItem.Pieces * invoiceItem.PricePerPiece);

                                        itemExists = true;
                                        break;
                                    }
                                    else
                                    {
                                        itemExists = false;
                                    }
                                }

                                if (itemExists == false)
                                {
                                    var _invoiceableItem = new InvoiceItems();
                                    _invoiceableItem.ItemDescription = itemMaster.Description;
                                    _invoiceableItem.Cases = allocatedItem.Cases;
                                    _invoiceableItem.Pieces = allocatedItem.Pieces;
                                    _invoiceableItem.PricePerCase = itemMaster.PackSize * itemMaster.PackSizeBO * itemMaster.CurrentPrice;
                                    //_invoiceableItem.PricePerPiece = price.;
                                    _invoiceableItem.Amount = (_invoiceableItem.Cases * _invoiceableItem.PricePerCase) + (_invoiceableItem.Pieces * _invoiceableItem.PricePerPiece);

                                    invoiceableItems.Add(_invoiceableItem);
                                }
                            }
                            else
                            {
                                var _invoiceableItem = new InvoiceItems();
                                _invoiceableItem.ItemDescription = itemMaster.Description;
                                _invoiceableItem.Cases = allocatedItem.Cases;
                                _invoiceableItem.Pieces = allocatedItem.Pieces;
                                _invoiceableItem.PricePerCase = itemMaster.PackSize * itemMaster.PackSizeBO * itemMaster.CurrentPrice;
                                _invoiceableItem.PricePerPiece = itemMaster.CurrentPrice;
                                _invoiceableItem.Amount = (_invoiceableItem.Cases * _invoiceableItem.PricePerCase) + (_invoiceableItem.Pieces * _invoiceableItem.PricePerPiece);

                                invoiceableItems.Add(_invoiceableItem);
                            }
                        }

                        // content - ITEM LIST
                        var pageNumber = 1;
                        var newPage = CreateNewPage(invoiceHeader, pageNumber++);
                        fixedDoc.Pages.Add((PageContent)newPage[1]);
                        invoiceTemplateViewModel = (InvoicePrintTemplateViewModel)newPage[0];

                        var lines = 0;

                        foreach (var _invoiceableItem in invoiceableItems)
                        {
                            totalCases += _invoiceableItem.Cases;
                            totalPieces += _invoiceableItem.Pieces;
                            totalAmount += _invoiceableItem.Amount;

                            if (lines >= 38)
                            {
                                newPage = CreateNewPage(invoiceHeader, pageNumber++);
                                fixedDoc.Pages.Add((PageContent)newPage[1]);
                                invoiceTemplateViewModel = (InvoicePrintTemplateViewModel)newPage[0];

                                lines = 0;
                            }

                            if (_invoiceableItem.ItemDescription.Length >= 35)
                                lines += 2;
                            else
                                lines++;

                            invoiceTemplateViewModel.ItemList.Add(_invoiceableItem);
                        }

                        //footer - TOTAL
                        if (lines >= 38)
                        {
                            newPage = CreateNewPage(invoiceHeader, pageNumber++);
                            fixedDoc.Pages.Add((PageContent)newPage[1]);
                            invoiceTemplateViewModel = (InvoicePrintTemplateViewModel)newPage[0];

                            lines = 0;
                        }

                        invoiceTemplateViewModel.TotalCases = totalCases;
                        invoiceTemplateViewModel.TotalPieces = totalPieces;
                        invoiceTemplateViewModel.TotalAmount = totalAmount.ToString();
                        invoiceTemplateViewModel.VatAmount = (totalAmount * decimal.Parse("0.12")).ToString();
                        invoiceTemplateViewModel.TaxedTotal = (totalAmount - decimal.Parse(invoiceTemplateViewModel.VatAmount)).ToString();
                    }
                }
            });
            return fixedDoc;
        }

        private List<object> CreateNewPage(InvoiceHeader _invoiceHeader, int pageNumber)
        {
            var fixedPage = new FixedPage();
            var grid = new Grid();
            var templateView = new InvoicePrintTemplate();
            var templateVM = (InvoicePrintTemplateViewModel)templateView.DataContext;

            templateView.Width = 768;
            templateView.MinHeight = 100;

            fixedPage.Width = 768;
            fixedPage.Height = 1056;

            templateVM.PageNumber = pageNumber;

            templateVM.InvoiceNumber = _invoiceHeader.InvoiceNumber;
            templateVM.InvoiceDate = _invoiceHeader.InvoiceDate;
            templateVM.AccountNumber = _invoiceHeader.AccountNumber;
            templateVM.Salesman = _invoiceHeader.Salesman;
            templateVM.District = _invoiceHeader.District;
            templateVM.Warehouse = _invoiceHeader.Warehouse;
            templateVM.Customer = _invoiceHeader.CustomerName;
            templateVM.Address = _invoiceHeader.CustomerAddress;
            templateVM.OrderNumber = _invoiceHeader.SoNumber;
            templateVM.OrderDate = _invoiceHeader.SoDate;
            templateVM.TermCode = _invoiceHeader.Terms;
            templateVM.DeliveryNotes = _invoiceHeader.DeliveryNotes;

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
