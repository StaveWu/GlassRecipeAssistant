﻿namespace RecipeAssistant
{
    partial class MainForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.连接ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.串口ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.误差设置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.关于ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.版本ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label3 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.button5 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.circularProgressBar1 = new CircularProgressBar.CircularProgressBar();
            this.label8 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.button7 = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel3 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel4 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel5 = new System.Windows.Forms.ToolStripStatusLabel();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.button9 = new System.Windows.Forms.Button();
            this.button8 = new System.Windows.Forms.Button();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.menuStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // comboBox1
            // 
            this.comboBox1.Font = new System.Drawing.Font("微软雅黑", 13.91597F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(15, 46);
            this.comboBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(211, 38);
            this.comboBox1.TabIndex = 0;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.连接ToolStripMenuItem,
            this.关于ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(7, 3, 0, 3);
            this.menuStrip1.Size = new System.Drawing.Size(1097, 37);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 连接ToolStripMenuItem
            // 
            this.连接ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.串口ToolStripMenuItem,
            this.误差设置ToolStripMenuItem});
            this.连接ToolStripMenuItem.Font = new System.Drawing.Font("Microsoft YaHei UI", 12.10084F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.连接ToolStripMenuItem.Name = "连接ToolStripMenuItem";
            this.连接ToolStripMenuItem.Size = new System.Drawing.Size(64, 31);
            this.连接ToolStripMenuItem.Text = "设置";
            // 
            // 串口ToolStripMenuItem
            // 
            this.串口ToolStripMenuItem.Font = new System.Drawing.Font("Microsoft YaHei UI", 12.10084F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.串口ToolStripMenuItem.Name = "串口ToolStripMenuItem";
            this.串口ToolStripMenuItem.Size = new System.Drawing.Size(170, 32);
            this.串口ToolStripMenuItem.Text = "串口设置";
            this.串口ToolStripMenuItem.Click += new System.EventHandler(this.串口ToolStripMenuItem_Click);
            // 
            // 误差设置ToolStripMenuItem
            // 
            this.误差设置ToolStripMenuItem.Name = "误差设置ToolStripMenuItem";
            this.误差设置ToolStripMenuItem.Size = new System.Drawing.Size(170, 32);
            this.误差设置ToolStripMenuItem.Text = "误差设置";
            this.误差设置ToolStripMenuItem.Click += new System.EventHandler(this.误差设置ToolStripMenuItem_Click);
            // 
            // 关于ToolStripMenuItem
            // 
            this.关于ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.版本ToolStripMenuItem});
            this.关于ToolStripMenuItem.Font = new System.Drawing.Font("Microsoft YaHei UI", 12.10084F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.关于ToolStripMenuItem.Name = "关于ToolStripMenuItem";
            this.关于ToolStripMenuItem.Size = new System.Drawing.Size(64, 31);
            this.关于ToolStripMenuItem.Text = "关于";
            // 
            // 版本ToolStripMenuItem
            // 
            this.版本ToolStripMenuItem.Name = "版本ToolStripMenuItem";
            this.版本ToolStripMenuItem.Size = new System.Drawing.Size(130, 32);
            this.版本ToolStripMenuItem.Text = "版本";
            this.版本ToolStripMenuItem.Click += new System.EventHandler(this.版本ToolStripMenuItem_Click);
            // 
            // label3
            // 
            this.label3.Enabled = false;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 36.30252F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label3.Location = new System.Drawing.Point(11, 218);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(273, 93);
            this.label3.TabIndex = 6;
            this.label3.Text = "-";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("微软雅黑", 13.91597F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button1.Location = new System.Drawing.Point(24, 419);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(215, 48);
            this.button1.TabIndex = 8;
            this.button1.Text = "完成";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button4
            // 
            this.button4.Font = new System.Drawing.Font("微软雅黑", 13.91597F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button4.Location = new System.Drawing.Point(262, 365);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(227, 102);
            this.button4.TabIndex = 11;
            this.button4.Text = "下一个配方";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // label7
            // 
            this.label7.Font = new System.Drawing.Font("微软雅黑", 13.91597F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.Location = new System.Drawing.Point(208, 89);
            this.label7.Name = "label7";
            this.label7.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label7.Size = new System.Drawing.Size(281, 35);
            this.label7.TabIndex = 4;
            this.label7.Text = "0g";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("微软雅黑", 13.91597F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button2.Location = new System.Drawing.Point(15, 122);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(211, 48);
            this.button2.TabIndex = 1;
            this.button2.Text = "添加";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Font = new System.Drawing.Font("微软雅黑", 13.91597F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button3.Location = new System.Drawing.Point(15, 176);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(211, 48);
            this.button3.TabIndex = 1;
            this.button3.Text = "删除";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.Font = new System.Drawing.Font("Microsoft YaHei UI", 12.10084F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(76, 31);
            this.toolStripButton1.Text = "连接";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1,
            this.toolStripButton2});
            this.toolStrip1.Location = new System.Drawing.Point(0, 37);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1097, 34);
            this.toolStrip1.TabIndex = 13;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.Font = new System.Drawing.Font("Microsoft YaHei UI", 12.10084F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.toolStripButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton2.Image")));
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(76, 31);
            this.toolStripButton2.Text = "断开";
            this.toolStripButton2.Click += new System.EventHandler(this.toolStripButton2_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(15, 365);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(272, 48);
            this.button5.TabIndex = 16;
            this.button5.Text = "添加";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(15, 419);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(272, 48);
            this.button6.TabIndex = 16;
            this.button6.Text = "删除";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.comboBox1);
            this.groupBox1.Controls.Add(this.button2);
            this.groupBox1.Controls.Add(this.button3);
            this.groupBox1.Font = new System.Drawing.Font("微软雅黑", 13.91597F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox1.Location = new System.Drawing.Point(12, 319);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(248, 245);
            this.groupBox1.TabIndex = 17;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "镜片型号";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.listBox1);
            this.groupBox2.Controls.Add(this.button6);
            this.groupBox2.Controls.Add(this.button5);
            this.groupBox2.Font = new System.Drawing.Font("微软雅黑", 13.91597F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox2.Location = new System.Drawing.Point(266, 76);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(304, 488);
            this.groupBox2.TabIndex = 18;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "配方";
            // 
            // listBox1
            // 
            this.listBox1.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.listBox1.Font = new System.Drawing.Font("微软雅黑", 13.91597F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 35;
            this.listBox1.Location = new System.Drawing.Point(15, 43);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(272, 284);
            this.listBox1.TabIndex = 17;
            this.listBox1.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.listBox1_DrawItem);
            this.listBox1.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.circularProgressBar1);
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.button7);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.button1);
            this.groupBox3.Controls.Add(this.button4);
            this.groupBox3.Font = new System.Drawing.Font("微软雅黑", 13.91597F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox3.Location = new System.Drawing.Point(576, 76);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(505, 488);
            this.groupBox3.TabIndex = 19;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "质量监控";
            // 
            // circularProgressBar1
            // 
            this.circularProgressBar1.AnimationFunction = WinFormAnimation.KnownAnimationFunctions.Liner;
            this.circularProgressBar1.AnimationSpeed = 500;
            this.circularProgressBar1.BackColor = System.Drawing.Color.Transparent;
            this.circularProgressBar1.Font = new System.Drawing.Font("微软雅黑", 18.15126F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.circularProgressBar1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.circularProgressBar1.InnerColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.circularProgressBar1.InnerMargin = 0;
            this.circularProgressBar1.InnerWidth = 0;
            this.circularProgressBar1.Location = new System.Drawing.Point(328, 174);
            this.circularProgressBar1.MarqueeAnimationSpeed = 2000;
            this.circularProgressBar1.Name = "circularProgressBar1";
            this.circularProgressBar1.OuterColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.circularProgressBar1.OuterMargin = -25;
            this.circularProgressBar1.OuterWidth = 26;
            this.circularProgressBar1.ProgressColor = System.Drawing.SystemColors.Highlight;
            this.circularProgressBar1.ProgressWidth = 15;
            this.circularProgressBar1.SecondaryFont = new System.Drawing.Font("微软雅黑", 9.07563F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.circularProgressBar1.Size = new System.Drawing.Size(150, 150);
            this.circularProgressBar1.StartAngle = 270;
            this.circularProgressBar1.SubscriptColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(166)))), ((int)(((byte)(166)))));
            this.circularProgressBar1.SubscriptMargin = new System.Windows.Forms.Padding(10, -35, 0, 0);
            this.circularProgressBar1.SubscriptText = "";
            this.circularProgressBar1.SuperscriptColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(166)))), ((int)(((byte)(166)))));
            this.circularProgressBar1.SuperscriptMargin = new System.Windows.Forms.Padding(10, 35, 0, 0);
            this.circularProgressBar1.SuperscriptText = "";
            this.circularProgressBar1.TabIndex = 16;
            this.circularProgressBar1.Text = "0%";
            this.circularProgressBar1.TextMargin = new System.Windows.Forms.Padding(0);
            // 
            // label8
            // 
            this.label8.Font = new System.Drawing.Font("微软雅黑", 13.91597F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label8.Location = new System.Drawing.Point(208, 43);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(281, 35);
            this.label8.TabIndex = 15;
            this.label8.Text = "-";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(18, 43);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(116, 31);
            this.label6.TabIndex = 14;
            this.label6.Text = "当前配方:";
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(24, 365);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(215, 48);
            this.button7.TabIndex = 13;
            this.button7.Text = "开始称重";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(18, 139);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(164, 31);
            this.label4.TabIndex = 12;
            this.label4.Text = "配方当前质量:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 89);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(164, 31);
            this.label1.TabIndex = 12;
            this.label1.Text = "配方标准质量:";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Font = new System.Drawing.Font("Microsoft YaHei UI", 12.10084F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(97, 27);
            this.toolStripStatusLabel1.Text = "通讯状态:";
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Font = new System.Drawing.Font("Microsoft YaHei UI", 12.10084F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.toolStripStatusLabel2.ForeColor = System.Drawing.Color.Red;
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(72, 27);
            this.toolStripStatusLabel2.Text = "未连接";
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.toolStripStatusLabel2,
            this.toolStripStatusLabel3,
            this.toolStripStatusLabel4,
            this.toolStripStatusLabel5});
            this.statusStrip1.Location = new System.Drawing.Point(0, 568);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1097, 32);
            this.statusStrip1.TabIndex = 12;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel3
            // 
            this.toolStripStatusLabel3.Name = "toolStripStatusLabel3";
            this.toolStripStatusLabel3.Size = new System.Drawing.Size(815, 27);
            this.toolStripStatusLabel3.Spring = true;
            // 
            // toolStripStatusLabel4
            // 
            this.toolStripStatusLabel4.Font = new System.Drawing.Font("Microsoft YaHei UI", 12.10084F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.toolStripStatusLabel4.Name = "toolStripStatusLabel4";
            this.toolStripStatusLabel4.Size = new System.Drawing.Size(77, 27);
            this.toolStripStatusLabel4.Text = "总质量:";
            // 
            // toolStripStatusLabel5
            // 
            this.toolStripStatusLabel5.Font = new System.Drawing.Font("Microsoft YaHei UI", 12.10084F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.toolStripStatusLabel5.Name = "toolStripStatusLabel5";
            this.toolStripStatusLabel5.Size = new System.Drawing.Size(21, 27);
            this.toolStripStatusLabel5.Text = "-";
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // timer2
            // 
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.button9);
            this.groupBox4.Controls.Add(this.button8);
            this.groupBox4.Controls.Add(this.comboBox2);
            this.groupBox4.Font = new System.Drawing.Font("微软雅黑", 13.91597F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox4.Location = new System.Drawing.Point(12, 76);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(248, 237);
            this.groupBox4.TabIndex = 20;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "客户";
            // 
            // button9
            // 
            this.button9.Location = new System.Drawing.Point(15, 162);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(211, 48);
            this.button9.TabIndex = 1;
            this.button9.Text = "删除";
            this.button9.UseVisualStyleBackColor = true;
            // 
            // button8
            // 
            this.button8.Location = new System.Drawing.Point(15, 108);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(211, 48);
            this.button8.TabIndex = 1;
            this.button8.Text = "添加";
            this.button8.UseVisualStyleBackColor = true;
            // 
            // comboBox2
            // 
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Location = new System.Drawing.Point(15, 43);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(211, 38);
            this.comboBox2.TabIndex = 0;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(119F, 119F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(1097, 600);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Font = new System.Drawing.Font("微软雅黑", 9.07563F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "配方辅助工具";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ToolStripMenuItem 连接ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 串口ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 关于ToolStripMenuItem;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ToolStripMenuItem 版本ToolStripMenuItem;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
        private System.Windows.Forms.Timer timer2;
        private System.Windows.Forms.ToolStripMenuItem 误差设置ToolStripMenuItem;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel3;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel4;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel5;
        private CircularProgressBar.CircularProgressBar circularProgressBar1;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button button9;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.ComboBox comboBox2;
    }
}

