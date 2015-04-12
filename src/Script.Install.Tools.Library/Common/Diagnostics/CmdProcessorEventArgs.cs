using System;

namespace Script.Install.Tools.Library.Common.Diagnostics
{
    public class CmdProcessorEventArgs: EventArgs
    {        
        public CmdProcessorEventArgs(string text, int code)
        {
            Text = text;
            Code = code;
        }

        public string Text { get; set; }
        public int Code { get; set; }

    }
}