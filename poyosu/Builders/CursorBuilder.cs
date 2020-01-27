using poyosu.Configuration;
using SixLabors.ImageSharp;
using poyosu.Utilities;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using SixLabors.Primitives;
using SixLabors.Shapes;
using System;
using System.Threading.Tasks;

namespace poyosu.Builders
{
    public class CursorBuilder : Builder
    {
        private const int image_size = 256;

        private const float fill_radius = 20;
        private const float fill_blur = 1.8f;

        private const float inner_glow_radius = 26;
        private const float inner_glow_blur = 12.6f;

        private const float ring_radius = 26;
        private const float ring_blur = 3.3f;

        private const float outer_glow_radius = 52;
        private const float outer_glow_blur = 36f;

        public override string Folder => "cursor";
        public override string Name => "cursor";

        public override async Task Generate(string path, Parameters parameters)
        {
            using var cursor = new Image<Rgba32>(image_size, image_size);

            var center = new PointF(image_size / 2f, image_size / 2f);

            float multiplier = parameters.CursorRadius / ring_radius;

            if (parameters.CursorTrailSmooth)
            {
                // divide by two for ultrasmooth
                multiplier /= 2;
            }

            float fillRadius = fill_radius * multiplier;
            float ringRadius = ring_radius * multiplier;
            float innerGlow_radius = inner_glow_radius * multiplier;
            float outerGlow_radius = outer_glow_radius * multiplier;

            float fillBlur = fill_blur * multiplier;
            float ringBlur = ring_blur * multiplier;
            float innerGlow_blur = inner_glow_blur * multiplier;
            float outerGlow_blur = outer_glow_blur * multiplier;

            using (var outerGlow = new Image<Rgba32>(image_size, image_size))
            {
                outerGlow.Mutate(ctx => ctx
                    .Fill(parameters.CursorColor, new EllipsePolygon(center, outerGlow_radius))
                    .GaussianBlur(outer_glow_blur));

                cursor.Mutate(ctx => ctx.DrawImage(outerGlow));
            }

            using (var innerGlow = new Image<Rgba32>(image_size, image_size))
            {
                innerGlow.Mutate(ctx => ctx
                    .Fill(parameters.CursorColor, new EllipsePolygon(center, innerGlow_radius))
                    .GaussianBlur(inner_glow_blur));

                cursor.Mutate(ctx => ctx.DrawImage(innerGlow));
            }

            using (var ring = new Image<Rgba32>(image_size, image_size))
            {
                ring.Mutate(ctx => ctx
                    .Fill(parameters.CursorColor, new EllipsePolygon(center, ringRadius))
                    .GaussianBlur(ring_blur));

                cursor.Mutate(ctx => ctx.DrawImage(ring));
            }

            using (var fillCenter = new Image<Rgba32>(image_size, image_size))
            {
                fillCenter.Mutate(ctx => ctx
                    .Fill(Rgba32.White, new EllipsePolygon(center, fillRadius))
                    .GaussianBlur(fill_blur));

                cursor.Mutate(ctx => ctx.DrawImage(fillCenter));
            }

            cursor.SaveToFileWithHD(System.IO.Path.Combine(path, $"cursor"), parameters.HD);

            await Task.CompletedTask;
        }
    }
}
