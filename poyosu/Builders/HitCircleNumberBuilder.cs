using System;
using System.IO;
using System.Threading.Tasks;
using poyosu.Configuration;
using poyosu.Utilities;
using SixLabors.Fonts;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;

namespace poyosu.Builders
{
    public class HitCircleNumberBuilder : Builder
    {
        private const int image_size = 256;
        private const float font_size = 128;

        public override string Folder => "hitnumbers";
        public override string Name => "hit numbers";

        public override async Task Generate(string path, Parameters parameters)
        {
            var center = new PointF(image_size / 2f, image_size / 2f);

            for (int i = 0; i <= 9; i++)
            {
                using var text = new Image<Rgba32>(image_size, image_size);

                text.Mutate(ctx => ctx
                    .DrawText(new TextGraphicsOptions
                    {
                        HorizontalAlignment = HorizontalAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Center,
                    }, i.ToString(), new Font(Assets.UniSansBold, font_size), Rgba32.White, center));

                text.Trim();

                text.SaveToFileWithHD(System.IO.Path.Combine(path, $"default-{i}"), parameters.HD);
            }

            await Task.CompletedTask;
        }
    }
}
