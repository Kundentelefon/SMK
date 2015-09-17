using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMK.Model
{
    public class Product
    {

        public int product_ID { get; set; }
        public String product_Name { get; set; }
        public String product_Thumbnail { get; set; }
        public String product_Text { get; set; }
        public List<int> product_PContents { get; set; }

        public Product(int product_ID, String product_Name, String product_Thumbnail, String product_Text, List<int> product_PContents)
        {
            this.product_ID = product_ID;
            this.product_Name = product_Name;            
            this.product_Text = product_Text;
            this.product_Thumbnail = product_Thumbnail;
            this.product_PContents = product_PContents;
        }

    }
}
