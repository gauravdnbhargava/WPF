using System;
using System.Xml;

namespace MNAIS
{
    class UpdateConfiguration
    {
        public static void UpdateKey(string strKey, string newValue)
        {
            XmlDocument xmlDoc = new XmlDocument();

            try
            {
                xmlDoc.Load(App.configFilePath);
                if (!KeyExists(strKey))
                {
                    throw new ArgumentNullException("Key", "<" + strKey + "> does not exist in the configuration. Update failed.");
                }
                XmlNode appSettingsNode = xmlDoc.SelectSingleNode("configuration/appSettings");
                foreach (XmlNode childNode in appSettingsNode)
                {
                    if (childNode.Attributes["key"].Value == strKey)
                    {
                        childNode.Attributes["value"].Value = newValue;
                        break;
                    }
                }
                xmlDoc.Save(System.Reflection.Assembly.GetExecutingAssembly().Location + ".config");
                xmlDoc.Save(AppDomain.CurrentDomain.BaseDirectory + "..\\..\\App.config");
                xmlDoc.Save(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile);
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message, "Update Configuration");
            }
        }

        public static bool KeyExists(string strKey)
        {
            XmlDocument xmlDoc = new XmlDocument();

            try
            {
                xmlDoc.Load(App.configFilePath);
                XmlNode appSettingsNode = xmlDoc.SelectSingleNode("configuration/appSettings");
                foreach (XmlNode childNode in appSettingsNode)
                {
                    if (childNode.Attributes["key"].Value == strKey)
                        return true;
                }
                return false;
            }             
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message, "Update Configuration");
                return false;
            }
        }
    }
}
