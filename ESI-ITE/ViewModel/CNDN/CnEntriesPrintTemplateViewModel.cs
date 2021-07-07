using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESI_ITE.ViewModel.CNDN
{
    public class CnEntriesPrintTemplateViewModel : ViewModelBase
    {
        private string user;
        public string User
        {
            get { return user; }
            set
            {
                user = value;
                OnPropertyChanged();
            }
        }

        private string printDate;
        public string PrintDate
        {
            get { return printDate; }
            set
            {
                printDate = value;
                OnPropertyChanged();
            }
        }

        private string printTime;
        public string PrintTime
        {
            get { return printTime; }
            set
            {
                printTime = value;
                OnPropertyChanged();
            }
        }

        private int pageNumber;
        public int PageNumber
        {
            get { return pageNumber; }
            set
            {
                pageNumber = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<List<string>> cnEntryCollection = new ObservableCollection<List<string>>();
        public ObservableCollection<List<string>> CnEntryCollection
        {
            get { return cnEntryCollection; }
            set
            {
                cnEntryCollection = value;
                OnPropertyChanged();
            }
        }

    }
}
