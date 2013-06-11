using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.Routing;

namespace catimajans
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            YonetimRoutes(RouteTable.Routes);
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }

        void YonetimRoutes(RouteCollection routes)
        {
            routes.MapPageRoute("icerik", "{Sayfa}", "~/page.aspx", true);
            routes.MapPageRoute("alt-icerik", "{Sayfa}/{AltSayfa}", "~/page.aspx", true);
            routes.MapPageRoute("en-alt-icerik", "{Sayfa}/{AltSayfa}/{EnAltSayfa}", "~/page.aspx", true);
            routes.MapPageRoute("link", "{Sayfa}/{AltSayfa}/{EnAltSayfa}/{Icerik}", "~/page.aspx", true);
        }
    }
}