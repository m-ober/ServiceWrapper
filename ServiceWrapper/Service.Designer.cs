using System.Xml;

namespace servicewrapper {
    public partial class Service {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent() {
            var doc = new XmlDocument();
            doc.Load(Config.CfgFile);

            this.ServiceName = doc.DocumentElement.SelectSingleNode("/Configuration/Service/ShortName").InnerText;
        }
    }
}