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
    protected void Page_Load(object sender, EventArgs e)
    {
        this.Page.Title = "登陆界面";
        //用于显示相关信息，页面载入时默认为null
        lblMessage.Text = "";
    }
    protected void btnRegister_Click(object sender, EventArgs e)
    {
        //跳转到注册页面
        Response.Redirect("~/Register.aspx");
    }
    protected void btnLogin_Click(object sender, EventArgs e)
    {
        //检验验证码是否正确
        //if (txtCheckCode.Text.ToString() != Session["checkcode"].ToString())
        //{
        //    lblMessage.Text = "验证码错误，请重新输入";
        //    return;
        //}
        //检验用户名，密码是否有为空的项
        if (txtAccount.Text.ToString() == "" || txtPassword.Text.ToString() == "")
        {
            lblMessage.Text = "用户名或密码为空";
            return;
        }
        else
        {
            if (Person.GetCountByAccount(txtAccount.Text.ToString()) > 0)   //如果在数据库中查到了与输入账号相同的用户
            {
                DataRow dr;
                dr = Person.GetPersonByAccount(txtAccount.Text.ToString().Trim());
                //判断用户名对应的密码是否正确
                if (txtPassword.Text.ToString().Trim() == Person.DeCrypt(dr["Person_Ps"].ToString()))
                {
                    Session.Add("account", txtAccount.Text.ToString());     //将账号加入到Session
                    Response.Redirect("Default.aspx");
                }
                else
                {
                    lblMessage.Text = "密码错误，请重新输入";
                }
            }
            else
            {
                lblMessage.Text = "该账号不存在，请重新输入";
            }
        }
    }
}


