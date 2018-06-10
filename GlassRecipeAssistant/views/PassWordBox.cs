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
    public partial class PassWordBox : Form
    {
        public PassWordBox()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {// 确定
            if (textBox1.Text.Equals(Settings.Password))
            {
                if (textBox2.Text.Equals(textBox3.Text))
                {
                    
                    Settings.Password = textBox2.Text;
                    this.Close();
                }
                else
                {
                    label4.Text = "新密码不一致！";
                }
            }
            else
            {
                label4.Text = "密码错误！";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {// 取消
            this.Close();
        }
    }
}
