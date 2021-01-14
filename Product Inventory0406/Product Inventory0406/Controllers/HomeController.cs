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
    public class HomeController : Controller
    {
        //宣告pdi_table資料表的Service物件
        private readonly PdiTableDBService pditabele_service = new PdiTableDBService();

        // GET: Home，首頁
        public ActionResult Index()
        {
            //宣告一個新的頁面模型
            PdiTableViewModel pdiviewmodel = new PdiTableViewModel();
            //從Serivce中取得所有頁面所需要的陣列資料，然後放入到ViewModel中
            pdiviewmodel.DataList = pditabele_service.GetPdi_Tables();

            //未登入狀態
            return View("Index", "_Layout", pdiviewmodel);
        }
    }
}