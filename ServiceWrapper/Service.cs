using System.ServiceProcess;

namespace servicewrapper
{
    public partial class Service : ServiceBase
    {
        public Service()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            ServiceWrapper.start();
        }

        protected override void OnStop()
        {
            ServiceWrapper.stop();
        }
    }
}
