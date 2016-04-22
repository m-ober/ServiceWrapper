using System;
using System.Diagnostics;
using System.Threading;

namespace ServiceWrapper
{
    public static class ServiceWrapper
    {
        private static volatile bool clean;
        private static bool executing;
        private static Process proc;

        public static void Start()
        {
            if (executing)
            {
                return;
            }

            new Thread(Execute) { IsBackground = true }.Start();
            executing = true;
        }

        public static void Stop()
        {
            clean = true;
            if (proc != null)
            {
                proc.Kill();
            }
        }

        private static void Execute()
        {
            proc = new Process();
            proc.StartInfo.WorkingDirectory = Config.Instance.Directory;
            proc.StartInfo.FileName = Config.Instance.Executable;
            proc.StartInfo.Arguments = Config.Instance.Arguments;
            proc.Start();
            proc.WaitForExit();

            executing = false;

            if (!clean)
            {
                throw new Exception("Process exited prematurely");
            }
        }
    }
}
