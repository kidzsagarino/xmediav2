using System;
using BusinessModel.Interfaces.Generics;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using model = BusinessModel.ObjectModel.EmailSenderParameterObjectModel;


namespace DataAccess
{
    public class EmailSenderParameterDataAccess : IGetDatabaseData<model>
    {
        public model GetDatabaseData()
        {
            string connString = ConfigurationManager.ConnectionStrings["X-MEDIAWEB_DB"].ConnectionString;
            model data = new model();


            using (SqlConnection con = new SqlConnection(connString))
            {

                con.Open();

                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandText = "[admin].[spGetEmailSenderParamaters]";
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        //Check for errors and if true, retreive the error message!
                        if (reader.GetSchemaTable().Rows[0].ItemArray[0].ToString() == "ErrorMessage")
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    data.hasError = true;
                                    data.ErrorMessage = reader["ErrorMessage"].ToString();
                                }
                            }
                        }
                        //Retrieve data if no error happenned!
                        else
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    data.SenderEmail = reader["SenderEmail"].ToString();
                                    data.SenderPassword = reader["SenderPassword"].ToString();
                                    data.EmailSubject = reader["EmailSubject"].ToString();
                                    data.EmailHost = reader["EmailHost"].ToString();
                                    data.PortNumber = Convert.ToInt32(reader["PortNumber"]);
                                    data.EnableSsl = (bool)reader["EnableSsl"];
                                    data.UseDefaultCredentials = (bool)reader["UseDefaultCredentials"];
                                }
                            }
                        }

                    }
                }

                return data;

            }
        }
    }
}
