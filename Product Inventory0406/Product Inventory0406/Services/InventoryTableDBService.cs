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
    public class InventoryTableDBService
    {
        //建立資料庫的連線字串
        private readonly static string constr = ConfigurationManager.ConnectionStrings["pdi04"].ConnectionString;

        //建立資料庫的連線
        private readonly SqlConnection pdi04_conn = new SqlConnection(constr);


        #region 查詢盤點清單
        //根據搜尋來取得成員陣列資料
        public List<pdi_inventory> GetInv_Tables()
        {
            //宣告要回傳的搜尋資料為資料庫中的產品資料表
            List<pdi_inventory> Inv_DataTable = new List<pdi_inventory>();

            //sql語法
            //SQL需要做inner join才可以
            //SELECT pdi_table.ProductKey,pdi_table.ProductName,pdi_table.SafeAmount,pdi_inventory.Inv_number,pdi_inventory.NewProductNumber,pdi_inventory.InventoryDate from pdi_table INNER JOIN pdi_inventory ON pdi_table.ProductKey = pdi_inventory.ProductKey ORDER BY InventoryDate desc
            var sql_select_inventorytable = @"SELECT pdi_inventory.Inv_number,pdi_table.ProductKey,pdi_table.ProductName,pdi_table.SafeAmount,pdi_inventory.NewProductNumber,pdi_inventory.InventoryDate from pdi_table INNER JOIN pdi_inventory ON pdi_table.ProductKey = pdi_inventory.ProductKey ORDER BY InventoryDate desc";

            //DataAdApter 資料配接器 會自動開啟或關閉連結  
            //順序依照sql語法由最新到最舊，要兩個資料表合併在寫入
            //SqlDataAdapter sqlAd = new SqlDataAdapter(sql_select_inventorytable, pdi04_conn);
            //DataSet ds = new DataSet();
            //sqlAd.Fill(ds, "inventory_tb");

            //設定例外狀況
            try
            {
                pdi04_conn.Open();

                //執行SQL指令
                SqlCommand sqlcmd_select_inventorytable = new SqlCommand(sql_select_inventorytable, pdi04_conn);

                //取得SQL資料
                SqlDataReader sqldr_inventorytable = sqlcmd_select_inventorytable.ExecuteReader();
                while (sqldr_inventorytable.Read())
                {
                    pdi_inventory InvData = new pdi_inventory();
                    InvData.Inv_number = Convert.ToInt32(sqldr_inventorytable["Inv_number"]);
                    InvData.ProductKey = sqldr_inventorytable["ProductKey"].ToString();
                    InvData.NewProductNumber = Convert.ToInt32(sqldr_inventorytable["NewProductNumber"]);
                    InvData.InventoryDate = Convert.ToDateTime(sqldr_inventorytable["InventoryDate"]);
                    //
                    InvData.ProductName = sqldr_inventorytable["ProductName"].ToString();
                    InvData.SafeAmount = Convert.ToInt32(sqldr_inventorytable["SafeAmount"]);


                    Inv_DataTable.Add(InvData);
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
            //回傳資料庫中的盤點資料表
            return Inv_DataTable;
        }
        #endregion



        #region 新增盤點紀錄
        public void Insert_Inventory(pdi_inventory new_data) 
        {
            //insert into pdi_inventory(ProductKey,NewProductNumber,InventoryDate) values('test10',1500,2021-01-15)
            var sql_insert_inventory = $@"insert into pdi_inventory(ProductKey,NewProductNumber,InventoryDate)" + "values(@ProductKey,@NewProductNumber,@InventoryDate)";

            try
            {
                //執行SQL指令
                SqlCommand cmd = new SqlCommand(sql_insert_inventory, pdi04_conn);
                //隱藏碼攻擊問題。
                cmd.Parameters.AddWithValue("@ProductKey", new_data.ProductKey);
                cmd.Parameters.AddWithValue("@NewProductNumber",new_data.NewProductNumber);
                cmd.Parameters.AddWithValue("@InventoryDate", new_data.InventoryDate.ToString("yyyy-MM-dd HH:mm:ss"));


                //開啟資料庫
                pdi04_conn.Open();
                cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                //丟出錯誤
                throw new Exception(ex.Message.ToString());
            }
            finally
            {
                //關閉資料庫
                pdi04_conn.Close();
            }


        }
        #endregion


        #region 刪除盤點紀錄

        public void Delete_Inventory(int Inv_number) 
        {
            //Sql刪除語法
            //根據Inv_number取得要刪除的資料
            //string mysqlDelSQL = "DELETE from pdi_inventory  where Inv_number=@Inv_number";
            var sql_delete = $@"delete from pdi_inventory where Inv_number=@Inv_number";

            try
            {
                //執行SQL指令
                SqlCommand cmd_del = new SqlCommand(sql_delete, pdi04_conn);
                cmd_del.Parameters.AddWithValue("@Inv_number", Inv_number);
                //開啟資料庫連線
                pdi04_conn.Open();
                cmd_del.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                //丟出錯誤
                throw new Exception(ex.Message.ToString());
            }
            finally
            {
                //關閉資料庫連線
                pdi04_conn.Close();
            }
        }
        #endregion





    }
}