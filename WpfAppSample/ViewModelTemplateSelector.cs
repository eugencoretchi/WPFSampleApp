namespace WpfAppSample
{
    using GalaSoft.MvvmLight;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;

    class CurrentConfig
    {
        public string ImgA { get; set; } = string.Empty;
        public string ImgB { get; set; } = string.Empty;
        public string ImgC { get; set; } = string.Empty;
        public string ImgD { get; set; } = string.Empty;
    }

    static class TemplateManager
    {
        static public CurrentConfig CurrentConfig { get; } = new CurrentConfig();
    }

    public class ViewModelTemplateSelector : ViewModelBase, INotifyPropertyChanged
    {
        ImageSlot imageSlot;
        public ImageSlot ImageSlot { get => imageSlot;
            set
            {
                imageSlot = value;
                RaisePropertyChanged(nameof(ImageSlot));
            }
        }

        CurrentConfig CurrentConfig => TemplateManager.CurrentConfig;

        public void SetImageUrl(string url)
        {
            var map = new Dictionary<ImageSlot, Action<string>>
            {
                { ImageSlot.A, u => CurrentConfig.ImgA = u },
                { ImageSlot.B, u => CurrentConfig.ImgB = u },
                { ImageSlot.C, u => CurrentConfig.ImgC = u },
                { ImageSlot.D, u => CurrentConfig.ImgD = u },
            };

            if(map.TryGetValue(ImageSlot, out var item))
            {
                item.Invoke(url);
            }            
        }
    }
}
