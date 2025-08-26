using OpenTK.Windowing.GraphicsLibraryFramework;
using RGL.API;
using RGL.API.SceneFolder;
using SGTV2.Impl.RS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGTV2.Impl.EFS
{
    internal class HandleMousePanning : EveryFrameScript
    {
        public override void Init()
        {
        }

        private bool isPanning = false;
        private Vector2 LastPos = Vector2.Zero;
        public override void Advance()
        {
            if (!(((Main)Window).OnWindow == typeof(DisplayRender)) && isPanning == false)
                // return if we are not on the main render
                return;

            if (MouseState.IsButtonDown(MouseButton.Right))
            {

                if (!isPanning && Scene.IsMouseOverFBO)
                {
                    isPanning = true;
                    LastPos = new Vector2(MouseState.X, MouseState.Y);
                    return;
                }

                //skip if we just started the panning
                if (LastPos == Vector2.Zero)
                {
                    LastPos = new(MouseState.X, MouseState.Y);
                    return;
                }


                // Calculate the offset of the mouse position
                var deltaX = MouseState.X - LastPos.X;
                var deltaY = MouseState.Y - LastPos.Y;
                LastPos = new Vector2(MouseState.X, MouseState.Y);


                Camera.Position.X -= deltaX * Settings.MouseSensitivity * 3f;
                Camera.Position.Y += deltaY * Settings.MouseSensitivity * 3f;
            }
            else if (MouseState.IsButtonReleased(MouseButton.Right))
            {
                LastPos = Vector2.Zero;
                isPanning = false;
            }


        }

    }
}
