using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using BusinessModel.Interfaces.Generics;
using BusinessModel.ObjectModel;
using BusinessModel.ObjectModel.DashboardModel;
using model = BusinessModel.ObjectModel.LoginReturnInformationObjectModel;

namespace DataAccess
{
    public class LoginReturnInformationDataAccess : IPostDatabaseData<model>
    {
        private readonly LoginInformationFromUserObjectModel _loginInfo;
        public LoginReturnInformationDataAccess(LoginInformationFromUserObjectModel _loginInfo)
        {
            this._loginInfo = _loginInfo;
        }

        public model PostDatabaseData()
        {
            string connString = ConfigurationManager.ConnectionStrings["X-MEDIAWEB_DB"].ConnectionString;
            model data = new model();

            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "[customer].[spGetUserDataByLoginInfomation]";
                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlParameter EmailAddress = new SqlParameter("@EmailAddress", SqlDbType.NVarChar);
                    cmd.Parameters.Add(EmailAddress);
                    cmd.Parameters["@EmailAddress"].Value = _loginInfo.EmailAddress;

                    SqlParameter IStillLoveYou = new SqlParameter("@IStillLoveYou", SqlDbType.Char);
                    cmd.Parameters.Add(IStillLoveYou);
                    cmd.Parameters["@IStillLoveYou"].Value = _loginInfo.IStillLoveYou;


                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        //Check for errors and if true, retreive the error message!
                        if (reader.GetSchemaTable().Rows[0].ItemArray[0].ToString() == "ErrorMessage")
                        {
                            reader.Read();
                            data.hasError = true;
                            data.ErrorMessage = reader["ErrorMessage"].ToString();
                        }
                        //Retrieve data if no error happenned!
                        else
                        {
                            if (reader.GetSchemaTable().Rows[0].ItemArray[0].ToString() == "ID")
                            {
                                if (reader.HasRows)
                                {

                                    reader.Read();
                                    PersonalInformationObjectModel persInfoData = new PersonalInformationObjectModel()
                                    {
                                        UserID = Convert.ToInt32(reader["ID"]),
                                        FirstName = reader["FirstName"].ToString(),
                                        LastName = reader["LastName"].ToString(),
                                        PhotoImageFileName = reader["PhotoImageFilename"].ToString()
                                    };
                                    data.PersonalInfo = persInfoData;

                                }

                            }
                            else
                            {
                                if (reader.HasRows)
                                {
                                    reader.Read();
                                    LoginStatusAndResultsObjectModel logStatCodeData = new LoginStatusAndResultsObjectModel()
                                    {
                                        LoginStatusNumberCode = Convert.ToInt32(reader["LoginStatusNumberCode"])
                                    };

                                    data.LoginStatusCode = logStatCodeData;
                                }
                            }


                            if (reader.NextResult())
                            {
                                if (reader.HasRows)
                                {
                                    reader.Read();
                                    LoginStatusAndResultsObjectModel logStatCodeData = new LoginStatusAndResultsObjectModel()
                                    {
                                        LoginStatusNumberCode = Convert.ToInt32(reader["LoginStatusNumberCode"])
                                    };
                                    data.LoginStatusCode = logStatCodeData;
                                }

                            }


                        }

                    }
                }
            }
            return data;
        }
    }
}
