using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//新增
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Product_Inventory0406.ViewMode
{

    //登入用ViewModel
    public class UserLoginViewModel
    {
        [DisplayName("會員帳號")]
        [Required(ErrorMessage = "請輸入會員帳號")]
        public string UserID { get; set; }

        [DisplayName("會員密碼")]
        [Required(ErrorMessage = "請輸入密碼")]
        public string UserPwd { get; set; }

    }
}