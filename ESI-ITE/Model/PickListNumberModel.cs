using ESI_ITE.Data_Access;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ESI_ITE.Model
{
    public class PickListNumberModel: IModelTemplate
    {
        #region Properties

        DataAccess db = new DataAccess();

        private int id;
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        private string number;
        public string Number
        {
            get { return number; }
            set { number = value; }
        }

        private DateTime date;
        public DateTime Date
        {
            get { return date; }
            set { date = value; }
        }


        #endregion

        public void AddNew( object item )
        {
            var pickNumber = (PickListNumberModel)item;

            StringBuilder sb = new StringBuilder();

            sb.Append("insert into picknumber values( null, ");
            sb.Append("'" + GenerateNumber() + "', ");
            sb.Append("time(now()) ");
            sb.Append(")");
        }

        public string GetAddQuery( )
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("insert into picknumber values( null, ");
            sb.Append("'" + GenerateNumber() + "', ");
            sb.Append("time(now()) ");
            sb.Append(")");

            return sb.ToString();
        }

        public string GenerateNumber( )
        {
            var result = db.Select("select pick_number from picknumber order by date desc limit 1");
            int lastPickNumber = 0;

            try
            {
                lastPickNumber = int.Parse(result);
                lastPickNumber++;
            }
            catch ( Exception e )
            {
                MessageBox.Show(e.Message);
            }

            string pickNumber = lastPickNumber.ToString();

            for ( int x = pickNumber.Length;x < 6;x++ )
            {
                pickNumber = "0" + pickNumber;
            }

            return pickNumber;
        }

        public void DeleteItem( string qry )
        {
            throw new NotImplementedException();
        }

        public object Fetch( string id, string type )
        {
            throw new NotImplementedException();
        }

        public List<object> FetchAll( )
        {
            throw new NotImplementedException();
        }

        public void UpdateItem( string qry )
        {
            throw new NotImplementedException();
        }
    }
}
