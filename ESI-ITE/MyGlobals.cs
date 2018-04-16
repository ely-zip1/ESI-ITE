using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ESI_ITE.ViewModel;
using ESI_ITE.Model;
using ESI_ITE.View;
using System.Windows.Controls;
using System.Windows.Documents;
using ESI_ITE.ViewModel.CNDN;

namespace ESI_ITE
{
    public static class MyGlobals
    {
        public static MainWindowViewModel MainWindow;

        public static LoginView loginView;

        public static UserModel LoggedUser;


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

        public static InvoicingMainPageViewModel InvoicingMainVM;

        #endregion


        #region CN/DN
        public static CnDnViewModel CnDnVM;

        public static CnDnEntryOptionsViewModel CnDnEntryOptionsVM;

        public static string SelectedCNDNTransaction;

        public static string SelectedCNDNPrice;

        #endregion


        public static FixedDocument printingDoc;

        public static bool hasTransactionError = false;

        //Progress Monitors
        public static float ProgressTotalStep;
        public static float ProgressCurrentStep;
        public static float ProgressPercentComplete;
        public static float ProgressCurrentStage;
        public static string ProgressDescription;
    }
}
