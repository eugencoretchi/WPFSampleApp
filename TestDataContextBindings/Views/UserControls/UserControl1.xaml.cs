﻿namespace TestDataContextBindings.Views.UserControls
{
    using System.Diagnostics;
    using System.Windows;
    using System.Windows.Controls;
    using TestDataContextBindings.ViewModels;

    public partial class UserControl1 : UserControl
    {

        string? DataContextName => (DataContext == null) ? "null" : DataContext.GetType().Name;
            
        public UserControl1()
        {
            InitializeComponent();
            LbInfo.Content = $"DataContext is {DataContextName}";
        }

        void OnBtnClick(object sender, RoutedEventArgs e)
        {
            LbInfo.Content = $"DataContext is {DataContextName}";
            var vm = DataContext as ViewModel1;
            if(vm!=null)
            {
                vm.OnButtonClicked();
            }
        }
    }
}
