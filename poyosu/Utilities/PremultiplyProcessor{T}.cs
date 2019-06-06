using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing.Processors;
using SixLabors.Primitives;
using System.Numerics;

namespace poyosu.Utilities
{
    public class PremultiplyProcessor<TPixel> : IImageProcessor<TPixel>
        where TPixel : struct, IPixel<TPixel>
    {
        public void Apply(Image<TPixel> source, Rectangle sourceRectangle)
        {
            for (int y = 0; y < source.Height; y++)
            {
                for (int x = 0; x < source.Width; x++)
                {
                    var pixel = source[x, y].ToVector4();

                    source[x, y].FromVector4(new Vector4(
                        pixel.X * pixel.W,
                        pixel.Y * pixel.W,
                        pixel.Z * pixel.W,
                        pixel.W));
                }
            }
        }
    }
}