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
            string sp = "[orders].[spInsertUserFormsOrder]";

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
                cmd.Parameters["@OrderFormsCategory_ID"].Value = this.Model.OrderFormsCategory_ID;

                SqlParameter UsersID = new SqlParameter("@UsersID", SqlDbType.Int);
                cmd.Parameters.Add(UsersID);
                cmd.Parameters["@UsersID"].Value = this.Model.UsersID;

                SqlParameter OrderDate = new SqlParameter("@OrderDate", SqlDbType.DateTime);
                cmd.Parameters.Add(OrderDate);
                cmd.Parameters["@OrderDate"].Value = this.Model.OrderDate;

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

                SqlParameter PaperColorRef_ID  = new SqlParameter("@PaperColorRef_ID", SqlDbType.Int);
                cmd.Parameters.Add(PaperColorRef_ID);
                cmd.Parameters["@PaperColorRef_ID"].Value = this.Model.Orderforms.PaperColorRef_ID;

                SqlParameter PaddingGlue_ID = new SqlParameter("@PaddingGlue_ID", SqlDbType.Int);
                cmd.Parameters.Add(PaddingGlue_ID);
                cmd.Parameters["@PaddingGlue_ID"].Value = this.Model.Orderforms.PaddingGlue_ID;

                SqlParameter hasPaddingGlue  = new SqlParameter("@hasPaddingGlue", SqlDbType.Bit);
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

                #region TVP Duplicates
                DataTable duplicatesFormDT = new DataTable();
                duplicatesFormDT.Columns.Add("ID");
                duplicatesFormDT.Columns.Add("OrderForms_ID");
                duplicatesFormDT.Columns.Add("FormsPaperSizesRef_ID");
                duplicatesFormDT.Columns.Add("PaperTypeRef_ID");
                duplicatesFormDT.Columns.Add("PaperColorRef_ID");
                duplicatesFormDT.Columns.Add("UnitPrice");
                duplicatesFormDT.Columns.Add("isOriginal");

                foreach (var duplicateData in this.Model.OrderFormDuplicates)
                {
                    DataRow row = duplicatesFormDT.NewRow();
                    row["ID"] = 0;
                    row["OrderForms_ID"] = duplicateData.OrderForms_ID;
                    row["FormsPaperSizesRef_ID"] = duplicateData.FormsPaperSizesRef_ID;
                    row["PaperTypeRef_ID"] = duplicateData.PaperTypeRef_ID;
                    row["PaperColorRef_ID"] = duplicateData.PaperColorRef_ID;
                    row["UnitPrice"] = duplicateData.UnitPrice;
                    row["isOriginal"] = duplicateData.isOriginal;
                    duplicatesFormDT.Rows.Add(row);
                }

                SqlParameter TVP_DuplicateForms = new SqlParameter("@TVP_DuplicateForms", SqlDbType.Structured);
                cmd.Parameters.Add(TVP_DuplicateForms);
                cmd.Parameters["@TVP_DuplicateForms"].Value = duplicatesFormDT;

                #endregion
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
