using BusinessRef.Interfaces.Generics;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using BusinessRef.ModelObject;

using model = BusinessRef.ModelObject.Orders.UserFormOrdersNoDuplicateModel;

namespace DataAccess.Orders
{
    public class UserFormsOrderNoDuplicatesDataAccess : IPostDatabaseData<ConfirmInsertDataModel>
    {
        private model Model;
        public UserFormsOrderNoDuplicatesDataAccess(model DataModel)
        {
            this.Model = DataModel;
        }
        public ConfirmInsertDataModel PostDatabaseData()
        {
            string ConnectionString = ConfigurationManager.ConnectionStrings["XMEDIA_DB"].ConnectionString;
            string sp = "[orders].[spInsertUserFormsOrderNoDuplicate]";

            ConfirmInsertDataModel data = new ConfirmInsertDataModel();
            
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(sp, con)
                {
                    CommandType = CommandType.StoredProcedure
                };
                SqlParameter OrderFormsCategory_ID = new SqlParameter("@OrderFormsCategory_ID", SqlDbType.Int);
                cmd.Parameters.Add(OrderFormsCategory_ID);
                cmd.Parameters["@OrderID"].Value = this.Model.OrderID;

                SqlParameter UsersID = new SqlParameter("@UsersID", SqlDbType.Int);
                cmd.Parameters.Add(UsersID);
                cmd.Parameters["@UsersID"].Value = this.Model.UsersID;

                SqlParameter TotalPrice = new SqlParameter("@TotalPrice", SqlDbType.Float);
                cmd.Parameters.Add(TotalPrice);
                cmd.Parameters["@TotalPrice"].Value = this.Model.TotalPrice;

                SqlParameter OrderStatusID = new SqlParameter("@OrderStatusID", SqlDbType.Int);
                cmd.Parameters.Add(OrderStatusID);
                cmd.Parameters["@OrderStatusID"].Value = this.Model.OrderStatusID;

                SqlParameter FormsMasterData_ID = new SqlParameter("@FormsMasterData_ID", SqlDbType.Int);
                cmd.Parameters.Add(FormsMasterData_ID);
                cmd.Parameters["@FormsMasterData_ID"].Value = this.Model.Orderforms.FormsMasterData_ID;

                SqlParameter FormsPaperSizesRef_ID = new SqlParameter("@FormsPaperSizesRef_ID", SqlDbType.Int);
                cmd.Parameters.Add(FormsPaperSizesRef_ID);
                cmd.Parameters["@FormsPaperSizesRef_ID"].Value = this.Model.Orderforms.FormsPaperSizesRef_ID;

                SqlParameter PaperTypeRef_ID = new SqlParameter("@PaperTypeRef_ID", SqlDbType.Int);
                cmd.Parameters.Add(PaperTypeRef_ID);
                cmd.Parameters["@PaperTypeRef_ID"].Value = this.Model.Orderforms.PaperTypeRef_ID;

                SqlParameter PaperColorRef_ID = new SqlParameter("@PaperColorRef_ID", SqlDbType.Int);
                cmd.Parameters.Add(PaperColorRef_ID);
                cmd.Parameters["@PaperColorRef_ID"].Value = this.Model.Orderforms.PaperColorRef_ID;

                SqlParameter PaddingGlue_ID = new SqlParameter("@PaddingGlue_ID", SqlDbType.Int);
                cmd.Parameters.Add(PaddingGlue_ID);
                cmd.Parameters["@PaddingGlue_ID"].Value = this.Model.Orderforms.PaddingGlue_ID;

                SqlParameter hasPaddingGlue = new SqlParameter("@hasPaddingGlue", SqlDbType.Bit);
                cmd.Parameters.Add(hasPaddingGlue);
                cmd.Parameters["@hasPaddingGlue"].Value = this.Model.Orderforms.hasPaddingGlue;

                SqlParameter NoOfSetPad = new SqlParameter("@NoOfSetPad", SqlDbType.Int);
                cmd.Parameters.Add(NoOfSetPad);
                cmd.Parameters["@NoOfSetPad"].Value = this.Model.Orderforms.NoOfSetPad;

                SqlParameter PadSide = new SqlParameter("@PadSide", SqlDbType.VarChar, 15);
                cmd.Parameters.Add(PadSide);
                cmd.Parameters["@PadSide"].Value = this.Model.Orderforms.PadSide;

                SqlParameter UnitPrice = new SqlParameter("@UnitPrice", SqlDbType.Float);
                cmd.Parameters.Add(UnitPrice);
                cmd.Parameters["@UnitPrice"].Value = this.Model.Orderforms.UnitPrice;

                SqlParameter Quantity = new SqlParameter("@Quantity", SqlDbType.Int);
                cmd.Parameters.Add(Quantity);
                cmd.Parameters["@Quantity"].Value = this.Model.Orderforms.Quantity;

                SqlParameter hasDuplicate = new SqlParameter("@hasDuplicate", SqlDbType.Bit);
                cmd.Parameters.Add(hasDuplicate);
                cmd.Parameters["@hasDuplicate"].Value = this.Model.Orderforms.hasDuplicate;

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
