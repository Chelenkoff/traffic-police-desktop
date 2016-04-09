using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
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


        public int UpdateUser(User user)
        {

            string updateQuery = String.Format(("call update_user({0},\"{1}\",\"{2}\",\"{3}\",{4},\"{5}\")"), user.UserId, user.FirstName, user.SecondName, user.LastName, user.IsTrafficPoliceman, user.UserPassword);


            //Open connection
            if (this.OpenConnection() == true)
            {
                //create mysql command
                try
                {
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.CommandText = updateQuery;
                    cmd.Connection = connection;

                    cmd.ExecuteNonQuery();
                    //DB-OK User - Updated
                    return 0;
                }
                catch (Exception)
                {
                    //DB-OK User -Not updated
                    return 2;
                }
                finally
                {
                    this.CloseConnection();
                }

            
            }
            else
            {
                //DB - NOT CONNECTED
                return 1;
            }
        }


        public User GetUserByIdAndPass(string id, string password)
        {

            string query = String.Format("call get_user_by_id_and_pass({0},\"{1}\")", id, password);


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



        public int InsertUser(User usr)
        {
            string insertQuery = String.Format(("INSERT INTO users (user_id,first_name,second_name,last_name,is_traffic_policeman,password)"+
                                               " VALUES({0},\"{1}\",\"{2}\",\"{3}\",{4},\"{5}\")"),
                                                usr.UserId,usr.FirstName,usr.SecondName,usr.LastName,usr.IsTrafficPoliceman,usr.UserPassword);
            //DB - Connected
            if (this.OpenConnection() == true)
            {
                try
                {
                    MySqlCommand cmd = new MySqlCommand(insertQuery, connection);                 
                    cmd.ExecuteNonQuery();
                    return 0;
                }
                catch
                {   //User is already present with the given credentials
                    return 2;
                }
                finally
                {
                    this.CloseConnection();
                }

            }
            else
            {
                //DB - Not connected
                return 1;
            }                              

        }

       public User GetReadOnlyUserById(string id)
        {
            string query = String.Format("SELECT * FROM users WHERE user_id ={0} ", id);

            string userId;
            string firstName;
            string secondName;
            string lastName;
            string isTrafficPoliceman;

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

                        usr.IsTrafficPoliceman = isTrafficPoliceman == "1" ? true : false;
                        usr.UserId = long.Parse(userId);
                        usr.FirstName = firstName;
                        usr.SecondName = secondName;
                        usr.LastName = lastName;
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

       public int InsertCat(Categories categ)
       {
           return 1;
       }


      public int RegisterDriverOwner(DriverOwner drOwner)
       {
           //0 - driver_owner_id            
           //1 - first_name
           //2 - second_name
           //3 - last_name
           //4 - sex
           //5 - nationality
           //6 - birth_date
           //7 - birth_place
           //8 - residence
           //9 - licence_num
           //10 - remaining_pts
           //11 - licence_issue_date
           //12 - licence_expiry_date
           //13 - licence_issued_by



           //string dtTest = drOwner.Categories.a1AcquiryDate;
           string insertQuery = String.Format(("INSERT INTO driver_owners (driver_owner_id,first_name,second_name,last_name," +
                                             "sex,nationality,birth_date,birth_place,residence,remaining_pts,licence_issue_date,"+
                                             "licence_expiry_date,licence_issued_by," +
                                             "a1_acquiry_date,a1_expiry_date,a1_restrictions,"+
                                             "a_acquiry_date,a_expiry_date,a_restrictions,"+
                                             "b1_acquiry_date,b1_expiry_date,b1_restrictions,"+
                                             "b_acquiry_date,b_expiry_date,b_restrictions,"+
                                             "c1_acquiry_date,c1_expiry_date,c1_restrictions,"+
                                             "c_acquiry_date,c_expiry_date,c_restrictions,"+
                                             "d1_acquiry_date,d1_expiry_date,d1_restrictions,"+
                                             "d_acquiry_date,d_expiry_date,d_restrictions,"+
                                             "be_acquiry_date,be_expiry_date,be_restrictions,"+
                                             "c1e_acquiry_date,c1e_expiry_date,c1e_restrictions,"+
                                             "ce_acquiry_date,ce_expiry_date,ce_restrictions,"+
                                             "d1e_acquiry_date,d1e_expiry_date,d1e_restrictions,"+
                                             "de_acquiry_date,de_expiry_date,de_restrictions,"+
                                             "ttb_acquiry_date,ttb_expiry_date,ttb_restrictions,"+
                                             "ttm_acquiry_date,ttm_expiry_date,ttm_restrictions,"+
                                             "tkt_acquiry_date,tkt_expiry_date,tkt_restrictions,"+
                                             "m_acquiry_date,m_expiry_date,m_restrictions)"+
                                            
                                             "VALUES({0},\"{1}\",\"{2}\",\"{3}\"," +
                                             "\"{4}\",\"{5}\",\'{6}\',\"{7}\",\"{8}\",{9},\'{10}\', "+
                                             "\'{11}\',\"{12}\", "+
                                             //Categories
                                             "{13},{14},{15}, "+    //a1
                                             "{16},{17},{18}, "+    //a
                                             "{19},{20},{21}, "+    //b1
                                             "{22},{23},{24}, "+    //b
                                             "{25},{26},{27}, "+    //c1
                                             "{28},{29},{30}, "+    //c
                                             "{31},{32},{33}, "+    //d1
                                             "{34},{35},{36}, "+    //d
                                             "{37},{38},{39}, "+    //be
                                             "{40},{41},{42}, "+    //c1e
                                             "{43},{44},{45}, "+    //ce
                                             "{46},{47},{48}, "+    //d1e
                                             "{49},{50},{51}, "+    //de
                                             "{52},{53},{54}, "+    //ttb
                                             "{55},{56},{57}, "+    //ttm
                                             "{58},{59},{60}, "+    //tkt
                                             "{61},{62},{63} )"),    //m
                                     drOwner.DriverOwnerId, drOwner.FirstName, drOwner.SecondName, drOwner.LastName,
                                     parseSexSqlFormat(drOwner.Sex), drOwner.Nationality, parseDateSqlFormat(drOwner.BirthDate), drOwner.BirthPlace, drOwner.Residence, drOwner.RemainingPts, parseDateSqlFormat(drOwner.LicenceIssueDate),
                                     parseDateSqlFormat(drOwner.LicenceExpiryDate), drOwner.LicenceIssuedBy,
                                     parseDateSqlFormat(drOwner.Categories.a1AcquiryDate), parseDateSqlFormat(drOwner.Categories.a1ExpiryDate),parseSqlRestrictions(drOwner.Categories.a1Restrictions),
                                      parseDateSqlFormat(drOwner.Categories.aAcquiryDate), parseDateSqlFormat(drOwner.Categories.aExpiryDate), parseSqlRestrictions(drOwner.Categories.aRestrictions),
                                     parseDateSqlFormat(drOwner.Categories.b1AcquiryDate), parseDateSqlFormat(drOwner.Categories.b1ExpiryDate), parseSqlRestrictions(drOwner.Categories.b1Restrictions),
                                     parseDateSqlFormat(drOwner.Categories.bAcquiryDate), parseDateSqlFormat(drOwner.Categories.bExpiryDate), parseSqlRestrictions(drOwner.Categories.bRestrictions),
                                     parseDateSqlFormat(drOwner.Categories.c1AcquiryDate), parseDateSqlFormat(drOwner.Categories.c1ExpiryDate), parseSqlRestrictions(drOwner.Categories.c1Restrictions),
                                     parseDateSqlFormat(drOwner.Categories.cAcquiryDate), parseDateSqlFormat(drOwner.Categories.cExpiryDate), parseSqlRestrictions(drOwner.Categories.cRestrictions),
                                    parseDateSqlFormat(drOwner.Categories.d1AcquiryDate), parseDateSqlFormat(drOwner.Categories.d1ExpiryDate),parseSqlRestrictions(drOwner.Categories.d1Restrictions),
                                    parseDateSqlFormat(drOwner.Categories.dAcquiryDate), parseDateSqlFormat(drOwner.Categories.dExpiryDate), parseSqlRestrictions(drOwner.Categories.dRestrictions),
                                    parseDateSqlFormat(drOwner.Categories.beAcquiryDate), parseDateSqlFormat(drOwner.Categories.beExpiryDate), parseSqlRestrictions(drOwner.Categories.beRestrictions),
                                     parseDateSqlFormat(drOwner.Categories.c1eAcquiryDate), parseDateSqlFormat(drOwner.Categories.c1eExpiryDate), parseSqlRestrictions(drOwner.Categories.c1eRestrictions),
                                     parseDateSqlFormat(drOwner.Categories.ceAcquiryDate), parseDateSqlFormat(drOwner.Categories.ceExpiryDate), parseSqlRestrictions(drOwner.Categories.ceRestrictions),
                                     parseDateSqlFormat(drOwner.Categories.d1eAcquiryDate), parseDateSqlFormat(drOwner.Categories.d1eExpiryDate), parseSqlRestrictions(drOwner.Categories.d1eRestrictions),
                                     parseDateSqlFormat(drOwner.Categories.deAcquiryDate), parseDateSqlFormat(drOwner.Categories.deExpiryDate), parseSqlRestrictions(drOwner.Categories.deRestrictions),
                                    parseDateSqlFormat(drOwner.Categories.ttbAcquiryDate), parseDateSqlFormat(drOwner.Categories.ttbExpiryDate), parseSqlRestrictions(drOwner.Categories.ttbRestrictions),
                                     parseDateSqlFormat(drOwner.Categories.ttmAcquiryDate), parseDateSqlFormat(drOwner.Categories.ttmExpiryDate), parseSqlRestrictions(drOwner.Categories.ttmRestrictions),
                                     parseDateSqlFormat(drOwner.Categories.tktAcquiryDate), parseDateSqlFormat(drOwner.Categories.tktExpiryDate), parseSqlRestrictions(drOwner.Categories.tktRestrictions),
                                     parseDateSqlFormat(drOwner.Categories.mAcquiryDate), parseDateSqlFormat(drOwner.Categories.mExpiryDate), parseSqlRestrictions(drOwner.Categories.mRestrictions)
                                     );
           //DB - Connected
           if (this.OpenConnection() == true)
           {
               try
               {
                   MySqlCommand cmd = new MySqlCommand(insertQuery, connection);
                   cmd.ExecuteNonQuery();
                   return 0;
               }
               catch
               {   //User is already present with the given credentials

                   return 2;
               }
               finally
               {
                   this.CloseConnection();
               }

           }
           else
           {
               //DB - Not connected
               return 1;
           }                       
       }


      public DriverOwner  GetDriverOwnerById(string id)
      {
          string query = String.Format("call get_driverowner_data_by_id({0})", id);



          //Open connection
          if (this.OpenConnection() == true)
          {
             
              MySqlDataReader dataReader;
              try
              {
                  //Create Command
                  MySqlCommand cmd = new MySqlCommand(query, connection);
                  //Create a data reader and Execute the command                    
                  dataReader = cmd.ExecuteReader();

                  //Initializing empty user
                  DriverOwner drOwner = new DriverOwner();
                  drOwner.Categories = new Categories();
                  drOwner.Penalties = new List<Penalty>();

                  //Read the data and store them in the list

                  while (dataReader.Read())
                  {
                      drOwner.DriverOwnerId =Convert.ToInt64(dataReader["driver_owner_id"]);
                      drOwner.FirstName = dataReader["first_name"] + "";
                      drOwner.SecondName = dataReader["second_name"] + "";
                      drOwner.LastName = dataReader["last_name"] + "";
                      drOwner.Sex = ((dataReader["sex"] + "") == "М") ? SexEnum.Man : SexEnum.Woman;
                      drOwner.Nationality = dataReader["nationality"] + "";
                      drOwner.BirthDate = Convert.ToDateTime(dataReader["birth_date"]);
                      drOwner.BirthPlace = dataReader["birth_place"] + "";
                      drOwner.Residence = dataReader["residence"] + "";
                      drOwner.RemainingPts =Convert.ToByte(dataReader["remaining_pts"]);
                      drOwner.LicenceIssueDate =Convert.ToDateTime(dataReader["licence_issue_date"]);
                      drOwner.LicenceExpiryDate =Convert.ToDateTime(dataReader["licence_expiry_date"]);
                      drOwner.LicenceIssuedBy = dataReader["licence_issued_by"] + "";

                      //Constructing 'Categories'


                      drOwner.Categories.a1AcquiryDate = dataReader["a1_acquiry_date"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dataReader["a1_acquiry_date"]);
                      drOwner.Categories.a1ExpiryDate = dataReader["a1_expiry_date"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dataReader["a1_expiry_date"]);
                      drOwner.Categories.a1Restrictions = dataReader["a1_restrictions"].ToString();

                      drOwner.Categories.aAcquiryDate = dataReader["a_acquiry_date"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dataReader["a_acquiry_date"]);
                      drOwner.Categories.aExpiryDate = dataReader["a_expiry_date"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dataReader["a_expiry_date"]);
                      drOwner.Categories.aRestrictions = dataReader["a_restrictions"].ToString();

                      drOwner.Categories.b1AcquiryDate = dataReader["b1_acquiry_date"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dataReader["b1_acquiry_date"]);
                      drOwner.Categories.b1ExpiryDate = dataReader["b1_expiry_date"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dataReader["b1_expiry_date"]);
                      drOwner.Categories.b1Restrictions = dataReader["b1_restrictions"].ToString();

                      drOwner.Categories.bAcquiryDate = dataReader["b_acquiry_date"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dataReader["b_acquiry_date"]);
                      drOwner.Categories.bExpiryDate = dataReader["b_expiry_date"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dataReader["b_expiry_date"]);
                      drOwner.Categories.bRestrictions = dataReader["b_restrictions"].ToString();

                      drOwner.Categories.c1AcquiryDate = dataReader["c1_acquiry_date"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dataReader["c1_acquiry_date"]);
                      drOwner.Categories.c1ExpiryDate = dataReader["c1_expiry_date"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dataReader["c1_expiry_date"]);
                      drOwner.Categories.c1Restrictions = dataReader["c1_restrictions"].ToString();

                      drOwner.Categories.cAcquiryDate = dataReader["c_acquiry_date"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dataReader["c_acquiry_date"]);
                      drOwner.Categories.cExpiryDate = dataReader["c_expiry_date"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dataReader["c_expiry_date"]);
                      drOwner.Categories.cRestrictions = dataReader["c_restrictions"].ToString();

                      drOwner.Categories.d1AcquiryDate = dataReader["d1_acquiry_date"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dataReader["d1_acquiry_date"]);
                      drOwner.Categories.d1ExpiryDate = dataReader["d1_expiry_date"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dataReader["d1_expiry_date"]);
                      drOwner.Categories.d1Restrictions = dataReader["d1_restrictions"].ToString();

                      drOwner.Categories.dAcquiryDate = dataReader["d_acquiry_date"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dataReader["d_acquiry_date"]);
                      drOwner.Categories.dExpiryDate = dataReader["d_expiry_date"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dataReader["d_expiry_date"]);
                      drOwner.Categories.dRestrictions = dataReader["d_restrictions"].ToString();

                      drOwner.Categories.beAcquiryDate = dataReader["be_acquiry_date"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dataReader["be_acquiry_date"]);
                      drOwner.Categories.beExpiryDate = dataReader["be_expiry_date"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dataReader["be_expiry_date"]);
                      drOwner.Categories.beRestrictions = dataReader["be_restrictions"].ToString();

                      drOwner.Categories.c1eAcquiryDate = dataReader["c1e_acquiry_date"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dataReader["c1e_acquiry_date"]);
                      drOwner.Categories.c1eExpiryDate = dataReader["c1e_expiry_date"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dataReader["c1e_expiry_date"]);
                      drOwner.Categories.c1eRestrictions = dataReader["c1e_restrictions"].ToString();

                      drOwner.Categories.ceAcquiryDate = dataReader["ce_acquiry_date"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dataReader["ce_acquiry_date"]);
                      drOwner.Categories.ceAcquiryDate = dataReader["ce_expiry_date"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dataReader["ce_expiry_date"]);
                      drOwner.Categories.ceRestrictions = dataReader["ce_restrictions"].ToString();

                      drOwner.Categories.d1eAcquiryDate = dataReader["d1e_acquiry_date"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dataReader["d1e_acquiry_date"]);
                      drOwner.Categories.d1eExpiryDate = dataReader["d1e_expiry_date"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dataReader["d1e_expiry_date"]);
                      drOwner.Categories.d1eRestrictions = dataReader["d1e_restrictions"].ToString();

                      drOwner.Categories.deAcquiryDate = dataReader["de_acquiry_date"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dataReader["de_acquiry_date"]);
                      drOwner.Categories.deExpiryDate = dataReader["de_expiry_date"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dataReader["de_expiry_date"]);
                      drOwner.Categories.deRestrictions = dataReader["de_restrictions"].ToString();

                      drOwner.Categories.ttbAcquiryDate = dataReader["ttb_acquiry_date"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dataReader["ttb_acquiry_date"]);
                      drOwner.Categories.ttbExpiryDate = dataReader["ttb_expiry_date"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dataReader["ttb_expiry_date"]);
                      drOwner.Categories.ttbRestrictions = dataReader["ttb_restrictions"].ToString();

                      drOwner.Categories.ttmAcquiryDate = dataReader["ttm_acquiry_date"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dataReader["ttm_acquiry_date"]);
                      drOwner.Categories.ttmExpiryDate = dataReader["ttm_expiry_date"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dataReader["ttm_expiry_date"]);
                      drOwner.Categories.ttmRestrictions = dataReader["ttm_restrictions"].ToString();

                      drOwner.Categories.tktAcquiryDate = dataReader["tkt_acquiry_date"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dataReader["tkt_acquiry_date"]);
                      drOwner.Categories.tktExpiryDate = dataReader["tkt_expiry_date"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dataReader["tkt_expiry_date"]);
                      drOwner.Categories.tktRestrictions = dataReader["tkt_restrictions"].ToString();

                      drOwner.Categories.mAcquiryDate = dataReader["m_acquiry_date"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dataReader["m_acquiry_date"]);
                      drOwner.Categories.mExpiryDate = dataReader["m_expiry_date"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dataReader["m_expiry_date"]);
                      drOwner.Categories.mRestrictions = dataReader["m_restrictions"].ToString();

                  }
                  //Closing current reader and prepare for the new one for the nex query
                  dataReader.Close();

                  query = String.Format("call get_driverowner_penalties_info({0})", id);
                  cmd = new MySqlCommand(query, connection);
                  //Create a data reader and Execute the command                    
                  dataReader = cmd.ExecuteReader();
                  Penalty penalty;
                  while (dataReader.Read())
                  {
                      penalty = new Penalty();
                      //Fetch penalty info
                      penalty.PenaltyId = Convert.ToUInt64(dataReader["penalty_id"]);
                      penalty.IssuerId = Convert.ToInt64(dataReader["user_id"]);
                      penalty.IssuedDateTime = Convert.ToDateTime(dataReader["date_time_issued"]);
                      penalty.HappenedDateTime = Convert.ToDateTime(dataReader["penalty_date_time"]);
                      penalty.Location = dataReader["location"].ToString();
                      penalty.Description = dataReader["description"].ToString();
                      penalty.Disagreement = dataReader["disagreement"].ToString();

                      drOwner.Penalties.Add(penalty);
                  }


                  dataReader.Close();
                  return drOwner;
              }
              catch
              {   //Returning empty user with uninitialized properties (UserId = 0)
                  return new DriverOwner();
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

      private string parseDateSqlFormat(DateTime dt)
      {
          return dt.ToString("yyyy-MM-dd");
      }
      private string parseDateSqlFormat(DateTime? dt)
      {
          return (dt != null ? String.Format("\"{0}\"",dt.Value.ToString("yyyy-MM-dd")) : "NULL");
      }

      private string parseSexSqlFormat(SexEnum sex)
      {
          return (sex == SexEnum.Man ? 'М' : 'Ж').ToString();
      }

        private string parseSqlRestrictions(string rest)
      {
            if(String.IsNullOrWhiteSpace(rest))
            {
                return "NULL";
            }
            else
            {
                return String.Format("\"{0}\"", rest);
            }
      }

        

        

    }


}
