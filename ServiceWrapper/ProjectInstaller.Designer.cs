using System.Xml;

namespace servicewrapper
{
    public partial class ProjectInstaller
    {
		private System.ServiceProcess.ServiceProcessInstaller serviceProcessInstaller;
        private System.ServiceProcess.ServiceInstaller serviceInstaller;
		private System.ComponentModel.IContainer components;
		
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent() {
            var doc = new XmlDocument();
            doc.Load(Config.CfgFile);

            serviceProcessInstaller = new System.ServiceProcess.ServiceProcessInstaller() {
				Account = System.ServiceProcess.ServiceAccount.LocalSystem
				Password = null,
				Username = null				
			};
			
            serviceInstaller = new System.ServiceProcess.ServiceInstaller() {
			    DisplayName = doc.DocumentElement.SelectSingleNode("/Configuration/Service/LongName").InnerText,
				ServiceName = doc.DocumentElement.SelectSingleNode("/Configuration/Service/ShortName").InnerText,
				ServicesDependedOn = doc.DocumentElement.SelectSingleNode("/Configuration/Service/Dependencies").InnerText.Split(','),
				StartType = System.ServiceProcess.ServiceStartMode.Automatic
			}

            Installers.AddRange(new System.Configuration.Install.Installer[] {
				serviceProcessInstaller,
				serviceInstaller});
        }
    }
}