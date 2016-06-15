using ESI_ITE.Data_Access;
using ESI_ITE.Model;
using ESI_ITE.View;
using ESI_ITE.ViewModel.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using MySql;
using System.Globalization;

namespace ESI_ITE.ViewModel
{
    class SalesOrderEntryViewModel: ViewModelBase, IDataErrorInfo
    {
        public SalesOrderEntryViewModel( )
        {
            lineItemCommand = new DelegateCommand(lineItem);
            searchCommand = new DelegateCommand(toggleSearchVisibility);
            loadCustomerCommand = new DelegateCommand(loadCustomer);
            deleteEntryCommand = new DelegateCommand(deleteEntry);
            deleteOrdersCommand = new DelegateCommand(toggleDeleteVisibility);

            isFirstLoad = true;
            Load();
        }

        #region Properties

        DataAccess db = new DataAccess();

        private ObservableCollection<List<string>> cmbOrders = new ObservableCollection<List<string>>();
        public ObservableCollection<List<string>> CmbOrders
        {
            get { return cmbOrders; }
            set { cmbOrders = value; }
        }

        private ObservableCollection<SalesOrderModel> orderCollection = new ObservableCollection<SalesOrderModel>();
        public ObservableCollection<SalesOrderModel> OrderCollection
        {
            get { return orderCollection; }
            set { orderCollection = value; }
        }

        private List<List<string>> customerList = new List<List<string>>();
        public List<List<string>> CustomerList
        {
            get { return customerList; }
            set { customerList = value; }
        }

        private List<DistrictModel> districtList = new List<DistrictModel>();
        public List<DistrictModel> DistrictList
        {
            get { return districtList; }
            set { districtList = value; }
        }

        private List<SalesOrderPriceTypeModel> priceTypeList = new List<SalesOrderPriceTypeModel>();
        public List<SalesOrderPriceTypeModel> PriceTypeList
        {
            get { return priceTypeList; }
            set { priceTypeList = value; }
        }

        private List<TermModel> termList = new List<TermModel>();
        public List<TermModel> TermList
        {
            get { return termList; }
            set { termList = value; }
        }

        private ObservableCollection<SalesmanModel> salesmanCollection = new ObservableCollection<SalesmanModel>();
        public ObservableCollection<SalesmanModel> SalesmanCollection
        {
            get { return salesmanCollection; }
            set { salesmanCollection = value; }
        }

        private List<RouteModel> routeList = new List<RouteModel>();
        public List<RouteModel> RouteList
        {
            get { return routeList; }
            set { routeList = value; }
        }

        private List<WareHouseModel> warehouseList = new List<WareHouseModel>();
        public List<WareHouseModel> WarehouseList
        {
            get { return warehouseList; }
            set { warehouseList = value; }
        }



        private List<string> selectedSalesOrder = new List<string>();
        public List<string> SelectedSalesOrder
        {
            get { return selectedSalesOrder; }
            set
            {
                selectedSalesOrder = value;
                SelectedOrderChanged();
                OnPropertyChanged("SelectedSalesOrder");
            }
        }

        private DistrictModel selectedDistrict = new DistrictModel();
        public DistrictModel SelectedDistrict
        {
            get { return selectedDistrict; }
            set
            {
                selectedDistrict = value;
                OnPropertyChanged("SelectedDistrict");
                selectedDistrictChanged();
            }
        }

        private SalesOrderPriceTypeModel selectedPriceType = new SalesOrderPriceTypeModel();
        public SalesOrderPriceTypeModel SelectedPriceType
        {
            get { return selectedPriceType; }
            set
            {
                selectedPriceType = value;
                OnPropertyChanged("SelectedPriceType");
            }
        }

        private TermModel selectedTerm = new TermModel();
        public TermModel SelectedTerm
        {
            get { return selectedTerm; }
            set
            {
                selectedTerm = value;
                OnPropertyChanged("SelectedTerm");
            }
        }

        private SalesmanModel selectedSalesman = new SalesmanModel();
        public SalesmanModel SelectedSalesman
        {
            get { return selectedSalesman; }
            set
            {
                selectedSalesman = value;
                OnPropertyChanged("SelectedSalesman");
            }
        }

