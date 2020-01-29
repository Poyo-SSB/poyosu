using poyosu.Configuration;
using poyosu.Utilities;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using System;
using System.Threading.Tasks;

namespace poyosu.Builders
{
    public class CursorTrailBuilder : Builder
    {
        private const int gaussian_samples = 16;

        public override string Folder => "cursortrail";
        public override string Name => "cursor trail";

        public override async Task Generate(string path, Parameters parameters)
        {
            if (!parameters.CursorTrailEnabled)
            {
                Assets.ImageBlank.SaveToFileWithHD(System.IO.Path.Combine(path, $"cursortrail"), parameters.HD);
                return;
            }

            int size = (int)(parameters.CursorTrailRadius * 2);

            if (parameters.CursorTrailSmooth)
            {
                // divide by two for ultrasmooth
                size /= 2;
            }

            var colors = new ColorStop[gaussian_samples];

            for (int i = 0; i < gaussian_samples; i++)
            {
                float sample = ImageMath.GaussianFunction((float)i / gaussian_samples, 1, 0, 1 / 3f);

                colors[i] = new ColorStop((float)i / gaussian_samples, new Rgba32(
                    parameters.CursorTrailColor.R,
                    parameters.CursorTrailColor.G,
                    parameters.CursorTrailColor.B,
                    (byte)Math.Round(sample * parameters.CursorTrailColor.A)));
            }

            using var trail = new Image<Rgba32>(size, size);

            var center = new PointF(size / 2f, size / 2f);

            var brush = new EllipticGradientBrush(
                new PointF(size / 2f, size / 2f),
                new PointF(size, size / 2f),
                1, GradientRepetitionMode.None, colors);

            trail.Mutate(ctx => ctx.Fill(brush, new EllipsePolygon(center, parameters.CursorTrailRadius)));

            if (size < 10)
            {
                Logger.Log($"The cursor trail size is less than 10--the trail may not render in-game. Adjust cursor_trail_radius to fix this.", Logger.MessageType.Warning);
            }

            trail.SaveToFileWithHD(System.IO.Path.Combine(path, $"cursortrail"), parameters.HD);

            if (parameters.CursorTrailSmooth)
            {
                Assets.ImageBlank.SaveToFileWithHD(System.IO.Path.Combine(path, $"cursormiddle"), parameters.HD);
            }

            await Task.CompletedTask;
        }
    }
}
