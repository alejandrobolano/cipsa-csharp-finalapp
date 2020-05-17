using System;
using System.Linq;
using System.Reflection;
using VideoClub.Common.Model.Utils;

namespace VideoClub.Infrastructure.Repository.Utils
{
    public class Helper
    {
        
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

            return model + CommonHelper.Separator + result;
        }
    }
}
