﻿using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace WCFDBService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    public class Service1 : IService1
    {
        private MySqlConnection connection;
        private string server;
        private string database;
        private string uid;
        private string password;
        private string charset;

        public Service1()
        {
            ConnectToDb();
        }

        void ConnectToDb()
        {
            server = "sql7.freemysqlhosting.net";
            database = "sql7112557";
            uid = "sql7112557";
            password = "Fwg8uEkpT1";
            charset = "utf8";
            string connectionString;
            connectionString = "SERVER=" + server + ";" + "DATABASE=" +
            database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";" + "CHARSET=" + charset + ";";

            connection = new MySqlConnection(connectionString);
        }



        //Open connection to database
        private bool OpenConnection()
        {
            try
            {
                connection.Open();
                return true;
            }
            catch (MySqlException ex)
            {
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
                return false;
            }
        }


        public bool UpdateDriverOwner()
        {
            string query = "UPDATE driver_owners SET first_name ='Гергана' WHERE first_name = 'Гергава';";

            //Open connection
            if (this.OpenConnection() == true)
            {
                //MessageBox.Show("Connectnah se");
                //create mysql command
                MySqlCommand cmd = new MySqlCommand();
                //Assign the query using CommandText
                cmd.CommandText = query;
                //Assign the connection using Connection
                cmd.Connection = connection;

                //Execute query
                cmd.ExecuteNonQuery();

                //close connection
                this.CloseConnection();
                return true;
            }
            else
            {
                return false;
            }
        }


        public User GetUserByIdAndPass(string id, string password)
        {
            string query = String.Format("SELECT * FROM users WHERE user_id ={0} AND password = \"{1}\"", id, password);

            string userId;
            string firstName;
            string secondName;
            string lastName;
            string isTrafficPoliceman;
            string pwd;

            //Open connection
            if (this.OpenConnection() == true)
            {
                User usr = new User();
                MySqlDataReader dataReader;
                try
                {
                    //Create Command
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    //Create a data reader and Execute the command                    
                    dataReader = cmd.ExecuteReader();
                    
                    //Read the data and store them in the list
                    while (dataReader.Read())
                    {
                        userId = dataReader["user_id"] + "";
                        firstName = dataReader["first_name"] + "";
                        secondName = dataReader["second_name"] + "";
                        lastName = dataReader["last_name"] + "";
                        isTrafficPoliceman = dataReader["is_traffic_policeman"] + "";
                        pwd = dataReader["password"] + "";

                        usr.IsTrafficPoliceman = isTrafficPoliceman == "1" ? true : false;
                        usr.UserId = long.Parse(userId);
                        usr.FirstName = firstName;
                        usr.SecondName = secondName;
                        usr.LastName = lastName;
                        usr.UserPassword = pwd;
                    }
                    dataReader.Close();
                    return usr;
                }
                catch
                {   //Returning empty user with uninitialized properties (UserId = 0)
                    return new User();
                }
                finally
                {
                    this.CloseConnection();
                }
                           
            }
            else
            {
                return null;
            }

        }

        

        

    }


}