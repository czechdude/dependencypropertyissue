using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Esri.ArcGISRuntime;
using Esri.ArcGISRuntime.Geometry;
using Esri.ArcGISRuntime.Mapping;
using Esri.ArcGISRuntime.Portal;
using Esri.ArcGISRuntime.Security;
using GalaSoft.MvvmLight;

namespace DependencyPropertyTest
{
    public class MapViewModel : ViewModelBase
    {
        private double _actualControlWidth;
        private double _actualControlHeight;
        private Map _map;
        private Envelope _mapViewGeometry;
        private Envelope _mapExtent;
        private double _actualMapWidth;
        private double _actualMapHeight;

        public MapViewModel()
        {
            ArcGISRuntimeEnvironment.Initialize();
            var uri = new Uri("http://petr-divis.maps.arcgis.com/sharing/rest");

            var portal = ArcGISPortal.CreateAsync(uri).Result;

            // once connected, get the license info from the portal for the current user
            var licenseInfo = portal.PortalInfo.LicenseInfo;

            // set the license using the static ArcGISRuntimeEnvironment class
            ArcGISRuntimeEnvironment.SetLicense(licenseInfo);

            var portalItem = PortalItem.CreateAsync(portal, "69fdcd8e40734712aaec34194d4b988c").Result;

            Map = new Map(portalItem);
        }

        public Map Map
        {
            get => _map;
            set => Set(() => Map, ref _map, value);
        }

        public double ActualControlWidth
        {
            get => _actualControlWidth;
            set => _actualControlWidth = value;
        }

        public double ActualControlHeight
        {
            get => _actualControlHeight;
            set => _actualControlHeight = value;
        }

        public double ActualMapWidth
        {
            get => _actualMapWidth;
            set => _actualMapWidth = value;
        }

        public double ActualMapHeight
        {
            get => _actualMapHeight;
            set => _actualMapHeight = value;
        }

        public Envelope MapViewGeometry
        {
            get => _mapViewGeometry;
            set => Set(() => MapViewGeometry, ref _mapViewGeometry, value);
        }

        public Envelope MapExtent
        {
            get => _mapExtent;
            set
            {
                Set(() => MapExtent, ref _mapExtent, value);
               
            }
        }
    }
}
