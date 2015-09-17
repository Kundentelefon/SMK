using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMK.Model
{
    class PContent
    {
        public int content_ID { get; set; }
        public int content_Kind { get; set; }
        public String content_Title { get; set; }
        //Namen der Elemente im Orderner // FischerTechnik/PContent/0(ContentID)
        public List<String> content_FileNames { get; set; }

        public PContent(int content_ID, int content_Kind, String content_Title, List<String> content_FileNames)
        {
            this.content_ID = content_ID;
            this.content_Kind = content_Kind;
            this.content_FileNames = content_FileNames;
            this.content_Title = content_Title;
        }
    }
}
