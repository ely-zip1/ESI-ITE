using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ESI_ITE.Model;
using ESI_ITE.ViewModel.Command;

namespace ESI_ITE.ViewModel.CNDN
{
    public class CNLineItemViewModel : ViewModelBase
    {
        public CNLineItemViewModel()
        {
            exitCommand = new DelegateCommand(Exit);
            listValuesCommand = new DelegateCommand(ListItems);
            deleteLineCommand = new DelegateCommand(DeleteLineItem);
            selectItemSearchCommand = new DelegateCommand(SelectItem);
            addItemCommand = new DelegateCommand(AddItem);

            Load();
        }

        #region Properties

        private CreditNoteHeaderModel cnHeaderObject = new CreditNoteHeaderModel();

        private ObservableCollection<LineItem> linedItemCollection = new ObservableCollection<LineItem>();
        public ObservableCollection<LineItem> LinedItemCollection
        {
            get
            {
                return linedItemCollection;
            }
            set
            {
                linedItemCollection = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<ItemModel> searchItemCollection = new ObservableCollection<ItemModel>();
        public ObservableCollection<ItemModel> SearchItemCollection
        {
            get
            {
                return searchItemCollection;
            }
            set
            {
                searchItemCollection = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<PriceTypeModel> priceTypeList = new ObservableCollection<PriceTypeModel>();
        public ObservableCollection<PriceTypeModel> PriceTypeList
        {
            get
            {
                return priceTypeList;
            }
            set
            {
                priceTypeList = value;
                OnPropertyChanged();
            }
        }


        private ItemModel selectedSearchItem;
        public ItemModel SelectedSearchItem
        {
            get
            {
                return selectedSearchItem;
            }
            set
            {
                selectedSearchItem = value;
                OnPropertyChanged();
            }
        }

        private int selectedIndexSearchItem;
        public int SelectedIndexSearchItem
        {
            get
            {
                return selectedIndexSearchItem;
            }
            set
            {
                selectedIndexSearchItem = value;
                OnPropertyChanged();
            }
        }

        private List<string> selectedLinedItem;
        public List<string> SelectedLinedItem
        {
            get
            {
                return selectedLinedItem;
            }
            set
            {
                selectedLinedItem = value;
                OnPropertyChanged();
            }
        }

        private int selectedIndexLinedItem;
        public int SelectedIndexLinedItem
        {
            get
            {
                return selectedIndexLinedItem;
            }
            set
            {
                selectedIndexLinedItem = value;
                OnPropertyChanged();
            }
        }

        private bool isTenPercent;
        public bool IsTenPercent
        {
            get
            {
                return isTenPercent;
            }
            set
            {
                isTenPercent = value;
                OnPropertyChanged();
            }
        }

        private bool isTwelvePercent;
        public bool IsTwelvePercent
        {
            get
            {
                return isTwelvePercent;
            }
            set
            {
                isTwelvePercent = value;
                OnPropertyChanged();
            }
        }

        private string customerNumber;
        public string CustomerNumber
        {
            get
            {
                return customerNumber;
            }
            set
            {
                customerNumber = value;
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

        private string salesmanNumber;
        public string SalesmanNumber
        {
            get
            {
                return salesmanNumber;
            }
            set
            {
                salesmanNumber = value;
                OnPropertyChanged();
            }
        }

        private string salesmanName;
        public string SalesmanName
        {
            get
            {
                return salesmanName;
            }
            set
            {
                salesmanName = value;
                OnPropertyChanged();
            }
        }

        private string termCode;
        public string TermCode
        {
            get
            {
                return termCode;
            }
            set
            {
                termCode = value;
                OnPropertyChanged();
            }
        }

        private string termDescription;
        public string TermDescription
        {
            get
            {
                return termDescription;
            }
            set
            {
                termDescription = value;
                OnPropertyChanged();
            }
        }

        private string priceUsed;
        public string PriceUsed
        {
            get
            {
                return priceUsed;
            }
            set
            {
                priceUsed = value;
                OnPropertyChanged();
            }
        }

        private string cnNumber;
        public string CnNumber
        {
            get
            {
                return cnNumber;
            }
            set
            {
                cnNumber = value;
                OnPropertyChanged();
            }
        }

        private string totalCases;
        public string TotalCases
        {
            get
            {
                return totalCases;
            }
            set
            {
                totalCases = value;
                OnPropertyChanged();
            }
        }

        private string totalPieces;
        public string TotalPieces
        {
            get
            {
                return totalPieces;
            }
            set
            {
                totalPieces = value;
                OnPropertyChanged();
            }
        }


        private ItemModel selectedItem;
        public ItemModel SelectedItem
        {
            get { return selectedItem; }
            set { selectedItem = value; }
        }

        private string selectedItemCode;
        public string SelectedItemCode
        {
            get
            {
                return selectedItemCode;
            }
            set
            {
                selectedItemCode = value;
                OnPropertyChanged();

                if (value.Length == 6)
                    ItemCodeChanged(value);
            }
        }

        private string priceType;
        public string PriceType
        {
            get
            {
                return priceType;
            }
            set
            {
                priceType = value;
                OnPropertyChanged();
            }
        }

        private PriceTypeModel selectedPriceType;
        public PriceTypeModel SelectedPriceType
        {
            get { return selectedPriceType; }
            set
            {
                selectedPriceType = value;
                OnPropertyChanged();
            }
        }

        private string location;
        public string Location
        {
            get { return location; }
            set
            {
                location = value;
                OnPropertyChanged();
            }
        }

        private ReturnTypeModel selectedReturnType;
        public ReturnTypeModel SelectedReturnType
        {
            get
            {
                return selectedReturnType;
            }
            set
            {
                selectedReturnType = value;
                OnPropertyChanged();
            }
        }

        private int cases;
        public int Cases
        {
            get
            {
                return cases;
            }
            set
            {
                cases = value;
                OnPropertyChanged();
            }
        }

        private int pieces;
        public int Pieces
        {
            get
            {
                return pieces;
            }
            set
            {
                pieces = value;
                OnPropertyChanged();
            }
        }

        private List<PriceSellingModel> sellingPriceList = new List<PriceSellingModel>();
        public List<PriceSellingModel> SellingPriceList
        {
            get
            {
                return sellingPriceList;
            }
            set
            {
                sellingPriceList = value;
                OnPropertyChanged();
            }
        }

        private string pricePerPiece;
        public string PricePerPiece
        {
            get
            {
                return pricePerPiece;
            }
            set
            {
                pricePerPiece = value;
                OnPropertyChanged();
            }
        }

        private string taxRate;
        public string TaxRate
        {
            get
            {
                return taxRate;
            }
            set
            {
                taxRate = value;
                OnPropertyChanged();
            }
        }

        private string warehouseCode;
        public string WarehouseCode
        {
            get
            {
                return warehouseCode;
            }
            set
            {
                warehouseCode = value;
                OnPropertyChanged();
            }
        }

        private string truckingCharge;
        public string TruckingCharge
        {
            get
            {
                return truckingCharge;
            }
            set
            {
                truckingCharge = value;
                OnPropertyChanged();
            }
        }

        private string orderAmount;
        public string OrderAmount
        {
            get
            {
                return orderAmount;
            }
            set
            {
                orderAmount = value;
                OnPropertyChanged();
            }
        }

        private bool isItemSearchVisible = false;
        public bool IsItemSearchVisible
        {
            get { return isItemSearchVisible; }
            set
            {
                isItemSearchVisible = value;
                OnPropertyChanged();
            }
        }


        private DelegateCommand exitCommand;
        public ICommand ExitCommand
        {
            get
            {
                return exitCommand;
            }
        }

        private DelegateCommand listValuesCommand;
        public ICommand ListValuesCommand
        {
            get
            {
                return listValuesCommand;
            }
        }

        private DelegateCommand deleteLineCommand;
        public ICommand DeleteLineCommand
        {
            get
            {
                return deleteLineCommand;
            }
        }

        private DelegateCommand selectItemSearchCommand;
        public ICommand SelectItemSearchCommand
        {
            get
            {
                return selectItemSearchCommand;
            }
        }

        private DelegateCommand addItemCommand;
        public ICommand AddItemCommand
        {
            get
            {
                return addItemCommand;
            }
        }

        #endregion


        private void Load()
        {
            cnHeaderObject = (CreditNoteHeaderModel)cnHeaderObject.Fetch(MyGlobals.SelectedCNDNTransaction, "code");

            var customerObject = new CustomerModel();
            var districtObject = new DistrictModel();
            var salesmanObject = new SalesmanModel();
            var termObject = new TermModel();

            customerObject = (CustomerModel)customerObject.Fetch(cnHeaderObject.CustomerId.ToString(), "id");
            districtObject = (DistrictModel)districtObject.Fetch(customerObject.DistrictId.ToString(), "id");
            salesmanObject = (SalesmanModel)salesmanObject.Fetch(districtObject.Salesman.ToString(), "id");
            termObject = (TermModel)termObject.Fetch(customerObject.TermId.ToString(), "id");

            CustomerNumber = customerObject.CustomerNumber;
            CustomerName = customerObject.CustomerName;
            SalesmanNumber = salesmanObject.SalesmanNumber;
            SalesmanName = salesmanObject.SalesmanName;
            TermCode = termObject.TermCode;
            TermDescription = termObject.TermDescription;
            PriceUsed = MyGlobals.CnDnEntryOptionsVM.SelectedPrice;
            CnNumber = cnHeaderObject.CnNumber;
            TotalCases = cnHeaderObject.TotalCases.ToString();
            TotalPieces = cnHeaderObject.TotalPieces.ToString();
            OrderAmount = cnHeaderObject.CnAmount.ToString();

            var cnLineObject = new CreditNoteLineModel();
            var cnLineList = cnLineObject.FetchPerCreditNoteHead(cnHeaderObject.Id.ToString());

            if (cnLineList.Count > 0)
            {
                foreach (var row in cnLineList)
                {
                    var itemObject = new ItemModel();
                    itemObject = (ItemModel)itemObject.Fetch(row.ItemId.ToString(), "id");

                    var lineItem = new LineItem();
                    lineItem.ItemCode = itemObject.Code;
                    lineItem.PriceType = row.PriceType;
                    lineItem.Description = itemObject.Description;
                    lineItem.Location = row.Location;
                    lineItem.Cases = row.Cases.ToString();
                    lineItem.Pieces = row.Pieces.ToString();
                    lineItem.PricePerPiece = row.PricePerPiece.ToString();
                    lineItem.LineAmount = row.LineAmount.ToString();

                    LinedItemCollection.Add(lineItem);
                }
            }

            var sellingPriceObject = new PriceSellingModel();
            foreach (PriceSellingModel row in sellingPriceObject.FetchAll())
            {
                SellingPriceList.Add(row);
            }

            //Load search items

            SearchItemCollection.Clear();
            var itemModelObject = new ItemModel();
            var itemModelList = itemModelObject.FetchAll();

            foreach (ItemModel item in itemModelList)
            {
                SearchItemCollection.Add(item);
            }

            var priceTypeObject = new PriceTypeModel();
            SelectedPriceType = priceTypeObject.Fetch(cnHeaderObject.PriceTypeId.ToString(), "id") as PriceTypeModel;
            PriceType = SelectedPriceType.Code;

            var returnObject = new ReturnTypeModel();
            SelectedReturnType = returnObject.Fetch(cnHeaderObject.ReturnCodeId.ToString(), "id") as ReturnTypeModel;
            Location = SelectedReturnType.Code;
        }

        private void ItemCodeChanged(string value)
        {
            var matchedItem = new ItemModel();
            foreach (var row in SearchItemCollection)
            {
                if (row.Code == value)
                {
                    matchedItem = row;
                    SelectedItem = row;
                    break;
                }
            }
            if (matchedItem.Code.Length < 6)
            {
                return;
            }

            //var pricetypeObject = new PriceTypeModel();
            //foreach (var row in pricetypeObject.FetchPerItem(matchedItem.ItemId.ToString()))
            //{
            //    PriceTypeList.Add(row);
            //}

            var sellingPriceObject = new PriceSellingModel();
            foreach (var row in sellingPriceObject.FetchCurrentPrice(matchedItem.ItemId.ToString(), "Id"))
            {
                SellingPriceList.Add(row);
            }

            Cases = 0;
            Pieces = 0;

            decimal sellingPrice = 0;
            foreach (var row in SellingPriceList)
            {
                if (row.PriceTypeId == SelectedPriceType.PriceTypeId)
                    sellingPrice = row.SellingPrice;
            }

            PricePerPiece = (sellingPrice / matchedItem.PackSize).ToString();

            TaxRate = matchedItem.Taxrate.ToString();

            var warehouseObject = new WareHouseModel();
            warehouseObject = (WareHouseModel)warehouseObject.Fetch(matchedItem.Warehouse.ToString(), "Id");
        }

        private void SelectItem()
        {
            if (IsItemSearchVisible)
                IsItemSearchVisible = false;
            else
                IsItemSearchVisible = true;

            SelectedItemCode = SelectedSearchItem.Code;
        }

        private void DeleteLineItem()
        {
            throw new NotImplementedException();
        }

        private void ListItems()
        {
            if (IsItemSearchVisible)
                IsItemSearchVisible = false;
            else
                IsItemSearchVisible = true;
        }

        private void AddItem()
        {
            var cnLineObject = new CreditNoteLineModel();

            cnLineObject.CreditNoteHeadId = cnHeaderObject.Id;
            cnLineObject.ItemId = SelectedItem.ItemId;
            cnLineObject.PriceType = PriceType;
            cnLineObject.Cases = Cases;
            cnLineObject.Pieces = Pieces;
            cnLineObject.LineAmount = (((SelectedItem.PackSize * Cases) + Pieces) * decimal.Parse(PricePerPiece));

            cnLineObject.AddNew(cnLineObject);

            if (cnLineObject.Verify(cnHeaderObject.Id.ToString(), SelectedItem.ItemId.ToString()))
            {
                var listItem = new LineItem();
            }

        }

        private void Exit()
        {
            MyGlobals.CnDnVM.SelectedPage = MyGlobals.CreditNoteEntryView;
        }
    }

    public class LineItem
    {
        private string itemCode;
        public string ItemCode
        {
            get { return itemCode; }
            set { itemCode = value; }
        }

        private string priceType;
        public string PriceType
        {
            get { return priceType; }
            set { priceType = value; }
        }

        private string description;
        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        private string location;
        public string Location
        {
            get { return location; }
            set { location = value; }
        }

        private string cases;
        public string Cases
        {
            get { return cases; }
            set { cases = value; }
        }

        private string pieces;
        public string Pieces
        {
            get { return pieces; }
            set { pieces = value; }
        }

        private string pricePerPiece;
        public string PricePerPiece
        {
            get { return pricePerPiece; }
            set { pricePerPiece = value; }
        }

        private string lineAmount;
        public string LineAmount
        {
            get { return lineAmount; }
            set { lineAmount = value; }
        }

    }
}
