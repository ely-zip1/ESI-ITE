using ESI_ITE.Data_Access;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESI_ITE.Model
{
    public class AllocatedStocksModel: IModelTemplate
    {

        #region Properties

        DataAccess db = new DataAccess();

        private int id;
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        private int pickHeadId;
        public int PickHeadId
        {
            get { return pickHeadId; }
            set { pickHeadId = value; }
        }

        private int inventoryDummyId;
        public int InventoryDummyId
        {
            get { return inventoryDummyId; }
            set { inventoryDummyId = value; }
        }

        private int cases;
        public int Cases
        {
            get { return cases; }
            set { cases = value; }
        }

        private int pieces;
        public int Pieces
        {
            get { return pieces; }
            set { pieces = value; }
        }

        private DateTime expiry;
        public DateTime Expiry
        {
            get { return expiry; }
            set { expiry = value; }
        }


        #endregion


        public void AddNew( object item )
        {
            var stock = (AllocatedStocksModel)item;

            StringBuilder sb = new StringBuilder();

            sb.Append("insert into allocated_stocks values(null, ");
            sb.Append("'" + stock.PickHeadId + "', ");
            sb.Append("'" + stock.InventoryDummyId + "', ");
            sb.Append("'" + stock.Cases + "', ");
            sb.Append("'" + stock.Pieces + "', ");
            sb.Append("str_to_date('" + stock.Expiry.ToString("MM/dd/yyyy") + "', '%m/%d/%Y'))");

            db.Insert(sb.ToString());
        }



        public string GetAddQuery( AllocatedStocksModel item )
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("insert into allocated_stocks values(null, ");
            sb.Append("'" + item.PickHeadId + "', ");
            sb.Append("'" + item.InventoryDummyId + "', ");
            sb.Append("'" + item.Cases + "', ");
            sb.Append("'" + item.Pieces + "', ");
            sb.Append("str_to_date('" + item.Expiry.ToString("MM/dd/yyyy") + "', '%m/%d/%Y'))");

            return sb.ToString();
        }



        public void DeleteItem( string qry )
        {
            db.Delete(qry);
        }



        public void DeleteItem( AllocatedStocksModel item )
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("delete from allocated_stocks where ");
            sb.Append("allocated_stocks_id = '" + item.Id + "' and ");
            sb.Append("inventory_dummy_id = '" + item.InventoryDummyId + "'");

            db.Delete(sb.ToString());
        }



        public string GetDeleteQuery( AllocatedStocksModel item )
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("delete from allocated_stocks where ");
            sb.Append("allocated_stocks_id = '" + item.Id + "' and ");
            sb.Append("inventory_dummy_id = '" + item.InventoryDummyId + "'");

            return sb.ToString();
        }



        public object Fetch( string id, string type )
        {
            var result = new List<CloneableDictionary<string, string>>();
            var stock = new AllocatedStocksModel();

            switch ( type )
            {
                case "allocationId":
                    result = db.SelectMultiple("select * from allocated_stocks where allocated_stocks_id = '" + id + "' ");
                    ;
                    break;
                case "dummyId":
                    result = db.SelectMultiple("select * from allocated_stocks where inventory_dummy_id = '" + id + "' ");
                    break;
            }

            foreach ( var row in result )
            {
                var clone = row.Clone();

                stock.Id = int.Parse(row["allocated_stocks_id"]);
                stock.PickHeadId = int.Parse(row["pickhead_id"]);
                stock.InventoryDummyId = int.Parse(row["inventory_dummy_id"]);
                stock.Cases = int.Parse(row["cases"]);
                stock.Pieces = int.Parse(row["pieces"]);
                stock.Expiry = DateTime.Parse(row["expiry"]);
            }

            return stock;
        }



        public List<AllocatedStocksModel> FetchPerPickList( string picklistNumber )
        {
            var ListOfAllocatedStocks = new List<AllocatedStocksModel>();
            var pickHead = new PickListHeaderModel();
            var results = new List<CloneableDictionary<string, string>>();
            var stock = new AllocatedStocksModel();

            pickHead = (PickListHeaderModel)pickHead.Fetch(picklistNumber, "code");
            results = db.SelectMultiple("select * from allocated_stocks where pickhead_id = '" + pickHead.Id + "'");

            foreach ( var row in results )
            {
                var clone = row.Clone();

                stock.Id = int.Parse(row["allocated_stocks_id"]);
                stock.PickHeadId = int.Parse(row["pickhead_id"]);
                stock.InventoryDummyId = int.Parse(row["inventory_dummy_id"]);
                stock.Cases = int.Parse(row["cases"]);
                stock.Pieces = int.Parse(row["pieces"]);
                stock.Expiry = DateTime.Parse(row["expiry"]);
            }

            return ListOfAllocatedStocks;
        }



        public List<AllocatedStocksModel> FetchPerOrder( string orderNumber )
        {
            var ListOfAllocatedStocks = new List<AllocatedStocksModel>();



            return ListOfAllocatedStocks;
        }



        public List<object> FetchAll( )
        {
            var stockList = new List<object>();
            var result = db.SelectMultiple("select * from allocated_stocks");

            foreach ( var row in result )
            {
                var clone = row.Clone();
                var stock = new AllocatedStocksModel();

                stock.Id = int.Parse(row["allocated_stocks_id"]);
                stock.PickHeadId = int.Parse(row["pickhead_id"]);
                stock.InventoryDummyId = int.Parse(row["inventory_dummy_id"]);
                stock.Cases = int.Parse(row["cases"]);
                stock.Pieces = int.Parse(row["pieces"]);
                stock.Expiry = DateTime.Parse(row["expiry"]);

                stockList.Add(stock);
            }

            return stockList;
        }



        public void UpdateItem( string qry )
        {
            throw new NotImplementedException();
        }
    }
}
