using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ESI_ITE.Model;
using ESI_ITE.ViewModel.Command;
using System.Windows.Input;
using System.ComponentModel;
using ESI_ITE.Data_Access;
using System.Windows;

namespace ESI_ITE.ViewModel
{
    public class PostingPageViewModel: ViewModelBase
    {
        DataAccess db = new DataAccess();

        public PostingPageViewModel( )
        {
            postCommand = new DelegateCommand(InitializePosting);
            cancelCommand = new DelegateCommand(CancelPosting);
        }


        #region Properties

        private int pbMin;
        public int PbMin
        {
            get { return pbMin; }
            set
            {
                pbMin = value;
                OnPropertyChanged("PbMin");
            }
        }

        private int pbMax;
        public int PbMax
        {
            get { return pbMax; }
            set
            {
                pbMax = value;
                OnPropertyChanged("PbMax");
            }
        }

        private int pbValue;
        public int PbValue
        {
            get { return pbValue; }
            set
            {
                pbValue = value;
                OnPropertyChanged("PbValue");
            }
        }

        PostingModel postingModel = new PostingModel();
        TransactionModel transaction = new TransactionModel();
        InventoryDummyModel inventoryDummy = new InventoryDummyModel();

        private DelegateCommand postCommand;
        public ICommand PostCommand
        {
            get { return postCommand; }
        }

        private DelegateCommand cancelCommand;
        public ICommand Cancelcommand
        {
            get { return cancelCommand; }
        }

        #endregion

        #region Methods

        private async void InitializePosting( )
        {
            int numberOfItems = 0;

            numberOfItems = inventoryDummy.CountAll();

            PbMin = 0;
            PbMax = numberOfItems;

            var progress = new Progress<ProgressReport>();
            progress.ProgressChanged += ( o, report ) =>
            {
                PbValue = report.PercentComplete;
            };

            await ExecutePosting(progress, numberOfItems);

        }

        private Task ExecutePosting( IProgress<ProgressReport> progress, int maxValue )
        {
            var progressReport = new ProgressReport() { TotalStep = maxValue };

            return Task.Run(( ) =>
            {
                foreach ( var trans in transaction.FetchAll() )
                {
                    if ( HaveItems(trans.TransactionNumber) )
                    {
                        postingModel.Post(trans.TransactionNumber);
                        postingModel.ItemChecked += progressReport.OnItemChecked;
                        db.ItemPosted += progressReport.OnItemPosted;
                        progress.Report(progressReport);
                    }
                }
            });

        }

        private bool HaveItems( string transactionNumber )
        {
            string query = "select * from view_inventory_dummy where transaction_code = '" + transactionNumber + "'";

            var result = db.SelectMultiple(query);

            if ( result.Count > 0 )
                return true;
            else
                return false;
        }

        private void CancelPosting( )
        {
            throw new NotImplementedException();
        }

        #endregion
    }

    public class ProgressReport
    {
        public int PercentComplete
        {
            get { return MyGlobals.ProgressPercentComplete; }
            set { MyGlobals.ProgressPercentComplete = value; }
        }

        public int CurrentStep
        {
            get { return MyGlobals.ProgressCurrentStep; }
            set { MyGlobals.ProgressCurrentStep = value; }
        }

        public int TotalStep
        {
            get { return MyGlobals.ProgressTotalStep; }
            set { MyGlobals.ProgressTotalStep = value; }
        }

        public void OnItemChecked( object sender, EventArgs args )
        {
            CurrentStep++;
            PercentComplete = (CurrentStep / TotalStep) * 100;
        }

        public void OnItemPosted( object sender, EventArgs args )
        {
            CurrentStep++;
            PercentComplete = (CurrentStep / TotalStep) * 100;
        }

    }
}
