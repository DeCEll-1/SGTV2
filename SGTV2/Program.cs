global using OpenTK.Graphics.OpenGL;
global using OpenglTestConsole.Generated.Paths;
global using RGL.Generated.Paths;
global using OpenTK.Mathematics;
global using ImGuiNET;
global using RGL.API;

using OpenTK.Windowing.Desktop;
using RGL.API.Misc;


namespace SGTV2
{
    internal class Program
    {
        public static Main main;
        static void Main(string[] args)
        {
            Logger.Log($"Started app with arguments:\n{string.Concat(args)}", LogLevel.Info);

            Settings.AppName = "SGTV2";

            Settings.Load<Settings>();

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
