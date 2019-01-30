using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace PixivSharp
{
    public static class PixivConstants
    {
        public const string ClientID = "bYGKuGVw91e0NMfPGp44euvGt59s";
        public const string ClientSecret = "HP3RmkgAmEGro0gn1x9ioawQE8WMfvLXDz3ZqxpK";
        public static readonly string CacheFolder = Path.Combine(Path.GetTempPath(), "PixivSharp");
    }
}
