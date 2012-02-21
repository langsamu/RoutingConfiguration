using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Sample.App_Pages
{
    public partial class TestValues : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Values.DataSource = this.RouteData.Values;
            this.Values.DataBind();

            this.Tokens.DataSource = this.RouteData.DataTokens;
            this.Tokens.DataBind();
        }
    }
}