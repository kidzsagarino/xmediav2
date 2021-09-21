using BusinessRef.Interfaces.Generics;
using BusinessRef.ModelObject.Forms;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using model = BusinessRef.ModelObject.Forms.GetPurchaseOrderFormsInitialDataModel;

namespace DataAcces.Forms
{
    public class FormDataAccess : IGetDatabaseData<model>
    {
        string ConnectionString = ConfigurationManager.ConnectionStrings["MCBI_DB"].ConnectionString;
        private int ID;
        public FormDataAccess(int ID)
        {
            this.ID = ID;
        }
        public model GetDatabaseData()
        {
            string sp = "forms.spGetFormsInitialDataByID";

            model data = new model();

            ICollection<FormsAssignedSizeModel> paperSizeList = new List<FormsAssignedSizeModel>();
            ICollection<FormPaperTypeModel> paperTypeList = new List<FormPaperTypeModel>();

            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandText = sp;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@FormsMasterID", this.ID);
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
                                    FormsAssignedSizeModel dataList = new FormsAssignedSizeModel()
                                    {
                                        ID = (int)rdr["ID"],
                                        FormSizeID = (int)rdr["FormSizeID"],
                                        FormSizes = rdr["FormSizes"].ToString()

                                    };
                                    paperSizeList.Add(dataList);

                                }
                                data.formsAssignedSizes = paperSizeList;
                            }

                            if (rdr.NextResult())
                            {
                                if (rdr.HasRows)
                                {
                                    while (rdr.Read())
                                    {
                                        FormPaperTypeModel dataList = new FormPaperTypeModel()
                                        {
                                            ID = (int)rdr["PaperTypeID"],
                                            PaperType = rdr["PaperType"].ToString()
                                        };
                                        paperTypeList.Add(dataList);
                                    }
                                    data.formPaperTypes = paperTypeList;
                                }
                            }

                        }
                        con.Close();
                    }
                }

            }

            return data;
        }
    }
}
