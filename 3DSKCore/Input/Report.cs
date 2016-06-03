using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static _3DSKCore.Constants;

namespace _3DSKCore.Input
{
    /// <summary>
    /// Handles Input Reports
    /// </summary>
    public struct Report
    {
        /// <summary>
        /// Raw Report data
        /// </summary>
        public byte[] raw;

        /// <summary>
        /// Button data
        /// </summary>
        public Buttons buttons;

        /// <summary>
        /// Axis data
        /// </summary>
        public AxisData axes;

        /// <summary>
        /// Slider data
        /// </summary>
        public SliderData sliders;

        /// <summary>
        /// Parses a raw report into input data
        /// </summary>
        /// <param name="rawReport">The byte array containing the raw data</param>
        /// <returns>The parsed input report</returns>
        
        public Report(byte[] rawReport)
        {
            if (rawReport[0] != Packets.InputReport[0])
            {
                throw new FormatException("The provided data is not a report packet.");
            }
            else
            {
                // Raw Report
                raw = rawReport;
                var reportStr = Encoding.UTF8.GetString(rawReport, 1, rawReport.Length-1);
                var data = reportStr.Split(':');
                // Button Data
                buttons = (Input.Buttons)Convert.ToUInt32(data[0]);
                // Axis Data
                axes = new AxisData()
                {
                    X = Convert.ToSingle(data[1]),
                    Y = Convert.ToSingle(data[2]),
                    CX = Convert.ToSingle(data[3]),
                    CY = Convert.ToSingle(data[4]),
                    AccelerometerX = Convert.ToSingle(data[5]),
                    AccelerometerY = Convert.ToSingle(data[6]),
                    AccelerometerZ = Convert.ToSingle(data[7]),
                    GyroscopeRoll = Convert.ToSingle(data[8]),
                    GyroscopePitch = Convert.ToSingle(data[9]),
                    GyroscopeYaw = Convert.ToSingle(data[10]),
                };
                // Slider Data
                sliders = new SliderData()
                {
                    Volume = Convert.ToByte(data[11]),
                    ThreeD = Convert.ToByte(data[12]),
                };
            }
        }
    }

    /// <summary>
    /// EventArgs for OnReport events
    /// </summary>
    public class OnReportEventArgs : EventArgs
    {
        public Report report { get; set; }
    }
}
