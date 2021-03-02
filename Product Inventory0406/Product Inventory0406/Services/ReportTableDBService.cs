using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//新增，與資料庫的連線字串
using Product_Inventory0406.Models;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;



namespace Product_Inventory0406.Services
{
    public class ReportTableDBService
    {
        //建立資料庫的連線字串
        private readonly static string constr = ConfigurationManager.ConnectionStrings["pdi04"].ConnectionString;

        //建立資料庫的連線
        private readonly SqlConnection pdi04_conn = new SqlConnection(constr);

        #region 報表呈現
        public List<pdi_report> GetPdi_Reports()
        {
            //宣告要回傳的搜尋資料為資料庫中的產品資料表
            List<pdi_report> pdi_report_datalist = new List<pdi_report>();

            //SELECT pdi_table.ProductKey,pdi_table.ProductName,pdi_table.SafeAmount,pdi_inventory.NewProductNumber,pdi_inventory.InventoryDate FROM pdi_table INNER JOIN pdi_inventory ON pdi_table.ProductKey = pdi_inventory.ProductKey
            //sql語法
            var Sql_ReportInquiry = @"select pdi_table.ProductKey,pdi_table.ProductName,pdi_table.Product_Category,pdi_table.RFID_Category,pdi_inventory.NewProductNumber,pdi_inventory.InventoryDate FROM pdi_table INNER JOIN pdi_inventory ON pdi_table.ProductKey = pdi_inventory.ProductKey";

            try
            {
                //開啟資料庫
                pdi04_conn.Open();

                //執行SQL指令
                SqlCommand sqlcmd_select_pdi_report_data = new SqlCommand(Sql_ReportInquiry, pdi04_conn);

                //取得SQL資料
                SqlDataReader sqldr_pdi_report_table = sqlcmd_select_pdi_report_data.ExecuteReader();

                while (sqldr_pdi_report_table.Read())
                {
                    pdi_report pdi_report_data = new pdi_report();
                    pdi_report_data.ProductKey = sqldr_pdi_report_table["ProductKey"].ToString();
                    pdi_report_data.ProductName = sqldr_pdi_report_table["ProductName"].ToString();
                    pdi_report_data.Product_Category = sqldr_pdi_report_table["Product_Category"].ToString();
                    pdi_report_data.RFID_Category = sqldr_pdi_report_table["RFID_Category"].ToString();
                    pdi_report_data.NewProductNumber = Convert.ToInt32(sqldr_pdi_report_table["NewProductNumber"]);
                    pdi_report_data.InventoryDate = Convert.ToDateTime(sqldr_pdi_report_table["InventoryDate"]);

                    pdi_report_datalist.Add(pdi_report_data);
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
            finally
            {
                //關閉資料庫連線
                pdi04_conn.Close();
            }
            return pdi_report_datalist;
        }
        #endregion
        //報表呈現


        #region 透過產品編號查詢報表
        public List<pdi_report> GetPdi__ID_Reports(string ProudctKey)
        {
            //宣告要回傳的搜尋資料為資料庫中的產品資料表
            List<pdi_report> pdi_report_datalist = new List<pdi_report>();

            //select pdi_table.ProductKey,pdi_table.ProductName,pdi_table.Product_Category,pdi_table.RFID_Category,pdi_inventory.NewProductNumber,pdi_inventory.InventoryDate FROM pdi_table INNER JOIN pdi_inventory ON pdi_table.ProductKey = pdi_inventory.ProductKey where pdi_inventory.ProductKey='test10' ORDER BY pdi_inventory.InventoryDate desc
            //sql語法
            var Sql_ReportInquiry = @"select pdi_table.ProductKey,pdi_table.ProductName,pdi_table.Product_Category,pdi_table.RFID_Category,pdi_inventory.NewProductNumber,pdi_inventory.InventoryDate FROM pdi_table INNER JOIN pdi_inventory ON pdi_table.ProductKey = pdi_inventory.ProductKey where pdi_inventory.ProductKey=@ProductKey ORDER BY pdi_inventory.InventoryDate desc";

            try
            {
                //開啟資料庫
                pdi04_conn.Open();

                //執行SQL指令
                SqlCommand sqlcmd_select_pdi_report_data = new SqlCommand(Sql_ReportInquiry, pdi04_conn);

                sqlcmd_select_pdi_report_data.Parameters.AddWithValue("@ProductKey", ProudctKey);

                //取得SQL資料
                SqlDataReader sqldr_pdi_report_table = sqlcmd_select_pdi_report_data.ExecuteReader();

                while (sqldr_pdi_report_table.Read())
                {
                    pdi_report pdi_report_data = new pdi_report();
                    pdi_report_data.ProductKey = sqldr_pdi_report_table["ProductKey"].ToString();
                    pdi_report_data.ProductName = sqldr_pdi_report_table["ProductName"].ToString();
                    pdi_report_data.Product_Category = sqldr_pdi_report_table["Product_Category"].ToString();
                    pdi_report_data.RFID_Category = sqldr_pdi_report_table["RFID_Category"].ToString();
                    pdi_report_data.NewProductNumber = Convert.ToInt32(sqldr_pdi_report_table["NewProductNumber"]);
                    pdi_report_data.InventoryDate = Convert.ToDateTime(sqldr_pdi_report_table["InventoryDate"]);

                    pdi_report_datalist.Add(pdi_report_data);
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
            finally
            {
                //關閉資料庫連線
                pdi04_conn.Close();
            }
            return pdi_report_datalist;

        }

        #endregion


    }
}