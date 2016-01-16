using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using MyShop;

public partial class BuyGoods : System.Web.UI.Page
{
    public string myname;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["account"] == null)
        {
            Response.Redirect("Login.aspx");
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
        if (Request.QueryString["Goods_ID"] != null)
        {
            //根据商品ID获取所有相关信息
            string goodsid = Request.QueryString["Goods_ID"].ToString();
            DataSet ds = Goods.GetDsGoodsByGoodsID(goodsid);
            Repeater1.DataSource = ds;
            Repeater1.DataBind();
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
    protected void btnReturn_Click(object sender, EventArgs e)
    {
        //返回首页
        Response.Redirect("Default.aspx");
    }
    protected void btnAddCar_Click(object sender, EventArgs e)
    {
        //根据商品ID获取相关信息
        string goodsid=Request.QueryString["Goods_ID"].ToString();
        DataRow dr = Goods.GetGoodsByGoodsID(goodsid);
        if (int.Parse(dr["Goods_Count"].ToString()) == 0)
        {
            lblMessage.Text = "物品暂时缺货，购买失败！";
        }
        else if (int.Parse(txtBuyCount.Text.ToString()) > int.Parse(dr["Goods_Count"].ToString()))
        {
            lblMessage.Text = "购买数量超过现有量，购买失败！";
        }
        else
        {
            //为存入数据库准备字段信息
            string buygoodsname = dr["Goods_Name"].ToString();
            string buygoodstime = DateTime.Now.ToString();
            string buygoodscount = txtBuyCount.Text.ToString();
            string buygoodsaccount = Session["Account"].ToString();
            string buygoodsprice = dr["Goods_Price"].ToString();
            string buyeachtotal = (float.Parse(dr["Goods_Price"].ToString()) * int.Parse(txtBuyCount.Text.ToString())).ToString();
            SqlConnection cn = DBlink.GetConnection();
            SqlCommand cmm = new SqlCommand();          
            cmm.Connection = cn;
            cmm.CommandType = CommandType.StoredProcedure;
            cmm.CommandText = "AddTP";        //用于实现购物车功能，暂时存储，结算后删除
            cmm.Parameters.Add(new SqlParameter("@name", buygoodsname));
            cmm.Parameters.Add(new SqlParameter("@price", buygoodsprice));
            cmm.Parameters.Add(new SqlParameter("@count", buygoodscount));
            cmm.Parameters.Add(new SqlParameter("@account", buygoodsaccount));
            cmm.Parameters.Add(new SqlParameter("@goodsid", goodsid));
            cmm.Parameters.Add(new SqlParameter("@time", buygoodstime));
            cmm.Parameters.Add(new SqlParameter("@eachtotal", buyeachtotal));
            try
            {
                cn.Open();
                cmm.ExecuteNonQuery();
                cn.Close();
                Response.Write("<script>alert('加入购物车成功！');location='Default.aspx'</script>");
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
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