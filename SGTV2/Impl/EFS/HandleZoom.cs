using RGL.API;
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
            //Camera.Position.Z += (MouseState.Scroll - MouseState.PreviousScroll).X;
            Camera.Position.Z -= MouseState.ScrollDelta.Y * 50f; // i dont have scroll dawg 💔💔💔
        }


    }
}