        private RouteModel selectedRoute = new RouteModel();
        public RouteModel SelectedRoute
        {
            get { return selectedRoute; }
            set
            {
                selectedRoute = value;
                OnPropertyChanged("SelectedRoute");
            }
        }

        private WareHouseModel selectedWarehouse = new WareHouseModel();
        public WareHouseModel SelectedWarehouse
        {
            get { return selectedWarehouse; }
            set
            {
                selectedWarehouse = value;
                OnPropertyChanged("SelectedWarehouse");
            }
        }

        private List<string> selectedSearchedCustomer;
        public List<string> SelectedSearchedCustomer
        {
            get { return selectedSearchedCustomer; }
            set
            {
                selectedSearchedCustomer = value;
                OnPropertyChanged("SelectedSearchedCustomer");
                searchedCustomerChanged();
            }
        }



        private int selectedIndexSalesOrder;
        public int SelectedIndexSalesOrder
        {
            get { return selectedIndexSalesOrder; }
            set
            {
                selectedIndexSalesOrder = value;
                OnPropertyChanged("SelectedIndexSalesOrder");
            }
        }

        private int selectedIndexDistrict;
        public int SelectedIndexDistrict
        {
            get { return selectedIndexDistrict; }
            set
            {
                selectedIndexDistrict = value;
                OnPropertyChanged("SelectedIndexDistrict");
            }
        }

        private int selectedIndexPriceType;
        public int SelectedIndexPriceType
        {
            get { return selectedIndexPriceType; }
            set
            {
                selectedIndexPriceType = value;
                OnPropertyChanged("SelectedIndexPriceType");
            }
        }

        private int selectedIndexTerm;
        public int SelectedIndexTerm
        {
            get { return selectedIndexTerm; }
            set
            {
                selectedIndexTerm = value;
                OnPropertyChanged("SelectedIndexTerm");
            }
        }

        private int selecedIndexSalesman;
        public int SelectedIndexSalesman
        {
            get { return selecedIndexSalesman; }
            set
            {
                selecedIndexSalesman = value;
                OnPropertyChanged("SelectedIndexSalesman");
            }
        }

        private int selectedIndexRoute;
        public int SelectedIndexRoute
        {
            get { return selectedIndexRoute; }
            set
            {
                selectedIndexRoute = value;
                OnPropertyChanged("SelectedIndexRoute");
            }
        }

        private int selectedIndexWarehouse;
        public int SelectedIndexWarehouse
        {
            get { return selectedIndexWarehouse; }
            set
            {
                selectedIndexWarehouse = value;
                OnPropertyChanged("SelectedIndexWarehouse");
            }
        }

        private int selectedIndexSearchedCustomer;
        public int SelectedIndexSearchedCustomer
        {
            get { return selectedIndexSearchedCustomer; }
            set
            {
                selectedIndexSearchedCustomer = value;
                OnPropertyChanged("SelectedIndexSearchedCustomer");
            }
        }

        private string customerNumber;
        public string CustomerNumber
        {
            get { return customerNumber; }
            set
            {
                customerNumber = value;
                OnPropertyChanged("CustomerNumber");
            }
        }

        private string orderDate;
        public string OrderDate
        {
            get { return orderDate; }
            set
            {
                orderDate = value;
                OnPropertyChanged("OrderDate");
            }
        }

        private string requiredShipDate;
        public string RequiredShipDate
        {
            get { return requiredShipDate; }
            set
            {
                requiredShipDate = value;
                OnPropertyChanged("RequiredShipDate");
            }
        }

        private string customerPONumber;
        public string CustomerPONumber
        {
            get { return customerPONumber; }
            set
            {
                customerPONumber = value;
                OnPropertyChanged("CustomerPONumber");
            }
        }

        private string taxRate;
        public string TaxRate
        {
            get { return taxRate; }
            set
            {
                taxRate = value;
                OnPropertyChanged("TaxRate");
            }
        }

        private string orderNote;
        public string OrderNote
        {
            get { return orderNote; }
            set
            {
                orderNote = value;
                OnPropertyChanged("OrderNote");
            }
        }

