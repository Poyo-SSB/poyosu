using System.IO;

namespace poyosu.Utilities
{
    public static class DirectoryUtilities
    {
        public static void EmptyOrCreateDirectory(string path)
        {
            if (Directory.Exists(path))
            {
                try
                {
                    var clean = new DirectoryInfo(path);
                    foreach (FileInfo file in clean.EnumerateFiles())
                    {
                        file.Delete();
                    }
                    foreach (DirectoryInfo dir in clean.EnumerateDirectories())
                    {
                        dir.Delete(true);
                    }
                }
                catch (IOException) { } // .db files. Thank you, Windows.
            }
            else
            {
                Directory.CreateDirectory(path);
            }
        }
    }
}
