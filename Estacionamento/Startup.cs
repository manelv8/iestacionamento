using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Estacionamento.Startup))]
namespace Estacionamento
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
