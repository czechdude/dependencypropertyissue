using System;
using System.Windows;
using System.Windows.Controls;
using Esri.ArcGISRuntime.Geometry;
using Esri.ArcGISRuntime.Mapping;

namespace DependencyPropertyTest
{
    /// <summary>
    /// Interaction logic for MapControl.xaml
    /// </summary>
    public partial class MapControl : UserControl
    {
        public MapControl()
        {
            InitializeComponent();
            SizeChanged += OnControlSizeChanged;

            MapView.ViewpointChanged += OnViewPointChanged;
            MapView.SizeChanged += OnMapViewSizeChanged;
            MapView.InteractionOptions.IsRotateEnabled = true;
        }

        private void OnMapViewSizeChanged(object sender, SizeChangedEventArgs e)
        {
            MapWidth = MapView.ActualWidth;
            MapHeight = MapView.ActualHeight;
        }

        public static readonly DependencyProperty ActualControlWidthProperty = DependencyProperty.Register(
            "ActualControlWidth",
            typeof(double),
            typeof(MapControl),
            new FrameworkPropertyMetadata(PropertyChangedCallback));

        private static void PropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var newVar = e.NewValue;
        }

        public double ActualControlWidth
        {
            get { return (double)GetValue(ActualControlWidthProperty); }
            set { SetValue(ActualControlWidthProperty, value); }
        }

        public static readonly DependencyProperty ActualControlHeightProperty = DependencyProperty.Register(
            "ActualControlHeight",
            typeof(double),
            typeof(MapControl),
            new FrameworkPropertyMetadata(default(double)));

        public double ActualControlHeight
        {
            get { return (double) GetValue(ActualControlHeightProperty); }
            set { SetValue(ActualControlHeightProperty, value); }
        }


        public static readonly DependencyProperty ViewPointProperty = DependencyProperty.Register(
            "ViewPoint",
            typeof(Geometry),
            typeof(MapControl),
            new FrameworkPropertyMetadata(OnViewPointChanged));

        private static void OnViewPointChanged(DependencyObject d, DependencyPropertyChangedEventArgs a)
        {
            if (d != null && a.NewValue != null)
                ((MapControl)d).SetViewPoint((Geometry)a.NewValue);
        }

        private void OnViewPointChanged(object sender, EventArgs e)
        {
            MapExtent = MapView.GetCurrentViewpoint(ViewpointType.BoundingGeometry)?.TargetGeometry?.Extent;
        }

        public static readonly DependencyProperty MapWidthProperty = DependencyProperty.Register(
            "MapWidth",
            typeof(double),
            typeof(MapControl),
            new PropertyMetadata(default(double)));

        public double MapWidth
        {
            get { return (double)GetValue(MapWidthProperty); }
            set { SetValue(MapWidthProperty, value); }
        }

        public static readonly DependencyProperty MapHeightProperty = DependencyProperty.Register(
            "MapHeight",
            typeof(double),
            typeof(MapControl),
            new PropertyMetadata(default(double)));

        public double MapHeight
        {
            get { return (double)GetValue(MapHeightProperty); }
            set { SetValue(MapHeightProperty, value); }
        }

        public static readonly DependencyProperty MapExtentProperty = DependencyProperty.Register(
            "MapExtent",
            typeof(Envelope),
            typeof(MapControl),
            new PropertyMetadata(default(Envelope)));

        public Envelope MapExtent
        {
            get { return (Envelope)GetValue(MapExtentProperty); }
            set { SetValue(MapExtentProperty, value); }
        }

        private void OnControlSizeChanged(object sender, SizeChangedEventArgs e)
        {
            ActualControlWidth = ActualWidth;
            ActualControlHeight = ActualHeight;
        }

        public Geometry ViewPoint
        {
            get { return (Geometry)GetValue(ViewPointProperty); }
            set
            {
                SetValue(ViewPointProperty, value);
                SetViewPoint(value);
            }
        }

        private void SetViewPoint(Geometry viewPoint)
        {
            MapView.SetViewpointGeometryAsync(viewPoint);
        }
    }
}
