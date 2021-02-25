using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
//新增
using Product_Inventory0406.Models;
using Product_Inventory0406.Services;
using Product_Inventory0406.ViewMode;


namespace Product_Inventory0406.Controllers
{
    public class UserTableController : Controller
    {
        //宣告pdi_user資料表的Service物件
        private readonly UserTableDBService usertable_services = new UserTableDBService();

        #region 會員列表
        // GET: UserTable
        public ActionResult Index()
        {

            //宣告一個新的頁面模型
            UserTableViewModel userviewmodel = new UserTableViewModel();

            //從Serivce中取得所有頁面所需要的陣列資料，然後放入到ViewModel中
            userviewmodel.DataList = usertable_services.GetPdi_Users();


            //若Session["UserID"]為空，表示會員未登入， 就算登入，若權限沒有為最大者，一樣踢出去
            if (Session["Member"] == null  || Convert.ToInt32(Session["Rank"]) != 5)
            {
                //建議跳出訊息使用js來顯示
                //return View("Index", "_Layout", pdiviewmodel);
                //導入到登入畫面
                return RedirectToAction("Index", "PdiTable");
            }
            ////會員登入狀態，將頁面資料傳入View中
            return View("Index", "_LayoutMember", userviewmodel);
        }

        #endregion


        #region 登入
        public ActionResult Login() 
        {
            ////判斷使用者是否已經過登入驗證
            //if (User.Identity.IsAuthenticated) 
            //{
            //    return RedirectToAction("Index", "PdiTable"); //已登入則重新導向
            //}
            //return View();//否則進入登入畫面

            return View();
        }

        //穿入登入資料的Action
        [HttpPost]
        public ActionResult Login(UserLoginViewModel LoginUser) 
        {
            //使用Service裡的方法來驗證登入的帳號密碼
            string ValidateStr = usertable_services.LoginCheck(LoginUser.UserID, LoginUser.UserPwd);

            string Login_Rank = usertable_services.GetRole(LoginUser.UserID);

            //判斷驗證後結果是否有錯誤訊息
            //透過回傳若為無訊息
            if (String.IsNullOrEmpty(ValidateStr))
            {
                //使用Session變數記錄歡迎詞
                Session["Welcome"] = LoginUser.UserID + " 歡迎光臨";
                //使用Session變數記錄登入的會員物件
                Session["Member"] = LoginUser;

                Session["UserID"] = LoginUser.UserID;

                Session["Rank"] = Login_Rank;

                //取得權限部分
                //設定一小段程式碼來取得使用者的權限等級
                //pdi_user userdata = LoginUser.UserID
                //Session["Rank"] = userdata.Rank


                //再做一個判斷權限的資料
                return RedirectToAction("Index", "PdiTable");
            }
            else
            {
                ViewBag.Message = "資料有誤，登入失敗";

                //有驗證錯誤訊息，加入頁面模型中
                //ModelState.AddModelError("", ValidateStr);

                //將資料回填至View中
                //return View(LoginUser);
                return View();
            }
        }
        #endregion

        #region 登出
        public ActionResult Logout() 
        {
            Session.Clear();
            return RedirectToAction("Login");
        
        }
        #endregion


    }
}