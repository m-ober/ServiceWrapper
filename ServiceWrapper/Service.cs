using System.ServiceProcess;

namespace ServiceWrapper {
    public partial class Service : ServiceBase {
        public Service()  {
            InitializeComponent();
        }

        protected override void OnStart(string[] args) {
            ServiceWrapper.Start();
        }

        protected override void OnStop() {
            ServiceWrapper.Stop();
        }
    }
}
