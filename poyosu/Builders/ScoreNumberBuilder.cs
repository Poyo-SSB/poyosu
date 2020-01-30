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
    public class ScoreNumberBuilder : Builder
    {
        private const int image_size = 98;
        private const float font_size = 92;

        public override string Folder => "scorenumbers";
        public override string Name => "score numbers";

        public override async Task Generate(string path, Parameters parameters)
        {
            for (int i = 0; i <= 9; i++)
            {
                this.GenerateText(path, parameters, i.ToString(), i.ToString());
            }
            this.GenerateText(path, parameters, ",", "comma");
            this.GenerateText(path, parameters, ".", "dot");
            this.GenerateText(path, parameters, "%", "percent");
            this.GenerateText(path, parameters, "x", "x");

            await Task.CompletedTask;
        }

        private void GenerateText(string path, Parameters parameters, string text, string fileName)
        {
            using var image = new Image<Rgba32>(image_size, image_size);

            var center = new PointF(image_size / 2f, image_size / 2f);

            image.Mutate(ctx => ctx
                .DrawText(new TextGraphicsOptions
                {
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center,
                }, text.ToString(), new Font(Assets.UniSansBook, font_size), Rgba32.White, center));

            image.Trim(TrimSide.Horizontal);
            image.Mutate(ctx => ctx.Pad(image.Width + 12, image.Height));

            image.SaveToFileWithHD(System.IO.Path.Combine(path, $"score-{fileName}"), parameters.HD);
        }
    }
}
