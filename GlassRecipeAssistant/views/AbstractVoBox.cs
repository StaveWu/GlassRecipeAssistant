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

namespace GlassRecipeAssistant.views
{
    public delegate void ChangedHandler(GlassRecipeVo vo);

    public abstract partial class AbstractVoBox : Form
    {
        public event ChangedHandler DataChanged;

        protected IPowderModel powderModel;
        protected IGlassRecipePowderMapper dataMapper;

        public AbstractVoBox(IPowderModel powderModel, IGlassRecipePowderMapper mapper)
        {
            InitializeComponent();
            this.powderModel = powderModel;
            this.dataMapper = mapper;
            label5.Visible = false;

            // comboBox1以其选择的条目作为最终数据
            powderModel.findPowders()
                .ForEach(p => comboBox1.Items.Add(p.PowderName));

            // comboBox2和comboBox3以其text作为最终数据
            dataMapper.findCustomers()
                .ForEach(c => comboBox2.Items.Add(c));
            dataMapper.findGlasses()
                .ForEach(g => comboBox3.Items.Add(g));
        }

        private void button1_Click(object sender, EventArgs e)
        { // 确定
            // Check input is legal or not
            double weight;
            try
            {
                weight = Convert.ToDouble(textBox4.Text);
                textBox4.BackColor = Color.White;
            }
            catch (Exception ex)
            {
                textBox4.BackColor = Color.Yellow;
                return;
            }

            // Check combox is selected or not
            if (comboBox1.SelectedIndex == -1)
            {
                comboBox1.BackColor = Color.Yellow;
                return;
            }
            else
            {
                comboBox1.BackColor = Color.White;
            }

            handleConfirmRequest();
        }

        protected virtual void onDataChanged(GlassRecipeVo vo)
        {
            DataChanged(vo);
        }

        protected abstract void handleConfirmRequest();

        private void button2_Click(object sender, EventArgs e)
        { // 取消
            this.Close();
        }
    }
}
