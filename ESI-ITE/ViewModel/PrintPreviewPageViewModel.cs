using ESI_ITE.Model;
using ESI_ITE.ViewModel.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Printing;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Xps;

namespace ESI_ITE.ViewModel {
    public class PrintPreviewPageViewModel: ViewModelBase {

        public PrintPreviewPageViewModel( )
        {
            printPreviewCommand = new DelegateCommand(PrintPreview);
            cancelCommand = new DelegateCommand(CancelPrinting);

            Load();
        }

        #region Properties

        #region Header
        TextBlock txtUser = new TextBlock();
        TextBlock txtPrintDate = new TextBlock();
        TextBlock txtPrintTime = new TextBlock();
        Label lblCompanyName = new Label();
        Label lblModuleName = new Label();
        Label lblPage = new Label();
        TextBlock txtPageNumber = new TextBlock();

        Label lblTransactionType = new Label();
        Label lblTransactionNumber = new Label();
        Label lblTransactionDate = new Label();
        Label lblDocumentNumber = new Label();
        Label lblReason = new Label();
        Label lblWarehouse = new Label();
        Label lblLocation = new Label();
        Label lblSalesman = new Label();
        Label lblComment = new Label();

        TextBlock txtTransactionType = new TextBlock();
        TextBlock txtTransactionNumber = new TextBlock();
        TextBlock txtTransactionDate = new TextBlock();
        TextBlock txtDocumentNumber = new TextBlock();
        TextBlock txtReason = new TextBlock();
        TextBlock txtWarehouse = new TextBlock();
        TextBlock txtLocation = new TextBlock();
        TextBlock txtSalesman = new TextBlock();
        TextBlock txtComment = new TextBlock();

        #endregion

        #region Item Column Headers

        Label lblHorizontalLine_1 = new Label();
        Label lblHorizontalLine_2 = new Label();
        Label lblItemCode = new Label();
        Label lblItemDescription = new Label();
        Label lblLC = new Label();
        Label lblCases = new Label();
        Label lblPieces = new Label();
        Label lblExpiry = new Label();
        Label lblUnitPrice = new Label();
        Label lblValue = new Label();

        #endregion

        #region Item

        TextBlock txtItemCode = new TextBlock();
        TextBlock txtItemDescription = new TextBlock();
        TextBlock txtLC = new TextBlock();
        TextBlock txtCases = new TextBlock();
        TextBlock txtPieces = new TextBlock();
        TextBlock txtExpiry = new TextBlock();
        TextBlock txtUnitPrice = new TextBlock();
        TextBlock txtValue = new TextBlock();

        #endregion

        #region Footer

        TextBlock txtTotalCases = new TextBlock();
        TextBlock txtTotalPieces = new TextBlock();
        TextBlock txtOrderAmount = new TextBlock();

        Label lblPreparedBy = new Label();
        Label lblCheckedBy = new Label();
        Label lblReceivedBy = new Label();

        #endregion

        private FixedDocumentSequence docSequence = new FixedDocumentSequence();
        public FixedDocumentSequence DocSequence {
            get { return docSequence; }
            set {
                docSequence = value;
                OnPropertyChanged("DocSequence");
            }
        }

        private ObservableCollection<TransactionModel> printableTransactions = new ObservableCollection<TransactionModel>();
        public ObservableCollection<TransactionModel> PrintableTransactions {
            get { return printableTransactions; }
            set { printableTransactions = value; }
        }

        private ObservableCollection<CheckedItem> checkBoxCollection = new ObservableCollection<CheckedItem>();
        public ObservableCollection<CheckedItem> CheckBoxCollection {
            get { return checkBoxCollection; }
            set {
                checkBoxCollection = value;
            }
        }

        private bool selectAll;
        public bool SelectAll {
            get { return selectAll; }
            set {
                selectAll = value;
                OnPropertyChanged("SelectAll");
                SelectAllChanged();
            }
        }

        private bool isViewerVisible = false;
        public bool IsViewerVisible {
            get { return isViewerVisible; }
            set {
                isViewerVisible = value;
                OnPropertyChanged("IsViewerVisible");
            }
        }

        private DelegateCommand printPreviewCommand;
        public ICommand PrintPreviewCommand {
            get { return printPreviewCommand; }
        }

        private DelegateCommand cancelCommand;
        public ICommand CancelCommand {
            get { return cancelCommand; }
        }

        DocumentReference docRef = new DocumentReference();
        FixedDocument doc = new FixedDocument();
        InventoryDummyModel dummy = new InventoryDummyModel();
        FixedPage[] page;
        PageContent[] pageContent;
        Canvas[] content;

