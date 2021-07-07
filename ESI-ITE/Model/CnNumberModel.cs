using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ESI_ITE.Data_Access;

namespace ESI_ITE.Model
{
    public class CnNumberModel : IModelTemplate
    {
        #region Properties

        DataAccess db = new DataAccess();

        private int id;
        public int Id
        {
            get
            {
                return id;
            }
            set
            {
                id = value;
            }
        }

        private string creditNoteNumber;
        public string CreditNoteNumber
        {
            get
            {
                return creditNoteNumber;
            }
            set
            {
                creditNoteNumber = value;
            }
        }


        #endregion

        public void AddNew(object item)
        {
            var cn_number = item as CnNumberModel;

            StringBuilder sb = new StringBuilder();

            sb.Append("insert into cn_number values(");
            sb.Append("null,");
            sb.Append("'" + cn_number.CreditNoteNumber + "'");
            sb.Append(")");
        }

        public void DeleteItem(string qry)
        {
            throw new NotImplementedException();
        }

        public object Fetch(string id, string type)
        {
            throw new NotImplementedException();
        }

        public List<object> FetchAll()
        {
            var cnNumberList = new List<object>();

            var results = db.SelectMultiple("select * from cn_number");

            foreach (var row in results)
            {
                var clone = row.Clone();
                var cnNumber = new CnNumberModel();

                cnNumber.Id = int.Parse(row["id"]);
                cnNumber.CreditNoteNumber = row["cn_number"];

                cnNumberList.Add(cnNumber);
            }

            return cnNumberList;
        }

        public void UpdateItem(string qry)
        {
            throw new NotImplementedException();
        }

        public void UpdateCnNumber(CnNumberModel cnNumber)
        {
            var currentCnNumber = int.Parse(cnNumber.CreditNoteNumber);
            currentCnNumber++;

            var cnString = currentCnNumber.ToString();

            while (cnString.Length < 6)
            {
                cnString = "0" + cnString;
            }

            db.Update("update cn_number set cn_number = '" + cnString + "' where id = '" + cnNumber.Id + "'");
        }

        public CnNumberModel FetchLatest()
        {
            var cnNumberObject = new CnNumberModel();

            var result = db.SelectMultiple("select * from cn_number");

            foreach (var row in result)
            {
                var clone = row.Clone();

                cnNumberObject.Id = int.Parse(row["id"]);
                cnNumberObject.CreditNoteNumber = row["cn_number"];
                break;
            }

            return cnNumberObject;
        }
    }
}
