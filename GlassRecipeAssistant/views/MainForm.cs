using GlassRecipeAssistant.dao.entities;
using GlassRecipeAssistant.models;
using GlassRecipeAssistant.views;
using RecipeAssistant.models;
using RecipeAssistant.views;
using System;
using System.Collections.Generic;
using System.ComponentModel; 
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RecipeAssistant
{
    public partial class MainForm : Form
    {
        /// <summary>
        /// 实时质量, 也就是天平面板上显示的质量
        /// </summary>
        private double realTimeQuality;

        // model
        private SerialPort serialPort;
        private IGlassRecipePowderMapper grMapper;
        private IPowderModel powderModel;
        private ILogger logger;

        /// <summary>
        /// 该变量由listChanged事件决定，原因是为了反转时序；
        /// 原始的SelectedItem的改变发生在listChanged事件之前
        /// </summary>
        private ListBoxItemInfo currentSelectedItem;

        private Form adamForm;
        private Form clearZeroForm;
        private LoadingBox loadingBox;


        public MainForm(Form adamForm)
        {
            InitializeComponent();
            loadingBox = new LoadingBox();
            this.adamForm = adamForm;
            this.MinimizeBox = false; // this is a workaround for skip mininum error

            //CheckForIllegalCrossThreadCalls = false; // 防止串口回调报错

            // 初始化model
            serialPort = new SerialPort();
            grMapper = new GlassRecipePowderMapper();
            powderModel = new PowderModel();
            logger = new TextLogger();

            // 注册回调
            serialPort.DataReceived += new SerialDataReceivedEventHandler(dataReceived);

            loadClients();
            loadGlasses();
            loadRecipes();
            textBox1.Text = "" + Settings.RawMaterialQuality; // 原料质量

            // 状态栏监视定时器
            timer1.Start();
            // 质量更新定时器
            timer2.Start();
            // 清零监视定时器
            timer3.Start();
        }

        /// <summary>
        /// 内部类，定义listBox的item信息
        /// </summary>
        class ListBoxItemInfo
        {
            public Color BackColor { get; set; }
            private double currentQuality = 0.0;
            public double StandardQuality { get; set; }
            public string PowderName { get; set; }

            public ListBoxItemInfo(string powder, double standardQuality)
            {
                PowderName = powder;
                StandardQuality = standardQuality;
                BackColor = Color.FromArgb(209, 209, 209); // 灰色
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

            grMapper.findCustomers().ForEach(ele => comboBox2.Items.Add(ele));
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

            List<string> gs = grMapper.findGlassesByCustomer(getSelectedClientName());
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

            Dictionary<string, double> recipes = 
                grMapper.findPowders(getSelectedClientName(), getSelectedGlassName());
            fillRecipes(recipes);

        }

        /// <summary>
        /// 连接设备
        /// </summary>
        public void connectDevice()
        {
            serialPort.PortName = Settings.PortName;
            serialPort.BaudRate = Convert.ToInt32(Settings.BuadRate);
            serialPort.Open();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {// 工具栏的连接按钮
            if (serialPort.IsOpen)
            {
                MessageBox.Show("通讯已连接！");
            }
            else
            {
                try
                {
                    connectDevice();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {// 客户切换
            loadGlasses();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {// 镜片切换
            loadRecipes();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {// 切换配方项
            ListBoxItemInfo itemInfo = listBox1.SelectedItem as ListBoxItemInfo;
            if (itemInfo == null)
            {
                return;
            }
            // 提示清零
            clearZeroForm = new Form();
            while (realTimeQuality != 0.0)
            {
                DialogResult res = MessageBox.Show(clearZeroForm, "请先清零！", null, 
                    MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                if (res == DialogResult.Cancel)
                {
                    break;
                }
            }

            timer2.Stop();
            refreshRecipeInfoLabels();
            currentSelectedItem = itemInfo;
            timer2.Start();
        }

        private bool containsWeightMessage(string str)
        {
            return Regex.IsMatch(str, @"^WT");
        }

        private double getWeight(string str)
        {// 从串口报文中获取weight
            string[] s = str.Split(':');
            return Convert.ToDouble(s[1].Substring(0, s[1].Length - 1));
        }

        private void dataReceived(object sender, SerialDataReceivedEventArgs e)
        {// 串口回调函数
            if (serialPort.IsOpen)
            {
                try
                {// 从串口中获取并更新实时质量
                    string received = serialPort.ReadExisting().Trim();
                    string[] messages = received.Split('\n');
                    foreach (string s in messages)
                    {
                        if (containsWeightMessage(s))
                        {
                            realTimeQuality = getWeight(s.Trim());
                        }
                    }
                }
                catch (Exception ex)
                {
                    serialPort.Close();
                    MessageBox.Show("通讯出错！请检查接收的数据格式是否正确");
                }
            }
        }

        private async void button4_Click(object sender, EventArgs e)
        {// 下一个配方按钮
            if (checkRecipeSelected())
            {
                loadingBox.Show();
                loadingBox.Update();

                var selectedItem = getSelectedRecipes() as ListBoxItemInfo;

                bool isWithinErrorThreshold = await Task.Run(() =>
                {
                    for (int i = 0; i < 10; i++) // 多次采样验证
                    {
                        Thread.Sleep(200);
                        double cha = selectedItem.CurrentQuality
                                    - selectedItem.StandardQuality;
                        if (Math.Abs(cha) > Settings.ErrorThreshold)
                        {
                            return false;
                        }
                    }
                    return true;
                });

                loadingBox.Hide();

                if (isWithinErrorThreshold)
                {
                    if (!checkLastRecipeSelected())
                    {
                        listBox1.SelectedIndex += 1;
                    }
                }
                else
                {
                    MessageBox.Show("当前值不够稳定，未满足误差要求");
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {// 完成按钮
            if (checkGlassSelected() && checkRecipeSelected())
            {
                DialogResult dialogResult = MessageBox.Show("确定要保存本次称量结果吗？", 
                    "完成提示", MessageBoxButtons.OKCancel);
                if (dialogResult == DialogResult.OK)
                {
                    textBox1.Enabled = true; // 解锁原料质量
                    recordRecipesWeight();
                    weighOut();
                    this.Refresh();
                }
                else if (dialogResult == DialogResult.Cancel)
                {
                    // do nothing
                }
            }
        }

        private void recordRecipesWeight()
        {// 记录当前各配方质量
            Dictionary<string, double[]> recipes = new Dictionary<string, double[]>();
            foreach (ListBoxItemInfo ele in listBox1.Items)
            {
                recipes.Add(ele.PowderName, new double[] { ele.CurrentQuality, ele.StandardQuality });
            }

            try
            {   // here is an io operation, may be failed, so try catching exception 
                // in case the app dead
                logger.write(getSelectedClientName(), getSelectedGlassName(), recipes);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            
        }

        private void listBox1_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0)
            {
                return;
            }
            ListBoxItemInfo it = listBox1.Items[e.Index] as ListBoxItemInfo;

            Brush weightBrush;

            e.DrawBackground();
            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
            {// 如果被选中，则画出聚焦框
                e.DrawFocusRectangle();

                weightBrush = Brushes.Black; 
            }
            else
            {// 否则根据item存储的颜色来画背景色
                Brush br = new SolidBrush(it.BackColor);
                //Brush br = new SolidBrush(Color.Yellow);
                e.Graphics.FillRectangle(br, e.Bounds);

                weightBrush = Brushes.Gray;
            }

            e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;

            // 设置item的显示信息格式
            var powderFont = new Font("微软雅黑", 16, FontStyle.Bold);
            e.Graphics.DrawString(it.PowderName, powderFont,Brushes.Black, 
                e.Bounds, null);

            var standardWeightFont = new Font("微软雅黑", 10, FontStyle.Regular);
            e.Graphics.DrawString("标准质量为：" + it.StandardQuality + "g", standardWeightFont,
                weightBrush, e.Bounds.Left + 3, e.Bounds.Top + 32);
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {// 断开连接按钮
            if (serialPort.IsOpen)
            {
                serialPort.Close();
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {// 更新总质量、配方当前质量以及进度条
            toolStripStatusLabel5.Text = realTimeQuality.ToString() + "g";
            if (currentSelectedItem != null)
            {
                currentSelectedItem.CurrentQuality = realTimeQuality;

                label3.Text = "" + currentSelectedItem.CurrentQuality + "g";
                double rate = (double)currentSelectedItem.CurrentQuality / (double)currentSelectedItem.StandardQuality;

                circularProgressBar1.Text = "" + Math.Round(rate * 100) + "%";
                // progressBar只接受0-1的数值
                if (rate < 0)
                {
                    rate = 0;
                }
                else if (rate > 1)
                {
                    rate = 1;
                }
                circularProgressBar1.Value = (int)(rate * 100);
                changeProgressBarColor();
            }
        }

        private void changeProgressBarColor()
        {
            if (nonRecipeSelected())
            {
                return;
            }
            double cha = getSelectedRecipes().CurrentQuality
                - getSelectedRecipes().StandardQuality;
            if (Math.Abs(cha) <= Settings.ErrorThreshold)
            {// 绿色
                circularProgressBar1.ProgressColor = Color.FromArgb(34, 177, 76);
                circularProgressBar1.OuterColor = Color.FromArgb(181, 230, 29);
            }
            else if (cha > 0)
            {// 红色
                circularProgressBar1.ProgressColor = Color.FromArgb(237, 28, 36);
                circularProgressBar1.OuterColor = Color.FromArgb(255, 174, 201);
            }
            else if (cha < 0 && Math.Abs(cha) != Settings.ErrorThreshold)
            {// 蓝色
                circularProgressBar1.ProgressColor = Color.FromArgb(0, 162, 232);
                circularProgressBar1.OuterColor = Color.FromArgb(153, 217, 234);
            }
            else
            {// 默认为蓝色
                circularProgressBar1.ProgressColor = Color.FromArgb(0, 162, 232);
                circularProgressBar1.OuterColor = Color.FromArgb(153, 217, 234);
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {// 开始称重按钮
            if (!isWeighIn() || (isWeighIn() 
                && MessageBox.Show("当前称重还未完成，确定要重新开始称重吗？",
                    "称重提示", MessageBoxButtons.OKCancel) == DialogResult.OK))
            {
                // 检查原料质量
                if (!StringUtils.isNumber(textBox1.Text))
                {
                    MessageBox.Show("请先设置原料质量，并检查原料质量是否输入正确");
                    return;
                }
                textBox1.Enabled = false;
                Settings.RawMaterialQuality = Convert.ToDouble(textBox1.Text);
                loadRecipes();
                refreshRecipeInfoLabels();

                if (isRecipesEmpty())
                {
                    MessageBox.Show("配方不存在！");
                    return;
                }

                weighIn();
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            adamForm.Close();
            loadingBox.Close();
        }

        #region 菜单栏
        private void 串口ToolStripMenuItem_Click(object sender, EventArgs e)
        {// 菜单栏的通讯-串口
            new PortSettingBox().Show();
        }

        private void 版本ToolStripMenuItem_Click(object sender, EventArgs e)
        {// 版本信息
            new VersionBox().Show();
        }

        #endregion

        #region 状态栏
        private void timer1_Tick(object sender, EventArgs e)
        {// 监视串口状态
            if (serialPort.IsOpen)
            {
                indicateConnectionSuccess();
            }
            else
            {
                indicateConnectionFailure();
            }
        }

        private void indicateConnectionFailure()
        {
            toolStripStatusLabel2.Text = "未连接";
            toolStripStatusLabel2.ForeColor = Color.Red;
        }

        private void indicateConnectionSuccess()
        {
            toolStripStatusLabel2.Text = "连接成功";
            toolStripStatusLabel2.ForeColor = Color.Green;
        }
        #endregion

        #region 界面的粒子操作

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

        private bool isWeighIn()
        {
            return listBox1.SelectedIndex != -1;
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

        private void weighIn()
        {// 称重中
            listBox1.SelectedIndex = 0;
            label3.Enabled = true;
        }

        private void weighOut()
        {// 称重完成
            listBox1.SelectedIndex = -1;
            label3.Enabled = false;

            clearItemsWeightCache();
        }

        private void clearItemsWeightCache()
        {// 清空item存储的数据
            foreach (ListBoxItemInfo ele in listBox1.Items)
            {
                ele.CurrentQuality = 0.0;
            }
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

        private bool nonClientSelected()
        {
            return comboBox2.SelectedIndex < 0;
        }

        private bool nonGlassSelected()
        {
            return comboBox1.SelectedIndex < 0;
        }

        private bool nonRecipeSelected()
        {
            return listBox1.SelectedIndex < 0;
        }

        private bool isLastRecipeSelected()
        {
            return listBox1.SelectedIndex == listBox1.Items.Count - 1;
        }

        private bool checkLastRecipeSelected()
        {
            if (isLastRecipeSelected())
            {
                MessageBox.Show("已经是最后一个了！");
                return true;
            }
            return false;
        }

        private void fillRecipes(Dictionary<string, double> recipes)
        {
            clearRecipesCache();
            if (recipes != null)
            {
                double rate = Settings.RawMaterialQuality / (double)10;
                foreach (string ele in recipes.Keys)
                {
                    listBox1.Items.Add(new ListBoxItemInfo(ele, recipes[ele] * rate));
                }
            }
        }

        public void refreshRecipeInfoLabels()
        {
            if (nonRecipeSelected())
            {
                label7.Text = "-";
                label8.Text = "-";
                return;
            }
            label7.Text = "" + getSelectedRecipes().StandardQuality + "g";
            label8.Text = getSelectedRecipes().PowderName;
        }


        #endregion

        private void 历史记录ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (checkClientSelected() && checkGlassSelected())
            {
                try
                {
                    new HistoryBox(getSelectedClientName(), getSelectedGlassName()).Show();
                }
                 catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void timer3_Tick(object sender, EventArgs e)
        {// 监视是否清零
            if (realTimeQuality == 0.0 && clearZeroForm != null)
            {
                clearZeroForm.Close();
            }
        }
    }
}
