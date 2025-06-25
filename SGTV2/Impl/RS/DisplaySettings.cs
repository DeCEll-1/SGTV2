using ImGuiNET;
using RGL.API;
using RGL.API.Attributes;
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

            PropertyInfo[] properties = ReflectionMisc.GetPublicProperties(typeof(Settings), BindingFlags.Static | BindingFlags.Public).ToArray();

            foreach (PropertyInfo property in properties)
            {
                if (property.PropertyType == typeof(float))
                {


                    var limits = property.GetCustomAttribute<SliderLimitsAttribute>();
                    if (limits == null)
                        continue;

                    float min = limits.Min;
                    float max = limits.Max;



                    ImGui.PushItemWidth(250);
                    float s = (float)property.GetValue(null, null)!;
                    ImGui.SliderFloat("##" + "slider" + property.Name, ref s, min, max);


                    ImGui.SameLine();
                    ImGui.PushItemWidth(75);
                    ImGui.InputFloat("##" + "inputFloat" + property.Name, ref s);

                    ImGui.SameLine();
                    ImGui.Text(property.Name);


                    property.SetValue(null, s);
                }
            }

            ImGui.End();
        }

    }
}
