namespace TestHierarchyModels.ViewModels
{
    using System.ComponentModel;
    using GalaSoft.MvvmLight;
    using GalaSoft.MvvmLight.Command;

    internal class ViewModel1 : ViewModelBase, INotifyPropertyChanged
    {
        string ClassName => this.GetType().Name;

        public string BindingInfo { get; set; } = $"This view binded to ViewModel1";

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
