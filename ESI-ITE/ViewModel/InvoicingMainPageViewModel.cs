using ESI_ITE.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESI_ITE.ViewModel
{
    public class InvoicingMainPageViewModel: ViewModelBase, IDataErrorInfo
    {
        public InvoicingMainPageViewModel( )
        {
            MyGlobals.InvoicingMainVM = this;
        }

        private InvoicingPickListView pickListView = new InvoicingPickListView();
        public InvoicingPickListView PickListView
        {
            get { return pickListView; }
            set
            {
                pickListView = value;
                OnPropertyChanged();
            }
        }

        private InvoicingAllocationMaintenanceView allocationMaintenanceView = new InvoicingAllocationMaintenanceView();
        public InvoicingAllocationMaintenanceView AllocationMaintenanceView
        {
            get { return allocationMaintenanceView; }
            set
            {
                allocationMaintenanceView = value;
                OnPropertyChanged();
            }
        }

        private InvoicingAssignmentView assignmentView = new InvoicingAssignmentView();
        public InvoicingAssignmentView AssignmentView
        {
            get { return assignmentView; }
            set
            {
                assignmentView = value;
                OnPropertyChanged();
            }
        }

        private InvoicingGatepassView gatepassView = new InvoicingGatepassView();
        public InvoicingGatepassView GatepassView
        {
            get { return gatepassView; }
            set
            {
                gatepassView = value;
                OnPropertyChanged();
            }
        }

        private InvoicingDispatchView dispatchView = new InvoicingDispatchView();
        public InvoicingDispatchView DispatchView
        {
            get { return dispatchView; }
            set
            {
                dispatchView = value;
                OnPropertyChanged();
            }
        }



        #region IDataErrorInfo Members
        public string this[string columnName]
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public string Error
        {
            get
            {
                throw new NotImplementedException();
            }
        }
        #endregion


    }
}
