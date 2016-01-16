using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MyShop;
using System.Data;
using System.Data.SqlClient;

public partial class AddGoods : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //页面标题
        this.Page.Title = "添加商品";
        if (Session["Account"] == null)
        {
            Response.Redirect("~/Login.aspx");
        }
        lblmessage.Text = "";       //用于显示相关信息，页面载入时默认为null
    }
    protected void btbup_Click(object sender, EventArgs e)
    {
        DataRow dr;
        dr = Person.GetPersonByAccount(Session["Account"].ToString());      //获取登陆账号的所有信息的一条记录
        string str = Person.UpLoadPicture(GoodsPicUpLoad,dr);
        if (str.Equals("不支持上传此类图片文件！") || str.Equals("未选中文件！"))     //判断返回值
        {
            lblPicPath.Text = str;
            return;
        }
        if (lblPicPath.Text != "" && !(lblPicPath.Text.ToString().Equals("不支持上传此类图片文件！") || lblPicPath.Text.ToString().Equals("未选中文件！")))
        {
            lblPicPath.Text = lblPicPath.Text + "<br/>" + str;
        }
        else
            lblPicPath.Text = str;
    }
    protected void btnadd_Click(object sender, EventArgs e)
    {
        if (txtname.Text == "" || txtcount.Text == "" || txtprice.Text == "")
        {
            lblmessage.Text = "商品信息不完全！";
        }
        else
        {
            string time = DateTime.Now.ToString();      //获取当前时间
            string imagepath="";
            if (lblPicPath.Text != "")
            {
                imagepath = "~/Image/" + lblPicPath.Text.ToString().Trim();   //获取图片的完整路径，并准备存储到数据库中
            }
            SqlConnection cn = DBlink.GetConnection();
            SqlCommand cmm = new SqlCommand();
            cmm.Connection = cn;
            cmm.CommandType = CommandType.StoredProcedure;
            cmm.CommandText = "AddGoods";
            cmm.Parameters.Add(new SqlParameter("@goodsname", txtname.Text.ToString().Trim()));
            cmm.Parameters.Add((new SqlParameter("@goodsprice", float.Parse(txtprice.Text.ToString().Trim()))));
            cmm.Parameters.Add(new SqlParameter("@goodscount", int.Parse(txtcount.Text.ToString().Trim())));
            cmm.Parameters.Add(new SqlParameter("@goodstime",time));
            cmm.Parameters.Add(new SqlParameter("@goodsimage",imagepath));
            try
            {
                cn.Open();
                cmm.ExecuteNonQuery();
                cmm.Dispose();
                cn.Close();
                Response.Write("<script>alert('商品添加成功！');location='Default.aspx'</script>");
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }
    }
}