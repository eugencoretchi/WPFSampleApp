namespace TestHierarchyModels.Views.UserControls
{
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Windows;
    using System.Windows.Controls;
    using TestHierarchyModels.ViewModels;

    public partial class UserControl1 : UserControl
    {           
        string InfoText { get; set; }
        public UserControl1()
        {
            InitializeComponent();
            DataContext = new ViewModel1();
            
        }

        void OnTest(object source, RoutedEventArgs e)
        {
            InfoText = $"Parent type: {this.Parent.GetType().Name}";
            var btn = this.Btn;
        }
    }
}
