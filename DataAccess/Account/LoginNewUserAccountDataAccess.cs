using BusinessRef.Interfaces.Generics;
using BusinessRef.ModelObject.Account;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using BusinessRef.DashBoardObject;


using model = BusinessRef.ModelObject.Account.LoginNewUserAccountDataModel;

namespace DataAccess.Account
{
    public class LoginNewUserAccountDataAccess : IPostDatabaseData<LoginReturnInformationModel>
    {
        private LoginNewUserAccountDataModel Model;

        public LoginNewUserAccountDataAccess(model dataModel)
        {
            this.Model = dataModel;
        }

        public LoginReturnInformationModel PostDatabaseData()
        {
            string ConnectionString = ConfigurationManager.ConnectionStrings["XMEDIA_DB"].ConnectionString;
            string sp = "[customer].[spInsertNewUserAccount]";

            LoginReturnInformationModel loginInfo = new LoginReturnInformationModel();

            using(SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(sp, con)
                {
                    CommandType = CommandType.StoredProcedure
                };
                SqlParameter EmailAddress = new SqlParameter("@EmailAddress", SqlDbType.VarChar);
                cmd.Parameters.Add(EmailAddress);
                cmd.Parameters["@EmailAddress"].Value = this.Model.LoginInfo.EmailAddress;

                SqlParameter Password = new SqlParameter("@IStillLoveYou", SqlDbType.VarChar);
                cmd.Parameters.Add(Password);
                cmd.Parameters["@IStillLoveYou"].Value = this.Model.LoginInfo.IStillLoveYou;

                SqlParameter FirstName = new SqlParameter("@FirstName", SqlDbType.VarChar);
                cmd.Parameters.Add(FirstName);
                cmd.Parameters["@FirstName"].Value = this.Model.PersonalInfo.FirstName;

                SqlParameter LastName = new SqlParameter("@LastName", SqlDbType.VarChar);
                cmd.Parameters.Add(LastName);
                cmd.Parameters["@LastName"].Value = this.Model.PersonalInfo.LastName;

                SqlParameter CompanyName = new SqlParameter("@CompanyName", SqlDbType.VarChar);
                cmd.Parameters.Add(CompanyName);
                cmd.Parameters["@CompanyName"].Value = this.Model.CompanyName;

                SqlParameter LandlineNo = new SqlParameter("@LandlineNo", SqlDbType.VarChar);
                cmd.Parameters.Add(LandlineNo);
                cmd.Parameters["@LandlineNo"].Value = this.Model.LandlineNo != null ? this.Model.LandlineNo : "";

                SqlParameter MobileNo = new SqlParameter("@MobileNo", SqlDbType.VarChar);
                cmd.Parameters.Add(MobileNo);
                cmd.Parameters["@MobileNo"].Value = this.Model.MobileNo;

                SqlParameter PhotoImageFilename = new SqlParameter("@PhotoImageFilename", SqlDbType.VarChar);
                cmd.Parameters.Add(PhotoImageFilename);
                cmd.Parameters["@PhotoImageFilename"].Value = this.Model.PersonalInfo.PhotoImageFileName;

                using(SqlDataReader rdr = cmd.ExecuteReader())
                {
                    if (rdr.GetSchemaTable().Rows[0].ItemArray[0].ToString() == "ErrorMessage")
                    {
                        if (rdr.HasRows)
                        {
                            while (rdr.Read())
                            {
                                loginInfo.HasError = true;
                                loginInfo.ErrorMessage = rdr["ErrorMessage"].ToString();
                            }
                        }
                    }
                    else
                    {
                        if (rdr.HasRows)
                        {
                            while (rdr.Read())
                            {
                                loginInfo.IsSuccessful = true;
                                loginInfo.HasExistingEmail = (bool)rdr["HasExistingEmail"];
                            }
                        }
                    }
                }
            }

            return loginInfo;
        }
    }
}
