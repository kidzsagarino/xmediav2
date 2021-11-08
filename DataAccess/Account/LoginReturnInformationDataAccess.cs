using BusinessRef.Interfaces.Generics;
using BusinessRef.ModelObject.Account;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using BusinessRef.DashBoardObject;

using model = BusinessRef.ModelObject.Account.LoginReturnInformationModel;

namespace DataAccess.Account
{
    public class LoginReturnInformationDataAccess : IPostDatabaseData<model>
    {
        private LoginInfoUserModel Model;
        public LoginReturnInformationDataAccess(LoginInfoUserModel DataModel)
        {
            this.Model = DataModel;
        }
        public model PostDatabaseData()
        {
            string ConnectionString = ConfigurationManager.ConnectionStrings["XMEDIA_DB"].ConnectionString;
            string sp = "[customer].[spGetUserDataByLoginInfomation]";

            model data = new model();

            

            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(sp, con)
                {
                    CommandType = CommandType.StoredProcedure
                };
                SqlParameter EmailAddress = new SqlParameter("@EmailAddress", SqlDbType.VarChar, 25);
                cmd.Parameters.Add(EmailAddress);
                cmd.Parameters["@EmailAddress"].Value = this.Model.EmailAddress;

                SqlParameter IStillLoveYou = new SqlParameter("@IStillLoveYou", SqlDbType.VarChar, 25);
                cmd.Parameters.Add(IStillLoveYou);
                cmd.Parameters["@IStillLoveYou"].Value = this.Model.IStillLoveYou;

                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    if (rdr.GetSchemaTable().Rows[0].ItemArray[0].ToString() == "ErrorMessage")
                    {
                        if (rdr.HasRows)
                        {
                            while (rdr.Read())
                            {
                                data.HasError = true;
                                data.ErrorMessage = rdr["ErrorMessage"].ToString();
                            }
                        }
                    }
                    else
                    {
                        if (rdr.GetSchemaTable().Rows[0].ItemArray[0].ToString() == "ID")
                        {
                            if (rdr.HasRows)
                            {

                                rdr.Read();
                                PersonalInformationModel persInfoData = new PersonalInformationModel()
                                {
                                    UserID = Convert.ToInt32(rdr["ID"]),
                                    FirstName = rdr["FirstName"].ToString(),
                                    LastName = rdr["LastName"].ToString(),
                                    PhotoImageFileName = rdr["PhotoImageFilename"].ToString()
                                };
                                data.PersonalInfo = persInfoData;

                            }

                        }
                        else
                        {
                            if (rdr.HasRows)
                            {
                                rdr.Read();
                                LoginStatusAndResultsModel logStatCodeData = new LoginStatusAndResultsModel()
                                {
                                    LoginStatusNumberCode = Convert.ToInt32(rdr["LoginStatusNumberCode"])
                                };

                                data.LoginStatusCode = logStatCodeData;
                            }
                        }


                        if (rdr.NextResult())
                        {
                            if (rdr.HasRows)
                            {
                                rdr.Read();
                                LoginStatusAndResultsModel logStatCodeData = new LoginStatusAndResultsModel()
                                {
                                    LoginStatusNumberCode = Convert.ToInt32(rdr["LoginStatusNumberCode"])
                                };
                                data.LoginStatusCode = logStatCodeData;
                            }

                        }
                    }
                }
            }

            return data;
        }
    }
}
