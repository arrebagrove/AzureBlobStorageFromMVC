using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(UserImageUploadAzure.Startup))]
namespace UserImageUploadAzure
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
