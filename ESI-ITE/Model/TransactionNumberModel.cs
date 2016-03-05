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
            string current = Fetch();
            int value = Convert.ToInt32(current) + 1;
            string newValue;
            string leadingZero = "";

            for (int i = value.ToString().Length; i < 6; i++)
            {
                leadingZero = leadingZero + "0";
            }

            newValue = leadingZero + (Convert.ToInt32(current) + 1);

            db.Update("update transaction_number set transaction_number = '" + newValue + "' where transaction_number = '" + Fetch() + "'");
        }
    }
}
