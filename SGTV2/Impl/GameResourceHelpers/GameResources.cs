using SGTV2.Impl.GameResourceHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGTV2.Impl.CSVClasses
{
    public class GameResources
    {
        public List<IndustriesCSV> Industries { get; set; } = new();
        public List<SubmarketsCSV> Submarkets { get; set; } = new();
        public List<MarketConditionsCSV> MarketConditions { get; set; } = new();
        public List<PlanetGenData> PlanetGenDatas { get; set; } = new();
        public List<StarGenData> StarGenDatas { get; set; } = new();
        public Dictionary<string, PlanetJson> PlanetJson { get; set; } = new();

    }
}
