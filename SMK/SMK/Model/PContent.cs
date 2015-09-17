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
        //Namen der Elemente im Orderner // FischerTechnik/PContent/0(ContentID)
        public List<String> content_FileNames { get; set; }


    }
}
