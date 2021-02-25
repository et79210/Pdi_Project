using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace Product_Inventory0406.Models
{
    public class pdi_inventory
    {
        //盤點編號，自動新增
        public int Inv_number { get; set; }

        //產品ID
        [DisplayName("產品編號")]
        [Required(ErrorMessage = "請輸入產品編號")]
        [StringLength(50, ErrorMessage = "編號名稱不可大於50字元")]
        public string ProductKey { get; set; }

        //盤點數量
        [DisplayName("盤點數量")]
        public int NewProductNumber { get; set; }

        //盤點日期
        [DisplayName("盤點時間")]
        public DateTime InventoryDate { get; set; }


        //研究是否需要新增---------------------------------

        [DisplayName("基本數量")]
        public int SafeAmount { get; set; }


        [DisplayName("產品名稱")]
        [Required(ErrorMessage = "請輸入產品名稱")]
        [StringLength(50, ErrorMessage = "名稱不可大於50字元")]
        public string ProductName { get; set; }
        //研究是否需要新增---------------------------------

    }
}