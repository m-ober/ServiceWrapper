using System;
using System.Diagnostics;
using System.Reflection;
using System.Threading;
using System.Xml;

namespace servicewrapper
{
    class ServiceWrapper
    {
        private static Process proc;
        private static Thread thread;
        private static volatile bool clean = false;

        public static void start()
        {
            thread = new Thread(new ThreadStart(execute));
            thread.Start();
        }

        public static void stop()
        {
            clean = true;
            proc.Kill();
        }

        private static void execute()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\service.xml");

            proc = new Process();
            proc.StartInfo.WorkingDirectory =
                System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
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
