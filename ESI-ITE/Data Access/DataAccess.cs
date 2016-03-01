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

        private StringBuilder sb = new StringBuilder();

        public DataAccess()
        {
            Initialize();
        }

        private void Initialize()
        {
            server = "localhost";
            database = "esidb2";
            uid = "root";
            password = "1234";

            string connectionString;
            connectionString = "Server=" + server + ";Database=" + database +
                ";UID=" + uid + ";Password=" + password + ";";

            connection = new MySqlConnection(connectionString);
        }

        //Open connection
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
        public List<CloneableDictionary<string, string>> SelectMultiple(string query)
        {
            var lol = new List<CloneableDictionary<string, string>>();

            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                MySqlDataReader dataReader = cmd.ExecuteReader();

                while (dataReader.Read())
                {
                    var listOfStrings = new CloneableDictionary<string, string>();
                    int columns = dataReader.FieldCount;
                    for (int x = 0; x < columns; x++)
                    {
                        listOfStrings.Add(dataReader.GetName(x).ToLower(), dataReader[x].ToString());
                    }

                    lol.Add(listOfStrings.Clone());

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

        //private Dictionary<string, string> arrayToDictionary(string[] key, string[] value)
        //{
        //    var dictionary = new Dictionary<string, string>();

        //    int i = 0;
        //    foreach(string v in key)
        //    {
        //        dictionary.Add(v, value[i]);
        //        i++;
        //    }

        //    return dictionary;
        //}

        //Select single
        public string Select(string query)
        {
            string record = string.Empty;
            int i = 0;

            sb.Clear();

            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                MySqlDataReader dataReader = cmd.ExecuteReader();

                if (dataReader.Read())
                {
                    int x = dataReader.FieldCount;

                    while (i < x)
                    {

                        record = dataReader.GetString(i);
                        sb.Append(record);
                        i++;

                        if (i < x)
                        {
                            sb.Append("|");
                        }
                    }
                }
                dataReader.Close();
                this.CloseConnection();

                return sb.ToString();
            }
            else
            {
                return sb.ToString();
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

                    foreach (var query in transString)
                    {
                        cmd.CommandText = query;
                        cmd.ExecuteNonQuery();
                    }
                    myTransaction.Commit();
                }
                catch (MySqlException ex)
                {
                    try
                    {
                        myTransaction.Rollback();
                    }
                    catch (MySqlException ex1)
                    {
                        MessageBox.Show("Error: {0}", ex1.Message);
                    }
                    MessageBox.Show("Error: {0}", ex.Message);
                }
                finally
                {
                    if (this.OpenConnection() == true)
                    {
                        this.CloseConnection();
                    }

                }

            }
        }
    }
}
