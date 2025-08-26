
using RGL.API;
using RGL.API.Extensions;
using RGL.API.Misc;
using RGL.API.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace SGTV2.Impl.RS
{
    internal class DisplayDebug : RenderScript
    {
        public override void Init() { }

        public override void Advance()
        {
            ImGui.Begin("Debug");


            bool tempNonPublic = ImguiMisc.DisplayNonPublicVariables;
            ImGui.Checkbox("Display NonPublic Variables", ref tempNonPublic);
            ImguiMisc.DisplayNonPublicVariables = tempNonPublic;


            if (ImGui.TreeNodeEx("System Scene"))
            { // add more as we get more scenes
                ImguiMisc.RenderSceneDebugInfo(((Main)Window).SystemScene);
                ImGui.TreePop();
            }

            ImGui.End();


        }

    }
}
