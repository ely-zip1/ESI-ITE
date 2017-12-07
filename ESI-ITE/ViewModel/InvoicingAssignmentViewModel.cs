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
using ESI_ITE.Data_Access;

namespace ESI_ITE.ViewModel
{
    public class InvoicingAssignmentViewModel : ViewModelBase, IDataErrorInfo
    {
        public InvoicingAssignmentViewModel()
        {
            itemCheckedCommand = new DelegateCommand(itemChecked);
            itemUncheckedCommand = new DelegateCommand(itemUnchecked);
            selectAllCommand = new DelegateCommand(selectAllChanged);
            assignInvoiceCommand = new DelegateCommand(OrderSelectionCheck);
            printInvoiceCommand = new DelegateCommand(StartPrinting);

            Load();
        }

        #region Properties

        private DataAccess db = new DataAccess();

        private PickListHeaderModel PickHead = new PickListHeaderModel();
        private PickListLineModel PickLine = new PickListLineModel();
        private InventoryDummy2Model InventoryDummy = new InventoryDummy2Model();

        private ObservableCollection<List<string>> pickNumberCollection = new ObservableCollection<List<string>>();
        public ObservableCollection<List<string>> PickNumberCollection
        {
            get
            {
                return pickNumberCollection;
            }
            set
            {
                pickNumberCollection = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<OrdersToBeInvoiced> ordersCollection = new ObservableCollection<OrdersToBeInvoiced>();
        public ObservableCollection<OrdersToBeInvoiced> OrdersCollection
        {
            get
            {
                return ordersCollection;
            }
            set
            {
                ordersCollection = value;
                OnPropertyChanged();
            }
        }

        private List<string> selectedPickNumber;
        public List<string> SelectedPickNumber
        {
            get
            {
                return selectedPickNumber;
            }
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

        private int selectedIndexPickNumber = -1;
        public int SelectedIndexPickNumber
        {
            get
            {
                return selectedIndexPickNumber;
            }
            set
            {
                selectedIndexPickNumber = value;
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

        private bool isChecked;
        public bool IsChecked
        {
            get
            {
                return isChecked;
            }
            set
            {
                isChecked = value;
                OnPropertyChanged();
            }
        }

        private string currentInvoiceNumber;
        public string CurrentInvoiceNumber
        {
            get
            {
                return currentInvoiceNumber;
            }
            set
            {
                currentInvoiceNumber = value;
                OnPropertyChanged();
            }
        }

        private bool isItemChecked = false;
        private bool selectAllClicked = true;

        private List<string> InvoiceQueries = new List<string>();
        #region Commands

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

        private DelegateCommand assignInvoiceCommand;
        public ICommand AssignInvoiceCommand
        {
            get
            {
                return assignInvoiceCommand;
            }
        }

        private DelegateCommand printInvoiceCommand;
        public ICommand PrintInvoiceCommand
        {
            get
            {
                return printInvoiceCommand;
            }
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

            SetCurrentInvoiceNumber();
        }

        private void SetCurrentInvoiceNumber()
        {
            var invoiceNumberObj = new InvoiceNumberModel();
            invoiceNumberObj = invoiceNumberObj.FetchLatest();

            CurrentInvoiceNumber = invoiceNumberObj.InvoiceNumber;
        }

        private void SelectedPickNumberChanged()
        {
            OrdersCollection.Clear();

            var _ordersCollection = new List<string>();

            var orderObj = new SalesOrderModel();
            var customerObj = new CustomerModel();
            var invoiceObj = new InvoiceModel();

            var picklists = PickLine.FetchPerPickHead(SelectedPickNumber[3]);
            foreach (var _picklist in picklists)
            {
                var inventoryDummy = (InventoryDummy2Model)InventoryDummy.Fetch(_picklist.InventoryDummyId.ToString(), "id");
                var order = (SalesOrderModel)orderObj.Fetch(inventoryDummy.OrderNumber, "code");
                var customer = (CustomerModel)customerObj.Fetch(order.CustomerID.ToString(), "id");
                var invoice = invoiceObj.FetchByOrder(_picklist.PickListHeaderId.ToString(), order.OrderId.ToString());

                var hasDuplicate = false;
                if (OrdersCollection.Count > 0)
                {
                    foreach (var _order in OrdersCollection)
                    {
                        if (_order.OrderNumber == order.OrderNumber)
                        {
                            hasDuplicate = true;
                            _order.AllocCasesQuantity += _picklist.AllocatedCases;
                            _order.AllocPiecesQuantity += _picklist.AllocatedPieces;
                        }
                    }

                    if (hasDuplicate == false)
                    {
                        var orderToBeInvoiced = new OrdersToBeInvoiced();

                        if (invoice.Id > 0)
                            orderToBeInvoiced.InvoiceNumber = invoice.InvoiceNumber;

                        orderToBeInvoiced.OrderId = order.OrderId;
                        orderToBeInvoiced.PickId = _picklist.PickListHeaderId;
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

                    if (invoice.Id > 0)
                        orderToBeInvoiced.InvoiceNumber = invoice.InvoiceNumber;

                    orderToBeInvoiced.OrderId = order.OrderId;
                    orderToBeInvoiced.PickId = _picklist.PickListHeaderId;
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

        private void OrderSelectionCheck()
        {
            bool hasItemSelected = false;
            foreach (var order in OrdersCollection)
            {
                if (order.IsSelected)
                {
                    hasItemSelected = true;
                    break;
                }
            }

            if (hasItemSelected)
            {
                StartInvoicing();
            }
            else
            {
                MessageBox.Show("Please select orders to be invoiced.", "Invoice Assignment");
            }
        }


        #region Invoice Assignment Async

        private void StartInvoicing()
        {
            InvoiceQueries.Clear();

            CallInvoiceAssignment();
        }

        private async void CallInvoiceAssignment()
        {
            var result = await AssignInvoiceAsync();

            if (MyGlobals.hasTransactionError)
            {

            }
            else
            {
                foreach (var row in result)
                {
                    foreach (var order in OrdersCollection)
                    {
                        if (row.Key == order.OrderNumber)
                        {
                            order.InvoiceNumber = row.Value;
                        }
                    }
                }

                SetCurrentInvoiceNumber();
            }
        }

        private Task<Dictionary<string, string>> AssignInvoiceAsync()
        {
            return Task.Factory.StartNew(() => AssignInvoices());
        }

        private Dictionary<string, string> AssignInvoices()
        {
            var assignedInvoices = new Dictionary<string, string>();//
                                                                    //[0]Order Number

            var invoiceObj = new InvoiceModel();
            var invoiceNumberObj = new InvoiceNumberModel();
            var invoiceNumber = int.Parse(invoiceNumberObj.FetchLatest().InvoiceNumber);                                              //[1]Invoice Number

            foreach (var _orderToBeInvoiced in OrdersCollection)
            {

                if (_orderToBeInvoiced.AllocCasesQuantity > 0 || _orderToBeInvoiced.AllocPiecesQuantity > 0)
                {
                    var invoice = new InvoiceModel();

                    invoice.InvoiceNumber = invoiceNumber++.ToString();
                    invoice.PickheadId = _orderToBeInvoiced.PickId;
                    invoice.OrderId = _orderToBeInvoiced.OrderId;
                    invoice.UserId = MyGlobals.LoggedUser.Id;
                    invoice.Date = DateTime.UtcNow;

                    assignedInvoices.Add(_orderToBeInvoiced.OrderNumber, invoice.InvoiceNumber);

                    //invoiceObj.AddNew(invoice);
                    InvoiceQueries.Add(invoiceObj.GetAddQuery(invoice));

                    _orderToBeInvoiced.InvoiceNumber = invoice.InvoiceNumber;
                }
            }

            invoiceNumberObj.InvoiceNumber = invoiceNumber.ToString();
            InvoiceQueries.Add(invoiceNumberObj.GetAddQuery(invoiceNumberObj));

            db.RunMySqlTransaction(InvoiceQueries, null, null);

            return assignedInvoices;
        }

        //private void assignInvoice()
        //{
        //    foreach (var _orderToBeInvoiced in OrdersCollection)
        //    {
        //        var invoiceObj = new InvoiceModel();
        //        var invoiceNumberObj = new InvoiceNumberModel();

        //        if (_orderToBeInvoiced.AllocCasesQuantity > 0 || _orderToBeInvoiced.AllocPiecesQuantity > 0)
        //        {
        //            var currentInvoiceNumber = int.Parse(invoiceNumberObj.FetchLatest().InvoiceNumber);
        //            var invoice = new InvoiceModel();

        //            invoice.InvoiceNumber = currentInvoiceNumber++.ToString();
        //            invoice.PickheadId = _orderToBeInvoiced.PickId;
        //            invoice.OrderId = _orderToBeInvoiced.OrderId;
        //            invoice.UserId = MyGlobals.LoggedUser.Id;
        //            invoice.Date = DateTime.UtcNow;

        //            invoiceObj.AddNew(invoice);

        //            invoiceNumberObj.AddNew(new InvoiceNumberModel());

        //            _orderToBeInvoiced.InvoiceNumber = invoice.InvoiceNumber;
        //        }
        //    }

        //    SetCurrentInvoiceNumber();
        //}

        #endregion

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
                var pageNumber = 1;

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
                        var invoice = new InvoiceModel();
                        var priceType = new PriceTypeModel();

                        invoice = (InvoiceModel)invoice.Fetch(_orderToBeInvoiced.InvoiceNumber, "code");
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
                            var priceObj = new PriceSellingModel();

                            inventoryDummy = (InventoryDummy2Model)inventoryDummy.Fetch(allocatedItem.InventoryDummyId.ToString(), "id");
                            itemMaster = (ItemModel)itemMaster.Fetch(inventoryDummy.ItemCode, "code");
                            priceType = (PriceTypeModel)priceType.Fetch(inventoryDummy.PriceType, "code");

                            var itemPrice = priceObj.FetchPrice(itemMaster.ItemId.ToString(), priceType.PriceTypeId.ToString());

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
                                    _invoiceableItem.PricePerCase = itemPrice.SellingPrice;
                                    _invoiceableItem.PricePerPiece = itemPrice.SellingPrice / itemMaster.PackSize;
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
                                _invoiceableItem.PricePerCase = itemPrice.SellingPrice;
                                _invoiceableItem.PricePerPiece =itemPrice.SellingPrice/ itemMaster.PackSize;
                                _invoiceableItem.Amount = (_invoiceableItem.Cases * _invoiceableItem.PricePerCase) + (_invoiceableItem.Pieces * _invoiceableItem.PricePerPiece);

                                invoiceableItems.Add(_invoiceableItem);
                            }
                        }

                        // content - ITEM LIST
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
                        invoiceTemplateViewModel.TotalAmount = totalAmount.ToString("#,###.#0");
                        invoiceTemplateViewModel.VatAmount = (totalAmount * decimal.Parse("0.12")).ToString("#,###.#0");
                        invoiceTemplateViewModel.TaxedTotal = (totalAmount + decimal.Parse(invoiceTemplateViewModel.VatAmount)).ToString("#,###.#0");
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
                OnPropertyChanged();
            }
        }

        private int pickId;
        public int PickId
        {
            get
            {
                return pickId;
            }
            set
            {
                pickId = value;
                OnPropertyChanged();
            }
        }

        private string invoiceNumber;
        public string InvoiceNumber
        {
            get
            {
                return invoiceNumber;
            }
            set
            {
                invoiceNumber = value;
                OnPropertyChanged();
            }
        }

        private string orderNumber;
        public string OrderNumber
        {
            get
            {
                return orderNumber;
            }
            set
            {
                orderNumber = value;
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

        private int soCasesQuantity;
        public int SoCasesQuantity
        {
            get
            {
                return soCasesQuantity;
            }
            set
            {
                soCasesQuantity = value;
                OnPropertyChanged();
            }
        }

        private int soPiecesQuantity;
        public int SoPiecesQuantity
        {
            get
            {
                return soPiecesQuantity;
            }
            set
            {
                soPiecesQuantity = value;
                OnPropertyChanged();
            }
        }

        private int allocCasesQuantity;
        public int AllocCasesQuantity
        {
            get
            {
                return allocCasesQuantity;
            }
            set
            {
                allocCasesQuantity = value;
                OnPropertyChanged();
            }
        }

        private int allocPiecesQuantity;
        public int AllocPiecesQuantity
        {
            get
            {
                return allocPiecesQuantity;
            }
            set
            {
                allocPiecesQuantity = value;
                OnPropertyChanged();
            }
        }
    }
}
