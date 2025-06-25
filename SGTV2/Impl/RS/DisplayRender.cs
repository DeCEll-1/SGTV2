using RGL.API.Misc;
using RGL.API.Rendering;
using RGL.API.Rendering.Textures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGTV2.Impl.RS
{
    internal class DisplayRender : RenderScript
    {
        public override void Advance()
        {

            ImGui.Begin("Scene");

            Texture tex = RenderMisc.GetScreenFBO(Scene).ColorTexture;

            IntPtr texId = new IntPtr(tex.Handle);

            System.Numerics.Vector2 size = ImGui.GetWindowSize();
            Vector2i newSceneSize = new((int)size.X, (int)size.Y);

            if (newSceneSize != APISettings.SceneResolution)
            {
                APISettings.SceneResolution = newSceneSize;
                Scene.UpdateFBOs();
            }

            var uv0 = new System.Numerics.Vector2(0f, 1f);
            var uv1 = new System.Numerics.Vector2(1f, 0f);

            ImGui.Image(texId, size, uv0, uv1);

            ImGui.End();
        }

        public override void Init()
        {
        }
    }
}
