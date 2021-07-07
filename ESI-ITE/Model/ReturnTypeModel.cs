using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ESI_ITE.Data_Access;

namespace ESI_ITE.Model
{
    public class ReturnTypeModel : IModelTemplate
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

        private string code;
        public string Code
        {
            get
            {
                return code;
            }
            set
            {
                code = value;
            }
        }

        private string description;
        public string Description
        {
            get
            {
                return description;
            }
            set
            {
                description = value;
            }
        }

        #endregion

        public void AddNew(object item)
        {
            throw new NotImplementedException();
        }

        public void DeleteItem(string qry)
        {
            throw new NotImplementedException();
        }

        public object Fetch(string id, string type)
        {
            var returnTypeObject = new ReturnTypeModel();

            var result = new List<CloneableDictionary<string, string>>();

            if (type == "code")
            {
                var tempId = db.Select("select id from return_type where return_code = '" + id + "'");
                if (!string.IsNullOrWhiteSpace(tempId))
                    result = db.SelectMultiple("select * from return_type where id = '" + tempId + "'");
            }
            else if (type == "id")
            {
                result = db.SelectMultiple("select * from return_type where id = '" + id + "'");
            }

            foreach (var row in result)
            {
                var clone = row.Clone();

                returnTypeObject.id = int.Parse(row["id"]);
                returnTypeObject.Code = row["return_code"];
                returnTypeObject.Description = row["description"];
                break;
            }

            return returnTypeObject;
        }

        public List<object> FetchAll()
        {
            var returnTypeList = new List<object>();

            var results = db.SelectMultiple("select * from return_type");

            foreach (var row in results)
            {
                var clone = row.Clone();
                var returnTypeObject = new ReturnTypeModel();

                returnTypeObject.Id = int.Parse(row["id"]);
                returnTypeObject.Code = row["return_code"];
                returnTypeObject.Description = row["description"];

                returnTypeList.Add(returnTypeObject);
            }
            
            return returnTypeList;
        }

        public void UpdateItem(string qry)
        {
            throw new NotImplementedException();
        }
    }
}
