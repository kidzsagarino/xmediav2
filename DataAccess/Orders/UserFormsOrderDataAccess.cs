using BusinessRef.Interfaces.Generics;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using BusinessRef.ModelObject;

using model = BusinessRef.ModelObject.Orders.UserFormOrdersModel;

namespace DataAccess.Orders
{
    public class UserFormsOrderDataAccess : IPostDatabaseData<ConfirmInsertDataModel>
    {
        private model Model;
        public UserFormsOrderDataAccess(model DataModel)
        {
            this.Model = DataModel;
        }

        public ConfirmInsertDataModel PostDatabaseData()
        {
            string ConnectionString = ConfigurationManager.ConnectionStrings["XMEDIA_DB"].ConnectionString;
            string sp = "[orders].[spPostUserFormsOrder]";

            ConfirmInsertDataModel data = new ConfirmInsertDataModel();

            SqlParameter[] sqlParameters = new SqlParameter[6];

            sqlParameters[0] = new SqlParameter("@OrderFormsCategory_ID", SqlDbType.Int)
            {
                Direction = ParameterDirection.Input,
                Value = this.Model.OrderFormsCategory_ID

            };

            sqlParameters[1] = new SqlParameter("@UsersID", SqlDbType.Int)
            {
                Direction = ParameterDirection.Input,
                Value = this.Model.UsersID
            };

            sqlParameters[2] = new SqlParameter("@OrderDate", SqlDbType.DateTime)
            {
                Direction = ParameterDirection.Input,
                Value = this.Model.OrderDate
            };

            sqlParameters[3] = new SqlParameter("@TotalPrice", SqlDbType.Float)
            {
                Direction = ParameterDirection.Input,
                Value = this.Model.TotalPrice
            };

            #region TVP Oder Forms
            DataTable orderFormsDT = new DataTable();
            orderFormsDT.Columns.Add("UserOrderForms_ID");
            orderFormsDT.Columns.Add("FormsMasterData_ID");
            orderFormsDT.Columns.Add("FormsPaperSizesRef_ID");
            orderFormsDT.Columns.Add("PaperTypeRef_ID");
            orderFormsDT.Columns.Add("PaperColorRef_ID");
            orderFormsDT.Columns.Add("PaddingGlue_ID");
            orderFormsDT.Columns.Add("UnitPrice");
            orderFormsDT.Columns.Add("Quantity");
            orderFormsDT.Columns.Add("hasDuplicate");

            foreach (var orderFormsData in this.Model.Orderforms)
            {
                DataRow row = orderFormsDT.NewRow();
                row["UserOrderForms_ID"] = orderFormsData.UserOrderForms_ID;
                row["FormsMasterData_ID"] = orderFormsData.FormsMasterData_ID;
                row["FormsPaperSizesRef_ID"] = orderFormsData.FormsPaperSizesRef_ID;
                row["PaperTypeRef_ID"] = orderFormsData.PaperTypeRef_ID;
                row["PaperColorRef_ID"] = orderFormsData.PaperColorRef_ID;
                row["UnitPrice"] = orderFormsData.UnitPrice;
                row["Quantity"] = orderFormsData.Quantity;
                row["hasDuplicate"] = orderFormsData.hasDuplicate;
            }

            sqlParameters[4] = new SqlParameter("@TVP_OrderForms", SqlDbType.Structured)
            {
                Direction = ParameterDirection.Input,
                Value = this.Model.Orderforms
            };
            #endregion

            #region TVP Duplicates
            DataTable duplicatesFormDT = new DataTable();
            duplicatesFormDT.Columns.Add("OrderForms_ID");
            duplicatesFormDT.Columns.Add("FormsPaperSizesRef_ID");
            duplicatesFormDT.Columns.Add("PaperTypeRef_ID");
            duplicatesFormDT.Columns.Add("PaperColorRef_ID");
            duplicatesFormDT.Columns.Add("UnitPrice");
            duplicatesFormDT.Columns.Add("isOriginal");

            foreach (var duplicateData in this.Model.OrderFormDuplicates)
            {
                DataRow row = duplicatesFormDT.NewRow();
                row["OrderForms_ID"] = duplicateData.OrderForms_ID;
                row["FormsPaperSizesRef_ID"] = duplicateData.FormsPaperSizesRef_ID;
                row["PaperTypeRef_ID"] = duplicateData.PaperTypeRef_ID;
                row["PaperColorRef_ID"] = duplicateData.PaperColorRef_ID;
                row["UnitPrice"] = duplicateData.UnitPrice;
                row["isOriginal"] = duplicateData.isOriginal;
            }

            sqlParameters[5] = new SqlParameter("@TVP_DuplicateForms", SqlDbType.Structured)
            {
                Direction = ParameterDirection.Input,
                Value = this.Model.OrderFormDuplicates
            };
            #endregion


            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(sp, con)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddRange(sqlParameters);
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
                        if (rdr.HasRows)
                        {
                            while (rdr.Read())
                            {
                                data.IsSuccessful = true;
                            }
                        }
                    }
                }
            }
            return data;
        }
    }
}
