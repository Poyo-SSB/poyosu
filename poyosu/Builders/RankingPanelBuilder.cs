using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using poyosu.Configuration;
using poyosu.Utilities;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using SixLabors.Primitives;

namespace poyosu.Builders
{
    public class RankingPanelBuilder : Builder
    {
        private const int image_width = 2732;
        private const int image_height = 1333;

        private const int top = 8;

        private const int border_margin = 20;
        private const int border_width = 1252;
        private const int border_height = 4;

        private const int border_y1 = 209;
        private const int border_y2 = 401;
        private const int border_y3 = 593;
        private const int border_y4 = 783;

        private static readonly Rgba32 ranking_panel_color_background = Rgba32.FromHex("000000D9");

        public override string Folder => "rankingpanel";
        public override string Name => "ranking panel";

        public override async Task Generate(string path, Parameters parameters)
        {
            using (var rankingPanel = new Image<Rgba32>(image_width, image_height))
            {
                rankingPanel.Mutate(ctx => ctx.Fill(ranking_panel_color_background));
                rankingPanel.Mutate(ctx => ctx.Fill(Rgba32.White, new RectangleF(0, 0, image_width, top)));

                rankingPanel.Mutate(ctx => ctx.Fill(Rgba32.White, new RectangleF(border_margin, border_y1, border_width, border_height)));
                rankingPanel.Mutate(ctx => ctx.Fill(Rgba32.White, new RectangleF(border_margin, border_y2, border_width, border_height)));
                rankingPanel.Mutate(ctx => ctx.Fill(Rgba32.White, new RectangleF(border_margin, border_y3, border_width, border_height)));
                rankingPanel.Mutate(ctx => ctx.Fill(Rgba32.White, new RectangleF(border_margin, border_y4, border_width, border_height)));

                rankingPanel.SaveToFileWithHD(Path.Combine(path, $"ranking-panel"), parameters.HD);
            }

            await Task.CompletedTask;
        }
    }
}
