using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace FishBall01.Models
{
    public class FishBall_Table
    {
        //編號
        [DisplayName("產品編號：")]
        [Required(ErrorMessage = "請輸入產品編號")]
        [StringLength(50, ErrorMessage = "編號名稱不可大於50字元")]
        public string ID { get; set; }
        //get 取值
        //set 給值
        //可以在內部做預設值的使用

        //商品名稱
        [DisplayName("產品名稱：")]
        [Required(ErrorMessage = "請輸入產品名稱")]
        [StringLength(50, ErrorMessage = "名稱不可大於50字元")]
        public string Name { get; set; }

        //商品圖示-路徑
        [DisplayName("商品圖示：")]
        public string PictureURL { get; set; }

        //商品價格
        [DisplayName("商品價格：")]
        public int Price { get; set; }

        //上市日期
        [DisplayName("上市日期：")]
        public DateTime CreateTime { get; set; }

        //簡介
        [DisplayName("簡介" +
            "：")]
        public string Intro { get; set; }

    }
}