        private string lastSOEntered;
        public string LastSOEntered
        {
            get { return lastSOEntered; }
            set
            {
                lastSOEntered = value;
                OnPropertyChanged("LastSOEntered");
            }
        }

        private string orderAmount;
        public string OrderAmount
        {
            get { return orderAmount; }
            set
            {
                orderAmount = value;
                OnPropertyChanged("OrderAmount");
            }
        }

        private string discount;
        public string Discount
        {
            get { return discount; }
            set
            {
                discount = value;
                OnPropertyChanged("Discount");
            }
        }

        private string creditLimit;
        public string CreditLimit
        {
            get { return creditLimit; }
            set
            {
                creditLimit = value;
                OnPropertyChanged("CreditLimit");
            }
        }

        private string totalCases;
        public string TotalCases
        {
            get { return totalCases; }
            set
            {
                totalCases = value;
                OnPropertyChanged("TotalCases");
            }
        }

        private string totalPieces;
        public string TotalPieces
        {
            get { return totalPieces; }
            set
            {
                totalPieces = value;
                OnPropertyChanged("TotalPieces");
            }
        }

        private string searchedCustomer;
        public string SearchedCustomer
        {
            get { return searchedCustomer; }
            set
            {
                searchedCustomer = value;
                OnPropertyChanged("SearchedCustomer");
                SearchCustomer(value);
            }
        }

        private CustomerModel customer = new CustomerModel();
        public CustomerModel Customer
        {
            get { return customer; }
            set
            {
                customer = value;
                OnPropertyChanged("Customer");
            }
        }

        private string customerName;
        public string CustomerName
        {
            get { return customerName; }
            set
            {
                customerName = value;
                OnPropertyChanged("CustomerName");
            }
        }

        private string customerAddress;
        public string CustomerAddress
        {
            get { return customerAddress; }
            set
            {
                customerAddress = value;
                OnPropertyChanged("CustomerAddress");
            }
        }

        private string txtCutOffDate = "MM/DD/YYYY";
        public string TxtCutOffDate
        {
            get { return txtCutOffDate; }
            set
            {
                txtCutOffDate = value;
                OnPropertyChanged("TxtCutOffDate");
                cutOffDateChanged();
            }
        }

        private int caretIndex = 2;
        public int CaretIndex
        {
            get { return caretIndex; }
            set
            {
                caretIndex = value;
                OnPropertyChanged("CaretIndex");
            }
        }


        #region Commands

        private DelegateCommand lineItemCommand;
        public ICommand LineItemCommand
        {
            get
            {
                return lineItemCommand;
            }
        }

        private DelegateCommand deleteEntryCommand;
        public ICommand DeleteEntryCommand
        {
            get
            {
                return deleteEntryCommand;
            }
        }

        private DelegateCommand closeCommand;
        public ICommand CloseCommand
        {
            get
            {
                return closeCommand;
            }
        }

        private DelegateCommand searchCommand;
        public ICommand SearchCommand
        {
            get
            {
                return searchCommand;
            }
        }

        private DelegateCommand loadCustomerCommand;
        public ICommand LoadCustomerCommand
        {
            get
            {
                return loadCustomerCommand;
            }
        }

        private DelegateCommand deleteOrdersCommand;
        public ICommand DeleteOrdersCommand
        {
            get
            {
                return deleteOrdersCommand;
            }
        }


        #endregion


        #region Flags

        bool isFirstLoad = true;

        private bool isEnabled = true;
        public bool IsEnabled
        {
            get { return isEnabled; }
            set
            {
                isEnabled = value;
                OnPropertyChanged("IsEnabled");
            }
        }

        private bool isLineable;
        public bool IsLineable
        {
            get { return isLineable; }
            set
            {
                isLineable = value;
                OnPropertyChanged("IsLineable");
            }
        }

        private bool isNewOrder;
        public bool IsNewOrder
        {
            get { return isNewOrder; }
            set
            {
                isNewOrder = value;
                OnPropertyChanged("IsNewOrder");
            }
        }

        private bool isSearchVisible = false;
        public bool IsSearchVisible
        {
            get { return isSearchVisible; }
            set
            {
                isSearchVisible = value;
                OnPropertyChanged("IsSearchVisible");
            }
        }

