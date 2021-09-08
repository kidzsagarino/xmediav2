using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using BusinessModel.Interfaces.Generics;
using model = BusinessModel.ObjectModel.ForgotPassword.EmailStatusAndConfirmationObjectModel;

namespace DataAccess
{

    public class EmailStatusAndConfirmationDataAccess : IGetDatabaseData<model>
    {
        private readonly string _emailAdd;
        public EmailStatusAndConfirmationDataAccess(string _emailAdd)
        {
            this._emailAdd = _emailAdd;
        }

        public model GetDatabaseData()
        {
            string connString = ConfigurationManager.ConnectionStrings["X-MEDIAWEB_DB"].ConnectionString;
            model data = new model();
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "[customer].[spCheckEmailAddressAndStatus]";
                    cmd.CommandType = CommandType.StoredProcedure;


                    cmd.Parameters.Add(new SqlParameter("@EmailAddress", SqlDbType.NVarChar));
                    cmd.Parameters["@EmailAddress"].Value = _emailAdd;


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
                                while (reader.Read())
                                {
                                    data.EmailID = Convert.ToInt32(reader["EmailID"]);
                                    data.FirstName = reader["FirstName"].ToString();
                                    data.IsEmailExist = (bool)reader["IsEmailExist"];
                                    data.EmailAddress = reader["EmailAddress"].ToString();
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
