using System.Collections.Generic;
using Windows.Devices.Geolocation;

namespace MBTiles
{
    public class Metadata
    {
        private readonly IDictionary<string, string> _metadata;

        public Metadata(IDictionary<string, string> metadata)
        {
            _metadata = metadata;
        }

        public string Name
        {
            get { return GetValue("name"); }
        }

        public string Type
        {
            get { return GetValue("type"); }
        }

        public string Version
        {
            get { return GetValue("version"); }
        }

        public string Description
        {
            get { return GetValue("format"); }
        }

        public GeoboundingBox Bounds
        {
            get {  return ParseBounds(GetValue("bounds"));}
        }

        public string this[string key]
        {
            get { return GetValue(key); }
        }

        private string GetValue(string key)
        {
            string value;
            return _metadata.TryGetValue(key, out value) ? value : null;
        }

        private static GeoboundingBox ParseBounds(string bounds)
        {
            if (bounds == null) return null;

            string[] parts = bounds.Split(',');
            return new GeoboundingBox(
                new BasicGeoposition
                {
                    Latitude = double.Parse(parts[1]),
                    Longitude = double.Parse(parts[2])
                },
                new BasicGeoposition
                {
                    Latitude = double.Parse(parts[3]),
                    Longitude = double.Parse(parts[0])
                });
        }
    }
}