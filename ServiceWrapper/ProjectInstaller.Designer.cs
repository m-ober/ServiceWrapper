using System.Xml;

namespace servicewrapper
{
    partial class ProjectInstaller
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load("service.xml");

            this.serviceProcessInstaller = new System.ServiceProcess.ServiceProcessInstaller();
            this.serviceInstaller = new System.ServiceProcess.ServiceInstaller();

            this.serviceProcessInstaller.Account = System.ServiceProcess.ServiceAccount.LocalSystem;
            this.serviceProcessInstaller.Password = null;
            this.serviceProcessInstaller.Username = null;

            this.serviceInstaller.DisplayName =
                doc.DocumentElement.SelectSingleNode("/Configuration/Service/LongName").InnerText;
            this.serviceInstaller.ServiceName =
                doc.DocumentElement.SelectSingleNode("/Configuration/Service/ShortName").InnerText;
            this.serviceInstaller.ServicesDependedOn =
                doc.DocumentElement.SelectSingleNode("/Configuration/Service/Dependencies").InnerText.Split(',');

            this.serviceInstaller.StartType = System.ServiceProcess.ServiceStartMode.Automatic;

            this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.serviceProcessInstaller,
            this.serviceInstaller});

        }

        private System.ServiceProcess.ServiceProcessInstaller serviceProcessInstaller;
        private System.ServiceProcess.ServiceInstaller serviceInstaller;
    }
}