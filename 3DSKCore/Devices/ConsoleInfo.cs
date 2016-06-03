using System;
using System.Net;

namespace _3DSKCore.Devices
{
    #region Enums
    public enum ConsoleModel
    {
        Old3DS,
        Old3DSXL,
        New3D,
        Old2DS,
        New3DSXL
    }

    public enum ConsoleRegion
    {
        Japan,
        America,
        Europe,
        Australia,
        China,
        Korea,
        Taiwan
    }

    public enum ConsoleLanguage
    {
        Japanese,
        English,
        French,
        German,
        Italian,
        Spanish,
        Chinese,
        Korean,
        Dutch,
        Portuguese,
        Russian,
        Taiwanese
    }
    #endregion

    /// <summary>
    /// Holds Information for a Nintendo 3DS console
    /// </summary>
    public class ConsoleInfo
    {
        public IPAddress Address;

        public string Username;

        public ConsoleModel Model;

        public ConsoleRegion Region;

        public ConsoleLanguage Language;

        public Version AppVersion;

        public int PlayerIndex;
    }
}
