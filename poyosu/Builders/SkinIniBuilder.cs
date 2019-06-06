using poyosu.Configuration;
using System.IO;
using System.Threading.Tasks;

namespace poyosu.Builders
{
    public class SkinIniBuilder : Builder
    {
        public override string Folder => "ini";
        public override string Name => "skin.ini";

        public override async Task Generate(string path, Parameters parameters)
        {
            // TODO
            File.WriteAllText(Path.Combine(path, $"skin.ini"), $@"[General]
Name: poyosu (dev build)
Author: Poyo_SSB
Website: https://github.com/Poyo-SSB/poyosu
Version: 2.5
AnimationFramerate: {parameters.AnimationFramerate}
CursorExpand: 0
CursorRotate: 0
CursorTrailRotate: 0
AllowSliderBallTint: 0

[Colours]
Combo1: 242,80,54
Combo2: 255,168,4
Combo3: 248,222,50
Combo4: 99,255,38
Combo5: 4,221,246
Combo6: 0,148,255
Combo7: 70,102,241
Combo8: 236,99,209

InputOverlayText: 255,255,255
SliderTrackOverride: 0,0,0

SongSelectActiveText: 255,255,255
SongSelectInactiveText: 128,128,128

SpinnerBackground: 255,255,255");

                await Task.CompletedTask;
        }
    }
}
