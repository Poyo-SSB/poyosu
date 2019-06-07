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

        private const float base_font_size = 400f;

        private const float base_glow_blur = 10;

        private const float base_border_ratio = 0.86f;

        public override string Folder => "grades";
        public override string Name => "grades";

        public override async Task Generate(string path, Parameters parameters)
        {
            var center = new PointF(base_large_size / 2f, base_large_size / 2f);

            var hexagon = new RegularPolygon(center, 6, base_hexagon_size);
            //IPath token = PolygonRounder.Round(hexagon, base_border_radius, 32);

            var smallHexagon = new RegularPolygon(center, 6, base_border_ratio * base_hexagon_size);
            //IPath smallToken = PolygonRounder.Round(smallHexagon, base_border_ratio * base_border_radius, 32);

            await Task.WhenAll(new List<Task>
            {
                this.DrawGrade(path, parameters, hexagon, smallHexagon, color_xh, "S", "xh", true),
                this.DrawGrade(path, parameters, hexagon, smallHexagon, color_x, "S", "x", true),
                this.DrawGrade(path, parameters, hexagon, smallHexagon, color_sh, "S", "sh", false),
                this.DrawGrade(path, parameters, hexagon, smallHexagon, color_s, "S", "s", false),
                this.DrawGrade(path, parameters, hexagon, smallHexagon, color_a, "A", "a", false),
                this.DrawGrade(path, parameters, hexagon, smallHexagon, color_b, "B", "b", false),
                this.DrawGrade(path, parameters, hexagon, smallHexagon, color_c, "C", "c", false),
                this.DrawGrade(path, parameters, hexagon, smallHexagon, color_d, "D", "d", false)
            });
        }

        private async Task DrawGrade(string path, Parameters config, IPath token, IPath smallToken, Rgba32 color, string label, string name, bool two)
        {
            using var grade = new Image<Rgba32>(base_large_size, base_large_size);

            var center = new PointF(base_large_size / 2f, base_large_size / 2f);

            using (var outerGlow = new Image<Rgba32>(base_large_size, base_large_size))
            {
                outerGlow.Mutate(ctx => ctx
                    .Fill(color, token)
                    .GaussianBlur(base_glow_blur));

                grade.Mutate(ctx => ctx.DrawImage(outerGlow));
            }

            using (var outerRing = new Image<Rgba32>(base_large_size, base_large_size))
            {
                outerRing.Mutate(ctx => ctx.Fill(Rgba32.White, token));

                grade.Mutate(ctx => ctx.DrawImage(outerRing));
            }

            using (var innerToken = new Image<Rgba32>(base_large_size, base_large_size))
            {
                innerToken.Mutate(ctx => ctx.Fill(color, smallToken));

                grade.Mutate(ctx => ctx.DrawImage(innerToken));
            }

            using (var text = new Image<Rgba32>(base_large_size, base_large_size))
            {
                text.Mutate(ctx => ctx
                    .DrawText(new TextGraphicsOptions(true)
                    {
                        HorizontalAlignment = HorizontalAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Center,
                    }, label, new Font(Assets.ExoBlack, base_font_size), Rgba32.White, center));

                grade.Mutate(ctx => ctx.DrawImage(text));
            }

            grade.SaveToFileAsPng(System.IO.Path.Combine(path, $"ranking-{name}@2x.png"));
            grade.Mutate(ctx => ctx.Resize(grade.Width * base_small_size / base_large_size, grade.Height * base_small_size / base_large_size));
            grade.SaveToFileAsPng(System.IO.Path.Combine(path, $"ranking-{name}-small@2x.png"));

            await Task.CompletedTask;
        }
    }
}