        int pageNumber = 0;

        decimal totalCases = 0;
        decimal totalPieces = 0;
        decimal orderAmount = 0;

        int top = 30;
        int left = 30;

        private bool createNewPage = false;

        private bool isSelectAll = false;
        private bool isItemUnchecked = false;
        private bool isItemChecked = false;
        private bool uncheckAll = false;

        #endregion

        private void Load( )
        {
            foreach ( var trans in MyGlobals.TransactionList ) {
                PrintableTransactions.Add(trans);
                CheckBoxCollection.Add(new CheckedItem { IsChecked = false, Transaction = new TransactionModel(trans) });
            }
        }

        private void SelectAllChanged( )
        {
            if ( SelectAll ) {
                isSelectAll = true;
                foreach ( var item in CheckBoxCollection ) {
                    item.IsChecked = true;
                }
                isSelectAll = false;
            }
            else {
                if ( isItemUnchecked == false ) {
                    if ( isItemChecked == false ) {
                        uncheckAll = true;

                        foreach ( var item in CheckBoxCollection ) {
                            item.IsChecked = false;
                        }

                        uncheckAll = false;
                    }
                }
            }
        }

        public void ItemUnchecked( )
        {
            if ( uncheckAll == false ) {
                isItemUnchecked = true;

                SelectAll = false;

                isItemUnchecked = false;
            }
        }

        public void ItemChecked( )
        {
            if ( isSelectAll == false ) {
                isItemChecked = true;
                int itemCount = CheckBoxCollection.Count;
                int checkedItemsCounter = 0;

                foreach ( var i in CheckBoxCollection ) {
                    if ( i.IsChecked )
                        checkedItemsCounter++;
                }

                if ( checkedItemsCounter == itemCount )
                    SelectAll = true;
                else
                    SelectAll = false;

                isItemChecked = false;
            }
        }

        private void PrintPreview( )
        {
            PrintableTransactions.Clear();
            DocSequence = null;
            DocSequence = new FixedDocumentSequence();

            foreach ( var chkdItems in CheckBoxCollection ) {
                if ( chkdItems.IsChecked ) {
                    PrintableTransactions.Add(chkdItems.Transaction);
                }
            }

            IsViewerVisible = true;

            StartPrinting();
        }

        private void CancelPrinting( )
        {
            MyGlobals.IteViewModel.SelectedPage = MyGlobals.PrintingParent;
        }

        private void StartPrinting( )
        {
            foreach ( var transaction in PrintableTransactions ) {
                List<InventoryDummyModel> items = dummy.FetchAll(transaction.TransactionNumber);

                doc = new FixedDocument();

                double x = (double)items.Count / 20.0;
                int numberOfPages = x > 1 ? (int)(x + 1) : 1;

                if ( numberOfPages >= 1 ) {
                    page = new FixedPage[numberOfPages];
                    pageContent = new PageContent[numberOfPages];
                    content = new Canvas[numberOfPages];
                }
                else {
                    page = new FixedPage[1];
                    pageContent = new PageContent[1];
                    content = new Canvas[1];
                }

                ResetProperties();

                page[pageNumber] = new FixedPage();
                pageContent[pageNumber] = new PageContent();
                ((IAddChild)pageContent[pageNumber]).AddChild(page[pageNumber]);
                doc.Pages.Add(pageContent[pageNumber]);

                content[pageNumber] = new Canvas();
                page[pageNumber].Children.Add(content[pageNumber]);

                AppendHeader(content[pageNumber], transaction);

                foreach ( var item in items ) {
                    if ( createNewPage ) {
                        CreateNewPage(transaction);
                    }

                    totalCases += item.Cases;
                    totalPieces += item.Pieces;
                    orderAmount += item.LineAmount;

                    AppendItem(content[pageNumber], item);

                }
                AppendFooter(content[pageNumber], transaction);

                docRef = new DocumentReference();
                docRef.SetDocument(doc);
                DocSequence.References.Add(docRef);
            }
        }

        private void ResetProperties( )
        {
            pageNumber = 0;
            top = 30;
            totalCases = 0;
            totalPieces = 0;
            orderAmount = 0;
        }

        private void CreateNewPage( TransactionModel transaction )
        {
            pageNumber++;

            page[pageNumber] = new FixedPage();
            pageContent[pageNumber] = new PageContent();
            ((IAddChild)pageContent[pageNumber]).AddChild(page[pageNumber]);
            doc.Pages.Add(pageContent[pageNumber]);

            content[pageNumber] = new Canvas();
            page[pageNumber].Children.Add(content[pageNumber]);

            top = 30;

            AppendHeader(content[pageNumber], transaction);

            createNewPage = false;
        }

