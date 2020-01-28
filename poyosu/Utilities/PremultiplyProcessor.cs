using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing.Processors;

namespace poyosu.Utilities
{
    public class PremultiplyProcessor : IImageProcessor
    {
        public IImageProcessor<TPixel> CreatePixelSpecificProcessor<TPixel>()
            where TPixel : struct, IPixel<TPixel>
            => new PremultiplyProcessor<TPixel>();
    }
}