using System.IO;
using System.Threading.Tasks;
using poyosu.Configuration;
using poyosu.Utilities;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using SixLabors.Primitives;
using SixLabors.Shapes;

namespace poyosu.Builders
{
    public class StarsBuilder : Builder
    {
        private const int star_image_size = 100;
        private const int star2_image_size = 96;

        public override string Folder => "stars";
        public override string Name => "stars";

        public override async Task Generate(string path, Parameters parameters)
        {
            if (parameters.EnableUiStars)
            {
                using var star = new Image<Rgba32>(star2_image_size, star2_image_size);

                var center = new PointF(star2_image_size / 2f, star2_image_size / 2f);

                var brush = new EllipticGradientBrush(
                    new PointF(star2_image_size / 2f, star2_image_size / 2f),
                    new PointF(star2_image_size, star2_image_size / 2f),
                    1, GradientRepetitionMode.None, new ColorStop(0, Rgba32.White), new ColorStop(0, Rgba32.Transparent));

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
