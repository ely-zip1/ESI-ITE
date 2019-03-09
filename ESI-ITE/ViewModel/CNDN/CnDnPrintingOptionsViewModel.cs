using ESI_ITE.ViewModel.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ESI_ITE.ViewModel.CNDN
{
    public class CnDnPrintingOptionsViewModel : ViewModelBase
    {
        public CnDnPrintingOptionsViewModel()
        {
            Load();
        }

        private bool isCreditNoteSelected;
        public bool IsCreditNoteSelected
        {
            get { return isCreditNoteSelected; }
            set
            {
                isCreditNoteSelected = value;
                OnPropertyChanged();
            }
        }

        private bool isDebitNoteSelected;
        public bool IsDebitNoteSelected
        {
            get { return isDebitNoteSelected; }
            set
            {
                isDebitNoteSelected = value;
                OnPropertyChanged();
            }
        }

        private string startingNoteLabel;
        public string StartingNoteLabel
        {
            get { return startingNoteLabel; }
            set
            {
                startingNoteLabel = value;
                OnPropertyChanged();
            }
        }

        private string endingNoteLabel;
        public string EndingNoteLabel
        {
            get { return endingNoteLabel; }
            set
            {
                endingNoteLabel = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<List<string>> startingNoteCollection = new ObservableCollection<List<string>>();
        public ObservableCollection<List<string>> StartingNoteCollection
        {
            get { return startingNoteCollection; }
            set
            {
                startingNoteCollection = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<List<string>> endingNoteCollection = new ObservableCollection<List<string>>();
        public ObservableCollection<List<string>> EndingNoteCollection
        {
            get { return endingNoteCollection; }
            set
            {
                endingNoteCollection = value;
                OnPropertyChanged();
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

        private DelegateCommand exitCommand;
        public ICommand ExitCommand
        {
            get
            {
                return exitCommand;
            }
        }

        
        private void Load()
        {
            IsCreditNoteSelected = true;
            IsDebitNoteSelected = false;

            StartingNoteLabel = "Starting Credit Note:";
            EndingNoteLabel = "Ending Credit Note:";


        }
    }
}
