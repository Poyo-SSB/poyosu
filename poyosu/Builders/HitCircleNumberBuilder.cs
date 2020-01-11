using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using poyosu.Configuration;
using poyosu.Utilities;
using SixLabors.Fonts;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using SixLabors.Primitives;

namespace poyosu.Builders
{
    public class HitCircleNumberBuilder : Builder
    {
        private const int base_image_width = 256;
        private const int base_image_height = 256;
        private const float base_font_size = 128;

        public override string Folder => "defaulthitnumber";
        public override string Name => "hit numbers";

        public override async Task Generate(string path, Parameters parameters)
        {
            int width = base_image_width;
            int height = base_image_height;
            float fontSize = base_font_size;

            if (!parameters.HD)
            {
                width /= 2;
                height /= 2;
                fontSize /= 2;
            }

            var center = new PointF(width / 2f, height / 2f);

            for (int i = 0; i <= 9; i++)
            {
                using var text = new Image<Rgba32>(width, height);

                text.Mutate(ctx => ctx
                    .DrawText(new TextGraphicsOptions(true)
                    {
                        HorizontalAlignment = HorizontalAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Center,
                    }, i.ToString(), new Font(Assets.UniSansBold, fontSize), Rgba32.White, center)
                    .Trim());

                if (parameters.HD)
                {
                    text.SaveToFile(Path.Combine(path, $"default-{i}@2x.png"));
                }
                else
                {
                    text.SaveToFile(Path.Combine(path, $"default-{i}.png"));
                }
            }

            await Task.CompletedTask;
        }
    }
}
