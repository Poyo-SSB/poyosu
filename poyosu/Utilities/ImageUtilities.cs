﻿using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using System.IO;

namespace poyosu.Utilities
{
    public static class ImageUtilities
    {
        public static void SaveToFileAsPng<T>(this Image<T> image, string path) where T : struct, IPixel<T>
        {
            using var stream = new FileStream(path + ".png", FileMode.Create);
            image.SaveAsPng(stream);
        }

        public static void SaveToFileWithHD<T>(this Image<T> image, string path, bool hd) where T : struct, IPixel<T>
        {
            if (hd)
            {
                image.SaveToFileAsPng(path + "@2x");
            }
            else
            {
                using var clone = image.Clone();
                clone.Mutate(ctx => ctx.Resize(clone.Width / 2, clone.Height / 2));
                clone.SaveToFileAsPng(path);
            }
        }
    }
}
