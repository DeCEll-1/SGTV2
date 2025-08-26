using RGL.API.Extensions;
using RGL.API.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NativeFileDialogSharp;

namespace SGTV2.Impl.RS
{
    internal class DisplayGameFolderSelection : RenderScript
    {
        public override void Init() { }

        private static bool isPathCorrect = true;
        public override void Advance()
        {
            if (Settings.DoesGameFolderExists)
                return;

            var size = new System.Numerics.Vector2(600f, 160f);
            ImGui.SetNextWindowSize(size);
            ImGui.SetNextWindowPos(new System.Numerics.Vector2(Settings.Resolution.X / 2f - size.X / 2f, Settings.Resolution.Y / 2f - size.Y / 2f));


            ImGui.PushStyleColor(ImGuiCol.TitleBg, new Vector4i(0, 0, 0, 255).ToUIntForImgui());
            ImGui.PushStyleColor(ImGuiCol.WindowBg, new Vector4i(0, 0, 0, 255).ToUIntForImgui());
            ImGui.PushStyleColor(ImGuiCol.Text, new Vector4i(247, 13, 5, 255).ToUIntForImgui());
            ImGui.Begin("Game Folder Not Found");

            ImGui.Text("Game Folder Not Found");

            ImGui.PopStyleColor();
            ImGui.PopStyleColor();
            ImGui.PopStyleColor();

            ImGui.Text("You must select the game folder, you can change this path in settings later");
            if (ImGui.Button("Set game folder"))
            {
                DialogResult res = Dialog.FolderPicker();
                if (res.IsOk)
                {
                    Settings.GameRoot = new(res.Path);
                    // check if all the paths exist
                    isPathCorrect = Paths.IsValid;
                }
            }

            if (!isPathCorrect)
            {
                ImGui.PushStyleColor(ImGuiCol.WindowBg, new Vector4i(0, 0, 0, 255).ToUIntForImgui());
                ImGui.PushStyleColor(ImGuiCol.Text, new Vector4i(247, 13, 5, 255).ToUIntForImgui());
                ImGui.Text("Incorrect Folder");
                Settings.GameRoot = null;
                ImGui.PopStyleColor();
                ImGui.PopStyleColor();
            }

            ImGui.End();



        }

    }
}
