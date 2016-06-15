using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ESI_ITE.Model;
using ESI_ITE.Data_Access;
using ESI_ITE.ViewModel.Command;
using System.Windows.Input;
using System.Collections.ObjectModel;

namespace ESI_ITE.ViewModel
{
    public class SOLineItemViewModel: ViewModelBase
    {
        public SOLineItemViewModel( )
        {
            closeCommand = new DelegateCommand(closePage);

            Load();
        }

        #region Properties

        DataAccess db = new DataAccess();

        private SalesOrderModel salesOrder = new SalesOrderModel();
        private CustomerModel customer = new CustomerModel();
        private SalesmanModel salesman = new SalesmanModel();
        private DistrictModel district = new DistrictModel();
        private TermModel term = new TermModel();
        private SalesOrderPriceTypeModel priceType = new SalesOrderPriceTypeModel();
        private LocationModel location = new LocationModel();
        private WareHouseModel warehouse = new WareHouseModel();

        private ObservableCollection<InventoryDummy2Model> datagridItems;
        public ObservableCollection<InventoryDummy2Model> DatagridItems
        {
            get { return datagridItems; }
            set
            {
                datagridItems = value;
                OnPropertyChanged("DatagridItems");
            }
        }

        private InventoryDummy2Model selectedItem;
        public InventoryDummy2Model SelectedItem
        {
            get { return selectedItem; }
            set
            {
                selectedItem = value;
                OnPropertyChanged("SelectedItem");
            }
        }

        private string txtCustomerNumber;
        public string TxtCustomerNumber
        {
            get { return txtCustomerNumber; }
            set
            {
                txtCustomerNumber = value;
                OnPropertyChanged("TxtCustomerNumber");
            }
        }

        private string txtCustomerName;
        public string TxtCustomerName
        {
            get { return txtCustomerName; }
            set
            {
                txtCustomerName = value;
                OnPropertyChanged("TxtCustomerName");
            }
        }

        private string txtSalesmanNumber;
        public string TxtSalesmanNumber
        {
            get { return txtSalesmanNumber; }
            set
            {
                txtSalesmanNumber = value;
                OnPropertyChanged("TxtSalesmanNumber");
            }
        }

        private string txtSalesmanName;
        public string TxtsalesmanName
        {
            get { return txtSalesmanName; }
            set
            {
                txtSalesmanName = value;
                OnPropertyChanged("TxtsalesmanName");
            }
        }

        private string txtTermCode;
        public string TxtTermCode
        {
            get { return txtTermCode; }
            set
            {
                txtTermCode = value;
                OnPropertyChanged("TxtTermCode");
            }
        }

        private string txtTermDescription;
        public string TxtTermDescription
        {
            get { return txtTermDescription; }
            set
            {
                txtTermDescription = value;
                OnPropertyChanged("TxtTermDescription");
            }
        }

        private string txtOrderNumber;
        public string TxtOrderNumber
        {
            get { return txtOrderNumber; }
            set
            {
                txtOrderNumber = value;
                OnPropertyChanged("TxtOrderNumber");
            }
        }

        private string txtTotalCases;
        public string TxtTotalCases
        {
            get { return txtTotalCases; }
            set
            {
                txtTotalCases = value;
                OnPropertyChanged("TxtTotalCases");
            }
        }

        private string txtTotalPieces;
        public string TxtTotalPieces
        {
            get { return txtTotalCases; }
            set
            {
                txtTotalPieces = value;
                OnPropertyChanged("TxtTotalPieces");
            }
        }

        private string txtItemCode;
        public string TxtItemCode
        {
            get { return txtItemCode; }
            set
            {
                txtItemCode = value;
                OnPropertyChanged("TxtItemCode");
            }
        }

        private string txtPT;
        public string TxtPT
        {
            get { return txtPT; }
            set
            {
                txtPT = value;
                OnPropertyChanged("TxtPT");
            }
        }

