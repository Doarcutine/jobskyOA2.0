using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MyShop;
using System.Data;
using System.Data.SqlClient;

public partial class Judge : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {        
        this.Page.Title = "相关评论";
    }
    protected void btnreturn_Click(object sender, EventArgs e)
    {
        //返回首页
        Response.Redirect("Default.aspx");
    }
    protected void btnpublish_Click(object sender, EventArgs e)
    {
        string account = Session["Account"].ToString();     //获取当前登陆的账号
        string goodsid = Request.QueryString["Goods_ID"].ToString();        //获取商品的ID
        string time = DateTime.Now.ToString();      //获取当前时间
        string content = txtjudge.Text.ToString();          //获取评论内容
        SqlConnection cn = DBlink.GetConnection();
        SqlCommand cmm = new SqlCommand();
        cmm.Connection = cn;
        cmm.CommandType = CommandType.StoredProcedure;
        cmm.CommandText = "AddJudge";
        cmm.Parameters.Add(new SqlParameter("@goodsid", goodsid));
        cmm.Parameters.Add(new SqlParameter("@time", time));
        cmm.Parameters.Add(new SqlParameter("@content", content));
        cmm.Parameters.Add(new SqlParameter("@account", account));
        try
        {
            cn.Open();
            cmm.ExecuteNonQuery();
            cmm.Dispose();
            cn.Close();
            Response.Write("<script>alert('发表成功!');location='Judge.aspx?Goods_ID="+goodsid+"'</script>");
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }
    }
}