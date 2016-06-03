using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3DSKCore.Input
{
    /// <summary>
    /// Nintendo 3DS Buttons.
    /// Includes buttons from both Old3DS and New3DS
    /// </summary>
    [Flags]
    public enum Buttons
    {
        None=0,
        A=1,
        B=2,
        X=4,
        Y=8,
        DPadUp=16,
        DPadDown=32,
        DPadLeft=64,
        DPadRight=128,
        L=256,
        R=512,
        ZL=1024,
        ZR=2048,
        Select=4096,
        Start=8192,
    }

    /// <summary>
    /// EventArgs for both ButtonPressed and ButtonReleased events.
    /// </summary>
    public class ButtonChangedEventArgs : EventArgs
    {
        public Buttons Button { get; set; }
    }
}
