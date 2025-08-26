using RGL.API;
using SGTV2.Impl.RS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGTV2.Impl.EFS
{
    internal class HandleZoom : EveryFrameScript
    {
        public override void Init() { }
        public override void Advance()
        {
            if (!(((Main)Window).OnWindow == typeof(DisplayRender)))
                // return if we are not on the main render
                return;

            //Camera.Position.Z += (MouseState.Scroll - MouseState.PreviousScroll).X;
            Camera.Position.Z -= MouseState.ScrollDelta.Y * 50f; // i dont have scroll dawg 💔💔💔

        }


    }
}
