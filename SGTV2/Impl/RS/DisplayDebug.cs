
using RGL.API;
using RGL.API.Misc;
using RGL.API.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
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

            if (ImGui.TreeNodeEx("Scene"))
            {
                ImguiMisc.RenderSceneDebugInfo(Scene);
                ImGui.TreePop();
            }

            ImGui.End();

            
        }

    }
}
