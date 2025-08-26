using OpenTK.Windowing.Common;
using RGL.API;
using RGL.API.Attributes;
using SGTV2.Impl.CSVClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGTV2
{
    public class Settings : APISettings
    {
        public static bool DisplaySettingsMenu { get; set; } = false;
        public static bool DisplayStarMenu { get; set; } = false;
        public static WindowState WindowState { get; set; } = WindowState.Normal;
        public static Vector2i SystemSceneResolution { get; set; } = new(900, 900);
        public static List<DirectoryInfo> ModsToLoad { get; set; } = new();
        public static DirectoryInfo GameRoot { get; set; }

        [DoNotSave]
        public static GameResources GameResources { get; set; }
        [DoNotSave]
        public static bool DoesGameFolderExists => Directory.Exists(GameRoot?.FullName);
    }
}
