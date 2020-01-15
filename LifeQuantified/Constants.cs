using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace LifeQuantified
{
    public static class Constants
    {
        public const string DatabaseFilename = "LifeQuantifiedDB.db3";

        public const SQLite.SQLiteOpenFlags Flags =
            // open database 
            SQLite.SQLiteOpenFlags.ReadWrite |
            // create DB if doesn't exist
            SQLite.SQLiteOpenFlags.Create |
            // enable multi-threaded DB access
            SQLite.SQLiteOpenFlags.SharedCache;

        public static string DatabasePath
        {
            get
            {
                var basePath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.LocalApplicationData);
                return Path.Combine(basePath, DatabaseFilename);
            }
        } 
    }
}