        private bool isDeleteable;
        public bool IsDeleteable
        {
            get { return isDeleteable; }
            set
            {
                isDeleteable = value;
                OnPropertyChanged("IsDeleteable");
            }
        }

        private bool isDeletionVisible = false;
        public bool IsDeletionVisible
        {
            get { return isDeletionVisible; }
            set
            {
                isDeletionVisible = value;
                OnPropertyChanged("IsDeletionVisible");
            }
        }

        private bool isDateValid;
        public bool IsDateValid
        {
            get { return isDateValid; }
            set
            {
                isDateValid = value;
                OnPropertyChanged("IsDateValid");
            }
        }


        #endregion

        private string latestOrderNumber;
        private DateTime oldestSalesOrderDate;

        #endregion

        private void Load( )
        {
            isFirstLoad = true;

            //ORDERS LIST 
            var so = new SalesOrderModel();
            var list = new List<object>();
            list = so.FetchAll();

            foreach ( var row in list )
            {
                var order = new SalesOrderModel();
                var orderCust = new List<string>();

                order = (SalesOrderModel)row;

                orderCust.Add(order.OrderNumber);

                var custName = db.Select("select customer_name from customers where customer_id = '" + order.CustomerID + "' limit 1");

                orderCust.Add(custName);

                CmbOrders.Add(orderCust);

                OrderCollection.Add(order);
            }

            var orderNumber = new OrderNumberModel();
            var x = new string[2];
            orderNumber = (OrderNumberModel)orderNumber.Fetch("", "");
            latestOrderNumber = orderNumber.OrderNumber;
            x[0] = orderNumber.OrderNumber;
            x[1] = "(New Order)";

            CmbOrders.Insert(0, x.ToList());

            SelectedSalesOrder = null;
            SelectedIndexSalesOrder = -1;

            OrderDate = DateTime.Now.Date.ToShortDateString();

            RequiredShipDate = DateTime.Now.AddDays(1).Date.ToShortDateString();

            //DISTRICTS LIST
            var district = new DistrictModel();
            list = district.FetchAll();

            DistrictList.Add(district);

            foreach ( var row in list )
            {
                district = (DistrictModel)row;
                DistrictList.Add(district);
            }
            DistrictList.OrderBy(o => o.DistrictNumber);

            //PRICETYPE LIST
            var pricetype = new SalesOrderPriceTypeModel();
            list = pricetype.FetchAll();

            PriceTypeList.Add(pricetype);

            foreach ( var row in list )
            {
                pricetype = (SalesOrderPriceTypeModel)row;
                PriceTypeList.Add(pricetype);
            }
            PriceTypeList.OrderBy(o => o.Code);

            //TERMS LIST
            var term = new TermModel();
            list = term.FetchAll();

            TermList.Add(term);

            foreach ( var row in list )
            {
                term = (TermModel)row;
                TermList.Add(term);
            }
            TermList.OrderBy(o => o.TermCode);

            //SALESMAN LIST
            var salesman = new SalesmanModel();
            list = salesman.FetchAll();

            SalesmanCollection.Add(salesman);

            foreach ( var row in list )
            {
                salesman = (SalesmanModel)row;
                SalesmanCollection.Add(salesman);
            }
            SalesmanCollection.OrderBy(o => o.SalesmanNumber);

            //ROUTE LIST
            var route = new RouteModel();
            list = route.FetchAll();

            RouteList.Add(route);

            foreach ( var row in list )
            {
                route = (RouteModel)row;
                RouteList.Add(route);
            }
            RouteList.OrderBy(o => o.RouteCode);

            //WAREHOUSE LIST
            var warehouse = new WareHouseModel();
            var whList = warehouse.FetchAll();

            WarehouseList.Add(warehouse);

            foreach ( var row in whList )
            {
                WarehouseList.Add(row);
            }
            WarehouseList.OrderBy(o => o.Code);

            //CUSTOMER LIST
            var customer = new CustomerModel();
            var cList = db.SelectMultiple("select customer_number, customer_name from customers order by customer_name asc");

            foreach ( var row in cList )
            {
                var clone = row.Clone();
                var newList = new List<string>();

                newList.Add(row["customer_number"].ToString());
                newList.Add(row["customer_name"].ToString());

                CustomerList.Add(newList);
            }

            //isFirstLoad = false;

        }

