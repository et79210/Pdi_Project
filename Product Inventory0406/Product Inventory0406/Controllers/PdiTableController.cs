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
    public class PdiTableController : Controller
    {
        //宣告pdi_table資料表的Service物件
        private readonly PdiTableDBService pditabele_service = new PdiTableDBService();



        //產品資料表頁---------------------------------------------------------------------------
        // GET: PdiTable
        public ActionResult Index()
        {
            //宣告一個新的頁面模型
            PdiTableViewModel pdiviewmodel = new PdiTableViewModel();

            //從Serivce中取得所有頁面所需要的陣列資料，然後放入到ViewModel中
            pdiviewmodel.DataList = pditabele_service.GetPdi_Tables();


            //若Session["UserID"]為空，表示會員未登入
            if (Session["Member"] == null)
            {
                return View("Index", "_Layout", pdiviewmodel);
            }
            //會員登入狀態
            return View("Index", "_LayoutMember", pdiviewmodel);
        }
        //產品資料表頁---------------------------------------------------------------------------

        //查詢單一產品資料------------------------------------------------------------------------------------------------------
        #region 查詢單一產品資料
        public ActionResult SelectProduct(string Select_Key)
        {
            //要先根據編號來載入資料
            pdi_table pdi_data = pditabele_service.GetDataByKey(Select_Key);

            

            //將資料傳入View中
            return View(pdi_data);
        }

        //[HttpPost]
        //public ActionResult SelectProduct(string Select_Key) 
        //{
        //    //宣告一個新的頁面模型
        //    PdiTableViewModel pdiviewmodel = new PdiTableViewModel();
        //    //要先根據編號來載入資料
        //    pdi_table pdi_data = pditabele_service.GetDataByKey(Select_Key);

        //    //重新導向
        //    return View(pdi_data);

        //}

        #endregion
        //查詢單一產品資料------------------------------------------------------------------------------------------------------


        #region 新增產品資料
        //新增產品資料---------------------------------------------------------------------------
        //使用多載方式建立於新增中兩個同名Action方法，一個用於一開始頁面顯示，
        //另一個用於接受傳入的資料，並將資料寫入資料庫中。
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
        public ActionResult Create(string ProductKey, string ProductName, string Product_Category, string RFID_Category, int SafeAmount, DateTime InsertDate) 
        {

            pdi_table pdidata = new pdi_table();
            pdidata.ProductKey = ProductKey;
            pdidata.ProductName = ProductName;
            pdidata.Product_Category = Product_Category;
            pdidata.RFID_Category = RFID_Category;
            pdidata.SafeAmount = SafeAmount;
            pdidata.InsertDate = InsertDate;

            //使用Service來新增資料
            pditabele_service.InsertProduct(pdidata);

            return RedirectToAction("Index");
        }
        //新增產品資料---------------------------------------------------------------------------
        #endregion

        #region 刪除產品資料
        //刪除產品資料-------------------------------------------------------------------------------------------------------
        public ActionResult Delete(string ProductKey) 
        {
            //使用Service來刪除資料
            pditabele_service.DeleteProduct(ProductKey);

            //重新導向首頁
            return RedirectToAction("Index");
        
        }
        //刪除產品資料-------------------------------------------------------------------------------------------------------
        #endregion



        #region 修改產品資料
        //修改產品資料------------------------------------------------------------------------------------------------------
        public ActionResult Edit(string ProductKey) 
        {
            //要先根據編號來載入資料
            pdi_table pdi_data = pditabele_service.GetDataByKey(ProductKey);

            //將資料傳入View中
            return View(pdi_data);
        }

        [HttpPost] //使用Bind的Inculde來定義只接受的欄位，用來避免傳入其他不相干值
        public ActionResult Edit(string ProductKey,pdi_table update_pdidata) 
        {

            update_pdidata.ProductKey = ProductKey;

            pditabele_service.EditProduct(update_pdidata);

            //重新導向頁面至開始頁面
            return RedirectToAction("Index");

        }
        //修改產品資料------------------------------------------------------------------------------------------------------
        #endregion


    }
}