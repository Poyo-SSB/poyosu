using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using System.IO;

namespace poyosu.Utilities
{
    public static class ImageUtilities
    {
        public static void SaveToFileAsPng<T>(this Image<T> image, string path) where T : struct, IPixel<T>
        {
            using var stream = new FileStream(path, FileMode.Create);
            image.SaveAsPng(stream);
        }
    }
}
