using poyosu.Configuration;
using poyosu.Utilities;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using SixLabors.Primitives;
using SixLabors.Shapes;
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
                Assets.ImageBlank.SaveToFile(System.IO.Path.Combine(path, "cursortrail.png"));
                return;
            }

            int minSize = 10;
            Rgba32 color = parameters.CursorTrailColor;
            float radius = parameters.CursorTrailRadius;

            if (!parameters.HD)
            {
                radius /= 2;
                minSize /= 2;
            }

            if (parameters.CursorTrailUltrasmooth)
            {
                radius /= 2;
            }

            int size = (int)(radius * 2);

            var colors = new ColorStop[gaussian_samples];

            for (int i = 0; i < gaussian_samples; i++)
            {
                float sample = ImageMath.GaussianFunction((float)i / gaussian_samples, 1, 0, 1 / 3f);

                colors[i] = new ColorStop((float)i / gaussian_samples, new Rgba32(
                    color.R,
                    color.G,
                    color.B,
                    (byte)Math.Round(sample * color.A)));
            }

            using var trail = new Image<Rgba32>(size, size);

            var center = new PointF(radius, radius);

            var brush = new EllipticGradientBrush(
                new PointF(size / 2f, size / 2f),
                new PointF(size, size / 2f),
                1, GradientRepetitionMode.None, colors);

            trail.Mutate(ctx => ctx.Fill(brush, new EllipsePolygon(center, radius)));

            if (size < minSize)
            {
                Logger.Log($"The cursor trail size is less than {minSize}--the trail may not render in-game. Adjust cursor_trail_radius to fix this.", Logger.MessageType.Warning);
            }

            if (parameters.HD)
            {
                trail.SaveToFile(System.IO.Path.Combine(path, "cursortrail@2x.png"));
            }
            else
            {
                trail.SaveToFile(System.IO.Path.Combine(path, "cursortrail.png"));
            }

            if (parameters.CursorTrailSmooth)
            {
                Assets.ImageBlank.SaveToFile(System.IO.Path.Combine(path, "cursormiddle.png"));
            }

            await Task.CompletedTask;
        }
    }
}
