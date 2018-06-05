using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace RecipeAssistant.models
{
    class Settings
    {
        private static string path = System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "settings.xml";
        private static XmlParser xmlParser = new XmlParser(path);
        private const string baudRateXpath = "//serialPort/baudRate";
        private const string portNameXpath = "//serialPort/portName";
        private const string errorThresholdPath = "//errorThreshold";

        public static string BuadRate
        {
            get
            {
                return xmlParser.get(baudRateXpath);
            }
            set
            {
                xmlParser.set(baudRateXpath, value);
            }
        }
        public static string PortName
        {
            get
            {
                return xmlParser.get(portNameXpath);
            }
            set
            {
                xmlParser.set(portNameXpath, value);
            }
        }

        public static double ErrorThreshold
        {
            get
            {
                return Convert.ToDouble(xmlParser.get(errorThresholdPath));
            }
            set
            {
                xmlParser.set(errorThresholdPath, "" + value);
            }
        }
    }
}
