using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using BusinessModel.Interfaces.Generics;
using BusinessModel.ObjectModel.ForgotPassword;
using model = BusinessModel.ObjectModel.ForgotPassword.DmlInsertNewPassword;

namespace DataAccess.ForgotPasswordDataAccess
{
    public class AcceptNewPasswordDataAccess : IPostDatabaseData<model>
    {
        private readonly AcceptNewPasswordObjectModel _newPassdata;
        public AcceptNewPasswordDataAccess(AcceptNewPasswordObjectModel _newPassdata)
        {
            this._newPassdata = _newPassdata;
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
                    cmd.CommandText = "[customer].[spInsertNewForgotPassword]";
                    cmd.CommandType = CommandType.StoredProcedure;


                    cmd.Parameters.Add(new SqlParameter("@EmailID", SqlDbType.VarChar));
                    cmd.Parameters["@EmailID"].Value =_newPassdata.EmailID;

                    cmd.Parameters.Add(new SqlParameter("@NewPassword", SqlDbType.NVarChar));
                    cmd.Parameters["@NewPassword"].Value = _newPassdata.NewPassword;

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
                                data.IsSuccessful = (bool)reader["IsSuccessful"];
                                data.StatusCodeNumber = Convert.ToInt32(reader["StatusCodeNumber"]);

                            }

                        }



                    }

                }
            }
            return data;
        }
    }
}
