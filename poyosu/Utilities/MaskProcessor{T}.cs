using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing.Processors;
using SixLabors.Primitives;
using System.Numerics;

namespace poyosu.Utilities
{
    internal class MaskProcessor<TPixel> : IImageProcessor<TPixel>
        where TPixel : struct, IPixel<TPixel>
    {
        private readonly Image<TPixel> image;

        public MaskProcessor(Image image) => this.image = image.CloneAs<TPixel>();

        public void Apply(Image<TPixel> source, Rectangle sourceRectangle)
        {
            for (int y = 0; y < source.Height; y++)
            {
                for (int x = 0; x < source.Width; x++)
                {
                    var pixel = source[x, y].ToVector4();
                    var mask = this.image[x, y].ToVector4();

                    byte alpha = (byte)(pixel.W * (mask.X + mask.Y + mask.Z) / 3f * 255f);

                    source[x, y].FromVector4(new Vector4(pixel.X, pixel.Y, pixel.Z, alpha));
                }
            }
        }
    }
}