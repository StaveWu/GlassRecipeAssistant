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

namespace RecipeAssistant
{
    public partial class RecipeAddBox : Form
    {
        private IGlassRecipesModel model;
        private string glassName;
        private string clientName;

        public RecipeAddBox(IGlassRecipesModel model, string glassName, string clientName)
        {
            InitializeComponent();
            label3.Visible = false;
            label4.Visible = false;
            this.model = model;
            this.glassName = glassName;
            this.clientName = clientName;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void addRecipe()
        {
            if (!RegexUtils.isNumber(textBox2.Text))
            {
                textBox2.BackColor = Color.Yellow;
                label3.Visible = true;
            }
            else if (isDuplicate(textBox1.Text))
            {
                textBox1.BackColor = Color.Yellow;
                label4.Visible = true;
            }
            else
            {
                textBox2.BackColor = Color.White;
                label3.Visible = false;
                model.addRecipe(clientName, glassName, textBox1.Text, Convert.ToDouble(textBox2.Text));
                this.Close();
            }
        }

        private bool isDuplicate(string text)
        {
            return model.findRecipes(clientName, glassName).ContainsKey(text);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            addRecipe();
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                addRecipe();
            }
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                addRecipe();
            }
        }
    }
}