        private void SelectedOrderChanged( )
        {
            resetValidProperties();
            if ( SelectedSalesOrder != null )
                if ( SelectedSalesOrder[0] == latestOrderNumber )
                {
                    ClearForm();
                }
                else
                {
                    isFirstLoad = false;
                    FillForm();
                    IsEnabled = false;
                }
        }

        private void ClearForm( )
        {
            isFirstLoad = true;

            CustomerNumber = "";
            CustomerName = "";
            CustomerAddress = "";
            Customer = new CustomerModel();
            OrderDate = DateTime.Now.Date.ToShortDateString();
            RequiredShipDate = DateTime.Now.Date.AddDays(1).ToShortDateString();
            SelectedIndexDistrict = 0;
            SelectedDistrict = new DistrictModel();
            SelectedIndexPriceType = 0;
            SelectedPriceType = new SalesOrderPriceTypeModel();
            SelectedIndexTerm = 0;
            SelectedTerm = new TermModel();
            SelectedIndexSalesman = 0;
            SelectedSalesman = new SalesmanModel();
            SelectedIndexRoute = 0;
            SelectedRoute = new RouteModel();
            SelectedIndexWarehouse = 0;
            SelectedWarehouse = new WareHouseModel();
            CustomerPONumber = "";
            TaxRate = "0";
            OrderNote = "";
            OrderAmount = "0";
            Discount = "0";
            CreditLimit = "0";
            TotalCases = "0";
            TotalPieces = "0";
            LastSOEntered = latestOrderNumber;

            IsEnabled = true;
            isFirstLoad = false;

        }

        private void FillForm( )
        {
            var order = new SalesOrderModel();
            var temp = new object();

            temp = order.Fetch(SelectedSalesOrder[0], "code");
            order = (SalesOrderModel)temp;

            temp = Customer.Fetch(order.CustomerID.ToString(), "id");
            Customer = (CustomerModel)temp;

            CustomerNumber = Customer.CustomerNumber;
            CustomerName = Customer.CustomerName;
            CustomerAddress = Customer.AddressMain + ", " + Customer.AddressCity + ", " + customer.AddressProvince;

            OrderDate = order.OrderDate.Date.ToShortDateString();

            RequiredShipDate = order.RequiredShipDate.Date.ToShortDateString();

            SelectedDistrict = (DistrictModel)SelectedDistrict.Fetch(order.DistrictId.ToString(), "id");
            SelectedIndexDistrict = DistrictList.FindIndex(o => o.DistrictId == SelectedDistrict.DistrictId);

            SelectedPriceType = (SalesOrderPriceTypeModel)SelectedPriceType.Fetch(order.PriceId.ToString(), "id");
            SelectedIndexPriceType = PriceTypeList.FindIndex(o => o.PriceTypeId == SelectedPriceType.PriceTypeId);

            SelectedTerm = (TermModel)SelectedTerm.Fetch(order.TermId.ToString(), "id");
            SelectedIndexTerm = TermList.FindIndex(o => o.TermId == SelectedTerm.TermId);

            SelectedSalesman = (SalesmanModel)SelectedSalesman.Fetch(SelectedDistrict.Salesman.ToString(), "id");
            var x = 0;
            foreach ( var property in SalesmanCollection )
            {
                if ( property.SalesmanNumber == SelectedSalesman.SalesmanNumber )
                {
                    SelectedIndexSalesman = x;
                    break;
                }
                x++;
            }

            SelectedRoute = (RouteModel)SelectedRoute.Fetch(order.RouteId.ToString(), "id");
            SelectedIndexRoute = RouteList.FindIndex(o => o.RouteId == SelectedRoute.RouteId);

            SelectedWarehouse = (WareHouseModel)SelectedWarehouse.Fetch(order.WarehouseId.ToString(), "id");
            SelectedIndexWarehouse = WarehouseList.FindIndex(o => o.Id == SelectedWarehouse.Id);

            CustomerPONumber = order.PONumber;

            TaxRate = customer.TaxRate.ToString();

            OrderNote = order.OrderNote;

            OrderAmount = order.OrderAmount.ToString("#,##0.00");

            Discount = SelectedTerm.Discount1.ToString("#,##0.00");

            CreditLimit = customer.CreditLimit.ToString("#,##0.00");

            TotalCases = order.Cases.ToString();

            TotalPieces = order.Pieces.ToString();

            LastSOEntered = order.OrderNumber;
        }

