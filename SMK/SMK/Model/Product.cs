using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMK.Model
{
    class Product
    {

        public int product_ID { get; set; }
        public String product_Name { get; set; }
        public String product_Thumbnail { get; set; }
        public String product_Text { get; set; }
        public List<int> product_PContents { get; set; }

    }
}
