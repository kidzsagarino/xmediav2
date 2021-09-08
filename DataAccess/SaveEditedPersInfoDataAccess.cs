using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using BusinessModel.Interfaces.Generics;
using BusinessModel.ObjectModel.DashboardModel;
using model = BusinessModel.ObjectModel.DmlReturnDataFromDbObjectModel<bool>;
namespace DataAccess
{
    public class SaveEditedPersInfoDataAccess : IPostDatabaseData<model>
    {
        private readonly PersonalInformationObjectModel _userEditedPersInfo;
        public SaveEditedPersInfoDataAccess(PersonalInformationObjectModel _userEditedPersInfo)
        {
            this._userEditedPersInfo = _userEditedPersInfo;
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
                    cmd.CommandText = "[customer].[spUpdatePersonalInformation]";
                    cmd.CommandType = CommandType.StoredProcedure;

                    #region Sql Parameter
                    cmd.Parameters.Add(new SqlParameter("@MasterID", SqlDbType.Int));
                    cmd.Parameters["@MasterID"].Value = _userEditedPersInfo.UserID;

                    cmd.Parameters.Add(new SqlParameter("@FirstName", SqlDbType.NVarChar));
                    cmd.Parameters["@FirstName"].Value = (object)_userEditedPersInfo.FirstName ?? DBNull.Value;

                    cmd.Parameters.Add(new SqlParameter("@MiddleName", SqlDbType.NVarChar));
                    cmd.Parameters["@MiddleName"].Value = (object)_userEditedPersInfo.MiddleName ?? DBNull.Value;

                    cmd.Parameters.Add(new SqlParameter("@LastName", SqlDbType.NVarChar));
                    cmd.Parameters["@LastName"].Value = (object)_userEditedPersInfo.LastName ?? DBNull.Value;

                    cmd.Parameters.Add(new SqlParameter("@GenderID", SqlDbType.VarChar));
                    cmd.Parameters["@GenderID"].Value = (object)_userEditedPersInfo.GenderID ?? DBNull.Value;

                    cmd.Parameters.Add(new SqlParameter("@BirthDate", SqlDbType.VarChar));
                    cmd.Parameters["@BirthDate"].Value = (object)_userEditedPersInfo.BirthDate ?? DBNull.Value;

                    cmd.Parameters.Add(new SqlParameter("@Citizenship", SqlDbType.NVarChar));
                    cmd.Parameters["@Citizenship"].Value = (object)_userEditedPersInfo.Citizenship ?? DBNull.Value;

                    cmd.Parameters.Add(new SqlParameter("@Profession", SqlDbType.NVarChar));
                    cmd.Parameters["@Profession"].Value = (object)_userEditedPersInfo.Profession ?? DBNull.Value;

                    #endregion


                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (reader.HasRows)
                            {
                                //Check for errors and if true, retreive the error message!
                                if (reader.GetSchemaTable().Rows[0].ItemArray[0].ToString() == "ErrorMessage")
                                {


                                    data.hasError = true;
                                    data.ErrorMessage = reader["ErrorMessage"].ToString();


                                }
                                //Retrieve data if no error happenned!
                                else
                                {


                                    data.DmlReturnData = (bool)reader["DmlReturnData"];

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

