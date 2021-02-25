using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//
using Product_Inventory0406.Models;
using System.ComponentModel;

namespace Product_Inventory0406.ViewMode
{
    public class PdiTableViewModel
    {
        //顯示資料陣列；此資料陣列為SQL內的資料表:pdi_table
        public List<pdi_table> DataList { get; set; }

        //搜尋產品編號
        [DisplayName("搜尋")]
        public string Search_Product { get; set; }
    }
}