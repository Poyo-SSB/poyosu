using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;

namespace poyosu.Utilities
{
    public static class IImageProcessingContextExtensions
    {
        public static IImageProcessingContext SetColor(this IImageProcessingContext source, IPixel color)
            => source.Filter(GenerateColorMatrix(color));

        private static ColorMatrix GenerateColorMatrix(IPixel color)
        {
            var vector = color.ToVector4();
            return new ColorMatrix
            {
                // RGB are implicitly multiplied by 0, then the color value is added.
                M51 = vector.X,
                M52 = vector.Y,
                M53 = vector.Z,
                // Alpha is multiplied by 1.
                M44 = 1F
            };
        }

        public static IImageProcessingContext DrawImage(this IImageProcessingContext source, Image image)
            => source.DrawImage(image, 1);

        public static IImageProcessingContext DrawImage(this IImageProcessingContext source, Image image, Point point)
            => source.DrawImage(image, point, 1);

        public static IImageProcessingContext Mask(this IImageProcessingContext source, Image image)
        {
            image = image.CloneAs<Rgba32>();

            image.Mutate(ctx => ctx.Filter(new ColorMatrix
            {
                M14 = -1f,
                M24 = -1f,
                M34 = -1f,
                M54 = 1f
            }));

            return source.DrawImage(image, PixelColorBlendingMode.Normal, PixelAlphaCompositionMode.DestOut, 1);
        }
    }
}
