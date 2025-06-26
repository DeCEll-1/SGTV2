using RGL.API.Misc;
using RGL.API;
using RGL.API.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGTV2.Impl.RS
{
    internal class DisplayMasterWindow : RenderScript
    {
        public override void Advance()
        {
            // Window flags to disable collapse, resize, move, scrollbar, title bar buttons, etc.
            ImGuiWindowFlags windowFlags =
                ImGuiWindowFlags.NoTitleBar |               // No title bar
                ImGuiWindowFlags.NoResize |                 // Disable resizing
                ImGuiWindowFlags.NoMove |                   // Disable moving
                ImGuiWindowFlags.NoCollapse |               // Disable collapsing
                ImGuiWindowFlags.NoBringToFrontOnFocus |    // Optional, no auto focus
                ImGuiWindowFlags.NoNavFocus |               // Optional, disables nav focus
                                                            // ImGuiWindowFlags.NoBackground |             // Make background transparent 
                ImGuiWindowFlags.NoScrollbar |              // Optional, no scrollbars
                ImGuiWindowFlags.MenuBar |                  // Menubar
                ImGuiWindowFlags.NoDocking;                 // prevent docking
                                                            // ImGuiWindowFlags.DockNodeHost;              // Important for docking host

            // Set the window position and size
            ImGui.SetNextWindowPos(new System.Numerics.Vector2(0, 0));
            ImGui.SetNextWindowSize(new System.Numerics.Vector2(APISettings.Resolution.X, APISettings.Resolution.Y));
            ImGui.SetNextWindowViewport(ImGui.GetMainViewport().ID);

            ImGui.PushStyleVar(ImGuiStyleVar.WindowPadding, new System.Numerics.Vector2(0));

            ImGui.Begin("Master Window", windowFlags);

            ImGui.PopStyleVar(); // pop no padding

            if (ImGui.BeginMenuBar())
            {
                if (ImGui.BeginMenu("Tools"))
                { // todo: cook, harder jesse, cook the star
                    if (ImGui.MenuItem("Toggle Settings", null, Settings.DisplaySettingsMenu))
                        Settings.DisplaySettingsMenu = !Settings.DisplaySettingsMenu;

                    ImGui.EndMenu();
                }

                ImGui.EndMenuBar();
            }


            ImGuiDockNodeFlags dockspaceFlags =
            ImGuiDockNodeFlags.PassthruCentralNode;

            uint dockspaceId = ImGui.GetID("MasterDockspace");
            ImGui.DockSpace(dockspaceId, new System.Numerics.Vector2(0, 0), dockspaceFlags);

            ImGui.End();
        }

        public override void Init() { }
    }
}
