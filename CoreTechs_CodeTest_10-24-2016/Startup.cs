using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CoreTechs_CodeTest_10_24_2016.Startup))]
namespace CoreTechs_CodeTest_10_24_2016
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
