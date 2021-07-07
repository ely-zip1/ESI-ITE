using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ESI_ITE.Data_Access;

namespace ESI_ITE.Model
{
    public class OrderNumberModel: IModelTemplate
    {
        #region Properties

        DataAccess db = new DataAccess();

        private int id;
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        private string orderNumber;
        public string OrderNumber
        {
            get { return orderNumber; }
            set { orderNumber = value; }
        }

        #endregion

        public void AddNew( object item )
        {
            throw new NotImplementedException();
        }

        public void DeleteItem( string qry )
        {
            throw new NotImplementedException();
        }

        public object Fetch( string id, string type )
        {
            var number = new OrderNumberModel();

            var record = db.SelectMultiple("select * from order_number");

            foreach ( var row in record )
            {
                var clone = row.Clone();

                number.Id = int.Parse(row["id"]);
                number.OrderNumber = row["order_number"];
            }

            return number;
        }

        public List<object> FetchAll( )
        {
            return null;
        }

        public void UpdateItem( string qry )
        {
            var number = db.Select("select order_number from order_number limit 1");
            int temp;
            string finalValue = "";

            if ( !string.IsNullOrWhiteSpace(number) )
            {
                temp = int.Parse(number);
                temp++;

                switch ( temp.ToString().Length )
                {
                    case 1:
                        finalValue = "00000" + temp;
                        break;
                    case 2:
                        finalValue = "0000" + temp;
                        break;
                    case 3:
                        finalValue = "000" + temp;
                        break;
                    case 4:
                        finalValue = "00" + temp;
                        break;
                    case 5:
                        finalValue = "0" + temp;
                        break;
                    default:
                        finalValue = temp.ToString();
                        break;
                }

                db.Update("update order_number set order_number = '" + finalValue + "' where id = 1");
            }
        }
    }
}
