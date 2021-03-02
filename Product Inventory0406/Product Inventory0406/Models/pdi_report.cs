using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Product_Inventory0406.Models
{
    public class pdi_report
    {

        public string ProductKey { get; set; }

        public string ProductName { get; set; }


        public string Product_Category { get; set; }


        public string RFID_Category { get; set; }

        //盤點數量
        public int NewProductNumber { get; set; }

        //盤點日期
        public DateTime InventoryDate { get; set; }



    }
}