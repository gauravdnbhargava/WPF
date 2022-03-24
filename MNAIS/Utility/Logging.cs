using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace MNAIS
{
    public static class Logging
    {
        public static MainWindowViewModel mainVM { get; set; }

        static public void LogError(Enum ErrorType, String cClassName, String sErrorText)
        {
            using (TextWriter writer = new StreamWriter(mainVM.AdminVM.LogFilePath , true))
            {
                writer.WriteLine(DateTime.Now.ToString("yyyy-M-d HH:mm:ss") + " " + ErrorType + " " + cClassName + ":" + sErrorText);
                writer.Close();
            }
        }

        static public void LogWarning(Enum ErrorType, String cClassName, String sErrorText)
        {
            using (TextWriter writer = new StreamWriter(mainVM.AdminVM.LogFilePath, true))
            {
                writer.WriteLine(DateTime.Now.ToString("yyyy-M-d HH:mm:ss") + " " + ErrorType + " " + cClassName + ":" + sErrorText);
                writer.Close();
            }
        }

        static public void LogDebugInformation(Enum ErrorType, String cClassName, String sErrorText)
        {
            using (TextWriter writer = new StreamWriter(mainVM.AdminVM.LogFilePath, true))
            {
                writer.WriteLine(DateTime.Now.ToString("yyyy-M-d HH:mm:ss") + " " + ErrorType + " " + cClassName + ":" + sErrorText);
                writer.Close();
            }
        }
    }
}
