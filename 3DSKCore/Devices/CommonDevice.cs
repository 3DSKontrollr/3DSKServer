using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _3DSKCore.Devices
{
    /// <summary>
    /// Common Device class.
    /// </summary>
    public class CommonDevice
    {
        public ConsoleInfo Info { get; protected set; }
        public ConsoleStatus Status { get; protected set; }

        #region Input Data
        public Input.Buttons Buttons { get; protected set; }

        public Input.AxisData Axes { get; protected set; }

        public Input.SliderData Sliders { get; protected set; }

        public Input.Report Report { get; protected set; }
        #endregion
    }
}
