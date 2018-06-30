using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeAssistant.models
{
    class TextLogger : ILogger
    {
        public void write(string clientName, string glassName, 
            Dictionary<string, double[]> recipes)
        {
            string path = System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "/log/" + clientName +"_" + glassName + ".txt";

            StreamWriter sw;
            if (!File.Exists(path))
            {
                sw = new StreamWriter(path, true);
                // 写入表头
                string top = "";
                foreach (string ele in recipes.Keys)
                {
                    top += ele;
                    top += "\t";
                }
                top += "Time";
                sw.WriteLine(top.Substring(0, top.Length));
            }
            else
            {
                sw = new StreamWriter(path, true);
            }
            string data = "";
            foreach (string ele in recipes.Keys)
            {
                data += recipes[ele][0];
                data += "/";
                data += recipes[ele][1];
                data += "\t";
            }
            data += DateTime.Now.ToString();
            sw.WriteLine(data.Substring(0, data.Length));

            sw.Flush();
            sw.Close();
        }
    }
}
