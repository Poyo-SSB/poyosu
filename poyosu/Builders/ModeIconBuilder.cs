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

            await Task.CompletedTask;
        }
    }
}
