using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Biblioteka.Startup))]
namespace Biblioteka
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddIdentity<Models.ApplicationUser, IdentityRole>()
                .AddErrorDescriber<CustomIdentityErrorDescriber>();
        }
    }
}
