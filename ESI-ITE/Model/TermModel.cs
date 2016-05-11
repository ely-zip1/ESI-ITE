using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESI_ITE.Model
{
    class TermModel
    {
        #region Properties

        private int termId;

        public int TermId
        {
            get { return termId; }
            set { termId = value; }
        }

        private string termCode;

        public string TermCode
        {
            get { return termCode; }
            set { termCode = value; }
        }

        private string termDescription;

        public string TermDescription
        {
            get { return termDescription; }
            set { termDescription = value; }
        }

        private decimal discount1;

        public decimal Discount1
        {
            get { return discount1; }
            set { discount1 = value; }
        }

        private decimal discount2;

        public decimal Discount2
        {
            get { return discount2; }
            set { discount2 = value; }
        }

        private decimal discount3;

        public decimal Discount3
        {
            get { return discount3; }
            set { discount3 = value; }
        }

        private int days;

        public int Days
        {
            get { return days; }
            set { days = value; }
        }


        #endregion

    }
}
