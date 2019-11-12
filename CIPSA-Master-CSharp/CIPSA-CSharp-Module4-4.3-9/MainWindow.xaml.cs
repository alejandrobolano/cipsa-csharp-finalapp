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

namespace CIPSA_CSharp_Module4_4._3._9
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string _operation;
        private decimal _operatorOne;
        private decimal _operatorTwo;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void CalculateButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                switch (_operation)
                {
                    case "Suma":
                        ResultText.Content = _operatorOne + _operatorTwo;
                        break;
                    case "Resta":
                        ResultText.Content = _operatorOne - _operatorTwo;
                        break;
                    case "Division":
                        CalculateDivision();
                        break;
                    case "Multiplicacion":
                        ResultText.Content = _operatorOne * _operatorTwo;
                        break;
                    default:
                        throw new InvalidOperationException();
                }

            }
            catch (InvalidOperationException)
            {
                MessageBox.Show("Se debe seleccionar una operación para mostrar los resultados");
            }

        }

        private void CalculateDivision()
        {
            try
            {
                ResultText.Content = _operatorOne / _operatorTwo;
            }
            catch (DivideByZeroException)
            {
                MessageBox.Show("Inválido - division por cero");
            }

            return;
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ToggleButton_OnChecked(object sender, RoutedEventArgs e)
        {
            var radioButton = sender as RadioButton;
            if (radioButton == null)
                return;
            _operation = radioButton.Content.ToString();
        }

        private bool IsDouble(string input, ref decimal numberResult)
        {
            return decimal.TryParse(input, out numberResult);
        }

        private void ValidateNumber(string input, ref decimal numberResult)
        {
            try
            {
                if (!IsDouble(input, ref numberResult))
                {
                    CalculateButton.IsEnabled = false;
                    throw new FormatException();
                }

                if (input.Length > 7)
                {
                    CalculateButton.IsEnabled = false;
                    throw new OverflowException();
                }

                CalculateButton.IsEnabled = true;
            }
            catch (FormatException)
            {
                MessageBox.Show("Debe escribir un número válido");
            }
            catch (OverflowException)
            {
                MessageBox.Show("La longitud no debe superar los 7 caracteres");
            }
        }

        private void OperatorOneText_TextChanged(object sender, TextChangedEventArgs e)
        {
            ValidateNumber(Operator1Text.Text, ref _operatorOne);
            if (Operator2Text.Text.Equals(""))
            {
                CalculateButton.IsEnabled = false;
            }
        }

        private void OperatorTwoText_TextChanged(object sender, TextChangedEventArgs e)
        {
            ValidateNumber(Operator2Text.Text, ref _operatorTwo);
            if (Operator1Text.Text.Equals(""))
            {
                CalculateButton.IsEnabled = false;
            }
        }
    }
}
