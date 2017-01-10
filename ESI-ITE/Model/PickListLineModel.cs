using ESI_ITE.Data_Access;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESI_ITE.Model
{
    public class PickListLineModel : IModelTemplate
    {
        #region Properties

        DataAccess db = new DataAccess();

        private int id;
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        private int pickListHeaderId;
        public int PickListHeaderId
        {
            get { return pickListHeaderId; }
            set { pickListHeaderId = value; }
        }

        private int inventoryDummyId;
        public int InventoryDummyId
        {
            get { return inventoryDummyId; }
            set { inventoryDummyId = value; }
        }

        private int allocatedCases;
        public int AllocatedCases
        {
            get { return allocatedCases; }
            set { allocatedCases = value; }
        }

        private int allocatedPieces;
        public int AllocatedPieces
        {
            get { return allocatedPieces; }
            set { allocatedPieces = value; }
        }

        private bool isCritical;
        public bool IsCritical
        {
            get { return isCritical; }
            set { isCritical = value; }
        }


        #endregion

        public void AddNew(object item)
        {
            var line = (PickListLineModel)item;

            StringBuilder sb = new StringBuilder();

            sb.Append("insert into pickline values (null, ");
            sb.Append("(select max(pickhead_id) from pickhead), ");
            sb.Append("'" + line.InventoryDummyId + "', ");
            sb.Append("'" + line.AllocatedCases + "', ");
            sb.Append("'" + line.AllocatedPieces + "', ");
            sb.Append("'");
            sb.Append(line.IsCritical ? "1" : "0");
            sb.Append("')");

            db.Insert(sb.ToString());
        }


        public string GetAddQuery(PickListLineModel item)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("insert into pickline values (null, ");
            sb.Append("(select max(pickhead_id) from pickhead), ");
            sb.Append("'" + item.InventoryDummyId + "', ");
            sb.Append("'" + item.AllocatedCases + "', ");
            sb.Append("'" + item.AllocatedPieces + "', ");
            sb.Append("'");
            sb.Append(item.IsCritical ? "1" : "0");
            sb.Append("')");

            return sb.ToString();
        }


        public void DeleteItem(string qry)
        {
            db.Delete(qry);
        }


        public void DeleteItem(PickListLineModel pickLine)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("delete from pickline where ");
            sb.Append("pickhead_id = '" + pickLine.PickListHeaderId + "' and ");
            sb.Append("inventory_dummy_id = '" + pickLine.InventoryDummyId + "'");
            //sb.Append("pickline_id = '"+pickLine.Id + "' ");

            db.Delete(sb.ToString());
        }


        public string GetDeleteQuery(PickListLineModel pickLine)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("delete from pickline where ");
            sb.Append("pickhead_id = '" + pickLine.PickListHeaderId + "' and ");
            sb.Append("inventory_dummy_id = '" + pickLine.InventoryDummyId + "'");
            //sb.Append("pickline_id = '"+pickLine.Id + "' ");

            return sb.ToString();
        }


        public object Fetch(string id, string type)
        {
            var result = new List<CloneableDictionary<string, string>>();
            var line = new PickListLineModel();

            result = db.SelectMultiple("select * from pickline where pickline_id = '" + id + "'");

            foreach (var row in result)
            {
                var clone = row.Clone();

                line.Id = int.Parse(row["pickline_id"]);
                line.PickListHeaderId = int.Parse(row["pickhead_id"]);
                line.InventoryDummyId = int.Parse(row["inventory_dummy_id"]);
                line.AllocatedCases = int.Parse(row["allocated_cases"]);
                line.AllocatedPieces = int.Parse(row["allocated_pieces"]);
                line.IsCritical = row["is_critical"] == "1" ? true : false;
            }

            return line;
        }


        public List<object> FetchAll()
        {
            var lineList = new List<object>();
            var result = db.SelectMultiple("select * from pickline");

            foreach (var row in result)
            {
                var clone = row.Clone();
                var line = new PickListLineModel();

                line.Id = int.Parse(row["pickline_id"]);
                line.PickListHeaderId = int.Parse(row["pickhead_id"]);
                line.InventoryDummyId = int.Parse(row["inventory_dummy_id"]);
                line.AllocatedCases = int.Parse(row["allocated_cases"]);
                line.AllocatedPieces = int.Parse(row["allocated_pieces"]);
                line.IsCritical = row["is_critical"] == "1" ? true : false;

                lineList.Add(line);
            }

            return lineList;
        }


        public List<PickListLineModel> FetchPerPickHead(string pickHeadId)
        {
            var pickLineList = new List<PickListLineModel>();

            var result = db.SelectMultiple("select * from pickline where pickhead_id = '" + pickHeadId + "'");

            foreach (var row in result)
            {
                var clone = row.Clone();
                var line = new PickListLineModel();

                line.Id = int.Parse(row["pickline_id"]);
                line.PickListHeaderId = int.Parse(row["pickhead_id"]);
                line.InventoryDummyId = int.Parse(row["inventory_dummy_id"]);
                line.AllocatedCases = int.Parse(row["allocated_cases"]);
                line.AllocatedPieces = int.Parse(row["allocated_pieces"]);
                line.IsCritical = row["is_critical"] == "1" ? true : false;

                pickLineList.Add(line);
            }

            return pickLineList;
        }


        public void UpdateItem(string qry)
        {
            throw new NotImplementedException();
        }

        public void UpdateItem(PickListLineModel lineItem)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("update pickline set ");
            sb.Append("allocated_cases = '" + lineItem.AllocatedCases + "', ");
            sb.Append("allocated_pieces = '" + lineItem.AllocatedPieces + "', ");
            sb.Append("is_critical = '" + lineItem.IsCritical + "' ");
            sb.Append("where pickhead_id = '" + lineItem.PickListHeaderId + "' and inventory_dummy_id = '" + lineItem.InventoryDummyId + "'");

            db.Update(sb.ToString());
        }

        public string GetUpdateQuery(PickListLineModel lineItem)
        {

            StringBuilder sb = new StringBuilder();

            sb.Append("update pickline set ");
            sb.Append("allocated_cases = '" + lineItem.AllocatedCases + "', ");
            sb.Append("allocated_pieces = '" + lineItem.AllocatedPieces + "', ");
            sb.Append("is_critical = '" + lineItem.IsCritical + "' ");
            sb.Append("where pickhead_id = '" + lineItem.PickListHeaderId + "' and inventory_dummy_id = '" + lineItem.InventoryDummyId + "'");

            return sb.ToString();
        }
    }
}
