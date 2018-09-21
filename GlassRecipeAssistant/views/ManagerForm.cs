using GlassRecipeAssistant.dao;
using GlassRecipeAssistant.dao.entities;
using GlassRecipeAssistant.models;
using Microsoft.VisualBasic.FileIO;
using RecipeAssistant;
using RecipeAssistant.models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GlassRecipeAssistant.views
{
    public partial class ManagerForm : Form
    {
        private Form adamForm;

        public const string CUSTOMER = "客户";
        public const string GLASS = "镜片型号";
        public const string POWDER = "色粉";
        public const string WEIGHT = "质量";

        private IPowderModel powderModel;
        private IGlassRecipePowderMapper grMapper;

        public ManagerForm(Form form)
        {
            InitializeComponent();

            adamForm = form;
            powderModel = new PowderModel();
            grMapper = new GlassRecipePowderMapper();
        }

        private void GlassRecipeManager_FormClosing(object sender, FormClosingEventArgs e)
        {
            adamForm.Close();
        }


        private void 密码修改ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new PassWordBox().Show();
        }

        private void 误差限ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new ErrorThresholdBox().Show();
        }

        private void 色粉添加ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PowderBox powderBox = new PowderBox(this, powderModel);
            powderBox.PowderRenamed += reloadAllData;
            powderBox.Show();
        }

        private void ManagerForm_Load(object sender, EventArgs e)
        {
            reloadAllData();
        }

        private void reloadAllData()
        {
            loadDataTable();
            loadCustomerCombo();
            loadGlassCombo();
            loadPowderCombo();
        }

        private void loadDataTable()
        {
            loadDataTable(
                filterPowder(
                    filterGlass(
                        filterCustomer(getMatchedData()
                        ))));
        }

        private void loadDataTable(List<GlassRecipeVo> vos)
        {
            dataGridView1.Rows.Clear();

            vos.ForEach(vo =>
                dataGridView1.Rows.Add(new object[] {
                    vo.Customer,
                    vo.GlassName,
                    vo.PowderName,
                    vo.Weight
                }));

            changeRecordCount(dataGridView1.RowCount);
        }

        private List<GlassRecipeVo> getMatchedData()
        {
            return grMapper.search(textBox1.Text);
        }

        private List<GlassRecipeVo> filterCustomer(List<GlassRecipeVo> data)
        {
            return comboBox1.SelectedIndex == -1 ? data : 
                data.Where(vo => 
                vo.Customer == comboBox1.SelectedItem as string)
                .ToList();
        }

        private List<GlassRecipeVo> filterGlass(List<GlassRecipeVo> data)
        {
            return comboBox2.SelectedIndex == -1 ? data :
                data.Where(vo =>
                vo.GlassName == comboBox2.SelectedItem as string)
                .ToList();
        }

        private List<GlassRecipeVo> filterPowder(List<GlassRecipeVo> data)
        {
            return comboBox3.SelectedIndex == -1 ? data :
                data.Where(vo =>
                vo.PowderName == comboBox3.SelectedItem as string)
                .ToList();
        }

        private void loadCustomerCombo()
        {
            loadCustomerCombo(getMatchedData());
        }

        private void loadCustomerCombo(List<GlassRecipeVo> vos)
        {
            comboBox1.Items.Clear();
            comboBox1.Text = "";

            vos.Select(vo => vo.Customer)
                .Distinct()
                .ToList()
                .ForEach(c => comboBox1.Items.Add(c));
        }

        private void loadGlassCombo()
        {
            loadGlassCombo(filterCustomer(getMatchedData()));
        }

        private void loadGlassCombo(List<GlassRecipeVo> vos)
        {
            comboBox2.Items.Clear();
            comboBox2.Text = "";

            vos.Select(vo => vo.GlassName)
                .Distinct()
                .ToList()
                .ForEach(glass => comboBox2.Items.Add(glass));
        }

        private void loadPowderCombo()
        {
            loadPowderCombo(filterGlass(filterCustomer(getMatchedData())));
        }

        private void loadPowderCombo(List<GlassRecipeVo> vos)
        {
            comboBox3.Items.Clear();
            comboBox3.Text = "";

            vos.Select(vo => vo.PowderName)
                .Distinct()
                .ToList()
                .ForEach(powder => comboBox3.Items.Add(powder));
        }

        private void button4_Click(object sender, EventArgs e)
        { // 清除筛选
            textBox1.Text = "";

            loadCustomerCombo();
            loadGlassCombo();
            loadPowderCombo();
            loadDataTable();
        }

        private void button1_Click(object sender, EventArgs e)
        { // 添加
            VoAddBox voAddBox = new VoAddBox(powderModel, grMapper);
            voAddBox.DataChanged += addVoToTable;
            voAddBox.Show();
        }

        private void addVoToTable(GlassRecipeVo vo)
        {
            dataGridView1.Rows.Add(new object[] {
                vo.Customer,
                vo.GlassName,
                vo.PowderName,
                vo.Weight
            });
        }

        private bool isRowSelected()
        {
            return dataGridView1.SelectedRows.Count > 0;
        }

        private GlassRecipeVo getSelectGlassRecipeVo()
        {
            DataGridViewRow row = dataGridView1.SelectedRows[0];
            return new GlassRecipeVo(
                    row.Cells[CUSTOMER].Value.ToString(),
                    row.Cells[GLASS].Value.ToString(),
                    row.Cells[POWDER].Value.ToString(),
                    Convert.ToDouble(row.Cells[WEIGHT].Value)
                    );
        }

        private void button2_Click(object sender, EventArgs e)
        { // 删除
            if (!isRowSelected())
            {
                MessageBox.Show("请先选择要删除的行");
                return;
            }
            DialogResult res = MessageBox.Show("确定要删除吗？", "删除确认", MessageBoxButtons.YesNo);
            if (res == DialogResult.Yes)
            {
                grMapper.delete(getSelectGlassRecipeVo());
                reloadAllData();
            }
            else if (res == DialogResult.No)
            {
                return;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        { // 编辑
            if (!isRowSelected())
            {
                MessageBox.Show("请先选择要编辑的行");
                return;
            }
            VoEditBox editBox = new VoEditBox(powderModel, grMapper, getSelectGlassRecipeVo());
            editBox.DataChanged += localeEditedOne;
            editBox.Show();
        }

        private void localeEditedOne(GlassRecipeVo vo)
        {
            reloadAllData();
        }

        private void button10_Click(object sender, EventArgs e)
        { // 搜索
            loadCustomerCombo();
            loadGlassCombo();
            loadPowderCombo();
            loadDataTable();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        { // 客户切换了
            loadGlassCombo();
            loadPowderCombo();

            loadDataTable();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        { // 镜片型号切换了
            loadPowderCombo();
            loadDataTable();
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        { // 色粉切换了
            loadDataTable();
        }

        private void changeRecordCount(int count)
        {
            label4.Text = "共有" + count + "条记录";
        }

        private void csvToolStripMenuItem_Click(object sender, EventArgs e)
        { // 导入csv
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "CSV Files|*.csv";
            openFileDialog.Title = "Select a Csv File";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                List<GlassRecipeVo> csvData = new List<GlassRecipeVo>();
                using (TextFieldParser parser = new TextFieldParser(openFileDialog.FileName))
                {
                    parser.TextFieldType = FieldType.Delimited;
                    parser.SetDelimiters(",");
                    bool isSkippedHeader = false;
                    try
                    {
                        while (!parser.EndOfData)
                        {
                            string[] fields = parser.ReadFields();
                            if (!isSkippedHeader) // skip header
                            {
                                isSkippedHeader = true;
                                continue;
                            }
                            GlassRecipeVo vo = new GlassRecipeVo(
                                fields[0],
                                fields[1],
                                fields[2],
                                Convert.ToDouble(fields[3])
                                );
                            csvData.Add(vo);
                        }
                        grMapper.saveAll(csvData);
                        reloadAllData();
                        MessageBox.Show("导入完成！");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("导入数据出现问题，请检查数据格式是否匹配");
                    }
                }
            }
        }

        private void csvToolStripMenuItem1_Click(object sender, EventArgs e)
        { // 导出csv
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "CSV Files|*.csv";
            saveFileDialog.Title = "Save as a Csv File";
            saveFileDialog.FileName = "unknown.csv";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                if (saveFileDialog.FileName != "")
                {
                    using (StreamWriter writer = new StreamWriter(saveFileDialog.FileName))
                    {
                        writer.WriteLine(CUSTOMER + "," 
                            + GLASS + "," 
                            + POWDER + "," 
                            + WEIGHT);

                        grMapper.findAll().ForEach(vo =>
                        {
                            writer.WriteLine(vo.Customer + ","
                                + vo.GlassName + ","
                                + vo.PowderName + ","
                                + vo.Weight);
                        });
                    }
                    MessageBox.Show("导出完成！");
                }
                else
                {
                    MessageBox.Show("导出失败！文件名不能为空");
                }
                
            }
        }

        private void button5_Click(object sender, EventArgs e)
        { // 删除所有
            DialogResult res = MessageBox.Show("确定要删除所有配方吗？", "删除确认", MessageBoxButtons.YesNo);
            if (res == DialogResult.Yes)
            {
                grMapper.deleteAll();
                reloadAllData();
            }
        }
    }
}
