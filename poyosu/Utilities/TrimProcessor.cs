using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing.Processors;

namespace poyosu.Utilities
{
    internal class TrimProcessor : IImageProcessor
    {
        public IImageProcessor<TPixel> CreatePixelSpecificProcessor<TPixel>()
            where TPixel : struct, IPixel<TPixel>
            => new TrimProcessor<TPixel>();
    }
}