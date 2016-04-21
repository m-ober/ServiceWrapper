using System;
using System.Diagnostics;
using System.Threading;
using System.Xml;

namespace servicewrapper
{
    class ServiceWrapper
    {
        private static Process proc;
        private static volatile bool clean = false;

        public static void Start()
        {
            new Thread(new ThreadStart(Execute)).Start();
        }

        public static void Stop()
        {
            clean = true;
            proc.Kill();
        }

        private static void Execute()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(Config.CfgFile);

            proc = new Process();
            proc.StartInfo.WorkingDirectory =
                System.IO.Path.GetDirectoryName(Config.Directory);
            proc.StartInfo.FileName =
                doc.DocumentElement.SelectSingleNode("/Configuration/Process/Executable").InnerText;
            proc.StartInfo.Arguments =
                doc.DocumentElement.SelectSingleNode("/Configuration/Process/Arguments").InnerText;

            proc.Start();
            proc.WaitForExit();
            if (!clean)
                throw new Exception("Process exited prematurely");
        }
    }
}
