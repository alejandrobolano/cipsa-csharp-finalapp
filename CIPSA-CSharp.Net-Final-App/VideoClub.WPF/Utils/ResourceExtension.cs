using System;
using System.Resources;

namespace VideoClub.WPF.Utils
{
    public static class ResourceExtension
    {
        public static string GetResourceValue(this ResourceManager resource, string key)
        {
            var value = string.Empty;
            try
            {
                value = resource.GetString(key);
            }
            catch (Exception e)
            {
                WindowHelper.HandleLogError(e.Message + $"\nResource error: {key}");
            }
            return value;
        }
    }
}