        private void searchedCustomerChanged( )
        {
            if ( SelectedSearchedCustomer != null )
            {
                var customerId = db.Select("select customer_id from customers where customer_name = '" + MySql.Data.MySqlClient.MySqlHelper.EscapeString(SelectedSearchedCustomer[1]) + "'");

                Customer = Customer.Fetch(customerId, "id") as CustomerModel;
                CustomerName = Customer.CustomerName;
                CustomerAddress = Customer.AddressMain + ", " + Customer.AddressCity + ", " + customer.AddressProvince;

                TaxRate = Customer.TaxRate.ToString("0.##");
                CreditLimit = customer.CreditLimit.ToString("0.##");
            }
        }

        private void selectedDistrictChanged( )
        {
            if ( SelectedDistrict != null )
            {
                var x = 0;
                foreach ( var salesman in SalesmanCollection )
                {
                    if ( salesman.SalesmanId == SelectedDistrict.Salesman )
                    {
                        SelectedIndexSalesman = x;
                        break;
                    }
                    x++;
                }

                x = 0;
                foreach ( var warehouse in WarehouseList )
                {
                    if ( warehouse.Id == SelectedDistrict.Warehouse )
                    {
                        SelectedIndexWarehouse = x;
                        break;
                    }
                    x++;
                }
            }
        }

        private void lineItem( )
        {
            if ( latestOrderNumber == SelectedSalesOrder[0] )
            {
                var order = new SalesOrderModel();

                order.OrderNumber = SelectedSalesOrder[0];
                order.OrderDate = DateTime.Parse(OrderDate);
                order.RequiredShipDate = DateTime.Parse(RequiredShipDate);
                order.PONumber = CustomerPONumber;
                order.OrderNote = OrderNote;
                order.OrderAmount = decimal.Parse(OrderAmount);
                order.Cases = int.Parse(TotalCases);
                order.Pieces = int.Parse(TotalPieces);
                order.DistrictId = SelectedDistrict.DistrictId;
                order.CustomerID = Customer.CustomerId;
                order.RouteId = SelectedRoute.RouteId;
                order.TermId = SelectedTerm.TermId;
                order.PriceId = SelectedPriceType.PriceTypeId;
                order.WarehouseId = SelectedWarehouse.Id;

                var orderNumber = new OrderNumberModel();
                orderNumber.UpdateItem("");

                latestOrderNumber = ((OrderNumberModel)orderNumber.Fetch("", "")).OrderNumber;

                updateOrdersList(order);
                order.AddNew(order);

                FillForm();

                MyGlobals.SalesOrder = order;
                MyGlobals.SoViewModel.SelectedPage = new SOLineItemsView();
            }
            else
            {
                foreach ( var order in OrderCollection )
                {
                    if ( order.OrderNumber == SelectedSalesOrder[0] )
                    {
                        MyGlobals.SalesOrder = order;
                        MyGlobals.SoViewModel.SelectedPage = new SOLineItemsView();
                        break;
                    }
                }
            }
        }

        private void updateOrdersList( SalesOrderModel so )
        {
            var orderNumber = new OrderNumberModel();

            var newOrder = new string[2];
            newOrder[0] = selectedSalesOrder[0];
            newOrder[1] = Customer.CustomerName;

            CmbOrders.Insert(0, newOrder.ToList());
            CmbOrders.RemoveAt(1);

            SelectedSalesOrder = newOrder.ToList();

            orderNumber = (OrderNumberModel)orderNumber.Fetch("", "");
            newOrder[0] = orderNumber.OrderNumber;
            newOrder[1] = "(New Order)";

            CmbOrders.Insert(0, newOrder.ToList());

            SelectedIndexSalesOrder = 1;

            OrderCollection.Insert(0, so);
        }

