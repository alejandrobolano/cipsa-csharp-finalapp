using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIPSA_CSharp_Module10WPF
{
    class StringUtil
    {
        public static string ConcatPhrases(string phrase1, string phrase2, bool showWhiteSpace)
        {
            return showWhiteSpace ? phrase1 + " " + phrase2 : phrase1 + phrase2;
        }

        public static string SearchAndDeleteWord(string phrase, string word)
        {
            if (!IsExistWord(phrase, word))
            {
                throw new Exception($"La palabra {word} no existe");
            }
            return phrase.Replace(word,string.Empty);
        }


        private static bool IsExistWord(string phrase, string word)
        {
            return phrase.Contains(word);
        }
    }
}
