using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace VideoClub.Common.Model.Extensions
{
    public static class StringExtension
    {
        public static string ToUpperAllFirstLetter(this string value)
        {
            var splitValues = value.Split(' ');
            var result = new StringBuilder();
            splitValues.ToList().ForEach(x =>
                {
                    result.Append(x.ToUpperFirstLetter()).Append(" ");
                });
            var lastWhiteSpace = result.ToString().LastIndexOf(' ');
            return result.ToString().Remove(lastWhiteSpace);
        }

        public static string ToUpperFirstLetter(this string value)
        {
            if (string.IsNullOrEmpty(value)) return value;
            var charArray = value.ToCharArray();
            charArray[0] = char.ToUpper(charArray[0]);
            return new string(charArray);
        }

        public static string RemoveMultipleSpace(this string value)
        {
            return Regex.Replace(value, @"\s+", " ");
        }
    }
}
