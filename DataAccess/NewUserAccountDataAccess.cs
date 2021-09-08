using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using BusinessModel.Interfaces.Generics;
using BusinessModel.ObjectModel;
using model = BusinessModel.ObjectModel.DmlInsertNewUserAccountObjectModel;

namespace DataAccess
{
    public class NewUserAccountDataAccess : IPostDatabaseData<model>
    {
        private readonly NewAccountInfoObjectModel _newUserData;
        public NewUserAccountDataAccess(NewAccountInfoObjectModel _newUserData)
        {
            this._newUserData = _newUserData;
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
                    cmd.CommandText = "[customer].[spInsertNewUserAccount]";
                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlParameter EmailAddress = new SqlParameter("@EmailAddress", SqlDbType.NVarChar);
                    cmd.Parameters.Add(EmailAddress);
                    cmd.Parameters["@EmailAddress"].Value = _newUserData.EmailAddress;

                    SqlParameter IStillLoveYou = new SqlParameter("@IStillLoveYou", SqlDbType.NVarChar);
                    cmd.Parameters.Add(IStillLoveYou);
                    cmd.Parameters["@IStillLoveYou"].Value = _newUserData.IStillLoveYou;

                    SqlParameter FirstName = new SqlParameter("@FirstName", SqlDbType.NVarChar);
                    cmd.Parameters.Add(FirstName);
                    cmd.Parameters["@FirstName"].Value = _newUserData.FirstName;

                    SqlParameter LastName = new SqlParameter("@LastName", SqlDbType.NVarChar);
                    cmd.Parameters.Add(LastName);
                    cmd.Parameters["@LastName"].Value = _newUserData.LastName;

                    SqlParameter CompanyName = new SqlParameter("@CompanyName", SqlDbType.NVarChar);
                    cmd.Parameters.Add(CompanyName);
                    cmd.Parameters["@CompanyName"].Value = (object)_newUserData.CompanyName ?? DBNull.Value;

                    SqlParameter LandlineNo = new SqlParameter("@LandlineNo", SqlDbType.NVarChar);
                    cmd.Parameters.Add(LandlineNo);
                    cmd.Parameters["@LandlineNo"].Value = (object)_newUserData.LandlineNo ?? DBNull.Value;

                    SqlParameter MobileNo = new SqlParameter("@MobileNo", SqlDbType.NVarChar);
                    cmd.Parameters.Add(MobileNo);
                    cmd.Parameters["@MobileNo"].Value = (object)_newUserData.MobileNo ?? DBNull.Value;

                    SqlParameter PhotoImageFilename = new SqlParameter("@PhotoImageFilename", SqlDbType.NVarChar);
                    cmd.Parameters.Add(PhotoImageFilename);
                    cmd.Parameters["@PhotoImageFilename"].Value = (object)_newUserData.PhotoImageFileName ?? DBNull.Value;


                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        //Check for errors and if true, retreive the error message!
                        if (reader.GetSchemaTable().Rows[0].ItemArray[0].ToString() == "ErrorMessage")
                        {
                            if (reader.HasRows)
                            {
                                reader.Read();
                                data.hasError = true;
                                data.ErrorMessage = reader["ErrorMessage"].ToString();
                            }
                        }
                        //Retrieve data if no error happenned!
                        else
                        {
                            if (reader.HasRows)
                            {
                                reader.Read();

                                data.HasExistingEmail = (bool)reader["HasExistingEmail"];
                                data.IsSuccessful = (bool)reader["IsSuccesful"];
                                data.FirstName = reader["FirstName"].ToString();
                                data.EmailAddressId = Convert.ToInt32(reader["EmailAddressId"]);
                                data.EmailAddress = reader["EmailAddress"].ToString();
                            }
                        }

                    }
                }
            }
            return data;
        }
    }
}
