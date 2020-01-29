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
    public class PauseButtonBuilder : Builder
    {
        private const int image_width = 740;
        private const int image_height = 220;
        private const float font_size = 128;
        private const float glow_tiny_blur = 3;
        private const float glow_small_blur = 10;
        private const float glow_big_blur = 18;

        private static readonly Rgba32 color_back = Rgba32.FromHex("E8193B");
        private static readonly Rgba32 color_retry = Rgba32.FromHex("F69100");
        private static readonly Rgba32 color_continue = Rgba32.FromHex("A2C80C");
        private static readonly Rgba32 color_replay = Rgba32.FromHex("0078F2");

        public override string Folder => "pausebuttons";
        public override string Name => "pause buttons";

        public override async Task Generate(string path, Parameters parameters)
        {
            await this.GenerateButton(path, parameters, color_back, "back", "back");
            await this.GenerateButton(path, parameters, color_retry, "retry", "retry");
            await this.GenerateButton(path, parameters, color_continue, "continue", "continue");
            await this.GenerateButton(path, parameters, color_replay, "watch replay", "replay");

            await Task.CompletedTask;
        }

        private async Task GenerateButton(string path, Parameters parameters, Rgba32 color, string label, string name)
        {
            using var button = new Image<Rgba32>(image_width, image_height);

            var center = new PointF(image_width / 2f, image_height / 2f);

            using (var text = new Image<Rgba32>(image_width, image_height))
            {
                text.Mutate(ctx => ctx
                    .DrawText(new TextGraphicsOptions
                    {
                        HorizontalAlignment = HorizontalAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Center,
                    }, label, new Font(Assets.UniSansBook, font_size), Rgba32.White, center));

                using (var bigGlow = text.Clone())
                {
                    bigGlow.Mutate(ctx => ctx
                        .SetColor(color)
                        .GaussianBlur(glow_big_blur));
                    button.Mutate(ctx => ctx.DrawImage(bigGlow));
                }

                using (var smallGlow = text.Clone())
                {
                    smallGlow.Mutate(ctx => ctx
                        .SetColor(color)
                        .GaussianBlur(glow_small_blur));
                    button.Mutate(ctx => ctx.DrawImage(smallGlow));
                }

                using (var tinyGlow = text.Clone())
                {
                    tinyGlow.Mutate(ctx => ctx
                        .SetColor(color)
                        .GaussianBlur(glow_tiny_blur));
                    button.Mutate(ctx => ctx.DrawImage(tinyGlow));
                }

                button.Mutate(ctx => ctx.DrawImage(text));
            }

            button.SaveToFileWithHD(System.IO.Path.Combine(path, $"pause-{name}"), parameters.HD);

            await Task.CompletedTask;
        }
    }
}
