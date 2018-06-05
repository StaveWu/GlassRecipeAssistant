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
        /// 实时质量
        /// </summary>
        private double realTimeQuality;

        // model
        private SerialPort serialPort;
        private IGlassRecipesModel grModel;
        private ILogger logger;

        private IRecipeQualityCalculateStrategy recipeQualityStrategy;

        /// <summary>
        /// 该变量由listChanged事件决定，原因是为了反转时序；
        /// 原始的SelectedItem的改变发生在listChanged事件之前
        /// </summary>
        private ListBoxItemInfo currentSelectedItem;

        public MainForm()
        {
            InitializeComponent();

            //CheckForIllegalCrossThreadCalls = false; // 防止串口回调报错

            // 初始化model
            serialPort = new SerialPort();
            grModel = new GlassRecipesModel();
            logger = new TextLogger();

            recipeQualityStrategy = new ZeroQualityStrategy(realTimeQuality);

            // 注册回调
            serialPort.DataReceived += new SerialDataReceivedEventHandler(dataReceived);

            grModel.GlassChanged += loadGlasses;
            grModel.RecipeChanged += loadRecipes;

            loadGlasses();
            loadRecipes();

            // 状态栏监视定时器
            timer1.Start();

            timer2.Start();

        }

        /// <summary>
        /// 回调函数，加载镜片型号
        /// </summary>
        public void loadGlasses()
        {
            comboBox1.Items.Clear();
            comboBox1.Text = "";
            List<string> gs = grModel.findAllGlasses();
            foreach (string ele in gs)
            {
                comboBox1.Items.Add(ele);
            }
            if (comboBox1.Items.Count != 0)
            {
                comboBox1.SelectedIndex = comboBox1.Items.Count - 1;
            }
        }

        /// <summary>
        /// 加载配方
        /// </summary>
        public void loadRecipes()
        {
            listBox1.Items.Clear();
            string glass = (string)comboBox1.SelectedItem;
            Dictionary<string, double> recipes = grModel.findRecipes(glass);
            if (recipes != null)
            {
                foreach (string ele in recipes.Keys)
                {
                    listBox1.Items.Add(new ListBoxItemInfo(ele, recipes[ele]));
                }
            }
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
                BackColor = Color.White;
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
                {
                    BackColor = Color.White;
                }
            }

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

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadRecipes();
        }

        private void button2_Click(object sender, EventArgs e)
        {// 添加按钮
            new GlassAddBox(grModel).Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {// 删除按钮
            string sg = (string)comboBox1.SelectedItem;
            grModel.deleteGlass(sg);
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {// 切换配方项
            ListBoxItemInfo itemInfo = listBox1.SelectedItem as ListBoxItemInfo;
            if (itemInfo == null)
            {
                return;
            }

            timer2.Stop();

            if (itemInfo.CurrentQuality > 0)
            {// 如果当前被选中的item中有值，则认为是recheck操作，切换recheck计算策略
                //Console.WriteLine(itemInfo.RecipeName + " recheck " + itemInfo.CurrentQuality);
                recipeQualityStrategy = new ReCheckQualityStrategy(realTimeQuality, itemInfo.CurrentQuality);
            }
            else
            {// 否则，认为是新的未称重的item，切换为0质量计算策略
                //Console.WriteLine(itemInfo.RecipeName + " zero " + itemInfo.CurrentQuality);
                recipeQualityStrategy = new ZeroQualityStrategy(realTimeQuality);
            }
            label7.Text = "" + itemInfo.StandardQuality + "g";
            currentSelectedItem = itemInfo;

            timer2.Start();
        }

        private bool isWeight(string str)
        {
            return Regex.IsMatch(str, @"^WT");
        }

        private double getWeight(string str)
        {// 从串口报文中获取weight
            string[] s = str.Split(':');
            return Convert.ToDouble(s[1]);
        }

        private void dataReceived(object sender, SerialDataReceivedEventArgs e)
        {// 串口回调函数
            if (serialPort.IsOpen)
            {
                try
                {// 从串口中获取并更新实时质量
                    string received = serialPort.ReadLine().Trim();
                    //string received = serialPort.ReadExisting().Trim();
                    if (!isWeight(received))
                    {
                        return;
                    }
                    realTimeQuality = getWeight(received);
                }
                catch (Exception ex)
                {
                    serialPort.Close();
                    MessageBox.Show(ex.Message);
                    Console.WriteLine(ex.StackTrace);
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {// 下一个配方按钮
            int id = listBox1.SelectedIndex;
            if (id < 0)
            {
                return;
            }
            else if (id == listBox1.Items.Count - 1)
            {
                MessageBox.Show("已经是最后一个了！");
            }
            else
            {
                listBox1.SelectedIndex = id + 1;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {// 完成按钮
            if (comboBox1.Items.Count == 0 || listBox1.Items.Count == 0)
            {
                return;
            }
            string glass = comboBox1.SelectedItem.ToString();
            listBox1.SelectedIndex = -1;    // 让item失去焦点
            Dictionary<string, double> recipes = new Dictionary<string, double>();
            foreach (ListBoxItemInfo ele in listBox1.Items)
            {
                recipes.Add(ele.RecipeName, ele.CurrentQuality);
                ele.CurrentQuality = 0.0;   // 清空item存储的数据
            }
            logger.write(glass, recipes);   // 记录当前各配方质量
            this.Refresh();
        }

        private void listBox1_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0)
            {
                return;
            }
            ListBoxItemInfo it = listBox1.Items[e.Index] as ListBoxItemInfo;
            e.DrawBackground();
            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
            {// 如果被选中，则画出聚焦框
                e.DrawFocusRectangle();
            }
            else
            {// 否则根据item存储的颜色来画背景色
                Brush br = new SolidBrush(it.BackColor);
                //Brush br = new SolidBrush(Color.Yellow);
                e.Graphics.FillRectangle(br, e.Bounds);
            }
            e.Graphics.DrawString(it.ToString(), e.Font, new SolidBrush(Color.Black), e.Bounds, null);

        }

        private void button5_Click(object sender, EventArgs e)
        {// 配方添加按钮
            string glassName = comboBox1.SelectedItem as string;
            if (glassName == null)
            {
                MessageBox.Show("请先选择镜片型号！");
            }
            else
            {
                new RecipeAddBox(grModel, glassName).Show();
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {// 配方删除按钮
            string glassName = comboBox1.SelectedItem as string;
            if (glassName == null)
            {
                MessageBox.Show("请先选择镜片型号！");
            }
            else if (listBox1.SelectedIndex < 0)
            {
                MessageBox.Show("请先选择配方！");
            }
            else
            {
                grModel.deleteRecipe(glassName, ((ListBoxItemInfo)listBox1.SelectedItem).RecipeName);
            }
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {// 断开连接按钮
            serialPort.Close();
        }

        private void timer2_Tick(object sender, EventArgs e)
        {// 更新总质量、配方当前质量以及进度条
            label5.Text = realTimeQuality.ToString() + "g";
            if (currentSelectedItem != null)
            {
                currentSelectedItem.CurrentQuality = recipeQualityStrategy.getCurrentQuality(realTimeQuality);

                label3.Text = "" + currentSelectedItem.CurrentQuality + "g";
                double rate = (double)currentSelectedItem.CurrentQuality / (double)currentSelectedItem.StandardQuality;
                label2.Text = "" + (rate * 100).ToString("f2") + "%";
                // progressBar只接受0-1的数值
                if (rate < 0)
                {
                    rate = 0;
                }
                else if (rate > 1)
                {
                    rate = 1;
                }
                progressBar1.Value = (int)(rate * 100);
            }
        }

        #region 菜单栏
        private void 串口ToolStripMenuItem_Click(object sender, EventArgs e)
        {// 菜单栏的通讯-串口
            new PortSettingBox().Show();
        }

        private void 误差设置ToolStripMenuItem_Click(object sender, EventArgs e)
        {// 菜单栏的误差设置项
            new ErrorThresholdBox().Show();
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
    }
}
