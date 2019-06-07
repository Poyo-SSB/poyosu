using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.Processing.Processors;
using SixLabors.Primitives;

namespace poyosu.Utilities
{
    public class TrimProcessor<TPixel> : IImageProcessor<TPixel>
        where TPixel : struct, IPixel<TPixel>
    {
        public void Apply(Image<TPixel> source, Rectangle sourceRectangle)
        {
            int left;
            int top;
            int width;
            int height;

            for (left = 0; left < source.Width; left++)
            {
                bool transparent = true;
                for (int y = 0; y < source.Height; y++)
                {
                    Rgba32 pixel = Rgba32.Transparent;
                    source[left, y].ToRgba32(ref pixel);

                    if (pixel.A != 0)
                    {
                        transparent = false;
                        break;
                    }
                }

                if (!transparent)
                {
                    break;
                }
            }

            for (top = 0; top < source.Height; top++)
            {
                bool transparent = true;
                for (int x = 0; x < source.Width; x++)
                {
                    Rgba32 pixel = Rgba32.Transparent;
                    source[x, top].ToRgba32(ref pixel);

                    if (pixel.A != 0)
                    {
                        transparent = false;
                        break;
                    }
                }

                if (!transparent)
                {
                    break;
                }
            }

            for (width = 0; width < source.Width; width++)
            {
                bool transparent = true;
                for (int y = 0; y < source.Height; y++)
                {
                    Rgba32 pixel = Rgba32.Transparent;
                    source[left + width, y].ToRgba32(ref pixel);

                    if (pixel.A != 0)
                    {
                        transparent = false;
                        break;
                    }
                }

                if (transparent)
                {
                    break;
                }
            }

            for (height = 0; height < source.Height; height++)
            {
                bool transparent = true;
                for (int x = 0; x < source.Width; x++)
                {
                    Rgba32 pixel = Rgba32.Transparent;
                    source[x, top + height].ToRgba32(ref pixel);

                    if (pixel.A != 0)
                    {
                        transparent = false;
                        break;
                    }
                }

                if (transparent)
                {
                    break;
                }
            }

            source.Mutate(ctx => ctx.Crop(new Rectangle(left, top, width, height)));
        }
    }
}