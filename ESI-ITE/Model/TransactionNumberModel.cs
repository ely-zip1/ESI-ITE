using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ESI_ITE.Data_Access;

namespace ESI_ITE.Model
{
    public class TransactionNumberModel
    {
        private string Number { get; set; }

        readonly DataAccess db = new DataAccess();

        public string Fetch()
        {
            Number = db.Select("Select transaction_number from transaction_number order by transaction_number desc limit 1");

            return Number;
        }

        public void Update()
        {
            db.Update("update transaction_number set transaction_number = (transaction_number + 1 ) where transaction_number = '" + Fetch() + "'");
        }
    }
}
