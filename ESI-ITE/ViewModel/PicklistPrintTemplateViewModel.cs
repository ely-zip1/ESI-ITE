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
    public class PicklistPrintTemplateViewModel: ViewModelBase
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

        private bool isFooterVisible;
        public bool IsFooterVisible
        {
            get { return isFooterVisible; }
            set
            {
                isFooterVisible = value;
                OnPropertyChanged();
            }
        }


        private ObservableCollection<string> itemList;
        public ObservableCollection<string> ItemList
        {
            get { return itemList; }
            set
            {
                itemList = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<string> ordersList;
        public ObservableCollection<string> OrdersList
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
