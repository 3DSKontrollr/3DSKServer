using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3DSKCore.Input
{
    /// <summary>
    /// Nintendo 3DS Slider data
    /// </summary>
    public struct SliderData
    {
        public int Volume;
        public int ThreeD;
    }

    /// <summary>
    /// Nintendo 3DS Slider Enum
    /// </summary>
    public enum Sliders
    {
        Volume,
        ThreeD
    }

    /// <summary>
    /// EventArgs for SliderChanged events.
    /// </summary>
    public class SliderChangedEventArgs : EventArgs
    {
        public Sliders Slider { get; set; }
        
        public int newValue { get; set; }
    }
}
