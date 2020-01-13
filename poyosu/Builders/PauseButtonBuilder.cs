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
    public class PauseButtonBuilder : Builder
    {
        private const int base_image_width = 740;
        private const int base_image_height = 220;
        private const float base_font_size = 128;
        private const float base_glow_tiny_blur = 3;
        private const float base_glow_small_blur = 10;
        private const float base_glow_big_blur = 18;

        private static readonly Rgba32 color_back = Rgba32.FromHex("E8193B");
        private static readonly Rgba32 color_retry = Rgba32.FromHex("F69100");
        private static readonly Rgba32 color_continue = Rgba32.FromHex("A2C80C");
        private static readonly Rgba32 color_replay = Rgba32.FromHex("0078F2");

        public override string Folder => "pausebuttons";
        public override string Name => "pause buttons";

        public override async Task Generate(string path, Parameters parameters)
        {
            await GenerateButton(path, parameters, color_back, "back", "back");
            await GenerateButton(path, parameters, color_retry, "retry", "retry");
            await GenerateButton(path, parameters, color_continue, "continue", "continue");
            await GenerateButton(path, parameters, color_replay, "watch replay", "replay");

            await Task.CompletedTask;
        }

        private async Task GenerateButton(string path, Parameters parameters, Rgba32 color, string label, string name)
        {
            int width = base_image_width;
            int height = base_image_height;
            float fontSize = base_font_size;

            float tinyBlur = base_glow_tiny_blur;
            float smallBlur = base_glow_small_blur;
            float bigBlur = base_glow_big_blur;

            if (!parameters.HD)
            {
                width /= 2;
                height /= 2;
                fontSize /= 2;
                tinyBlur /= 2;
                smallBlur /= 2;
                bigBlur /= 2;
            }

            using var button = new Image<Rgba32>(width, height);

            var center = new PointF(width / 2f, height / 2f);

            using (var text = new Image<Rgba32>(width, height))
            {
                text.Mutate(ctx => ctx
                    .DrawText(new TextGraphicsOptions(true)
                    {
                        HorizontalAlignment = HorizontalAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Center,
                    }, label, new Font(Assets.UniSansBook, fontSize), Rgba32.White, center));

                using (var bigGlow = text.Clone())
                {
                    bigGlow.Mutate(ctx => ctx
                        .SetColor(color)
                        .GaussianBlur(bigBlur));
                    button.Mutate(ctx => ctx.DrawImage(bigGlow));
                }

                using (var smallGlow = text.Clone())
                {
                    smallGlow.Mutate(ctx => ctx
                        .SetColor(color)
                        .GaussianBlur(smallBlur));
                    button.Mutate(ctx => ctx.DrawImage(smallGlow));
                }

                using (var tinyGlow = text.Clone())
                {
                    tinyGlow.Mutate(ctx => ctx
                        .SetColor(color)
                        .GaussianBlur(tinyBlur));
                    button.Mutate(ctx => ctx.DrawImage(tinyGlow));
                }

                button.Mutate(ctx => ctx.DrawImage(text));
            }

            if (parameters.HD)
            {
                button.SaveToFileAsPng(Path.Combine(path, $"pause-{name}@2x.png"));
            }
            else
            {
                button.SaveToFileAsPng(Path.Combine(path, $"pause-{name}.png"));
            }

            await Task.CompletedTask;
        }
    }
}
