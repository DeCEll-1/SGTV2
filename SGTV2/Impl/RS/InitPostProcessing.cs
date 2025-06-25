using RGL.API.Rendering;
using RGL.API.Rendering.Materials;
using RGL.API.Rendering.Textures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGTV2.Impl.RS
{
    internal class InitPostProcessing : RenderScript
    {
        public override void Init()
        {
            PostProcessingMaterial PPMaterial = new PPGammaCorrection();

            PostProcess Effect = new PostProcess(PPMaterial);
            Scene.PostProcesses.Add(Effect);
        }


        public override void Advance() { }
    }
}
