using BusinessRef.Interfaces.Generics;
using BusinessRef.ModelObject.Login;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using BusinessRef.DashBoardObject;


using model = BusinessRef.ModelObject.Login.LoginNewUserAccountDataModel;

namespace DataAccess.Login
{
    public class LoginNewUserAccountDataAccess : IPostDatabaseData<model>
    {
        private LoginNewUserAccountDataModel Model;

        public LoginNewUserAccountDataAccess(LoginNewUserAccountDataModel dataModel)
        {
            this.Model = dataModel;
        }

        public model PostDatabaseData()
        {
            string ConnectionString = ConfigurationManager.ConnectionStrings["XMEDIA_DB"].ConnectionString;
            string sp = "[customer].[spInsertNewUserAccount]";

            model data = new model();

            throw new NotImplementedException();
        }
    }
}
