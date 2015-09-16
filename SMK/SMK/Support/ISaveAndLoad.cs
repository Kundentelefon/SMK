using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMK.Support
{
    public interface ISaveAndLoad
    {
                //        var saveButton = new Button { Text = "Save" };
                //        saveButton.Clicked += (sender, e) => {
                //    DependencyService.Get<ISaveAndLoad>().SaveText("temp.txt", input.Text);
                //    };
                //    var loadButton = new Button { Text = "Load" };
                //    loadButton.Clicked += (sender, e) => {
                //    output.Text = DependencyService.Get<ISaveAndLoad>().LoadText("temp.txt");
                //};
        void SaveText(string filename, string text);
        string LoadText(string filename);
    }
}
