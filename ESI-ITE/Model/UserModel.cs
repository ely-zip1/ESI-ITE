using ESI_ITE.Data_Access;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ESI_ITE.Model
{
    public class UserModel: IModelTemplate
    {
        #region Properties

        DataAccess db = new DataAccess();

        private int id;
        public int Id
        {
            get { return id; }
            set { id = value; }
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

        private string salt;
        public string Salt
        {
            get { return salt; }
            set { salt = value; }
        }


        //private List<string> authorizedPrograms;
        //public List<string> AuthorizedPrograms
        //{
        //    get { return authorizedPrograms; }
        //    set { authorizedPrograms = value; }
        //}


        #endregion

        public List<object> FetchAll( )
        {
            StringBuilder sb = new StringBuilder();
            var userList = new List<object>();
            var result = db.SelectMultiple("select * from users");

            foreach ( var row in result )
            {
                var clone = row.Clone();
                var user = new UserModel();

                user.Id = int.Parse(row["id"]);
                user.Username = row["username"];
                user.Password = row["password"];
                user.Salt = row["salt"];

                //var progList = db.SelectMultiple("select * from view_user_permissions where user_id = '" + user.Id + "'");

                //foreach ( var prog in progList )
                //{
                //    user.AuthorizedPrograms.Add(prog["program_name"]);
                //}

                userList.Add(user);
            }

            return userList;
        }

        public object Fetch( string id, string type )
        {
            var user = new UserModel();
            var result = new List<CloneableDictionary<string, string>>();

            switch ( type )
            {
                case "id":
                    result = db.SelectMultiple("select * from users where id = '" + id + "'");
                    break;
                case "code":
                    result = db.SelectMultiple("select * from users where username = '" + id + "'");
                    break;
            }

            foreach ( var row in result )
            {
                var clone = row.Clone();

                user.Id = int.Parse(row["id"]);
                user.Username = row["username"];
                user.Password = row["password"];
                user.Salt = row["salt"];

                //var progList = db.SelectMultiple("select * from view_user_permissions where user_id = '" + user.Id + "'");

                //foreach ( var prog in progList )
                //{
                //    user.AuthorizedPrograms.Add(prog["program_name"]);
                //}
            }

            return user;
        }

        public void AddNew( object item )
        {
            var newUser = (UserModel)item;
            if ( !HasDuplicateUser(newUser.Username) )
            {
                StringBuilder sb = new StringBuilder();

                var _salt = GenerateSalt();
                var saltedPassword = ComputeHash(_salt + newUser.Password);

                sb.Append("insert into users values(null, ");
                sb.Append("'" + newUser.Username + "', ");
                sb.Append("'" + saltedPassword + "', ");
                sb.Append("'" + _salt + "'");
                sb.Append(")");

                db.Insert(sb.ToString());
            }
        }

        public void UpdateItem( string qry )
        {
            throw new NotImplementedException();
        }

        public void DeleteItem( string qry )
        {
            throw new NotImplementedException();
        }

        public bool HasDuplicateUser( string username )
        {
            var duplicateUser = db.Select("select username from users where username = '" + username + "'");
            if ( string.IsNullOrWhiteSpace(duplicateUser) )
                return false;
            else
                return true;
        }

        private string GenerateSalt( )
        {
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            byte[] buffer = new byte[32];

            rng.GetBytes(buffer);
            string hashString = string.Empty;

            foreach ( byte x in buffer )
            {
                hashString += String.Format("{0:x2}", x);
            }
            return hashString;
        }

        public string ComputeHash( string input )
        {
            //Byte[] inputBytes = Encoding.UTF8.GetBytes(input);

            //Byte[] hashedBytes = new SHA256CryptoServiceProvider().ComputeHash(inputBytes);

            //return BitConverter.ToString(hashedBytes);

            byte[] bytes = Encoding.ASCII.GetBytes(input);
            SHA256Managed hashstring = new SHA256Managed();
            byte[] hash = hashstring.ComputeHash(bytes);
            string hashString = string.Empty;

            foreach ( byte x in hash )
            {
                hashString += String.Format("{0:x2}", x);
            }
            return hashString;
        }
    }
}
