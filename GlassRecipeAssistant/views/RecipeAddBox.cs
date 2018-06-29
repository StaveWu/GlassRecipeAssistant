using GlassRecipeAssistant.models;
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
        private IGlassRecipesModel grModel;
        private IPowderModel powderModel;
        private string glassName;
        private string clientName;

        public RecipeAddBox(IGlassRecipesModel grModel, IPowderModel powderModel, string glassName, string clientName)
        {
            InitializeComponent();
            label3.Visible = false;
            label4.Visible = false;
            this.grModel = grModel;
            this.powderModel = powderModel;
            this.glassName = glassName;
            this.clientName = clientName;

            foreach (string ele in powderModel.findPowders())
            {
                comboBox1.Items.Add(ele);
            }
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
            else if (isDuplicate(powderModel.getPowderId(comboBox1.SelectedIndex)))
            {
                comboBox1.BackColor = Color.Yellow;
                label4.Visible = true;
            }
            else
            {
                textBox2.BackColor = Color.White;
                label3.Visible = false;
                grModel.addRecipe(clientName, glassName, powderModel.getPowderId(comboBox1.SelectedIndex), Convert.ToDouble(textBox2.Text));
                this.Close();
            }
        }

        private bool isDuplicate(int powderId)
        {
            return grModel.findRecipes(clientName, glassName).ContainsKey(powderId);
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
