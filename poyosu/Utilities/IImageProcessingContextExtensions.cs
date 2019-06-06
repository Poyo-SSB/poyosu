using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Primitives;
using SixLabors.ImageSharp.Processing;
using SixLabors.Primitives;

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

        public static IImageProcessingContext Premultiply(this IImageProcessingContext source)
            => source.ApplyProcessor(new PremultiplyProcessor());

        public static IImageProcessingContext DrawImage(this IImageProcessingContext source, Image image)
            => source.DrawImage(image, 1);

        public static IImageProcessingContext DrawImage(this IImageProcessingContext source, Image image, Point point)
            => source.DrawImage(image, point, 1);

        public static IImageProcessingContext Mask(this IImageProcessingContext source, Image image)
            => source.ApplyProcessor(new MaskProcessor(image));
    }
}
