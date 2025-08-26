using Newtonsoft.Json;
using RGL.API.JSON;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGTV2.Impl.GameResourceHelpers
{
    public class PlanetJson
    {

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("tilt")]
        public float Tilt { get; set; }

        [JsonProperty("pitch")]
        public float Pitch { get; set; }

        [JsonProperty("rotation")]
        public float Rotation { get; set; }

        [JsonProperty("planetColor")]
        [JsonConverter(typeof(NewtonsoftVector4JsonConverter))]
        public Vector4 PlanetColor { get; set; }

        [JsonProperty("atmosphereThickness")]
        public float AtmosphereThickness { get; set; }

        [JsonProperty("atmosphereThicknessMin")]
        public float AtmosphereThicknessMin { get; set; }

        [JsonProperty("atmosphereColor")]
        [JsonConverter(typeof(NewtonsoftVector4JsonConverter))]
        public Vector4 AtmosphereColor { get; set; }

        [JsonProperty("texture")]
        public string Texture { get; set; }

        [JsonProperty("starscapeIcon")]
        public string StarscapeIcon { get; set; }

        [JsonProperty("icon")]
        public string Icon { get; set; }

        [JsonProperty("iconColor")]
        [JsonConverter(typeof(NewtonsoftVector4JsonConverter))]
        public Vector4 IconColor { get; set; }

        [JsonProperty("starCoronaSprite")]
        public string StarCoronaSprite { get; set; }

        [JsonProperty("starCoronaColor")]
        [JsonConverter(typeof(NewtonsoftVector4JsonConverter))]
        public Vector4? StarCoronaColor { get; set; }

        [JsonProperty("starCoronaSizeMult")]
        public float StarCoronaSizeMult { get; set; }

        [JsonProperty("isStar")]
        public bool IsStar { get; set; }

        [JsonProperty("lightPosition")]
        [JsonConverter(typeof(NewtonsoftVector3JsonConverter))]
        public Vector3 LightPosition { get; set; }
    }
}

