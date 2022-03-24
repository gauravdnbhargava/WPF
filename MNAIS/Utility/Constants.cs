using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MNAIS.Utility
{
    public static class Constants
    {
        public static string FolderBrowsePath = Convert.ToString(Environment.SpecialFolder.Desktop);

        public static string helpFile = AppDomain.CurrentDomain.BaseDirectory + "Include\\mNAIS_User_Manual.pdf";

        public static int Invalid = -999;
    }
}