        private void AppendHeader( Canvas content, TransactionModel trans )
        {
            txtUser = new TextBlock();
            txtPrintDate = new TextBlock();
            txtPrintTime = new TextBlock();
            lblCompanyName = new Label();
            lblModuleName = new Label();
            lblPage = new Label();
            txtPageNumber = new TextBlock();

            lblTransactionType = new Label();
            lblTransactionNumber = new Label();
            lblTransactionDate = new Label();
            lblDocumentNumber = new Label();
            lblReason = new Label();
            lblWarehouse = new Label();
            lblLocation = new Label();
            lblSalesman = new Label();
            lblComment = new Label();

            txtTransactionType = new TextBlock();
            txtTransactionNumber = new TextBlock();
            txtTransactionDate = new TextBlock();
            txtDocumentNumber = new TextBlock();
            txtReason = new TextBlock();
            txtWarehouse = new TextBlock();
            txtLocation = new TextBlock();
            txtSalesman = new TextBlock();
            txtComment = new TextBlock();

            lblHorizontalLine_1 = new Label();
            lblHorizontalLine_2 = new Label();
            lblItemCode = new Label();
            lblItemDescription = new Label();
            lblLC = new Label();
            lblCases = new Label();
            lblPieces = new Label();
            lblExpiry = new Label();
            lblUnitPrice = new Label();
            lblValue = new Label();

            #region line 1 : USER
            //User
            txtUser.Text = "ELI";
            txtUser.FontSize = 12;
            txtUser.FontFamily = new System.Windows.Media.FontFamily("consolas");
            txtUser.Width = 100;
            Canvas.SetLeft(txtUser, left);
            Canvas.SetTop(txtUser, top);
            content.Children.Add(txtUser);
            #endregion
            top += 20;
            #region line 2 : PRINT DATE, COMPANY NAME, PAGE NUMBER
            //Print date
            txtPrintDate.Text = DateTime.Now.ToString("MM/dd/yyyy");
            txtPrintDate.FontSize = 12;
            txtPrintDate.FontFamily = new System.Windows.Media.FontFamily("consolas");
            txtPrintDate.Width = 100;
            Canvas.SetLeft(txtPrintDate, left);
            Canvas.SetTop(txtPrintDate, top);
            content.Children.Add(txtPrintDate);


            //Company name
            lblCompanyName.Content = "Extract Sales, Inc.";
            lblCompanyName.FontSize = 12;
            lblCompanyName.FontFamily = new System.Windows.Media.FontFamily("consolas");
            lblCompanyName.Width = 200;
            lblCompanyName.Padding = new System.Windows.Thickness(0);
            Canvas.SetLeft(lblCompanyName, 350);
            Canvas.SetTop(lblCompanyName, top);
            content.Children.Add(lblCompanyName);

            //Page label
            lblPage.Content = "Page";
            lblPage.FontSize = 12;
            lblPage.FontFamily = new System.Windows.Media.FontFamily("consolas");
            lblPage.Width = 45;
            lblPage.Padding = new System.Windows.Thickness(0);
            lblPage.BorderThickness = new System.Windows.Thickness(1);
            Canvas.SetLeft(lblPage, 700);
            Canvas.SetTop(lblPage, top);
            content.Children.Add(lblPage);

            //Page number
            txtPageNumber.Text = (pageNumber + 1).ToString();
            txtPageNumber.FontSize = 12;
            txtPageNumber.FontFamily = new System.Windows.Media.FontFamily("consolas");
            txtPageNumber.Width = 50;
            Canvas.SetLeft(txtPageNumber, 755);
            Canvas.SetTop(txtPageNumber, top);
            content.Children.Add(txtPageNumber);

            #endregion
            top += 20;
            #region line 3 : PRINT TIME, MODULE NAME

            //Print time
            txtPrintTime.Text = DateTime.Now.ToString("H:mm:ss");
            txtPrintTime.FontSize = 12;
            txtPrintTime.FontFamily = new System.Windows.Media.FontFamily("consolas");
            txtPrintTime.Width = 150;
            Canvas.SetLeft(txtPrintTime, left);
            Canvas.SetTop(txtPrintTime, top);
            content.Children.Add(txtPrintTime);


            //Module name
            lblModuleName.Content = "Inventory Transaction";
            lblModuleName.FontSize = 12;
            lblModuleName.FontFamily = new System.Windows.Media.FontFamily("consolas");
            lblModuleName.Padding = new System.Windows.Thickness(0);
            Canvas.SetLeft(lblModuleName, 340);
            Canvas.SetTop(lblModuleName, top);
            content.Children.Add(lblModuleName);

            #endregion
            top += 40;
            #region line 4 : TRANSACTION TYPE, WAREHOUSE

            //transaction type
            lblTransactionType.Content = "Transaction Type : ";
            lblTransactionType.FontSize = 12;
            lblTransactionType.FontFamily = new System.Windows.Media.FontFamily("consolas");
            lblTransactionType.Padding = new System.Windows.Thickness(0);
            Canvas.SetLeft(lblTransactionType, left);
            Canvas.SetTop(lblTransactionType, top);
            content.Children.Add(lblTransactionType);

            txtTransactionType.Text = trans.TransactionType.ToUpper();
            txtTransactionType.FontSize = 12;
            txtTransactionType.FontFamily = new System.Windows.Media.FontFamily("consolas");
            txtTransactionType.Width = 150;
            Canvas.SetLeft(txtTransactionType, 170);
            Canvas.SetTop(txtTransactionType, top);
            content.Children.Add(txtTransactionType);

            //warehouse
            lblWarehouse.Content = "Source Warehouse : ";
            lblWarehouse.FontSize = 12;
            lblWarehouse.FontFamily = new System.Windows.Media.FontFamily("consolas");
            lblWarehouse.Padding = new System.Windows.Thickness(0);
            Canvas.SetLeft(lblWarehouse, 450);
            Canvas.SetTop(lblWarehouse, top);
            content.Children.Add(lblWarehouse);

            txtWarehouse.Text = trans.SourceWarehouseCode.ToUpper();
            txtWarehouse.FontSize = 12;
            txtWarehouse.FontFamily = new System.Windows.Media.FontFamily("consolas");
            txtWarehouse.Width = 150;
            Canvas.SetLeft(txtWarehouse, 580);
            Canvas.SetTop(txtWarehouse, top);
            content.Children.Add(txtWarehouse);

            #endregion
            top += 20;
            #region line 5 : TRANSACTION NUMBER, LOCATION

            //transaction number
            lblTransactionNumber.Content = "Transaction No. : ";
            lblTransactionNumber.FontSize = 12;
            lblTransactionNumber.FontFamily = new System.Windows.Media.FontFamily("consolas");
            lblTransactionNumber.Padding = new System.Windows.Thickness(0);
            Canvas.SetLeft(lblTransactionNumber, left);
            Canvas.SetTop(lblTransactionNumber, top);
            content.Children.Add(lblTransactionNumber);

            txtTransactionNumber.Text = trans.TransactionNumber;
            txtTransactionNumber.FontSize = 12;
            txtTransactionNumber.FontFamily = new System.Windows.Media.FontFamily("consolas");
            txtTransactionNumber.Width = 150;
            Canvas.SetLeft(txtTransactionNumber, 170);
            Canvas.SetTop(txtTransactionNumber, top);
            content.Children.Add(txtTransactionNumber);

            //location
            lblLocation.Content = "Source Location : ";
            lblLocation.FontSize = 12;
            lblLocation.FontFamily = new System.Windows.Media.FontFamily("consolas");
            lblLocation.Padding = new System.Windows.Thickness(0);
            Canvas.SetLeft(lblLocation, 450);
            Canvas.SetTop(lblLocation, top);
            content.Children.Add(lblLocation);

            txtLocation.Text = trans.SourceLocationCode;
            txtLocation.FontSize = 12;
            txtLocation.FontFamily = new System.Windows.Media.FontFamily("consolas");
            txtLocation.Width = 150;
            Canvas.SetLeft(txtLocation, 580);
            Canvas.SetTop(txtLocation, top);
            content.Children.Add(txtLocation);

            #endregion
            top += 20;
            #region line 6 : TRANSACTION DATE, SALESMAN

            //transaction date
            lblTransactionDate.Content = "Transaction Date : ";
            lblTransactionDate.FontSize = 12;
            lblTransactionDate.FontFamily = new System.Windows.Media.FontFamily("consolas");
            lblTransactionDate.Padding = new System.Windows.Thickness(0);
            Canvas.SetLeft(lblTransactionDate, left);
            Canvas.SetTop(lblTransactionDate, top);
            content.Children.Add(lblTransactionDate);

            txtTransactionDate.Text = trans.TransactionDate.ToString("MM/dd/yyyy");
            txtTransactionDate.FontSize = 12;
            txtTransactionDate.FontFamily = new System.Windows.Media.FontFamily("consolas");
            txtTransactionDate.Width = 150;
            Canvas.SetLeft(txtTransactionDate, 170);
            Canvas.SetTop(txtTransactionDate, top);
            content.Children.Add(txtTransactionDate);

            //salesman
            lblSalesman.Content = "Salesman : ";
            lblSalesman.FontSize = 12;
            lblSalesman.FontFamily = new System.Windows.Media.FontFamily("consolas");
            lblSalesman.Padding = new System.Windows.Thickness(0);
            Canvas.SetLeft(lblSalesman, 450);
            Canvas.SetTop(lblSalesman, top);
            content.Children.Add(lblSalesman);
            #endregion
            top += 20;
            #region line 7 : DOCUMENT NUMBER, COMMENT

            //document number
            lblDocumentNumber.Content = "Document No. : ";
            lblDocumentNumber.FontSize = 12;
            lblDocumentNumber.FontFamily = new System.Windows.Media.FontFamily("consolas");
            lblDocumentNumber.Padding = new System.Windows.Thickness(0);
            Canvas.SetLeft(lblDocumentNumber, left);
            Canvas.SetTop(lblDocumentNumber, top);
            content.Children.Add(lblDocumentNumber);

            txtDocumentNumber.Text = trans.DocumentNumber;
            txtDocumentNumber.FontSize = 12;
            txtDocumentNumber.FontFamily = new System.Windows.Media.FontFamily("consolas");
            txtDocumentNumber.Width = 150;
            Canvas.SetLeft(txtDocumentNumber, 170);
            Canvas.SetTop(txtDocumentNumber, top);
            content.Children.Add(txtDocumentNumber);

            //comment
            lblComment.Content = "Comment : ";
            lblComment.FontSize = 12;
            lblComment.FontFamily = new System.Windows.Media.FontFamily("consolas");
            lblComment.Padding = new System.Windows.Thickness(0);
            Canvas.SetLeft(lblComment, 450);
            Canvas.SetTop(lblComment, top);
            content.Children.Add(lblComment);

            txtComment.Text = trans.Comment;
            txtComment.FontSize = 12;
            txtComment.FontFamily = new System.Windows.Media.FontFamily("consolas");
            txtComment.MinWidth = 150;
            Canvas.SetLeft(txtComment, 580);
            Canvas.SetTop(txtComment, top);
            content.Children.Add(txtComment);

            #endregion
            top += 20;
            #region line 8 : REASON

            //document number
            lblReason.Content = "Reason : ";
            lblReason.FontSize = 12;
            lblReason.FontFamily = new System.Windows.Media.FontFamily("consolas");
            lblReason.Padding = new System.Windows.Thickness(0);
            Canvas.SetLeft(lblReason, left);
            Canvas.SetTop(lblReason, top);
            content.Children.Add(lblReason);

            txtReason.Text = trans.Reason.ToUpper();
            txtReason.FontSize = 12;
            txtReason.FontFamily = new System.Windows.Media.FontFamily("consolas");
            txtReason.MinWidth = 150;
            Canvas.SetLeft(txtReason, 170);
            Canvas.SetTop(txtReason, top);
            content.Children.Add(txtReason);

            #endregion
            top += 12;
            #region DIVIDER 1

            //divider
            string line = "";

            for ( int i = 0;i < 115;i++ )
                line = line + "-";

            lblHorizontalLine_1.Content = line;
            lblHorizontalLine_1.FontSize = 12;
            lblHorizontalLine_1.FontFamily = new System.Windows.Media.FontFamily("consolas");
            lblHorizontalLine_1.Padding = new System.Windows.Thickness(0);
            Canvas.SetLeft(lblHorizontalLine_1, left);
            Canvas.SetTop(lblHorizontalLine_1, top);
            content.Children.Add(lblHorizontalLine_1);

            #endregion
            top += 20;
            #region item columns

            //item code
            lblItemCode.Content = "Item Code";
            lblItemCode.FontSize = 12;
            lblItemCode.FontFamily = new System.Windows.Media.FontFamily("consolas");
            lblItemCode.Padding = new System.Windows.Thickness(0);
            Canvas.SetLeft(lblItemCode, left);
            Canvas.SetTop(lblItemCode, top);
            content.Children.Add(lblItemCode);

            //item description
            lblItemDescription.Content = "Item Description";
            lblItemDescription.FontSize = 12;
            lblItemDescription.FontFamily = new System.Windows.Media.FontFamily("consolas");
            lblItemDescription.Padding = new System.Windows.Thickness(0);
            Canvas.SetLeft(lblItemDescription, 100);
            Canvas.SetTop(lblItemDescription, top);
            content.Children.Add(lblItemDescription);

            //LC
            lblLC.Content = "LC";
            lblLC.FontSize = 12;
            lblLC.FontFamily = new System.Windows.Media.FontFamily("consolas");
            lblLC.Padding = new System.Windows.Thickness(0);
            Canvas.SetLeft(lblLC, 400);
            Canvas.SetTop(lblLC, top);
            content.Children.Add(lblLC);

            //cases
            lblCases.Content = "Cases";
            lblCases.FontSize = 12;
            lblCases.FontFamily = new System.Windows.Media.FontFamily("consolas");
            lblCases.Padding = new System.Windows.Thickness(0);
            lblCases.Width = 50;
            lblCases.HorizontalContentAlignment = System.Windows.HorizontalAlignment.Right;
            Canvas.SetLeft(lblCases, 430);
            Canvas.SetTop(lblCases, top);
            content.Children.Add(lblCases);

            //pieces
            lblPieces.Content = "Pieces";
            lblPieces.FontSize = 12;
            lblPieces.FontFamily = new System.Windows.Media.FontFamily("consolas");
            lblPieces.Padding = new System.Windows.Thickness(0);
            lblPieces.Width = 50;
            lblPieces.HorizontalContentAlignment = System.Windows.HorizontalAlignment.Right;
            Canvas.SetLeft(lblPieces, 480);
            Canvas.SetTop(lblPieces, top);
            content.Children.Add(lblPieces);

            //unit price
            lblUnitPrice.Content = "Unit Price";
            lblUnitPrice.FontSize = 12;
            lblUnitPrice.FontFamily = new System.Windows.Media.FontFamily("consolas");
            lblUnitPrice.Padding = new System.Windows.Thickness(0);
            lblUnitPrice.Width = 80;
            lblUnitPrice.HorizontalContentAlignment = System.Windows.HorizontalAlignment.Right;
            Canvas.SetLeft(lblUnitPrice, 540);
            Canvas.SetTop(lblUnitPrice, top);
            content.Children.Add(lblUnitPrice);

            //value
            lblValue.Content = "Value";
            lblValue.FontSize = 12;
            lblValue.FontFamily = new System.Windows.Media.FontFamily("consolas");
            lblValue.Padding = new System.Windows.Thickness(0);
            lblValue.Width = 115;
            lblValue.HorizontalContentAlignment = System.Windows.HorizontalAlignment.Right;
            Canvas.SetLeft(lblValue, 670);
            Canvas.SetTop(lblValue, top);
            content.Children.Add(lblValue);

            #endregion
            top += 20;
            #region DIVIDER 2
            //divider
            lblHorizontalLine_2.Content = line;
            lblHorizontalLine_2.FontSize = 12;
            lblHorizontalLine_2.FontFamily = new System.Windows.Media.FontFamily("consolas");
            lblHorizontalLine_2.Padding = new System.Windows.Thickness(0);
            Canvas.SetLeft(lblHorizontalLine_2, left);
            Canvas.SetTop(lblHorizontalLine_2, top);
            content.Children.Add(lblHorizontalLine_2);
            #endregion

            //for (int x = 0; x < 850; x++)
            //{
            //    if (x % 50 == 0)
            //    {
            //        lblHorizontalLine_2 = new Label();

            //        lblHorizontalLine_2.Content = x.ToString();
            //        lblHorizontalLine_2.FontSize = 12;
            //        lblHorizontalLine_2.FontFamily = new System.Windows.Media.FontFamily("consolas");
            //        lblHorizontalLine_2.Padding = new System.Windows.Thickness(0);
            //        Canvas.SetLeft(lblHorizontalLine_2, x);
            //        Canvas.SetTop(lblHorizontalLine_2, top);
            //        content.Children.Add(lblHorizontalLine_2);
            //    }
            //}
            top += 20;
        }

