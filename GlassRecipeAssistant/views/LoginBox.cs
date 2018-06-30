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
    public partial class LoginBox : Form
    {
        public LoginBox()
        {
            InitializeComponent();

            hiddenPasswordEnterPane();

            label2.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            displayPasswordEnterPane();
            if (textBox1.Text.Equals(Settings.Password))
            {
                new ManagerForm(this).Show();
                this.Hide();
            }
        }

        void hiddenPasswordEnterPane()
        {
            this.Height = 300;
            textBox1.Visible = false;
            label1.Visible = false;
        }

        void displayPasswordEnterPane()
        {
            this.Height = 381;
            textBox1.Visible = true;
            label1.Visible = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            new MainForm(this).Show();
            this.Hide();
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (textBox1.Text.Equals(Settings.Password))
                {
                    new ManagerForm(this).Show();
                    this.Hide();
                }
                else
                {
                    textBox1.BackColor = Color.Yellow;
                    label2.Visible = true;
                }
            }
        }
    }
}
