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
using System.Text.RegularExpressions;
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
            catch (MySqlException)
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
            catch (MySqlException)
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

        public int InsertRegistration(Registration reg)
        {
            DriverOwner drOwner = GetDriverOwnerById(reg.DriverOwnerId.ToString());
            if (drOwner == null)
                //ERROR code 2: Cannot connect to db
                return 2;
            if (drOwner.DriverOwnerId == 0)
                //ERROR code 1: Driver owner does not exist 
                return 1;



            string insertQuery = String.Format("CALL add_registration(\"{0}\",{1},"+
                "\"{2}\",\"{3}\",\"{4}\",\"{5}\",{6},{7}," +
                "\"{8}\",\'{9}\',{10},\"{11}\",{12},{13},"+
                "{14},{15},{16},\"{17}\",{18},{19},\'{20}\')",
                reg.RegNum,reg.DriverOwnerId,
                reg.CarManufacturer,reg.CarModel,reg.CarColor, reg.CarType,reg.CarCubage,reg.CarHp,
                reg.CarVin, parseDateSqlFormat(reg.FirstRegDate),reg.HasCivilInsurance,reg.CivilInsurer, parseDateSqlFormat(reg.CivilInsuranceStartDate), parseDateSqlFormat(reg.CivilInsuranceEndDate),
                reg.HasVignette, parseDateSqlFormat(reg.VignetteValidUntil),reg.HasDamageInsurance,reg.DamageInsurer, parseDateSqlFormat(reg.DamageInsuranceStartDate), parseDateSqlFormat(reg.DamageInsuranceEndDate), parseDateSqlFormat(reg.RecentRegDate));

            

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
                {   //Reg is already present with the given credentials
                    return 3;
                }
                finally
                {
                    this.CloseConnection();
                }

            }
            else
            {
                //DB - Not connected
                return 2;
            }


        }



        public string InsertUserAndGetGeneratedId(User usr)
        {
            string query = "CALL add_user_and_get_id(@FirstName,@SecondName,@LastName,@IsTrafficPoliceman,@Password)";
         
            //DB - Connected
            if (this.OpenConnection() == true)
            {
                MySqlDataReader dataReader;
                string id ="";
                try
                {
                    //Create Command
                    MySqlCommand cmd = new MySqlCommand(query, connection);

                    //Declaring query params
                    cmd.Parameters.Add("@FirstName", MySqlDbType.VarChar, 20);
                    cmd.Parameters.Add("@SecondName", MySqlDbType.VarChar, 20);
                    cmd.Parameters.Add("@LastName", MySqlDbType.VarChar,20);
                    cmd.Parameters.Add("@IsTrafficPoliceman", MySqlDbType.Bit, 1);
                    cmd.Parameters.Add("@Password", MySqlDbType.VarChar, 12);
                    //Setting params
                    cmd.Parameters["@FirstName"].Value = usr.FirstName;
                    cmd.Parameters["@SecondName"].Value = usr.SecondName;
                    cmd.Parameters["@LastName"].Value = usr.LastName;
                    cmd.Parameters["@IsTrafficPoliceman"].Value = usr.IsTrafficPoliceman;
                    cmd.Parameters["@Password"].Value = usr.UserPassword;

                    //Create a data reader and Execute the command                    
                    dataReader = cmd.ExecuteReader();

                    //Read the data
                    while (dataReader.Read())
                    {
                        id = dataReader["user_id"] + "";
                    }
                    dataReader.Close();
                    return id;
                }
                catch
                {   
                    return "QUERY_ERROR";
                }
                finally
                {
                    this.CloseConnection();
                }

            }
            else
            {
                //DB - Not connected
                return "DB_NOT_CONNECTED";
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
            ////////Test
            string insertQuery = "CALL add_driverowner(@driver_owner_id,@first_name,@second_name,@last_name," +
                                             "@sex,@nationality,@birth_date,@birth_place,@residence,@tel_num,@email,@remaining_pts,@licence_issue_date," +
                                             "@licence_expiry_date,@licence_issued_by," +
                                             "@a1_acquiry_date,@a1_expiry_date,@a1_restrictions," +
                                             "@a_acquiry_date,@a_expiry_date,@a_restrictions," +
                                            " @b1_acquiry_date,@b1_expiry_date,@b1_restrictions," +
                                            " @b_acquiry_date,@b_expiry_date,@b_restrictions," +
                                             "@c1_acquiry_date,@c1_expiry_date,@c1_restrictions," +
                                             "@c_acquiry_date,@c_expiry_date,@c_restrictions," +
                                             "@d1_acquiry_date,@d1_expiry_date,@d1_restrictions," +
                                             "@d_acquiry_date,@d_expiry_date,@d_restrictions," +
                                             "@be_acquiry_date,@be_expiry_date,@be_restrictions," +
                                             "@c1e_acquiry_date,@c1e_expiry_date,@c1e_restrictions," +
                                             "@ce_acquiry_date,@ce_expiry_date,@ce_restrictions," +
                                             "@d1e_acquiry_date,@d1e_expiry_date,@d1e_restrictions," +
                                             "@de_acquiry_date,@de_expiry_date,@de_restrictions," +
                                             "@ttb_acquiry_date,@ttb_expiry_date,@ttb_restrictions," +
                                             "@ttm_acquiry_date,@ttm_expiry_date,@ttm_restrictions," +
                                             "@tkt_acquiry_date,@tkt_expiry_date,@tkt_restrictions," +
                                             "@m_acquiry_date,@m_expiry_date,@m_restrictions)";
            //DB - Connected
            if (this.OpenConnection() == true)
           {
               try
               {
                   MySqlCommand cmd = new MySqlCommand(insertQuery, connection);

                    ///////////////////////////Test
                    //Declaring query params
                    cmd.Parameters.Add("@driver_owner_id", MySqlDbType.UInt64, 10);
                    cmd.Parameters.Add("@first_name", MySqlDbType.VarChar, 20);
                    cmd.Parameters.Add("@second_name", MySqlDbType.VarChar, 20);
                    cmd.Parameters.Add("@last_name", MySqlDbType.VarChar, 20);
                    cmd.Parameters.Add("@sex", MySqlDbType.Enum);
                    cmd.Parameters.Add("@nationality", MySqlDbType.VarChar, 20);
                    cmd.Parameters.Add("@birth_date", MySqlDbType.Date);
                    cmd.Parameters.Add("@birth_place", MySqlDbType.VarChar, 30);
                    cmd.Parameters.Add("@residence", MySqlDbType.VarChar, 50);
                    cmd.Parameters.Add("@tel_num", MySqlDbType.VarChar, 20);
                    cmd.Parameters.Add("@email", MySqlDbType.VarChar, 20);
                    cmd.Parameters.Add("@remaining_pts", MySqlDbType.UInt16, 2);
                    cmd.Parameters.Add("@licence_issue_date", MySqlDbType.Date);
                    cmd.Parameters.Add("@licence_expiry_date", MySqlDbType.Date);
                    cmd.Parameters.Add("@licence_issued_by", MySqlDbType.VarChar, 30);
                    cmd.Parameters.Add("@a1_acquiry_date", MySqlDbType.Date);
                    cmd.Parameters.Add("@a1_expiry_date", MySqlDbType.Date);
                    cmd.Parameters.Add("@a1_restrictions", MySqlDbType.VarChar,30);
                    cmd.Parameters.Add("@a_acquiry_date", MySqlDbType.Date);
                    cmd.Parameters.Add("@a_expiry_date", MySqlDbType.Date);
                    cmd.Parameters.Add("@a_restrictions", MySqlDbType.VarChar, 30);
                    cmd.Parameters.Add("@b1_acquiry_date", MySqlDbType.Date);
                    cmd.Parameters.Add("@b1_expiry_date", MySqlDbType.Date);
                    cmd.Parameters.Add("@b1_restrictions", MySqlDbType.VarChar, 30);
                    cmd.Parameters.Add("@b_acquiry_date", MySqlDbType.Date);
                    cmd.Parameters.Add("@b_expiry_date", MySqlDbType.Date);
                    cmd.Parameters.Add("@b_restrictions", MySqlDbType.VarChar, 30);
                    cmd.Parameters.Add("@c1_acquiry_date", MySqlDbType.Date);
                    cmd.Parameters.Add("@c1_expiry_date", MySqlDbType.Date);
                    cmd.Parameters.Add("@c1_restrictions", MySqlDbType.VarChar, 30);
                    cmd.Parameters.Add("@c_acquiry_date", MySqlDbType.Date);
                    cmd.Parameters.Add("@c_expiry_date", MySqlDbType.Date);
                    cmd.Parameters.Add("@c_restrictions", MySqlDbType.VarChar, 30);
                    cmd.Parameters.Add("@d1_acquiry_date", MySqlDbType.Date);
                    cmd.Parameters.Add("@d1_expiry_date", MySqlDbType.Date);
                    cmd.Parameters.Add("@d1_restrictions", MySqlDbType.VarChar, 30);
                    cmd.Parameters.Add("@d_acquiry_date", MySqlDbType.Date);
                    cmd.Parameters.Add("@d_expiry_date", MySqlDbType.Date);
                    cmd.Parameters.Add("@d_restrictions", MySqlDbType.VarChar, 30);
                    cmd.Parameters.Add("@be_acquiry_date", MySqlDbType.Date);
                    cmd.Parameters.Add("@be_expiry_date", MySqlDbType.Date);
                    cmd.Parameters.Add("@be_restrictions", MySqlDbType.VarChar, 30);
                    cmd.Parameters.Add("@c1e_acquiry_date", MySqlDbType.Date);
                    cmd.Parameters.Add("@c1e_expiry_date", MySqlDbType.Date);
                    cmd.Parameters.Add("@c1e_restrictions", MySqlDbType.VarChar, 30);
                    cmd.Parameters.Add("@ce_acquiry_date", MySqlDbType.Date);
                    cmd.Parameters.Add("@ce_expiry_date", MySqlDbType.Date);
                    cmd.Parameters.Add("@ce_restrictions", MySqlDbType.VarChar, 30);
                    cmd.Parameters.Add("@d1e_acquiry_date", MySqlDbType.Date);
                    cmd.Parameters.Add("@d1e_expiry_date", MySqlDbType.Date);
                    cmd.Parameters.Add("@d1e_restrictions", MySqlDbType.VarChar, 30);
                    cmd.Parameters.Add("@de_acquiry_date", MySqlDbType.Date);
                    cmd.Parameters.Add("@de_expiry_date", MySqlDbType.Date);
                    cmd.Parameters.Add("@de_restrictions", MySqlDbType.VarChar, 30);
                    cmd.Parameters.Add("@ttb_acquiry_date", MySqlDbType.Date);
                    cmd.Parameters.Add("@ttb_expiry_date", MySqlDbType.Date);
                    cmd.Parameters.Add("@ttb_restrictions", MySqlDbType.VarChar, 30);
                    cmd.Parameters.Add("@ttm_acquiry_date", MySqlDbType.Date);
                    cmd.Parameters.Add("@ttm_expiry_date", MySqlDbType.Date);
                    cmd.Parameters.Add("@ttm_restrictions", MySqlDbType.VarChar, 30);
                    cmd.Parameters.Add("@tkt_acquiry_date", MySqlDbType.Date);
                    cmd.Parameters.Add("@tkt_expiry_date", MySqlDbType.Date);
                    cmd.Parameters.Add("@tkt_restrictions", MySqlDbType.VarChar, 30);
                    cmd.Parameters.Add("@m_acquiry_date", MySqlDbType.Date);
                    cmd.Parameters.Add("@m_expiry_date", MySqlDbType.Date);
                    cmd.Parameters.Add("@m_restrictions", MySqlDbType.VarChar, 30);

                    //Setting params
                    cmd.Parameters["@driver_owner_id"].Value = drOwner.DriverOwnerId;
                    cmd.Parameters["@first_name"].Value = drOwner.FirstName;
                    cmd.Parameters["@second_name"].Value = drOwner.SecondName;
                    cmd.Parameters["@last_name"].Value = drOwner.LastName;
                    cmd.Parameters["@sex"].Value = parseSexSqlFormat(drOwner.Sex);
                    cmd.Parameters["@nationality"].Value = drOwner.Nationality;
                    cmd.Parameters["@birth_date"].Value = drOwner.BirthDate;
                    cmd.Parameters["@birth_place"].Value = drOwner.BirthPlace;
                    cmd.Parameters["@residence"].Value = drOwner.Residence;
                    cmd.Parameters["@tel_num"].Value = drOwner.TelNum;
                    cmd.Parameters["@email"].Value = drOwner.Email;
                    cmd.Parameters["@remaining_pts"].Value = drOwner.RemainingPts;
                    cmd.Parameters["@licence_issue_date"].Value = drOwner.LicenceIssueDate;
                    cmd.Parameters["@licence_expiry_date"].Value = drOwner.LicenceExpiryDate;
                    cmd.Parameters["@licence_issued_by"].Value = drOwner.LicenceIssuedBy;
                    cmd.Parameters["@a1_acquiry_date"].Value = drOwner.Categories.a1AcquiryDate;
                    cmd.Parameters["@a1_expiry_date"].Value = drOwner.Categories.a1ExpiryDate;
                    cmd.Parameters["@a1_restrictions"].Value = parseSqlRestrictions(drOwner.Categories.a1Restrictions);
                    cmd.Parameters["@a_acquiry_date"].Value = drOwner.Categories.aAcquiryDate;
                    cmd.Parameters["@a_expiry_date"].Value = drOwner.Categories.aExpiryDate;
                    cmd.Parameters["@a_restrictions"].Value = parseSqlRestrictions(drOwner.Categories.aRestrictions);
                    cmd.Parameters["@b1_acquiry_date"].Value = drOwner.Categories.b1AcquiryDate;
                    cmd.Parameters["@b1_expiry_date"].Value = drOwner.Categories.b1ExpiryDate;
                    cmd.Parameters["@b1_restrictions"].Value = parseSqlRestrictions(drOwner.Categories.b1Restrictions);
                    cmd.Parameters["@b_acquiry_date"].Value = drOwner.Categories.bAcquiryDate;
                    cmd.Parameters["@b_expiry_date"].Value = drOwner.Categories.bExpiryDate;
                    cmd.Parameters["@b_restrictions"].Value = parseSqlRestrictions(drOwner.Categories.bRestrictions);
                    cmd.Parameters["@c1_acquiry_date"].Value = drOwner.Categories.c1AcquiryDate;
                    cmd.Parameters["@c1_expiry_date"].Value = drOwner.Categories.c1ExpiryDate;
                    cmd.Parameters["@c1_restrictions"].Value = parseSqlRestrictions(drOwner.Categories.c1Restrictions);
                    cmd.Parameters["@c_acquiry_date"].Value = drOwner.Categories.cAcquiryDate;
                    cmd.Parameters["@c_expiry_date"].Value = drOwner.Categories.cExpiryDate;
                    cmd.Parameters["@c_restrictions"].Value = parseSqlRestrictions(drOwner.Categories.cRestrictions);
                    cmd.Parameters["@d1_acquiry_date"].Value = drOwner.Categories.d1AcquiryDate;
                    cmd.Parameters["@d1_expiry_date"].Value = drOwner.Categories.d1ExpiryDate;
                    cmd.Parameters["@d1_restrictions"].Value = parseSqlRestrictions(drOwner.Categories.d1Restrictions);
                    cmd.Parameters["@d_acquiry_date"].Value = drOwner.Categories.dAcquiryDate;
                    cmd.Parameters["@d_expiry_date"].Value = drOwner.Categories.dExpiryDate;
                    cmd.Parameters["@d_restrictions"].Value = parseSqlRestrictions(drOwner.Categories.dRestrictions);
                    cmd.Parameters["@be_acquiry_date"].Value = drOwner.Categories.beAcquiryDate;
                    cmd.Parameters["@be_expiry_date"].Value = drOwner.Categories.beExpiryDate;
                    cmd.Parameters["@be_restrictions"].Value = parseSqlRestrictions(drOwner.Categories.beRestrictions);
                    cmd.Parameters["@c1e_acquiry_date"].Value = drOwner.Categories.c1eAcquiryDate;
                    cmd.Parameters["@c1e_expiry_date"].Value = drOwner.Categories.c1eExpiryDate;
                    cmd.Parameters["@c1e_restrictions"].Value = parseSqlRestrictions(drOwner.Categories.c1eRestrictions);
                    cmd.Parameters["@ce_acquiry_date"].Value = drOwner.Categories.ceAcquiryDate;
                    cmd.Parameters["@ce_expiry_date"].Value = drOwner.Categories.ceExpiryDate;
                    cmd.Parameters["@ce_restrictions"].Value = parseSqlRestrictions(drOwner.Categories.ceRestrictions);
                    cmd.Parameters["@d1e_acquiry_date"].Value = drOwner.Categories.d1eAcquiryDate;
                    cmd.Parameters["@d1e_expiry_date"].Value = drOwner.Categories.d1eExpiryDate;
                    cmd.Parameters["@d1e_restrictions"].Value = parseSqlRestrictions(drOwner.Categories.d1eRestrictions);
                    cmd.Parameters["@de_acquiry_date"].Value = drOwner.Categories.deAcquiryDate;
                    cmd.Parameters["@de_expiry_date"].Value = drOwner.Categories.deExpiryDate;
                    cmd.Parameters["@de_restrictions"].Value = parseSqlRestrictions(drOwner.Categories.deRestrictions);
                    cmd.Parameters["@ttb_acquiry_date"].Value = drOwner.Categories.ttbAcquiryDate;
                    cmd.Parameters["@ttb_expiry_date"].Value = drOwner.Categories.ttbExpiryDate;
                    cmd.Parameters["@ttb_restrictions"].Value = parseSqlRestrictions(drOwner.Categories.ttbRestrictions);
                    cmd.Parameters["@ttm_acquiry_date"].Value = drOwner.Categories.ttmAcquiryDate;
                    cmd.Parameters["@ttm_expiry_date"].Value = drOwner.Categories.ttmExpiryDate;
                    cmd.Parameters["@ttm_restrictions"].Value = parseSqlRestrictions(drOwner.Categories.ttmRestrictions);
                    cmd.Parameters["@tkt_acquiry_date"].Value = drOwner.Categories.tktAcquiryDate;
                    cmd.Parameters["@tkt_expiry_date"].Value = drOwner.Categories.tktExpiryDate;
                    cmd.Parameters["@tkt_restrictions"].Value = parseSqlRestrictions(drOwner.Categories.tktRestrictions);
                    cmd.Parameters["@m_acquiry_date"].Value = drOwner.Categories.mAcquiryDate;
                    cmd.Parameters["@m_expiry_date"].Value = drOwner.Categories.mExpiryDate;
                    cmd.Parameters["@m_restrictions"].Value = parseSqlRestrictions(drOwner.Categories.mRestrictions);

                    cmd.ExecuteNonQuery();
                   return 0;
               }
               catch
               { 

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
                      drOwner.TelNum = dataReader["tel_num"] + "";
                      drOwner.Email = dataReader["email"] + "";
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
                      penalty.DriverOwnerId = Convert.ToInt64(dataReader["driver_owner_id"]);
                      penalty.IssuedDateTime = Convert.ToDateTime(dataReader["date_time_issued"]);
                      penalty.HappenedDateTime = Convert.ToDateTime(dataReader["penalty_date_time"]);
                      penalty.Location = dataReader["location"].ToString();
                      penalty.Latitude = Convert.ToDouble(dataReader["latitude"]);
                      penalty.Longtitude = Convert.ToDouble(dataReader["longtitude"]);
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

        public List<string> getAvailableCarTypes()
        {
            string query = String.Format("CALL get_available_car_types");

            List<string> carTypes = new List<string>();

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
                    string types = null;
                    //Read the data and store them in the list
                    while (dataReader.Read())
                    {
                        types = dataReader["COLUMN_TYPE"] + "";
                        
                    }
                    dataReader.Close();
                    //Formating
                    foreach (Match match in Regex.Matches(types, "\'([^\']*)\'"))
                        carTypes.Add(match.ToString().Trim('\''));

                    return carTypes;

                }
                catch
                {   
                    return new List<string>();
                }
                finally
                {
                    this.CloseConnection();
                }

            }
            else
            {
                return carTypes;
            }

            
        }

        public Registration getRegByRegNum(string regNum)
        {
            string query = String.Format("CALL get_reg_by_regnum(\"{0}\")", regNum);
            //Open connection
            if (this.OpenConnection() == true)
            {
                Registration reg = new Registration();
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
                        reg.RegNum = dataReader["reg_num"] + "";
                        reg.DriverOwnerId = Convert.ToInt64(dataReader["driver_owner_id"]);
                        reg.CarManufacturer = dataReader["car_manufacturer"] + "";
                        reg.CarModel = dataReader["car_model"] + "";
                        reg.CarColor = dataReader["car_color"] + "";
                        reg.CarType = dataReader["car_type"] + "";
                        reg.CarCubage = Convert.ToInt32(dataReader["car_cubage"]);
                        reg.CarHp = Convert.ToInt32(dataReader["car_hp"]);
                        reg.CarVin = dataReader["car_vin"] + "";
                        reg.FirstRegDate = Convert.ToDateTime(dataReader["first_reg_date"]) ;
                        reg.RecentRegDate = Convert.ToDateTime(dataReader["recent_certificate_reg_date"]);

                        reg.HasCivilInsurance = Convert.ToBoolean(dataReader["civil_insurance"]);
                        reg.CivilInsurer =dataReader["civil_insurer"] +"";
                        reg.CivilInsuranceStartDate = dataReader["civil_insurance_start_date"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dataReader["civil_insurance_start_date"]);
                        reg.CivilInsuranceEndDate = dataReader["civil_insurance_end_date"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dataReader["civil_insurance_end_date"]);

                        reg.HasVignette = Convert.ToBoolean(dataReader["has_vignette"]);
                        reg.VignetteValidUntil = dataReader["vignette_valid_until"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dataReader["vignette_valid_until"]);

                        reg.HasDamageInsurance = Convert.ToBoolean(dataReader["damage_insurance"]);
                        reg.DamageInsurer = dataReader["damage_insurer"] + "";
                        reg.DamageInsuranceStartDate = dataReader["damage_insurance_start_date"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dataReader["damage_insurance_start_date"]);
                        reg.DamageInsuranceEndDate = dataReader["damage_insurance_end_date"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dataReader["damage_insurance_end_date"]);


                    }
                    dataReader.Close();
                    return reg;
                }
                catch
                {   //Returning empty reg with uninitialized properties (regNum = null)
                    return new Registration();
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


        public int removePenalty(Penalty pen)
      {
          string query = String.Format("CALL remove_penalty_by_id({0})", pen.PenaltyId);


          //Open connection
          if (this.OpenConnection() == true)
          {
              try
              {
                  //Create Command
                  MySqlCommand cmd = new MySqlCommand(query, connection);
                  cmd.ExecuteNonQuery();
                  return 0;
                  //Create a data reader and Execute the command                    
              }
              catch
              {   //Returning empty user with uninitialized properties (UserId = 0)
                  return 2;
              }
              finally
              {
                  this.CloseConnection();
              }

          }
          else
          {
              //DB NOT CONNECTED
              return 1;
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
                return String.Format("{0}", rest);
            }
      }

       

        

        

    }


}
