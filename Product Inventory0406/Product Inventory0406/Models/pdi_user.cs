using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

//
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Product_Inventory0406.Models
{
    //成員資料表的內容
    public class pdi_user
    {
        [DisplayName("帳號資訊")]
        [Required(ErrorMessage = "請輸入帳號資訊")]
        public string UserID { get; set; }

        [DisplayName("密碼")]
        [Required(ErrorMessage = "請輸入密碼")]
        public string UserPwd { get; set; }

        public int UserRank { get; set; }
    }
}