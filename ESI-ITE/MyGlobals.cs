using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ESI_ITE.ViewModel;
using ESI_ITE.Model;
using ESI_ITE.View;
using System.Windows.Controls;

namespace ESI_ITE
{
    public static class MyGlobals
    {
        #region Transaction Entry 
        public static ITEViewModel IteViewModel;

        public static TransactionModel Transaction;

        public static bool IsNewTransaction;

        public static TransactionEntryPageView TransactionEntryPage;

        public static LineItemPageView LineItemPage;

        public static Page PrintingParent;

        public static Page PostingParent;

        public static List<InventoryDummyModel> TransactionItemsList;

        public static List<TransactionModel> TransactionList = new List<TransactionModel>();

        public static ItemModel SelectedItem = new ItemModel();
        #endregion


        #region Sales Order
        public static SOViewModel SoViewModel;

        public static SalesOrderModel SalesOrder;

        public static SalesOrderEntryView SalesOrderEntryPage;

        public static SalesOrderEntryViewModel SoEntryViewModel;
        #endregion


        #region Invoicing

        public static InvoicingViewModel InvoicingVM;
        
        #endregion


        //Progress Monitors
        public static int ProgressTotalStep;
        public static int ProgressCurrentStep;
        public static int ProgressPercentComplete;
    }
}
