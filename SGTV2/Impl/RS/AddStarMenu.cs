﻿using RGL.API.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGTV2.Impl.RS
{
    public class AddStarMenu : RenderScript
    {
        public override void Advance()
        {
            ImGui.Begin("AddStar");

            ImGui.End();
        }


        public override void Init()
        {
        }
    }
}
