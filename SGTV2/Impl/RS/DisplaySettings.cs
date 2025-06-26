using ImGuiNET;
using RGL.API;
using RGL.API.Misc;
using RGL.API.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SGTV2.Impl.RS
{
    internal class DisplaySettings : RenderScript
    {
        public override void Init() { }

        public override void Advance()
        {

            if (!Settings.DisplaySettingsMenu)
                return;


            ImGui.Begin("Settings");

            ImGui.End();
        }

    }
}
