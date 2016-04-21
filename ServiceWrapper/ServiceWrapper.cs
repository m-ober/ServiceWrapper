using System;
using System.Diagnostics;
using System.Threading;
using System.Xml;

namespace servicewrapper {
    public static class ServiceWrapper {
        private static volatile bool clean;
		private static bool executing;
		private static Process proc;

        public static void Start() {
			if(executing) {
				return;
			}
			
            new Thread(Execute){ IsBackground = true }.Start();
			executing = true;
        }

        public static void Stop() {
            clean = true;
			if(proc != null) {
				proc.Kill();
			}
        }

        private static void Execute() {
            var doc = new XmlDocument();
            doc.Load(Config.CfgFile);

            proc = new Process(){ 
				StartInfo.WorkingDirectory = System.IO.Path.GetDirectoryName(Config.Directory),
				StartInfo.FileName = doc.DocumentElement.SelectSingleNode("/Configuration/Process/Executable").InnerText,
				StartInfo.Arguments = doc.DocumentElement.SelectSingleNode("/Configuration/Process/Arguments").InnerText,
			};
			
            proc.Start();
            proc.WaitForExit();
			
			executing = false;
			
            if (!clean) {
                throw new Exception("Process exited prematurely");
			}
        }
    }
}
