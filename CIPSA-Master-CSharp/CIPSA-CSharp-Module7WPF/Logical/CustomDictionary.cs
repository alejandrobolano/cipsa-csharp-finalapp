using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIPSA_CSharp_Module7WPF.Logical
{
    public class CustomDictionary
    {
        private readonly Hashtable _dictionary;
        public CustomDictionary()
        {
            _dictionary = LoadData();
        }

        private Hashtable LoadData()
        {
            var dictionary = new Hashtable
            {
                {"padre", "father"},
                {"hijo", "son"},
                {"madre", "mother"},
                {"muchacho", "child"},
                {"grande", "big"},
                {"foto", "photo"},
                {"casa", "house"},
                {"edificio", "build"},
                {"muralla", "wall"},
                {"audifonos", "headphones"},
                {"país", "country"},
                {"corto", "short"},
                {"largo", "larger"},
                {"servicio", "service"},
                {"gratis", "free"},
                {"curso", "course"},
                {"palabra", "word"},
                {"frases", "phrases"},
                {"mundo", "world"},
                {"idioma", "language"},
                {"español", "spanish"},
                {"Cataluña", "Catalonia"},
                {"reverso", "reverse"},
                {"tierra", "earth"},
                {"página", "page"},
                {"búsqueda", "search"}
            };

            return dictionary;
        }

        public Hashtable GetDictionary()
        {
            return _dictionary;
        }
    }
}