        private void AppendItem( Canvas content, InventoryDummyModel item )
        {
            txtItemCode = new TextBlock();
            txtItemDescription = new TextBlock();
            txtLC = new TextBlock();
            txtCases = new TextBlock();
            txtPieces = new TextBlock();
            txtUnitPrice = new TextBlock();
            txtValue = new TextBlock();

            //Item Code
            txtItemCode.Text = item.ItemCode;
            txtItemCode.FontSize = 12;
            txtItemCode.FontFamily = new System.Windows.Media.FontFamily("consolas");
            txtItemCode.Width = 100;
            Canvas.SetLeft(txtItemCode, 30);
            Canvas.SetTop(txtItemCode, top);
            content.Children.Add(txtItemCode);

            //Item Description
            txtItemDescription.Text = item.ItemDescription;
            txtItemDescription.FontSize = 12;
            txtItemDescription.FontFamily = new System.Windows.Media.FontFamily("consolas");
            txtItemDescription.Width = 280;
            txtItemDescription.TextWrapping = System.Windows.TextWrapping.Wrap;
            Canvas.SetLeft(txtItemDescription, 100);
            Canvas.SetTop(txtItemDescription, top);
            content.Children.Add(txtItemDescription);

            //LC
            txtLC.Text = item.Location;
            txtLC.FontSize = 12;
            txtLC.FontFamily = new System.Windows.Media.FontFamily("consolas");
            txtLC.Width = 280;
            txtLC.TextWrapping = System.Windows.TextWrapping.Wrap;
            Canvas.SetLeft(txtLC, 400);
            Canvas.SetTop(txtLC, top);
            content.Children.Add(txtLC);

            //Cases
            txtCases.Text = item.Cases.ToString();
            txtCases.FontSize = 12;
            txtCases.FontFamily = new System.Windows.Media.FontFamily("consolas");
            txtCases.Width = 50;
            txtCases.TextWrapping = System.Windows.TextWrapping.Wrap;
            txtCases.TextAlignment = System.Windows.TextAlignment.Right;
            Canvas.SetLeft(txtCases, 430);
            Canvas.SetTop(txtCases, top);
            content.Children.Add(txtCases);

            //pieces
            txtPieces.Text = item.Pieces.ToString();
            txtPieces.FontSize = 12;
            txtPieces.FontFamily = new System.Windows.Media.FontFamily("consolas");
            txtPieces.Width = 50;
            txtPieces.TextWrapping = System.Windows.TextWrapping.Wrap;
            txtPieces.TextAlignment = System.Windows.TextAlignment.Right;
            Canvas.SetLeft(txtPieces, 480);
            Canvas.SetTop(txtPieces, top);
            content.Children.Add(txtPieces);

            //Unit price
            txtUnitPrice.Text = item.PricePerPiece.ToString("#,##0.00");
            txtUnitPrice.FontSize = 12;
            txtUnitPrice.FontFamily = new System.Windows.Media.FontFamily("consolas");
            txtUnitPrice.Width = 80;
            txtUnitPrice.TextWrapping = System.Windows.TextWrapping.Wrap;
            txtUnitPrice.TextAlignment = System.Windows.TextAlignment.Right;
            Canvas.SetLeft(txtUnitPrice, 540);
            Canvas.SetTop(txtUnitPrice, top);
            content.Children.Add(txtUnitPrice);

            //Value
            txtValue.Text = item.LineAmount.ToString("#,##0.00");
            txtValue.FontSize = 12;
            txtValue.FontFamily = new System.Windows.Media.FontFamily("consolas");
            txtValue.Width = 115;
            txtValue.TextWrapping = System.Windows.TextWrapping.Wrap;
            txtValue.TextAlignment = System.Windows.TextAlignment.Right;
            Canvas.SetLeft(txtValue, 670);
            Canvas.SetTop(txtValue, top);
            content.Children.Add(txtValue);

            if ( item.ItemDescription.Length > 42 ) {
                top += 35;
            }
            else {
                top += 20;
            }

            if ( top > 1030 )
                createNewPage = true;
        }

