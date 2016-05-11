using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESI_ITE.Model
{
    class SupplierModel
    {
        #region Properties

        private int supplierId;

        public int SupplierId
        {
            get { return supplierId; }
            set { supplierId = value; }
        }

        private string supplierCode;

        public string SupplierCode
        {
            get { return supplierCode; }
            set { supplierCode = value; }
        }

        private string supplierName;

        public string SupplierName
        {
            get { return supplierName; }
            set { supplierName = value; }
        }

        private string address;

        public string Address
        {
            get { return address; }
            set { address = value; }
        }

        private string contactPerson;

        public string ContactPerson
        {
            get { return contactPerson; }
            set { contactPerson = value; }
        }

        private string contactnumber;

        public string ContacNumber
        {
            get { return contactnumber; }
            set { contactnumber = value; }
        }

        private int termId;

        public int TermId
        {
            get { return termId; }
            set { termId = value; }
        }


        #endregion

    }
}
