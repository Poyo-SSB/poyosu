using System.Threading.Tasks;
using poyosu.Configuration;
using poyosu.Utilities;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;

namespace poyosu.Builders
{
    public class ReverseArrowBuilder : Builder
    {
        private const int image_size = 120;

        public override string Folder => "reversearrow";
        public override string Name => "reverse arrow";

        public override async Task Generate(string path, Parameters parameters)
        {
            using var menuButton = new Image<Rgba32>(image_size, image_size);

            var line1 = new Path(new LinearLineSegment(new PointF(67, 60), new PointF(36, 88)));
            var line2 = new Path(new LinearLineSegment(new PointF(36, 32), new PointF(67, 60)));

            menuButton.Mutate(ctx => ctx
                .Draw(new Pen(Rgba32.White, 20), line1)
                .Draw(new Pen(Rgba32.White, 20), line2)
                .Fill(Rgba32.White, new EllipsePolygon(new PointF(36, 32), 10))
                .Fill(Rgba32.White, new EllipsePolygon(new PointF(67, 60), 10))
                .Fill(Rgba32.White, new EllipsePolygon(new PointF(36, 88), 10)));

            menuButton.SaveToFileWithHD(System.IO.Path.Combine(path, $"reversearrow"), parameters.HD);

            await Task.CompletedTask;
        }
    }
}
