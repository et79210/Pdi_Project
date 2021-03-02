using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Product_Inventory0406.Models
{

    //產品資料表
    public class pdi_table
    {

        [DisplayName("產品編號：")]
        [Required(ErrorMessage = "請輸入產品編號")]
        [StringLength(50, ErrorMessage = "編號名稱不可大於50字元")]
        public string ProductKey { get; set; }
        //get 取值
        //set 給值
        //可以在內部做預設值的使用。


        [DisplayName("產品名稱：")]
        [Required(ErrorMessage = "請輸入產品名稱")]
        [StringLength(50, ErrorMessage = "名稱不可大於50字元")]
        public string ProductName { get; set; }

        [DisplayName("產品種類：")]
        public string Product_Category { get; set; }

        [DisplayName("RFID規格：")]
        public string RFID_Category { get; set; }

        [DisplayName("基本數量：")]
        [Required]
        public int SafeAmount { get; set; }

        [DisplayName("上市日期：")]
        [Required]
        public DateTime InsertDate { get; set; }

    }
}