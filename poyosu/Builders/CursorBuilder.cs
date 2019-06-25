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
        private const int reference_image_size = 256;

        private const float base_fill_radius = 20;
        private const float base_ring_radius = 26;
        private const float base_inner_glow_radius = 26;
        private const float base_outer_glow_radius = 52;
        private const float base_border_radius = 24;

        private const float base_fill_blur = 1.8f;
        private const float base_ring_blur = 3.3f;
        private const float base_inner_glow_blur = 12.6f;
        private const float base_outer_glow_blur = 36f;

        public override string Folder => "cursor";
        public override string Name => "cursor";

        public override async Task Generate(string path, Parameters parameters)
        {
            Rgba32 cursorInnerColor = parameters.CursorInnerColor;
            Rgba32 cursorOuterColor = parameters.CursorOuterColor;
            Rgba32 cursorGlowColor = parameters.CursorGlowColor;
            bool cursorGlowEnabled = parameters.CursorGlowEnabled;
            bool cursorBorderEnabled = parameters.CursorBorderEnabled;

            float multiplier = parameters.CursorRadius / base_ring_radius;

            int imageSize = (int)Math.Ceiling(reference_image_size * multiplier);

            if (!parameters.HD)
            {
                imageSize /= 2;
                multiplier /= 2;
            }

            if (parameters.CursorTrailUltrasmooth)
            {
                imageSize /= 2;
                multiplier /= 2;
            }

            var center = new PointF(imageSize / 2f, imageSize / 2f);

            float fillRadius = base_fill_radius * multiplier;
            float ringRadius = base_ring_radius * multiplier;
            float innerGlowRadius = base_inner_glow_radius * multiplier;
            float outerGlowRadius = base_outer_glow_radius * multiplier;
            float borderRadius = base_border_radius * multiplier;

            float fillBlur = base_fill_blur * multiplier;
            float ringBlur = base_ring_blur * multiplier;
            float innerGlowBlur = base_inner_glow_blur * multiplier;
            float outerGlowBlur = base_outer_glow_blur * multiplier;

            using (var cursor = new Image<Rgba32>(imageSize, imageSize))
            {
                if (cursorGlowEnabled)
                {
                    using (var outerGlow = new Image<Rgba32>(imageSize, imageSize))
                    {
                        outerGlow.Mutate(ctx => ctx
                            .Fill(cursorGlowColor, new EllipsePolygon(center, outerGlowRadius))
                            .GaussianBlur(outerGlowBlur));

                        cursor.Mutate(ctx => ctx.DrawImage(outerGlow));
                    }

                    if (!cursorBorderEnabled)
                    {
                        using (var innerGlow = new Image<Rgba32>(imageSize, imageSize))
                        {
                            innerGlow.Mutate(ctx => ctx
                                .Fill(cursorOuterColor, new EllipsePolygon(center, innerGlowRadius))
                                .GaussianBlur(innerGlowBlur));

                            cursor.Mutate(ctx => ctx.DrawImage(innerGlow));
                        }

                        using (var ring = new Image<Rgba32>(imageSize, imageSize))
                        {
                            ring.Mutate(ctx => ctx
                                .Fill(cursorOuterColor, new EllipsePolygon(center, ringRadius))
                                .GaussianBlur(ringBlur));

                            cursor.Mutate(ctx => ctx.DrawImage(ring));
                        }
                    }
                }

                if (cursorBorderEnabled)
                {
                    using (var border = new Image<Rgba32>(imageSize, imageSize))
                    {
                        border.Mutate(ctx => ctx.Fill(cursorOuterColor, new EllipsePolygon(center, borderRadius)));

                        cursor.Mutate(ctx => ctx.DrawImage(border));
                    }
                }

                using (var fillCenter = new Image<Rgba32>(imageSize, imageSize))
                {
                    fillCenter.Mutate(ctx => ctx.Fill(cursorInnerColor, new EllipsePolygon(center, fillRadius)));

                    if (!cursorBorderEnabled)
                    {
                        fillCenter.Mutate(ctx => ctx.GaussianBlur(fillBlur));
                    }

                    cursor.Mutate(ctx => ctx.DrawImage(fillCenter));
                }

                if (parameters.HD)
                {
                    cursor.SaveToFileAsPng(System.IO.Path.Combine(path, "cursor@2x.png"));
                }
                else
                {
                    cursor.SaveToFileAsPng(System.IO.Path.Combine(path, "cursor.png"));
                }

                await Task.CompletedTask;
            }
        }
    }
}
