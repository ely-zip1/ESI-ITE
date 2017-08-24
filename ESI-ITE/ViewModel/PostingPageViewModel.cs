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
using System.Threading;

namespace ESI_ITE.ViewModel
{
    public class PostingPageViewModel : ViewModelBase
    {
        DataAccess db = new DataAccess();

        public PostingPageViewModel()
        {
            postCommand = new DelegateCommand(CallPostTransactions);
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

        private float pbMax;
        public float PbMax
        {
            get { return pbMax; }
            set
            {
                pbMax = value;
                OnPropertyChanged("PbMax");
            }
        }

        private float pbValue;
        public float PbValue
        {
            get { return pbValue; }
            set
            {
                pbValue = value;
                OnPropertyChanged("PbValue");
            }
        }

        private string progressInfo;
        public string ProgressInfo
        {
            get { return progressInfo; }
            set
            {
                progressInfo = value;
                OnPropertyChanged();
            }
        }

        private bool isClickable = true;
        public bool IsClickable
        {
            get { return isClickable; }
            set
            {
                isClickable = value;
                OnPropertyChanged();
            }
        }


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

        //private async void CallPostTransactions()
        //{
        //    var totalItems = 100;
        //    var numberOfStages = 2;
        //    PbMin = 0;

        //    var progress = new Progress<ProgressReportModel>();

        //    progress.ProgressChanged += (o, report) =>
        //    {
        //        PbMax = report.TotalStep;
        //        PbValue = report.PercentComplete;
        //        ProgressInfo = "Stage " + report.CurrentStage + " of " + numberOfStages + ": " + report.Description;
        //    };

        //    IsClickable = false;

        //    await PostTransactionsAsync(progress, totalItems);

        //    IsClickable = true;
        //}

        //private Task PostTransactionsAsync(IProgress<ProgressReportModel> progress, int totalItems)
        //{
        //    return Task.Factory.StartNew(() => PostTransactions(progress, totalItems));
        //}

        //private void PostTransactions(IProgress<ProgressReportModel> progress, float totalItems)
        //{
        //    var progressReport = new ProgressReportModel();
        //    float percentComplete = 0;
        //    progressReport.TotalStep = 100;
        //    progressReport.CurrentStage = 1;
        //    progressReport.Description = "Preparing files for Posting";

        //    for (float x = 1; x <= totalItems; x++)
        //    {
        //        Thread.Sleep(100);

        //        progressReport.CurrentStep = x;
        //        percentComplete = (x / totalItems) * 100;

        //        progressReport.PercentComplete = percentComplete;
        //        progress.Report(progressReport);
        //    }

        //    progressReport.CurrentStage += 1;
        //    progressReport.Description = "Posting transactions";
        //    db.Test(progress, progressReport);
        //}

        private async void CallPostTransactions()
        {
            var totalItems = db.Count("select count(*) from transaction_details");
            var numberOfStages = 2;
            PbMin = 0;

            var progress = new Progress<ProgressReportModel>();

            progress.ProgressChanged += (o, report) =>
            {
                PbMax = report.TotalStep;
                PbValue = report.PercentComplete;
                ProgressInfo = "Stage " + report.CurrentStage + " of " + numberOfStages + ": " + report.Description;
            };
            IsClickable = false;

            await PostTransactionsAsync(progress, totalItems);
            IsClickable = true;
        }

        private Task PostTransactionsAsync(IProgress<ProgressReportModel> progress, float totalItems)
        {
            return Task.Factory.StartNew(() => PostTransactions(progress, totalItems));
        }

        private void PostTransactions(IProgress<ProgressReportModel> progress, float totalItems)
        {
            var progressReport = new ProgressReportModel();
            progressReport.TotalStep = totalItems;
            progressReport.Description = "Preparing files for Posting";
            progressReport.CurrentStage = 1;

            float currentStep = 0;
            float percentComplete = 0;

            var transactionObj = new TransactionModel();
            var transactionDummyObj = new InventoryDummyModel();

            var transactionList = transactionObj.FetchAll();
            var queryList = new List<string>();

            foreach (var transaction in transactionList)        //TRANSACTIONS --------------------
            {
                var warehouseObj = new WareHouseModel();
                warehouseObj = (WareHouseModel)warehouseObj.Fetch(transaction.SourceWarehouseCode, "code");

                var locationObj = new LocationModel();
                locationObj = (LocationModel)locationObj.Fetch(transaction.SourceLocationCode, "code");

                if (HaveItems(transaction.TransactionNumber))
                {
                    var dummyItems = transactionDummyObj.Fetch(transaction.TransactionNumber);
                    var inventoryMasterObj = new InventoryMaster2Model();

                    foreach (var dummyItem in dummyItems)       //ITEMS ----------------------------
                    {
                        var itemMasterObj = new ItemModel();
                        var item = (ItemModel)itemMasterObj.Fetch(dummyItem.ItemCode, "code");
                        var inventoryItem = inventoryMasterObj.FetchItem(item.ItemId, dummyItem.Expiration);

                        if (string.IsNullOrWhiteSpace(inventoryItem.Id.ToString()))
                        {
                            inventoryItem.ItemId = item.ItemId;
                            inventoryItem.WarehouseId = warehouseObj.Id;
                            inventoryItem.LocationId = locationObj.Id;
                            inventoryItem.Cases = dummyItem.Cases;
                            inventoryItem.Pieces = dummyItem.Pieces;
                            inventoryItem.ExpirationDate = dummyItem.Expiration;
                            inventoryItem.LotNumber = dummyItem.LotNumber;

                            queryList.Add(inventoryMasterObj.GetAddQuery(inventoryItem));

                            //
                            // Progress Reporting
                            //
                            currentStep++;
                            percentComplete = (currentStep / totalItems) * 100;

                            progressReport.CurrentStep = currentStep;
                            progressReport.PercentComplete = percentComplete;

                            progress.Report(progressReport);
                        }
                        else
                        {
                            var piecePerCase = item.PackSize * item.PackSizeBO;

                            if ((inventoryItem.Pieces + dummyItem.Pieces) >= piecePerCase)
                            {
                                inventoryItem.Cases += dummyItem.Cases;
                                inventoryItem.Cases += (dummyItem.Pieces + inventoryItem.Pieces) / piecePerCase;

                                inventoryItem.Pieces = (dummyItem.Pieces + inventoryItem.Pieces) % piecePerCase;
                            }
                            else
                            {
                                inventoryItem.Cases += dummyItem.Cases;
                                inventoryItem.Pieces += dummyItem.Pieces;
                            }

                            queryList.Add(inventoryMasterObj.GetUpdateQuery(inventoryItem));

                            //
                            // Progress Reporting
                            //
                            currentStep++;
                            percentComplete = (currentStep / totalItems) * 100;

                            progressReport.CurrentStep = currentStep;
                            progressReport.PercentComplete = percentComplete;

                            progress.Report(progressReport);
                        }
                    }
                }
            }

            progressReport.CurrentStage += 1;
            progressReport.Description = "Posting transactions";
            db.RunMySqlTransaction(queryList, progress, progressReport);
        }

        private bool HaveItems(string transactionNumber)
        {
            string query = "select * from view_inventory_dummy where transaction_code = '" + transactionNumber + "'";

            var result = db.SelectMultiple(query);

            if (result.Count > 0)
                return true;
            else
                return false;
        }

        private void CancelPosting()
        {
            MyGlobals.IteViewModel.SelectedPage = MyGlobals.PostingParent;
        }

        #endregion
    }
}
