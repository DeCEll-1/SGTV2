using OpenTK.Windowing.GraphicsLibraryFramework;
using RGL.API;
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

        private Vector2 LastPos = Vector2.Zero;
        public override void Advance()
        {

            if (MouseState.IsButtonDown(MouseButton.Right)) // This bool variable is initially set to true.
            {
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


                Camera.Position.X -= deltaX;
                Camera.Position.Y += deltaY;
            }
            else if (MouseState.IsButtonReleased(MouseButton.Right))
                LastPos = Vector2.Zero;


        }

    }
}
