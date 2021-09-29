using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//新增
using FishBall01.Models;
using FishBall01.Services;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FishBall01.ViewModels
{
    public class FishBallTableViewModel
    {
        //顯示資料陣列
        public List<FishBall_Table> DataList { get; set; }

    }
}