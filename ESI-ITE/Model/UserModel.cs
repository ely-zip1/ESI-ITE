using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESI_ITE.Model
{
    public class UserModel
    {
        private string firstName;
        public string FirstName
        {
            get { return firstName; }
            set { firstName = value; }
        }

        private string lastName;
        public string LastName
        {
            get { return lastName; }
            set { lastName = value; }
        }

        private string middleName;
        public string MiddleName
        {
            get { return middleName; }
            set { middleName = value; }
        }

        private string username;
        public string Username
        {
            get { return username; }
            set { username = value; }
        }

        private string password;
        public string Password
        {
            get { return password; }
            set { password = value; }
        }

        private Dictionary<string, string> userGroups;
        public Dictionary<string, string> UserGroups
        {
            get { return userGroups; }
            set { userGroups = value; }
        }


    }
}
