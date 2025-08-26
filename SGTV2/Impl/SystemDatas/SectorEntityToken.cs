using RGL.API.Rendering.MeshClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGTV2.Impl.SystemDatas
{
    public class SectorEntityToken
    {

        public Mesh Mesh { get; set; }
        public string ID { get; set; }
        public Vector2 Location { get; set; }
        public SectorEntityToken OrbitFocus { get; set; }
        public float Radius { get; set; }
        public float OrbitAngle { get; set; }
        public float OrbitRadius { get; private set; }
        public float OrbitDays { get; set; }
        public float MinSpin { get; set; }
        public float MaxSpin { get; set; }
        public OrbitMode OrbitMode { get; set; } = OrbitMode.FixedLocation;
    }

    public enum OrbitMode
    {
        FixedLocation,
        CircularOrbit,
        CircularOrbitPointingDown,
        CircularOrbitWithSpin,
    }
}
