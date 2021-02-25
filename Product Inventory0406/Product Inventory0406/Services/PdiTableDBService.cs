using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//新增，與資料庫的連線字串
using Product_Inventory0406.Models;
using System.Configuration;
using System.Data.SqlClient;

namespace Product_Inventory0406.Services
{
    public class PdiTableDBService
    {
        //建立資料庫的連線字串
        private readonly static string constr = ConfigurationManager.ConnectionStrings["pdi04"].ConnectionString;

        //建立資料庫的連線
        private readonly SqlConnection pdi04_conn = new SqlConnection(constr);

        #region 查詢產品資料表
        //根據搜尋來取得成員陣列資料
        public List<pdi_table> GetPdi_Tables() 
        {
            //宣告要回傳的搜尋資料為資料庫中的產品資料表
            List<pdi_table> PdiDataList = new List<pdi_table>();

            //sql語法
            var sql_select_pditable = @"select * from pdi_table";

            //設定例外狀況
            try 
            {
                pdi04_conn.Open();

                //執行SQL指令
                SqlCommand sqlcmd_select_pditable = new SqlCommand(sql_select_pditable,pdi04_conn) ;

                //取得SQL資料
                SqlDataReader sqldr_pditable = sqlcmd_select_pditable.ExecuteReader();
                while (sqldr_pditable.Read()) 
                {
                    pdi_table PdiData = new pdi_table();
                    PdiData.ProductKey = sqldr_pditable["ProductKey"].ToString();
                    PdiData.ProductName = sqldr_pditable["ProductName"].ToString();
                    PdiData.Product_Category = sqldr_pditable["Product_Category"].ToString();
                    PdiData.RFID_Category = sqldr_pditable["RFID_Category"].ToString();
                    PdiData.SafeAmount = Convert.ToInt32(sqldr_pditable["SafeAmount"]);
                    PdiData.InsertDate = Convert.ToDateTime(sqldr_pditable["InsertDate"]);

                    PdiDataList.Add(PdiData);
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
            //回傳資料庫中的產品資料表
            return PdiDataList;
        }
        #endregion

        #region 新增產品資料
        public void InsertProduct(pdi_table new_data) 
        {
            //SQL新增語法
            //var sql_insertproduct = $@"insert into pdi_table(ProductKey,ProductName,Product_Category,RFID_Category,SafeAmount,InsertDate)  values('{new_data.ProductKey}','{new_data.ProductName}','{new_data.Product_Category}','{new_data.RFID_Category}','{new_data.SafeAmount}','{new_data.InsertDate.ToString("yyyy-MM-dd HH:mm:ss")}')";
            //string sql = $@" INSERT INTO Guestbooks(Account,Content,CreateTime) VALUES ( '{newData.Account}','{newData.Content}','{DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")}' ) ";
            var sql_insertproduct = $@"insert into pdi_table(ProductKey,ProductName,Product_Category,RFID_Category,SafeAmount,InsertDate)" + "values(@ProductKey,@ProductName,@Product_Category,@RFID_Category,@SafeAmount,@InsertDate)";
            try 
            {
                //執行SQL指令
                SqlCommand cmd = new SqlCommand(sql_insertproduct,pdi04_conn);
                //隱藏碼攻擊問題。
                cmd.Parameters.AddWithValue("@ProductKey", new_data.ProductKey);
                cmd.Parameters.AddWithValue("@ProductName", new_data.ProductName);
                cmd.Parameters.AddWithValue("@Product_Category", new_data.Product_Category);
                cmd.Parameters.AddWithValue("@RFID_Category", new_data.RFID_Category);
                cmd.Parameters.AddWithValue("@SafeAmount", new_data.SafeAmount);
                cmd.Parameters.AddWithValue("@InsertDate", new_data.InsertDate.ToString("yyyy-MM-dd HH:mm:ss"));

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


        #region 刪除產品資料
        public void DeleteProduct(string ProductKey) 
        {
            //Sql刪除語法
            //根據ProductKey取得要刪除的資料
            //string mysqlDelSQL = "Delete from products where ProductKey = @ProductKey";
            var sql_delete = $@"delete from pdi_table where ProductKey =  @ProductKey";
            try 
            {

                //執行SQL指令
                SqlCommand cmd_del = new SqlCommand(sql_delete,pdi04_conn);
                cmd_del.Parameters.AddWithValue("@ProductKey", ProductKey);
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

        #region 查詢單筆資料
        //藉由編號查詢單筆資料，此方法可用來修改產品資料，也可用來新增查詢某資料並顯示
        public pdi_table GetDataByKey(string DataKey) 
        {
            pdi_table pdi_data = new pdi_table();
            //sql語法
            // where ProductKey=@ProductKey
            var sql_search = $@"select * from pdi_table where  where ProductKey=@ProductKey";
            

            //確保程式不會因為執行錯誤而中斷
            try
            {
                //開啟資料庫
                pdi04_conn.Open();

                //執行SQL指令
                SqlCommand sqlcmd_select_pdidata = new SqlCommand(sql_search, pdi04_conn);
                // update_cmd.Parameters.AddWithValue("@ProductKey", update_data.ProductKey);
                sqlcmd_select_pdidata.Parameters.AddWithValue("@ProductKey",DataKey);

                //取得SQL資料
                SqlDataReader sqldr_pditable = sqlcmd_select_pdidata.ExecuteReader();
                sqldr_pditable.Read();

                pdi_data.ProductKey = sqldr_pditable["ProductKey"].ToString();
                pdi_data.ProductName = sqldr_pditable["ProductName"].ToString();
                pdi_data.Product_Category = sqldr_pditable["Product_Category"].ToString();
                pdi_data.RFID_Category = sqldr_pditable["RFID_Category"].ToString();
                pdi_data.SafeAmount = Convert.ToInt32(sqldr_pditable["SafeAmount"]);
                pdi_data.InsertDate = Convert.ToDateTime(sqldr_pditable["InsertDate"]);

            }
            catch (Exception ex)
            {
                //查無資料
                pdi_data = null;
            }
            finally 
            {
                //關閉資料庫
                pdi04_conn.Close();
            }

            //回傳根據編號所取得的資料
            return pdi_data;


        }

        #endregion


        #region  修改產品資料
        public void EditProduct(pdi_table update_data) 
        {
            //Sql修改語法
            var sql_update = $@"update pdi_table set  ProductName=@ProductName,Product_Category=@Product_Category,RFID_Category=@RFID_Category,SafeAmount=@SafeAmount,InsertDate=@InsertDate where ProductKey=@ProductKey";
            //Update products set ProductName=@ProductName,Category=@Category,RFID_Category=@RFID_Category,SafeAmount=@SafeAmount where ProductKey=@ProductKey

            //確保程式不會因執行錯誤而整個中斷
            try
            {
                //執行SQL指令
                SqlCommand update_cmd = new SqlCommand(sql_update,pdi04_conn);
                update_cmd.Parameters.AddWithValue("@ProductName",update_data.ProductName);
                update_cmd.Parameters.AddWithValue("@Product_Category", update_data.Product_Category);
                update_cmd.Parameters.AddWithValue("@RFID_Category", update_data.RFID_Category);
                update_cmd.Parameters.AddWithValue("@SafeAmount", update_data.SafeAmount);
                update_cmd.Parameters.AddWithValue("@InsertDate", update_data.InsertDate);
                update_cmd.Parameters.AddWithValue("@ProductKey", update_data.ProductKey);
                //做完確認後要開啟連結把SQL語法帶進去

                //開啟資料庫連線
                pdi04_conn.Open();

                update_cmd.ExecuteNonQuery();
                

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