using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using BusinessModel.Interfaces.Generics;
using model = BusinessModel.ObjectModel.DashboardModel.PersonalInformationObjectModel;

namespace DataAccess.DashboardDataAccess
{
    public class GetPersonalInformationDataAccess : IGetDatabaseData<model>
    {
        private readonly int _userID;
        public GetPersonalInformationDataAccess(int _userID)
        {
            this._userID = _userID;
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
                    cmd.CommandText = "[customer].[spGetPersonalInformation]";
                    cmd.CommandType = CommandType.StoredProcedure;

                   
                    cmd.Parameters.Add(new SqlParameter("@MasterID", SqlDbType.Int));
                    cmd.Parameters["@MasterID"].Value = _userID;

                    
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

                                    data.FirstName = reader["FirstName"].ToString();
                                    data.MiddleName = reader["MiddleName"].ToString();
                                    data.LastName = reader["LastName"].ToString();
                                    data.GenderID = reader["GenderID"].ToString();
                                    data.BirthDate = reader["BirthDate"].ToString();
                                    data.Citizenship = reader["Citizenship"].ToString();
                                    data.Profession = reader["Profession"].ToString();


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
