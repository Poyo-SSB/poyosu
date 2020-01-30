using System;
using System.Threading.Tasks;
using poyosu.Configuration;
using poyosu.Utilities;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;

namespace poyosu.Builders
{
    public class ModeIconBuilder : Builder
    {
        private const int large_image_width = 2731;
        private const int large_image_height = 1536;

        private const int medium_image_size = 192;

        private const int small_image_width = 128;
        private const int small_image_height = 172;
        private const int small_icon_size = 90;
        private static readonly Point small_icon_offset = new Point(15, small_image_height - 15 - small_icon_size);

        private const int small_glow_blur = 6;
        private const float small_glow_opacity = 0.5f;

        public override string Folder => "modeicons";
        public override string Name => "mode icons";

        public override async Task Generate(string path, Parameters parameters)
        {
            using (var large = new Image<Rgba32>(large_image_width, large_image_height))
            {
                var shape = new PathBuilder()
                    .AddLines(
                        new PointF(large_image_width / 2f, large_image_height / 2f),
                        new PointF(0.65f * large_image_width, large_image_height),
                        new PointF(large_image_width, large_image_height),
                        new PointF(large_image_width, 0),
                        new PointF(0.65f * large_image_width, 0))
                    .Build();

                large.Mutate(ctx => ctx.Fill(Rgba32.White, shape));

                large.SaveToFileWithHD(System.IO.Path.Combine(path, $"mode-osu"), parameters.HD);
                large.SaveToFileWithHD(System.IO.Path.Combine(path, $"mode-taiko"), parameters.HD);
                large.SaveToFileWithHD(System.IO.Path.Combine(path, $"mode-fruits"), parameters.HD);
                large.SaveToFileWithHD(System.IO.Path.Combine(path, $"mode-mania"), parameters.HD);
            }

            this.GenerateImages(path, parameters, Assets.ImageModeOsu, "osu");
            this.GenerateImages(path, parameters, Assets.ImageModeTaiko, "taiko");
            this.GenerateImages(path, parameters, Assets.ImageModeCatch, "fruits");
            this.GenerateImages(path, parameters, Assets.ImageModeMania, "mania");

            await Task.CompletedTask;
        }

        private void GenerateImages(string path, Parameters parameters, Image<Rgba32> image, string name)
        {
            using (var medium = image.Clone())
            {
                medium.Mutate(ctx => ctx.Resize(medium_image_size, medium_image_size));
                medium.SaveToFileWithHD(System.IO.Path.Combine(path, $"mode-{name}-med"), parameters.HD);
            }

            using var small = new Image<Rgba32>(small_image_width, small_image_height);

            using (var icon = image.Clone())
            {
                icon.Mutate(ctx => ctx.Resize(small_icon_size, small_icon_size));

                using (var smallGlow = new Image<Rgba32>(small_image_width, small_image_height))
                {
                    smallGlow.Mutate(ctx => ctx
                        .DrawImage(icon, small_icon_offset)
                        .GaussianBlur(small_glow_blur));

                    small.Mutate(ctx => ctx.DrawImage(smallGlow, small_glow_opacity));
                }

                small.Mutate(ctx => ctx
                    .DrawImage(icon, small_icon_offset));
            }

            small.SaveToFileWithHD(System.IO.Path.Combine(path, $"mode-{name}-small"), parameters.HD);
        }
    }
}
