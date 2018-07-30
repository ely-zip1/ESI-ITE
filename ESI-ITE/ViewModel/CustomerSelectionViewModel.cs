using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ESI_ITE.Model;
using ESI_ITE.ViewModel.Command;

namespace ESI_ITE.ViewModel
{
    public class CustomerSelectionViewModel : ViewModelBase
    {
        public CustomerSelectionViewModel()
        {
            loadCustomerCommand = new DelegateCommand(LoadCustomers);
            toggleSearchVisibilityCommand = new DelegateCommand(CloseSearch);

            Load();
        }

        private ObservableCollection<List<string>> customerList = new ObservableCollection<List<string>>();
        public ObservableCollection<List<string>> CustomerList
        {
            get
            {
                return customerList;
            }
            set
            {
                customerList = value;
                OnPropertyChanged();
            }
        }

        private List<string> selectedSearchedCustomer;
        public List<string> SelectedSearchedCustomer
        {
            get
            {
                return selectedSearchedCustomer;
            }
            set
            {
                selectedSearchedCustomer = value;
                OnPropertyChanged();
            }
        }

        private int selectedIndexSearchedCustomer;
        public int SelectedIndexSearchedCustomer
        {
            get
            {
                return selectedIndexSearchedCustomer;
            }
            set
            {
                selectedIndexSearchedCustomer = value;
                OnPropertyChanged();
            }
        }


        private List<string> selectedCustomer;
        public List<string> SelectedCustomer
        {
            get
            {
                return selectedCustomer;
            }
            set
            {
                selectedCustomer = value;
                OnPropertyChanged();
            }
        }

        private int selectedIndexCustomer;
        public int SelectedIndexCustomer
        {
            get
            {
                return selectedIndexCustomer;
            }
            set
            {
                selectedIndexCustomer = value;
                OnPropertyChanged();
            }
        }

        private string searchedCustomer;
        public string SearchedCustomer
        {
            get
            {
                return searchedCustomer;
            }
            set
            {
                searchedCustomer = value;
                OnPropertyChanged();
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

        private DelegateCommand toggleSearchVisibilityCommand;
        public ICommand ToggleSearchVisibilityCommand
        {
            get
            {
                return toggleSearchVisibilityCommand;
            }
        }



        private void CloseSearch()
        {
            MyGlobals.MainWindow.IsChildWindowOpen = false;
        }

        private void LoadCustomers()
        {
            if (SelectedSearchedCustomer != null)
            {
                MyGlobals.CreditNoteEntryVM.SelectedCustomer = SelectedSearchedCustomer;
                CloseSearch();
            }
        }


        private void Load()
        {
            var customerObject = new CustomerModel();
            var tempCustomerList = customerObject.FetchAll();

            foreach (CustomerModel row in tempCustomerList)
            {
                var customerItem = new List<string>();
                customerItem.Add(row.CustomerNumber);
                customerItem.Add(row.CustomerName);

                CustomerList.Add(customerItem);
            }
        }
    }
}
