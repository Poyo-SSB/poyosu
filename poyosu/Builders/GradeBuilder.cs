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

        private const int large_size = 900;
        private const int small_size = 80;

        private const float hexagon_size = 355;
        private const float border_radius = 80;

        private const float font_size = 350f;
        private const float text_glow_blur = 24;
        private const float text_glow_opacity = 0.55f;

        private const float glow_blur = 40;

        private const float border_ratio = 0.86f;

        public override string Folder => "grades";
        public override string Name => "grades";

        public override async Task Generate(string path, Parameters parameters)
        {
            var center = new PointF(large_size / 2f, large_size / 2f);

            var hexagon = RegularPolygonGenerator.Generate(6, hexagon_size, 0, center);
            IPath token = PolygonRounder.Round(hexagon, border_radius, 32);

            var smallHexagon = RegularPolygonGenerator.Generate(6, border_ratio * hexagon_size, 0, center);
            IPath smallToken = PolygonRounder.Round(smallHexagon, border_ratio * border_radius, 32);

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
            using var grade = new Image<Rgba32>(large_size, large_size);

            var center = new PointF(grade.Width / 2f, grade.Height / 2f);

            using (var outerGlow = new Image<Rgba32>(large_size, large_size))
            {
                outerGlow.Mutate(ctx => ctx
                    .Fill(color, token)
                    .GaussianBlur(glow_blur));
                grade.Mutate(ctx => ctx.DrawImage(outerGlow));
            }

            using (var outerRing = new Image<Rgba32>(large_size, large_size))
            {
                outerRing.Mutate(ctx => ctx.Fill(Rgba32.White, token));
                grade.Mutate(ctx => ctx.DrawImage(outerRing));
            }

            using (var innerToken = new Image<Rgba32>(large_size, large_size))
            {
                innerToken.Mutate(ctx => ctx.Fill(color, smallToken));
                grade.Mutate(ctx => ctx.DrawImage(innerToken));
            }

            using (var text = new Image<Rgba32>(large_size, large_size))
            {
                text.Mutate(ctx => ctx
                    .DrawText(new TextGraphicsOptions(true)
                    {
                        HorizontalAlignment = HorizontalAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Center,
                    }, label, new Font(Assets.ExoBlack, font_size), Rgba32.White, center));

                if (two)
                {
                    text.Mutate(ctx => ctx
                        .DrawText(new TextGraphicsOptions(true)
                        {
                            HorizontalAlignment = HorizontalAlignment.Center,
                            VerticalAlignment = VerticalAlignment.Center,
                        }, label, new Font(Assets.ExoBlack, font_size), Rgba32.White, center + new Point((int)(font_size * 0.45f), (int)(font_size * 0.09f))));
                }

                text.Mutate(ctx => ctx.Trim());

                using (var textGlow = text.Clone())
                {
                    textGlow.Mutate(ctx => ctx
                        .Pad(text.Width * 2, text.Height * 2)
                        .GaussianBlur(text_glow_blur));

                    grade.Mutate(ctx => ctx.DrawImage(textGlow, new Point(((large_size - textGlow.Width) / 2) + (int)(font_size * xOffset), (large_size - textGlow.Height) / 2), text_glow_opacity));
                }

                grade.Mutate(ctx => ctx.DrawImage(text, new Point(((large_size - text.Width) / 2) + (int)(font_size * xOffset), (large_size - text.Height) / 2)));
            }

            grade.SaveToFileWithHD(System.IO.Path.Combine(path, $"ranking-{name}"), parameters.HD);
            grade.Mutate(ctx => ctx.Resize(grade.Width * small_size / large_size, grade.Height * small_size / large_size));
            grade.SaveToFileWithHD(System.IO.Path.Combine(path, $"ranking-{name}-small"), parameters.HD);

            await Task.CompletedTask;
        }
    }
}
