using System.Reflection;

namespace servicewrapper {
    public class Config {
        public static string Directory {
            get {
                return Assembly.GetExecutingAssembly().Location;
            }
        }
		
        public static string CfgFile {
            get {
                return System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\service.xml";
            }
        }
    }
}
