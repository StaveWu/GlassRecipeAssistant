using GlassRecipeAssistant.models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GlassRecipeAssistant.views
{
    public partial class HistoryBox : Form
    {

        public HistoryBox(string clientName, string glassName)
        {
            InitializeComponent();

            string path = System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "/log/" + clientName + "_" + glassName + ".txt";

            string content = "";
            using (StreamReader sr = new StreamReader(path))
            {
                string line = null;
                while ((line = sr.ReadLine()) != null)
                {
                    content += line;
                    content += "\r\n";
                }

            }

            richTextBox1.Text = content;

        }

    }
}
