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


            //將頁面資料傳入View中
            return View(userviewmodel);
        }

        #endregion


        #region 登入
        public ActionResult Login() 
        {
        
        
        }

        //穿入登入資料的Action
        [HttpPost]
        public ActionResult Login() 
        {
        
        
        
        }


        #endregion



        #region 登出


        #endregion


    }
}