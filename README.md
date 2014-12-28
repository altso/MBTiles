MBTiles for Windows Phone 8.1
=============================

Build your offline maps for Windows Phone 8.1 with MBTiles.

Usage
-----

    var uri = new Uri("ms-appx:///Maps/geography-class.mbtiles");
    var file = await StorageFile.GetFileFromApplicationUriAsync(uri);
    MapControl.TileSources.Add(MBTileSource.Create(file.Path));

See [GegraphyClass](https://github.com/altso/MBTiles/tree/master/GeographyClass) sample project for more details.

Documentation
-------------

* [SQLitePCL Installation Instructions](https://sqlitepcl.codeplex.com/documentation)
* [MBTiles Specification](https://github.com/mapbox/mbtiles-spec)
