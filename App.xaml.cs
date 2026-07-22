using System.Windows;
using FactoryGuardian.Database;

namespace FactoryGuardian
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            new DbInitializer().Initialize();
        }
    }
}
