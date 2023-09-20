using System.Windows.Documents;
using System.Windows;
using System.Collections.Generic;
using System.Windows.Controls;

namespace WpfAppSample
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
        private void OnRBClick(object sender, RoutedEventArgs e)
        {
            var rb = sender as RadioButton;
            var tag = rb?.Tag.ToString();

            SelectorControl.Template = this.Resources[tag] as ControlTemplate;
        }

    }
}
