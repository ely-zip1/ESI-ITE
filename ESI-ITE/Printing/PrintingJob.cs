using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ESI_ITE.Model;
using ESI_ITE.View;
using System.Windows.Documents;
using System.Windows.Controls;
using ESI_ITE.Printing;
using System.Windows.Markup;
using System.Printing;
using System.Windows.Xps;
using System.Drawing;

namespace ESI_ITE.Printing
{
    public class PrintingJob
    {
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

        int top = 30;
        int left = 30;

        InventoryDummyModel dummy = new InventoryDummyModel();

        private bool createNewPage = false;

        #endregion

        public void StartPrinting()
        {
            DummyPrint();
            //InventoryDummyModel dummy = new InventoryDummyModel();

            //FixedDocumentSequence documentSequence = new FixedDocumentSequence();

            //DocumentReference docReference = new DocumentReference();

            //foreach (var transaction in MyGlobals.TransactionList)
            //{
            //    //create a document for each transaction
            //    FixedDocument printDocument = new FixedDocument();

            //    docReference.SetDocument(printDocument);
            //    documentSequence.References.Add(docReference);

            //    //load templates
            //    PrintingHeaderTemplate headerTemplate = new PrintingHeaderTemplate();
            //    PrintingItemTemplate itemTemplate = new PrintingItemTemplate();
            //    PrintingFooterTemplate footerTemplate = new PrintingFooterTemplate();

            //    //get linedItems for this transaction
            //    List<InventoryDummyModel> items = dummy.FetchAll(transaction.TransactionNumber);

            //    //create the page & page content
            //    FixedPage page = new FixedPage();
            //    PageContent pageContent = new PageContent();
            //    Canvas content = new Canvas();

            //    ((IAddChild)pageContent).AddChild(page);
            //    printDocument.Pages.Add(pageContent);

            //    bool createNewPage = false;
            //    int left = 96;
            //    int top = 96;
            //    int pageNumber = 1;

            //    foreach (var line in items)
            //    {
            //        if (createNewPage)
            //        {
            //            page = new FixedPage();
            //            pageContent = new PageContent();
            //            ((IAddChild)pageContent).AddChild(page);
            //            printDocument.Pages.Add(pageContent);

            //            content = new Canvas();
            //            page.Children.Add(content);

            //            headerTemplate = new PrintingHeaderTemplate();
            //            headerTemplate.txbUser.Text = "ELI";
            //            headerTemplate.txbPrintDate.Text = DateTime.Now.ToString("MM/dd/yyyy");
            //            headerTemplate.txbPrintTime.Text = DateTime.Now.ToString("H:mm:ss");
            //            headerTemplate.txbPrintPageNo.Text = pageNumber.ToString();

            //            headerTemplate.txbTransNo.Text = transaction.TransactionType;
            //            headerTemplate.txbTransNo.Text = transaction.TransactionNumber;
            //            headerTemplate.txbTransDate.Text = transaction.TransactionDate.ToString("MM/dd/yyyy");
            //            headerTemplate.txbDocNo.Text = transaction.DocumentNumber;
            //            headerTemplate.txbReason.Text = transaction.Reason;
            //            headerTemplate.txbSourceWH.Text = transaction.SourceWarehouseCode;
            //            headerTemplate.txbLocation.Text = transaction.SourceLocationCode;
            //            headerTemplate.txbComment.Text = transaction.Comment;

            //            content.Children.Add(headerTemplate);

            //            top += (int)Math.Round(headerTemplate.Height);
            //            pageNumber++;

            //            createNewPage = false;
            //        }

            //        itemTemplate = new PrintingItemTemplate();
            //        itemTemplate.txbItemCode.Text = line.ItemCode;
            //        itemTemplate.txbDescription.Text = line.ItemDescription;
            //        itemTemplate.txbLC.Text = line.Location;
            //        itemTemplate.txbCases.Text = line.Cases.ToString();
            //        itemTemplate.txbPieces.Text = line.Pieces.ToString();
            //        itemTemplate.txbExpiry.Text = line.Expiration.ToString("MM/dd/yyyy");
            //        itemTemplate.txbUnitPrice.Text = line.PricePerPiece.ToString();
            //        itemTemplate.txbValue.Text = line.LineAmount.ToString();

            //        Canvas.SetLeft(itemTemplate, left);
            //        Canvas.SetTop(itemTemplate, top);

            //        top += (int)Math.Round(itemTemplate.Height);

            //        if (top >= page.Height)
            //        {
            //            createNewPage = true;
            //            top = 96;
            //        }
            //        content.Children.Add(itemTemplate);
            //    }
            //}

            //// Print document
            //PrintDocumentImageableArea imageArea = null;
            //XpsDocumentWriter xpsdw = PrintQueue.CreateXpsDocumentWriter(ref imageArea);
            //if (xpsdw != null)
            //{
            //    xpsdw.Write(documentSequence);
            //}
        }

