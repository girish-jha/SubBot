using System;

namespace RosettaMovies.Extentions
{
    static class UriX
    {
        internal static Uri ApplyQueryStringForDownload(this Uri uri, string hashKey)
        {
             return new Uri(uri + QueryString.CreateForDownload(hashKey));
        }
    }
}