using OpenTK.Windowing.Common;
using RGL.API;
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
        public static WindowState WindowState{ get; set; } = WindowState.Normal;
    }
}
