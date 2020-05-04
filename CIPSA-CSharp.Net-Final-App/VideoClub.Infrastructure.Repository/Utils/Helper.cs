using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace VideoClub.Infrastructure.Repository.Utils
{
    public class Helper
    {
        public static readonly string Separator = "-";
        
        public static readonly log4net.ILog Log = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        //public static readonly Assembly assembly = Assembly.Load("VideoClub.Infrastructure.Repository");
        //public static readonly ResourceManager resourceManager = new ResourceManager("VideoClub.Infrastructure.Repository.en-US", assembly);
        public static readonly string Connection = "VideoClubConnection";

        public static string GetCodeNumber(string model, int length, Random random)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var result = new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)])
                .ToArray());

            return model + Separator + result;
        }
    }
}
