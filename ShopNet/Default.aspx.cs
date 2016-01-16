using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using MyShop;

public partial class _Default : System.Web.UI.Page
{
    public string myname;
    protected void Page_Load(object sender, EventArgs e)
    {
        //Session["account"] = "0902130501";
        if (Session["account"] == null)
        {
            Response.Redirect("Login.aspx");
        }
        if (Request.QueryString["SearchInfo"] != null)
        {
            txtSearchInfo.Text = Request.QueryString["SearchInfo"].ToString();
        }
        else
        {
            //获取登陆者的姓名，并将变量绑定前台
            DataRow dr;
            dr = Person.GetPersonByAccount(Session["account"].ToString());
            this.myname = dr["Person_Name"].ToString();
            //Label的DataBind
            this.lblname.DataBind();
        }
    }
    protected void btncancel_Click(object sender, EventArgs e)
    {
        //注销按钮
        Session["account"] = null;
        Response.Redirect("Login.aspx");
    }
    protected void btnupgoods_Click(object sender, EventArgs e)
    {
        //跳转到上架商品页面
        Response.Redirect("~/AddGoods.aspx");
    }
    protected void btSearch_Click(object sender, EventArgs e)
    {
        if (txtSearchInfo.Text.ToString().Trim() == "")
        {
            lblMessage.Text = "未输入搜索内容";
            return;
        }
        else
        {
            string searchInfo = txtSearchInfo.Text.ToString();
            string value = dpSearchGoods.SelectedValue;
            string url = Request.Url.ToString().Split('?')[0];
            url = url + "?SearchInfo=" + searchInfo + "&Value=" + value;
            Response.Redirect(url);
        }
    }
    protected void btnsolu_Click(object sender, EventArgs e)
    {
        Response.Redirect("FinishDeal.aspx");
    }
    protected void btnmemory_Click(object sender, EventArgs e)
    {
        Response.Redirect("BuyGoodsMemory.aspx");
    }
}