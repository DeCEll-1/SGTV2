using ImGuiNET;
using NativeFileDialogSharp;
using RGL.API;
using RGL.API.Extensions;
using RGL.API.Misc;
using RGL.API.Rendering;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SGTV2.Impl.RS
{
    internal class DisplaySettings : RenderScript
    {
        public override void Init() { }

        private bool isPathCorrect = true;
        public override void Advance()
        {

            if (!Settings.DisplaySettingsMenu)
                return;

            ImGui.Begin("Settings");
            if (Settings.GameRoot != null)
            {
                ImGui.Text("Game Folder: ");
                ImGui.Text(Settings.GameRoot.FullName);
                ImGui.SameLine();
                DirectoryInfo oldPath = new(Settings.GameRoot.FullName);
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
                    Settings.GameRoot = oldPath;
                    ImGui.PopStyleColor();
                    ImGui.PopStyleColor();
                }
            }


            Settings.MouseSensitivity = DisplayFloatSlider(Settings.MouseSensitivity, "Mouse Sensitivity", 0.1f, 2f, step: 0.1f);

            Settings.Fov = DisplayFloatSlider(Settings.Fov, "Fov", 45f, 135f, step: 1f);

            Settings.Gamma = DisplayFloatSlider(Settings.Gamma, "Gamma", 0.5f, 2.2f, step: 0.1f);

            // The value is converted from milliseconds to seconds for display, then back to milliseconds for storage.
            Settings.LogNotificationDurationMS = (int)(DisplayFloatSlider((float)(Settings.LogNotificationDurationMS / 1000f), "Notification Duration Seconds", 1, 120, step: 1) * 1000);

            if (ImGui.TreeNodeEx("Advanced Options"))
            {
                ImGui.Unindent();

                Settings.LogForceGC = DisplayCheckbox(Settings.LogForceGC, "LogForceGC");

                // The value is converted from milliseconds to seconds for display, then back to milliseconds for storage.
                Settings.ForceGCIntervalMS = (int)(DisplayFloatSlider(Settings.ForceGCIntervalMS / 1000f, "ForceGCIntervalS", 1, 120, step: 1) * 1000);

                // The value is converted from bytes to megabytes for display, then back to bytes for storage.
                Settings.MinRamBytesForForcedGC = (long)(DisplayFloatSlider(Settings.MinRamBytesForForcedGC / 1048576f, "MinRamMBsForForcedGC", 0, 50, step: 2f) * 1048576);

                ImGui.Indent();
                ImGui.TreePop();
            }


            ImGui.End();
        }

        private float DisplayFloatSlider(float val, string name, float min, float max, float step = 0.05f)
        {
            ImGui.PushItemWidth(250);
            ImGui.SliderFloat("##" + "slider" + name, ref val, min, max);
            ImGui.PopItemWidth();

            ImGui.SameLine();
            ImGui.PushItemWidth(75);
            ImGui.InputFloat("##" + "inputFloat" + name, ref val);
            ImGui.PopItemWidth();

            ImGui.SameLine();
            ImGui.Text(name);

            val = (float)(Math.Round(val / step) * step);

            return val;
        }

        private bool DisplayCheckbox(bool val, string name)
        {
            ImGui.PushItemWidth(20);
            ImGui.Checkbox("##" + "checkbox" + name, ref val);
            ImGui.PopItemWidth();

            ImGui.SameLine();
            ImGui.Text(name);

            return val;
        }

        private unsafe int DisplayIntSlider(int val, string name, int min, int max, int step = 1)
        {
            ImGui.PushItemWidth(250);
            ImGui.SliderInt("##" + "sliderInt" + name, ref val, min, max);
            ImGui.PopItemWidth();

            ImGui.SameLine();
            ImGui.PushItemWidth(75);
            ImGui.InputScalar("##" + "inputInt" + name, ImGuiDataType.S32, (IntPtr)(&val), IntPtr.Zero, IntPtr.Zero, null, ImGuiInputTextFlags.None);
            ImGui.PopItemWidth();

            ImGui.SameLine();
            ImGui.Text(name);

            val = (int)(Math.Round((float)val / step) * step);

            return val;
        }

        private unsafe long DisplayLongSlider(long val, string name, long min, long max)
        {
            long tempLong = val;
            ImGui.PushItemWidth(250);
            ImGui.SliderScalar("##" + "sliderLong" + name, ImGuiDataType.S64, (IntPtr)(&tempLong), (IntPtr)(&min), (IntPtr)(&max));
            ImGui.PopItemWidth();

            ImGui.SameLine();
            ImGui.PushItemWidth(75);
            ImGui.InputScalar("##" + "inputLong" + name, ImGuiDataType.S64, (IntPtr)(&tempLong));
            ImGui.PopItemWidth();

            ImGui.SameLine();
            ImGui.Text(name);

            return tempLong;
        }
    }
}
