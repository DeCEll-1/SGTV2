global using OpenTK.Graphics.OpenGL;
global using OpenglTestConsole.Generated.Paths;
global using RGL.Generated.Paths;
global using OpenTK.Mathematics;
global using ImGuiNET;
global using RGL.API;

using OpenTK.Windowing.Desktop;
using RGL.API.Misc;
using SGTV2.Impl.CSVClasses;
using SGTV2.Impl.GameResourceHelpers;


namespace SGTV2
{
    internal class Program
    {
        public static Main main;
        static void Main(string[] args)
        {

            if (args.Length == 0)
                Logger.Log($"Started app with no arguments", LogLevel.Info);
            else
                Logger.Log($"Started app with arguments:\n{string.Concat(args)}", LogLevel.Info);

            // default settings
            Settings.AppName = "SGTV2";
            Settings.MouseSensitivity = 1f;
            Settings.Fov = 90f;


            Settings.Load<Settings>();
            Settings.GameResources = new();
            GameResources res = Settings.GameResources;
            CSVHelper.PutGameCSVs(ref res);
            CSVHelper.PutGameCSVs(ref res);
            JsonHelper.PutGameJson(ref res);
            Settings.GameResources = res;

            GameWindowSettings gameWindowSettings = GameWindowSettings.Default;
            NativeWindowSettings nativeWindowSettings = new NativeWindowSettings
            {
                ClientSize = APISettings.Resolution,
                Title = "SGT",
                DepthBits = 24,
            };


            gameWindowSettings.UpdateFrequency = 60;

            main = new Main(gameWindowSettings, nativeWindowSettings);
            main.Run();
        }

    }
}
