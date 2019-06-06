using SixLabors.ImageSharp.PixelFormats;

namespace poyosu.Utilities
{
    public static class ColorUtilities
    {
        public static Rgba32 Rgba32FromInt(int rgba32)
            => new Rgba32(
                (byte)((rgba32 >> 16) & 0xFF),
                (byte)((rgba32 >> 8) & 0xFF),
                (byte)(rgba32 & 0xFF));
    }
}
