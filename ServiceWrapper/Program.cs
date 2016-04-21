using System;
using System.ComponentModel;
using System.Configuration.Install;
using System.Reflection;
using System.ServiceProcess;

namespace servicewrapper
{
    static class Program
    {

        // http://stackoverflow.com/a/9021540/1857436
        public static int Main(string[] args)
        {
            if (System.Environment.UserInteractive)
            {
                string arg = "";
                if (args.Length > 0)
                    arg = args[0].ToLowerInvariant().Substring(0, 2);

                switch (arg)
                {
                    case "/i":
                        return InstallService();

                    case "/u":
                        return UninstallService();

                    default:
                        Console.WriteLine("This application is intended to run as as service.");
                        Console.WriteLine("Use /i to install or /u to uninstall.");
                        return 1;
                }
            }
            else
            {
                ServiceBase.Run(new Service());
            }

            return 0;
        }

        private static int InstallService()
        {
            var service = new Service();

            try
            {
                // install the service with the Windows Service Control Manager (SCM)
                ManagedInstallerClass.InstallHelper(new string[] { Assembly.GetExecutingAssembly().Location });
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null && ex.InnerException.GetType() == typeof(Win32Exception))
                {
                    Win32Exception wex = (Win32Exception)ex.InnerException;
                    Console.WriteLine("Error 0x{0:X}", wex.ErrorCode);
                    return wex.ErrorCode;
                }
                else
                {
                    Console.WriteLine(ex.ToString());
                    return -1;
                }
            }

            return 0;
        }

        private static int UninstallService()
        {
            var service = new Service();

            try
            {
                // uninstall the service from the Windows Service Control Manager (SCM)
                ManagedInstallerClass.InstallHelper(new string[] { "/u", Assembly.GetExecutingAssembly().Location });
            }
            catch (Exception ex)
            {
                if (ex.InnerException.GetType() == typeof(Win32Exception))
                {
                    Win32Exception wex = (Win32Exception)ex.InnerException;
                    Console.WriteLine("Error 0x{0:X}", wex.ErrorCode);
                    return wex.ErrorCode;
                }
                else
                {
                    Console.WriteLine(ex.ToString());
                    return -1;
                }
            }

            return 0;
        }
    }
}
