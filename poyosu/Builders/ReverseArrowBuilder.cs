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
    public class ReverseArrowBuilder : Builder
    {
        private const int image_size = 120;

        public override string Folder => "reversearrow";
        public override string Name => "reverse arrow";

        public override async Task Generate(string path, Parameters parameters)
        {
            using var menuButton = new Image<Rgba32>(image_size, image_size);

            var arrow = new Path(new LinearLineSegment(
                new PointF(45, 40),
                new PointF(85, 75),
                new PointF(45, 110)));

            menuButton.Mutate(ctx => ctx
                .Draw(new Pen(Rgba32.White, 20), arrow));

            menuButton.SaveToFileWithHD(System.IO.Path.Combine(path, $"reversearrow"), parameters.HD);

            await Task.CompletedTask;
        }
    }
}
