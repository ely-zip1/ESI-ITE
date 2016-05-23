using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using ESI_ITE.Data_Access;
using ESI_ITE.Model;
using System.ComponentModel;
using ESI_ITE.ViewModel.Command;
using System.Windows.Input;

namespace ESI_ITE.ViewModel
{
    class SalesOrderEntryViewModel: ViewModelBase, IDataErrorInfo
    {
        public SalesOrderEntryViewModel( )
        {

        }

        #region Properties

        DataAccess db = new DataAccess();

        private ObservableCollection<SalesOrderModel> orderCollection;
        public ObservableCollection<SalesOrderModel> OrderCollection
        {
            get { return orderCollection; }
            set { orderCollection = value; }
        }

        private List<CustomerModel> customerList;
        public List<CustomerModel> CustomerList
        {
            get { return customerList; }
            set { customerList = value; }
        }

        private List<DistrictModel> districtList;
        public List<DistrictModel> DistrictList
        {
            get { return districtList; }
            set { districtList = value; }
        }

        private List<SalesOrderPriceTypeModel> priceTypeList;
        public List<SalesOrderPriceTypeModel> PriceTypeList
        {
            get { return priceTypeList; }
            set { priceTypeList = value; }
        }

        private List<TermModel> termList;
        public List<TermModel> TermList
        {
            get { return termList; }
            set { termList = value; }
        }

        private ObservableCollection<SalesmanModel> salesmanCollection;
        public ObservableCollection<SalesmanModel> SalesmanCollection
        {
            get { return salesmanCollection; }
            set { salesmanCollection = value; }
        }

        private List<RouteModel> routeList;
        public List<RouteModel> RouteList
        {
            get { return routeList; }
            set { routeList = value; }
        }

        private List<WareHouseModel> warehouseList;
        public List<WareHouseModel> WarehouseList
        {
            get { return warehouseList; }
            set { warehouseList = value; }
        }



        private SalesOrderModel selectedSalesOrder;
        public SalesOrderModel SelectedSalesOrder
        {
            get { return selectedSalesOrder; }
            set
            {
                selectedSalesOrder = value;
                OnPropertyChanged("SelectedSalesOrder");
            }
        }

        private DistrictModel selectedDistrict;
        public DistrictModel SelectedDistrict
        {
            get { return selectedDistrict; }
            set
            {
                selectedDistrict = value;
                OnPropertyChanged("SelectedDistrict");
            }
        }

        private SalesOrderPriceTypeModel selectedPriceType;
        public SalesOrderPriceTypeModel SelectedPriceType
        {
            get { return selectedPriceType; }
            set
            {
                selectedPriceType = value;
                OnPropertyChanged("SelectedPriceType");
            }
        }

        private TermModel selectedTerm;
        public TermModel SelectedTerm
        {
            get { return selectedTerm; }
            set
            {
                selectedTerm = value;
                OnPropertyChanged("SelectedTerm");
            }
        }

        private SalesmanModel selectedSalesman;
        public SalesmanModel SelectedSalesman
        {
            get { return selectedSalesman; }
            set
            {
                selectedSalesman = value;
                OnPropertyChanged("SelectedSalesman");
            }
        }

        private RouteModel selectedRoute;
        public RouteModel SelectedRoute
        {
            get { return selectedRoute; }
            set
            {
                selectedRoute = value;
                OnPropertyChanged("SelectedRoute");
            }
        }

        private WareHouseModel selectedWarehouse;
        public WareHouseModel SelectedWarehouse
        {
            get { return selectedWarehouse; }
            set
            {
                selectedWarehouse = value;
                OnPropertyChanged("SelectedWarehouse");
            }
        }



        private DateTime orderDate;
        public DateTime OrderDate
        {
            get { return orderDate; }
            set
            {
                orderDate = value;
                OnPropertyChanged("OrderDate");
            }
        }

        private DateTime requiredShipDate;
        public DateTime RequiredShipDate
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

        private decimal taxRate;
        public decimal TaxRate
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

        private decimal orderAmount;
        public decimal OrderAmount
        {
            get { return orderAmount; }
            set
            {
                orderAmount = value;
                OnPropertyChanged("OrderAmount");
            }
        }

        private decimal discount;
        public decimal Discount
        {
            get { return discount; }
            set
            {
                discount = value;
                OnPropertyChanged("Discount");
            }
        }

        private decimal creditLimit;
        public decimal CreditLimit
        {
            get { return creditLimit; }
            set
            {
                creditLimit = value;
                OnPropertyChanged("CreditLimit");
            }
        }

        private int totalCases;
        public int TotalCases
        {
            get { return totalCases; }
            set
            {
                totalCases = value;
                OnPropertyChanged("TotalCases");
            }
        }

        private int totalPieces;
        public int TotalPieces
        {
            get { return totalPieces; }
            set
            {
                totalPieces = value;
                OnPropertyChanged("TotalPieces");
            }
        }
        

        private DelegateCommand lineItemCommand;
        public ICommand LineItemCommand;

        private DelegateCommand deleteEntryCommand;
        public ICommand DeleteEntryCommand;

        private DelegateCommand closeCommand;
        public ICommand CloseCommand;

        #endregion

        private void Load( )
        {
            var so = new SalesOrderModel();
            var list = new List<object>();
            list = so.FetchAll();

            foreach(var row in list )
            {
                var order = new SalesOrderModel();
                order = (SalesOrderModel)row;
                OrderCollection.Add(order);
            }

        }

        #region IDataErrorInfo Members
        public string Error
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public string this[string columnName]
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        #endregion
    }
}
