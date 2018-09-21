using GlassRecipeAssistant.dao.entities;
using GlassRecipeAssistant.models;
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
    public delegate void PowderRenamedHandler();

    public partial class PowderBox : Form
    {
        public event PowderRenamedHandler PowderRenamed;

        private IPowderModel model;

        private ManagerForm form;

        public PowderBox(ManagerForm form, IPowderModel model)
        {
            InitializeComponent();

            this.form = form;
            this.model = model;
            model.PowdersUpdated += loadPowders;

            loadPowders();

        }

        public void loadPowders()
        {
            clearPowdersCache();

            try
            {
                //listBox1.DataSource = model.findPowders();
                listBox1.DisplayMember = "PowderName";
                listBox1.ValueMember = "PowderName";

                model.findPowders().ForEach(p => listBox1.Items.Add(p));
            }
            catch (Exception ex)
            { // skip it if nothing found
                return;
            }
        }

        private void clearPowdersCache()
        {
            listBox1.Items.Clear();
        }

        private void button2_Click(object sender, EventArgs e)
        {// 添加按钮
            new PowderAddBox(model).Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {// 编辑按钮
            if (nonPowderSelected())
            {
                MessageBox.Show("请先选择要编辑的色粉");
                return;
            }
            textBox1.Enabled = true;
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (nonPowderSelected())
            {
                return;
            }
            textBox1.Text = listBox1.SelectedItem as string;
        }

        private bool nonPowderSelected()
        {
            return listBox1.SelectedIndex < 0;
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (textBox1.Text.Equals(""))
                {
                    textBox1.BackColor = Color.Yellow;
                }
                else
                {
                    model.renamePowder(((Powder)listBox1.SelectedItem).Id, textBox1.Text);
          
                    textBox1.BackColor = Color.White;
                    textBox1.Enabled = false;

                    PowderRenamed();
                }
            }
        }

        private void PowderBox_FormClosing(object sender, FormClosingEventArgs e)
        {
            model.PowdersUpdated -= loadPowders;
        }
    }
}
