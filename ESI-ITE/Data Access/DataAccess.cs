﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

using MySql.Data.MySqlClient;
using ESI_ITE.Model;
using System.Threading;

namespace ESI_ITE.Data_Access
{
    public class DataAccess
    {
        public event EventHandler ItemPosted;

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
            password = "root";

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

            if (this.OpenConnection() == true)
                this.CloseConnection();

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

            if (this.OpenConnection() == true)
                this.CloseConnection();
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

            if (this.OpenConnection() == true)
                this.CloseConnection();
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
                if (this.OpenConnection() == true)
                    this.CloseConnection();

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

                    do
                    {
                        record = dataReader[i].ToString();
                        sb.Append(record);
                        i++;

                        if (i < x)
                        {
                            sb.Append("|");
                        }

                    } while (dataReader.Read() && i < x);
                }
                dataReader.Close();
                this.CloseConnection();

                return sb.ToString();
            }
            else
            {
                if (this.OpenConnection() == true)
                    this.CloseConnection();

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
                if (this.OpenConnection() == true)
                    this.CloseConnection();

                return Count;
            }
        }

        //Transaction 
        public void RunMySqlTransaction(List<string> transString, IProgress<ProgressReportModel> progress, ProgressReportModel progressReport)
        {
            float currentStep = 0;
            float percentComplete = 0;
            float queryCount = transString.Count();

            if (progressReport != null)
            {
                progressReport.TotalStep = transString.Count();
            }

            if (this.OpenConnection() == true)
            {
                //MySqlTransaction myTransaction = connection.BeginTransaction();
                MySqlTransaction myTransaction = connection.BeginTransaction(System.Data.IsolationLevel.RepeatableRead);
                try
                {
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.Connection = connection;
                    cmd.Transaction = myTransaction;

                    foreach (var query in transString)
                    {
                        cmd.CommandText = query;
                        cmd.ExecuteNonQuery();

                        if (progress != null)
                        {
                            progressReport.CurrentStep = currentStep++;
                            progressReport.PercentComplete = currentStep / queryCount * 100;

                            progress.Report(progressReport);
                        }
                    }
                    myTransaction.Commit();
                }
                catch (MySqlException ex)
                {
                    MessageBox.Show("COMMIT ERROR : " + ex.Message, "Posting Message");
                    try
                    {
                        myTransaction.Rollback();
                    }
                    catch (MySqlException ex1)
                    {
                        MessageBox.Show("ROLLBACK ERROR : " + ex1.Message, "Posting Message");
                    }

                    MyGlobals.hasTransactionError = true;
                }
                finally
                {
                    this.CloseConnection();
                }

            }

            if (this.OpenConnection() == true)
                this.CloseConnection();

        }

        public void Test(IProgress<ProgressReportModel> progress, ProgressReportModel progressReport)
        {
            float totalItems = 100;

            for (float x = 1; x <= totalItems; x++)
            {
                Thread.Sleep(100);
                progressReport.CurrentStep = x;
                progressReport.PercentComplete = (x / totalItems) * 100;

                progress.Report(progressReport);
            }
        }
    }
}
