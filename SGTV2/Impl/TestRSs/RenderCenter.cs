using RGL.API.Rendering;
using RGL.API.Rendering.Geometries;
using RGL.API.Rendering.Materials;
using RGL.API.Rendering.MeshClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RGL.API.Extensions;

namespace SGTV2.Impl.TestRSs
{
    internal class RenderCenter : RenderScript
    {
        public override void Init()
        {

            Vector4 col = Color4.Brown.ToVector4();

            MonoColorMaterial mat = new MonoColorMaterial(col);

            Geometry3D geom = new Sphere(16, 16, 100);

            Mesh testCenter = new(geom, mat);

            Scene.Add(testCenter);

        }

        public override void Advance() { }

    }
}
