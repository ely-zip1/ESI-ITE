using ESI_ITE.Model;
using ESI_ITE.View.PrintingTemplate;
using ESI_ITE.ViewModel.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;

namespace ESI_ITE.ViewModel.CNDN
{
    public class CnDnPrintingOptionsViewModel : ViewModelBase
    {
        public CnDnPrintingOptionsViewModel()
        {
            okCommand = new DelegateCommand(StartPrinting);
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

            StartingNoteLabel = "Starting Credit Note : ";
            EndingNoteLabel = "Ending Credit Note : ";

            var cnHeadObject = new CreditNoteHeaderModel();
            var customerObject = new CustomerModel();

            var cnHeadList = cnHeadObject.FetchAll();
            foreach(CreditNoteHeaderModel row in cnHeadList)
            {
                var customer = (CustomerModel)customerObject.Fetch(row.CustomerId.ToString(), "id");

                var cnList = new List<string>();
                cnList.Add(row.CnNumber);
                cnList.Add(customer.CustomerName);

                StartingNoteCollection.Add(cnList);
                EndingNoteCollection.Add(cnList);
            }
        }


        #region Printing

        private void StartPrinting()
        {
            CallPrinting();
        }

        private async void CallPrinting()
        {
            var result = await PicklistPrintingAsync();

            MyGlobals.printingDoc = result;
            
            MyGlobals.PrintingParent = MyGlobals.cre;
            MyGlobals.CnDnVM.SelectedPage = new View.PrintingTemplate.PrintingMainPageView();
        }

        private Task<FixedDocument> PicklistPrintingAsync()
        {
            return Task.Factory.StartNew(() => CnEntryPrinting());
        }

        private FixedDocument CnEntryPrinting()
        {
            FixedDocument fixedDoc = null;
            Application.Current.Dispatcher.Invoke(() =>
            {
                // Your Code Goes here
                fixedDoc = new FixedDocument();

                var CnObject = new CreditNoteHeaderModel();
                var user = MyGlobals.LoggedUser.Username;
                var printDateTime = DateTime.UtcNow;
                var pageNumber = 1;

                var templateVM = new CnEntriesPrintTemplateViewModel();

                var customerObject = new CustomerModel();

                var cnList = CnObject.FetchAll();

                List<object> newPage = CreateNewPage(pageNumber++);
                fixedDoc.Pages.Add((PageContent)newPage[1]);
                templateVM = (CnEntriesPrintTemplateViewModel)newPage[0];

                var itemcounter = 0;
                foreach (var row in cnList)
                {
                    var cnEntry = row as CreditNoteHeaderModel;
                    customerObject = (CustomerModel)customerObject.Fetch(cnEntry.CustomerId.ToString(), "id");

                    var cnItem = new List<string>();
                    cnItem.Add(customerObject.CustomerName);
                    cnItem.Add(cnEntry.CnNumber);
                    cnItem.Add(cnEntry.CnDate.ToShortDateString());
                    cnItem.Add(cnEntry.ReferenceNumber);
                    cnItem.Add(cnEntry.CnAmount.ToString());
                    cnItem.Add(cnEntry.IsPrinted ? "Yes" : "No");

                    templateVM.CnEntryCollection.Add(cnItem);

                    if (customerObject.CustomerName.Length > 34)
                        itemcounter += 2;
                    else
                        itemcounter
                            ++;

                    if (itemcounter >= 40)
                    {
                        List<object> newPage2 = CreateNewPage(pageNumber++);
                        fixedDoc.Pages.Add((PageContent)newPage2[1]);
                        templateVM = (CnEntriesPrintTemplateViewModel)newPage2[0];

                        itemcounter = 0;
                    }
                }
            });
            return fixedDoc;
        }

        private List<object> CreateNewPage(int pageNumber)
        {
            var fixedPage = new FixedPage();
            var grid = new Grid();
            var templateView = new CreditNotePrintTemplate();
            var templateVM = (CnEntriesPrintTemplateViewModel)templateView.DataContext;

            templateView.Width = 1100;
            templateView.MinHeight = 100;

            fixedPage.Width = 1100;
            fixedPage.Height = 850;

            templateVM.User = MyGlobals.LoggedUser.Username;
            templateVM.PrintDate = DateTime.Now.ToShortDateString();
            templateVM.PrintTime = DateTime.Now.ToLongTimeString();
            templateVM.PageNumber = pageNumber;

            grid.Children.Add(templateView);
            fixedPage.Children.Add(grid);

            var pageContent = new PageContent();
            pageContent.Child = fixedPage;

            var newPage = new List<object>();
            newPage.Add(templateVM);
            newPage.Add(pageContent);

            return newPage;
        }

        #endregion
    }
}
