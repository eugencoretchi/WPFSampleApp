namespace WpfAppSample.ViewModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Linq;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using GalaSoft.MvvmLight;
    using GalaSoft.MvvmLight.Command;

    public class ViewModel : ViewModelBase, INotifyPropertyChanged
    {
        readonly double todegrees = 180 / Math.PI;
        readonly double oneRadian = Math.PI / 180;


        public ViewModelTemplateSelector TemplateSelectorVM { get; set; } = new ViewModelTemplateSelector();

        static readonly string digitform = "00.0";


        public bool IsCheckedA
        {
            get;
            set;
        }
        public bool IsCheckedB
        {
            get;
            set;
        }
        public bool IsCheckedC
        {
            get;
            set;
        }

        public string TextInfo { get; set; } = "Ignition";

        public bool IgnitionGo { get; set; }

        public string SpeedValue { get; set; } = digitform;
        public string AngleValue { get; set; } = digitform;
        public string HeightValue { get; set; } = digitform;

        public string TargetSpeedValue { get; set; } = digitform;
        public string TargetAngleValue { get; set; } = digitform;
        public string TargetHeightValue { get; set; } = digitform;
      
        AirplaneSimulator airplaneSim = new AirplaneSimulator();

        public ICommand IgnitionCommand { get; }
        public ICommand IncreaseSpeedCommand { get; }
        public ICommand IncreaseAngleCommand { get; }


        public void OnButtonClick(ImageSlot btnNum)
        {
            switch(btnNum)
            {
                case ImageSlot.A:
                    break;
            }
        }



        public ViewModel()
        {
            IgnitionCommand = new RelayCommand( () =>
            {
                if (!airplaneSim.Telemetry.Ignition && IgnitionGo)
                {
                    airplaneSim.StartAsync();
                }
                if (airplaneSim.Telemetry.Ignition && !IgnitionGo)
                {
                    airplaneSim.StopAsync();
                }
                RaisePropertyChanged(nameof(IgnitionGo));
            });

            IncreaseSpeedCommand = new RelayCommand(() =>
            {
                airplaneSim.AddSpeed(1);
                TargetSpeedValue = airplaneSim.TargetSpeed.ToString(digitform);
                RaisePropertyChanged(nameof(TargetSpeedValue));

            });

            IncreaseAngleCommand = new RelayCommand(() =>
            {
                airplaneSim.AddAngle(oneRadian);
                var degrees = airplaneSim.TargetAngle * todegrees;
                TargetAngleValue = degrees.ToString(digitform);
                RaisePropertyChanged(nameof(TargetAngleValue));

            });

            airplaneSim.OnAngleChanged = v =>
            {
                var degrees = airplaneSim.Telemetry.Angle * todegrees;
                AngleValue = degrees.ToString(digitform);
                RaisePropertyChanged(nameof(AngleValue));
            };

            airplaneSim.OnHeightChanged = v =>
            {
                HeightValue = airplaneSim.Telemetry.Height.ToString(digitform);
                RaisePropertyChanged(nameof(HeightValue));
            };

            airplaneSim.OnSpeedChanged = v =>
            {
                SpeedValue = airplaneSim.Telemetry.Speed.ToString(digitform);
                RaisePropertyChanged(nameof(SpeedValue));
            };

            Task.Run(() =>
            {
                ProcessingText(TextInfo, newText =>
                {
                    TextInfo = newText;
                    RaisePropertyChanged(nameof(TextInfo));
                });
            });

            testList.Add(new SomeTestValue() { Value = "123" });
            testList.Add(new SomeTestValue() { Value = "---" });
            testList.Add(new SomeTestValue() { Value = "end" });

            IsCheckedC = true;
        }

        private void ProcessingText(string orignText, Action<string> onTextChanged)
        {
            string[] dots = new string[] { "", ".", "..", "...", "...." };
            int dotsId = 0;
            try
            {
                while (true)
                {
                    Thread.Sleep(300);

                    if (IgnitionGo)
                    {
                        var text = orignText + dots[dotsId];
                        onTextChanged(text);

                        ++dotsId;
                        if (dotsId >= dots.Length)
                        {
                            dotsId = 0;
                        }                        
                    }
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }
        }

        private List<SomeTestValue> testList = new List<SomeTestValue>();

        public List<SomeTestValue> TestList { get => testList; set => Set(ref testList, value); }

        private SomeTestValue testValue;

        public SomeTestValue SelectedTestValue 
        { 
            get => testValue; 
            set
            {
                testValue = value;
                testValue.Value = "selected";

                Debug.WriteLine("dump testList:");
                foreach (var t in testList)
                {
                    Debug.WriteLine(t.Value);
                }
                Debug.WriteLine("");
            }
        }
    }
}
