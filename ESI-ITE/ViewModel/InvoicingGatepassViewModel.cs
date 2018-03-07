using ESI_ITE.Model;
using ESI_ITE.ViewModel.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Documents;
using System.Windows.Controls;
using ESI_ITE.View.PrintingTemplate;

namespace ESI_ITE.ViewModel
{
    public class InvoicingGatepassViewModel : ViewModelBase, IDataErrorInfo
    {
        public InvoicingGatepassViewModel()
        {
            Load();
            printGatepassCommand = new DelegateCommand(startPrinting);
            cancelCommand = new DelegateCommand(CancelGatepass);
        }

        #region Properties

        private ObservableCollection<List<string>> pickListCollection = new ObservableCollection<List<string>>();
        public ObservableCollection<List<string>> PickListCollection
        {
            get
            {
                return pickListCollection;
            }
            set
            {
                pickListCollection = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<List<string>> invoiceCollection = new ObservableCollection<List<string>>();
        public ObservableCollection<List<string>> InvoiceCollection
        {
            get
            {
                return invoiceCollection;
            }
            set
            {
                invoiceCollection = value;
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
                PickListSelectionChanged();
                OnPropertyChanged();
            }
        }

        private int selectedIndexPickList = -1;
        public int SelectedIndexPickList
        {
            get
            {
                return selectedIndexPickList;
            }
            set
            {
                selectedIndexPickList = value;
                OnPropertyChanged();
            }
        }

        #region Commands

        private DelegateCommand printGatepassCommand;
        public ICommand PrintGatepassCommand
        {
            get
            {
                return printGatepassCommand;
            }
        }

        private DelegateCommand cancelCommand;
        public ICommand CancelCommand
        {
            get
            {
                return cancelCommand;
            }
        }

        #endregion

        #endregion

        private void Load()
        {
            PickListCollection.Clear();

            var pickHeadObj = new PickListHeaderModel();
            var pickHeadList = pickHeadObj.FetchAll();

            var userObj = new UserModel();
            PickListCollection.Add(new List<string>());

            foreach (PickListHeaderModel pickHead in pickHeadList)
            {
                userObj = (UserModel)userObj.Fetch(pickHead.UserId.ToString(), "id");

                var list = new List<string>();
                list.Add(pickHead.HeaderNumber);
                list.Add(userObj.Username);
                list.Add(pickHead.Pickdate.ToString());

                PickListCollection.Add(list);
            }
        }

        private void PickListSelectionChanged()
        {
            if (SelectedIndexPickList > 0)
            {
                FetchInvoices();
            }
            else
            {
                InvoiceCollection.Clear();
            }
        }

        private void FetchInvoices()
        {
            InvoiceCollection.Clear();

            var invoiceObj = new InvoiceHeadModel();
            var pickHeadObj = new PickListHeaderModel();
            var orderObj = new SalesOrderModel();
            var customerObj = new CustomerModel();

            var invoiceHeadList = invoiceObj.FetchPerPickHead(SelectedPickList[0], "code");

            foreach (var invoice in invoiceHeadList)
            {
                orderObj = (SalesOrderModel)orderObj.Fetch(invoice.OrderId.ToString(), "id");
                customerObj = (CustomerModel)customerObj.Fetch(orderObj.CustomerID.ToString(), "id");

                var list = new List<string>();
                list.Add(invoice.InvoiceNumber);
                list.Add(customerObj.CustomerNumber);
                list.Add(customerObj.CustomerName);
                list.Add(invoice.InvoiceDate.ToString("MM/dd/yyyy"));
                list.Add(Math.Round(invoice.InvoiceAmount, 2).ToString());

                InvoiceCollection.Add(list);
            }
        }

        #region Printing

        private void startPrinting()
        {
            CallPrintingAsync();
        }

        private async void CallPrintingAsync()
        {
            var result = await GatepassPrintingAsync();

            MyGlobals.printingDoc = result;
            MyGlobals.PrintingParent = MyGlobals.InvoicingVM.SelectedPage;
            MyGlobals.InvoicingVM.SelectedPage = new PrintingMainPageView();
        }
        private Task<FixedDocument> GatepassPrintingAsync()
        {
            return Task.Factory.StartNew(() => GatepassPrinting());
        }

        private FixedDocument GatepassPrinting()
        {
            FixedDocument fixedDoc = null;

            System.Windows.Application.Current.Dispatcher.Invoke(() =>
            {
                fixedDoc = new FixedDocument();

                var gatepassPrintTemplateViewModel = new GatepassPrintTemplateViewModel();
                var allocatedStocks = new AllocatedStocksModel();
                var allocatedStocksList = allocatedStocks.FetchPerPickList(SelectedPickList[0]);
                var pickheadObj = new PickListHeaderModel();
                var gatepassItems = new List<GatepassItem>();
                var invoiceList = new List<string>();
                var customerList = new List<string>();
                var gatepass = new GatepassModel();

                var totalCases = 0;
                var totalPieces = 0;
                decimal totalWeight = 0;
                decimal totalValue = 0;

                pickheadObj = (PickListHeaderModel)pickheadObj.Fetch(SelectedPickList[0], "code");
                var gatepassList = gatepass.FetchPerPickhead(SelectedPickList[0], "code");
                if (gatepassList.Count == 0)
                {

                    var gatepassNumber = new GatepassNumberModel();
                    gatepassNumber = gatepassNumber.FetchNew();

                    foreach (var stock in allocatedStocksList)
                    {
                        var inventoryDummy = new InventoryDummy2Model();
                        var itemModel = new ItemModel();
                        var inventoryItem = new InventoryMaster2Model();

                        inventoryDummy = (InventoryDummy2Model)inventoryDummy.Fetch(stock.InventoryDummyId.ToString(), "id");
                        itemModel = (ItemModel)itemModel.Fetch(inventoryDummy.ItemCode, "code");
                        inventoryItem = (InventoryMaster2Model)inventoryItem.Fetch(stock.InventoryId.ToString(), "id");

                        gatepass.GatepassNumber = gatepassNumber.GatepassNumber;
                        gatepass.PickId = pickheadObj.Id;
                        gatepass.LocationId = inventoryItem.LocationId;
                        gatepass.ItemId = itemModel.ItemId;
                        gatepass.Cases = stock.Cases;
                        gatepass.Pieces = stock.Pieces;
                        gatepass.Expiry = inventoryItem.ExpirationDate;
                        gatepass.Weight = itemModel.UnitWeight * stock.Cases + ((itemModel.UnitWeight / itemModel.PackSize)* stock.Pieces);
                        gatepass.Date = DateTime.Now;

                        totalCases += stock.Cases;
                        totalPieces += stock.Pieces;
                        totalWeight += gatepass.Weight;

                        gatepass.AddNew(gatepass);
                    }
                    gatepassList = gatepass.FetchPerPickhead(SelectedPickList[0], "code");
                }

                foreach (var row in gatepassList)
                {
                    var gatepassItem = new GatepassItem();
                    var locationObj = new LocationModel();
                    var itemObj = new ItemModel();

                    itemObj = (ItemModel)itemObj.Fetch(row.ItemId.ToString(), "id");

                    locationObj = (LocationModel)locationObj.Fetch(row.LocationId.ToString(), "id");

                    gatepassItem.Itemcode = itemObj.Code;
                    gatepassItem.Description = itemObj.Description;
                    gatepassItem.Location = locationObj.Code;
                    gatepassItem.Cases = row.Cases.ToString();
                    gatepassItem.CasesCode = itemObj.UnitMeasure;
                    gatepassItem.Pieces = row.Pieces.ToString();
                    gatepassItem.PiecesCode = itemObj.SmallestUnitSymbol;
                    gatepassItem.ExpirationDate = row.Expiry.ToString("MM/dd/yyyy");

                    gatepassItems.Add(gatepassItem);
                }


                //Get customers and invoices
                var invoiceHeadObj = new InvoiceHeadModel();
                var invoiceHeadList = invoiceHeadObj.FetchPerPickHead(SelectedPickList[0], "code");

                var orderObj = new SalesOrderModel();
                var customerObj = new CustomerModel();

                foreach (var row in invoiceHeadList)
                {
                    if (row.Cases > 0 || row.Pieces > 0)
                    {
                        invoiceList.Add(row.InvoiceNumber);

                        orderObj = (SalesOrderModel)orderObj.Fetch(row.OrderId.ToString(), "id");
                        customerObj = (CustomerModel)customerObj.Fetch(orderObj.CustomerID.ToString(), "id");

                        customerList.Add(customerObj.CustomerName);

                        totalValue += row.InvoiceAmount;
                    }
                }
                var pagenumber = 1;
                var newPage = CreateNewPage(gatepass, pagenumber++);
                fixedDoc.Pages.Add((PageContent)newPage[1]);
                gatepassPrintTemplateViewModel = (GatepassPrintTemplateViewModel)newPage[0];


                var lines = 0;

                // Items
                foreach (var row in gatepassItems)
                {
                    if (lines >= 38)
                    {
                        gatepassPrintTemplateViewModel.IsFooterVisible = false;

                        newPage = CreateNewPage(gatepass, pagenumber++);
                        fixedDoc.Pages.Add((PageContent)newPage[1]);
                        gatepassPrintTemplateViewModel = (GatepassPrintTemplateViewModel)newPage[0];

                        lines = 0;
                    }
                    else if (row.Description.Length >= 35)
                    {
                        lines += 2;
                    }
                    else
                    {
                        lines++;
                    }

                    gatepassPrintTemplateViewModel.ItemCollection.Add(row);
                }

                if (lines >= 38)
                {
                    newPage = CreateNewPage(gatepass, pagenumber++);
                    fixedDoc.Pages.Add((PageContent)newPage[1]);
                    gatepassPrintTemplateViewModel = (GatepassPrintTemplateViewModel)newPage[0];

                    lines = 0;
                }

                // Footer
                foreach (var invoice in invoiceList)
                {
                    gatepassPrintTemplateViewModel.AttachedInvoicesList.Add(invoice);
                }

                foreach (var customer in customerList)
                {
                    gatepassPrintTemplateViewModel.AttachedCustomerList.Add(customer);
                }

                gatepassPrintTemplateViewModel.TotalCases = totalCases;
                gatepassPrintTemplateViewModel.TotalPieces = totalPieces;
                gatepassPrintTemplateViewModel.TotalWeight = totalWeight;
                gatepassPrintTemplateViewModel.TotalValue = totalValue;
            });

            return fixedDoc;
        }

        private List<object> CreateNewPage(GatepassModel gatepass, int pageNumber)
        {
            var fixedPage = new FixedPage();
            var grid = new Grid();
            var templateView = new GatepassPrintTemplate();
            var templateVM = (GatepassPrintTemplateViewModel)templateView.DataContext;

            templateView.Width = 768;
            templateView.MinHeight = 100;

            fixedPage.Width = 768;
            fixedPage.Height = 1056;

            var userObj = new UserModel();
            userObj = (UserModel)userObj.Fetch(gatepass.UserId.ToString(), "id");

            templateVM.PageNumber = pageNumber.ToString();
            templateVM.GatepassNumber = gatepass.GatepassNumber;
            templateVM.PicklistNumber = SelectedPickList[0];
            templateVM.Username = userObj.Username;
            templateVM.GatePassDate = DateTime.Now.ToString("MM/dd/yyyy");
            templateVM.PrintTime = DateTime.Now.ToLongTimeString();

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

        private void CancelGatepass()
        {
            InvoiceCollection.Clear();
            SelectedIndexPickList = -1;
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
