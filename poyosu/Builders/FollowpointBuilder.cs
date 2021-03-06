﻿using System;
using System.Threading.Tasks;
using poyosu.Configuration;
using poyosu.Utilities;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;

namespace poyosu.Builders
{
    public class FollowpointBuilder : Builder
    {
        private const int image_width = 256;
        private const int image_padding = 2;

        private const int base_reference_fps = 60;
        private const int base_delay_frames = 22;
        private const int base_visible_frames = 28;
        private const int base_fade_frames = 3;

        public override string Folder => "followpoints";
        public override string Name => "followpoints";

        public override async Task Generate(string path, Parameters parameters)
        {
            if (!parameters.FollowpointEnabled)
            {
                Assets.ImageBlank.SaveToFileAsPng(System.IO.Path.Combine(path, "followpoint"));
                return;
            }

            float followpointWidth = parameters.FollowpointWidth;

            int imageWidth = image_width;
            int imagePadding = image_padding;

            int imageHeight = (int)Math.Ceiling(parameters.FollowpointWidth);

            using (var followpoint = new Image<Rgba32>(imageWidth, imageHeight))
            {
                var rectangle = new RectangularPolygon(0, (imageHeight - parameters.FollowpointWidth) / 2f, imageWidth, parameters.FollowpointWidth);

                followpoint.Mutate(ctx => ctx
                    .Fill(Rgba32.White, rectangle));

                var taperGradient = new LinearGradientBrush(
                    new PointF(0, 0),
                    new PointF(0, imageHeight),
                    GradientRepetitionMode.None,
                    new ColorStop(0, Rgba32.Black),
                    new ColorStop(1 / 3f, Rgba32.White),
                    new ColorStop(2 / 3f, Rgba32.White),
                    new ColorStop(1, Rgba32.Black));

                if (parameters.AnimationEnabled)
                {
                    float multiplier = base_reference_fps / 60f;

                    float delayFrames = base_delay_frames * multiplier;
                    float visibleFrames = base_visible_frames * multiplier;
                    float fadeFrames = base_fade_frames * multiplier;

                    int totalFrames = (int)Math.Ceiling(delayFrames + visibleFrames);

                    for (int i = 0; i < totalFrames; i++)
                    {
                        if (i < delayFrames)
                        {
                            Assets.ImageBlank.SaveToFileWithHD(System.IO.Path.Combine(path, $"followpoint-{i}"), parameters.HD);
                        }
                        else
                        {
                            using var mask = new Image<Rgba32>(imageWidth, imageHeight);

                            float localTime = i - delayFrames;

                            float xStart = 0;
                            float xEnd = 1;

                            if (localTime <= fadeFrames)
                            {
                                xEnd = (localTime + 1) / (fadeFrames + 1);
                            }

                            if (visibleFrames - localTime <= fadeFrames)
                            {
                                xStart = (fadeFrames + 1 - (visibleFrames - localTime)) / (fadeFrames + 1);
                            }

                            float slide = xEnd - xStart >= 1 / 2f ?
                                1 / 3f :
                                (xEnd - xStart) / 2f;

                            var baseGradient = new LinearGradientBrush(
                                new PointF(0, 0),
                                new PointF(imageWidth, 0),
                                GradientRepetitionMode.None,
                                new ColorStop(xStart, Rgba32.Black),
                                new ColorStop(xStart + slide, Rgba32.White),
                                new ColorStop(xEnd - slide, Rgba32.White),
                                new ColorStop(xEnd, Rgba32.Black));

                            mask.Mutate(ctx => ctx
                                .Fill(taperGradient)
                                .Fill(new GraphicsOptions { ColorBlendingMode = PixelColorBlendingMode.Multiply }, baseGradient));

                            using Image<Rgba32> frame = followpoint.Clone();

                            frame.Mutate(ctx => ctx
                                .Mask(mask)
                                .Pad(imageWidth, imageHeight + (2 * imagePadding)));

                            frame.SaveToFileWithHD(System.IO.Path.Combine(path, $"followpoint-{i}"), parameters.HD);
                        }
                    }
                }
                else
                {
                    using var mask = new Image<Rgba32>(imageWidth, imageHeight);

                    var baseGradient = new LinearGradientBrush(
                        new PointF(0, 0),
                        new PointF(imageWidth / 2f, 0),
                        GradientRepetitionMode.Reflect,
                        new ColorStop(0, Rgba32.Black),
                        new ColorStop(1 / 4f, Rgba32.White));

                    mask.Mutate(ctx => ctx
                        .Fill(taperGradient)
                        .Fill(new GraphicsOptions { ColorBlendingMode = PixelColorBlendingMode.Multiply }, baseGradient)
                        .Fill(new GraphicsOptions { ColorBlendingMode = PixelColorBlendingMode.Add }, baseGradient));

                    followpoint.Mutate(ctx => ctx
                        .Mask(mask)
                        .Pad(imageWidth, imageHeight + (2 * imagePadding)));

                    followpoint.SaveToFileWithHD(System.IO.Path.Combine(path, $"followpoint"), parameters.HD);
                }
            }

            await Task.CompletedTask;
        }
    }
}
