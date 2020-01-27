using System;
using System.Collections.Generic;
using System.Text;
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
    public class ApproachCircleBuilder : Builder
    {
        private const int image_size = 256;
        private const int border = 10;

        public override string Folder => "approachcircle";
        public override string Name => "approach circle";

        public override async Task Generate(string path, Parameters parameters)
        {
            using var approachCircle = new Image<Rgba32>(image_size, image_size);

            var center = new PointF(image_size / 2f, image_size / 2f);

            approachCircle.Mutate(c => c
                .Draw(Rgba32.FromHex("FFFFFF"), border, new EllipsePolygon(center, new SizeF(image_size - border, image_size - border))));

            approachCircle.SaveToFileWithHD(System.IO.Path.Combine(path, $"approachcircle"), parameters.HD);

            await Task.CompletedTask;
        }
    }
}
