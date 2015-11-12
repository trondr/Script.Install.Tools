using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace Script.Install.Tools.Library.Common.IO
{

    /// <summary>
    /// Ini file operation
    /// </summary>
    public class IniFileOperation : IIniFileOperation
    {
        [DllImport("kernel32", SetLastError = true)]
        private static extern int WritePrivateProfileString(string section, string key, string val, string filePath);
        [DllImport("kernel32", SetLastError = true)]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);

        public string Read(string path, string section, string key)
        {
            if(!File.Exists(path))
            {
                throw new FileNotFoundException("Ini file not found: " + path);
            }
            var value = new StringBuilder(255);
            var n = GetPrivateProfileString(section, key, "", value, 255, path);            
            var errorCode = Marshal.GetLastWin32Error();
            if (errorCode != 0)
            {
                var errorMessage = new Win32Exception(errorCode).Message;
                throw new Win32Exception(errorCode, string.Format("Failed to read '\"{0}\"[{1}]{2}'. [section]value does not exist. Win32 error: {3} ({4}).", path, section, key, errorMessage, errorCode));
            }
            return value.ToString();
        }

        public void Write(string path, string section, string key, string value)
        {
            WritePrivateProfileString(section, key, value, path);
            var errorCode = Marshal.GetLastWin32Error();
            if (errorCode != 0)
            {

                var errorMessage = new Win32Exception(errorCode).Message;
                throw new Win32Exception(errorCode, string.Format("Failed to write '\"{0}\"[{1}]{2}={3}'. Win32 error: {4} ({5}).", path, section, key, value, errorMessage, errorCode));
            }
        }
    }
}
