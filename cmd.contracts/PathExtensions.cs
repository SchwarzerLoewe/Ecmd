using System;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace cmd.contracts
{
    public static class PathExtensions
    {
        public static string GetShortPathName(string longPath)
        {
            StringBuilder shortPath = new StringBuilder(longPath.Length + 1);

            if (PathExtensions.GetShortPathName(longPath, shortPath, shortPath.Capacity) == 0)
            {
                return longPath;
            }

            return shortPath.ToString();
        }

        public static string GetShortPathName(this DirectoryInfo di)
        {
            return GetShortPathName(di.FullName);
        }
        public static string GetShortPathName(this FileInfo di)
        {
            return GetShortPathName(di.FullName);
        }

        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        private static extern Int32 GetShortPathName(String path, StringBuilder shortPath, Int32 shortPathLength);

        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        private static extern Int32 GetLongPathName(String shortPath, StringBuilder longPath, Int32 longPathLength);

        public static string GetLongPathName(string shortPath)
        {
            StringBuilder longPath = new StringBuilder(1024);

            if (0 == PathExtensions.GetLongPathName(shortPath, longPath, longPath.Capacity))
            {
                return shortPath;
            }

            return longPath.ToString();
        }

    }
}