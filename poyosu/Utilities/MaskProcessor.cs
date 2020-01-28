using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing.Processors;

namespace poyosu.Utilities
{
    internal class MaskProcessor : IImageProcessor
    {
        private readonly Image image;

        public MaskProcessor(Image image) => this.image = image;

        public IImageProcessor<TPixel> CreatePixelSpecificProcessor<TPixel>()
            where TPixel : struct, IPixel<TPixel>
            => new MaskProcessor<TPixel>(this.image);
    }
}