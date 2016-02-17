using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ESI_ITE.Data_Access;

namespace ESI_ITE.Model
{
    public class TransactionTypesModel
    {
        public TransactionTypesModel()
        {

        }

        public TransactionTypesModel(TransactionTypesModel source)
        {
            Id = source.Id;
            Code = source.Code;
            Description = source.Description;
        }
        DataAccess db = new DataAccess();
        private List<TransactionTypesModel> _transactionTypes { get; } = new List<TransactionTypesModel>();

        public int Id { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }

        public List<TransactionTypesModel> FetchAll()
        {
            
            List<CloneableDictionary<string, string>> table = db.SelectMultiple("select * from transaction_type");
            foreach (CloneableDictionary<string, string> row in table)
            {
                var temp = new TransactionTypesModel();
                var clone = row.Clone();
                temp.Id = Int32.Parse(clone["id"]);
                temp.Code = clone["transaction_code"];
                temp.Description = clone["transaction_type"];

                _transactionTypes.Add(temp);
            }
            return _transactionTypes;
        }
    }
}
