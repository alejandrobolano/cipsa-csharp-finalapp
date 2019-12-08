using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using CIPSA_CSharp_Module7WPF.Logical;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace CIPSA_CSharp_Module7WPF
{
    /// <summary>
    /// Interaction logic for Translator.xaml
    /// </summary>
    public partial class Translator : MetroWindow
    {
        private readonly CustomDictionary _customDictionary;
        public Translator()
        {
            InitializeComponent();
            _customDictionary = new CustomDictionary();
            LoadDataSpanish();
            LoadDataEnglish();
        }

        private void LoadDataSpanish()
        {
            var words = new StringBuilder();
            foreach (var value in _customDictionary.GetDictionary().Keys)
            {
                words.Append(value).Append(", ");
            }
            words.Replace(",", ".", words.ToString().LastIndexOf(','), 1);
            AdditionalSpanishInformationText.Text = "Algunas palabras que puedes buscar: " + words;
        }

        private void LoadDataEnglish()
        {
            var words = new StringBuilder();
            foreach (var value in _customDictionary.GetDictionary().Values)
            {
                words.Append(value).Append(", ");
            }

            
            words.Replace(",", ".", words.ToString().LastIndexOf(','), 1);
            AdditionalEnglishInformationText.Text = "Some words you can search: " + words;
        }


        private async void TranslateEnglishButton_Click(object sender, RoutedEventArgs e)
        {
            TranslationEnglishText.Text = string.Empty;
            if (OriginalText.Text.Equals(string.Empty)) return;
            if (_customDictionary.GetDictionary().Contains(OriginalText.Text))
            {
                TranslationEnglishText.Text = _customDictionary.GetDictionary()[OriginalText.Text].ToString();

            }
            else
            {
                await ShowMessage("Información",
                    $"No se encuentra la palabra '{OriginalText.Text}' en nuestro diccionario, intente con otra búsqueda");
            }
        }
        private async void TranslateSpanishButton_Click(object sender, RoutedEventArgs e)
        {
            TranslationSpanishText.Text = string.Empty;
            if (OriginalEnglishText.Text.Equals(string.Empty)) return;
            if (_customDictionary.GetDictionary().ContainsValue(OriginalEnglishText.Text))
            {
                var translate = _customDictionary.GetDictionary().Keys.OfType<string>().FirstOrDefault(key => _customDictionary.GetDictionary()[key].Equals(OriginalEnglishText.Text));
                if (!string.IsNullOrEmpty(translate))
                {
                    TranslationSpanishText.Text = translate;
                }
            }
            else
            {
                await ShowMessage("Information", 
                    $"The word '{OriginalEnglishText.Text}' is not found in our dictionary, try again with another search");
            }

        }

        private async Task ShowMessage(string title, string message)
        {
            await this.ShowMessageAsync(title,message);
        }
    }
}
