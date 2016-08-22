using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESI_ITE.ViewModel
{
    public class InvoicingPickListViewModel: ViewModelBase, IDataErrorInfo
    {
        public InvoicingPickListViewModel( )
        {

        }

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
    }
}
