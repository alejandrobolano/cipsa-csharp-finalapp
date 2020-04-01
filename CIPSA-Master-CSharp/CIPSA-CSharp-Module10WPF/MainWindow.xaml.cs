using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CIPSA_CSharp_Module10WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void SearchAndDeleteButton_Click(object sender, RoutedEventArgs e)
        {
            var phraseForSearchText = PhraseForSearchText.Text;
            var wordForDeleteText = WordForDeleteText.Text;
            try
            {
                var result = StringUtil.SearchAndDeleteWord(phraseForSearchText, wordForDeleteText);

                #region Show result

                PhraseForSearchText.Text = result;
                WordForDeleteText.Clear();
                if (!result.Equals(string.Empty))
                {
                    MessageBox.Show(result);
                }
                

                #endregion
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
                WordForDeleteText.Clear();
            }

        }

        private void ConcatButton_Click(object sender, RoutedEventArgs e)
        {
            var firstPhraseText = FirstPhraseForConcatText.Text;
            var secondPhraseText = SecondPhraseForConcatText.Text;
            var showWhiteSpace = ShowWhiteSpaceCheckBox.IsChecked != null && (bool)ShowWhiteSpaceCheckBox.IsChecked;
            var result = StringUtil.ConcatPhrases(firstPhraseText, secondPhraseText, showWhiteSpace);

            #region Show result

            FirstPhraseForConcatText.Text = result;
            SecondPhraseForConcatText.Clear();
            MessageBox.Show(result);

            #endregion
            
        }

        private void WordForDeleteText_TextChanged(object sender, TextChangedEventArgs e)
        {
            ValidateFieldsWithEnabledButton(SearchAndDeleteButton, ((TextBox)sender), PhraseForSearchText);
        }

        private void PhraseForSearchText_TextChanged(object sender, TextChangedEventArgs e)
        {
            ValidateFieldsWithEnabledButton(SearchAndDeleteButton, ((TextBox)sender), WordForDeleteText);
        }

        private void FirstPhraseForConcatText_TextChanged(object sender, TextChangedEventArgs e)
        {
            ValidateFieldsWithEnabledButton(ConcatButton, ((TextBox)sender), SecondPhraseForConcatText);
        }

        private void SecondPhraseForConcatText_TextChanged(object sender, TextChangedEventArgs e)
        {
            ValidateFieldsWithEnabledButton(ConcatButton, ((TextBox)sender), FirstPhraseForConcatText);
        }

        private void ValidateFieldsWithEnabledButton(Button buttonToEnabled, TextBox firsTextBox, TextBox secondTextBox)
        {
            buttonToEnabled.IsEnabled = !firsTextBox.Text.Equals(string.Empty) &&
                                     !secondTextBox.Text.Equals(string.Empty);
        }

    }
}
