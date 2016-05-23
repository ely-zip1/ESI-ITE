using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ESI_ITE.Data_Access;


namespace ESI_ITE.Model
{
    class TermModel: IModelTemplate
    {
        #region Properties

        DataAccess db = new DataAccess();

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


        public List<object> FetchAll( )
        {
            List<object> list = new List<object>();

            var record = db.SelectMultiple("select * from terms");

            IFormatProvider culture = new System.Globalization.CultureInfo("en-US", true);

            foreach ( var row in record )
            {
                list.Clear();
                var term = new TermModel();
                var clone = row.Clone();

                term.TermId = int.Parse(row["term_id"]);
                term.TermCode = row["term_code"].ToString();
                term.TermDescription = row["term_description"].ToString();
                term.Discount1 = decimal.Parse(row["discount_1"]);
                term.Discount2 = decimal.Parse(row["discount_2"]);
                term.Discount3 = decimal.Parse(row["discount_3"]);
                term.Days = int.Parse(row["days"]);

                list.Add(term);
            }

            return list;
        }

        public object Fetch( string qry )
        {
            var term = new TermModel();

            var record = db.SelectMultiple("select * from terms where term_code = '" + qry + "'");

            IFormatProvider culture = new System.Globalization.CultureInfo("en-US", true);

            foreach ( var row in record )
            {
                var clone = row.Clone();

                term.TermId = int.Parse(row["term_id"]);
                term.TermCode = row["term_code"].ToString();
                term.TermDescription = row["term_description"].ToString();
                term.Discount1 = decimal.Parse(row["discount_1"]);
                term.Discount2 = decimal.Parse(row["discount_2"]);
                term.Discount3 = decimal.Parse(row["discount_3"]);
                term.Days = int.Parse(row["days"]);

            }

            return term;
        }

        public void AddNew( object item )
        {
            throw new NotImplementedException();
        }

        public void UpdateItem( string qry )
        {
            throw new NotImplementedException();
        }

        public void DeleteItem( string qry )
        {
            throw new NotImplementedException();
        }

    }
}
