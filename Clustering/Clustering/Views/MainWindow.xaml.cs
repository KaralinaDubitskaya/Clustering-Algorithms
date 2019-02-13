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

namespace k_means
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new ApplicationViewModel();
        }

        // Allow only digits and backspace to be entered into the text box.
        private void TextBox_CheckNumericInput(object sender, TextCompositionEventArgs e)
        {
            char lastKey = e.Text[e.Text.Length - 1];
            if (!char.IsDigit(lastKey) && lastKey != '\b')
            {
                e.Handled = true;
            }
        }
    }
}
