using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ESI_ITE.Data_Access;

namespace ESI_ITE.Model
{
    public class DnNumberModel : IModelTemplate
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

        private string debitNoteNumber;
        public string DebitNoteNumber
        {
            get
            {
                return debitNoteNumber;
            }
            set
            {
                debitNoteNumber = value;
            }
        }


        #endregion

        public void AddNew(object item)
        {
            var cn_number = item as DnNumberModel;

            StringBuilder sb = new StringBuilder();

            sb.Append("insert into cn_number values(");
            sb.Append("null,");
            sb.Append("'" + cn_number.DebitNoteNumber + "'");
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
            var dnNumberList = new List<object>();

            var results = db.SelectMultiple("select * from dn_number");

            foreach (var row in results)
            {
                var clone = row.Clone();
                var dnNumber = new DnNumberModel();

                dnNumber.Id = int.Parse(row["id"]);
                dnNumber.DebitNoteNumber = row["dn_number"];

                dnNumberList.Add(dnNumber);
            }

            return dnNumberList;
        }

        public void UpdateItem(string qry)
        {
            throw new NotImplementedException();
        }

        public void UpdateCnNumber(DnNumberModel dnNumber)
        {
            var currentDnNumber = int.Parse(dnNumber.DebitNoteNumber);
            currentDnNumber++;

            var dnString = currentDnNumber.ToString();

            while (dnString.Length < 6)
            {
                dnString = "0" + dnString;
            }

            db.Update("update dn_number set dn_number = '" + dnString + "' where id = '" + dnNumber.Id + "'");
        }

        public DnNumberModel FetchLatest()
        {
            var dnNumberObject = new DnNumberModel();

            var result = db.SelectMultiple("select * from dn_number");

            foreach (var row in result)
            {
                var clone = row.Clone();

                dnNumberObject.Id = int.Parse(row["id"]);
                dnNumberObject.DebitNoteNumber = row["dn_number"];
                break;
            }

            return dnNumberObject;
        }
    }
}
