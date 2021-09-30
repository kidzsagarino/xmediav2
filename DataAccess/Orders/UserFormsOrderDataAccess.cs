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

            SqlParameter[] sqlParameters = new SqlParameter[12];

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

            sqlParameters[4] = new SqlParameter("@UserOrderForms_ID", SqlDbType.Int)
            {
                Direction = ParameterDirection.Input,
                Value = this.Model.Orderforms.UserOrderForms_ID
            };
            sqlParameters[5] = new SqlParameter("@FormsMasterData_ID", SqlDbType.Int)
            {
                Direction = ParameterDirection.Input,
                Value = this.Model.Orderforms.FormsMasterData_ID
            };
            sqlParameters[6] = new SqlParameter("@FormsPaperSizesRef_ID", SqlDbType.Int)
            {
                Direction = ParameterDirection.Input,
                Value = this.Model.Orderforms.FormsPaperSizesRef_ID
            };
            sqlParameters[7] = new SqlParameter("@PaperTypeRef_ID", SqlDbType.Int)
            {
                Direction = ParameterDirection.Input,
                Value = this.Model.Orderforms.PaperTypeRef_ID
            };
            sqlParameters[8] = new SqlParameter("@PaperColorRef_ID", SqlDbType.Int)
            {
                Direction = ParameterDirection.Input,
                Value = this.Model.Orderforms.PaperColorRef_ID
            };
            sqlParameters[9] = new SqlParameter("@PaddingGlue_ID", SqlDbType.Int)
            {
                Direction = ParameterDirection.Input,
                Value = this.Model.Orderforms.PaddingGlue_ID
            };
            sqlParameters[10] = new SqlParameter("@UnitPrice", SqlDbType.Float)
            {
                Direction = ParameterDirection.Input,
                Value = this.Model.Orderforms.UnitPrice
            };
            sqlParameters[11] = new SqlParameter("@Quantity", SqlDbType.Int)
            {
                Direction = ParameterDirection.Input,
                Value = this.Model.Orderforms.Quantity
            };
            sqlParameters[12] = new SqlParameter("@hasDuplicate", SqlDbType.Bit)
            {
                Direction = ParameterDirection.Input,
                Value = this.Model.Orderforms.hasDuplicate
            };

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

            sqlParameters[13] = new SqlParameter("@TVP_DuplicateForms", SqlDbType.Structured)
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
