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
        private static string domain = AppDomain.CurrentDomain
            .SetupInformation.ApplicationBase + "/log/";

        public void write(string clientName, string glassName, 
            Dictionary<string, double[]> recipes)
        {
            string pathname = domain + clientName + "_" + glassName + ".txt";

            StreamWriter sw = null;
            try
            {
                if (!File.Exists(pathname))
                {
                    sw = new StreamWriter(pathname, true);
                    // 写入表头
                    string header = "";
                    foreach (string ele in recipes.Keys)
                    {
                        header += ele;
                        header += "\t";
                    }
                    header += "Time";
                    sw.WriteLine(header);
                }
                else
                {
                    sw = new StreamWriter(pathname, true);
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
                sw.WriteLine(data);
            }
            finally
            {
                if (sw != null)
                {
                    sw.Flush();
                    sw.Close();
                }
            }
        }
    }
}
