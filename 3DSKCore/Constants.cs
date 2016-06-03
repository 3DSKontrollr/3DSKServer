using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _3DSKCore
{
    /// <summary>
    /// Constants used by 3DSKontrollr+
    /// </summary>
    public static class Constants
    {
        public static class Packets
        {
            // Ping!
            public static byte[] PingPacket = new byte[1] { 0 };
            // Pong!
            public static byte[] PongPacket = new byte[1] { 1 };
            // Input Report
            public static byte[] InputReport = new byte[1] { 2 };
        }
    }
}
