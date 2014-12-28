using System;

namespace MBTiles
{
    public class TileNotFoundException : Exception
    {
        public TileNotFoundException()
        {
        }

        public TileNotFoundException(string message)
            : base(message)
        {
        }

        public TileNotFoundException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}