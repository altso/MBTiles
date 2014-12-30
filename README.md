MBTiles for Windows Phone 8.1
=============================

[![Build status](https://ci.appveyor.com/api/projects/status/32my9lyqcc2hrpy7/branch/master?svg=true)](https://ci.appveyor.com/project/altso/mbtiles/branch/master)

Build your offline maps for Windows Phone 8.1 with MBTiles.

Usage
-----

    var uri = new Uri("ms-appx:///Maps/geography-class.mbtiles");
    var file = await StorageFile.GetFileFromApplicationUriAsync(uri);
    MapControl.TileSources.Add(MBTileSource.Create(file.Path));

See [GeographyClass](https://github.com/altso/MBTiles/tree/master/GeographyClass) sample project for more details.

Dependencies
------------

MBTiles are SQLite databases. Therefore, SQLitePCL package must be installed in a target project.

Documentation
-------------

* [SQLitePCL Installation Instructions](https://sqlitepcl.codeplex.com/documentation)
* [MBTiles Specification](https://github.com/mapbox/mbtiles-spec)
