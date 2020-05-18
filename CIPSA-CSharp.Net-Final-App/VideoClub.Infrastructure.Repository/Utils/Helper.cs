using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using VideoClub.Common.Model.Utils;

namespace VideoClub.Infrastructure.Repository.Utils
{
    public class Helper
    {
        
        public static readonly log4net.ILog Log = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        public static readonly string Connection = "VideoClubConnection";

        public static string GetCodeNumber(string model, int length, Random random)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var result = new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)])
                .ToArray());

            return model + CommonHelper.Separator + result;
        }

        public static void HandleLogError(string errorMessage)
        {
            var callStack = new StackFrame(1, true);
            Log.Error($"{errorMessage}" +
                      $"\n {callStack.GetFileName()} {callStack.GetFileLineNumber()}");
        }
    }
}
