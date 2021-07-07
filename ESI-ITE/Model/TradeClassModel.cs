using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ESI_ITE.Data_Access;

namespace ESI_ITE.Model
{
    public class TradeClassModel: IModelTemplate
    {
        #region Properties

        DataAccess db = new DataAccess();

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

        public List<object> FetchAll( )
        {
            List<object> list = new List<object>();

            var record = db.SelectMultiple("select * from trade_class");

            IFormatProvider culture = new System.Globalization.CultureInfo("en-US", true);

            foreach ( var row in record )
            {
                list.Clear();
                var trade = new TradeClassModel();
                var clone = row.Clone();

                trade.TradeClassId = int.Parse(row["trade_class_id"]);
                trade.Code = row["code"].ToString();
                trade.Description = row["description"].ToString();

                list.Add(trade);
            }

            return list;
        }

        public object Fetch( string id, string type )
        {
            var trade = new TradeClassModel();

            var record = new List<CloneableDictionary<string, string>>();

            if ( type == "code" )
                record = db.SelectMultiple("select * from trade_class where code = '" + id + "'");
            else if ( type == "id" )
                record = db.SelectMultiple("select * from trade_class where trade_class_id = '" + id + "'");


            IFormatProvider culture = new System.Globalization.CultureInfo("en-US", true);

            foreach ( var row in record )
            {
                var clone = row.Clone();

                trade.TradeClassId = int.Parse(row["trade_class_id"]);
                trade.Code = row["code"].ToString();
                trade.Description = row["description"].ToString();
            }

            return trade;
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
