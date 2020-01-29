using System.IO;
using System.Threading.Tasks;
using poyosu.Configuration;
using poyosu.Utilities;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;

namespace poyosu.Builders
{
    public class StarsBuilder : Builder
    {
        private const int star_image_size = 90;
        private const int star2_image_size = 96;

        private const float star2_opacity_center = 0.05f;
        private const float star2_opacity_edge = 0.1f;

        public override string Folder => "stars";
        public override string Name => "stars";

        public override async Task Generate(string path, Parameters parameters)
        {
            if (parameters.EnableUiStars)
            {
                using var star = new Image<Rgba32>(star2_image_size, star2_image_size);

                var center = new PointF(star2_image_size / 2f, star2_image_size / 2f);

                var brush = new EllipticGradientBrush(
                    center,
                    new PointF(star2_image_size, star2_image_size / 2f),
                    1, GradientRepetitionMode.None,
                    new ColorStop(0f, new Rgba32(1f, 1f, 1f, star2_opacity_center)),
                    new ColorStop(1f, new Rgba32(1f, 1f, 1f, star2_opacity_edge)));

                star.Mutate(ctx => ctx.Fill(brush, new EllipsePolygon(center, star2_image_size / 2f)));

                star.SaveToFileWithHD(System.IO.Path.Combine(path, $"star2"), parameters.HD);
            }
            else
            {
                Assets.ImageBlank.SaveToFileWithHD(System.IO.Path.Combine(path, $"star2"), parameters.HD);
            }

            using (var star = Assets.ImageIconStar.Clone())
            {
                star.Mutate(ctx => ctx.Resize(star_image_size, star_image_size));
                star.SaveToFileWithHD(System.IO.Path.Combine(path, $"star"), parameters.HD);
            }

            await Task.CompletedTask;
        }
    }
}
