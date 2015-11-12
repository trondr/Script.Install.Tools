using Script.Install.Tools.Library.Common.IO;

namespace Script.Install.Tools.Library
{
    public static class IniFileOperations
    {
        private static readonly IIniFileOperation _iniFileOperation = new IniFileOperation();

        public static string Read(string path, string section, string key)
        {
            return _iniFileOperation.Read(path,section,key);
        }

        public static void Write(string path, string section, string key, string value)
        {
            _iniFileOperation.Write(path,section,key,value);
        }
    }
}
