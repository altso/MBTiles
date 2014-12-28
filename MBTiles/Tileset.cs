using System;
using System.Collections.Generic;
using System.IO;
using Windows.Storage.Streams;
using SQLitePCL;

namespace MBTiles
{
    public class Tileset : IDisposable
    {
        private readonly Lazy<SQLiteConnection> _connection;
        private readonly Lazy<Metadata> _metadata;

        public Tileset(string fileName)
        {
            _connection = new Lazy<SQLiteConnection>(() => new SQLiteConnection(fileName, SQLiteOpen.READONLY));
            _metadata = new Lazy<Metadata>(() => GetMetadata(_connection.Value));
        }

        public Metadata Metadata
        {
            get { return _metadata.Value; }
        }

        public IRandomAccessStream GetTile(int zoom, int x, int y)
        {
            using (var statement = _connection.Value.Prepare(Query.Tiles))
            {
                statement.Bind("@zoom_level", zoom);
                statement.Bind("@tile_column", x);
                statement.Bind("@tile_row", (1 << zoom) - y - 1);
                if (statement.Step() == SQLiteResult.ROW)
                {
                    return new MemoryStream(statement.GetBlob(0)).AsRandomAccessStream();
                }
            }
            return null;
        }

        private static Metadata GetMetadata(ISQLiteConnection connection)
        {
            using (var statement = connection.Prepare(Query.Metadata))
            {
                var dictionary = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
                while (statement.Step() == SQLiteResult.ROW)
                {
                    string key = statement.GetText(0);
                    string value = statement.GetText(1);
                    dictionary[key] = value;
                }
                return new Metadata(dictionary);
            }
        }

        public void Dispose()
        {
            if (_connection.IsValueCreated)
            {
                _connection.Value.Dispose();
            }
        }
    }
}