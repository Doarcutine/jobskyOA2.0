using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MyShop;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

public partial class Register : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.Page.Title = "注册界面";
        lblMessage.Text = "";       //用于显示相关信息，页面载入时默认为null
    }
    protected void btnReturn_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Login.aspx");      //跳转到登陆页面
    }
    protected void btnRegister_Click(object sender, EventArgs e)
    {
        if (txtPassword.Text.ToString().Trim() != txtConfirmps.Text.ToString().Trim())  //判断密码与确认密码是否一致
        {
            lblMessage.Text = "密码前后不一致，请重新输入";
        }
        if (txtAccount.Text.ToString() == "" || txtPassword.Text.ToString() == "")
        {
            lblMessage.Text = "用户名或者密码为空";
            return;
        }
        else
        {
            //获取输入的账号信息，并执行语句插入到数据库中
            string account, ps, name;
            account = txtAccount.Text.ToString();
            ps = txtPassword.Text.ToString();
            name = txtName.Text.ToString();
            SqlConnection cnn = DBlink.GetConnection();
            SqlCommand cmm = new SqlCommand();
            cmm.Connection = cnn;
            cmm.CommandType = CommandType.StoredProcedure;
            cmm.CommandText = "ResgisterPerson";
            cmm.Parameters.Add(new SqlParameter("@account", account));
            cmm.Parameters.Add(new SqlParameter("@name", name));
            cmm.Parameters.Add(new SqlParameter("@ps", Person.EnCrypt(ps)));
            try
            {
                cnn.Open();
                cmm.ExecuteNonQuery();
                cmm.Dispose();
                cnn.Close();
                Response.Write("<script>alert('注册成功！');location='Login.aspx'</script>");
            }
            catch
            {
                lblMessage.Text = "该账号已存在，请重新注册";
            }
        }
    }
}