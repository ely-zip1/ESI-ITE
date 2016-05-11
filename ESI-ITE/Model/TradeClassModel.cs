using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESI_ITE.Model
{
    class TradeClassModel
    {
        #region Properties

        private int tradeClassId;

        public int TradeClassId
        {
            get { return tradeClassId; }
            set { tradeClassId = value; }
        }

        private string code;

        public string Code
        {
            get { return code; }
            set { code = value; }
        }

        private string description;

        public string Description
        {
            get { return description; }
            set { description = value; }
        }



        #endregion
    }
}
