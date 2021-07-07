using ESI_ITE.Model;
using ESI_ITE.View.PrintingTemplate;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ESI_ITE.ViewModel
{
    public class PicklistPrintTemplateViewModel : ViewModelBase
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

        private string date;
        public string Date
        {
            get { return date; }
            set
            {
                date = value;
                OnPropertyChanged();
            }
        }

        private string time;
        public string Time
        {
            get { return time; }
            set
            {
                time = value;
                OnPropertyChanged();
            }
        }

        private string picklistNumber;
        public string PicklistNumber
        {
            get { return picklistNumber; }
            set
            {
                picklistNumber = value;
                OnPropertyChanged();
            }
        }

        private string pageNumber;
        public string PageNumber
        {
            get { return pageNumber; }
            set
            {
                pageNumber = value;
                OnPropertyChanged();
            }
        }

        private string totalOrderedCases;
        public string TotalOrderedCases
        {
            get { return totalOrderedCases; }
            set
            {
                totalOrderedCases = value;
                OnPropertyChanged();
            }
        }

        private string totalOrderedPieces;
        public string TotalOrderedPieces
        {
            get { return totalOrderedPieces; }
            set
            {
                totalOrderedPieces = value;
                OnPropertyChanged();
            }
        }

        private string totalAllocatedCases;
        public string TotalAllocatedCases
        {
            get { return totalAllocatedCases; }
            set
            {
                totalAllocatedCases = value;
                OnPropertyChanged();
            }
        }

        private string totalAllocatedPieces;
        public string TotalAllocatedPieces
        {
            get { return totalAllocatedPieces; }
            set
            {
                totalAllocatedPieces = value;
                OnPropertyChanged();
            }
        }

        private bool isTotalVisible = false;
        public bool IsTotalVisible
        {
            get { return isTotalVisible; }
            set
            {
                isTotalVisible = value;
                OnPropertyChanged();
            }
        }

        private bool isOrderVisible = false;
        public bool IsOrderVisible
        {
            get { return isOrderVisible; }
            set
            {
                isOrderVisible = value;
                OnPropertyChanged();
            }
        }


        private ObservableCollection<List<string>> itemList = new ObservableCollection<List<string>>();
        public ObservableCollection<List<string>> ItemList
        {
            get { return itemList; }
            set
            {
                itemList = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<List<string>> ordersList = new ObservableCollection<List<string>>();
        public ObservableCollection<List<string>> OrdersList
        {
            get { return ordersList; }
            set
            {
                ordersList = value;
                OnPropertyChanged();
            }
        }


    }
}
