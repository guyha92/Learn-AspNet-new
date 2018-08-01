using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TrainingManagmentSystem.Startup))]
namespace TrainingManagmentSystem
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
