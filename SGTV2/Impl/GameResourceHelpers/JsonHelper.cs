using Newtonsoft.Json;
using RGL.API.Misc;
using SGTV2.Impl.CSVClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGTV2.Impl.GameResourceHelpers
{
    public class JsonHelper
    {
        public static void PutGameJson(ref GameResources res)
        {

            string gameConfigFolder = Paths.GameConfigFolder.FullName;

            Dictionary<string, PlanetJson> planetJsons = GetPlanetJsonListFromPath(Path.Combine(gameConfigFolder, Consts.PLANETS_JSON_FILE_NAME));
            var tempRes = res;
            res.PlanetJson = res.PlanetJson.Concat(planetJsons.Where(x => !tempRes.PlanetJson.ContainsKey(x.Key))).ToDictionary(x => x.Key, x => x.Value);
            res = tempRes;

        }


        private static Dictionary<string, PlanetJson> GetPlanetJsonListFromPath(string path)
        {
            JsonSerializer serializer = new JsonSerializer();
            string json = File.ReadAllText(path);
            json = JsonMisc.RemoveHashComments(json);
            return JsonConvert.DeserializeObject<Dictionary<string, PlanetJson>>(json);
        }


    }
}
