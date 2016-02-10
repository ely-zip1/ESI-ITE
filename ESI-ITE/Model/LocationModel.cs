using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ESI_ITE.Data_Access;

namespace ESI_ITE.Model
{
    public class LocationModel : ObjectBase
    {
        DataAccess db = new DataAccess();
        private List<LocationModel> _locations = new List<LocationModel>();

        private int id;
        public int Id
        {
            get { return id; }
            set
            {
                if (id != value)
                {
                    id = value;
                    OnPropertyChanged("Id");
                }
            }
        }

        private string location;
        public string Location
        {
            get { return location; }
            set
            {
                if (location != value)
                {
                    location = value;
                    OnPropertyChanged("Location");
                }
            }
        }

        private string code;
        public string Code
        {
            get { return code; }
            set
            {
                if (code != value)
                {
                    code = value;
                    OnPropertyChanged("Code");
                }
            }
        }

        private bool status;
        public bool Status
        {
            get { return status; }
            set
            {
                if (status != value)
                {
                    status = value;
                    OnPropertyChanged("Status");
                }
            }
        }

        public List<LocationModel> FetchAll()
        {
            List<CloneableDictionary<string, string>> table = db.SelectMultiple("select * from location");
            foreach (var row in table)
            {
                var temp = new LocationModel();
                var clone = row.Clone();
                temp.Id = Int32.Parse(row["location_id"]);
                temp.Location = row["location"];
                temp.Code = row["code"];
                if (row["status"] == "0")
                    temp.Status = false;
                else
                    temp.Status = true;

                _locations.Add(temp);
            }
            return _locations;
        }


    }
}
