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

namespace SGTV2
{
    public class Main : GameWindow
    {
        ImGuiController _controller;

        public static Scene mainScene = new Scene();
        public List<EveryFrameScript> EveryFrameScripts { get; set; } =
            new List<EveryFrameScript>();
        public List<RenderScript> RenderScripts { get; set; } = new List<RenderScript>();
        private Camera Camera => mainScene.Camera;

        [SetsRequiredMembers]
        public Main(
            GameWindowSettings gameWindowSettings,
            NativeWindowSettings nativeWindowSettings
        )
            : base(gameWindowSettings, nativeWindowSettings)
        {
            ResourceController.Init(typeof(AppResources));

            mainScene.Camera = new Camera();

            mainScene.Camera.screenWidth = APISettings.Resolution.X;
            mainScene.Camera.screenHeight = APISettings.Resolution.Y;

            mainScene.Camera.Position.Z = 3000f;

            Settings.CameraDepthFar = 4000f;

            Settings.Gamma = 1.0f;

            //CursorState = CursorState.Grabbed; // this isnt an fps cuh!

            //Logger.Log($"{GL.GetInteger(GetPName.MaxVertexAttribs)}", LogLevel.Info);

            GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);
            GL.Enable(EnableCap.DepthTest);
            GL.DepthFunc(DepthFunction.Less);

            Scene.Lights.Add(new Light(new Vector3(-70f, 50f, -50f), new Vector3(1.0f, 1.0f, 1.0f)));

            #region EFSs
            this.EveryFrameScripts.AddRange(
                [
                    new HandleMousePanning(),
                    new HandleZoom()

                ]
            );
            #endregion

            #region Render Scripts

            this.RenderScripts.AddRange(
                [
                    new RenderCenter(),
                    new InitPostProcessing(),


                    new AddStarMenu(),

                    new DisplayMasterWindow(), // importante
                    new DisplayRender(), // the scene render

                    new DisplaySettings(),
                    new DisplayDebug(),

                ]
            );

            #endregion

        }

        protected override void OnLoad()
        {
            base.OnLoad();

            GL.ClearColor(0.0f, 0.1f, 0.05f, 1.0f);

            mainScene.Resolution = APISettings.Resolution;
            mainScene.Init(renderScripts: RenderScripts, everyFrameScripts: EveryFrameScripts, window: this);
            mainScene.SkyboxCubeMap = Resources.Cubemaps[AppResources.Cubemaps.Space_1.Name];

            _controller = new ImGuiController(ClientSize.X, ClientSize.Y, true);

        }

        protected override void OnRenderFrame(FrameEventArgs args)
        {
            base.OnRenderFrame(args);
            if (!this.IsFocused)
                return;

            _controller.Update(this, (float)args.Time);

            // rendered everything we need to render
            mainScene.Render(args: args, window: this);

            //GL.Disable(EnableCap.DepthTest);
            //GL.DepthMask(false);
            //GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);
            //GL.Enable(EnableCap.Blend);

            _controller.Render();

            //GL.DepthMask(true);
            //GL.Enable(EnableCap.DepthTest);
            //GL.Disable(EnableCap.Blend);
            ImGuiController.CheckGLError("End of frame");

            SwapBuffers();
        }

        protected override void OnUpdateFrame(FrameEventArgs args)
        {
            base.OnUpdateFrame(args);

            if (!IsFocused) // check to see if the window is focused
                return;

            mainScene.RunEveryFrameScripts(args: args, window: this);

            OpenTK.Graphics.OpenGL.ErrorCode error = GL.GetError();
            if (error != OpenTK.Graphics.OpenGL.ErrorCode.NoError)
            {
                Logger.LogWithoutGLErrorCheck(error.ToString(), LogLevel.Error);
            }

            if (KeyboardState.IsKeyDown(Keys.Escape))
            {
                Close();
            }


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
    }
}
