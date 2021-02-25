using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//
using Product_Inventory0406.Models;

namespace Product_Inventory0406.ViewMode
{
    public class InvTableViewModel
    {
        //顯示資料陣列；此資料陣列為SQL內的資料表:pdi_inventory
        public List<pdi_inventory> DataList { get; set; }
    }
}