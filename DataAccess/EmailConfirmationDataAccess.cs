using BusinessModel.ObjectModel;
using BusinessModel.Interfaces.Generics;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using model = BusinessModel.ObjectModel.DmlInsertNewUserAccountObjectModel;
using System;


namespace DataAccess
{
    public class EmailConfirmationDataAccess : IPostDatabaseData<model>
    {
        private readonly int _databaseEmailId;
        public EmailConfirmationDataAccess(int _databaseEmailId)
        {
            this._databaseEmailId = _databaseEmailId;
        }
        public model PostDatabaseData()
        {
            string connString = ConfigurationManager.ConnectionStrings["X-MEDIAWEB_DB"].ConnectionString;
            model data = new model();


            using (SqlConnection con = new SqlConnection(connString))
            {
                
                con.Open();

                using(SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;

                    cmd.CommandText = "[customer].[spConfirmUserEmailAddress]";
                    cmd.CommandType = CommandType.StoredProcedure;
                    
                    cmd.Parameters.Add(new SqlParameter("@EmailAddressID", SqlDbType.Int));
                    cmd.Parameters["@EmailAddressID"].Value = _databaseEmailId;

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
                                data.IsSuccessful = (bool)reader["IsSuccesful"];
                                
                            }
                        }

                    }
                }

                return data;
                
            }
        }
    }
}
