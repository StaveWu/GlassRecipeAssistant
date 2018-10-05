using RecipeAssistant;
using RecipeAssistant.models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GlassRecipeAssistant.views
{
    public partial class QualityEnterBox : Form
    {
        private MainForm mainForm;

        public QualityEnterBox(MainForm form)
        {
            InitializeComponent();
            label2.Visible = false;

            textBox1.Text = "" + Settings.RawMaterialQuality;

            mainForm = form;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!StringUtils.isNumber(textBox1.Text))
            {
                textBox1.BackColor = Color.Yellow;
                label2.Visible = true;
            }
            else
            {
                Settings.RawMaterialQuality = Convert.ToDouble(textBox1.Text);
                mainForm.loadRecipes();
                mainForm.refreshRecipeInfoLabels();
                this.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
