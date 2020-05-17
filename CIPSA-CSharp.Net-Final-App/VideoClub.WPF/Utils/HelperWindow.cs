using System;
using System.Diagnostics;
using System.Reflection;
using System.Resources;
using System.Windows;
using System.Windows.Controls;
using log4net;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace VideoClub.WPF.Utils
{
    public class HelperWindow
    {
        public static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

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

    }
    
   
}