        private void AppendFooter( Canvas content, TransactionModel transaction )
        {
            if ( top > 900 )
                CreateNewPage(transaction);

            txtTotalCases = new TextBlock();
            txtTotalPieces = new TextBlock();
            txtOrderAmount = new TextBlock();

            lblPreparedBy = new Label();
            lblCheckedBy = new Label();
            lblReceivedBy = new Label();

            #region Borders

            Thickness borderThickness = new Thickness(0, 2, 0, 0);
            SolidColorBrush borderColor = new SolidColorBrush(Colors.Black);

            Border borderCases = new Border();
            borderCases.BorderThickness = borderThickness;
            borderCases.BorderBrush = borderColor;

            Border borderPieces = new Border();
            borderPieces.BorderThickness = borderThickness;
            borderPieces.BorderBrush = borderColor;

            Border borderAmount = new Border();
            borderAmount.BorderThickness = borderThickness;
            borderAmount.BorderBrush = borderColor;

            Border borderPreparedBy = new Border();
            borderPreparedBy.BorderThickness = borderThickness;
            borderPreparedBy.BorderBrush = borderColor;

            Border borderCheckedBy = new Border();
            borderCheckedBy.BorderThickness = borderThickness;
            borderCheckedBy.BorderBrush = borderColor;

            Border borderReceivedBy = new Border();
            borderReceivedBy.BorderThickness = borderThickness;
            borderReceivedBy.BorderBrush = borderColor;
            #endregion

            #region Totals

            //Total cases
            txtTotalCases.Text = totalCases.ToString("#,##0");
            txtTotalCases.FontSize = 12;
            txtTotalCases.FontFamily = new System.Windows.Media.FontFamily("consolas");
            txtTotalCases.Width = 50;
            txtTotalCases.TextAlignment = System.Windows.TextAlignment.Right;
            Canvas.SetLeft(borderCases, 430);
            Canvas.SetTop(borderCases, top);
            content.Children.Add(borderCases);

            borderCases.Child = txtTotalCases;

            //Total Pieces
            txtTotalPieces.Text = totalPieces.ToString("#,##0");
            txtTotalPieces.FontSize = 12;
            txtTotalPieces.FontFamily = new System.Windows.Media.FontFamily("consolas");
            txtTotalPieces.Width = 50;
            txtTotalPieces.TextAlignment = System.Windows.TextAlignment.Right;
            Canvas.SetLeft(borderPieces, 482);
            Canvas.SetTop(borderPieces, top);
            content.Children.Add(borderPieces);

            borderPieces.Child = txtTotalPieces;

            //order amount
            txtOrderAmount.Text = orderAmount.ToString("#,##0.00");
            txtOrderAmount.FontSize = 12;
            txtOrderAmount.FontFamily = new System.Windows.Media.FontFamily("consolas");
            txtOrderAmount.Width = 115;
            txtOrderAmount.TextAlignment = System.Windows.TextAlignment.Right;
            Canvas.SetLeft(borderAmount, 675);
            Canvas.SetTop(borderAmount, top);
            content.Children.Add(borderAmount);

            borderAmount.Child = txtOrderAmount;

            #endregion

            top += 100;

            #region Signatures

            //Prepared by
            lblPreparedBy.Content = "Prepared By";
            lblPreparedBy.FontSize = 12;
            lblPreparedBy.FontFamily = new System.Windows.Media.FontFamily("consolas");
            lblPreparedBy.Width = 100;
            lblPreparedBy.HorizontalContentAlignment = HorizontalAlignment.Center;
            Canvas.SetLeft(borderPreparedBy, 160);
            Canvas.SetTop(borderPreparedBy, top);
            content.Children.Add(borderPreparedBy);

            borderPreparedBy.Child = lblPreparedBy;

            //Checked by
            lblCheckedBy.Content = "Checked By";
            lblCheckedBy.FontSize = 12;
            lblCheckedBy.FontFamily = new System.Windows.Media.FontFamily("consolas");
            lblCheckedBy.Width = 100;
            lblCheckedBy.HorizontalContentAlignment = HorizontalAlignment.Center;
            Canvas.SetLeft(borderCheckedBy, 370);
            Canvas.SetTop(borderCheckedBy, top);
            content.Children.Add(borderCheckedBy);

            borderCheckedBy.Child = lblCheckedBy;

            //Received by
            lblReceivedBy.Content = "Received By";
            lblReceivedBy.FontSize = 12;
            lblReceivedBy.FontFamily = new System.Windows.Media.FontFamily("consolas");
            lblReceivedBy.Width = 100;
            lblReceivedBy.HorizontalContentAlignment = HorizontalAlignment.Center;
            Canvas.SetLeft(borderReceivedBy, 590);
            Canvas.SetTop(borderReceivedBy, top);
            content.Children.Add(borderReceivedBy);

            borderReceivedBy.Child = lblReceivedBy;

            #endregion
        }
    }

    public class CheckedItem: ViewModelBase {

        private bool isChecked;
        public bool IsChecked {
            get { return isChecked; }
            set {
                isChecked = value;
                OnPropertyChanged("IsChecked");
            }
        }

        private TransactionModel transaction;
        public TransactionModel Transaction {
            get { return transaction; }
            set {
                transaction = value;
                OnPropertyChanged("Transaction");
            }
        }
    }
}
