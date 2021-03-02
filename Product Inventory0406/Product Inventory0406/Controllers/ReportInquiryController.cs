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
    public class ReportInquiryController : Controller
    {
        //宣告pdi_table資料表的Service物件
        private readonly ReportTableDBService report_service = new ReportTableDBService();

        // GET: ReportInquiry
        public ActionResult Index()
        {

            //宣告一個新的頁面模型
            ReportTableViewModel reprot_viewmodel = new ReportTableViewModel();

            //從Serivce中取得所有頁面所需要的陣列資料，然後放入到ViewModel中
            reprot_viewmodel.Data_ReportInquiry_List = report_service.GetPdi_Reports();

            //若Session["UserID"]為空，表示會員未登入
            if (Session["Member"] == null)
            {
                //ViewBag.Message = "尚未登入";
                //導入到登入畫面
                return RedirectToAction("Login", "UserTable");
            }
            //會員登入狀態
            return View("Index", "_LayoutMember", reprot_viewmodel);
        }

        [HttpPost]
        public ActionResult Index(string Search_Product_Report) 
        {
            //宣告一個新的頁面模型
            ReportTableViewModel reprot_viewmodel = new ReportTableViewModel();

            //從Serivce中取得所有頁面所需要的陣列資料，然後放入到ViewModel中
            reprot_viewmodel.Data_ReportInquiry_List = report_service.GetPdi__ID_Reports(Search_Product_Report);

            //將資料傳入View中
            return View("Index", "_LayoutMember", reprot_viewmodel);

        }

    }
}