        private void toggleSearchVisibility( )
        {
            if ( IsSearchVisible )
            {
                IsSearchVisible = false;
                IsEnabled = true;
            }
            else
            {
                IsEnabled = false;
                IsSearchVisible = true;
            }
        }

        private void SearchCustomer( string value )
        {
            //SelectedIndexSearchedCustomer = CustomerList.FindIndex(o => o[1].StartsWith(value));
            var x = 0;
            foreach ( var customer in CustomerList )
            {
                if ( customer[1].ToLower().StartsWith(value.ToLower()) )
                {
                    SelectedIndexSearchedCustomer = x;
                    SelectedSearchedCustomer = customer;
                    break;
                }
                x++;
            }
        }

        private void loadCustomer( )
        {
            if ( SelectedSearchedCustomer != null )
            {
                CustomerNumber = Customer.CustomerNumber;
                toggleSearchVisibility();
            }
        }

        private void deleteEntry( )
        {
            if ( SelectedSalesOrder[0] == latestOrderNumber )
            {
                ClearForm();
            }
        }

        private void toggleDeleteVisibility( )
        {
            if ( IsDeletionVisible )
            {
                IsDeletionVisible = false;
                IsEnabled = true;
            }
            else
            {
                IsEnabled = false;
                IsDeletionVisible = true;
                isFirstLoad = false;
                TxtCutOffDate = "MM/DD/YYYY";
                oldestSalesOrderDate = DateTime.Parse(db.Select("select order_date from orders order by order_date asc limit 1"));
            }
        }

        private void deleteOrders( )
        {
            if ( IsDateValid )
            {
                var order = new SalesOrderModel();
                order.DeleteOrders(DateTime.Parse(TxtCutOffDate));
            }
        }

        private void cutOffDateChanged( )
        {
            var text = TxtCutOffDate;
        }

        #region IDataErrorInfo Members

        public string Error
        {
            get
            {
                return null;
            }
        }

        public string this[string propertyname]
        {
            get
            {
                if ( isFirstLoad == false )
                    return GetValidationError(propertyname);
                else
                    return null;
            }
        }

        #endregion

        #region Validation Members

        string[] validProperties = { "Error", "Error", "Error", "Error", "Error", "Error", "Error", "Error", "Error" };

        private void resetValidProperties( )
        {
            int x = validProperties.Length;
            for ( int i = 0;i < x;i++ )
            {
                validProperties[i] = "Error";
            }
            IsLineable = false;
            IsDeleteable = false;
        }

        private void IsValid( )
        {
            int fieldNumber = 0;
            int errorCounter = 0;

            Debug.WriteLine("VALIDATION");

            foreach ( var field in validProperties )
            {
                if ( field != null )
                {
                    errorCounter++;
                    Debug.WriteLine("Error on field: " + fieldNumber);
                }
                fieldNumber++;
            }

            if ( errorCounter > 0 )
            {
                IsLineable = false;
                IsDeleteable = false;
            }
            else
            {
                IsLineable = true;
                IsDeleteable = true;
            }
        }

