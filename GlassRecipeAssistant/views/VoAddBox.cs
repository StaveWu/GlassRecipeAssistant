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
    class VoAddBox : AbstractVoBox
    {
        public VoAddBox(IPowderModel powderModel, IGlassRecipePowderMapper mapper)
            : base(powderModel, mapper)
        {
            
        }

        protected override void handleConfirmRequest()
        {
            // Make sure new in is unique
            if (dataMapper.existsByCustomerAndGlassAndPowder(comboBox2.Text, 
                comboBox3.Text, comboBox1.SelectedItem.ToString()))
            {
                label5.Visible = true;
            }
            else
            {
                GlassRecipeVo vo = new GlassRecipeVo(
                    comboBox2.Text,
                    comboBox3.Text,
                    comboBox1.SelectedItem.ToString(),
                    Convert.ToDouble(textBox4.Text));
                dataMapper.save(vo);
                onDataChanged(vo);
                Close();
            }
        }
    }
}
