using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing.Processors;
using SixLabors.Primitives;

namespace poyosu.Utilities
{
    internal class MaskProcessor : IImageProcessor
    {
        private readonly Image image;

        public MaskProcessor(Image<Rgba32> image) => this.image = image;

        public void Apply(Image<Rgba32> source, Rectangle sourceRectangle)
        {
            for (int y = 0; y < source.Height; y++)
            {
                for (int x = 0; x < source.Width; x++)
                {
                    Rgba32 pixel = source[x, y];
                    Rgba32 mask = this.image[x, y];

                    byte alpha = (byte)(pixel.A * (mask.R + mask.G + mask.B) / 3f * 255f);

                    source[x, y] = new Rgba32(pixel.R, pixel.G, pixel.B, alpha);
                }
            }
        }

        public IImageProcessor<TPixel> CreatePixelSpecificProcessor<TPixel>() where TPixel : struct, IPixel<TPixel> => throw new System.NotImplementedException();
    }
}