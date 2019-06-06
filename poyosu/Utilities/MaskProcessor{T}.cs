using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Primitives;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.Processing.Processors;
using SixLabors.Primitives;
using System.Numerics;

namespace poyosu.Utilities
{
    internal class MaskProcessor<TPixel> : IImageProcessor<TPixel>
        where TPixel : struct, IPixel<TPixel>
    {
        private readonly Image<TPixel> image;

        public MaskProcessor(Image image) {
            this.image = image.CloneAs<TPixel>();

            this.image.Mutate(ctx => ctx.Filter(new ColorMatrix
            {
                M14 = -1f,
                M24 = -1f,
                M34 = -1f,
                M54 = 1f
            }));
        }

        public void Apply(Image<TPixel> source, Rectangle sourceRectangle)
            => source.Mutate(ctx => ctx.DrawImage(this.image, PixelColorBlendingMode.Normal, PixelAlphaCompositionMode.DestOut, 1f));
    }
}