using System;
using System.ComponentModel;
using System.Configuration.Install;
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
                    arg = args[0].ToLowerInvariant();

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
            if (!System.IO.File.Exists(Config.CfgFile))
            {
                Console.WriteLine("No configuration (service.xml) found.");
                return 1;
            }
            try
            {
                // install the service with the Windows Service Control Manager (SCM)
                ManagedInstallerClass.InstallHelper(new []{ Config.Directory });
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null && ex.InnerException is Win32Exception)
                {
                    Win32Exception wex = ex.InnerException as Win32Exception;
                    Console.WriteLine("Error 0x{0:X}", wex.ErrorCode);
                    return wex.ErrorCode;
                }
                else
                {
                    Console.WriteLine(ex.ToString());
                    return 1;
                }
            }

            return 0;
        }

        private static int UninstallService()
        {
            try
            {
                // uninstall the service from the Windows Service Control Manager (SCM)
                ManagedInstallerClass.InstallHelper(new[] { "/u", Config.Directory });
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null && ex.InnerException is Win32Exception)
                {
                    Win32Exception wex = ex.InnerException as Win32Exception;
                    Console.WriteLine("Error 0x{0:X}", wex.ErrorCode);
                    return wex.ErrorCode;
                }
                    
				Console.WriteLine(ex.ToString());
				return 1;
            }

            return 0;
        }
    }
}
