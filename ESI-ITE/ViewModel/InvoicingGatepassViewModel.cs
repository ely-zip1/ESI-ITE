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

namespace ESI_ITE.ViewModel
{
    public class InvoicingGatepassViewModel : ViewModelBase, IDataErrorInfo
    {
        public InvoicingGatepassViewModel()
        {

        }

        #region Properties

        private ObservableCollection<List<string>> pickListCollection = new ObservableCollection<List<string>>();
        public ObservableCollection<List<string>> PickListCollection
        {
            get { return pickListCollection; }
            set
            {
                pickListCollection = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<List<string>> invoiceCollection = new ObservableCollection<List<string>>();
        public ObservableCollection<List<string>> InvoiceCollection
        {
            get { return invoiceCollection; }
            set
            {
                invoiceCollection = value;
                OnPropertyChanged();
            }
        }

        private List<string> selectedPickList;
        public List<string> SelectedPickList
        {
            get { return selectedPickList; }
            set
            {
                selectedPickList = value;
                OnPropertyChanged();
            }
        }

        private int selectedIndexPickList;
        public int SelectedIndexPickList
        {
            get { return selectedIndexPickList; }
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
            get { return printGatepassCommand; }
        }

        private DelegateCommand cancelCommand;
        public ICommand CancelCommand
        {
            get { return cancelCommand; }
        }

        #endregion

        #endregion

        private void Load()
        {
            PickListCollection.Clear();

            var pickHeadObj = new PickListHeaderModel();
            var pickHeadList = pickHeadObj.FetchAll();

            var userObj = new UserModel();

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

        private void pickListSelectionChanged()
        {
            if (SelectedIndexPickList > -1)
            {
                FetchInvoices();
            }
        }

        private void FetchInvoices()
        {
            InvoiceCollection.Clear();

            var invoiceObj = new InvoiceModel();
            var pickHeadObj = new PickListHeaderModel();
            var orderObj = new SalesOrderModel();
            var customerObj = new CustomerModel();

            var invoiceList = invoiceObj.FetchPerPickHead(SelectedPickList[0], "code");

            foreach (var invoice in invoiceList)
            {
                orderObj = (SalesOrderModel)orderObj.Fetch(invoice.OrderId.ToString(), "id");
                customerObj = (CustomerModel)customerObj.Fetch(orderObj.CustomerID.ToString(), "id");

                var list = new List<string>();
                list.Add(invoice.InvoiceNumber);
                list.Add(customerObj.CustomerNumber);
                list.Add(customerObj.CustomerName);
                list.Add(invoice.Date.ToString("MM/dd/yyyy"));

                InvoiceCollection.Add(list);
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
