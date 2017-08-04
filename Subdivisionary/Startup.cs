using System.Text.RegularExpressions;
using Microsoft.Owin;
using Owin;
using Subdivisionary.Models.Collections;
using Subdivisionary.Models.Forms;

[assembly: OwinStartupAttribute(typeof(Subdivisionary.Startup))]
namespace Subdivisionary
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);

        }
    }
}
