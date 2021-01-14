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
    public class UserTableDBService
    {
        //建立資料庫的連線字串
        private readonly static string constr = ConfigurationManager.ConnectionStrings["pdi04"].ConnectionString;

        //建立資料庫的連線
        private readonly SqlConnection pdi04_conn = new SqlConnection(constr);

        #region 查詢成員資料表
        //根據搜尋來取得成員陣列資料
        public List<pdi_user> GetPdi_Users() 
        {
            //宣告要回傳的搜尋資料為資料庫中的成員資料表
            List<pdi_user> UserDataList = new List<pdi_user>();

            //SQL語法
            var sql_usertable = @"select * from pdi_user";

            //要設定例外狀況
            try
            {
                //開啟資料庫連線
                pdi04_conn.Open();

                //執行SQL指令
                SqlCommand sqlcmd_selectusertable = new SqlCommand(sql_usertable, pdi04_conn);

                //取得SQL資料
                SqlDataReader sqldr_usertable = sqlcmd_selectusertable.ExecuteReader();
                while (sqldr_usertable.Read()) 
                {
                    //呼叫user的model
                    pdi_user UserData = new pdi_user();
                    UserData.UserID = sqldr_usertable["UserID"].ToString();
                    UserData.UserPwd = sqldr_usertable["UserPwd"].ToString();
                    UserData.UserRank = Convert.ToInt32(sqldr_usertable["UserRank"]);

                    //把每一筆資料都放入到UserDataList
                    UserDataList.Add(UserData);
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
            //回傳資料庫中的成員資料表
            return UserDataList;
        }
        #endregion


        #region  新增使用者(註冊)
        //使用Linq的方式
        public void InsertNewUser(pdi_user new_data) 
        {
            //SQL新增語法 
            //var sql_inseruser = $@"insert into pdi_table(ProductKey,ProductName,Product_Category,RFID_Category,SafeAmount,InsertDate)";
        }
        #endregion


        #region 查詢一筆使用者資料
        //可透過此方法來進行"登入確認"
        private pdi_user GetDataByAccount(string Account) 
        {
            pdi_user user_data = new pdi_user();
            //SQL語法
            string sql = $@" select * from pdi_user where UserID=@UserID and UserPwd=@UserPwd";

            //設定例外，確保程式不會因執行錯誤而整個中斷
            try
            {
                //執行Sql指令
                SqlCommand cmd = new SqlCommand(sql, pdi04_conn);

                //隱藏碼攻擊問題。
                cmd.Parameters.AddWithValue("@UserID", user_data.UserID);
                cmd.Parameters.AddWithValue("@UserPwd", user_data.UserPwd);
                cmd.Parameters.AddWithValue("@UserPwd", user_data.UserRank);

                //開啟資料庫連線
                pdi04_conn.Open();
                //執行SQL，取得Sql資料
                SqlDataReader dr = cmd.ExecuteReader();
            }
            catch (Exception e)
            {
                //查無資料
                user_data = null;
            }
            finally
            {
                //關閉資料庫連線
                pdi04_conn.Close();
            }
            //回傳根據編號所取得的資料
            return user_data;
        }

        #endregion



        #region 登入確認
        //登入帳密確認方法，並回傳驗證後訊息
        public string LoginCheck(string Account, string Password)
        {
            //取得傳入帳號的會員資料
            pdi_user LoginMember = GetDataByAccount(Account);

            if (LoginMember !=null) 
            {
                
            }
            else 
            {
                return "無此會員帳號，請去註冊";
            }

            ////判斷是否有此會員
            //if (LoginMember != null)
            //{
            //   //進行帳號密碼確認
            //   if (PasswordCheck(LoginMember, Password))
            //    {
            //            return "";
            //    }
            //    else
            //    {
            //        return "此帳號尚未經過Email驗證，請去收信";
            //    }
            //}
            //else
            //{
            //    return "無此會員帳號，請去註冊";
            //}
        }
        #endregion

        #region 取得角色
        //取得會員的權限角色資料
        public string GetRole(string Account)
        {

            ////宣告初始角色字串
            //string Role = "User";
            ////取得傳入帳號的會員資料
            //pdi_user LoginMember = GetDataByAccount(Account);
            ////判斷資料庫欄位，用以確認是否為Admon
            //if (LoginMember.UserPwd.ToString())
            //{
            //    Role += ",Admin"; //添加Admin
            //}
            ////回傳最後結果
            //return Role;
        }
        #endregion


        #region 密碼確認
        //進行密碼確認方法
        public bool PasswordCheck(pdi_user CheckMember, string Password)
        {
            //判斷資料庫裡的密碼資料與傳入密碼資料Hash後是否一樣
            bool result = CheckMember.UserPwd.Equals(HashPassword(Password));
            //回傳結果
            return result;
        }
        #endregion







    }
}