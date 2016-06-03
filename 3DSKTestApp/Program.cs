using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using _3DSKCore.Devices;
using System.Net.Sockets;
using _3DSKCore.Input;
using ScpDriverInterface;
using System.Runtime.InteropServices;
using System.Diagnostics;
using NAudio.CoreAudioApi;

namespace _3DSKTestApp
{
    
    class Program
    {
        #region VolumeControl
        private static void SetVolume(int value)
        {
            try
            {
                MMDeviceEnumerator DevEnum = new MMDeviceEnumerator();
                MMDevice device = DevEnum.GetDefaultAudioEndpoint(DataFlow.Render, Role.Multimedia);

                device.AudioEndpointVolume.MasterVolumeLevelScalar = (float)value / 100.0f;
            }
            catch (Exception)
            {
            }
        }
        #endregion
        public static Dictionary<IPAddress, CommonDevice> devices { get; set; } = new Dictionary<IPAddress, CommonDevice>();
        static void Main(string[] args)
        {
            var _info = new ConsoleInfo()
            {
                Address = IPAddress.Parse("192.168.0.101"),
            };

            UdpClient udpServer = new UdpClient(33333);
            TcpClient asd = new TcpClient();

            var _console = new HomebrewConsole(_info, udpServer, asd);

            devices.Add(_info.Address, _console);

            _console.OnReport += _console_OnReport;

            _console.OnUpdate += _console_OnUpdate;

            awe.PlugIn(2);

            Console.ReadLine();
            Console.Write((int)Buttons.A + "" + (int)Buttons.B + "" + (int)Buttons.X +" "+ (int)Buttons.Y);

            awe.Unplug(2);
        }

        static int cVol = 0;

        static ScpBus awe = new ScpBus();

        private static void _console_OnUpdate(object sender, EventArgs e)
        {
        }

        private static short CircleToSquare(float Input)
        {
            if (Input > 128) Input = 128;
            if (Input < -128) Input = -128;
            return (short)((Input / 128) * 32767); ;
        }

        private static void _console_OnReport(object s, _3DSKCore.Input.OnReportEventArgs e)
        {
            var sender = (CommonDevice)s;
            var XC = new X360Controller();
            XC.LeftStickX = CircleToSquare(sender.Axes.X);
            XC.LeftStickY = CircleToSquare(sender.Axes.Y);
            XC.LeftTrigger = (byte)(sender.Buttons.HasFlag(Buttons.L) ? 0xFF : 0x00);
            XC.RightTrigger = (byte)(sender.Buttons.HasFlag(Buttons.R) ? 0xFF : 0x00);
            
            // Buttons :D
            if (sender.Buttons.HasFlag(Buttons.A)) XC.Buttons |= X360Buttons.B;
            if (sender.Buttons.HasFlag(Buttons.B)) XC.Buttons |= X360Buttons.A;
            if (sender.Buttons.HasFlag(Buttons.X)) XC.Buttons |= X360Buttons.Y;
            if (sender.Buttons.HasFlag(Buttons.Y)) XC.Buttons |= X360Buttons.X;
            if (sender.Buttons.HasFlag(Buttons.DPadUp)) XC.Buttons |= X360Buttons.Up;
            if (sender.Buttons.HasFlag(Buttons.DPadDown)) XC.Buttons |= X360Buttons.Down;
            if (sender.Buttons.HasFlag(Buttons.DPadLeft)) XC.Buttons |= X360Buttons.Left;
            if (sender.Buttons.HasFlag(Buttons.DPadRight)) XC.Buttons |= X360Buttons.Right;
            if (sender.Buttons.HasFlag(Buttons.Start)) XC.Buttons |= X360Buttons.Start;
            if (sender.Buttons.HasFlag(Buttons.Select)) XC.Buttons |= X360Buttons.Back;

            if(cVol != sender.Sliders.Volume)
            {
                SetVolume(sender.Sliders.Volume);
                cVol = sender.Sliders.Volume;
            }
            
            awe.Report(2, XC.GetReport());
        }

        
    }

}
