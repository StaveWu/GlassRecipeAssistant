using GlassRecipeAssistant.models;
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
    public partial class ManagerForm : Form
    {
        private Form adamForm;

        private IGlassRecipesModel grModel;
        private IPowderModel powderModel;

        public ManagerForm(Form form)
        {
            InitializeComponent();

            adamForm = form;
            grModel = new GlassRecipesModel();
            powderModel = new PowderModel();

            grModel.ClientChanged += loadClients;
            grModel.GlassChanged += loadGlasses;
            grModel.RecipeChanged += loadRecipes;

            loadClients();
            loadGlasses();
            loadRecipes();
        }

        private void GlassRecipeManager_FormClosing(object sender, FormClosingEventArgs e)
        {
            adamForm.Close();
        }

        /// <summary>
        /// 内部类，定义listBox的item信息
        /// </summary>
        class ListBoxItemInfo
        {
            public Color BackColor { get; set; }
            private double currentQuality = 0.0;
            public double StandardQuality { get; set; }
            public string RecipeName { get; set; }

            public ListBoxItemInfo(string recipeName, double standardQuality)
            {
                RecipeName = recipeName;
                StandardQuality = standardQuality;
                BackColor = Color.FromArgb(209, 209, 209); // 灰色
            }

            public override String ToString()
            {
                return RecipeName;
            }

            public double CurrentQuality
            {
                get
                {
                    return currentQuality;
                }
                set
                {
                    currentQuality = value;
                    changeBackColor(Settings.ErrorThreshold);
                }
            }

            private void changeBackColor(double threshold)
            {
                double cha = currentQuality - StandardQuality;
                if (Math.Abs(cha) <= threshold)
                {// 绿色
                    BackColor = Color.FromArgb(0, 255, 0);
                }
                else if (cha > 0)
                {// 浅红
                    BackColor = Color.FromArgb(255, 128, 128);
                }
                else if (cha < 0 && Math.Abs(cha) != StandardQuality)
                {// 黄色
                    BackColor = Color.Yellow;
                }
                else
                {// 灰色
                    BackColor = Color.FromArgb(209, 209, 209);
                }
            }

        }

        public void loadClients()
        {
            clearClientsCache();
            clearRecipesCache();
            clearGlassesCache();

            List<string> gs = grModel.findClients();
            foreach (string ele in gs)
            {
                comboBox2.Items.Add(ele);
            }
            if (!isClientEmpty())
            {
                comboBox2.SelectedIndex = comboBox2.Items.Count - 1;
            }
        }

        /// <summary>
        /// 回调函数，加载镜片型号
        /// </summary>
        public void loadGlasses()
        {
            clearRecipesCache();
            clearGlassesCache();

            if (nonClientSelected())
            {
                return;
            }

            List<string> gs = grModel.findGlasses(getSelectedClientName());
            foreach (string ele in gs)
            {
                comboBox1.Items.Add(ele);
            }
            if (!isGlassesEmpty())
            {
                comboBox1.SelectedIndex = comboBox1.Items.Count - 1;
            }

        }

        /// <summary>
        /// 加载配方
        /// </summary>
        public void loadRecipes()
        {
            clearRecipesCache();

            if (nonClientSelected() || nonGlassSelected())
            {
                return;
            }

            Dictionary<int, double> recipes = grModel.findRecipes(getSelectedClientName(), getSelectedGlassName());
            if (recipes != null)
            {
                foreach (int ele in recipes.Keys)
                {
                    listBox1.Items.Add(new ListBoxItemInfo(powderModel.getPowderName(ele), recipes[ele]));
                }
            }

        }

        private void clearClientsCache()
        {
            comboBox2.Items.Clear();
            comboBox2.Text = "";
        }

        private void clearGlassesCache()
        {
            comboBox1.Items.Clear();
            comboBox1.Text = "";
        }

        private void clearRecipesCache()
        {
            listBox1.Items.Clear();
        }

        private bool isClientEmpty()
        {
            return comboBox2.Items.Count <= 0;
        }

        private bool isGlassesEmpty()
        {
            return comboBox1.Items.Count <= 0;
        }

        private bool isRecipesEmpty()
        {
            return listBox1.Items.Count <= 0;
        }

        private string getSelectedClientName()
        {
            return comboBox2.SelectedItem as string;
        }

        private string getSelectedGlassName()
        {
            return comboBox1.SelectedItem as string;
        }

        private ListBoxItemInfo getSelectedRecipes()
        {
            return listBox1.SelectedItem as ListBoxItemInfo;
        }

        private bool checkClientSelected()
        {
            if (nonClientSelected())
            {
                MessageBox.Show("请先选择客户！");
                return false;
            }
            return true;
        }

        private bool checkGlassSelected()
        {
            if (nonGlassSelected())
            {
                MessageBox.Show("请先选择镜片型号！");
                return false;
            }
            return true;
        }

        private bool checkRecipeSelected()
        {
            if (nonRecipeSelected())
            {
                MessageBox.Show("请先选择配方！");
                return false;
            }
            return true;
        }

        public bool nonClientSelected()
        {
            return comboBox2.SelectedIndex < 0;
        }

        public bool nonGlassSelected()
        {
            return comboBox1.SelectedIndex < 0;
        }

        public bool nonRecipeSelected()
        {
            return listBox1.SelectedIndex < 0;
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {// 客户切换
            loadGlasses();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadRecipes();
        }

        private void button8_Click(object sender, EventArgs e)
        {// 添加客户按钮
            new ClientAddBox(grModel).Show();
        }

        private void button9_Click(object sender, EventArgs e)
        {// 删除客户按钮
            if (checkClientSelected())
            {
                grModel.deleteClient(getSelectedClientName());
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {// 添加镜片按钮
            if (checkClientSelected())
            {
                new GlassAddBox(grModel, getSelectedClientName()).Show();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {// 删除镜片按钮
            if (checkClientSelected() && checkGlassSelected())
            {
                grModel.deleteGlass(getSelectedGlassName(), getSelectedClientName());
            }

        }

        private void button5_Click(object sender, EventArgs e)
        {// 配方添加按钮
            if (checkClientSelected() && checkGlassSelected())
            {
                new RecipeAddBox(grModel, powderModel, getSelectedGlassName(), getSelectedClientName()).Show();
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {// 配方删除按钮
            if (checkClientSelected() && checkGlassSelected() && checkRecipeSelected())
            {
                grModel.deleteRecipe(getSelectedClientName(), getSelectedGlassName(), 
                    powderModel.getPowderId(listBox1.SelectedIndex));
            }
        }

        private void 密码修改ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new PassWordBox().Show();
        }

        private void 误差限ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new ErrorThresholdBox().Show();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (nonRecipeSelected())
            {
                label1.Text = "-";
            }
            else
            {
                label1.Text = "" + getSelectedRecipes().StandardQuality + "g";
            }
        }

        private void 色粉添加ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new PowderBox(this, powderModel).Show();
        }
    }
}
