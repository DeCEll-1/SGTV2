using RGL.API.ImGuiHelpers;
using RGL.API.Rendering;
using SGTV2.Impl.CSVClasses;
using SGTV2.Impl.SystemDatas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGTV2.Impl.RS
{
    public class AddStarMenu : RenderScript
    {
        public Dictionary<string, Star> Stars { get; set; }

        // Variables for the ImGui window state
        private string id = "";
        private int type = 0;
        private float radius = 300f;
        private float coronaSize = 100f;
        private float windBurnLevel = 0.5f;
        private float flareProbability = 0.1f;
        private float crLossMult = 1.0f;
        // For orbit methods
        private float locationX = 0f, locationY = 0f;
        private float orbitAngle = 0f, orbitRadius = 1000f, orbitDays = 365f, minSpin = 0.1f, maxSpin = 1.0f;
        private int orbitMethod = 0; // 0 = none, 1 = normal, 2 = down, 3 = spin

        // Overload selector
        private int overloadIdx = 0; // 0, 1, 2 for the three overloads

        // Created object
        private Star created;

        public override void Advance()
        {
            if (!Settings.DisplayStarMenu)
                return;

            ImGui.Begin("Stars");

            ImGui.InputText("ID", ref id, 64);

            string[] planetJsonKeys = Settings.GameResources.PlanetJson.Keys.ToArray();
            ImGui.Combo("Type", ref type, planetJsonKeys, planetJsonKeys.Length);
            ImGui.InputFloat("Radius", ref radius);
            ImGui.InputFloat("Corona Size", ref coronaSize);

            ImGui.Separator();

            ImGui.Text("Select initStar Overload:");
            string[] overloads = {
                "initStar(id, type, radius, coronaSize)",
                "initStar(id, type, radius, coronaSize, windBurnLevel, flareProbability, crLossMult)"
            };

            ImGui.Combo("Overload", ref overloadIdx, overloads, overloads.Length);

            if (overloadIdx == 1)
            {
                ImGui.InputFloat("Wind Burn Level", ref windBurnLevel);
                ImGui.InputFloat("Flare Probability", ref flareProbability);
                ImGui.InputFloat("CR Loss Mult", ref crLossMult);
            }

            if (ImGui.Button("Create Star"))
            {
                switch (overloadIdx)
                {
                    case 0:
                        created = new(id, planetJsonKeys[type], radius, coronaSize);
                        break;
                    case 1:
                        created = new(id, planetJsonKeys[type], radius, coronaSize, windBurnLevel, flareProbability, crLossMult);
                        break;
                }

                ImguiNotification.DisplayNotification("Star Menu", "Star Created", Settings.LogNotificationDurationMS);

                ((Main)Window).SystemScene.Add(created.Mesh);

            }



            ImGui.End();
        }


        public override void Init()
        {
        }
    }
}
