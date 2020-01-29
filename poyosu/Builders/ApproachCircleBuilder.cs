using System.Threading.Tasks;
using poyosu.Configuration;
using poyosu.Utilities;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;

namespace poyosu.Builders
{
    public class ApproachCircleBuilder : Builder
    {
        private const int image_size = 256;
        private const int border_thickness = 10;

        public override string Folder => "approachcircle";
        public override string Name => "approach circle";

        public override async Task Generate(string path, Parameters parameters)
        {
            var center = new PointF(image_size / 2f, image_size / 2f);

            using var approachCircle = new Image<Rgba32>(image_size, image_size);

            approachCircle.Mutate(ctx => ctx
                .Draw(Rgba32.White, border_thickness, new EllipsePolygon(center, new SizeF(image_size - border_thickness, image_size - border_thickness))));

            approachCircle.SaveToFileWithHD(System.IO.Path.Combine(path, $"approachcircle"), parameters.HD);

            await Task.CompletedTask;
        }
    }
}
