namespace TestHierarchyModels.ViewModels
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Windows.Controls;
    using System.Windows.Input;
    using GalaSoft.MvvmLight;
    using GalaSoft.MvvmLight.Command;
    using TestHierarchyModels.Views.UserControls;


    internal class ViewModel : ViewModelBase, INotifyPropertyChanged
    {
        string ClassName => this.GetType().Name;

        public string BindingInfo { get; set; } = $"This view binded to ViewModel";

        public bool IsBtnVisabled { get; set; } = true;

        public RelayCommand OnButton => new RelayCommand(() =>
        {
            BindingInfo = $"Clicked from {ClassName}";
            RaisePropertyChanged(nameof(BindingInfo));

            IsBtnVisabled = false;
            RaisePropertyChanged(nameof(IsBtnVisabled));
        });
    }
}