        private string GetValidationError( string propertyname )
        {
            string error = null;

            switch ( propertyname )
            {
                case "SelectedSalesOrder":
                    if ( SelectedSalesOrder != null )
                    {
                        error = ValidateNullOrEmpty("Order Number", SelectedSalesOrder[0]);
                        if ( string.IsNullOrWhiteSpace(error) )
                            validProperties[0] = null;
                        else
                            validProperties[0] = error;
                    }
                    break;
                case "CustomerNumber":
                    error = ValidateNullOrEmpty("Customer", CustomerNumber);
                    if ( string.IsNullOrWhiteSpace(error) )
                        validProperties[1] = null;
                    else
                        validProperties[1] = error;
                    break;
                case "SelectedDistrict":
                    error = ValidateNullOrEmpty("District", SelectedDistrict.DistrictNumber);
                    if ( string.IsNullOrWhiteSpace(error) )
                        validProperties[2] = null;
                    else
                        validProperties[2] = error;
                    break;
                case "SelectedPriceType":
                    error = ValidateNullOrEmpty("Price Type", SelectedPriceType.Code);
                    if ( string.IsNullOrWhiteSpace(error) )
                        validProperties[3] = null;
                    else
                        validProperties[3] = error;
                    break;
                case "SelectedTerm":
                    error = ValidateNullOrEmpty("Term", SelectedTerm.TermCode);
                    if ( string.IsNullOrWhiteSpace(error) )
                        validProperties[4] = null;
                    else
                        validProperties[4] = error;
                    break;
                case "SelectedSalesman":
                    error = ValidateNullOrEmpty("Salesman", SelectedSalesman.SalesmanNumber);
                    if ( string.IsNullOrWhiteSpace(error) )
                        validProperties[5] = null;
                    else
                        validProperties[5] = error;
                    break;
                case "SelectedRoute":
                    error = ValidateNullOrEmpty("Route", SelectedRoute.RouteCode);
                    if ( string.IsNullOrWhiteSpace(error) )
                        validProperties[6] = null;
                    else
                        validProperties[6] = error;
                    break;
                case "SelectedWarehouse":
                    error = ValidateNullOrEmpty("Warehouse", SelectedWarehouse.Code);
                    if ( string.IsNullOrWhiteSpace(error) )
                        validProperties[7] = null;
                    else
                        validProperties[7] = error;
                    break;
                case "TaxRate":
                    error = ValidateNullOrEmpty("Tax Rate", TaxRate);
                    if ( string.IsNullOrWhiteSpace(error) )
                        validProperties[8] = null;
                    else
                        validProperties[8] = error;
                    break;
                case "TxtCutOffDate":
                    error = ValidateNullOrEmpty("Cut-off Date", TxtCutOffDate);

                    if ( string.IsNullOrWhiteSpace(error) )
                        if ( TxtCutOffDate.Length < 10 )
                            error = "Invalid date!";

                    if ( string.IsNullOrWhiteSpace(error) )
                        error = CheckDate(TxtCutOffDate);

                    if ( string.IsNullOrWhiteSpace(error) )
                        IsDateValid = true;
                    else
                        IsDateValid = false;
                    break;
                    #region
                    //case "OrderNote":
                    //    error = ValidateNullOrEmpty("Order Note", OrderNote);
                    //    if ( string.IsNullOrWhiteSpace(error) )
                    //        validProperties[12] = null;
                    //    else
                    //        validProperties[12] = error;
                    //    break;
                    //case "CustomerPONumber":
                    //    error = ValidateNullOrEmpty("Customer P.O. Number", CustomerPONumber);
                    //    if ( string.IsNullOrWhiteSpace(error) )
                    //        validProperties[10] = null;
                    //    else
                    //        validProperties[10] = error;
                    //    break;
                    //case "OrderDate":
                    //    error = ValidateNullOrEmpty("Order Date", OrderDate.ToString());
                    //    if ( string.IsNullOrWhiteSpace(error) )
                    //        validProperties[2] = null;
                    //    else
                    //        validProperties[2] = error;
                    //    break;
                    //case "RequiredShipDate":
                    //    error = ValidateNullOrEmpty("Required Ship Date", RequiredShipDate.ToString());
                    //    if ( string.IsNullOrWhiteSpace(error) )
                    //        validProperties[3] = null;
                    //    else
                    //        validProperties[3] = error;
                    //    break;
                    #endregion
            }

            IsValid();

            return error;
        }

        private string ValidateNullOrEmpty( string propertyName, string value )
        {
            if ( string.IsNullOrWhiteSpace(value) )
                return propertyName + " cannot be empty!";
            else
                return null;
        }

        private string CheckDate( string inputDate )
        {
            string error = null;
            DateTime date;

            try
            {
                date = DateTime.Parse(inputDate, CultureInfo.CreateSpecificCulture("en-US"));

                if ( date < oldestSalesOrderDate )
                    error = "Invalid date!";
            }
            catch ( Exception e )
            {
                error = "Invalid date!";
            }

            return error;
        }

        #endregion
    }
}
