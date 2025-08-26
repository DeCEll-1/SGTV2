using OpenglTestConsole.Generated.Paths;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using RGL.API.ImGuiHelpers;
using RGL.API.Misc;
using RGL.API.Rendering;
using RGL.API.SceneFolder;
using RGL.API;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGTV2.Impl.EFS;
using SGTV2.Impl.TestRSs;
using RGL.Classes.Implementations.RenderScripts;
using SGTV2.Impl.RS;
using ICSharpCode.Decompiler.CSharp.Resolver;
using RGL.API.Rendering.Textures;

namespace SGTV2
{
    public class Main : GameWindow
    {
        ImGuiController _controller;
        public List<RenderScript> ImguiRenderScripts;
        public Type OnWindow
        {
            get; set
            {
                field = value;
            }
        }
        public Scene SystemScene;

        [SetsRequiredMembers]
        public Main(
            GameWindowSettings gameWindowSettings,
            NativeWindowSettings nativeWindowSettings
        )
            : base(gameWindowSettings, nativeWindowSettings)
        {
            Logger.PrintEmptyLine();
            Logger.LogOpenglAttributes();

            Logger.PrintTestColors();

            ResourceController.Init(typeof(AppResources));

            this.WindowState = Settings.WindowState;


            #region System Rendering Scene

            List<EveryFrameScript> SystemEFSs = new List<EveryFrameScript>()
            {
                    new HandleMousePanning(),
                    new HandleZoom()
            };

            List<RenderScript> SystemRenderScripts = new()
            {
                    //new RenderCenter(),
                    new InitCommonPostProcessing(),

                    new DisplayRender(), // the scene render

            };

            SystemScene = new Scene(Settings.SystemSceneResolution, skyboxCubemap: Resources.Cubemaps[AppResources.Cubemaps.Space_1.Name]);

            SystemScene.Camera.Position.Z = 3000f;

            SystemScene.Init(renderScripts: SystemRenderScripts, everyFrameScripts: SystemEFSs, window: this);

            #endregion


            this.ImguiRenderScripts = new()
            {
                    new DisplayMasterWindow(), // importante

                    new DisplayGameFolderSelection(),

                    new DisplaySettings(),
                    new DisplayDebug(),
                    new AddStarMenu(),
            };

            // init imgui controller
            _controller = new ImGuiController(ClientSize.X, ClientSize.Y, true);

        }

        protected override void OnLoad()
        {
            base.OnLoad();

            GL.ClearColor(0.0f, 0.1f, 0.05f, 1.0f);
            GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);
            GL.Enable(EnableCap.DepthTest);
            GL.DepthFunc(DepthFunction.Less);

            // render the imgui windows (that are unrelated to scenes)
            foreach (RenderScript script in this.ImguiRenderScripts)
            {
                // the scene timer is global
                script.Timer = Scene.Timer; script.Window = this;

                script.Init();
            }
        }

        protected override void OnRenderFrame(FrameEventArgs args)
        {
            base.OnRenderFrame(args);
            if (!this.IsFocused)
                return;

            _controller.Update(this, (float)args.Time);


            // render the imgui windows (that are unrelated to scenes)
            foreach (RenderScript script in this.ImguiRenderScripts)
            {
                // advance the render scripts
                script.args = args; script.Timer = Scene.Timer; script.Window = this;

                script.Advance();
            }


            // rendered the star system
            SystemScene.Render(args: args, window: this);


            ImguiNotification.RenderNotifications();

            _controller.Render();

            ImGuiController.CheckGLError("End of frame");

            SwapBuffers();
        }

        protected override void OnUpdateFrame(FrameEventArgs args)
        {
            base.OnUpdateFrame(args);

            if (!IsFocused) // check to see if the window is focused
                return;

            SystemScene.RunEveryFrameScripts(args: args, window: this);
            
            OpenTK.Graphics.OpenGL.ErrorCode error = GL.GetError();
            if (error != OpenTK.Graphics.OpenGL.ErrorCode.NoError)
            {
                Logger.LogWithoutGLErrorCheck(error.ToString());
            }

            if (KeyboardState.IsKeyDown(Keys.Escape))
            {
                Close();
            }

            this.OnWindow = typeof(Main);


        }

        public override void Close()
        {
            Settings.Save<Settings>();
            base.Close();
        }

        protected override void OnResize(ResizeEventArgs e)
        {
            base.OnResize(e);
            if (APISettings.Resolution == new Vector2(e.Width, e.Height))
                return;

            APISettings.Resolution = new(e.Width, e.Height);
            // Update the opengl viewport
            GL.Viewport(0, 0, e.Width, e.Height);

            // Tell ImGui of the new size
            _controller.WindowResized(e.Width, e.Height);

            Settings.WindowState = this.WindowState;

            //mainScene.UpdateFBOs();
        }

        protected override void OnTextInput(TextInputEventArgs e)
        {
            base.OnTextInput(e);


            _controller.PressChar((char)e.Unicode);
        }

        protected override void OnMouseWheel(MouseWheelEventArgs e)
        {
            base.OnMouseWheel(e);

            _controller.MouseScroll(e.Offset);
        }

        protected override void OnMinimized(MinimizedEventArgs e)
        {
            base.OnMinimized(e);
            Settings.WindowState = this.WindowState;
        }
        protected override void OnMaximized(MaximizedEventArgs e)
        {
            base.OnMaximized(e);
            Settings.WindowState = this.WindowState;
        }

    }
}
