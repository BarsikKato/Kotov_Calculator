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

namespace RpnCalculator
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void AddCharacter(object sender, RoutedEventArgs e)
        {
            string c = ((Button)sender).Content.ToString();
            EnteredExpression.Text += c;
        }

        private void RemoveCharacter(object sender, RoutedEventArgs e)
        {
            EnteredExpression.Text = EnteredExpression.Text.Remove(EnteredExpression.Text.Length - 1, 1);
        }

        private void ClearCharacters(object sender, RoutedEventArgs e)
        {
            EnteredExpression.Text = string.Empty;
        }

        private void Calculate(object sender, RoutedEventArgs e)
        {
            ReversedPolishNotationCalculator rpn = new ReversedPolishNotationCalculator();
            string expression = rpn.ConvertToReversedPolishNotation(EnteredExpression.Text);
            try
            {
                Result.Text = rpn.GetResult(expression).ToString();
            }
            catch
            {
                Result.Text = "Ошибка";
            }
        }
    }
}
