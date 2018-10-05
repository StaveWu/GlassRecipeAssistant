using GlassRecipeAssistant.dao.entities;
using GlassRecipeAssistant.models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GlassRecipeAssistant.views
{
    public partial class PowderAddBox : Form
    {
        private IPowderModel model;

        public PowderAddBox(IPowderModel model)
        {
            InitializeComponent();

            this.model = model;
        }

        private void button1_Click(object sender, EventArgs e)
        {// 确定按钮
            string text = textBox1.Text;
            if (text.Equals(""))
            {
                label2.Visible = true;
            }
            else
            {
                if (model.contains(text))
                {
                    MessageBox.Show("色粉名不允许重复");
                }
                else
                {
                    model.addPowder(new Powder(text));
                    this.Close();
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {// 取消按钮
            this.Close();
        }
    }
}
