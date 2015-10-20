using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMK.Model
{
    public class PContent
    {
        public int content_ID { get; set; }
        public int content_Kind { get; set; }
        public String content_Title { get; set; }
        public int product_ID { get; set; }

        public List<string> files;

        public string content_path { get; set; }
    }
}
