using System;
using System.IO;
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
        private const int animated_image_size = 500;

        private const float label_static_proportion = 0.5f;
        private const float label_static_image_proportion = 0.3f;
        private const float label_animated_proportion = label_static_proportion * 0.65f;
        private const float label_animated_image_proportion = label_static_image_proportion * 0.65f;

        private const float glow_static_blur_big = 30f;
        private const float glow_static_blur_small = 10f;
        private const float glow_animated_blur_big = glow_static_blur_big * 0.75f;
        private const float glow_animated_blur_small = glow_static_blur_small * 0.75f;

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

            this.GenerateJudgement(path, parameters, label_static_proportion, label_animated_proportion, image300, color_300, "300", parameters.Judgement300Enabled);
            this.GenerateJudgement(path, parameters, label_static_image_proportion, label_animated_image_proportion, Assets.ImageGlyphGeki, color_300, "300g", parameters.Judgement300Enabled);
            this.GenerateJudgement(path, parameters, label_static_image_proportion, label_animated_image_proportion, Assets.ImageGlyphKatu, color_300, "300k", parameters.Judgement300Enabled);
            this.GenerateJudgement(path, parameters, label_static_proportion, label_animated_proportion, image100, color_100, "100", parameters.Judgement100Enabled);
            this.GenerateJudgement(path, parameters, label_static_image_proportion, label_animated_image_proportion, Assets.ImageGlyphKatu, color_100, "100k", parameters.Judgement100Enabled);
            this.GenerateJudgement(path, parameters, label_static_proportion, label_animated_proportion, image50, color_50, "50", parameters.Judgement100Enabled);
            this.GenerateJudgement(path, parameters, label_static_image_proportion, label_animated_image_proportion, Assets.ImageIconTimes, color_miss, "0", true);

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

        private void GenerateJudgement(
            string path,
            Parameters parameters,
            float staticSize,
            float animatedSize,
            Image<Rgba32> label,
            Rgba32 color,
            string name,
            bool enabled)
        {
            using (var judgement = new Image<Rgba32>(static_image_size, static_image_size))
            {
                var center = new PointF(judgement.Width / 2f, judgement.Height / 2f);

                using (var text = label.Clone())
                {
                    text.Mutate(ctx => ctx
                        .Resize(
                            (int)(static_image_size * staticSize),
                            (int)(static_image_size * staticSize))
                        .Pad(static_image_size, static_image_size));

                    using var labelGlowBig = text.Clone();
                    labelGlowBig.Mutate(ctx => ctx.SetColor(color));

                    using var labelGlowSmall = labelGlowBig.Clone();

                    labelGlowBig.Mutate(ctx => ctx.GaussianBlur(glow_static_blur_big));
                    labelGlowSmall.Mutate(ctx => ctx.GaussianBlur(glow_static_blur_small));

                    judgement.Mutate(ctx => ctx.DrawImage(labelGlowBig));
                    judgement.Mutate(ctx => ctx.DrawImage(labelGlowSmall));
                    judgement.Mutate(ctx => ctx.DrawImage(text));
                }

                judgement.SaveToFileWithHD(Path.Combine(path, $"hit{name}"), parameters.HD);
            }

            if (!enabled)
            {
                Assets.ImageBlank.SaveToFileWithHD(Path.Combine(path, $"hit{name}-0"), parameters.HD);
            }
            else if (parameters.AnimationEnabled)
            {
                int frames = (int)Math.Floor(parameters.AnimationFramerate * parameters.JudgementLength);
                
                for (int i = 0; i < frames - 1; i++)
                {
                    using var frame = new Image<Rgba32>(animated_image_size, animated_image_size);
                    var center = new PointF(frame.Width / 2f, frame.Height / 2f);

                    float time = i / (frames - 1f);
                    float easeTime = 1  - ((time - 1) * (time - 1));

                    float scaleMultiplier = 1 + (easeTime * 0.35f);
                    float opacityMultiplier = Math.Min(1, -3 * (time - 1));

                    using (var text = label.Clone())
                    {
                        text.Mutate(ctx => ctx
                            .Resize(
                                (int)(animated_image_size * animatedSize * scaleMultiplier),
                                (int)(animated_image_size * animatedSize * scaleMultiplier))
                            .Pad(animated_image_size, animated_image_size));

                        using var labelGlowBig = text.Clone();
                        labelGlowBig.Mutate(ctx => ctx.SetColor(color));

                        using var labelGlowSmall = labelGlowBig.Clone();

                        labelGlowBig.Mutate(ctx => ctx.GaussianBlur(glow_animated_blur_big));
                        labelGlowSmall.Mutate(ctx => ctx.GaussianBlur(glow_animated_blur_small));

                        frame.Mutate(ctx => ctx.DrawImage(labelGlowBig));
                        frame.Mutate(ctx => ctx.DrawImage(labelGlowSmall));
                        frame.Mutate(ctx => ctx.DrawImage(text));
                    }

                    frame.Mutate(ctx => ctx.Opacity(opacityMultiplier));

                    frame.SaveToFileWithHD(Path.Combine(path, $"hit{name}-{i}"), parameters.HD);
                }

                // TODO: Add rings around combo finishers

                Assets.ImageBlank.SaveToFileWithHD(Path.Combine(path, $"hit{name}-{frames - 1}"), parameters.HD);
            }
        }
    }
}
