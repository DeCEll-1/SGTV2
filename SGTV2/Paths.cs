using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGTV2
{
    public class Paths
    {
        /// <summary>
        /// The root directory of the game. Must be set manually before using other properties.
        /// Example: D:\Game Files\Fractal Softworks\0.97a-RC11
        /// </summary>
        public static DirectoryInfo GameRoot => Settings.GameRoot;

        /// <summary>
        /// D:\Game Files\Fractal Softworks\0.97a-RC11\mods
        /// </summary>
        public static DirectoryInfo ModsFolderRoot => GameRoot == null ? null : new DirectoryInfo(Path.Combine(GameRoot.FullName, "mods"));

        /// <summary>
        /// D:\Game Files\Fractal Softworks\0.97a-RC11\starsector-core
        /// </summary>
        public static DirectoryInfo GameCore => GameRoot == null ? null : new DirectoryInfo(Path.Combine(GameRoot.FullName, "starsector-core"));

        /// <summary>
        /// D:\Game Files\Fractal Softworks\0.97a-RC11\starsector-core\data\campaign
        /// </summary>
        public static DirectoryInfo GameCoreCampaignFolder => GameCore == null ? null : new DirectoryInfo(Path.Combine(GameCore.FullName, "data", "campaign"));

        /// <summary>
        /// D:\Game Files\Fractal Softworks\0.97a-RC11\starsector-core\data\config
        /// </summary>
        public static DirectoryInfo GameConfigFolder => GameCore == null ? null : new DirectoryInfo(Path.Combine(GameCore.FullName, "data", "config"));

        public static bool IsValid
        {
            get
            {
                return
                    Paths.GameRoot.Exists &&
                    Paths.ModsFolderRoot.Exists &&
                    Paths.GameCore.Exists &&
                    Paths.GameCoreCampaignFolder.Exists &&
                    Paths.GameConfigFolder.Exists;
            }
        }
    }
}
