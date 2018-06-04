using RecipeAssistant.models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RecipeAssistant
{
    public partial class PortSettingBox : Form
    {
        public PortSettingBox()
        {
            InitializeComponent();

            foreach (string ele in SerialPort.GetPortNames())
            {
                comboBox1.Items.Add(ele);
            }

            comboBox2.Items.Add("1200");
            comboBox2.Items.Add("2400");
            comboBox2.Items.Add("4800");
            comboBox2.Items.Add("9600");
            comboBox2.Items.Add("19200");
            comboBox2.Items.Add("115200");

            comboBox1.Text = Settings.PortName;
            comboBox2.Text = Settings.BuadRate;
        }

        private void button2_Click(object sender, EventArgs e)
        {// 取消按钮
            this.Close();
        }

        private void configurate()
        {
            try
            {
                Settings.PortName = comboBox1.SelectedItem as string;
                Settings.BuadRate = comboBox2.SelectedItem as string;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {// 确定按钮
            configurate();
        }

        private void comboBox2_KeyDown(object sender, KeyEventArgs e)
        {// 响应回车键
            if (e.KeyCode == Keys.Enter)
            {
                configurate();
            }
        }

        private void comboBox1_KeyDown(object sender, KeyEventArgs e)
        {// 响应回车键
            if (e.KeyCode == Keys.Enter)
            {
                configurate();
            }
        }
    }
}
