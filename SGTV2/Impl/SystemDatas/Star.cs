using RGL.API.Rendering.Geometries;
using RGL.API.Rendering.Materials;
using RGL.API.Rendering.MeshClasses;
using SGTV2.Impl.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGTV2.Impl.SystemDatas
{
    public class Star : SectorEntityToken
    {
        public Vector2 Location { get; set; }
        public bool IsInit { get; set; }
        public string Type { get; set; }
        public float CoronaSize { get; set; }
        public float WindBurnLevel { get; set; }
        public float FlareProbability { get; set; }
        public float CRLossMult { get; set; }

        public Star(string ID, string Type, float Radius, float CoronaSize)
        {
            this.ID = ID;
            this.Type = Type;
            this.Radius = Radius;
            this.CoronaSize = CoronaSize;

            Geometry3D geom = new Sphere(16, 16, Radius);
            TextureMaterial mat = new TextureMaterial(ResourceHelper.GetTypeTexture(Type));

            this.Mesh = new Mesh(geom, mat);
            this.Mesh.IsTransparent = true;

        }

        public Star(string ID, string Type, float Radius, float CoronaSize, float WindBurnLevel, float FlareProbability, float CRLossMult)
        {
            this.ID = ID;
            this.Type = Type;
            this.Radius = Radius;
            this.CoronaSize = CoronaSize;
            this.WindBurnLevel = WindBurnLevel;
            this.FlareProbability = FlareProbability;
            this.CRLossMult = CRLossMult;

            Geometry3D geom = new Sphere(16, 16, Radius);
            TextureMaterial mat = new TextureMaterial(ResourceHelper.GetTypeTexture(Type));

            this.Mesh = new Mesh(geom, mat);
            this.Mesh.IsTransparent = true;
        }




    }
}