        private void DummyPrint()
        {
            foreach (var transaction in MyGlobals.TransactionList)
            {
                FixedDocument doc = new FixedDocument();

                List<InventoryDummyModel> items = dummy.FetchAll(transaction.TransactionNumber);

                FixedPage[] page = new FixedPage[items.Count / 20];
                PageContent[] pageContent = new PageContent[items.Count / 20];
                Canvas[] content = new Canvas[items.Count / 20];

                int pageNumber = 0;

                page[pageNumber] = new FixedPage();
                pageContent[pageNumber] = new PageContent();
                ((IAddChild)pageContent[pageNumber]).AddChild(page[pageNumber]);
                doc.Pages.Add(pageContent[pageNumber]);

                content[pageNumber] = new Canvas();
                page[pageNumber].Children.Add(content[pageNumber]);

                AppendHeader(content[pageNumber], pageNumber + 1, transaction);

                foreach (var item in items)
                {
                    AppendItem(content[pageNumber], item);

                    if (createNewPage)
                    {
                        pageNumber++;

                        page[pageNumber] = new FixedPage();
                        pageContent[pageNumber] = new PageContent();
                        ((IAddChild)pageContent[pageNumber]).AddChild(page[pageNumber]);
                        doc.Pages.Add(pageContent[pageNumber]);

                        content[pageNumber] = new Canvas();
                        page[pageNumber].Children.Add(content[pageNumber]);

                        top = 30;
                        
                        AppendHeader(content[pageNumber], pageNumber + 1, transaction);

                        createNewPage = false;
                    }
                }

                // Print document
                PrintDocumentImageableArea imageArea = null;
                XpsDocumentWriter xpsdw = PrintQueue.CreateXpsDocumentWriter(ref imageArea);
                if (xpsdw != null)
                {
                    xpsdw.Write(doc);
                }
            }
        }

        private void AppendHeader(Canvas content, int pageNumber, TransactionModel trans)
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
            txtPageNumber.Text = pageNumber.ToString();
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

            for (int i = 0; i < 115; i++)
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
            Canvas.SetLeft(lblCases, 430);
            Canvas.SetTop(lblCases, top);
            content.Children.Add(lblCases);

            //pieces
            lblPieces.Content = "Pieces";
            lblPieces.FontSize = 12;
            lblPieces.FontFamily = new System.Windows.Media.FontFamily("consolas");
            lblPieces.Padding = new System.Windows.Thickness(0);
            Canvas.SetLeft(lblPieces, 480);
            Canvas.SetTop(lblPieces, top);
            content.Children.Add(lblPieces);

            //unit price
            lblUnitPrice.Content = "Unit Price";
            lblUnitPrice.FontSize = 12;
            lblUnitPrice.FontFamily = new System.Windows.Media.FontFamily("consolas");
            lblUnitPrice.Padding = new System.Windows.Thickness(0);
            Canvas.SetLeft(lblUnitPrice, 540);
            Canvas.SetTop(lblUnitPrice, top);
            content.Children.Add(lblUnitPrice);

            //value
            lblValue.Content = "Value";
            lblValue.FontSize = 12;
            lblValue.FontFamily = new System.Windows.Media.FontFamily("consolas");
            lblValue.Padding = new System.Windows.Thickness(0);
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

        private void AppendItem(Canvas content, InventoryDummyModel item)
        {
            txtItemCode = new TextBlock();
            txtItemDescription = new TextBlock();

            //Item Code
            txtItemCode.Text = item.ItemCode;
            txtItemCode.FontSize = 12;
            txtItemCode.FontFamily = new System.Windows.Media.FontFamily("consolas");
            txtItemCode.Width = 100;
            Canvas.SetLeft(txtItemCode, left);
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

            if (item.ItemDescription.Length > 42)
            {
                top += 35;
            }
            else
            {
                top += 20;
            }

            if (top > 1030)
                createNewPage = true;
        }
    }
}
