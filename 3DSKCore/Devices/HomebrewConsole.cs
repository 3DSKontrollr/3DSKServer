using System;
using Dawn.Net.Sockets;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using static _3DSKCore.Constants;

namespace _3DSKCore.Devices
{
    /// <summary>
    /// Represents a Nintendo 3DS console connected using the Homebrew App
    /// </summary>
    public class HomebrewConsole : CommonDevice
    {
        #region Events
        /// <summary>
        /// OnUpdate fires when data is received from the console.
        /// </summary>
        public event EventHandler OnUpdate;

        /// <summary>
        /// OnReport fires when report data changes
        /// </summary>
        public event EventHandler<Input.OnReportEventArgs> OnReport;
        #endregion

        #region Sockets
        internal TcpClient TcpSocket { get; set; }

        internal UdpClient UdpSocket { get; set; }
        #endregion

        #region Methods
        /// <summary>
        /// Send a Ping packet to the console
        /// </summary>
        /// <param name="Pong">Delegate to execute after retrieving "Pong" packet.</param>
        public async void Ping(Action Pong)
        {
            if (!TcpSocket.Connected) return;
            var a = new SocketAwaitable();
            a.Buffer = new ArraySegment<byte>(Packets.PingPacket);
            var result = await TcpSocket.Client.SendAsync(a);
            var res = await TcpSocket.Client.ReceiveAsync(a);
            if (a.Transferred.Array == Packets.PongPacket)
            {
                Pong();
            }
        }

        /// <summary>
        /// Processes input data sent by the console
        /// </summary>
        public async void HandleReports()
        {
            while (Status.Connected)
            {
                var report = await UdpSocket.ReceiveAsync();
                byte[] buffer = report.Buffer;
                // Check if the received data is a valid report packet
                if (buffer[0] == Packets.InputReport[0] && report.RemoteEndPoint.Address != Info.Address && buffer != Report.raw)
                {
                    var newReport = new Input.Report(buffer);
                    Report = newReport;
                    // Update button data
                    Buttons = newReport.buttons;
                    // Update Axis data
                    Axes = newReport.axes;
                    // Update slider data
                    Sliders = newReport.sliders;
                    // Fire the OnReport event
                    if (OnReport != null) OnReport(this, new Input.OnReportEventArgs() { report = newReport });
                }
                // Fire the OnUpdate event
                if (OnUpdate != null) OnUpdate(this, EventArgs.Empty);
            }
        }
        #endregion

        /// <summary>
        /// Create a new Console Object
        /// </summary>
        /// <param name="inf">Console Information</param>
        /// <param name="udp">UDP Socket</param>
        /// <param name="tcp">TCP Socket</param>
        public HomebrewConsole(ConsoleInfo inf, UdpClient udp, TcpClient tcp)
        {
            this.Info = inf;
            this.UdpSocket = udp;
            this.TcpSocket = tcp;
            this.Status = new ConsoleStatus() { Connected = true };
            HandleReports();
        }
    }
}
