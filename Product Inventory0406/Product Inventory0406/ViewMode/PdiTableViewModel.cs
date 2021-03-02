using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

//新增
using Product_Inventory0406.Models;
using Product_Inventory0406.Services;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Product_Inventory0406.ViewMode
{
    public class PdiTableViewModel
    {
        //顯示資料陣列；此資料陣列為SQL內的資料表:pdi_table
        public List<pdi_table> DataList { get; set; }

        //搜尋產品編號
        [DisplayName("查詢的產品編號：")]
        [Required]
        [StringLength(20, ErrorMessage = "不可輸入超過20字元")]
        public string Search_Product { get; set; }


        //分頁內容
        public ForPaging Paging { get; set; }
    }
}