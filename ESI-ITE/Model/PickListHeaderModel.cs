using ESI_ITE.Data_Access;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESI_ITE.Model
{
    public class PickListHeaderModel : IModelTemplate
    {
        #region Properties

        DataAccess db = new DataAccess();

        private int id;
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        private string headerNumber;
        public string HeaderNumber
        {
            get { return headerNumber; }
            set { headerNumber = value; }
        }

        private int userId;
        public int UserId
        {
            get { return userId; }
            set { userId = value; }
        }

        private DateTime pickDate;
        public DateTime Pickdate
        {
            get { return pickDate; }
            set { pickDate = value; }
        }

        private bool isSuccessful;
        public bool IsSuccessful
        {
            get { return isSuccessful; }
            set { isSuccessful = value; }
        }

        private bool isAssigned;
        public bool IsAssigned
        {
            get { return isAssigned; }
            set { isAssigned = value; }
        }

        private bool isGatepassPrinted;
        public bool IsGatepassPrinted
        {
            get { return isGatepassPrinted; }
            set { isGatepassPrinted = value; }
        }

        private int? gatepassId;
        public int? GatepassId
        {
            get { return gatepassId; }
            set { gatepassId = value; }
        }

        #endregion

        public void AddNew(object item)
        {
            var header = (PickListHeaderModel)item;

            StringBuilder sb = new StringBuilder();
            sb.Append("insert into pickhead values (null, ");
            sb.Append("'" + header.HeaderNumber + "', ");
            sb.Append("'" + header.UserId + "', ");
            sb.Append("str_to_date('" + header.Pickdate.ToString("MM-dd-yyyy-HH-mm-ss") + "','%m-%d-%Y-%H-%i-%s'), ");
            sb.Append("'");
            sb.Append(header.IsSuccessful ? "1" : "0");
            sb.Append("', ");
            sb.Append("'");
            sb.Append(header.IsAssigned ? "1" : "0");
            sb.Append("', ");
            sb.Append("'");
            sb.Append(header.IsGatepassPrinted ? "1" : "0");
            sb.Append("', ");
            sb.Append(string.IsNullOrWhiteSpace(header.GatepassId.ToString()) ? "null" : "'" + header.GatepassId + "'");
            sb.Append(")");

            db.Insert(sb.ToString());
        }

        public string GetAddQuery(PickListHeaderModel item)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("insert into pickhead values (null, ");
            sb.Append("'" + item.HeaderNumber + "', ");
            sb.Append("'" + item.UserId + "', ");
            sb.Append("str_to_date('" + item.Pickdate.ToString("MM-dd-yyyy-HH-mm-ss") + "','%m-%d-%Y-%H-%i-%s'), ");
            sb.Append("'");
            sb.Append(item.IsSuccessful ? "1" : "0");
            sb.Append("', ");
            sb.Append("'");
            sb.Append(item.IsAssigned ? "1" : "0");
            sb.Append("', ");
            sb.Append("'");
            sb.Append(item.IsGatepassPrinted ? "1" : "0");
            sb.Append("', ");
            sb.Append(string.IsNullOrWhiteSpace(item.GatepassId.ToString()) ? "null" : "'" + item.GatepassId + "'");
            sb.Append(")");

            return sb.ToString();
        }


        public void DeleteItem(string qry)
        {
            throw new NotImplementedException();
        }


        public void DeleteItem(string id, string type)
        {
            switch (type)
            {
                case "id":
                    db.Delete("delete from pickhead where pickhead_id = '" + id + "'");
                    break;
                case "code":
                    db.Delete("delete from pickhead where pick_number = '" + id + "'");
                    break;
            }

        }


        public void DeleteItem(PickListHeaderModel item)
        {
            if (item != null)
                db.Delete("delete from pickhead where pickhead_id = '" + item.Id + "'");
        }

        public string GetDeleteQuery(PickListHeaderModel item)
        {
            return "delete from pickhead where pickhead_id = '" + item.Id + "'";
        }

        public object Fetch(string id, string type)
        {
            var result = new List<CloneableDictionary<string, string>>();
            switch (type)
            {
                case "id":
                    result = db.SelectMultiple("select * from pickhead where pickhead_id = '" + id + "'");
                    break;
                case "code":
                    result = db.SelectMultiple("select * from pickhead where pick_number = '" + id + "'");
                    break;
            }

            var header = new PickListHeaderModel();

            foreach (var row in result)
            {
                var clone = row.Clone();

                header.Id = int.Parse(row["pickhead_id"]);
                header.HeaderNumber = row["pick_number"];
                header.UserId = int.Parse(row["user_id"]);
                header.Pickdate = DateTime.Parse(row["pick_date"]);
                header.IsSuccessful = row["is_successful"] == "1" ? true : false;
                header.IsAssigned = row["is_assigned"] == "1" ? true : false;
                header.IsGatepassPrinted = row["is_gatepass_printed"] == "1" ? true : false;

                if (!string.IsNullOrWhiteSpace(row["gatepass_id"]))
                    header.GatepassId = int.Parse(row["gatepass_id"]);
                else
                    header.GatepassId = null;
            }

            return header;
        }

        public List<object> FetchAll()
        {
            var headerList = new List<object>();
            var result = db.SelectMultiple("select * from pickhead");

            foreach (var row in result)
            {
                var clone = row.Clone();
                var header = new PickListHeaderModel();

                header.Id = int.Parse(row["pickhead_id"]);
                header.HeaderNumber = row["pick_number"];
                header.UserId = int.Parse(row["user_id"]);
                header.Pickdate = DateTime.Parse(row["pick_date"]);
                header.IsSuccessful = row["is_successful"] == "True" ? true : false;
                header.IsAssigned = row["is_assigned"] == "True" ? true : false;
                header.IsGatepassPrinted = row["is_gatepass_printed"] == "True" ? true : false;

                if (!string.IsNullOrWhiteSpace(row["gatepass_id"]))
                    header.GatepassId = int.Parse(row["gatepass_id"]);
                else
                    header.GatepassId = null;

                headerList.Add(header);
            }

            return headerList;
        }

        public void UpdateItem(string qry)
        {
            throw new NotImplementedException();
        }

        public int GenerateId()
        {
            var maxId = db.Select("select max(pickhead_id) from pickhead limit 1");

            if (!string.IsNullOrWhiteSpace(maxId))
                return int.Parse(maxId) + 1;
            else
                return 1;
        }
    }
}
