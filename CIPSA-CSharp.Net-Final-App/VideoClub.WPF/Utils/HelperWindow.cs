using System.Configuration;
using System.Diagnostics;
using System.Reflection;
using System.Windows.Controls;
using log4net;
using MahApps.Metro.Controls;

namespace VideoClub.WPF.Utils
{
    public class HelperWindow
    {
        public static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        public static readonly string BlockedUserProcessAutomatic = "BlockedUserProcess";
        public static readonly string VipUserProcessAutomatic = "VipUserProcess";

        //public static readonly Assembly assembly = Assembly.Load("namespace");
        //public static readonly ResourceManager resourceManager = new ResourceManager("VideoClub.Infrastructure.Repository.en-US", assembly);


        public static void ClearFields(StackPanel panel)
        {
            foreach (var control in panel.Children)
            {
                switch (control)
                {
                    case TextBox box:
                        box.Text = string.Empty;
                        break;
                    case ComboBox box:
                        box.SelectedItem = string.Empty;
                        break;
                    case NumericUpDown numeric:
                        numeric.Value = new double();
                        break;
                    case StackPanel panelChild:
                        ClearFields(panelChild);
                        break;
                }
            }
        }
        public static bool HasAnyEmptyFields(StackPanel panel)
        {
            for (var i = 0; i < panel.Children.Count; i++)
            {
                var control = panel.Children[i];
                switch (control)
                {
                    case TextBox box when string.IsNullOrEmpty(box.Text):
                    case ComboBox comboBox when string.IsNullOrEmpty(comboBox.SelectedItem.ToString()):
                    case NumericUpDown numeric when numeric.Value == 0d:
                        return true;
                    case StackPanel panelChild:
                        return HasAnyEmptyFields(panelChild);
                }
            }
            return false;
        }

        public static void HandleLogError(string errorMessage)
        {
            var callStack = new StackFrame(1, true);
            Log.Error($"{errorMessage}" +
                      $"\n {callStack.GetFileName()} {callStack.GetFileLineNumber()}");
        }
        public static void HandleLogInfo(string infoMessage)
        {
            Log.Info(infoMessage);
        }

        public static void AddUpdateAppSettings(string key, string value)
        {
            try
            {
                var configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                var settings = configFile.AppSettings.Settings;
                if (settings[key] == null)
                {
                    settings.Add(key, value);
                }
                else
                {
                    settings[key].Value = value;
                }
                configFile.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection(configFile.AppSettings.SectionInformation.Name);
            }
            catch (ConfigurationErrorsException exception)
            {
                HandleLogError(exception.Message);
            }
        }

        public static string ReadSetting(string key)
        {
            var result = string.Empty;
            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                result = appSettings[key] ?? "Not Found";
            }
            catch (ConfigurationErrorsException exception)
            {
                HandleLogError(exception.Message);
            }

            return result;
        }


    }


}
