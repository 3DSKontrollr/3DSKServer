using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3DSKCore.Input
{
    /// <summary>
    /// Nintendo 3DS Axis data
    /// </summary>
    public struct AxisData
    {
        public float X;
        public float Y;
        public float CX;
        public float CY;
        public float AccelerometerX;
        public float AccelerometerY;
        public float AccelerometerZ;
        public float GyroscopeRoll;
        public float GyroscopePitch;
        public float GyroscopeYaw;
    }

    /// <summary>
    /// Nintendo 3DS Axis Enum
    /// </summary>
    public enum Axes
    {
        X,
        Y,
        CX,
        CY,
        AccelerometerX,
        AccelerometerY,
        AccelerometerZ,
        GyroscopeRoll,
        GyroscopePitch,
        GyroscopeYaw,
    }

    /// <summary>
    /// EventArgs for AxisChanged events.
    /// </summary>
    public class AxisChangedEventArgs : EventArgs
    {
        public Axes Axis { get; set; }
        
        public float newValue { get; set; }
    }
}
