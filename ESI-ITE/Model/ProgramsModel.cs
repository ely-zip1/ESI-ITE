using ESI_ITE.Data_Access;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESI_ITE.Model
{
    public class ProgramsModel: IModelTemplate
    {
        #region Properties

        DataAccess db = new DataAccess();

        private int id;
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        private string programName;
        public string ProgramName
        {
            get { return programName; }
            set { programName = value; }
        }

        private string category;
        public string Category
        {
            get { return category; }
            set { category = value; }
        }


        #endregion



        public List<object> FetchAll( )
        {
            var programsList = new List<object>();
            var results = db.SelectMultiple("select * from programs");

            foreach ( var row in results )
            {
                var clone = row.Clone();
                var program = new ProgramsModel();

                program.Id = int.Parse(row["program_id"]);
                program.ProgramName = row["program_name"];
                program.Category = row["program_category"];

                programsList.Add(program);
            }

            return programsList;
        }

        public object Fetch( string id, string type )
        {
            var program = new ProgramsModel();
            var result = new List<CloneableDictionary<string, string>>();

            switch ( type )
            {
                case "id":
                    result = db.SelectMultiple("select * from programs where program_id = '" + id + "'");
                    break;
                case "code":
                    result = db.SelectMultiple("select * from programs where program_name = '" + id + "'");
                    break;
            }

            foreach ( var row in result )
            {
                var clone = row.Clone();

                program.Id = int.Parse(row["program_id"]);
                program.ProgramName = row["program_name"];
                program.Category = row["program_category"];
            }

            return program;
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
