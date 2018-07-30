using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ESI_ITE.View.CNDN;
using ESI_ITE.ViewModel.Command;

namespace ESI_ITE.ViewModel.CNDN
{
    public class CnDnEntryOptionsViewModel : ViewModelBase
    {
        public CnDnEntryOptionsViewModel()
        {
            MyGlobals.CnDnEntryOptionsVM = this;

            okCommand = new DelegateCommand(SelectTransaction);

            Load();
        }
        #region Properties

        private bool creditSelected;
        public bool CreditSelected
        {
            get
            {
                return creditSelected;
            }
            set
            {
                creditSelected = value;
                if (value)
                {
                    SelectedTransaction = "Credit";
                }
                OnPropertyChanged();
            }
        }

        private bool debitSelected;
        public bool DebitSelected
        {
            get
            {
                return debitSelected;
            }
            set
            {
                debitSelected = value;
                if (value)
                {
                    SelectedTransaction = "Debit";
                }
                OnPropertyChanged();
            }
        }

        private bool isCurrentPrice;
        public bool IsCurrentPrice
        {
            get
            {
                return isCurrentPrice;
            }
            set
            {

                isCurrentPrice = value;
                if (value)
                {
                    SelectedPrice = "Current";
                }
                OnPropertyChanged();
            }
        }

        private bool is3MonthsAgo;
        public bool Is3MonthsAgo
        {
            get
            {
                return is3MonthsAgo;
            }
            set
            {

                is3MonthsAgo = value;
                if (value)
                {
                    SelectedPrice = "3 Months Ago";
                }
                OnPropertyChanged();
            }
        }

        private bool is6MonthsAgo;
        public bool Is6MonthsAgo
        {
            get
            {
                return is6MonthsAgo;
            }
            set
            {

                is6MonthsAgo = value;
                if (value)
                {
                    SelectedPrice = "6 Months Ago";
                }
                OnPropertyChanged();
            }
        }

        private string selectedTransaction;
        public string SelectedTransaction
        {
            get
            {
                return selectedTransaction;
            }
            set
            {
                selectedTransaction = value;
            }
        }

        private string selectedPrice;
        public string SelectedPrice
        {
            get
            {
                return selectedPrice;
            }
            set
            {
                selectedPrice = value;
            }
        }

        private DelegateCommand okCommand;
        public ICommand OkCommand
        {
            get
            {
                return okCommand;
            }
        }
        #endregion



        private void Load()
        {
            CreditSelected = true;
            IsCurrentPrice = true;
        }

        private void SelectTransaction()
        {
            if (SelectedPrice == "Current")
            {
                MyGlobals.SelectedCNDNPrice = "Current";
            }
            else if (SelectedPrice == "3 Months Ago")
            {
                MyGlobals.SelectedCNDNPrice = "3 Months Ago";
            }
            else if (SelectedPrice == "6 Months Ago")
            {
                MyGlobals.SelectedCNDNPrice = "6 Months Ago";
            }


            if (SelectedTransaction == "Credit")
            {
                MyGlobals.SelectedCNDNTransaction = "Credit";
                MyGlobals.CnDnVM.SelectedPage = new CreditNoteEntryView();
            }
            else if (SelectedTransaction == "Debit")
            {
                MyGlobals.SelectedCNDNTransaction = "Debit";
                MyGlobals.CnDnVM.SelectedPage = new DebitNoteEntryView();
            }
        }

    }
}
