using CommunityToolkit.Mvvm.ComponentModel;
using OxyPlot;
using OxyPlot.Series;
using System.ComponentModel;
using System.Windows;

namespace InteWPF
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Loaded += (sender, e) =>
            { };
        }
    }
    namespace ViewModels
    {
        partial class MainWindowViewModel : ObservableObject
        {
            [ObservableProperty]
            private bool _isSpectralPlotVisible = true;

            // This is a property that is updated below when the check box changes
            public Visibility SpectralPlotVisibility => IsSpectralPlotVisible ? Visibility.Visible : Visibility.Collapsed;

            protected override void OnPropertyChanged(PropertyChangedEventArgs e)
            {
                base.OnPropertyChanged(e);
                switch (e.PropertyName)
                {
                    case nameof(IsSpectralPlotVisible):
                        // You can use an `IValueConverter` or just do this.
                        OnPropertyChanged(nameof(SpectralPlotVisibility));
                        break;
                }
            }
        }
        namespace Components
        {
            partial class PlotViewModel : ObservableObject, IDisposable
            {
                public PlotViewModel()
                {
                    SpectralPlotModel = new PlotModel { Title = "Demo Model" };
                    var lineSeries = new LineSeries();
                    for (int i = 0; i < 128; i++)
                    {
                        double x = i;
                        double y = Math.Sin(2 * Math.PI * i / 128);
                        lineSeries.Points.Add(new DataPoint(x, y));
                    }
                    SpectralPlotModel.Series.Add(lineSeries);
                }
                public void Dispose()
                { }

                [ObservableProperty]
                private PlotModel _spectralPlotModel;

                protected override void OnPropertyChanged(PropertyChangedEventArgs e)
                {
                    base.OnPropertyChanged(e);
                }
            }
        }
    }
}