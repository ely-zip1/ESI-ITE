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
                CreditNoteSelected();
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
                DebitNoteSelected();
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

        private List<string> selectedStartingNote;
        public List<string> SelectedStartingNote
        {
            get { return selectedStartingNote; }
            set
            {
                selectedStartingNote = value;
                OnPropertyChanged();
            }
        }

        private List<string> selectedEndingNote;
        public List<string> SelectedEndingNote
        {
            get { return selectedEndingNote; }
            set
            {
                selectedEndingNote = value;
                OnPropertyChanged();
            }
        }

        private int selectedIndexStartingNote;
        public int SelectedIndexStartingNote
        {
            get { return selectedIndexStartingNote; }
            set
            {
                selectedIndexStartingNote = value;
                OnPropertyChanged();
                StartingNoteChanged();
            }
        }

        private int selectedIndexEndingNote;
        public int SelectedIndexEndingNote
        {
            get { return selectedIndexEndingNote; }
            set
            {
                selectedIndexEndingNote = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<List<string>> PrintNoteCollection = new ObservableCollection<List<string>>();


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
            StartingNoteCollection.Add(new List<string>());

            foreach (CreditNoteHeaderModel row in cnHeadList)
            {
                var customer = (CustomerModel)customerObject.Fetch(row.CustomerId.ToString(), "id");

                var cnList = new List<string>();
                cnList.Add(row.CnNumber);
                cnList.Add(customer.CustomerName);

                StartingNoteCollection.Add(cnList);
            }

            SelectedIndexStartingNote = 0;
        }


        private void DebitNoteSelected()
        {
            if (IsDebitNoteSelected)
            {
                IsCreditNoteSelected = false;
            }
        }

        private void CreditNoteSelected()
        {
            if (IsCreditNoteSelected)
            {
                IsDebitNoteSelected = false;
            }
        }

        private void StartingNoteChanged()
        {
            EndingNoteCollection.Clear();

            var index = SelectedIndexStartingNote;
            if (IsCreditNoteSelected)
            {
                while (true)
                {
                    try
                    {
                        EndingNoteCollection.Add(StartingNoteCollection[index]);
                        index++;
                    }
                    catch (Exception e)
                    {
                        break;
                    }
                }
            }
        }

        private void CreatePrintNoteCollection()
        {
            var index = 0;
            foreach (var row in EndingNoteCollection)
            {
                if (index == SelectedIndexEndingNote)
                {
                    PrintNoteCollection.Add(row);
                    break;
                }
                else
                {
                    PrintNoteCollection.Add(row);
                    index++;
                }
            }
        }

        #region Printing

        private void StartPrinting()
        {
            if (SelectedIndexStartingNote > 0 && SelectedIndexEndingNote > 0)
            {
                CallPrinting();
            }
        }

        private async void CallPrinting()
        {
            var result = await PicklistPrintingAsync();

            MyGlobals.printingDoc = result;

            MyGlobals.PrintingParent = MyGlobals.CnDnPrintingOptionsView;
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

                if (IsCreditNoteSelected)
                {
                    CreatePrintNoteCollection();

                    var CnObject = new CreditNoteHeaderModel();
                    var user = MyGlobals.LoggedUser.Username;
                    var printDateTime = DateTime.UtcNow;

                    var templateVM = new CreditNotePrintTemplateViewModel();

                    var customerObject = new CustomerModel();

                    var cnList = CnObject.FetchAll();
                    var startingCN = (CreditNoteHeaderModel)CnObject.Fetch(SelectedStartingNote[0], "code");

                    var itemcounter = 0;
                    foreach (var row in PrintNoteCollection)
                    {
                        var pageNumber = 1;
                        List<object> newPage = CreateNewPage(pageNumber, startingCN);
                        fixedDoc.Pages.Add((PageContent)newPage[1]);
                        templateVM = (CreditNotePrintTemplateViewModel)newPage[0];

                        var cnEntry = (CreditNoteHeaderModel)CnObject.Fetch(row[0], "code");
                        customerObject = (CustomerModel)customerObject.Fetch(cnEntry.CustomerId.ToString(), "id");

                        var cnLineObject = new CreditNoteLineModel();
                        var cnLine = cnLineObject.FetchPerCreditNoteHead(cnEntry.Id.ToString());

                        foreach (var lineItem in cnLine)
                        {
                            var printItem = new CnItem();
                            var itemMasterObject = new ItemModel();
                            var currentItem = (ItemModel)itemMasterObject.Fetch(lineItem.ItemId.ToString(), "id");
                            var pricetypeObject = new PriceTypeModel();
                            var currentPricetype = (PriceTypeModel)pricetypeObject.Fetch(cnEntry.PriceTypeId.ToString(), "id");
                            var purchasePriceObject = new PricePurchaseModel();
                            var purchasePrice = purchasePriceObject.FetchCurrentPrice(lineItem.ItemId.ToString(), "id");


                            printItem.ItemDescription = currentItem.Description;
                            printItem.ItemNumber = currentItem.Code;
                            printItem.PriceType = lineItem.PriceType;
                            printItem.Cases = lineItem.Cases.ToString();
                            printItem.Pieces = lineItem.Pieces.ToString();
                            printItem.UnitPrice = purchasePrice.PurchasePrice.ToString();
                            printItem.PricePerPiece = lineItem.PricePerPiece.ToString();
                            printItem.Amount = lineItem.LineAmount.ToString();

                            templateVM.CnItemCollection.Add(printItem);
                            itemcounter++;


                            if (itemcounter >= 40)
                            {
                                List<object> newPage2 = CreateNewPage(pageNumber++, startingCN);
                                fixedDoc.Pages.Add((PageContent)newPage2[1]);
                                templateVM = (CreditNotePrintTemplateViewModel)newPage2[0];

                                itemcounter = 0;
                            }
                        }
                    }
                }
            });
            return fixedDoc;
        }

        private List<object> CreateNewPage(int pageNumber, CreditNoteHeaderModel cnHead)
        {
            var fixedPage = new FixedPage();
            var grid = new Grid();
            var templateView = new CreditNotePrintTemplate();
            var templateVM = (CreditNotePrintTemplateViewModel)templateView.DataContext;

            templateView.Width = 1100;
            templateView.MinHeight = 100;

            fixedPage.Width = 1100;
            fixedPage.Height = 850;

            templateVM.User = MyGlobals.LoggedUser.Username;
            templateVM.PrintDate = DateTime.Now.ToShortDateString();
            templateVM.PrintTime = DateTime.Now.ToLongTimeString();
            templateVM.PageNumber = pageNumber.ToString();

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
