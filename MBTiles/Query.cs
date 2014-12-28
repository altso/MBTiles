namespace MBTiles
{
    internal static class Query
    {
        public const string Tiles = @"SELECT tile_data
                                        FROM tiles
                                       WHERE zoom_level = @zoom_level
                                         AND tile_column = @tile_column
                                         AND tile_row = @tile_row";
        public const string Metadata = @"SELECT * FROM metadata";
    }
}