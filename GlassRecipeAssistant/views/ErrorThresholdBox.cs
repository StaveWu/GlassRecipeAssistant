﻿using RecipeAssistant;
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
    public partial class ErrorThresholdBox : Form
    {
        public ErrorThresholdBox()
        {
            InitializeComponent();
            textBox1.Text = "" + Settings.ErrorThreshold;
            label2.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            trySettingErrorThreshold();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void trySettingErrorThreshold()
        {
            if (StringUtils.isNumber(textBox1.Text))
            {
                label2.Visible = false;
                textBox1.BackColor = Color.White;
                Settings.ErrorThreshold = Convert.ToDouble(textBox1.Text);
                this.Close();
            }
            else
            {
                textBox1.BackColor = Color.Yellow;
                label2.Visible = true;
            }
                
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                trySettingErrorThreshold();
            }
        }
    }
}
