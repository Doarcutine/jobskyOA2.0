using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class checkcodeimage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        CheckCodeImage image = new CheckCodeImage(this.Response);
        string strCheckCode = image.GetCheckCode();
        Session.Add("checkcode", strCheckCode);
        image.CreatCheckCodeImage(strCheckCode);
    }
}