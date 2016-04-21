namespace ServiceWrapper
{
    public class Config
    {
        private static Config instance;

        private string longName;
        private string shortName;
        private string dependencies;
        private string executable;
        private string arguments;

        private Config()
        {
            var cfg = new System.Xml.XmlDocument();
            cfg.Load(CfgFile);
            longName = cfg.DocumentElement.SelectSingleNode("/Configuration/Service/LongName").InnerText;
            shortName = cfg.DocumentElement.SelectSingleNode("/Configuration/Service/ShortName").InnerText;
            dependencies = cfg.DocumentElement.SelectSingleNode("/Configuration/Service/Dependencies").InnerText;
            executable = cfg.DocumentElement.SelectSingleNode("/Configuration/Process/Executable").InnerText;
            arguments = cfg.DocumentElement.SelectSingleNode("/Configuration/Process/Arguments").InnerText;
        }

        public static Config Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Config();
                }
                return instance;
            }
        }

        public string LongName
        {
            get { return longName; }
        }

        public string ShortName
        {
            get { return shortName; }
        }

        public string Dependencies
        {
            get { return dependencies; }
        }

        public string Executable
        {
            get { return executable; }
        }

        public string Arguments
        {
            get { return arguments; }
        }

        public string ThisAssembly
        {
            get { return System.Reflection.Assembly.GetExecutingAssembly().Location; }
        }

        public string Directory
        {
            get { return System.IO.Path.GetDirectoryName(ThisAssembly); }
        }

        public string CfgFile
        {
            get { return Directory + @"\service.xml"; }
        }
    }
}
