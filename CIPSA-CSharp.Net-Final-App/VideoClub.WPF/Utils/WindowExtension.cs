using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace VideoClub.WPF.Utils
{
    public static class WindowExtension
    {
        public static void ShowMessage(this Window window, string title, string message)
        {
            ShowMetroMessage(window, title, message);
        }

        private static void ShowMetroMessage(Window window, string title, string message)
        {
            var metroWindow = (window as MetroWindow);
            metroWindow.ShowMessageAsync(title, message);
        }

        public static void ShowGenericErrorMessage(this Window window, ResourceManager resourceManager)
        {
            var title = resourceManager.GetResourceValue("ERROR");
            var message = resourceManager.GetResourceValue("ERROR_MESSAGE");
            ShowMessage(window, title, message);
        }
        public static void ShowGenericErrorDataMessage(this Window window, ResourceManager resourceManager)
        {
            var title = resourceManager.GetResourceValue("ERROR");
            var message = resourceManager.GetResourceValue("ERROR_DATA_MESSAGE");
            ShowMessage(window, title, message);
        }
        public static void ShowInformationMessage(this Window window, ResourceManager resourceManager, string keyMessage)
        {
            var title = resourceManager.GetResourceValue("INFORMATION");
            var message = resourceManager.GetResourceValue(keyMessage);
            ShowMessage(window, title, message);
        }
        public static void ShowCustomInformationMessage(this Window window, ResourceManager resourceManager, string message)
        {
            var title = resourceManager.GetResourceValue("INFORMATION");
            ShowMessage(window, title, message);
        }
        public static void ShowCustomMessage(this Window window, ResourceManager resourceManager, string keyTitle, string keyMessage)
        {
            var title = resourceManager.GetResourceValue(keyTitle);
            var message = resourceManager.GetResourceValue(keyMessage);
            ShowMessage(window, title, message);
        }
    }
}
