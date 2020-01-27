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
    public class InterfaceBuilder : Builder
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

        private const int menu_button_image_width = 1398;
        private const int menu_button_image_height = 206;
        private const int menu_button_top_margin = 18;
        private const int menu_button_border = 17;
        private const int menu_button_width = 1378;
        private const int menu_button_height = 172;

        private static readonly Rgba32 color_background = Rgba32.FromHex("000000D9");

        private const int regular_selection_width = 148;
        private const int wide_selection_width = 178;
        private const int selection_height = 180;
        private const int selection_top = 6;

        private const int base_selection_glow_blur = 12;
        private const float base_selection_glow_opacity = 0.5f;

        private static readonly Rgba32 color_selection_mode = Rgba32.FromHex("8B3BEE");
        private static readonly Rgba32 color_selection_mods = Rgba32.FromHex("D747AD");
        private static readonly Rgba32 color_selection_options = Rgba32.FromHex("0096ED");
        private static readonly Rgba32 color_selection_random = Rgba32.FromHex("8ED700");

        public override string Folder => "interface";
        public override string Name => "interface";

        public override async Task Generate(string path, Parameters parameters)
        {
            using (var rankingPanel = new Image<Rgba32>(ranking_panel_image_width, ranking_panel_image_height))
            {
                rankingPanel.Mutate(ctx => ctx.Fill(color_background));
                rankingPanel.Mutate(ctx => ctx.Fill(Rgba32.White, new RectangleF(0, 0, ranking_panel_image_width, ranking_panel_border_height)));

                rankingPanel.Mutate(ctx => ctx.Fill(Rgba32.White, new RectangleF(ranking_panel_border_margin, ranking_panel_border_y1, ranking_panel_border_width, ranking_panel_border_height)));
                rankingPanel.Mutate(ctx => ctx.Fill(Rgba32.White, new RectangleF(ranking_panel_border_margin, ranking_panel_border_y2, ranking_panel_border_width, ranking_panel_border_height)));
                rankingPanel.Mutate(ctx => ctx.Fill(Rgba32.White, new RectangleF(ranking_panel_border_margin, ranking_panel_border_y3, ranking_panel_border_width, ranking_panel_border_height)));
                rankingPanel.Mutate(ctx => ctx.Fill(Rgba32.White, new RectangleF(ranking_panel_border_margin, ranking_panel_border_y4, ranking_panel_border_width, ranking_panel_border_height)));

                rankingPanel.SaveToFileWithHD(Path.Combine(path, $"ranking-panel"), parameters.HD);
            }

            await this.GenerateSelectionButton(path, parameters, color_selection_mode, null, wide_selection_width, "mode");
            await this.GenerateSelectionButton(path, parameters, color_selection_mods, Assets.ImageIconStar, regular_selection_width, "mods");
            await this.GenerateSelectionButton(path, parameters, color_selection_options, Assets.ImageIconCog, regular_selection_width, "options");
            await this.GenerateSelectionButton(path, parameters, color_selection_random, Assets.ImageIconDice, regular_selection_width, "random");

            using var menuButton = new Image<Rgba32>(menu_button_image_width, menu_button_image_height);

            menuButton.Mutate(ctx => ctx.Fill(Rgba32.Black, new RectangleF(0, menu_button_top_margin, menu_button_width, menu_button_height)));
            menuButton.Mutate(ctx => ctx.Fill(Rgba32.White, new RectangleF(0, menu_button_top_margin, menu_button_border, menu_button_height)));

            menuButton.SaveToFileWithHD(Path.Combine(path, $"menu-button-background"), parameters.HD);

            await Task.CompletedTask;
        }

        private async Task GenerateSelectionButton(string path, Parameters parameters, Rgba32 color, Image<Rgba32> buttonIcon, int baseWidth, string name)
        {
            int width = baseWidth;
            int height = selection_height;
            int topHeight = selection_top;

            int blur = base_selection_glow_blur;

            double size = 0.3;

            var icon = buttonIcon?.Clone();
            icon.Mutate(ctx => ctx.Resize((int)(buttonIcon.Width * size), (int)(buttonIcon.Height * size)));

            using (var button = new Image<Rgba32>(width, height))
            {
                button.Mutate(ctx => ctx.Fill(color));
                button.Mutate(ctx => ctx.Fill(Rgba32.White, new RectangleF(0, 0, width, topHeight)));

                if (icon != null)
                {
                    var baseCenter = new Point((width - icon.Width) / 2, (topHeight - icon.Height + height) / 2);
                    var glowCenter = new Point((width - (3 * icon.Width)) / 2, (topHeight - (3 * icon.Height) + height) / 2);
                    
                    using (var glow = icon.Clone())
                    {
                        glow.Mutate(ctx => ctx
                            .Pad(glow.Width * 3, glow.Height * 3)
                            .GaussianBlur(blur));
                        button.Mutate(ctx => ctx.DrawImage(glow, glowCenter, base_selection_glow_opacity));
                    }

                    button.Mutate(ctx => ctx.DrawImage(icon, baseCenter));
                }

                button.SaveToFileWithHD(Path.Combine(path, $"selection-{name}"), parameters.HD);
            }

            using (var hovered = new Image<Rgba32>(width, height))
            {
                hovered.Mutate(ctx => ctx.Fill(Rgba32.White));
                hovered.Mutate(ctx => ctx.Fill(color, new RectangleF(0, 0, width, topHeight)));

                if (icon != null)
                {
                    icon.Mutate(ctx => ctx.SetColor(color)); // Icon was already resized.

                    var center = new Point((width - icon.Width) / 2, (topHeight - icon.Height + height) / 2);

                    hovered.Mutate(ctx => ctx.DrawImage(icon, center));
                }

                hovered.SaveToFileWithHD(Path.Combine(path, $"selection-{name}-over"), parameters.HD);
            }

            await Task.CompletedTask;
        }
    }
}
