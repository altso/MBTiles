using System;
using System.Threading.Tasks;
using Windows.Graphics.Imaging;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Controls.Maps;

namespace MBTiles
{
    public static class MBTileSource
    {
        public static MapTileSource Create(string fileName)
        {
            var tileset = new Tileset(fileName);

            var dataSource = new CustomMapTileDataSource();
            dataSource.BitmapRequested += async (sender, args) =>
            {
                var deferral = args.Request.GetDeferral();
                try
                {
                    var tile = tileset.GetTile(args.ZoomLevel, args.X, args.Y);
                    if (tile != null)
                    {
                        var pixels = await GetPixelsAsync(tile);
                        var stream = await GetStreamAsync(pixels);
                        args.Request.PixelData = RandomAccessStreamReference.CreateFromStream(stream);
                    }
                }
                finally
                {
                    deferral.Complete();
                }
            };

            var tileSource = new MapTileSource(dataSource);

            // TODO: bounds restriction
            // TODO: zoom level restriction

            return tileSource;
        }

        private static async Task<byte[]> GetPixelsAsync(IRandomAccessStream stream)
        {
            var decoder = await BitmapDecoder.CreateAsync(stream);
            var pixelProvider = await decoder.GetPixelDataAsync(
                BitmapPixelFormat.Rgba8,
                BitmapAlphaMode.Straight,
                new BitmapTransform(),
                ExifOrientationMode.RespectExifOrientation,
                ColorManagementMode.ColorManageToSRgb);
            return pixelProvider.DetachPixelData();
        }

        private static async Task<IRandomAccessStream> GetStreamAsync(byte[] bytes)
        {
            var stream = new InMemoryRandomAccessStream();
            using (var writer = new DataWriter(stream.GetOutputStreamAt(0L)))
            {
                writer.WriteBytes(bytes);
                await writer.StoreAsync();
                await writer.StoreAsync();
            }
            return stream;
        }
    }
}