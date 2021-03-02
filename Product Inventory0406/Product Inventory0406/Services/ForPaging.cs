using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Product_Inventory0406.Services
{
    public class ForPaging
    {
        //當前頁數
        public int NowPage { get; set; }

        //最大頁數
        public int MaxPage { get; set; }

        //分頁項目個數

        public int Item 
        {
            get { return 4; }
        }



        //此類別建構式
        public ForPaging() 
        {
            //預設當前頁數為1
            this.NowPage = 1;        
        }

        //此類別建構式，包含傳入頁數
        public ForPaging(int Page) 
        {
            this.NowPage = Page;
        }


        //設定正確頁數的方法，以避免傳入不正確數值
        public void SetRightPage() 
        {
            //判斷當前頁數是否小於1
            if (this.NowPage < 1)
            {
                //將頁數設回1
                this.NowPage = 1;
            }

            //判斷當前頁數是否大於總頁數
            else if (this.NowPage > this.MaxPage)  
            {
                //設定當前頁數為總頁數
                this.NowPage = this.MaxPage;
            }

            //將無資料的當前頁數，重設回1
            if (this.MaxPage.Equals(0)) 
            {
                this.NowPage = 1;
            }
        }

    }
}