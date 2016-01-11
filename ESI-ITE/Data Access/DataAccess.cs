using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

using MySql.Data.MySqlClient;

namespace ESI_ITE.Data_Access
{
    public class DataAccess
    {
        private MySqlConnection connection;
        private string server;
        private string database;
        private string uid;
        private string password;

        public DataAccess()
        {
            Initialize();
        }

        private void Initialize()
        {
            server = "localhost";
            database = "test";
            uid = "admin";
            password = "admin";

            string connectionString;
            connectionString = "Server=" + server + ";Database=" + database +
                ";UID=" + uid + ";Password=" + password + ";";

            connection = new MySqlConnection(connectionString);
        }

        //Close connection
        private bool OpenConnection()
        {
            try
            {
                connection.Open();
                return true;
            }
            catch (MySqlException ex)
            {
                switch (ex.Number)
                {
                    case 0:
                        MessageBox.Show("Cannot connect to server.  Contact administrator");
                        break;

                    case 1045:
                        MessageBox.Show("Invalid username/password, please try again");
                        break;


                }
                return false;
            }
        }

        //Close connection
        private bool CloseConnection()
        {
            try
            {
                connection.Close();
                return true;
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        //Insert statement
        public void Insert(string query)
        {
            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.ExecuteNonQuery();
                this.CloseConnection();
            }
        }

        //Update statement
        public void Update(string query)
        {
            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandText = query;
                cmd.Connection = connection;
                cmd.ExecuteNonQuery();
                this.CloseConnection();
            }
        }

        //Delete statement
        public void Delete(string query)
        {
            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.ExecuteNonQuery();
                this.CloseConnection();
            }
        }

        //Select multiple statement
        public List<Dictionary<string, string>> SelectMultiple(string query)
        {
            List<Dictionary<string, string>> lol = new List<Dictionary<string, string>>();
            Dictionary<string, string> listOfStrings = new Dictionary<string, string>();

            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                MySqlDataReader dataReader = cmd.ExecuteReader();

                while (dataReader.Read())
                {
                    int columns = dataReader.FieldCount;
                    for (int x = 0; x < columns; x++)
                    {
                        listOfStrings.Add(dataReader.GetName(x), dataReader[x].ToString());
                    }
                    lol.Add(listOfStrings);

                }
                dataReader.Close();
                this.CloseConnection();

                return lol;
            }
            else
            {
                return lol;
            }

        }

        //Select single
        public string Select(string query)
        {
            string record = string.Empty;

            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                MySqlDataReader dataReader = cmd.ExecuteReader();

                if (dataReader.Read())
                {
                    record = dataReader.GetString(0);
                }
                dataReader.Close();
                this.CloseConnection();

                return record;
            }
            else
            {
                return record;
            }
        }

        //Count statement
        public int Count(string query)
        {
            int Count = -1;

            //Open Connection
            if (this.OpenConnection() == true)
            {
                //Create Mysql Command
                MySqlCommand cmd = new MySqlCommand(query, connection);

                //ExecuteScalar will return one value
                Count = int.Parse(cmd.ExecuteScalar() + "");

                //close Connection
                this.CloseConnection();

                return Count;
            }
            else
            {
                return Count;
            }
        }

        //Transaction 
        public void RunMySqlTransaction(List<string> transString)
        {
            if (this.OpenConnection() == true)
            {
                MySqlTransaction myTransaction =
                    connection.BeginTransaction(System.Data.IsolationLevel.ReadCommitted);
                try
                {
                    MySqlCommand cmd = new MySqlCommand();
                    foreach (string query in transString)
                    {
                        cmd.CommandText = query;
                        cmd.ExecuteNonQuery();
                    }

                    myTransaction.Commit();
                }
                catch (Exception ex)
                {
                    myTransaction.Rollback();
                }

            }
        }
    }
}
