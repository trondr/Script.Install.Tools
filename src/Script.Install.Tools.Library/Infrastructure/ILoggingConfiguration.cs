namespace Script.Install.Tools.Library.Infrastructure
{
    public interface ILoggingConfiguration
    {
        string LogDirectoryPath { get; set; }
        string LogFileName { get; set; }
    }
}
