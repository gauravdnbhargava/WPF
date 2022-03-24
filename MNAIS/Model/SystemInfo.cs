using System;

namespace MNAIS
{
    [Serializable]
    public class SystemInfo
    {             
        public string UserName { get; set; }
        public string SystemName { get; set; } // "Win32_Processor"
        public string ProcessorID { get; set; } // "Win32_Processor"
        public string BIOSSerialNumber { get; set; } // "Win32_BIOS"
        public string BaseBoardSerialNumber { get; set; }  // "Win32_BaseBoard"
        public DateTime CurrentTime { get; set; }
    }
}
