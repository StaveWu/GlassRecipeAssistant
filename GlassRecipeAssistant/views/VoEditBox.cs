using GlassRecipeAssistant.models;
using RecipeAssistant.models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlassRecipeAssistant.views
{
    class VoEditBox : AbstractVoBox
    {
        private GlassRecipeVo oldVo;

        public VoEditBox(IPowderModel powderModel, IGlassRecipePowderMapper grModel, GlassRecipeVo vo)
            : base(powderModel, grModel)
        {
            oldVo = vo;
            comboBox2.Text = vo.Customer;
            comboBox3.Text = vo.GlassName;
            textBox4.Text = vo.Weight.ToString();
            comboBox1.SelectedValue = vo.PowderName;
        }

        protected override void handleConfirmRequest()
        {
            GlassRecipeVo newVo = new GlassRecipeVo(
                    comboBox2.Text,
                    comboBox3.Text,
                    comboBox1.SelectedItem.ToString(),
                    Convert.ToDouble(textBox4.Text));

            if (isEqual(oldVo, newVo))
            {
                dataMapper.save(newVo);
                onDataChanged(newVo);
                Close();
            }
            else if (dataMapper.existsByCustomerAndGlassAndPowder(
                newVo.Customer, newVo.GlassName, newVo.PowderName))
            {
                label5.Visible = true;
            }
            else
            {
                dataMapper.save(newVo);
                dataMapper.delete(oldVo);
                onDataChanged(newVo);
                Close();
            }
        }

        private bool isEqual(GlassRecipeVo oldVo, GlassRecipeVo newVo)
        {
            return oldVo.Customer.Equals(newVo.Customer)
                && oldVo.GlassName.Equals(newVo.GlassName)
                && oldVo.PowderName.Equals(newVo.PowderName);
        }
    }
}
