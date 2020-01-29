using System.Threading.Tasks;
using poyosu.Configuration;
using poyosu.Utilities;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;

namespace poyosu.Builders
{
    public class SpinnerBuilder : Builder
    {
        private const int image_size = 1160;
        private const int border_thickness = 24;
        private const int axle_size = 38;

        public override string Folder => "spinner";
        public override string Name => "spinner";

        public override async Task Generate(string path, Parameters parameters)
        {
            var center = new PointF(image_size / 2f, image_size / 2f);

            using (var spinnerBottom = new Image<Rgba32>(image_size, image_size))
            {
                spinnerBottom.Mutate(ctx => ctx
                    .Draw(Rgba32.White, border_thickness, new EllipsePolygon(center, new SizeF(image_size - border_thickness, image_size - border_thickness)))
                    .Fill(Rgba32.White, new EllipsePolygon(center, axle_size / 2f)));

                spinnerBottom.SaveToFileWithHD(System.IO.Path.Combine(path, $"spinner-bottom"), parameters.HD);
            }

            using (var spinnerMiddle = new Image<Rgba32>(image_size, image_size))
            {
                var builder = new PathBuilder(); // TODO: this is horrid but it must do for now
                builder.AddLines(
                    new PointF(604.5f, 543.05f),
                    new PointF(685f, 543.05f),
                    new PointF(676.33f, 558.05f),
                    new PointF(604.5f, 558.05f));
                builder.CloseFigure();

                var line = builder.Build();

                spinnerMiddle.Mutate(ctx => ctx
                    .Fill(Rgba32.White, line)
                    .Fill(Rgba32.White, line.RotateDegree(120f, center))
                    .Fill(Rgba32.White, line.RotateDegree(240f, center)));

                spinnerMiddle.SaveToFileWithHD(System.IO.Path.Combine(path, $"spinner-middle2"), parameters.HD);
            }

            Assets.ImageBlank.SaveToFileWithHD(System.IO.Path.Combine(path, $"spinner-approachcircle"), parameters.HD);
            Assets.ImageBlank.SaveToFileWithHD(System.IO.Path.Combine(path, $"spinner-glow"), parameters.HD);
            Assets.ImageBlank.SaveToFileWithHD(System.IO.Path.Combine(path, $"spinner-middle"), parameters.HD);
            Assets.ImageBlank.SaveToFileWithHD(System.IO.Path.Combine(path, $"spinner-top"), parameters.HD);
            Assets.ImageBlank.SaveToFileWithHD(System.IO.Path.Combine(path, $"spinner-clear"), parameters.HD);
            Assets.ImageBlank.SaveToFileWithHD(System.IO.Path.Combine(path, $"spinner-spin"), parameters.HD);
            Assets.ImageBlank.SaveToFileWithHD(System.IO.Path.Combine(path, $"spinner-rpm"), parameters.HD); // TODO: rpm

            await Task.CompletedTask;
        }
    }
}
