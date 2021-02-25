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
    public class InventoryTableController : Controller
    {

        //宣告pdi_inventory資料表的Service物件
        private readonly InventoryTableDBService invtable_service = new InventoryTableDBService();


        //盤點資料表頁---------------------------------------------------------------------------
        // GET: InventoryTable
        public ActionResult Index()
        {
            //宣告一個新的頁面模型
            InvTableViewModel inv_viewmodel = new InvTableViewModel();
            //從Serivce中取得所有頁面所需要的陣列資料，然後放入到ViewModel中
            inv_viewmodel.DataList = invtable_service.GetInv_Tables();


            //若Session["UserID"]為空，表示會員未登入
            if (Session["Member"] == null)
            {
                return View("Index", "_Layout", inv_viewmodel);
            }
            //會員登入狀態
            return View("Index", "_LayoutMember", inv_viewmodel);
        }
        //盤點資料表頁---------------------------------------------------------------------------


        #region 刪除盤點資料
        //刪除產品資料-------------------------------------------------------------------------------------------------------
        public ActionResult Delete(int Inv_number)
        {
            //使用Service來刪除資料
            invtable_service.Delete_Inventory(Inv_number);

            //重新導向首頁
            return RedirectToAction("Index");

        }
        //刪除產品資料-------------------------------------------------------------------------------------------------------
        #endregion


        #region
        public ActionResult Create() 
        {
            //若Session["UserID"]為空，表示會員未登入
            if (Session["Member"] == null)
            {
                ViewBag.Message = "尚未登入";
                //導入到登入畫面
                return RedirectToAction("Login", "UserTable");
            }
            //會員登入狀態
            return View("Create", "_LayoutMember");

        }

        [HttpPost]
        public ActionResult Create(string ProductKey, int NewProductNumber, DateTime InventoryDate) 
        {

            pdi_inventory inventory_data = new pdi_inventory();
            inventory_data.ProductKey = ProductKey;
            inventory_data.NewProductNumber = NewProductNumber;
            inventory_data.InventoryDate = InventoryDate;


            //使用Service來新增資料
            invtable_service.Insert_Inventory(inventory_data);

            return RedirectToAction("Index");
        }

        #endregion


    }
}