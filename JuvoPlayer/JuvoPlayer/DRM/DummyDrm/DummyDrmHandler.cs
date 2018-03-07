using JuvoPlayer.Common;
using JuvoPlayer.Common.Logging;
using System;
using System.Linq;

namespace JuvoPlayer.DRM.DummyDrm
{
    public class DummyDrmHandler : IDRMHandler
    {
        private readonly ILogger Logger = LoggerManager.GetInstance().GetLogger("JuvoPlayer");
        public static readonly byte[] DummySystemId = {0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0,
                                                            0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, };
        public static readonly string DummyScheme = "dummy";

        public DummyDrmHandler()
        {
        }

        public IDRMSession CreateDRMSession(DRMInitData initData, DRMDescription drmDescription)
        {
            return DummyDrmSession.Create();
        }

        public bool SupportsSystemId(byte[] uuid)
        {
            return uuid.SequenceEqual(DummySystemId);
        }

        public bool SupportsType(string type)
        {
            return string.Equals(type, DummyScheme, StringComparison.CurrentCultureIgnoreCase);
        }

        public string GetScheme(byte[] uuid)
        {
            return DummyScheme;
        }
    }
}
