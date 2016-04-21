using System.Reflection;
using System.Xml;

namespace servicewrapper
{
    partial class Service
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
            doc.Load(System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\service.xml");

            this.ServiceName =
                doc.DocumentElement.SelectSingleNode("/Configuration/Service/ShortName").InnerText;
        }

    }
}
