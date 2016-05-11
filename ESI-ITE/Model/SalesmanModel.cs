using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESI_ITE.Model
{
    class SalesmanModel
    {
        #region Properties

        private int salesmanId;

        public int SalesmanId
        {
            get { return salesmanId; }
            set { salesmanId = value; }
        }

        private string salesmanNumber;

        public string SalesmanNumber
        {
            get { return salesmanNumber; }
            set { salesmanNumber = value; }
        }

        private string salesmanName;

        public string SalesmanName
        {
            get { return salesmanName; }
            set { salesmanName = value; }
        }


        #endregion

    }
}
