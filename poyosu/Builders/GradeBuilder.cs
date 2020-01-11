using poyosu.Configuration;
using poyosu.Geometry;
using poyosu.Utilities;
using SixLabors.Fonts;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using SixLabors.Primitives;
using SixLabors.Shapes;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace poyosu.Builders
{
    public class GradeBuilder : Builder
    {
        private static readonly Rgba32 color_a = Rgba32.FromHex("5FCD0B");
        private static readonly Rgba32 color_b = Rgba32.FromHex("026AF3");
        private static readonly Rgba32 color_c = Rgba32.FromHex("BF17DF");
        private static readonly Rgba32 color_d = Rgba32.FromHex("E20012");
        private static readonly Rgba32 color_s = Rgba32.FromHex("FF702E");
        private static readonly Rgba32 color_x = Rgba32.FromHex("FFBD0D");
        private static readonly Rgba32 color_sh = Rgba32.FromHex("BDBDBD");
        private static readonly Rgba32 color_xh = Rgba32.FromHex("BDBDBD");

        private const int base_large_size = 900;
        private const int base_small_size = 80;

        private const float base_hexagon_size = 355;
        private const float base_border_radius = 80;

        private const float base_font_size = 350f;
        private const float base_text_glow_blur = 24;
        private const float base_text_glow_opacity = 0.55f;

        private const float base_glow_blur = 40;

        private const float base_border_ratio = 0.86f;

        public override string Folder => "grades";
        public override string Name => "grades";

        public override async Task Generate(string path, Parameters parameters)
        {
            var center = new PointF(base_large_size / 2f, base_large_size / 2f);

            var hexagon = RegularPolygonGenerator.Generate(6, base_hexagon_size, 0, center);
            IPath token = PolygonRounder.Round(hexagon, base_border_radius, 32);

            var smallHexagon = RegularPolygonGenerator.Generate(6, base_border_ratio * base_hexagon_size, 0, center);
            IPath smallToken = PolygonRounder.Round(smallHexagon, base_border_ratio * base_border_radius, 32);

            await Task.WhenAll(new List<Task>
            {
                this.DrawGrade(path, parameters, token, smallToken, color_xh, "S", "xh", true),
                this.DrawGrade(path, parameters, token, smallToken, color_x, "S", "x", true),
                this.DrawGrade(path, parameters, token, smallToken, color_sh, "S", "sh", false),
                this.DrawGrade(path, parameters, token, smallToken, color_s, "S", "s", false),
                this.DrawGrade(path, parameters, token, smallToken, color_a, "A", "a", false),
                this.DrawGrade(path, parameters, token, smallToken, color_b, "B", "b", false, 0.02f),
                this.DrawGrade(path, parameters, token, smallToken, color_c, "C", "c", false),
                this.DrawGrade(path, parameters, token, smallToken, color_d, "D", "d", false, 0.03f)
            });
        }

        private async Task DrawGrade(string path, Parameters parameters, IPath token, IPath smallToken, Rgba32 color, string label, string name, bool two, float xOffset = 0)
        {
            int largeSize = base_large_size;
            int smallSize = base_small_size;
            
            float hexagonSize = base_hexagon_size;
            float borderRadius = base_border_radius;
            
            float fontSize = base_font_size;
            float textGlowBlur = base_text_glow_blur;
            
            float glowBlur = base_glow_blur;

            if (!parameters.HD)
            {
                largeSize /= 2;
                smallSize /= 2;

                hexagonSize /= 2;
                borderRadius /= 2;

                fontSize /= 2;
                textGlowBlur /= 2;

                glowBlur /= 2;
            }

            using var grade = new Image<Rgba32>(largeSize, largeSize);

            var center = new PointF(grade.Width / 2f, grade.Height / 2f);

            using (var outerGlow = new Image<Rgba32>(largeSize, largeSize))
            {
                outerGlow.Mutate(ctx => ctx
                    .Fill(color, token)
                    .GaussianBlur(glowBlur));
                grade.Mutate(ctx => ctx.DrawImage(outerGlow));
            }

            using (var outerRing = new Image<Rgba32>(largeSize, largeSize))
            {
                outerRing.Mutate(ctx => ctx.Fill(Rgba32.White, token));
                grade.Mutate(ctx => ctx.DrawImage(outerRing));
            }

            using (var innerToken = new Image<Rgba32>(largeSize, largeSize))
            {
                innerToken.Mutate(ctx => ctx.Fill(color, smallToken));
                grade.Mutate(ctx => ctx.DrawImage(innerToken));
            }

            using (var text = new Image<Rgba32>(largeSize, largeSize))
            {
                text.Mutate(ctx => ctx
                    .DrawText(new TextGraphicsOptions(true)
                    {
                        HorizontalAlignment = HorizontalAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Center,
                    }, label, new Font(Assets.ExoBlack, fontSize), Rgba32.White, center));

                if (two)
                {
                    text.Mutate(ctx => ctx
                        .DrawText(new TextGraphicsOptions(true)
                        {
                            HorizontalAlignment = HorizontalAlignment.Center,
                            VerticalAlignment = VerticalAlignment.Center,
                        }, label, new Font(Assets.ExoBlack, fontSize), Rgba32.White, center + new Point((int)(fontSize * 0.45f), (int)(fontSize * 0.09f))));
                }

                text.Mutate(ctx => ctx.Trim());

                using (var textGlow = text.Clone())
                {
                    textGlow.Mutate(ctx => ctx
                        .Pad(text.Width * 2, text.Height * 2)
                        .GaussianBlur(textGlowBlur));

                    grade.Mutate(ctx => ctx.DrawImage(textGlow, new Point(((largeSize - textGlow.Width) / 2) + (int)(fontSize * xOffset), (largeSize - textGlow.Height) / 2), base_text_glow_opacity));
                }

                grade.Mutate(ctx => ctx.DrawImage(text, new Point(((largeSize - text.Width) / 2) + (int)(fontSize * xOffset), (largeSize - text.Height) / 2)));
            }

            if (parameters.HD)
            {
                grade.SaveToFile(System.IO.Path.Combine(path, $"ranking-{name}@2x.png"));
                grade.Mutate(ctx => ctx.Resize(grade.Width * smallSize / largeSize, grade.Height * smallSize / largeSize));
                grade.SaveToFile(System.IO.Path.Combine(path, $"ranking-{name}-small@2x.png"));
            }
            else
            {
                grade.SaveToFile(System.IO.Path.Combine(path, $"ranking-{name}.png"));
                grade.Mutate(ctx => ctx.Resize(grade.Width * smallSize / largeSize, grade.Height * smallSize / largeSize));
                grade.SaveToFile(System.IO.Path.Combine(path, $"ranking-{name}-small.png"));
            }

            await Task.CompletedTask;
        }
    }
}