        private string txtLC;
        public string TxtLC
        {
            get { return txtLC; }
            set
            {
                txtLC = value;
                OnPropertyChanged("TxtLC");
            }
        }

        private string txtCases;
        public string TxtCases
        {
            get { return txtCases; }
            set
            {
                txtCases = value;
                OnPropertyChanged("txtCases");
            }
        }

        private string txtPieces;
        public string TxtPieces
        {
            get { return txtPieces; }
            set
            {
                txtPieces = value;
                OnPropertyChanged("TxtPieces");
            }
        }

        private string txtUnitPrice;
        public string TxtUnitPrice
        {
            get { return txtUnitPrice; }
            set
            {
                txtUnitPrice = value;
                OnPropertyChanged("TxtUnitPrice");
            }
        }

        private string txtTaxRate;
        public string TxtTaxRate
        {
            get { return txtTaxRate; }
            set
            {
                txtTaxRate = value;
                OnPropertyChanged("TxtTaxRate");
            }
        }

        private string txtWarehouse;
        public string TxtWarehouse
        {
            get { return txtWarehouse; }
            set
            {
                txtWarehouse = value;
                OnPropertyChanged("TxtWarehouse");
            }
        }

        private string txbItemName;
        public string TxbItemName
        {
            get { return txbItemName; }
            set
            {
                txbItemName = value;
                OnPropertyChanged("TxbItemName");
            }
        }

        private string txtOrderAmount;
        public string TxtOrderAmount
        {
            get { return txtOrderAmount; }
            set
            {
                txtOrderAmount = value;
                OnPropertyChanged("TxtOrderAmount");
            }
        }

        private string txtPesoDiscount;
        public string TxtPesoDiscount
        {
            get { return txtPesoDiscount; }
            set
            {
                txtPesoDiscount = value;
                OnPropertyChanged("TxtPesoDiscount");
            }
        }

        private string txtTruckingCharge;
        public string TxtTruckingCharge
        {
            get { return txtTruckingCharge; }
            set
            {
                txtTruckingCharge = value;
                OnPropertyChanged("TxtTruckingCharge");
            }
        }


        #region Commands

        private DelegateCommand closeCommand;
        public ICommand CloseCommand
        {
            get { return closeCommand; }
        }

        #endregion

        #endregion


        private void Load( )
        {
            salesOrder = MyGlobals.SalesOrder;
            customer = (CustomerModel)customer.Fetch(salesOrder.CustomerID.ToString(), "id");
            district = (DistrictModel)district.Fetch(salesOrder.DistrictId.ToString(), "id");
            term = (TermModel)term.Fetch(salesOrder.TermId.ToString(), "id");
            priceType = (SalesOrderPriceTypeModel)priceType.Fetch(salesOrder.PriceId.ToString(), "id");
            salesman = (SalesmanModel)salesman.Fetch(district.Salesman.ToString(), "id");
            warehouse = (WareHouseModel)warehouse.Fetch(salesOrder.WarehouseId.ToString(), "id");

            TxtCustomerNumber = customer.CustomerNumber;
            TxtCustomerName = customer.CustomerName;

            TxtSalesmanNumber = salesman.SalesmanNumber;
            TxtsalesmanName = salesman.SalesmanName;

            TxtTermCode = term.TermCode;
            TxtTermDescription = term.TermDescription;

            TxtOrderNumber = salesOrder.OrderNumber;
            TxtTotalCases = salesOrder.Cases.ToString();
            TxtTotalPieces = salesOrder.Pieces.ToString();

            TxtPT = priceType.Code;
            TxtLC = "GD";

            TxtTaxRate = customer.TaxRate.ToString();
            TxtWarehouse = warehouse.Code;
        }

        private void closePage( )
        {
            MyGlobals.SoViewModel.SelectedPage = MyGlobals.SalesOrderEntryPage;
        }
    }
}
