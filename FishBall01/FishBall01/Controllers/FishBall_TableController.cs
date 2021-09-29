using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
//
using FishBall01.Services;
using FishBall01.ViewModels;
using FishBall01.Models;

namespace FishBall01.Controllers
{
    public class FishBall_TableController : Controller
    {
        //宣告FishBall_Table資料表的Service物件
        private readonly FishBallDBService FishBall_service = new FishBallDBService();

        //產品資料首頁介紹---------------------------------------------------------------------------
        // GET: FishBall_Table
        public ActionResult Index()
        {
            //宣告一個新的頁面模型
            FishBallTableViewModel DataViewModel = new FishBallTableViewModel();
            //從Serivce中取得所有頁面所需要的陣列資料，然後放入到ViewModel中
            DataViewModel.DataList = FishBall_service.GetFishBall_DataList();
            //設定要顯示的頁面、版型、屬性
            return View("Index", "_Layout", DataViewModel);
        }
        //產品資料表頁---------------------------------------------------------------------------


        #region 新增產品資料頁面(能對資料庫內進行更改-唯有管理者權限才可以)
        //使用多載方式建立於新增中兩個同名Action方法，一個用於一開始頁面顯示，
        //另一個用於接受傳入的資料，並將資料寫入資料庫中。
        public ActionResult Create() 
        {
            return View("Create", "_Layout");
        }
        [HttpPost]
        public ActionResult Create(FishBall_Table fishb_table_data)
        {
            //使用Service來新增資料
            FishBall_service.InsertFishBall_Data(fishb_table_data);

            return RedirectToAction("Index");
        }

        #endregion

        #region 產品資料編輯頁面(能對資料庫內進行更改-唯有管理者權限才可以)




        #endregion


    }
}