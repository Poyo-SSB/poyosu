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
    public class JudgementBuilder : Builder
    {
        private const int base_static_image_size = 400;
        private const int base_animated_image_size = 1280;

        private const float base_font_size = 136f;

        private const float base_glow_blur_big = 30;
        private const float base_glow_blur_small = 10;

        private static readonly Rgba32 color_300 = Rgba32.FromHex("44B5D9");
        private static readonly Rgba32 color_100 = Rgba32.FromHex("21E449");
        private static readonly Rgba32 color_50 = Rgba32.FromHex("F2D326");
        private static readonly Rgba32 color_miss = Rgba32.FromHex("FF0000");

        public override string Folder => "judgements";
        public override string Name => "judgements";

        public override async Task Generate(string path, Parameters parameters)
        {
            int staticImageSize = base_static_image_size;
            int animatedImageSize = base_animated_image_size;

            float fontSize = base_font_size;

            float glowBlurBig = base_glow_blur_big;
            float glowBlurSmall = base_glow_blur_small;

            if (!parameters.HD)
            {
                staticImageSize /= 2;
                animatedImageSize /= 2;

                fontSize /= 2;

                glowBlurBig /= 2;
                glowBlurSmall /= 2;
            }

            using var judgement = new Image<Rgba32>(staticImageSize, staticImageSize);

            var center = new PointF(judgement.Width / 2f, judgement.Height / 2f);

            using (var text = new Image<Rgba32>(staticImageSize, staticImageSize))
            {
                text.Mutate(ctx => ctx
                    .DrawText(new TextGraphicsOptions(true)
                    {
                        HorizontalAlignment = HorizontalAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Center
                    }, "300", new Font(Assets.ExoBlack, fontSize), Rgba32.White, center));

                using var textGlowBig = text.Clone();

                textGlowBig.Mutate(ctx => ctx.SetColor(color_300));

                using var textGlowSmall = textGlowBig.Clone();

                textGlowBig.Mutate(ctx => ctx.GaussianBlur(glowBlurBig));
                textGlowSmall.Mutate(ctx => ctx.GaussianBlur(glowBlurSmall));

                judgement.Mutate(ctx => ctx.DrawImage(textGlowBig));
                judgement.Mutate(ctx => ctx.DrawImage(textGlowSmall));
                judgement.Mutate(ctx => ctx.DrawImage(text));
            }

            judgement.SaveToFileWithHD(Path.Combine(path, $"hit{name}"), parameters.HD);

            if (parameters.AnimationEnabled)
            {
                int frames = (int)Math.Floor(parameters.AnimationFramerate / parameters.JudgementLength);
            }

            await Task.CompletedTask;
        }
    }
}
