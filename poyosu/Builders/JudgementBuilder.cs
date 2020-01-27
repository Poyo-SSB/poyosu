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
        private const int static_image_size = 400;
        private const int animated_image_size = 1280;

        private const float label_proportion = 0.5f;
        private const float label_image_proportion = 0.3f;

        private const float glow_blur_big = 30f;
        private const float glow_blur_small = 10f;

        // adjust 100/50/S0 colors
        private static readonly Rgba32 color_300 = Rgba32.FromHex("44B5D9");
        private static readonly Rgba32 color_100 = Rgba32.FromHex("65DD2A");
        private static readonly Rgba32 color_50 = Rgba32.FromHex("D1AA4F");
        private static readonly Rgba32 color_miss = Rgba32.FromHex("FF0000");

        public override string Folder => "judgements";
        public override string Name => "judgements";

        public override async Task Generate(string path, Parameters parameters)
        {
            using var image300 = this.GenerateTextImage("300");
            using var image100 = this.GenerateTextImage("100");
            using var image50 = this.GenerateTextImage("50");

            this.GenerateJudgement(path, parameters, label_proportion, image300, color_300, "300");
            this.GenerateJudgement(path, parameters, label_image_proportion, Assets.ImageGlyphGeki, color_300, "300g");
            this.GenerateJudgement(path, parameters, label_image_proportion, Assets.ImageGlyphKatu, color_300, "300k");
            this.GenerateJudgement(path, parameters, label_proportion, image100, color_100, "100");
            this.GenerateJudgement(path, parameters, label_image_proportion, Assets.ImageGlyphKatu, color_100, "100k");
            this.GenerateJudgement(path, parameters, label_proportion, image50, color_50, "50");
            this.GenerateJudgement(path, parameters, label_image_proportion, Assets.ImageIconTimes, color_miss, "0");

            await Task.CompletedTask;
        }

        private Image<Rgba32> GenerateTextImage(string label)
        {
            var text = new Image<Rgba32>(1024, 1024);
            text.Mutate(ctx => ctx
                .DrawText(new TextGraphicsOptions(true)
                {
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center
                }, label, new Font(Assets.ExoBlack, 512), Rgba32.White, new PointF(512, 512)));
            return text;
        }

        private void GenerateJudgement(string path, Parameters parameters, float size, Image<Rgba32> label, Rgba32 color, string name)
        {
            using (var judgement = new Image<Rgba32>(static_image_size, static_image_size))
            {
                var center = new PointF(judgement.Width / 2f, judgement.Height / 2f);

                using (label)
                {
                    label.Mutate(ctx => ctx
                        .Resize(
                            (int)(static_image_size * size),
                            (int)(static_image_size * size))
                        .Pad(static_image_size, static_image_size));

                    using var labelGlowBig = label.Clone();
                    labelGlowBig.Mutate(ctx => ctx.SetColor(color));

                    using var labelGlowSmall = labelGlowBig.Clone();

                    labelGlowBig.Mutate(ctx => ctx.GaussianBlur(glow_blur_big));
                    labelGlowSmall.Mutate(ctx => ctx.GaussianBlur(glow_blur_small));

                    judgement.Mutate(ctx => ctx.DrawImage(labelGlowBig));
                    judgement.Mutate(ctx => ctx.DrawImage(labelGlowSmall));
                    judgement.Mutate(ctx => ctx.DrawImage(label));
                }

                judgement.SaveToFileWithHD(Path.Combine(path, $"hit{name}"), parameters.HD);
            }

            if (parameters.AnimationEnabled)
            {
                int frames = (int)Math.Floor(parameters.AnimationFramerate / parameters.JudgementLength);
            }
        }
    }
}
