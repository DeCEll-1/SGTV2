using CsvHelper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGTV2.Impl.CSVClasses
{
    public class CSVHelper
    {

        public static void PutGameCSVs(ref GameResources csv)
        {
            if (!Settings.DoesGameFolderExists)
                return;


            string coreCampaignFolderPath = Paths.GameCoreCampaignFolder.FullName;

            csv.Industries.AddRange(GetIndrustryListFromPath(
                Path.Combine(coreCampaignFolderPath, Consts.INDUSTRIES_FILE_NAME)));

            csv.Submarkets.AddRange(GetSubmarketsListFromPath(
                Path.Combine(coreCampaignFolderPath, Consts.SUBMARKETS_FILE_NAME)));

            csv.MarketConditions.AddRange(GetMarketConditionsListFromPath(
                Path.Combine(coreCampaignFolderPath, Consts.MARKET_CONDITIONS_FILE_NAME)));

            csv.PlanetGenDatas.AddRange(GetPlanetGenDataListFromPath(
                Path.Combine(coreCampaignFolderPath, Consts.PROCGEN_FOLDER_NAME, Consts.PLANET_GEN_DATA_FILE_NAME)));

            csv.StarGenDatas.AddRange(GetStarGenDataListFromPath(
                Path.Combine(coreCampaignFolderPath, Consts.PROCGEN_FOLDER_NAME, Consts.STAR_GEN_DATA_FILE_NAME)));

            foreach (DirectoryInfo modDirectory in Settings.ModsToLoad)
            {
                string modPath = modDirectory.FullName;

                string modCampaignPath = Path.Combine(modPath, "data", "campaign");

                string industriesPath = Path.Combine(modCampaignPath, Consts.INDUSTRIES_FILE_NAME);
                if (File.Exists(industriesPath))
                {
                    csv.Industries.AddRange(GetIndrustryListFromPath(industriesPath));
                }

                string submarketsPath = Path.Combine(modCampaignPath, Consts.SUBMARKETS_FILE_NAME);
                if (File.Exists(submarketsPath))
                {
                    csv.Submarkets.AddRange(GetSubmarketsListFromPath(submarketsPath));
                }

                string marketConditionsPath = Path.Combine(modCampaignPath, Consts.MARKET_CONDITIONS_FILE_NAME);
                if (File.Exists(marketConditionsPath))
                {
                    csv.MarketConditions.AddRange(GetMarketConditionsListFromPath(marketConditionsPath));
                }

                string procgenFolderPath = Path.Combine(modCampaignPath, Consts.PROCGEN_FOLDER_NAME);

                string planetGenDataPath = Path.Combine(procgenFolderPath, Consts.PLANET_GEN_DATA_FILE_NAME);
                if (File.Exists(planetGenDataPath))
                {
                    csv.PlanetGenDatas.AddRange(GetPlanetGenDataListFromPath(planetGenDataPath));
                }

                string starGenDataPath = Path.Combine(procgenFolderPath, Consts.STAR_GEN_DATA_FILE_NAME);
                if (File.Exists(starGenDataPath))
                {
                    csv.StarGenDatas.AddRange(GetStarGenDataListFromPath(starGenDataPath));
                }

            }
        }


        #region Industries

        /// <summary>
        /// get industry csv
        /// </summary>
        /// <param name="path"></param>
        /// <returns>null if not found anything</returns>
        public static List<IndustriesCSV> GetIndrustryListFromPath(string path)
        {
            List<IndustriesCSV> records = null;

            //https://joshclose.github.io/CsvHelper/getting-started/#reading-a-csv-file

            using (var reader = new StreamReader(path))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                records = csv.GetRecords<IndustriesCSV>().ToList();
            }

            string modName = path.Split('\\')[path.Split('\\').Length - 4];
            records.ForEach(record =>
            {
                record.owner = modName;
            });

            List<IndustriesCSV> stuffToDelete = new List<IndustriesCSV>();

            foreach (IndustriesCSV industry in records)
            {

                if (industry.id == "" || industry.id == null || industry.id.Contains("#"))
                {
                    stuffToDelete.Add(industry);
                    continue;
                }

            }

            foreach (IndustriesCSV industryToDelete in stuffToDelete) { records.Remove(industryToDelete); }

            return records;

        }

        #endregion

        #region Submarkets

        /// <summary>
        /// get submarkets csv
        /// </summary>
        /// <param name="path"></param>
        /// <returns>null if not found anything</returns>
        public static List<SubmarketsCSV> GetSubmarketsListFromPath(string path)
        {

            List<SubmarketsCSV> records = null;

            //https://joshclose.github.io/CsvHelper/getting-started/#reading-a-csv-file

            using (var reader = new StreamReader(path))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                records = csv.GetRecords<SubmarketsCSV>().ToList();
            }

            string modName = path.Split('\\')[path.Split('\\').Length - 4];
            records.ForEach(record =>
            {
                record.owner = modName;
            });

            List<SubmarketsCSV> stuffToDelete = new List<SubmarketsCSV>();

            foreach (SubmarketsCSV submarket in records)
            {
                if (submarket.id == "" || submarket.id == null || submarket.id.Contains("#"))
                {
                    stuffToDelete.Add(submarket);
                    continue;
                }
            }

            foreach (SubmarketsCSV submarketToDelete in stuffToDelete) { records.Remove(submarketToDelete); }

            return records;

        }
        #endregion

        #region MarketConditions

        /// <summary>
        /// get market conditions csv
        /// </summary>
        /// <param name="path"></param>
        /// <returns>null if not found anything</returns>
        public static List<MarketConditionsCSV> GetMarketConditionsListFromPath(string path)
        {

            List<MarketConditionsCSV> records = null;

            //https://joshclose.github.io/CsvHelper/getting-started/#reading-a-csv-file

            using (var reader = new StreamReader(path))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                records = csv.GetRecords<MarketConditionsCSV>().ToList();
            }

            string modName = path.Split('\\')[path.Split('\\').Length - 4];
            records.ForEach(record =>
            {
                record.owner = modName;
            });

            List<MarketConditionsCSV> stuffToDelete = new List<MarketConditionsCSV>();

            foreach (MarketConditionsCSV conditions in records)
            {
                if (conditions.id == "" || conditions.id == null || conditions.id.Contains("#"))
                {
                    stuffToDelete.Add(conditions);
                    continue;
                }
            }

            foreach (MarketConditionsCSV conditionToRemove in stuffToDelete) { records.Remove(conditionToRemove); }

            return records;

        }

        #endregion

        #region PlanetGenData

        /// <summary>
        /// get market conditions csv
        /// </summary>
        /// <param name="path"></param>
        /// <returns>null if not found anything</returns>
        public static List<PlanetGenData> GetPlanetGenDataListFromPath(string path)
        {
            List<PlanetGenData> records = null;

            //https://joshclose.github.io/CsvHelper/getting-started/#reading-a-csv-file

            using (var reader = new StreamReader(path))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                records = csv.GetRecords<PlanetGenData>().ToList();
            }

            string modName = path.Split('\\')[path.Split('\\').Length - 5];
            records.ForEach(record =>
            {
                record.owner = modName;
            });

            List<PlanetGenData> stuffToDelete = new List<PlanetGenData>();

            foreach (PlanetGenData genData in records)
            {
                if (genData.type == "" || genData.id.Contains("#"))
                {
                    stuffToDelete.Add(genData);
                    continue;
                }
            }

            foreach (PlanetGenData genDataToDelete in stuffToDelete) { records.Remove(genDataToDelete); }

            return records;

        }

        #endregion

        #region PlanetGenData

        /// <summary>
        /// get market conditions csv
        /// </summary>
        /// <param name="path"></param>
        /// <returns>null if not found anything</returns>
        public static List<StarGenData> GetStarGenDataListFromPath(string path)
        {
            List<StarGenData> records = null;

            //https://joshclose.github.io/CsvHelper/getting-started/#reading-a-csv-file

            using (var reader = new StreamReader(path))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                records = csv.GetRecords<StarGenData>().ToList();
            }

            string modName = path.Split('\\')[path.Split('\\').Length - 5];
            records.ForEach(record =>
            {
                record.owner = modName;
            });

            List<StarGenData> stuffToDelete = new List<StarGenData>();

            foreach (StarGenData genData in records)
            {
                if (genData.id == "" || genData.id.Contains("#"))
                {
                    stuffToDelete.Add(genData);
                    continue;
                }
            }

            foreach (StarGenData genDataToDelete in stuffToDelete) { records.Remove(genDataToDelete); }

            return records;
        }

        #endregion
    }
}
