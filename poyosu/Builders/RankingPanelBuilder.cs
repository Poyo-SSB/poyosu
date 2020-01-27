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
        private const int ranking_panel_image_width = 2732;
        private const int ranking_panel_image_height = 1333;
        private const int ranking_panel_top = 8;
        private const int ranking_panel_border_margin = 20;
        private const int ranking_panel_border_width = 1252;
        private const int ranking_panel_border_height = 4;
        private const int ranking_panel_border_y1 = 209;
        private const int ranking_panel_border_y2 = 401;
        private const int ranking_panel_border_y3 = 593;
        private const int ranking_panel_border_y4 = 783;

        private static readonly Rgba32 ranking_panel_color_background = Rgba32.FromHex("000000D9");

        public override string Folder => "rankingpanel";
        public override string Name => "ranking panel";

        public override async Task Generate(string path, Parameters parameters)
        {
            using (var rankingPanel = new Image<Rgba32>(ranking_panel_image_width, ranking_panel_image_height))
            {
                rankingPanel.Mutate(ctx => ctx.Fill(ranking_panel_color_background));
                rankingPanel.Mutate(ctx => ctx.Fill(Rgba32.White, new RectangleF(0, 0, ranking_panel_image_width, ranking_panel_top)));

                rankingPanel.Mutate(ctx => ctx.Fill(Rgba32.White, new RectangleF(ranking_panel_border_margin, ranking_panel_border_y1, ranking_panel_border_width, ranking_panel_border_height)));
                rankingPanel.Mutate(ctx => ctx.Fill(Rgba32.White, new RectangleF(ranking_panel_border_margin, ranking_panel_border_y2, ranking_panel_border_width, ranking_panel_border_height)));
                rankingPanel.Mutate(ctx => ctx.Fill(Rgba32.White, new RectangleF(ranking_panel_border_margin, ranking_panel_border_y3, ranking_panel_border_width, ranking_panel_border_height)));
                rankingPanel.Mutate(ctx => ctx.Fill(Rgba32.White, new RectangleF(ranking_panel_border_margin, ranking_panel_border_y4, ranking_panel_border_width, ranking_panel_border_height)));

                rankingPanel.SaveToFileWithHD(Path.Combine(path, $"ranking-panel"), parameters.HD);
            }

            await Task.CompletedTask;
        }
    }
}
