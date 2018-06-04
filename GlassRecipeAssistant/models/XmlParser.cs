using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace RecipeAssistant.models
{
    class XmlParser
    {
        private XmlDocument doc;
        private string path;

        public XmlParser(string path)
        {
            this.path = path;
            doc = new XmlDocument();
            doc.Load(path);
        }

        /// <summary>
        /// 获取指定值
        /// </summary>
        /// <param name="xpath">XPath表达式</param>
        /// <returns></returns>
        public string get(string xpath)
        {
            return doc.SelectSingleNode(xpath).InnerText;
        }

        /// <summary>
        /// 修改元素的值
        /// </summary>
        /// <param name="xpath">XPath表达式</param>
        /// <param name="newValue">新值</param>
        public void set(string xpath, string newValue)
        {
            doc.SelectSingleNode(xpath).InnerText = newValue;
            doc.Save(path);
        }
    }
}
