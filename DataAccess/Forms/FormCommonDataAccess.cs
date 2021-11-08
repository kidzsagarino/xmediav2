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

namespace DataAccess.Forms
{
    public class FormCommonDataAccess : IGetDatabaseData<model>
    {
        string ConnectionString = ConfigurationManager.ConnectionStrings["XMEDIA_DB"].ConnectionString;

        public model GetDatabaseData()
        {
            string sp = "forms.spGetFormsInitialData";

            model data = new model();

            ICollection<FormAssignedSizeModel> paperSizeList = new List<FormAssignedSizeModel>();
            ICollection<FormPaperTypeModel> paperTypeList = new List<FormPaperTypeModel>();
            ICollection<FormQuantityModel> formQuantities = new List<FormQuantityModel>();
            ICollection<FormPrintColorModel> printColors = new List<FormPrintColorModel>();

            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandText = sp;
                    cmd.CommandType = CommandType.StoredProcedure;
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
                                    FormAssignedSizeModel dataList = new FormAssignedSizeModel()
                                    {
                                        ID = (int)rdr["ID"],
                                        FormMasterID = (int)rdr["FormMasterID"],
                                        FormSizeID = (int)rdr["FormSizeID"],
                                        PaperSize = rdr["PaperSize"].ToString(),
                                        DivisorFactor = float.Parse(rdr["DivisorFactor"].ToString()),
                                        LaborFactor = float.Parse(rdr["LaborFactor"].ToString())
                                    };
                                    paperSizeList.Add(dataList);

                                }
                                data.FormAssignedSizes = paperSizeList;
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
                                            FormMasterID = (int)rdr["FormMasterID"],
                                            PaperType = rdr["PaperType"].ToString(),
                                            Thickness = rdr["Thickness"].ToString(),
                                            PaperCostAtA3 = float.Parse(rdr["PaperCostAtA3"].ToString()),
                                            LaborCostAtA3 = float.Parse(rdr["LaborCostAtA3"].ToString()),
                                            PrintingCostAtA3BW = float.Parse(rdr["PrintingCostAtA3BW"].ToString()),
                                            PrintingCostAtA3Colored = float.Parse(rdr["PrintingCostAtA3Colored"].ToString())

                                        };
                                        paperTypeList.Add(dataList);
                                    }
                                    data.FormPaperTypes = paperTypeList;
                                }
                            }

                            if (rdr.NextResult())
                            {
                                if (rdr.HasRows)
                                {
                                    while (rdr.Read())
                                    {
                                        FormQuantityModel dataList = new FormQuantityModel
                                        {
                                            ID = (int)rdr["ID"],
                                            FormMasterID = (int)rdr["FormMasterID"],
                                            FormQuantity = (int)rdr["FormQuantity"],
                                            QuantityFactor = float.Parse(rdr["QuantityFactor"].ToString())
                                        };

                                        formQuantities.Add(dataList);
                                    }

                                    data.FormQuantities = formQuantities;
                                }
                            }

                            if (rdr.NextResult())
                            {
                                if (rdr.HasRows)
                                {
                                    while (rdr.Read())
                                    {
                                        FormPrintColorModel dataList = new FormPrintColorModel
                                        {
                                            ID = (int)rdr["ID"],
                                            FormMasterID = (int)rdr["FormMasterID"],
                                            PrintColor = rdr["PrintColor"].ToString()
                                        };

                                        printColors.Add(dataList);
                                    }

                                    data.FormPrintColors = printColors;
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
