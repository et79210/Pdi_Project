using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//新增，與資料庫的連線字串
using System.Configuration;
using System.Data.SqlClient;
using FishBall01.Models;

namespace FishBall01.Services
{
    public class FishBallDBService
    {
        //建立資料庫的連線字串
        private readonly static string constr = ConfigurationManager.ConnectionStrings["FishBall"].ConnectionString;

        //建立資料庫的連線
        private readonly SqlConnection FishBallDB_conn = new SqlConnection(constr);


        #region 查詢產品資料表
        public List<FishBall_Table> GetFishBall_DataList() 
        {
            //宣告要回傳的搜尋資料為資料庫中的產品資料表
            List<FishBall_Table> FishBall_DataList = new List<FishBall_Table>();

            //SQL語法
            var sql_sel_FishBtable = @"select * from FishBall_Table";

            //設定例外狀況
            try 
            {
                //開啟資料庫
                FishBallDB_conn.Open();

                //執行SQL指令
                SqlCommand sqlcmd_sel_FishBtable = new SqlCommand(sql_sel_FishBtable, FishBallDB_conn);

                //取得SQL資料
                SqlDataReader sqldr_FIBtable = sqlcmd_sel_FishBtable.ExecuteReader();
                while (sqldr_FIBtable.Read()) 
                {
                    FishBall_Table FishBall_Data = new FishBall_Table();

                    FishBall_Data.ID = sqldr_FIBtable["ID"].ToString();
                    FishBall_Data.Name = sqldr_FIBtable["Name"].ToString();
                    FishBall_Data.PictureURL = sqldr_FIBtable["PictureURL"].ToString();
                    FishBall_Data.Price = Convert.ToInt32(sqldr_FIBtable["Price"].ToString());
                    FishBall_Data.CreateTime = Convert.ToDateTime(sqldr_FIBtable["CreateTime"].ToString());
                    FishBall_Data.Intro = sqldr_FIBtable["Intro"].ToString();

                    FishBall_DataList.Add(FishBall_Data);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
            finally
            {
                //關閉資料庫連線
                FishBallDB_conn.Close();
            }
            //回傳資料庫中的產品資料表
            return FishBall_DataList;
        }
        #endregion

        #region 新增產品資料表
        public void InsertFishBall_Data(FishBall_Table new_data) 
        {
            //SQL新增語法
            var sql_insert_FishBtable = $@"insert into FishBall_Table(ID,Name,PictureURL,Price,CreateTime,Intro)" 
                                        + "value(@ID,@Name,@PictureURL,@Price,@CreateTime,@Intro)";
            try 
            {
                SqlCommand sql_cmd_insert = new SqlCommand(sql_insert_FishBtable, FishBallDB_conn);
                //注意隱藏碼攻擊
                sql_cmd_insert.Parameters.AddWithValue("@ID", new_data.ID);
                sql_cmd_insert.Parameters.AddWithValue("@Name", new_data.Name);
                sql_cmd_insert.Parameters.AddWithValue("@PictureURL", new_data.PictureURL);
                sql_cmd_insert.Parameters.AddWithValue("@Price", new_data.Price);
                sql_cmd_insert.Parameters.AddWithValue("@CreateTime", new_data.CreateTime);
                sql_cmd_insert.Parameters.AddWithValue("@Intro", new_data.Intro);

                //開啟資料庫
                FishBallDB_conn.Open();
                //執行SQL指令
                sql_cmd_insert.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                //丟出錯誤
                throw new Exception(ex.Message.ToString());
            }
            finally
            {
                //關閉資料庫
                FishBallDB_conn.Close();
            }

        }

        #endregion


    }
}