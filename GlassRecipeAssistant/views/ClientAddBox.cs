using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RecipeAssistant.models
{
    public partial class ClientAddBox : Form
    {
        private IGlassRecipesModel model;

        public ClientAddBox(IGlassRecipesModel model)
        {
            InitializeComponent();
            label2.Visible = false;
            this.model = model;
        }

        private void addClient()
        {
            if (model.containsClient(textBox1.Text))
            {
                textBox1.BackColor = Color.Yellow;
                label2.Visible = true;
            }
            else
            {
                textBox1.BackColor = Color.White;
                label2.Visible = false;
                model.addClient(textBox1.Text);
                this.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {// 添加按钮
            addClient();
        }

        private void button2_Click(object sender, EventArgs e)
        {// 取消按钮
            this.Close();
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                addClient();
            }
        }
    }
}
